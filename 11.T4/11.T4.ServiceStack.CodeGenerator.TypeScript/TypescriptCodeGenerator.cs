// <copyright file="TypescriptCodeGenerator.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the typescript code generator class</summary>
// ReSharper disable RedundantSuppressNullableWarningExpression
namespace ServiceStack.CodeGenerator.TypeScript
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Text.RegularExpressions;
    using Clarity.Ecommerce;
    using Clarity.Ecommerce.Interfaces.Models;
    using Clarity.Ecommerce.Interfaces.Providers.Searching;
    using Clarity.Ecommerce.Models;
    using Clarity.Ecommerce.Service;
    using JetBrains.Annotations;
    using MoreLinq;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A typescript code generator.</summary>
    public partial class TypescriptCodeGenerator
    {
        /// <summary>(Immutable) The character(s) to use for Tab.</summary>
        public const string Tab = "\t";

        /// <summary>(Immutable) The sub namespace.</summary>
        public readonly string? SubNamespace;

        // ReSharper disable once InconsistentNaming
        private readonly HashSet<Type> DTOs = new();
        private readonly string[] exclusionNamespaces = Array.Empty<string>();
        private readonly string? serviceName;
        private readonly Type? usedInType;
        private readonly List<KeyValuePair<string, List<KeyValuePair<Type, List<RouteAttribute>>>>> serviceStackRoutes = new();
        private readonly List<Type> examinedTypes = new() { typeof(Type) };

        /// <summary>Initializes a new instance of the <see cref="TypescriptCodeGenerator"/> class.</summary>
        public TypescriptCodeGenerator()
        {
            DefaultExamines();
        }

        /// <summary>Initializes a new instance of the <see cref="TypescriptCodeGenerator"/> class.</summary>
        /// <param name="serviceName">   Name of the service.</param>
        /// <param name="exclNamespaces">The excl namespaces.</param>
        /// <param name="subNamespace">  The sub namespace.</param>
        public TypescriptCodeGenerator(
            string serviceName,
            string[] exclNamespaces,
            string subNamespace)
        {
            try
            {
                this.serviceName = serviceName;
                SubNamespace = subNamespace;
                exclusionNamespaces = exclNamespaces;
                usedInType = subNamespace == "admin" ? typeof(UsedInAdminAttribute)
                    : subNamespace == "brandAdmin" ? typeof(UsedInBrandAdminAttribute)
                    : subNamespace == "franchiseAdmin" ? typeof(UsedInFranchiseAdminAttribute)
                    : subNamespace == "store" ? typeof(UsedInStorefrontAttribute)
                    : subNamespace == "storeAdmin" ? typeof(UsedInStoreAdminAttribute)
                    : subNamespace == "vendorAdmin" ? typeof(UsedInVendorAdminAttribute)
                    : null; // No filtering attribute, read all
                // ReSharper restore ExpressionIsAlwaysNull
                DefaultExamines();
                var routeTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.FullName!.StartsWith("Clarity.Ecommerce.Service"))
                    .SelectMany(a => a.GetTypes()
                        .Where(t => t.CustomAttributes.Any(attr => attr.AttributeType == typeof(RouteAttribute))
                            && (usedInType == null
                                || t.CustomAttributes.Any(attr => attr.AttributeType == usedInType))))
                    .ToList();
                // Load Custom Plugins (CEF Providers that have services in them)
                var pluginsLocation = "{CEF_RootPath}Plugins";
                if (pluginsLocation.Contains("{CEF_RootPath}"))
                {
                    pluginsLocation = pluginsLocation.Replace("{CEF_RootPath}", Globals.CEFRootPath);
                }
                var clientPluginsLocation = "{CEF_RootPath}ClientPlugins";
                if (clientPluginsLocation.Contains("{CEF_RootPath}"))
                {
                    clientPluginsLocation = clientPluginsLocation.Replace("{CEF_RootPath}", Globals.CEFRootPath);
                }
                if (Directory.Exists(pluginsLocation))
                {
                    try
                    {
                        var assembliesToAdd = Directory.GetFiles(pluginsLocation, "Clarity.Ecommerce.Providers.*.dll")
                            .Select(Assembly.LoadFrom)
                            .ToList();
                        if (Directory.Exists(clientPluginsLocation))
                        {
                            assembliesToAdd.AddRange(Directory.GetFiles(clientPluginsLocation, "Clarity.Clients.*.dll")
                                .Select(Assembly.LoadFrom)
                                .ToList());
                        }
                        var additionalProviderRoutes = assembliesToAdd
                            .DistinctBy(x => x.FullName)
                            .SelectMany(x => x.GetTypes()
                                .Where(y => y.CustomAttributes.Any(z => z.AttributeType == typeof(RouteAttribute))
                                    && (usedInType == null
                                        || y.CustomAttributes.Any(z => z.AttributeType == usedInType))));
                        routeTypes.AddRange(additionalProviderRoutes);
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        // Pretty up type exceptions
                        var sb = new StringBuilder();
                        foreach (var inner in ex.LoaderExceptions)
                        {
                            sb.AppendLine(inner!.Message);
                            var fileNotFound = inner as FileNotFoundException;
                            if (!string.IsNullOrEmpty(fileNotFound?.FusionLog))
                            {
                                sb.AppendLine("Fusion Log:");
                                sb.AppendLine(fileNotFound!.FusionLog!);
                            }
                            sb.AppendLine();
                        }
                        throw new(sb.ToString(), ex);
                    }
                }
                serviceStackRoutes = FindRegisteredServiceStackRoutes(routeTypes);
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Pretty up type exceptions
                var sb = new StringBuilder();
                foreach (var inner in ex.LoaderExceptions)
                {
                    sb.AppendLine(inner!.Message);
                    var fileNotFound = inner as FileNotFoundException;
                    if (!string.IsNullOrEmpty(fileNotFound?.FusionLog))
                    {
                        sb.AppendLine("Fusion Log:");
                        sb.AppendLine(fileNotFound!.FusionLog!);
                    }
                    sb.AppendLine();
                }
                throw new(sb.ToString(), ex);
            }
        }

        /// <summary>Generates the js document.</summary>
        /// <param name="writer">        The writer.</param>
        /// <param name="type">          The type.</param>
        /// <param name="properties">    The properties.</param>
        /// <param name="commentSection">True to comment section.</param>
