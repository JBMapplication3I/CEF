using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using ServiceStack.Html;
using ServiceStack.Logging;
using ServiceStack.Markdown;
using ServiceStack.Text;

namespace ServiceStack.Support.Markdown
{
	public class PageContext
	{
		public PageContext() { }

		public PageContext(MarkdownPage markdownPage, Dictionary<string, object> scopeArgs, bool renderHtml)
		{
			MarkdownPage = markdownPage;
			ScopeArgs = scopeArgs ?? new Dictionary<string, object>();
			RenderHtml = renderHtml;
		}

		public MarkdownPage MarkdownPage { get; set; }
		public Dictionary<string, object> ScopeArgs { get; set; }
		public bool RenderHtml { get; set; }

		public PageContext Create(MarkdownPage markdownPage, bool renderHtml)
		{
			return new(markdownPage, ScopeArgs, renderHtml);
		}
	}


	public abstract class TemplateBlock : ITemplateWriter
	{
		protected MarkdownPage Page { get; set; }

		protected Evaluator Evaluator { get; set; }

		public bool IsNested { get; set; }

		protected bool WriteRawHtml { get; set; }

		protected bool RenderHtml { get; set; }

		//protected PageContext PageContext { get; set; }

		protected Dictionary<string, object> ScopeArgs { get; set; }

		protected PageContext CreatePageContext()
		{
			return new(Page, ScopeArgs, RenderHtml);
		}

		public void DoFirstRun(PageContext pageContext)
		{
			//this.PageContext = pageContext;
			Page = pageContext.MarkdownPage;
			RenderHtml = pageContext.RenderHtml;
			ScopeArgs = pageContext.ScopeArgs;

			OnFirstRun();
		}

		public void AfterFirstRun(Evaluator evaluator)
		{
			Evaluator = evaluator;

			OnAfterFirstRun();
		}

		protected virtual void OnFirstRun() { }
		protected virtual void OnAfterFirstRun() { }

		public void AddEvalItem(EvaluatorItem evalItem)
		{
			Page.ExecutionContext.Items.Add(evalItem);
		}

		private const string EscapedStartTagArtefact = "<p>^";

		public string TransformHtml(string markdownText)
		{
			var html = Page.Markdown.Transform(markdownText);

			return CleanHtml(html);
		}

		public static string CleanHtml(string html)
		{
			// ^ is added before ^<html></html> tags to trick Markdown into not thinking its a HTML
			// Start tag so it doesn't skip it and encodes the inner body as normal.
			// We need to Un markdown encode the result i.e. <p>^<div id="searchresults"></p>

			var pos = 0;
			var hasEscapedTags = false;
			while ((pos = html.IndexOf(EscapedStartTagArtefact, pos, StringComparison.CurrentCulture)) != -1)
			{
				hasEscapedTags = true;

				var endPos = html.IndexOf("</p>", pos, StringComparison.CurrentCulture);
				if (endPos == -1)
                {
                    return html; //Unexpected Error so skip
                }

                html = html.Substring(0, endPos)
					   + html.Substring(endPos + 4);

				pos = endPos;
			}

			if (hasEscapedTags)
            {
                html = html.Replace(EscapedStartTagArtefact, "");
            }

            return html;
		}

		public string Transform(string markdownText)
		{
			return RenderHtml ? TransformHtml(markdownText) : markdownText;
		}

		public abstract void Write(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs);
	}

	public class TextBlock : TemplateBlock
	{
		public TextBlock(string content)
		{
			Content = CleanHtml(content);
		}

		public string Content { get; set; }

		public override void Write(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
		{
			textWriter.Write(Content);
		}
	}

	public class VarReferenceBlock : TemplateBlock
	{
		private readonly string varName;

		public VarReferenceBlock(string varName)
		{
			this.varName = varName;
		}

