// <copyright file="EndpointHelpers.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the endpoint helpers class</summary>
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global, InconsistentNaming, MemberCanBePrivate.Global, MissingXmlDoc, UnusedAutoPropertyAccessor.Global, UnusedMember.Global
// ReSharper disable MultipleSpaces
// ReSharper disable BadParensSpaces
#pragma warning disable SA1009 // Closing parenthesis should be spaced correctly
#pragma warning disable SA1025 // Code should not contain multiple whitespace in a row
#pragma warning disable SA1501 // Statement should not be on a single line
namespace CodeGenerator
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Clarity.Ecommerce;
    using Clarity.Ecommerce.Service;
    using ServiceStack;

    /// <summary>An endpoint helpers.</summary>
    public static class EndpointHelpers
    {
        /// <summary>Interrogate types.</summary>
        /// <param name="solutionDirectory">      Pathname of the solution directory.</param>
        /// <param name="routeTypes">             List of types of the routes.</param>
        /// <param name="typeToRouteLookup">      The type to route lookup.</param>
        /// <param name="resultsToWrite">         The results to write.</param>
        /// <param name="processedResultsToWrite">The processed results to write.</param>
        /// <returns>A Task.</returns>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
        public static async Task InterrogateTypesAsync(
            string solutionDirectory,
            List<Type> routeTypes,
            ConcurrentDictionary<Type, List<Type>> typeToRouteLookup,
            ConcurrentDictionary<Type, bool[]> resultsToWrite,
            List<ProcessedResult> processedResultsToWrite)
        {
            HelpFunctions.ResetPreviouslyLoadedProperties();
            var skips = new[]
            {
                "Decimal", "Double", "Enum", "Boolean", "String", "Int32", "Byte", "Byte[]", "Guid", "Int32[]", "Int64", "String[]", "Array", "TimeZoneInfo",
                "SerializableAttributesDictionary", "SerializableAttributeObject", "CEFActionResponse",
                "Grouping", "Paging", "Sort", "Grouping[]", "Paging", "Sort[]",
                "SearchSort", "FileEntityType", "UploadStatus", "UrlConfig",
            };
            try
            {
                ////var dte = (EnvDTE.DTE)serviceProvider.GetCOMService(typeof(EnvDTE.DTE));
                ////var solutionDirectory = Path.GetDirectoryName(dte.Solution.FullName) + "\\";
                /*
                var usedInTypes = new[]
                {
                    "UsedInStorefrontAttribute",
                    "UsedInAdminAttribute",
                    "UsedInBrandAdminAttribute",
                    "UsedInFranchiseAdminAttribute",
                    "UsedInStoreAdminAttribute",
                    "UsedInVendorAdminAttribute",
                    "UsedInManufacturerAdminAttribute",
                };
                */
                var serviceBinDirectory = solutionDirectory + "06.Services\\06.Clarity.Ecommerce.Service\\bin\\";
                var pluginsDirectory = solutionDirectory + "Plugins\\";
                var clientPluginsDirectory = solutionDirectory + "ClientPlugins\\";
                var skipDlls = new[]
                {
                    serviceBinDirectory + "Clarity.Ecommerce.Core.dll",
                    serviceBinDirectory + "Clarity.Ecommerce.DataModel.dll",
                    serviceBinDirectory + "Clarity.Ecommerce.Interfaces.Models.dll",
                    serviceBinDirectory + "Clarity.Ecommerce.Interfaces.Workflows.dll",
                    serviceBinDirectory + "Clarity.Ecommerce.Models.dll",
                    serviceBinDirectory + "Clarity.Ecommerce.Models.Mapping.dll",
                    serviceBinDirectory + "Clarity.Ecommerce.Utilities.dll",
                    serviceBinDirectory + "Clarity.Ecommerce.Workflow.dll",
                };
                var dlls = Directory.GetFiles(serviceBinDirectory, "Clarity.*.dll").ToList();
                dlls.AddRange(Directory.GetFiles(pluginsDirectory, "Clarity.*.dll"));
                dlls.AddRange(Directory.GetFiles(clientPluginsDirectory, "Clarity.*.dll"));
                var assemblies = dlls.Where(x => !skipDlls.Contains(x)).Select(Assembly.LoadFrom).ToList();
                routeTypes.AddRange(
                    assemblies
                        .SelectMany(x => x.GetTypes()
                            .Where(y =>
                            {
                                var attributes = y.GetCustomAttributes().Select(z => z.GetType()).ToArray();
                                return attributes.Contains(typeof(RouteAttribute))
                                    && (attributes.Contains(typeof(UsedInStorefrontAttribute))
                                        || attributes.Contains(typeof(UsedInAdminAttribute))
                                        || attributes.Contains(typeof(UsedInBrandAdminAttribute))
                                        || attributes.Contains(typeof(UsedInFranchiseAdminAttribute))
                                        || attributes.Contains(typeof(UsedInManufacturerAdminAttribute))
                                        || attributes.Contains(typeof(UsedInStoreAdminAttribute))
                                        || attributes.Contains(typeof(UsedInVendorAdminAttribute)));
                            }))
                        .Distinct()
                        .OrderBy(x => x.GetCustomAttribute<RouteAttribute>()!.Path ?? string.Empty)
                        .ToList());
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Pretty up type exceptions
                var sb = new StringBuilder();
                foreach (var inner in ex.LoaderExceptions)
                {
                    sb.AppendLine(inner!.Message);
                    if (inner is FileNotFoundException fileNotFound
                        && !string.IsNullOrEmpty(fileNotFound.FusionLog))
                    {
                        sb.AppendLine("Fusion Log:");
                        sb.AppendLine(fileNotFound.FusionLog);
                    }
                    sb.AppendLine();
                }
                throw new(sb.ToString(), ex);
            }
            var write = resultsToWrite;
            await routeTypes.ForEachAsync(
                    1,
                    routeType => InterrogateTypeAsync(routeType, true, null!, 0, typeToRouteLookup, write))
                .ConfigureAwait(false);
            resultsToWrite = new(
                write
                    .Where(x => !x.Key.IsInterface
                        // && !x.Key.IsAbstract
                        /*&& !x.Key.Name.Contains("List`1")
                        && !x.Key.Name.Contains("Nullable`1")
                        && !x.Key.Name.Contains("Dictionary`2")
                        && !x.Key.Name.Contains("CEFActionResponse`1")
                        && !x.Key.Name.Contains("SalesItemBaseModel`2")
                        && !x.Key.Name.Contains("SalesCollectionBaseModel`12")
                        && !x.Key.Name.Contains("KeyValuePair`2")
                        && !x.Key.Name.Contains("ValueTuple`2")
                        && !x.Key.Name.Contains("CheckoutModes")
                        && !x.Key.Name.EndsWith("[]")*/ // Array
                        && !skips.Contains(x.Key.Name))
                    .OrderBy(x => x.Key.Name));
            foreach (var result in resultsToWrite)
            {
                if (/*result.Key.Name.StartsWith("PagedResultsBase")
                    ||*/ result.Key.Name.StartsWith("SearchViewModelBase")
                    // || result.Key.Name.StartsWith("CartItemPagedResults")
                    || result.Key.HasAttribute<RouteAttribute>())
                {
                    continue;
                }
                if (result.Value.Contains(false) && typeToRouteLookup.ContainsKey(result.Key))
                {
                    foreach (var route in typeToRouteLookup[result.Key])
                    {
                        if (result.Key.Name.Contains("Dictionary`2")
                            || result.Key.Name.Contains("KeyValuePair`2")
                            || result.Key.Name.Contains("ValueTuple`2")
                            || result.Key.Name.Contains("SalesItemBaseModel`2"))
                        {
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[0], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[1], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            result.Value[0] = result.Value[1] = result.Value[2] = result.Value[3] = result.Value[4] = result.Value[5] = result.Value[6] = false;
                            continue;
                        }
                        if (result.Key.Name.Contains("List`1")
                            || result.Key.Name.Contains("Nullable`1")
                            || result.Key.Name.Contains("CEFActionResponse`1"))
                        {
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments.Length == 0 ? result.Key.GetElementType()! : result.Key.GenericTypeArguments[0]!, false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            result.Value[0] = result.Value[1] = result.Value[2] = result.Value[3] = result.Value[4] = result.Value[5] = result.Value[6] = false;
                            continue;
                        }
                        if (result.Key.Name.Contains("SalesCollectionBaseModel`12"))
                        {
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[00], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[01], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[02], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[03], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[04], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[05], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[06], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[07], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[08], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[09], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[10], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            await InterrogateTypeAsync(result.Key.GenericTypeArguments[11], false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            result.Value[0] = result.Value[1] = result.Value[2] = result.Value[3] = result.Value[4] = result.Value[5] = result.Value[6] = false;
                            continue;
                        }
                        if (result.Key.Name.Contains("[]"))
                        {
                            // var t = result.Key.Name;
                            await InterrogateTypeAsync(result.Key.GetElementType()!, false, route, 1, typeToRouteLookup, write).ConfigureAwait(false);
                            result.Value[0] = result.Value[1] = result.Value[2] = result.Value[3] = result.Value[4] = result.Value[5] = result.Value[6] = false;
                            continue;
                        }
                        var routeCustomAttributes = route.GetCustomAttributes().Select(x => x.GetType()).ToArray();
                        var usedInStorefront = routeCustomAttributes.Contains(typeof(UsedInStorefrontAttribute));
                        var usedInAdmin = routeCustomAttributes.Contains(typeof(UsedInAdminAttribute));
                        var usedInBrandAdmin = routeCustomAttributes.Contains(typeof(UsedInBrandAdminAttribute));
                        var usedInFranchiseAdmin = routeCustomAttributes.Contains(typeof(UsedInFranchiseAdminAttribute));
                        var usedInManufacturerAdmin = routeCustomAttributes.Contains(typeof(UsedInManufacturerAdminAttribute));
                        var usedInStoreAdmin = routeCustomAttributes.Contains(typeof(UsedInStoreAdminAttribute));
                        var usedInVendorAdmin = routeCustomAttributes.Contains(typeof(UsedInVendorAdminAttribute));
                        if (!result.Value[0] && usedInStorefront)
                        {
                            result.Value[0] = true;
                        }
                        if (!result.Value[1] && usedInAdmin)
                        {
                            result.Value[1] = true;
                        }
                        if (!result.Value[2] && usedInBrandAdmin)
                        {
                            result.Value[2] = true;
                        }
                        if (!result.Value[3] && usedInFranchiseAdmin)
                        {
                            result.Value[3] = true;
                        }
                        if (!result.Value[4] && usedInStoreAdmin)
                        {
                            result.Value[4] = true;
                        }
                        if (!result.Value[5] && usedInVendorAdmin)
                        {
                            result.Value[5] = true;
                        }
                        if (!result.Value[6] && usedInManufacturerAdmin)
                        {
                            result.Value[6] = true;
                        }
                        if (result.Value.All(x => x))
                        {
                            break;
                        }
                    }
                }
#pragma warning disable SA1305 // Field names should not use Hungarian notation
                // ReSharper disable once NotAccessedVariable
                var btString = string.Empty;
#pragma warning restore SA1305 // Field names should not use Hungarian notation
                var bt = result.Key.BaseType != null
                    && result.Key.BaseType != typeof(object)
                    && result.Key.BaseType != typeof(Array)
                    ? result.Key.BaseType
                    : null;
                if (bt != null)
                {
                    btString = " : ";
                    // ReSharper disable once RedundantAssignment
                    btString += HelpFunctions.ProcessType(bt).Replace("&lt;", "<").Replace("&gt;", ">").Replace(",", ", ");
                    // if (bt.IsGenericType)
                    // {
                    //     foreach (Type gt in bt.GetGenericArguments())
                    //     {
                    //         resultsToWrite.Add(gt);
                    //     }
                    // }
                }
                var generatedName = result.Key.Name + string.Empty;
                if (generatedName.Contains("`"))
                {
                    continue;
                }
                if (processedResultsToWrite.Any(x => x.generatedName == generatedName))
                {
                    continue;
                }
                processedResultsToWrite.Add(new(result.Key, bt, string.Empty, generatedName, result.Value));
            }
        }

        /// <summary>Interrogate type.</summary>
        /// <param name="type">             The type.</param>
        /// <param name="isRoute">          True if this EndpointHelpers is route.</param>
        /// <param name="routeType">        Type of the route.</param>
        /// <param name="depthLevel">       The depth level.</param>
        /// <param name="typeToRouteLookup">The type to route lookup.</param>
        /// <param name="resultsToWrite">   The results to write.</param>
        /// <returns>A Task.</returns>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
        private static async Task InterrogateTypeAsync(
            Type type,
            bool isRoute,
            Type routeType,
            int depthLevel,
            IDictionary<Type, List<Type>> typeToRouteLookup,
            IDictionary<Type, bool[]> resultsToWrite)
        {
            var skips = new[]
            {
                "Decimal", "Double", "Enum", "Boolean", "String", "Int32", "Byte", "Byte[]", "Guid", "Int32[]", "Int64", "String[]", "Array", "TimeZoneInfo",
                "SerializableAttributesDictionary", "SerializableAttributeObject", "CEFActionResponse",
                "Grouping", "Paging", "Sort", "Grouping[]", "Paging", "Sort[]",
                "SearchSort", "FileEntityType", "UploadStatus", "UrlConfig",
                "Type", "Assembly", "Module", "ModuleHandle", "CustomAttributeData", "ParameterInfo", "ParameterAttributes",
            };
            if (depthLevel > 5 // Prevent Stack Overflows
                || type.Name == "PayoneerOrderEventWebhookReturn"
                || type.IsInterface
                || skips.Contains(type.Name))
            {
                return;
            }
            // ReSharper disable UnusedVariable (Used for Debugging)
            var typeName = type.Name;
            var routeTypeName = isRoute ? type.Name : routeType.Name;
            // ReSharper restore UnusedVariable
            var attributes = (isRoute ? type : routeType).GetCustomAttributes().Select(x => x.GetType()).ToArray();
            var usedInStorefront = attributes.Contains(typeof(UsedInStorefrontAttribute));
            var usedInAdmin = attributes.Contains(typeof(UsedInAdminAttribute));
            var usedInBrandAdmin = attributes.Contains(typeof(UsedInBrandAdminAttribute));
            var usedInFranchiseAdmin = attributes.Contains(typeof(UsedInFranchiseAdminAttribute));
            var usedInStoreAdmin = attributes.Contains(typeof(UsedInStoreAdminAttribute));
            var usedInVendorAdmin = attributes.Contains(typeof(UsedInVendorAdminAttribute));
            var usedInManufacturerAdmin = attributes.Contains(typeof(UsedInManufacturerAdminAttribute));
            if (!isRoute)
            {
                if (!typeToRouteLookup.ContainsKey(type))
                {
                    typeToRouteLookup[type] = new();
                }
                if (!typeToRouteLookup[type].Contains(routeType))
                {
                    typeToRouteLookup[type].Add(routeType);
                    if (resultsToWrite.ContainsKey(type))
                    {
                        // Aggregate to allow per kind
                        if (!resultsToWrite[type][0] && usedInStorefront)
                        {
                            resultsToWrite[type][0] = true;
                        }
                        if (!resultsToWrite[type][1] && usedInAdmin)
                        {
                            resultsToWrite[type][1] = true;
                        }
                        if (!resultsToWrite[type][2] && usedInBrandAdmin)
                        {
                            resultsToWrite[type][2] = true;
                        }
                        if (!resultsToWrite[type][3] && usedInFranchiseAdmin)
                        {
                            resultsToWrite[type][3] = true;
                        }
                        if (!resultsToWrite[type][4] && usedInStoreAdmin)
                        {
                            resultsToWrite[type][4] = true;
                        }
                        if (!resultsToWrite[type][5] && usedInVendorAdmin)
                        {
                            resultsToWrite[type][5] = true;
                        }
                        if (!resultsToWrite[type][6] && usedInManufacturerAdmin)
                        {
                            resultsToWrite[type][6] = true;
                        }
                        if (!type.IsGenericType)
                        {
                            // For Generic types, we need to interrogate their generic arguments in addition to the type itself
                            return;
                        }
                    }
                    else
                    {
                        resultsToWrite[type] = new[]
                        {
                            usedInStorefront,
                            usedInAdmin,
                            usedInBrandAdmin,
                            usedInFranchiseAdmin,
                            usedInStoreAdmin,
                            usedInVendorAdmin,
                            usedInManufacturerAdmin,
                        };
                    }
                }
            }
            else
            {
                if (resultsToWrite.ContainsKey(type))
                {
                    // Aggregate to allow per kind
                    if (!resultsToWrite[type][0] && usedInStorefront)
                    {
                        resultsToWrite[type][0] = true;
                    }
                    if (!resultsToWrite[type][1] && usedInAdmin)
                    {
                        resultsToWrite[type][1] = true;
                    }
                    if (!resultsToWrite[type][2] && usedInBrandAdmin)
                    {
                        resultsToWrite[type][2] = true;
                    }
                    if (!resultsToWrite[type][3] && usedInFranchiseAdmin)
                    {
                        resultsToWrite[type][3] = true;
                    }
                    if (!resultsToWrite[type][4] && usedInStoreAdmin)
                    {
                        resultsToWrite[type][4] = true;
                    }
                    if (!resultsToWrite[type][5] && usedInVendorAdmin)
                    {
                        resultsToWrite[type][5] = true;
                    }
                    if (!resultsToWrite[type][6] && usedInManufacturerAdmin)
                    {
                        resultsToWrite[type][6] = true;
                    }
                    if (!type.IsGenericType)
                    {
                        // For Generic types, we need to interrogate their generic arguments in addition to the type itself
                        return;
                    }
                }
                else
                {
                    resultsToWrite[type] = new[]
                    {
                        usedInStorefront,
                        usedInAdmin,
                        usedInBrandAdmin,
                        usedInFranchiseAdmin,
                        usedInStoreAdmin,
                        usedInVendorAdmin,
                        usedInManufacturerAdmin,
                    };
                }
            }
            if (type.IsGenericType)
            {
                foreach (var gt in type.GetGenericArguments())
                {
                    await InterrogateTypeAsync(gt, false, isRoute ? type : routeType, depthLevel + 1, typeToRouteLookup, resultsToWrite).ConfigureAwait(false);
                }
            }
            var ifaceForIReturnT = type.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.Name.StartsWith("IReturn`"));
            if (ifaceForIReturnT != null)
            {
                // var t = ifaceForIReturnT.GenericTypeArguments[0].Name;
                await InterrogateTypeAsync(ifaceForIReturnT.GenericTypeArguments[0], false, isRoute ? type : routeType, 1, typeToRouteLookup, resultsToWrite).ConfigureAwait(false);
            }
            if (type.BaseType != null && type.BaseType != typeof(object) && type.BaseType != typeof(Array))
            {
                await InterrogateTypeAsync(type.BaseType, false, isRoute ? type : routeType, depthLevel + 1, typeToRouteLookup, resultsToWrite).ConfigureAwait(false);
            }
            foreach (var prop in type.GetPublicProperties(
                    BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly,
                    true))
            {
                if (prop.PropertyType.IsInterface
                    || HelpFunctions.IsSimpleType(prop.PropertyType)
                    || skips.Contains(prop.PropertyType.Name))
                {
                    continue;
                }
                if (prop.PropertyType.IsGenericType)
                {
                    foreach (var gt in prop.PropertyType.GetGenericArguments())
                    {
                        if (gt.IsInterface
                            || HelpFunctions.IsSimpleType(gt)
                            || skips.Contains(gt.Name))
                        {
                            continue;
                        }
                        await InterrogateTypeAsync(gt, false, isRoute ? type : routeType, depthLevel + 1, typeToRouteLookup, resultsToWrite).ConfigureAwait(false);
                    }
                    continue;
                }
                if (prop.PropertyType.Name.EndsWith("[]"))
                {
                    var et = prop.PropertyType.GetElementType();
                    if (!(et!.IsInterface
                            || HelpFunctions.IsSimpleType(et)
                            || skips.Contains(et.Name)
                            || prop.PropertyType.Name == type.Name))
                    {
                        await InterrogateTypeAsync(et, false, isRoute ? type : routeType, depthLevel + 1, typeToRouteLookup, resultsToWrite).ConfigureAwait(false);
                    }
                    continue;
                }
                await InterrogateTypeAsync(prop.PropertyType, false, isRoute ? type : routeType, depthLevel + 1, typeToRouteLookup, resultsToWrite).ConfigureAwait(false);
            }
        }
    }
}
