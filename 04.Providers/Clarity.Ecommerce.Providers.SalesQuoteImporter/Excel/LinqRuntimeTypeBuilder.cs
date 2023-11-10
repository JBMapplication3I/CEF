// <copyright file="LinqRuntimeTypeBuilder.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the linq runtime type builder class</summary>
#pragma warning disable 1591
#pragma warning disable SA1600 // Elements should be documented
// ReSharper disable InvertIf, RedundantEnumerableCastCall
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter.Excel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Threading;
    using Utilities;

    internal static class LinqRuntimeTypeBuilder
    {
        private static readonly ILogger Logger = RegistryLoaderWrapper.GetInstance<ILogger>();

        private static readonly AssemblyName AssemblyName = new AssemblyName { Name = "DynamicLinqTypes" };

        private static readonly ModuleBuilder ModuleBuilder;

        private static readonly Dictionary<string, Type> BuiltTypes = new Dictionary<string, Type>();

        static LinqRuntimeTypeBuilder()
        {
            ModuleBuilder = Thread.GetDomain()
                .DefineDynamicAssembly(AssemblyName, AssemblyBuilderAccess.Run)
                .DefineDynamicModule(AssemblyName.Name);
        }

        // ReSharper disable once UnusedMember.Global
        internal static Dictionary<string, SPTA> SelectDynamicA(List<string> propertyNames, string? contextProfileName)
        {
            // Group the property names by their nested levels
            var nestedPropertyGroupings = SixthPoint(propertyNames);
            // Take the groupings nad determine Expressions for them in the eventual tree
            return SourceProps(nestedPropertyGroupings, contextProfileName);
        }

        /// <summary>Gets dynamic type.</summary>
        /// <param name="properties">        The properties.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The dynamic type.</returns>
        internal static Type GetDynamicType(Dictionary<string, SPTA> properties, string? contextProfileName)
        {
            Contract.RequiresNotNull(properties);
            Contract.Requires<ArgumentOutOfRangeException>(
                properties.Count > 0,
                $"'{properties}' must have at least 1 field definition");
            try
            {
                Monitor.Enter(BuiltTypes);
                var className = GetTypeKey(properties);
                if (BuiltTypes.ContainsKey(className))
                {
                    return BuiltTypes[className];
                }
                var typeBuilder = ModuleBuilder.DefineType(
                    className,
                    TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable);
                foreach (var property in properties)
                {
                    var split = property.Key.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    var fieldBuilder = typeBuilder.DefineField(
                        (split.Length > 1 ? split.Skip(1) : split).Aggregate(string.Empty, (c, n) => c + n),
                        // ReSharper disable once StyleCop.SA1118
                        property.Value.IsJsonChild
                            ? typeof(string)
                            : property.Value.LevelDynamicType ?? property.Value.LevelProperty.PropertyType,
                        FieldAttributes.Public);
                    // ReSharper disable once AssignNullToNotNullAttribute
                    fieldBuilder.SetCustomAttribute(new CustomAttributeBuilder(
                        typeof(DescriptionAttribute).GetConstructor(new[] { typeof(string) }),
                        new object[] { property.Value.GetFullKey() }));
                }
                BuiltTypes[className] = typeBuilder.CreateType();
                return BuiltTypes[className];
            }
            catch (Exception ex)
            {
                Logger.LogError("Generating Quote Export to Excel File", "LinqRuntimeTypeBuilder", ex, contextProfileName);
            }
            finally
            {
                Monitor.Exit(BuiltTypes);
            }
            return null;
        }

        private static Dictionary<string, SPTA> SourceProps(
            Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>>>> nestedPropertyGroupings,
            string? contextProfileName)
        {
            var sourceProps = new Dictionary<string, SPTA>();
            var lambdaNameInstanceCounter = -1;
            foreach (var key1 in nestedPropertyGroupings.Keys)
            {
                // Check if over depth and continue
                if (!Contract.CheckValidKey(key1))
                {
                    continue;
                } // Should already be processed
                if (int.TryParse(key1, out _))
                {
                    continue;
                } // TODO: Lookup by instance
                // Process This Level
                SPTA spta1 = null;
                if (!sourceProps.ContainsKey(key1))
                {
                    if (!key1.Contains("."))
                    {
                        // Need to go deeper
                        spta1 = sourceProps[key1] = new SPTA(key1, ref lambdaNameInstanceCounter, null);
                    }
                    else
                    {
                        // This is the end, but that's too shallow
                        throw new InvalidOperationException("Unable to process mapping point: too shallow");
                    }
                }
                // Go deeper
                foreach (var key2 in nestedPropertyGroupings[key1].Keys)
                {
                    // Check if blank (over the depth of the intended key) and ignore it
                    if (!Contract.CheckValidKey(key2))
                    {
                        continue;
                    } // Should already be processed
                    if (int.TryParse(key2, out _))
                    {
                        continue;
                    } // TODO: Lookup by instance
                    // Process This Level
                    SPTA spta2 = null;
                    if (!sourceProps[key1].ContainsKey(key2))
                    {
                        var isJsonAttrOf2 = new[] { key1, key2 }.Contains("JsonAttributes");
                        if (!key2.Contains("."))
                        {
                            // Need to go deeper
                            spta2 = sourceProps[key1][key2] = new SPTA(key2, ref lambdaNameInstanceCounter, sourceProps[key1], isPropertyOfParent: !isJsonAttrOf2, isJsonAttributeOf: isJsonAttrOf2);
                        } // else { This is the end }
                    }
                    // Go deeper
                    foreach (var key3 in nestedPropertyGroupings[key1][key2].Keys)
                    {
                        // Check if blank (over the depth of the intended key) and ignore it
                        if (!Contract.CheckValidKey(key3))
                        {
                            continue;
                        } // Should already be processed
                        if (int.TryParse(key3, out _))
                        {
                            continue;
                        } // TODO: Lookup by instance
                        // Process This Level
                        SPTA spta3 = null;
                        if (!sourceProps[key1][key2].ContainsKey(key3))
                        {
                            var isJsonAttrOf3 = new[] { key1, key2, key3 }.Contains("JsonAttributes");
                            if (!key3.Contains("."))
                            {
                                // Need to go deeper
                                spta3 = sourceProps[key1][key2][key3] = new SPTA(key3, ref lambdaNameInstanceCounter, sourceProps[key1][key2], isPropertyOfParent: !isJsonAttrOf3, isJsonAttributeOf: isJsonAttrOf3);
                            } // else { This is the end }
                        }
                        // Go deeper
                        foreach (var key4 in nestedPropertyGroupings[key1][key2][key3].Keys)
                        {
                            // Check if blank (over the depth of the intended key) and ignore it
                            if (!Contract.CheckValidKey(key4))
                            {
                                continue;
                            } // Should already be processed
                            if (int.TryParse(key4, out _))
                            {
                                continue;
                            } // TODO: Lookup by instance
                            // Process This Level
                            SPTA spta4 = null;
                            if (!sourceProps[key1][key2][key3].ContainsKey(key4))
                            {
                                var isJsonAttrOf4 = new[] { key1, key2, key3, key4 }.Contains("JsonAttributes");
                                if (!key4.Contains("."))
                                {
                                    // Need to go deeper
                                    spta4 = sourceProps[key1][key2][key3][key4] = new SPTA(key4, ref lambdaNameInstanceCounter, sourceProps[key1][key2][key3], isPropertyOfParent: !isJsonAttrOf4, isJsonAttributeOf: isJsonAttrOf4);
                                } // else { This is the end }
                            }
                            // Go deeper
                            foreach (var key5 in nestedPropertyGroupings[key1][key2][key3][key4].Keys)
                            {
                                // Check if blank (over the depth of the intended key) and ignore it
                                if (!Contract.CheckValidKey(key5))
                                {
                                    continue;
                                } // Should already be processed
                                if (int.TryParse(key5, out _))
                                {
                                    continue;
                                } // TODO: Lookup by instance
                                // Process This Level
                                SPTA spta5 = null;
                                if (!sourceProps[key1][key2][key3][key4].ContainsKey(key5))
                                {
                                    var isJsonAttrOf5 = new[] { key1, key2, key3, key4, key5 }.Contains("JsonAttributes");
                                    if (!key5.Contains("."))
                                    {
                                        // Need to go deeper
                                        spta5 = sourceProps[key1][key2][key3][key4][key5] = new SPTA(key5, ref lambdaNameInstanceCounter, sourceProps[key1][key2][key3][key4], isPropertyOfParent: !isJsonAttrOf5, isJsonAttributeOf: isJsonAttrOf5);
                                    } // else { This is the end }
                                }
                                // Go deeper
                                foreach (var key6 in nestedPropertyGroupings[key1][key2][key3][key4][key5].Keys)
                                {
                                    // Check if blank (over the depth of the intended key) and ignore it
                                    if (!Contract.CheckValidKey(key6))
                                    {
                                        continue;
                                    } // Should already be processed
                                    if (int.TryParse(key6, out _))
                                    {
                                        continue;
                                    } // TODO: Lookup by instance
                                    // Process This Level
                                    // ReSharper disable once NotAccessedVariable
                                    SPTA spta6 = null;
                                    if (!sourceProps[key1][key2][key3][key4][key5].ContainsKey(key6))
                                    {
                                        var isJsonAttrOf6 = new[] { key1, key2, key3, key4, key5, key6 }.Contains("JsonAttributes");
                                        if (!key6.Contains("."))
                                        {
                                            // Need to go deeper
                                            // ReSharper disable once RedundantAssignment
                                            spta6 = sourceProps[key1][key2][key3][key4][key5][key6] = new SPTA(key6, ref lambdaNameInstanceCounter, sourceProps[key1][key2][key3][key4][key5], isPropertyOfParent: !isJsonAttrOf6, isJsonAttributeOf: isJsonAttrOf6);
                                        } // else { This is the end }
                                    }
                                    // Go deeper
                                    foreach (var key7 in nestedPropertyGroupings[key1][key2][key3][key4][key5][key6])
                                    {
                                        // Check if blank (over the depth of the intended key) and ignore it
                                        if (!Contract.CheckValidKey(key7))
                                        {
                                            continue;
                                        } // Should already be processed
                                        if (int.TryParse(key7, out _))
                                        {
                                            continue;
                                        } // TODO: Lookup by instance
                                        // Process This Level
                                        if (!sourceProps[key1][key2][key3][key4][key5][key6].ContainsKey(key7))
                                        {
                                            if (!key7.Contains("."))
                                            {
                                                // Would need to go deeper
                                                throw new InvalidOperationException("Unable to process mapping point: too deep");
                                            } // else { This is the end }
                                        } // else { Already added }
                                        // Don't go any deeper
                                    }
                                    // Don't do any consolidation
                                }
                                // Consolidate Member Bindings
                                if (spta5.HasChildren)
                                {
                                    spta5.GeneratePreCall(sourceProps, ref lambdaNameInstanceCounter, contextProfileName);
                                }
                            }
                            // Consolidate Member Bindings
                            if (spta4.HasChildren)
                            {
                                spta4.GeneratePreCall(sourceProps, ref lambdaNameInstanceCounter, contextProfileName);
                            }
                        }
                        // Consolidate Member Bindings
                        if (spta3.HasChildren)
                        {
                            spta3.GeneratePreCall(sourceProps, ref lambdaNameInstanceCounter, contextProfileName);
                        }
                    }
                    // Consolidate Member Bindings
                    if (spta2.HasChildren)
                    {
                        spta2.GeneratePreCall(sourceProps, ref lambdaNameInstanceCounter, contextProfileName);
                    }
                }
                // Consolidate Member Bindings
                if (spta1.HasChildren)
                {
                    spta1.GeneratePreCall(sourceProps, ref lambdaNameInstanceCounter, contextProfileName);
                }
            }
            return sourceProps;
        }

        private static Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>>>> SixthPoint(IEnumerable<string> propertyNames) =>
            propertyNames
                .GroupBy(x => x.ToArray().Count(c => c == '.') >= 0
                    ? x.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0]
                    : string.Empty)
                .ToDictionary(key1 => key1.Key, element1 => element1.ToList())
                .ToDictionary(
                    key1 => key1.Key,
                    element1 => element1.Value
                        .GroupBy(x => x.ToArray().Count(c => c == '.') >= 1
                            ? x.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[1]
                            : string.Empty)
                        .ToDictionary(key2 => key2.Key, element2 => element2.ToList()))
                .ToDictionary(
                    key1 => key1.Key,
                    element1 => element1.Value
                        .ToDictionary(
                            key2 => key2.Key,
                            element2 => element2.Value
                                .GroupBy(x => x.ToArray().Count(c => c == '.') >= 2
                                    ? x.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[2]
                                    : string.Empty)
                                .ToDictionary(key3 => key3.Key, element3 => element3.ToList())))
                .ToDictionary(
                    key1 => key1.Key,
                    element1 => element1.Value
                        .ToDictionary(
                            key2 => key2.Key,
                            element2 => element2.Value
                                .ToDictionary(
                                    key3 => key3.Key,
                                    element3 => element3.Value
                                        .GroupBy(x => x.ToArray().Count(c => c == '.') >= 3
                                            ? x.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[3]
                                            : string.Empty)
                                        .ToDictionary(key4 => key4.Key, element4 => element4.ToList()))))
                .ToDictionary(
                    key1 => key1.Key,
                    element1 => element1.Value
                        .ToDictionary(
                            key2 => key2.Key,
                            element2 => element2.Value
                                .ToDictionary(
                                    key3 => key3.Key,
                                    element3 => element3.Value
                                        .ToDictionary(
                                            key4 => key4.Key,
                                            element4 => element4.Value
                                                .GroupBy(x => x.ToArray().Count(c => c == '.') >= 4
                                                    ? x.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[4]
                                                    : string.Empty)
                                                .ToDictionary(key5 => key5.Key, element5 => element5.ToList())))))
                .ToDictionary(
                    key1 => key1.Key,
                    element1 => element1.Value
                        .ToDictionary(
                            key2 => key2.Key,
                            element2 => element2.Value
                                .ToDictionary(
                                    key3 => key3.Key,
                                    element3 => element3.Value
                                        .ToDictionary(
                                            key4 => key4.Key,
                                            element4 => element4.Value
                                                .ToDictionary(
                                                    key5 => key5.Key,
                                                    element5 => element5.Value
                                                        .GroupBy(x => x.ToArray().Count(c => c == '.') >= 5
                                                            ? x.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[5]
                                                            : string.Empty)
                                                        .ToDictionary(key6 => key6.Key, element6 => element6.ToList()))))));

        private static string GetTypeKey(Dictionary<string, SPTA> properties)
        {
            return properties
                .OrderBy(x => x.Key)
                .ThenBy(x => x.Value)
                .Aggregate(
                    string.Empty,
                    (c, n) => $"{c}_{n.Key}");
        }
    }
}