#pragma warning disable IDE0060, RCS1163 // Remove unused parameter
        // ReSharper disable UnusedParameter.Global
        public static void GenerateJsDoc(
            TextWriter writer,
            Type type,
            PropertyInfo[]? properties,
            bool commentSection = true)
        // ReSharper restore UnusedParameter.Global
#pragma warning restore IDE0060, RCS1163 // Remove unused parameter
        {
            ////try
            ////{
            ////    if (commentSection) { writer.WriteLine("/**"); }
            ////    var documentation = XmlDocumentationReader.XmlDocumentationReader.XMLFromType(type);
            ////    if (documentation?["summary"] != null)
            ////    {
            ////        var classSummary = documentation["summary"].InnerText.Trim();
            ////        foreach (var line in classSummary.Split('\n')) { writer.WriteLine(" * " + line); }
            ////    }
            ////    if (properties != null)
            ////    {
            ////        WriteJsDocProperties(writer, properties);
            ////    }
            ////    if (commentSection) { writer.WriteLine(" */"); }
            ////}
            ////catch (XmlDocumentationReader.NoDocumentationFoundException) { }
            ////catch (FileNotFoundException) { }
            ////catch (Exception e)
            ////{
            ////    if (commentSection) { writer.WriteLine("/*"); }
            ////    writer.WriteLine($" * Unable to generate documentation: {e}");
            ////    if (commentSection) { writer.WriteLine(" */"); }
            ////}
        }

        /// <summary>Iterate through the route types and pull out types tagged with the [Route] attribute.</summary>
        /// <param name="routeTypes">List of types of the routes.</param>
        /// <returns>An enumerator that allows foreach to be used to process the registered service stack routes in this
        /// collection.</returns>
        private static List<KeyValuePair<string, List<KeyValuePair<Type, List<RouteAttribute>>>>> FindRegisteredServiceStackRoutes(
            IEnumerable<Type> routeTypes)
        {
            var folders = new Dictionary<
                string, // Folder
                Dictionary<
                    Type, // Endpoint Class
                    List<RouteAttribute> /* Routes on the endpoint class */>>();
            foreach (var type in routeTypes)
            {
                foreach (var route in type.GetCustomAttributes<RouteAttribute>())
                {
                    var folder = route.Path.TrimStart('/').Split('/')[0];
                    if (!folders.ContainsKey(folder))
                    {
                        folders.Add(folder, new());
                    }
                    if (!folders[folder].ContainsKey(type))
                    {
                        folders[folder].Add(type, new());
                    }
                    folders[folder][type].Add(route);
                }
            }
            return folders
                .OrderBy(x => x.Key)
                .Select(x => new KeyValuePair<string, List<KeyValuePair<Type, List<RouteAttribute>>>>(
                    x.Key,
                    x.Value
                        .OrderBy(y => y.Key.Name)
                        .Select(y => new KeyValuePair<Type, List<RouteAttribute>>(
                            y.Key,
                            y.Value.OrderBy(z => z.Path).ToList()))
                        .ToList()))
                .ToList();
        }

        private void DefaultExamines()
        {
            ExamineDTOForMoreDTOWeNeed(typeof(Clarity.Ecommerce.Enums.ItemType), true);
            ExamineDTOForMoreDTOWeNeed(typeof(BaseModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(NameableBaseModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(DisplayableBaseModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(TypableBaseModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(TypeModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(StatusableBaseModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(StatusModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(ContactModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(DiscountModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(SalesItemBaseModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(SalesCollectionBaseModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(SearchViewModelBase<,>), true);
            ExamineDTOForMoreDTOWeNeed(typeof(BaseSearchModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(NameableBaseSearchModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(DisplayableBaseSearchModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(TypableBaseSearchModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(StatusableBaseSearchModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(SalesItemBaseSearchModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(SalesCollectionBaseSearchModel), true);
            ExamineDTOForMoreDTOWeNeed(typeof(AuthUserSession), true);
            ExamineDTOForMoreDTOWeNeed(typeof(AuthenticateResponse), true);
            ExamineDTOForMoreDTOWeNeed(typeof(ResponseStatus), true);
            ExamineDTOForMoreDTOWeNeed(typeof(Paging), true);
        }

        /// <summary>Responsible for transforming our .NET ServiceStack Routes into TypeScript code
        /// capable of calling those routes.</summary>
        /// <example>Here's a sample:
        /// C#:
        ///     [Route("/Dashboard/SalesGrossVsNet", "POST")]
        ///     public class GetSalesGrossVsNet : IReturn&lt;List&lt;GrossVsNetSales&gt;&gt; {
        ///         [ApiMember(...)] public string StartDate { get; set; }
        ///         [ApiMember] public string EndDate { get; set; }
        ///     }
        ///     public class GrossVsNetSales {
        ///         public DateTime Date { get; set; }
        ///         public double GrossSales {get; set;}
        ///         public double NetSales {get; set;}
        ///     }
        /// Typescript:
        ///     export interface GrossVsNetSales {
        ///         Date: Date;
        ///         GrossSales: number;
        ///         NetSales: number;
        ///     }
        ///    /* Route:  /Dashboard/SalesGrossVsNet
        ///     * Source: Clarity.Ecommerce.Framework.Dashboards.GetSalesGrossVsNet
        ///     */
        ///    GetSalesGrossVsNet = (routeParams ?: GetSalesGrossVsNetDto) =&gt; {
        ///         return this.$http&lt;Array&lt;GrossVsNetSales&gt;&gt;({
        ///             url:  [this.rootUrl, "Dashboard", "SalesGrossVsNet"].join('/'),
        ///             method: 'POST',
        ///             data:  routeParams
        ///         });
        ///     }
        /// .</example>
        private List<RouteResult> GenerateRouteClassesContents()
        {
            return serviceStackRoutes
                .ConvertAll(x => new RouteResult(folder: x, tcg: this).Process());
        }

        private List<RouteResult> GenerateRouteClassesContentsForReact()
        {
            return serviceStackRoutes
                .ConvertAll(x => new RouteResult(folder: x, tcg: this, true).ProcessForReact());
        }

        /// <summary>Gets the generate.</summary>
        /// <returns>A List{string[]}.</returns>
        [PublicAPI]
        public List<string[]> Generate()
        {
            var lines = new List<string[]>();
            var routeResults = GenerateRouteClassesContents();
            using (var writer1 = new IndentedTextWriter(new StringWriter(), Tab) { Indent = 1 })
            {
                WriteServiceInterface(writer1);
                lines.Add(new[] { "_ServiceInterface", writer1.InnerWriter.ToString()! });
            }
            using (var writer2 = new IndentedTextWriter(new StringWriter(), Tab) { Indent = 1 })
            {
                WriteServiceClass(writer2);
                lines.Add(new[] { "_ServiceClass", writer2.InnerWriter.ToString()! });
            }
            using (var writer3 = new IndentedTextWriter(new StringWriter(), Tab) { Indent = 1 })
            {
                WriteDTOClasses(writer3);
                lines.Add(new[] { "_DtoClasses", writer3.InnerWriter.ToString()! });
            }
            lines.AddRange(
                routeResults
                    .GroupBy(x => x.Folder.Key)
                    .Select(x => new[]
                    {
                        x.Key,
                        x.Aggregate("\r\n", (c, n) => c + n),
                    }));
            foreach (var routeResult in routeResults)
            {
                // Free up memory
                routeResult.Dispose();
            }
            return lines;
        }

        /// <summary>Generates for react.</summary>
        /// <returns>for react.</returns>
        public List<string[]> GenerateForReact()
        {
            var lines = new List<string[]>();
            var routeResults = GenerateRouteClassesContentsForReact();
            using (var writer1 = new IndentedTextWriter(new StringWriter(), Tab) { Indent = 0 })
            {
                WriteServiceClassForReact(writer1);
                lines.Add(new[] { "_ServiceClass", writer1.InnerWriter.ToString() }!);
            }
            using (var writer2 = new IndentedTextWriter(new StringWriter(), Tab) { Indent = 1 })
            {
                WriteDTOClassesForReact(writer2);
                lines.Add(new[] { "_DtoClasses", writer2.InnerWriter.ToString() }!);
            }
            lines.AddRange(
                routeResults
                    .GroupBy(x => x.Folder.Key)
                    .Select(x => new[]
                    {
                        x.Key,
                        x.Aggregate("\r\n", (c, n) => c + n),
                    }));
            foreach (var routeResult in routeResults)
            {
                routeResult.Dispose();
            }
            return lines;
        }

        /// <summary>Determine TS type.</summary>
        /// <param name="type">    The type.</param>
        /// <param name="isForDTO">True if this TypescriptCodeGenerator is for data transfer object.</param>
        /// <returns>A nullable string.</returns>
        public string? DetermineTSType(Type? type, bool isForDTO = false)
        {
            return type == null
                ? throw new ArgumentNullException(nameof(type))
                : DoCommonTypes(type, out var determineTsType)
                    ? determineTsType
                    : DoOtherTypes(type, isForDTO);
        }

        private static bool DoCommonTypes(Type type, out string? determineTsType)
        {
            if (type == typeof(bool))
            {
                determineTsType = "boolean";
                return true;
            }
            // ReSharper disable BuiltInTypeReferenceStyle
#pragma warning disable IDE0049 // Simplify Names
            if (type == typeof(Object) || type == typeof(object))
            {
                determineTsType = "any";
                return true;
            }
            if (type == typeof(string) || type == typeof(char))
            {
                determineTsType = "string";
                return true;
            }
            if (type == typeof(int)
                || type == typeof(byte)
                || type == typeof(short)
                || type == typeof(uint)
                || type == typeof(ulong)
                || type == typeof(ushort)
                || type == typeof(Int64)
                || type == typeof(UInt64)
                || type == typeof(double)
                || type == typeof(decimal))
            {
                determineTsType = "number";
                return true;
            }
#pragma warning restore IDE0049 // Simplify Names
            // ReSharper restore BuiltInTypeReferenceStyle
            if (type == typeof(DateTime) || type == typeof(DateTimeOffset))
            {
                determineTsType = "Date";
                return true;
            }
            if (type == typeof(Regex))
            {
                determineTsType = "RegExp";
                return true;
            }
            determineTsType = null;
            return false;
        }

        private string? DoOtherTypes(Type type, bool isForDTO)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (!isForDTO && type.HasAttribute<RouteAttribute>()
                && (usedInType == null || type.HasAttributeNamed(usedInType.Name)))
            {
                return ToInterfaceBasedDTO(type);
            }
#if NET5_0_OR_GREATER
            if (type.IsArray)
#else
            if (type.IsArray())
#endif
            {
                return "Array<" + DetermineTSType(type.GetElementType(), isForDTO) + ">";
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(HashSet<>))
            {
                return "Set<" + DetermineTSType(type.GetGenericArguments()[0], isForDTO) + ">";
            }
            if (type.IsNullableType())
            {
                // int?, bool?, etc.  Use the more underlying type
                return DetermineTSType(type.GenericTypeArguments[0], isForDTO);
            }
            if (type.IsGenericType)
            {
                return DoGenericTypeDTO(type, isForDTO);
            }
            if (type == typeof(IProductModel))
            {
                return DetermineTSType(typeof(ProductModel), isForDTO);
            }
            return ExamineDTOForMoreDTOWeNeed(type, isForDTO);
        }

        private string ToInterfaceBasedDTO(Type type)
        {
#if NET5_0_OR_GREATER
            var interfaces = type.GetInterfaces();
#else
            var interfaces = type.GetTypeInterfaces();
#endif
            if (interfaces.Any(ti => ti == typeof(IReturnVoid)))
            {
                return "void";
            }
            if (!type.HasInterface(typeof(IReturn)))
            {
                return "void";
            }
            // When a route implements IReturn<whatever> it ends up with two interfaces on it
            // One is a straight IReturn, the other is a generic IReturn
#pragma warning disable SA1305 // Field names should not use Hungarian notation
#if NET5_0_OR_GREATER
            var iReturn = Array.Find(type.GetInterfaces(), x => x.IsGenericType && x.Name.StartsWith("IReturn`"));
#else
            var iReturn = Array.Find(type.Interfaces(), x => x.IsGenericType && x.Name.StartsWith("IReturn`"));
#endif
#pragma warning restore SA1305 // Field names should not use Hungarian notation
            return iReturn == null ? "void" : DetermineTSType(iReturn.GenericTypeArguments[0]) ?? "void";
        }

        /// <summary>Executes the generic type data transfer object operation.</summary>
        /// <param name="type">    The type.</param>
        /// <param name="isForDTO">True if this TypescriptCodeGenerator is for data transfer object.</param>
        /// <returns>A string.</returns>
        private string DoGenericTypeDTO(Type type, bool isForDTO)
        {
            var genericDefinition = type.GetGenericTypeDefinition();
            // ReSharper disable once PossibleNullReferenceException
            if (genericDefinition.Name.StartsWith("SalesItemBaseModel`2"))
            {
                var typeString = DetermineTSType(type.GenericTypeArguments[1], isForDTO);
                if (typeString?.StartsWith("I") == true)
                {
                    typeString = typeString[1..];
                }
                return $"SalesItemBaseModel<{typeString}>";
            }
            if (genericDefinition.Name.StartsWith("ISalesItemBaseModel`1"))
            {
                var typeString = DetermineTSType(type.GenericTypeArguments[0], isForDTO);
                if (typeString?.StartsWith("I") == true)
                {
                    typeString = typeString[1..];
                }
                return $"SalesItemBaseModel<{typeString}>";
            }
            if (genericDefinition.Name.StartsWith("SalesCollectionBaseModel`12"))
            {
                /*
                 * SalesCollectionBaseModel<
                 *   00 TITypeModel,
                 *   01 TTypeModel,             // Pos 00
                 *   02 TIStoredFileModel,
                 *   03 TStoredFileModel,       // Pos 05
                 *   04 TIContactModel,
                 *   05 TContactModel,          // Pos 01
                 *   06 TISalesEventModel,
                 *   07 TSalesEventModel,       // Pos 02
                 *   08 TIDiscountModel,
                 *   09 TDiscountModel,         // Pos 03
                 *   10 TIItemDiscountModel,
                 *   11 TItemDiscountModel>     // Pos 04
                 */
                /*
                 * SalesCollectionBaseModelT<
                 *   00 TTypeModel,
                 *   01 TContactModel,
                 *   02 TSalesEventModel extends SalesEventBaseModel,
                 *   03 TDiscountModel extends AppliedDiscountBaseModel,
                 *   04 TItemDiscountModel extends AppliedDiscountBaseModel,
                 *   05 TFileModel extends AmAStoredFileRelationshipTableModel>
                 */
                var typeString = new[]
                {
                    DetermineTSType(type.GenericTypeArguments[01], isForDTO),
                    DetermineTSType(type.GenericTypeArguments[05], isForDTO),
                    DetermineTSType(type.GenericTypeArguments[07], isForDTO),
                    DetermineTSType(type.GenericTypeArguments[09], isForDTO),
                    DetermineTSType(type.GenericTypeArguments[11], isForDTO),
                    DetermineTSType(type.GenericTypeArguments[03], isForDTO),
                };
                for (var i = 0; i < typeString.Length; i++)
                {
                    if (typeString[i]?.StartsWith("I") == true)
                    {
                        typeString[i] = typeString[i]?[1..];
                    }
                }
                return $"SalesCollectionBaseModelT<{typeString[00]},{typeString[01]},{typeString[02]},{typeString[03]},{typeString[04]},{typeString[05]}>";
            }
            if (genericDefinition.Name.StartsWith("CEFActionResponse`1"))
            {
                var typeString = DetermineTSType(type.GenericTypeArguments[0], isForDTO);
                if (typeString?.StartsWith("I") == true)
                {
                    typeString = typeString[1..];
                }
                // T because we can't have two interface declarations with same name with
                // only one having a type argument
                return $"CEFActionResponseT<{typeString}>";
            }
            if (genericDefinition.Name.StartsWith("SearchViewModelBase")
                || genericDefinition.Name.StartsWith("ISearchViewModelBase"))
            {
                var typeString = DetermineTSType(type.GenericTypeArguments[0], isForDTO);
                // if (typeString.StartsWith("I")) { typeString = typeString.Substring(1); }
                var typeString2 = DetermineTSType(type.GenericTypeArguments[1], isForDTO);
                // if (typeString2.StartsWith("I")) { typeString2 = typeString2.Substring(1); }
                return $"SearchViewModelBase<{typeString},{typeString2}>";
            }
            if (genericDefinition.Name.StartsWith("Report`"))
            {
                return $"Report<{DetermineTSType(type.GenericTypeArguments[0], isForDTO)}>";
            }
            if (genericDefinition.Name.StartsWith("IEqualityComparer`"))
            {
                return $"IEqualityComparer<{DetermineTSType(type.GenericTypeArguments[0], isForDTO)}>";
            }
            if (genericDefinition.Name.StartsWith("KeyCollection"))
            {
                return $"KeyCollection<{DetermineTSType(type.GenericTypeArguments[0], isForDTO)}>";
            }
            if (genericDefinition.Name.StartsWith("ValueCollection"))
            {
                return $"ValueCollection<{DetermineTSType(type.GenericTypeArguments[0], isForDTO)}>";
            }
            if (genericDefinition.Name.StartsWith("PagedResultsBase`"))
            {
                return $"PagedResultsBase<{DetermineTSType(type.GenericTypeArguments[0], isForDTO)}>";
            }
            if (genericDefinition.Name.StartsWith("KeyValuePair`"))
            {
                return $"KeyValuePair<{DetermineTSType(type.GenericTypeArguments[0], isForDTO)},{DetermineTSType(type.GenericTypeArguments[1], isForDTO)}>";
            }
            if (genericDefinition.Name.StartsWith("ValueTuple`"))
            {
                return $"{{ item1: {DetermineTSType(type.GenericTypeArguments[0], isForDTO)}, item2: {DetermineTSType(type.GenericTypeArguments[1], isForDTO)} }}";
            }
            if (genericDefinition.Name.StartsWith("Dictionary") || genericDefinition.Name.StartsWith("IDictionary") || genericDefinition.Name.StartsWith("ConcurrentDictionary"))
            {
#if NET5_0_OR_GREATER
                return $"cefalt.{(string.IsNullOrWhiteSpace(SubNamespace) ? string.Empty : SubNamespace + ".")}Dictionary<{DetermineTSType(type.GetGenericArguments()[1], isForDTO)}>";
#else
                return $"cefalt.{(string.IsNullOrWhiteSpace(SubNamespace) ? string.Empty : SubNamespace + ".")}Dictionary<{DetermineTSType(type.GenericTypeArguments()[1], isForDTO)}>";
#endif
            }
            if (genericDefinition.Name.StartsWith("IdentityUser"))
            {
#if NET5_0_OR_GREATER
                return $"IdentityUser<{DetermineTSType(type.GetGenericArguments()[0], isForDTO)}"
                                + $", {DetermineTSType(type.GetGenericArguments()[1], isForDTO)}"
                                + $", {DetermineTSType(type.GetGenericArguments()[2], isForDTO)}"
                                + $", {DetermineTSType(type.GetGenericArguments()[3], isForDTO)}>";
#else
                return $"IdentityUser<{DetermineTSType(type.GenericTypeArguments()[0], isForDTO)}"
                                + $", {DetermineTSType(type.GenericTypeArguments()[1], isForDTO)}"
                                + $", {DetermineTSType(type.GenericTypeArguments()[2], isForDTO)}"
                                + $", {DetermineTSType(type.GenericTypeArguments()[3], isForDTO)}>";
#endif
            }
            if (genericDefinition.Name.StartsWith("List`")
                || genericDefinition.Name.StartsWith("IList`")
                || genericDefinition.Name.StartsWith("ICollection`")
                || genericDefinition.Name.StartsWith("IEnumerable`"))
            {
                return $"Array<{DetermineTSType(type.GenericTypeArguments[0], isForDTO)}>";
            }
            // ReSharper disable once PossibleNullReferenceException
            throw new(
                $"Error processing {type.Name} - Unknown generic type {type.GetGenericTypeDefinition().Name}");
        }

        /// <summary>Examine data transfer object for more data transfer object we need.</summary>
        /// <param name="type">    The type.</param>
        /// <param name="isForDTO">True if this TypescriptCodeGenerator is for data transfer object.</param>
        /// <returns>A string.</returns>
        public string ExamineDTOForMoreDTOWeNeed(Type? type, bool isForDTO)
        {
            if (type is null)
            {
                return "any";
            }
            if (examinedTypes.Contains(type))
            {
                return DTOs.Contains(type) ? type.Name : "any";
            }
            if (type.Namespace == null
                || DTOs.Contains(type)
                || exclusionNamespaces.Any(ns => type.Namespace.StartsWith(ns)))
            {
                examinedTypes.Add(type);
                // We put our models in a DTOs module in typescript
                return DTOs.Contains(type) ? type.Name : "any";
            }
            if (AlwaysSkipTypes.All(x => x.Name != type.Name))
            {
                DTOs.Add(type);
            }
            // Since the DTO might expose other DTOs we need to examine all of the return types of properties
            foreach (var property in type.Properties()
                .Where(p => p.CanRead
                    && !p.HasAttribute<IgnoreDataMemberAttribute>()
                    && !p.HasAttribute<JsonIgnoreAttribute>()
                    && p.Name != "noCache"))
            {
                if (property.GetMethod!.ReturnType == typeof(Type))
                {
                    // Don't process 'Type' itself
                    continue;
                }
                if (AlwaysSkipTypes.Contains(property.GetMethod.ReturnType))
                {
                    continue;
                }
                if (examinedTypes.Contains(property.GetMethod.ReturnType))
                {
                    continue;
                }
                DetermineTSType(property.GetMethod.ReturnType, isForDTO);
                examinedTypes.Add(property.GetMethod.ReturnType);
            }
            examinedTypes.Add(type);
            // We put our models in a DTOs module in typescript
            return DTOs.Contains(type) ? type.Name : "any";
        }

        ////private void WriteJsDocProperties(TextWriter writer, IEnumerable<PropertyInfo> properties)
        ////{
        ////    foreach (var property in properties)
        ////    {
        ////        try
        ////        {
        ////            var paramDocs = XmlDocumentationReader.XmlDocumentationReader.XMLFromMember(property);
        ////            writer.WriteLine($" * @property {{{DetermineTsType(property.GetMethod.ReturnType)}}} {property.Name}"
        ////                + (paramDocs?["summary"] != null ? $" - {paramDocs["summary"]?.InnerText.Trim()}" : string.Empty));
        ////        }
        ////        catch
        ////        {
        ////            // Do Nothing
        ////        }
        ////    }
        ////}
    }
}
