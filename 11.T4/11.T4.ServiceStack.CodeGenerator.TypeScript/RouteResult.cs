// <copyright file="RouteResult.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the route result class</summary>
// ReSharper disable RedundantSuppressNullableWarningExpression
namespace ServiceStack.CodeGenerator.TypeScript
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using global::CodeGenerator;

    /// <summary>Encapsulates the result of a route.</summary>
    /// <seealso cref="IDisposable"/>
    public class RouteResult : IDisposable
    {
        /// <summary>Initializes a new instance of the <see cref="RouteResult"/> class.</summary>
        /// <param name="folder"> Pathname of the folder.</param>
        /// <param name="tcg">    The tcg.</param>
        /// <param name="isReact">True if this RouteResult is react.</param>
        public RouteResult(
            KeyValuePair<string, List<KeyValuePair<Type, List<RouteAttribute>>>> folder,
            TypescriptCodeGenerator tcg,
            bool isReact = false)
        {
            TCG = tcg;
            Folder = folder;
            IsReact = isReact;
            ClassWriter = new(new StringWriter(), TypescriptCodeGenerator.Tab) { Indent = isReact ? 0 : 1 };
            DTOsWriter = new(new StringWriter(), TypescriptCodeGenerator.Tab) { Indent = isReact ? 0 : 1 };
            if (!isReact)
            {
                return;
            }
            ImportWriter = new(new StringWriter(), TypescriptCodeGenerator.Tab) { Indent = 0 };
            RequiredDTOs = new();
            RequiredFromShared = new();
        }

        ~RouteResult()
        {
            Dispose(false);
        }

        private IndentedTextWriter ClassWriter { get; }

        private IndentedTextWriter DTOsWriter { get; }

        private List<string>? RequiredDTOs { get; }

        private List<string>? RequiredFromShared { get; }

        private bool IsReact { get; set; }

        private IndentedTextWriter? ImportWriter { get; }

        private string[] FromShared { get; } =
        {
            "CEFActionResponse",
            "CEFActionResponseT",
            "Dictionary",
            "KeyValuePair",
            "Guid",
            "SalesItemBaseModel",
        };

        /// <inheritdoc/>
        public override string ToString()
        {
            var output = new StringBuilder();
            if (IsReact)
            {
                output.Append(ImportWriter!.InnerWriter.ToString()!.Trim());
                output.AppendLine();
                output.AppendLine();
            }
            output.Append(DTOsWriter.InnerWriter!);
            if (!IsReact)
            {
                output.AppendLine();
            }
            var classWriter = ClassWriter.InnerWriter.ToString()!
                // Clean out lines that just have a tab on them
                .Replace(Environment.NewLine + TypescriptCodeGenerator.Tab + Environment.NewLine, Environment.NewLine + Environment.NewLine)
                // Clean out blank lines before closing braces
                .Replace(Environment.NewLine + Environment.NewLine + "}", Environment.NewLine + "}");
            if (IsReact)
            {
                classWriter = classWriter.Trim();
            }
            output.Append(classWriter);
            return output.ToString();
        }

        /// <summary>Gets the pathname of the folder.</summary>
        /// <value>The pathname of the folder.</value>
        public KeyValuePair<string, List<KeyValuePair<Type, List<RouteAttribute>>>> Folder { get; }

        // ReSharper disable once InconsistentNaming
        private TypescriptCodeGenerator TCG { get; }

        /// <summary>Process this RouteResult.</summary>
        /// <returns>A RouteResult.</returns>
        public RouteResult Process()
        {
            IsReact = false;
            DTOsWriter.WriteLine();
            ClassWriter.WriteLine(
                TypescriptCodeGenerator.Tab + "export class " + Folder.Key + " extends ServiceStackRoute {");
            ClassWriter.Indent++;
            foreach (var type in Folder.Value)
            {
                WriteRouteType(type.Key, Folder);
            }
            ClassWriter.Indent--;
            ClassWriter.WriteLine("}");
            return this;
        }

        /// <summary>Process for react.</summary>
        /// <returns>A RouteResult.</returns>
        public RouteResult ProcessForReact()
        {
            IsReact = true;
            ClassWriter.WriteLine();
            ClassWriter.WriteLine("export class " + Folder.Key + " {");
            ClassWriter.Indent++;
            foreach (var type in Folder.Value)
            {
                WriteRouteTypeForReact(type.Key, Folder);
            }
            ClassWriter.Indent--;
            ClassWriter.WriteLine("}");
            ImportWriter!.WriteLine("import axios from \"../axios\";");
            ImportWriter.WriteLine("import cvApi from \"./cvApi\";");
            ImportWriter.WriteLine();
            var routeNames = Folder.Value
                .Select(x => x.Key.Name)
                .Distinct();
            var finalDTOs = RequiredDTOs!
                .Distinct()
                .Where(x => !routeNames.Contains(x))
                .ToList();
            var uploadResponseDetected = false;
            var transactionResponseDetected = false;
            if (finalDTOs.Count > 0)
            {
                ImportWriter.WriteLine("import {");
                ImportWriter.Indent++;
                foreach (var requiredDTO in finalDTOs.Distinct())
                {
                    if (requiredDTO == "IUploadResponse")
                    {
                        uploadResponseDetected = true;
                        continue;
                    }
                    if (requiredDTO == "TransactionResponse")
                    {
                        transactionResponseDetected = true;
                        continue;
                    }
                    if (requiredDTO is "{ item1: number" or "item2: number }")
                    {
                        // It's improperly reading out a ValueTuple, skip it
                        continue;
                    }
                    ImportWriter.WriteLine($"{requiredDTO},");
                }
                ImportWriter.Indent--;
                ImportWriter.WriteLine("} from \"./cvApi._DtoClasses\";");
                ImportWriter.WriteLine();
            }
            // There will always be at least the IHttpPromise import
            ImportWriter.WriteLine("import {");
            ImportWriter.Indent++;
            foreach (var shared in RequiredFromShared!.Distinct())
            {
                ImportWriter.WriteLine($"{shared},");
            }
            if (uploadResponseDetected)
            {
                ImportWriter.WriteLine("IUploadResponse,");
            }
            if (transactionResponseDetected)
            {
                ImportWriter.WriteLine("TransactionResponse,");
            }
            ImportWriter.WriteLine("IHttpPromise");
            ImportWriter.Indent--;
            ImportWriter.WriteLine("} from \"./cvApi.shared\";");
            ImportWriter.WriteLine(string.Empty);
            return this;
        }

        private static void ReleaseUnmanagedResources()
        {
        }

        private void WriteRouteTypeForReact(
            Type type,
            KeyValuePair<string, List<KeyValuePair<Type, List<RouteAttribute>>>> folder)
        {
            try
            {
                var returnTsType = TCG.DetermineTSType(type);
                var routeClass = folder.Value.Single(x => x.Key == type);
                foreach (var route in routeClass.Value)
                {
                    WriteMethodHeader(ClassWriter, type, route, returnTsType);
                    var cg = new RouteCodeGeneration(route, type, returnTsType, true);
                    cg.ParseRoutePath(TCG);
                    cg.ProcessRouteProperties(TCG);
                    TCG.ExamineDTOForMoreDTOWeNeed(cg.RouteType, true);
                    if (cg.RouteType.BaseType != null)
                    {
                        TCG.ExamineDTOForMoreDTOWeNeed(cg.RouteType.BaseType, true);
                    }
                    foreach (var verb in cg.Verbs)
                    {
                        WriteTypescriptMethodForReact(
                            cg,
                            verb,
                            cg.Verbs.Length > 1,
                            verb == cg.Verbs[0]);
                    }
                    break;
                }
            }
            catch (Exception e)
            {
                ClassWriter.WriteLine("// BUG ERROR! Processing " + folder.Key + " - " + type.Name);
                ClassWriter.WriteLine("//\t" + e.Message);
                DTOsWriter.WriteLine("// BUG ERROR! Processing " + folder.Key + " - " + type.Name);
                DTOsWriter.WriteLine("//\t" + e.Message);
            }
        }

        private void WriteRouteType(
            Type type,
            KeyValuePair<string, List<KeyValuePair<Type, List<RouteAttribute>>>> folder)
        {
            try
            {
                var returnTsType = TCG.DetermineTSType(type);
                var routeClass = folder.Value.Single(x => x.Key == type);
                if (routeClass.Value.Count > 1)
                {
                    ////ClassWriter.WriteLine("// WARNING! '" + type + "' exports multiple routes. Typescript does"
                    ////     + " not support operator overloading and this operation is not supported. Make separate"
                    ////     + " routes instead. Printing the one with the highest Priority value");
                }
                foreach (var route in routeClass.Value)
                {
                    WriteMethodHeader(ClassWriter, type, route, returnTsType);
                    var cg = new RouteCodeGeneration(route, type, returnTsType);
                    // Translate the path into a coded URL
                    // We may have tokens like {ID} in the route
                    cg.ParseRoutePath(TCG);
                    // Generate code for route properties
                    cg.ProcessRouteProperties(TCG);
                    TCG.ExamineDTOForMoreDTOWeNeed(cg.RouteType, true);
                    if (cg.RouteType.BaseType != null)
                    {
                        TCG.ExamineDTOForMoreDTOWeNeed(cg.RouteType.BaseType, true);
                    }
                    foreach (var verb in cg.Verbs)
                    {
                        WriteTypescriptMethodForAngJS(
                            cg,
                            verb,
                            cg.Verbs.Length > 1,
                            verb == cg.Verbs[0]);
                    }
                    // Only do the one with highest priority
                    break;
                }
            }
            catch (Exception e)
            {
                ClassWriter.WriteLine("// BUG ERROR! Processing " + folder.Key + " - " + type.Name);
                ClassWriter.WriteLine("//\t" + e.Message);
                DTOsWriter.WriteLine("// BUG ERROR! Processing " + folder.Key + " - " + type.Name);
                DTOsWriter.WriteLine("//\t" + e.Message);
            }
        }

        private void WriteMethodHeader(TextWriter writer, Type type, RouteAttribute route, string? returnTsType)
        {
            writer.WriteLine("/**");
            var td = new TypeDeterminer(type);
            if (!string.IsNullOrEmpty(route.Summary))
            {
                writer.WriteLine(" * " + route.Summary);
            }
            if (!string.IsNullOrEmpty(route.Verbs) && route.Verbs != "GET")
            {
                writer.WriteLine(
                    " * @param {@link cef."
                    + (string.IsNullOrWhiteSpace(TCG.SubNamespace) ? string.Empty : TCG.SubNamespace + ".")
                    + "api."
                    + type.Name
                    + "Dto} routeParams - The route parameters as a Body Object");
            }
            writer.WriteLine(" * @generatedByCSharpType " + type.Namespace + "." + type.Name);
            if (!string.IsNullOrEmpty(route.Path))
            {
                writer.WriteLine(" * @path <API Root>" + route.Path);
            }
            if (!string.IsNullOrEmpty(route.Verbs))
            {
                writer.WriteLine(" * @verb " + route.Verbs);
            }
            if (!string.IsNullOrEmpty(route.Notes))
            {
                writer.WriteLine(" * @remarks " + route.Notes);
            }
            if (route.Priority > 0)
            {
                writer.WriteLine(" * @priority " + route.Priority);
            }
            if (!string.IsNullOrWhiteSpace(returnTsType))
            {
                writer.WriteLine(" * @returns {ng.IHttpPromise<" + returnTsType + ">}");
            }
            if (td.IsDeprecated)
            {
                writer.WriteLine(" * @deprecated " + td.DeprecatedMessage);
            }
            TypescriptCodeGenerator.GenerateJsDoc(writer, type, null, false);
            writer.WriteLine(" * @public");
            writer.WriteLine(" */");
        }

        private List<string> GetRequiredTypesFromDto(string tsType)
        {
            var result = new List<string>();
            if (tsType.Contains('<'))
            {
                // Recurse through generic children
                var index = tsType.IndexOf('<');
                var end = tsType.LastIndexOf('>');
                var genericType = tsType.Substring(index + 1, end - index - 1);
                result.AddRange(GetRequiredTypesFromDto(genericType));
                // Cut out the generic part (ie "<ProductModel>") and just parse the rest
                var genericSubsection = tsType.Substring(index, end - index + 1);
                tsType = tsType.Replace(genericSubsection, string.Empty);
            }
            // Break apart comma-delimited stuff (ie from something like Dictionary<string, number>)
            if (tsType.Contains(','))
            {
                var subTypes = tsType.Split(',')
                    .Select(x => x.Trim());
                foreach (var subType in subTypes)
                {
                    result.AddRange(GetRequiredTypesFromDto(subType));
                }
                return result;
            }
            switch (tsType)
            {
                // TS default types
                case "number":
                case "string":
                case "void":
                case "boolean":
                case "any":
                case "Array":
                case "Set":
                case "Date":
                {
                    break;
                }
                default:
                {
                    if (!FromShared.Contains(tsType))
                    {
                        result.Add(tsType);
                    }
                    break;
                }
            }
            return result;
        }

        private List<string> GetRequiredTypesFromShared(
            string tsType)
        {
            var result = new List<string>();
            if (tsType.Contains('<'))
            {
                // Recurse through its children
                var index = tsType.IndexOf('<');
                var end = tsType.LastIndexOf('>');
                var genericType = tsType.Substring(index + 1, end - index - 1);
                result.AddRange(GetRequiredTypesFromShared(genericType));
                // Cut out the generic part (ie "<ProductModel>") and just parse the rest
                var genericSubsection = tsType.Substring(index, end - index + 1);
                tsType = tsType.Replace(genericSubsection, string.Empty);
            }
            // Break apart comma-delimited stuff (ie from something like Dictionary<string, number>)
            if (tsType.Contains(','))
            {
                var subTypes = tsType.Split(',')
                    .Select(x => x.Trim());
                foreach (var subType in subTypes)
                {
                    result.AddRange(GetRequiredTypesFromShared(subType));
                }
            }
            else
            {
                if (FromShared.Contains(tsType))
                {
                    result.Add(tsType);
                }
            }
            return result;
        }

        private void WriteTypescriptMethodForReact(
            RouteCodeGeneration cg,
            string verb,
            bool includeVerbNameInMethod,
            bool writeInputDTO)
        {
            ClassWriter.Write($"{cg.RouteType.Name + (includeVerbNameInMethod ? "_" + verb : string.Empty)}");
            ClassWriter.Write(" = (");
            ClassWriter.Write(cg.MethodParameters.Join(", "));
            if (cg.RouteInputPropertyLines.Count > 0)
            {
                WriteRouteParams(cg, writeInputDTO);
            }
            var returnType = cg.ReturnTsType?.Replace("cefalt.store.", string.Empty) ?? "any";
            RequiredDTOs?.AddRange(GetRequiredTypesFromDto(returnType));
            RequiredFromShared?.AddRange(GetRequiredTypesFromShared(returnType));
            ClassWriter.WriteLine($"): IHttpPromise<{returnType}> =>");
            ClassWriter.Indent++;
            ClassWriter.Write($"axios.{verb.ToLower()}([");
            ClassWriter.Write(cg.UrlPath.Join(", "));
            ClassWriter.Write("].join(\"/\")");
            if (cg.RouteInputPropertyLines.Count <= cg.MethodParameters.Count)
            {
                if (!string.Equals(verb, "GET", StringComparison.InvariantCultureIgnoreCase))
                {
                    // We're done, close the function and exit
                    ClassWriter.WriteLine(");");
                }
                else
                {
                    ClassWriter.WriteLine(", { params: { _: cvApi.cacheCounter } });");
                }
                ClassWriter.Indent--;
                ClassWriter.WriteLine(string.Empty);
                return;
            }
            if (string.Equals(verb, "GET", StringComparison.InvariantCultureIgnoreCase)
                || string.Equals(verb, "DELETE", StringComparison.InvariantCultureIgnoreCase)
                // Special case, this is a multi-part form endpoint that has to use it as a body like DELETE does
                || string.Equals(cg.RouteType.Name, "UploadStoredFile", StringComparison.InvariantCultureIgnoreCase))
            {
                // We need to write a body-style argument
                ClassWriter.WriteLine(",");
                ClassWriter.WriteLine("{");
                ClassWriter.Indent++;
                ClassWriter.WriteLine(
                    string.Equals(verb, "GET", StringComparison.InvariantCultureIgnoreCase)
                        ? "params: { ...routeParams, _: cvApi.cacheCounter }"
                        : "data: routeParams");
                ClassWriter.Indent--;
                ClassWriter.WriteLine("});");
                ClassWriter.Indent--;
                ClassWriter.WriteLine(string.Empty);
                return;
            }
            // For any other verb type, we need to pass the routeParams as an argument to axios
            ClassWriter.WriteLine(", routeParams);");
            ClassWriter.Indent--;
            ClassWriter.WriteLine(string.Empty);
        }

        /// <summary>Writes a typescript method.</summary>
        /// <param name="cg">                     The cg.</param>
        /// <param name="verb">                   The verb.</param>
        /// <param name="includeVerbNameInMethod">True to include, false to exclude the verb name in method.</param>
        /// <param name="writeInputDTO">          True to write input dto.</param>
        private void WriteTypescriptMethodForAngJS(
            RouteCodeGeneration cg,
            string verb,
            bool includeVerbNameInMethod,
            bool writeInputDTO)
        {
            ClassWriter.Write(cg.RouteType.Name + (includeVerbNameInMethod ? "_" + verb : string.Empty));
            ClassWriter.Write(" = (");
            // Optional parameters must come last in typescript
            for (var i = 0; i < cg.MethodParameters.Count; i++)
            {
                if (i > 0)
                {
                    ClassWriter.Write(", ");
                }
                ClassWriter.Write(cg.MethodParameters[i]);
            }
            if (cg.RouteInputPropertyLines.Count > 0)
            {
                WriteRouteParams(cg, writeInputDTO);
            }
            ClassWriter.Write(")");
            /*
            for (int i = 0; i < cg.MethodParametersOptional.Count; i++)
            {
                if (i > 0 || cg.MethodParameters.Count > 0) { ClassWriter.Write(", "); }
                ClassWriter.Write(cg.MethodParametersOptional[i]);
            }
            ClassWriter.Write(");
            */
            ClassWriter.Write(" => ");
            WriteRouteMethodBodyForAngJS(cg, verb);
        }

        private void WriteRouteMethodBodyForAngJS(RouteCodeGeneration cg, string verb)
        {
            ClassWriter.Write("this.$http<" + cg.ReturnTsType + ">({\r\n\t\t\t");
            ClassWriter.Indent++;
            // Write out the url array
            ClassWriter.Write("url: [");
            for (var i = 0; i < cg.UrlPath.Count; i++)
            {
                if (i > 0)
                {
                    ClassWriter.Write(", ");
                }
                ClassWriter.Write(cg.UrlPath[i]);
            }
            ClassWriter.WriteLine("].join(\"/\"),");
            ClassWriter.WriteLine("method: \"" + verb.ToUpper() + "\",");
            if (cg.RouteInputPropertyLines.Count > cg.MethodParameters.Count)
            {
                ClassWriter.WriteLine(
                    string.Equals(verb, "GET", StringComparison.InvariantCultureIgnoreCase)
                        ? "params: routeParams"
                        : "data: routeParams");
            }
            ClassWriter.Indent--;
            ClassWriter.WriteLine("});");
            ClassWriter.WriteLineNoTabs(string.Empty);
        }

        /// <summary>Writes a route parameters.</summary>
        /// <param name="cg">           The cg.</param>
        /// <param name="writeInputDTO">true to write input data transfer object.</param>
        private void WriteRouteParams(
            RouteCodeGeneration cg,
            bool writeInputDTO)
        {
            if (cg.MethodParameters.Count > 0)
            {
                ClassWriter.Write(", ");
            }
            ClassWriter.Write("routeParams");
            if (cg.RouteInputHasOnlyOptionalParams)
            {
                ClassWriter.Write("?");
            }
            ClassWriter.Write(": " + cg.RouteInputDTOName);
            if (!writeInputDTO)
            {
                return;
            }
            if (!IsReact)
            {
                DTOsWriter.Indent++;
            }
            TCG.WriteBlockHeader(
                writer: DTOsWriter,
                dto: cg.RouteType,
                isInheritedClass: cg.RouteType.BaseType != null && cg.RouteType.BaseType != typeof(object),
                td: new(cg.RouteType),
                addDTOToName: true,
                isReact: IsReact,
                imports: IsReact ? RequiredDTOs : null);
            if (!IsReact)
            {
                DTOsWriter.Indent--;
            }
            DTOsWriter.Indent++;
            foreach (var line in cg.RouteInputPropertyLines.Where(x => !string.IsNullOrEmpty(x) && x != "Inherited"))
            {
                // Parse the type off and add it to the lists for React
                var output = line;
                if (IsReact && !output.StartsWith("/*"))
                {
                    output = output.Replace("cefalt.store.", string.Empty);
                    var start = output.IndexOf(':') + 1;
                    var end = output.IndexOf(';');
                    var type = output[start..end].Trim();
                    RequiredDTOs!.AddRange(GetRequiredTypesFromDto(type));
                    RequiredFromShared!.AddRange(GetRequiredTypesFromShared(type));
                }
                DTOsWriter.WriteLine(output);
            }
            DTOsWriter.Indent--;
            DTOsWriter.WriteLine("}");
            if (IsReact)
            {
                DTOsWriter.WriteLine();
            }
        }

        /// <summary>Releases the unmanaged resources used by the RouteResult and optionally releases the managed
        /// resources.</summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only
        ///                         unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (!disposing)
            {
                return;
            }
            // ReSharper disable ConstantConditionalAccessQualifier
            ClassWriter?.Dispose();
            DTOsWriter?.Dispose();
            // ReSharper restore ConstantConditionalAccessQualifier
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
