using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using ServiceStack.Formats;
using ServiceStack.Logging;
using ServiceStack.Markdown;
using ServiceStack.Text;
using ServiceStack.Web;

namespace ServiceStack.Support.Markdown
{
    public class MarkdownPage : IExpirable, IViewPage
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MarkdownPage));

        public const string ModelName = "Model";

        public MarkdownPage()
        {
            ExecutionContext = new();
            Dependents = new();
        }

        public MarkdownPage(MarkdownFormat markdown, string fullPath, string name, string contents)
            : this(markdown, fullPath, name, contents, MarkdownPageType.ViewPage)
        {
        }

        public MarkdownPage(MarkdownFormat markdown, string fullPath, string name, string contents, MarkdownPageType pageType)
            : this()
        {
            Markdown = markdown;
            FilePath = fullPath;
            Name = name;
            Contents = contents;
            PageType = pageType;
        }

        public MarkdownFormat Markdown { get; set; }

        private int timesRun;
        private bool hasCompletedFirstRun;

        public MarkdownPageType PageType { get; set; }
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public string HtmlContents { get; set; }
        public string Template { get; set; }
        public string DirectiveTemplate { get; set; }
        public EvaluatorExecutionContext ExecutionContext { get; private set; }

        public DateTime? LastModified { get; set; }
        public List<IExpirable> Dependents { get; private set; }

        public DateTime? GetLastModified()
        {
            if (!hasCompletedFirstRun)
            {
                return null;
            }

            var lastModified = LastModified;
            foreach (var expirable in Dependents)
            {
                if (!expirable.LastModified.HasValue)
                {
                    continue;
                }

                if (!lastModified.HasValue || expirable.LastModified > lastModified)
                {
                    lastModified = expirable.LastModified;
                }
            }
            return lastModified;
        }

        public string GetTemplatePath()
        {
            return DirectiveTemplate ?? Template;
        }

        private Evaluator evaluator;
        public Evaluator Evaluator
        {
            get
            {
                if (evaluator == null)
                {
                    throw new InvalidOperationException("evaluator not ready");
                }

                return evaluator;
            }
        }

        private int exprSeq;

        public int GetNextId()
        {
            return exprSeq++;
        }

        public TemplateBlock[] MarkdownBlocks { get; set; }
        public TemplateBlock[] HtmlBlocks { get; set; }

        private Exception initException;
        private TemplateBlock lastBlockProcessed;
        private readonly object readWriteLock = new();
        private bool isBusy;
        public void Reload(string contents, DateTime lastModified)
        {
            lock (readWriteLock)
            {
                try
                {
                    isBusy = true;

                    Contents = contents;
                    LastModified = lastModified;

                    initException = null;
                    exprSeq = 0;
                    timesRun = 0;
                    ExecutionContext = new();
                    Compile(force: true);
                }
                catch (Exception ex)
                {
                    initException = ex;
                }
                isBusy = false;
                Monitor.PulseAll(readWriteLock);
            }
        }

        public bool IsCompiled { get; set; }

        public void Compile(bool force = false)
        {
            if (IsCompiled && !force)
            {
                return;
            }

            var sw = Stopwatch.StartNew();

            try
            {
                if (!typeof(MarkdownViewBase).IsAssignableFromType(Markdown.MarkdownBaseType))
                {
                    throw new ConfigurationErrorsException(
                        "Config.MarkdownBaseType must inherit from MarkdownViewBase");
                }

                if (Contents.IsNullOrEmpty())
                {
                    return;
                }

                var markdownStatements = new List<StatementExprBlock>();

                var markdownContents = StatementExprBlock.Extract(Contents, markdownStatements);

                MarkdownBlocks = markdownContents.CreateTemplateBlocks(markdownStatements).ToArray();

                var htmlStatements = new List<StatementExprBlock>();
                var htmlContents = StatementExprBlock.Extract(Contents, htmlStatements);

                HtmlContents = Markdown.Transform(htmlContents);
                HtmlBlocks = HtmlContents.CreateTemplateBlocks(htmlStatements).ToArray();

                SetTemplateDirectivePath();

                IsCompiled = true;
                Log.DebugFormat("Compiled {0} in {1}ms", FilePath, sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                Log.Error("Error compiling {0}".Fmt(FilePath), ex);
                throw;
            }
        }

        private void SetTemplateDirectivePath()
        {
            var templateDirective = HtmlBlocks.FirstOrDefault(
                x => (x as DirectiveBlock)?.TemplatePath != null);
            if (templateDirective == null)
            {
                return;
            }

            DirectiveTemplate = ((DirectiveBlock)templateDirective).TemplatePath;
        }

        public void Write(TextWriter textWriter, PageContext pageContext)
        {
            if (textWriter == null)
            {
                throw new ArgumentNullException(nameof(textWriter));
            }

            pageContext ??= new(this, new(), true);

            var blocks = pageContext.RenderHtml ? HtmlBlocks : MarkdownBlocks;

            if (Interlocked.Increment(ref timesRun) == 1)
            {
                lock (readWriteLock)
                {
                    try
                    {
                        isBusy = true;

                        ExecutionContext.BaseType = Markdown.MarkdownBaseType;
                        ExecutionContext.TypeProperties = Markdown.MarkdownGlobalHelpers;

                        pageContext.MarkdownPage = this;
                        var initHtmlContext = pageContext.Create(this, true);
                        var initMarkdownContext = pageContext.Create(this, false);

                        foreach (var block in HtmlBlocks)
                        {
                            lastBlockProcessed = block;
                            block.DoFirstRun(initHtmlContext);
                        }
                        foreach (var block in MarkdownBlocks)
                        {
                            lastBlockProcessed = block;
                            block.DoFirstRun(initMarkdownContext);
                        }

                        evaluator = ExecutionContext.Build();

                        foreach (var block in HtmlBlocks)
                        {
                            lastBlockProcessed = block;
                            block.AfterFirstRun(evaluator);
                        }
                        foreach (var block in MarkdownBlocks)
                        {
                            lastBlockProcessed = block;
                            block.AfterFirstRun(evaluator);
                        }

                        AddDependentPages(blocks);

                        lastBlockProcessed = null;
                        initException = null;
                        hasCompletedFirstRun = true;
                    }
                    catch (Exception ex)
                    {
                        initException = ex;
                        throw;
                    }
                    finally
                    {
                        isBusy = false;
                    }
                }
            }

            lock (readWriteLock)
            {
                while (isBusy)
                {
                    Monitor.Wait(readWriteLock);
                }
            }

            if (initException != null)
            {
                timesRun = 0;
                throw initException;
            }

#if NETSTANDARD1_6
            textWriter.Write(pageContext.MarkdownPage.Contents);
            return;
#else
            MarkdownViewBase instance = null;
            if (evaluator != null)
            {
                instance = (MarkdownViewBase)evaluator.CreateInstance();

                pageContext.ScopeArgs.TryGetValue(ModelName, out var model);

                instance.Init(Markdown.AppHost, this, pageContext.ScopeArgs, model, pageContext.RenderHtml);
                instance.ViewEngine = Markdown;
            }

            foreach (var block in blocks)
            {
                block.Write(instance, textWriter, pageContext.ScopeArgs);
            }

            if (instance != null)
            {
                instance.OnLoad();
            }
#endif
        }

        private void AddDependentPages(IEnumerable<TemplateBlock> blocks)
        {
            foreach (var block in blocks)
            {
                var exprBlock = block as MethodStatementExprBlock;
                if (exprBlock?.DependentPageName == null)
                {
                    continue;
                }

                var page = Markdown.GetViewPage(exprBlock.DependentPageName);
                if (page != null)
                {
                    Dependents.Add(page);
                }
            }

            if (DirectiveTemplate != null
                && Markdown.MasterPageTemplates.TryGetValue(DirectiveTemplate, out var template))
            {
                Dependents.Add(template);
            }
            if (Template != null
                && Markdown.MasterPageTemplates.TryGetValue(Template, out template))
            {
                Dependents.Add(template);
            }

        }
    }
}