		public override void Write(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
		{
            scopeArgs.TryGetValue(varName, out var value);

            if (value == null)
            {
                return;
            }

            textWriter.Write(value);
		}
	}

	public class MemberExprBlock : TemplateBlock
	{
		private static ILog Log = LogManager.GetLogger(typeof(MemberExprBlock));

		private string memberExpr;
		private readonly string modelMemberExpr;
		private readonly string varName;

		private bool ReferencesSelf => modelMemberExpr == null;

		public MemberExprBlock(string memberExpr)
		{
			try
			{
				this.memberExpr = memberExpr;
				varName = memberExpr.GetVarName();
				modelMemberExpr = varName != memberExpr
					? memberExpr.Substring(varName.Length + 1)
					: null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private Func<object, string> valueFn;
		private Func<string> staticValueFn;

		protected override void OnFirstRun()
		{
			base.OnFirstRun();

            if (ScopeArgs.TryGetValue(varName, out var memberExprValue))
            {
                InitializeValueFn(memberExprValue);
            }
            else
            {
                staticValueFn = DataBinder.CompileStaticAccessToString(memberExpr);
            }
        }

		private void InitializeValueFn(object memberExprValue)
		{
			valueFn = ReferencesSelf
				? Convert.ToString
				: DataBinder.CompileToString(memberExprValue.GetType(), modelMemberExpr);
		}

		public override void Write(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
		{
            if (!scopeArgs.TryGetValue(varName, out var memberExprValue))
            {
                if (staticValueFn != null)
                {
                    var strValue = staticValueFn();
                    textWriter.Write(PclExportClient.Instance.HtmlEncode(strValue));
                }
                else
                {
                    textWriter.Write(memberExpr);
                }
                return;
            }

            if (memberExprValue == null)
            {
                return;
            }

            try
			{
				if (memberExprValue is MvcHtmlString)
				{
					textWriter.Write(memberExprValue);
					return;
				}
				if (valueFn == null)
				{
					InitializeValueFn(memberExprValue);
				}
				var strValue = ReferencesSelf
					? Convert.ToString(memberExprValue)
					: valueFn(memberExprValue);

				textWriter.Write(PclExportClient.Instance.HtmlEncode(strValue));
			}
			catch (Exception ex)
			{
				Log.Error("MemberExprBlock: " + ex.Message, ex);
			}
		}
	}

	public class StatementExprBlock : TemplateBlock
	{
		public StatementExprBlock(string condition, string statement)
		{
			Condition = condition;
			Statement = statement;
			ChildBlocks = TypeConstants<TemplateBlock>.EmptyArray;
		}

		public string Condition { get; set; }
		public string Statement { get; set; }

		public TemplateBlock[] ChildBlocks { get; set; }

		protected virtual void Prepare(List<StatementExprBlock> allStatements)
		{
			if (Statement.IsNullOrEmpty())
            {
                return;
            }

            var parsedStatement = Extract(Statement, allStatements);

			var childBlocks = parsedStatement.CreateTemplateBlocks(allStatements);
			childBlocks.ForEach(x => x.IsNested = true);

			RemoveTrailingNewLineIfProceedsStatement(childBlocks);

			ChildBlocks = childBlocks.ToArray();
		}

		internal static void RemoveTrailingNewLineIfProceedsStatement(List<TemplateBlock> childBlocks)
		{
			if (childBlocks.Count < 2)
            {
                return;
            }

            var lastIndex = childBlocks.Count - 1;
			if (childBlocks[lastIndex - 1] is not StatementExprBlock)
            {
                return;
            }

            if (childBlocks[lastIndex] is not TextBlock textBlock)
            {
                return;
            }

            if (textBlock.Content == "\r\n")
			{
				childBlocks.RemoveAt(lastIndex);
			}
		}

		public int Id { get; set; }

		protected void OnFirstRun(bool applyToChildren)
		{
			if (applyToChildren)
            {
                OnFirstRun();
            }
            else
            {
                base.OnFirstRun();
            }
        }

		protected override void OnFirstRun()
		{
			base.OnFirstRun();

			Id = Page.GetNextId();

			var pageContext = CreatePageContext();
			foreach (var templateBlock in ChildBlocks)
			{
				templateBlock.DoFirstRun(pageContext);
			}
		}

		protected override void OnAfterFirstRun()
		{
			base.OnAfterFirstRun();

			foreach (var templateBlock in ChildBlocks)
			{
				templateBlock.AfterFirstRun(Evaluator);
			}
		}

		public override void Write(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
		{
			WriteInternal(instance, textWriter, scopeArgs);
		}

		private void WriteInternal(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
		{
			foreach (var templateBlock in ChildBlocks)
			{
				templateBlock.Write(instance, textWriter, scopeArgs);
			}
		}

		public static string Extract(string content, List<StatementExprBlock> allStatements)
		{
			var sb = StringBuilderCache.Allocate();

			var initialCount = allStatements.Count;
			int pos;
			var lastPos = 0;
			while ((pos = content.IndexOf('@', lastPos)) != -1)
			{
				var peekChar = content.Substring(pos + 1, 1);
				var isComment = peekChar == "*";
				if (isComment)
				{
					var endPos = content.IndexOf("*@", pos);
					if (endPos == -1)
                    {
                        throw new InvalidDataException("Unterminated Comment at charIndex: " + pos);
                    }

                    lastPos = endPos + 2;
					continue;
				}
				if (peekChar == "@")
				{
					sb.Append('@');
					pos += 2;
					lastPos = pos;
					continue;
				}

				var contentBlock = content.Substring(lastPos, pos - lastPos);

				var startPos = pos;
				pos++; //@

				var statementExpr = content.GetNextStatementExpr(ref pos);
				if (statementExpr != null)
				{
					contentBlock = contentBlock.TrimLineIfOnlyHasWhitespace();
					sb.Append(contentBlock);

					if (statementExpr is MethodStatementExprBlock)
                    {
                        sb.Append(' '); //ensure a spacer between method blocks
                    }

                    statementExpr.Prepare(allStatements);
					allStatements.Add(statementExpr);
					var placeholder = "@" + TemplateExtensions.StatementPlaceholderChar + allStatements.Count;
					sb.Append(placeholder);
					lastPos = pos;
				}
				else
				{
					sb.Append(contentBlock);

					sb.Append('@');
					lastPos = startPos + 1;
				}
			}

			if (lastPos != content.Length - 1)
			{
				var lastBlock = lastPos == 0 ? content : content.Substring(lastPos);
				sb.Append(lastBlock);
			}

			return allStatements.Count > initialCount ? StringBuilderCache.ReturnAndFree(sb) : content;
		}

		protected void WriteStatement(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
		{
			if (IsNested)
			{
				//Write Markdown
				WriteInternal(instance, textWriter, scopeArgs);
			}
			else
			{
				//Buffer Markdown output before converting and writing HTML
				var sw = StringWriterCacheAlt.Allocate();
				WriteInternal(instance, sw, scopeArgs);

				var markdown = StringWriterCacheAlt.ReturnAndFree(sw);
				var html = Transform(markdown);
				textWriter.Write(html);
			}
		}
	}

	public class DirectiveBlock : StatementExprBlock
	{
		public Type BaseType { get; set; }

		public Type[] GenericArgs { get; set; }

		public Dictionary<string, Type> Helpers { get; set; }

		public string TemplatePath { get; set; }

		protected Dictionary<string, Func<object, object>> VarDeclarations { get; set; }

		public Type GetType(string typeName)
		{
			var type = Evaluator.FindType(typeName)
				?? AssemblyUtils.FindType(typeName);
			if (type == null)
			{
				var parts = typeName.Split(new[] { '<', '>' });
				if (parts.Length > 1)
				{
					var genericTypeName = parts[0];
					var genericArgNames = parts[1].Split(',');
					var genericDefinitionName = genericTypeName + "`" + genericArgNames.Length;
					var genericDefinition = Type.GetType(genericDefinitionName);
					var argTypes = genericArgNames.Select(AssemblyUtils.FindType).ToArray();
					var concreteType = genericDefinition.GetCachedGenericType(argTypes);
					type = concreteType;
				}
				else
				{
					throw new TypeLoadException("Could not load type: " + typeName);
				}
			}

			return type;
		}

		public DirectiveBlock(string directive, string line)
			: base(directive, null)
		{
			if (directive == null)
            {
                throw new ArgumentNullException(nameof(directive));
            }

            if (line == null)
            {
                throw new ArgumentNullException(nameof(line));
            }

            directive = directive.ToLower();
			line = line.Trim();

			if (directive == "model")
			{
				BaseType = typeof(MarkdownViewBase<>);
				GenericArgs = new[] { GetType(line) };
			}
			else if (directive == "inherits")
			{
				var parts = line.Split(new[] { '<', '>' })
					.Where(x => !x.IsNullOrEmpty()).ToArray();

				var isGenericType = parts.Length >= 2;

				BaseType = isGenericType ? GetType(parts[0] + "`1") : GetType(parts[0]);

				if (isGenericType)
				{
					GenericArgs = new[] { GetType(parts[1]) };
				}
			}
			else if (directive == "helper")
			{
				var helpers = line.Split(',');
				Helpers = new();

				foreach (var helper in helpers)
				{
					var parts = helper.Split(':');
					if (parts.Length != 2)
                    {
                        throw new InvalidDataException(
							"Invalid helper directive, should be 'TagName: Helper.Namespace.And.Type'");
                    }

                    var tagName = parts[0].Trim();
					var typeName = parts[1].Trim();

					var helperType = GetType(typeName);

					Helpers[tagName] = helperType ?? throw new InvalidDataException("Unable to resolve helper type: " + typeName);
				}
			}
			else if (directive == "template" || directive == "layout")
			{
				TemplatePath = line.Trim().Trim('"');
			}
		}

		protected override void OnFirstRun()
		{
			base.OnFirstRun();

			if (BaseType != null)
            {
                Page.ExecutionContext.BaseType = BaseType;
            }

            Page.ExecutionContext.GenericArgs = GenericArgs;

			if (Helpers != null)
			{
				foreach (var helper in Helpers)
				{
					Page.ExecutionContext.TypeProperties[helper.Key] = helper.Value;
				}
			}
		}

		public override void Write(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs) { }
	}


	public class ForEachStatementExprBlock : StatementExprBlock
	{
		public ForEachStatementExprBlock(string condition, string statement)
			: base(condition, statement)
		{
			Prepare();
		}

		public string EnumeratorName { get; set; }
		public string MemberExpr { get; set; }
		public string MemberVarName { get; set; }

		private void Prepare()
		{
			var parts = Condition.SplitOnWhiteSpace();
			if (parts.Length < 3)
            {
                throw new InvalidDataException("Invalid foreach condition: " + Condition);
            }

            var i = parts[0] == "var" ? 1 : 0;
			EnumeratorName = parts[i++];
			if (parts[i++] != "in")
            {
                throw new InvalidDataException("Invalid foreach 'in' condition: " + Condition);
            }

            MemberExpr = parts[i++];
			MemberVarName = MemberExpr.GetVarName();
		}

		private object GetModel(Dictionary<string, object> scopeArgs)
		{
            if (!scopeArgs.TryGetValue(MemberVarName, out var model))
            {
                throw new ArgumentException(MemberVarName + " does not exist");
            }

            return model;
		}

		private IEnumerable GetMemberExprEnumerator(object model)
		{
            if (getMemberFn(model) is not IEnumerable memberExprEnumerator)
            {
                throw new ArgumentException(MemberExpr + " is not an IEnumerable");
            }

            return memberExprEnumerator;
		}

		private Func<object, object> getMemberFn;
		protected override void OnFirstRun()
		{
			base.OnFirstRun(false);
			var model = GetModel(ScopeArgs);

			getMemberFn = DataBinder.Compile(model.GetType(), MemberExpr);
			var memberExprEnumerator = GetMemberExprEnumerator(model);

			var pageContext = CreatePageContext();
			foreach (var item in memberExprEnumerator)
			{
				ScopeArgs[EnumeratorName] = item;
				foreach (var templateBlock in ChildBlocks)
				{
					templateBlock.DoFirstRun(pageContext);
				}
			}
		}

		public override void Write(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
		{
			var model = GetModel(scopeArgs);
			var memberExprEnumerator = GetMemberExprEnumerator(model);

			if (IsNested)
			{
				//Write Markdown
				foreach (var item in memberExprEnumerator)
				{
					scopeArgs[EnumeratorName] = item;
					base.Write(instance, textWriter, scopeArgs);
				}
			}
			else
			{
				//Buffer Markdown output before converting and writing HTML
				var sw = StringWriterCacheAlt.Allocate();
				foreach (var item in memberExprEnumerator)
				{
					scopeArgs[EnumeratorName] = item;
					base.Write(instance, sw, scopeArgs);
				}

				var markdown = StringWriterCacheAlt.ReturnAndFree(sw);
				var renderedMarkup = Transform(markdown);
				textWriter.Write(renderedMarkup);
			}
		}
	}

	public class SectionStatementExprBlock : StatementExprBlock
	{
		public string SectionName { get; set; }

		public SectionStatementExprBlock(string condition, string statement)
			: base(condition, statement)
		{
			Prepare();
		}

		public void Prepare()
		{
			SectionName = Condition.Trim();
		}

		public override void Write(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
		{
			//Don't output anything, capture all output and store it in scopeArgs[SectionName]
			var sb = new StringBuilder();
			using (var sw = new StringWriter(sb))
			{
				base.Write(instance, sw, scopeArgs);
			}

			var markdown = sb.ToString();
			var renderedMarkup = Transform(markdown);
			scopeArgs[SectionName] = MvcHtmlString.Create(renderedMarkup);
		}
	}


	public abstract class EvalExprStatementBase : StatementExprBlock
	{
		protected EvalExprStatementBase(string condition, string statement)
			: base(condition, statement)
		{
		}

		protected Type ReturnType = typeof(string);
		private string[] paramNames;
		protected string CodeGenMethodName { get; set; }

		public string[] GetParamNames(Dictionary<string, object> scopeArgs)
		{
			return paramNames ??= scopeArgs.Keys.ToArray();
		}

		protected override void OnFirstRun()
		{
			base.OnFirstRun();

			CodeGenMethodName = "EvalExpr_" + Id;

			var exprParams = GetExprParams();
			var evalItem = new EvaluatorItem(ReturnType, CodeGenMethodName, Condition, exprParams);

			AddEvalItem(evalItem);
		}

		protected Dictionary<string, Type> GetExprParams()
		{
			var exprParams = new Dictionary<string, Type>();
			paramNames = GetParamNames(ScopeArgs);
			var paramValues = GetParamValues(ScopeArgs);
			for (var i = 0; i < paramNames.Length; i++)
			{
				var paramName = paramNames[i];
				var paramValue = paramValues[i];

				exprParams[paramName] = paramValue != null ? paramValue.GetType() : typeof(object);
			}
			return exprParams;
		}

		protected List<object> GetParamValues(IDictionary<string, object> scopeArgs, bool defaultToNullValues)
		{
			var results = new List<object>();
			foreach (var paramName in paramNames)
			{
                if (!scopeArgs.TryGetValue(paramName, out var paramValue) && !defaultToNullValues)
                {
                    throw new ArgumentException("Unresolved param " + paramName + " in " + Condition);
                }

                results.Add(paramValue);
			}
			return results;
		}

		protected List<object> GetParamValues(IDictionary<string, object> scopeArgs)
		{
			return GetParamValues(scopeArgs, false);
		}

		public T Evaluate<T>(Dictionary<string, object> scopeArgs, bool defaultToNullValues)
		{
			var paramValues = GetParamValues(scopeArgs, defaultToNullValues);
			return (T)Evaluator.Evaluate(CodeGenMethodName, paramValues.ToArray());
		}

		public T Evaluate<T>(Dictionary<string, object> scopeArgs)
		{
			return Evaluate<T>(scopeArgs, true);
		}
	}

	public class VarStatementExprBlock : EvalExprStatementBase
	{
		private string varName;
		private string memberExpr;

		public VarStatementExprBlock(string directive, string line)
			: base(line, null)
		{
			if (directive != "var")
            {
                throw new ArgumentException("Expected 'var' got: " + directive);
            }

            ReturnType = typeof(object);
		}

		protected override void OnFirstRun()
		{
			if (varName != null)
            {
                return;
            }

            var declaration = Condition.TrimEnd().TrimEnd(';');

			var parts = declaration.Split('=');
			if (parts.Length != 2)
            {
                throw new InvalidDataException(
					"Invalid var declaration, should be '@var varName = {MemberExpression} [, {VarDeclaration}]' was: " + declaration);
            }

            varName = parts[0].Trim();
			memberExpr = parts[1].Trim();

			Condition = memberExpr;

			const string methodName = "resolveVarType";
			var exprParams = GetExprParams();
			var evaluator = new Evaluator(ReturnType, Condition, methodName, exprParams);
			var result = evaluator.Evaluate(methodName, GetParamValues(ScopeArgs).ToArray());
			ScopeArgs[varName] = result;
			if (result != null)
            {
                ReturnType = result.GetType();
            }

            base.OnFirstRun();
		}

		public override void Write(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
		{
			//Resolve and add to ScopeArgs
			var resultCondition = Evaluate<object>(scopeArgs, true);
			scopeArgs[varName] = resultCondition;
		}
	}

	public class IfStatementExprBlock : EvalExprStatementBase
	{
		public string ElseStatement { get; set; }

		public IfStatementExprBlock(string condition, string statement, string elseStatement)
			: base(condition, statement)
		{
			ReturnType = typeof(bool);
			ElseStatement = elseStatement;
			ElseChildBlocks = TypeConstants<TemplateBlock>.EmptyArray;
		}

		public TemplateBlock[] ElseChildBlocks { get; set; }

		protected override void Prepare(List<StatementExprBlock> allStatements)
		{
			base.Prepare(allStatements);

			if (ElseStatement.IsNullOrEmpty())
            {
                return;
            }

            var parsedStatement = Extract(ElseStatement, allStatements);

			var elseChildBlocks = parsedStatement.CreateTemplateBlocks(allStatements);
			elseChildBlocks.ForEach(x => x.IsNested = true);

			RemoveTrailingNewLineIfProceedsStatement(elseChildBlocks);

			ElseChildBlocks = elseChildBlocks.ToArray();
		}

		public override void Write(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
		{
			var resultCondition = Evaluate<bool>(scopeArgs);
			if (resultCondition)
			{
				WriteStatement(instance, textWriter, scopeArgs);
			}
			else
			{
				if (ElseStatement != null && ElseChildBlocks.Length > 0)
				{
					WriteElseStatement(instance, textWriter, scopeArgs);
				}
			}
		}

		protected override void OnFirstRun()
		{
			base.OnFirstRun();

			var pageContext = CreatePageContext();
			foreach (var templateBlock in ElseChildBlocks)
			{
				templateBlock.DoFirstRun(pageContext);
			}
		}

		//TODO: DRY IT
		protected void WriteElseStatement(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
		{
			if (IsNested)
			{
				//Write Markdown
				foreach (var templateBlock in ElseChildBlocks)
				{
					templateBlock.Write(instance, textWriter, scopeArgs);
				}
			}
			else
			{
				//Buffer Markdown output before converting and writing HTML
				var sw = StringWriterCacheAlt.Allocate();
				foreach (var templateBlock in ElseChildBlocks)
				{
					templateBlock.Write(instance, sw, scopeArgs);
				}

				var markdown = StringWriterCacheAlt.ReturnAndFree(sw);
				var html = Transform(markdown);
				textWriter.Write(html);
			}
		}
	}

	public class MethodStatementExprBlock : EvalExprStatementBase
	{
		public MethodStatementExprBlock(string methodExpr, string condition, string statement)
			: base(condition, statement)
		{
			this.methodExpr = methodExpr;
		}

		private readonly string methodExpr;
		protected override void OnFirstRun()
		{
			Prepare(Page);
			base.OnFirstRun();
		}

		public string DependentPageName { get; private set; }

		private void Prepare(MarkdownPage markdownPage)
		{
			var rawMethodExpr = methodExpr.Replace("Html.", "");
			if (rawMethodExpr == "Partial")
			{
				DependentPageName = Condition.ExtractContents("\"", "\"");
			}
			WriteRawHtml = rawMethodExpr == "Raw";

			var parts = methodExpr.Split('.');
			if (parts.Length > 2)
            {
                throw new ArgumentException("Unable to resolve method: " + methodExpr);
            }

            var usesBaseType = parts.Length == 1;
			var typePropertyName = parts[0];
			var methodName = usesBaseType ? parts[0] : parts[1];

			Type type = null;
#if !NETSTANDARD1_6
			if (typePropertyName == "Html")
			{
				type = markdownPage.ExecutionContext.BaseType.HasGenericType()
					   ? typeof(HtmlHelper<>)
					   : typeof(HtmlHelper);
			}
#endif
			if (type == null)
			{
				type = usesBaseType
					? markdownPage.ExecutionContext.BaseType
					: markdownPage.Markdown.MarkdownGlobalHelpers.TryGetValue(typePropertyName, out type) ? type : null;
			}

			if (type == null)
            {
                throw new InvalidDataException(string.Format(
					"Unable to resolve type '{0}'. Check type exists in Config.MarkdownBaseType or Page.Markdown.MarkdownGlobalHelpers",
					typePropertyName));
            }

            try
			{
				var mi = methodName == "Partial"
					? type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
						.FirstOrDefault(m => m.GetParameters().Length == 2 && m.Name == methodName)
					: type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
#if !NETSTANDARD1_6
				if (mi == null)
				{
					mi = HtmlHelper.GetMethod(methodName);
					if (mi == null)
                    {
                        throw new ArgumentException("Unable to resolve method '" + methodExpr + "' on type " + type.GetOperationName());
                    }
                }
#endif

				ReturnType = mi.ReturnType;
			}
			catch (Exception)
			{
				throw;
			}

			var isMemberExpr = Condition.IndexOf('(') != -1;
			if (!isMemberExpr || WriteRawHtml)
			{
				Condition = methodExpr + "(" + Condition + ")";
			}
		}

		public override void Write(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
		{
			var paramValues = GetParamValues(scopeArgs);
			var result = Evaluator.Evaluate(instance, CodeGenMethodName, paramValues.ToArray());
			if (result == null)
            {
                return;
            }

            string strResult;

            if (result is MvcHtmlString mvcString)
			{
				WriteRawHtml = true;
				strResult = mvcString.ToHtmlString();
			}
			else
			{
				strResult = result as string ?? Convert.ToString(result);
			}

			if (!WriteRawHtml)
            {
                strResult = PclExportClient.Instance.HtmlEncode(strResult);
            }

            textWriter.Write(strResult);
		}
	}
}