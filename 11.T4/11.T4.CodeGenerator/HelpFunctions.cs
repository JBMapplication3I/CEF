// <copyright file="HelpFunctions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the help functions class</summary>
// ReSharper disable BadControlBracesLineBreaks, CognitiveComplexity, MemberCanBePrivate.Global, MissingLinebreak, MissingXmlDoc, UnusedMember.Global
namespace CodeGenerator
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A help functions.</summary>
    public static class HelpFunctions
    {
        private static ConcurrentDictionary<Type, PropertyInfo[]> PreviouslyLoadedProperties { get; set; } = new();

        private static Type[] AttributesForIgnored { get; } =
        {
            typeof(JsonIgnoreAttribute),
            typeof(NotMappedAttribute),
        };

        private static Type[] SimpleTypes { get; } =
        {
            typeof(int),
            typeof(int?),
            typeof(long),
            typeof(long?),
            typeof(decimal),
            typeof(decimal?),
            typeof(bool),
            typeof(bool?),
            typeof(void),
            typeof(string),
            typeof(DateTime),
            typeof(DateTime?),
            typeof(byte[]),
        };

        /// <summary>Resets the previously loaded properties.</summary>
        public static void ResetPreviouslyLoadedProperties()
        {
            PreviouslyLoadedProperties = new();
        }

        /// <summary>Swap to model type.</summary>
        /// <param name="name">The name.</param>
        /// <returns>A string.</returns>
        // ReSharper disable once CyclomaticComplexity
        public static string? SwapToModelType(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            if (name!.Contains("Status"))
            {
                return "Status";
            }
            if (name.Contains("State"))
            {
                return "State";
            }
            switch (name)
            {
                case "SalesOrderItem":
                case "PurchaseOrderItem":
                case "SalesQuoteItem":
                case "SampleRequestItem":
                case "SalesInvoiceItem":
                case "SalesReturnItem":
                case "CartItem":
                {
                    return "SalesItemBase";
                }
                case "SalesOrderItemTarget":
                case "PurchaseOrderItemTarget":
                case "SalesQuoteItemTarget":
                case "SampleRequestItemTarget":
                case "SalesInvoiceItemTarget":
                case "SalesReturnItemTarget":
                case "CartItemTarget":
                {
                    return "SalesItemTargetBase";
                }
            }
            // The Rules
            if (name.Contains("Type")
                // The Exceptions to the rule
                && name != "CartType"
                && name != "ReportType"
                && name != "NoteType"
                && name != "DiscountAccountType"
                && name != "DiscountProductType"
                && name != "SubscriptionType"
                && name != "SubscriptionTypeRepeatType"
                && name != "MembershipRepeatType"
                && name != "RepeatType"
                && name != "PriceRuleAccountType"
                && name != "PriceRuleProductType"
                && name != "ProductSubscriptionType"
                && name != "UserProductType"
                && name != "AttributeType"
                && name != "GeneralAttributeType")
            {
                return "Type";
            }
            if (name == "GeneralAttributeType")
            {
                return "AttributeType";
            }
            return name;
        }

        /// <summary>Query if 't' is simple type.</summary>
        /// <param name="t">The Type to process.</param>
        /// <returns>True if simple type, false if not.</returns>
        public static bool IsSimpleType(Type t)
        {
            return t.Name == "Void" || SimpleTypes.Contains(t);
        }

        /// <summary>Query if 't' is simple type.</summary>
        /// <param name="t">The string to process.</param>
        /// <returns>True if simple type, false if not.</returns>
        public static bool IsSimpleType(string t)
        {
            return t == nameof(Int32)
                || t == typeof(int?).Name
                || t == typeof(long).Name
                || t == typeof(long?).Name
                || t == typeof(decimal).Name
                || t == typeof(decimal?).Name
                || t == typeof(double?).Name
                || t == typeof(bool?).Name
                || t == typeof(DateTime?).Name
                || t == typeof(void).Name
                || t is "int?"
                    or "long?"
                    or "decimal?"
                    or "double?"
                    or "long"
                    or "decimal"
                    or "bool?"
                    or "DateTime?"
                    or nameof(Int64)
                    or nameof(Decimal)
                    or nameof(Double)
                    or nameof(Boolean)
                    or nameof(String)
                    or nameof(DateTime)
                || t.ToLower() == "void";
        }

        /// <summary>Clean property type.</summary>
        /// <param name="root">           The root.</param>
        /// <param name="t">              The Type to process.</param>
        /// <param name="prefixI">        True to prefix i.</param>
        /// <param name="appendModel">    True to append model.</param>
        /// <param name="isCollection">   True if this HelpFunctions is collection.</param>
        /// <param name="swapToModelType">True to swap to model type.</param>
        /// <returns>A string.</returns>
        public static string? CleanPropertyType(
            string root,
            Type t,
            bool prefixI,
            bool appendModel,
#pragma warning disable IDE0060 // Remove unused parameter
            bool isCollection,
#pragma warning restore IDE0060 // Remove unused parameter
            bool swapToModelType)
        {
            var originalName = t.Name;
            var retVal = t.ToString()
                .Replace(root + ".", string.Empty)
                .Replace("Schema.", string.Empty)
                .Replace("System.Collections.Generic.", string.Empty)
                .Replace("System.", string.Empty)
                .Replace("String", "string")
                .Replace("Int32", "int")
                .Replace("Int64", "long")
                .Replace("Decimal", "decimal")
                .Replace("Boolean", "bool")
                .Replace("Nullable`1[int]", "int?")
                .Replace("Nullable`1[long]", "long?")
                .Replace("Nullable`1[bool]", "bool?")
                .Replace("Nullable`1[decimal]", "decimal?")
                .Replace("Nullable`1[DateTime]", "DateTime?")
                .Replace("Nullable`1[Guid]", "Guid?")
                .Replace("List`1", "ICollection`1")
                .Replace("ICollection`1", "List").Replace("[", "<").Replace("]", ">")
                .Replace("ICollection`1", "List").Replace("[", "<").Replace("]", ">")
                .Replace("Byte", "byte")
                .Replace("byte<>", "byte[]");
            if (originalName == "ICollection`1" && (prefixI || appendModel))
            {
                var m = new Regex("(?<outer>.+)<(?<inner>.+)>").Match(retVal);
                var outer = m.Groups["outer"].Value;
                var inner = m.Groups["inner"].Value;
                if (swapToModelType)
                {
                    inner = SwapToModelType(inner);
                }
                if (prefixI && appendModel)
                {
                    retVal = outer + "<I" + inner + "Model>";
                }
                else if (prefixI)
                {
                    retVal = outer + "<I" + inner + ">";
                }
                else
                {
                    // if (appendModel)
                    retVal = outer + "<" + inner + "Model>";
                }
            }
            else
            {
                if (swapToModelType)
                {
                    retVal = SwapToModelType(retVal);
                }
                if (prefixI)
                {
                    retVal = "I" + retVal;
                }
                if (appendModel)
                {
                    retVal += "Model";
                }
            }
            return retVal;
        }

        /// <summary>Process the type described by t.</summary>
        /// <param name="t">The string to process.</param>
        /// <returns>A string.</returns>
        public static string ProcessType(string t)
        {
            return t;
        }

        /// <summary>Process the type described by t.</summary>
        /// <param name="t">The Type to process.</param>
        /// <returns>A string.</returns>
        public static string ProcessType(Type? t)
        {
            if (t is null || t == typeof(IReturnVoid))
            {
                return "void";
            }
            var typeString = t.Name;
            // ReSharper disable once InvertIf
            if (t.IsGenericType)
            {
                var genericTypesString = "&lt;"
                    + t.GetGenericArguments().Select(x => x.Name).Aggregate((c, n) => c + "," + n)
                    + "&gt;";
                typeString = typeString.Replace("`" + t.GetGenericArguments().Length, genericTypesString);
                if (typeString.StartsWith("Nullable"))
                {
                    typeString = typeString
                        .Replace("Nullable&lt;", string.Empty)
                        .Replace("&gt;", "?");
                }
            }
            if (t.Name.EndsWith("[]"))
            {
                var et = t.GetElementType();
                return ProcessType(et) + "[]";
            }
            if (IsNullableHelper(t, null, t.CustomAttributes) && !typeString.EndsWith("?"))
            {
                typeString += "?";
            }
            return typeString
                .Replace("Int32", "int")
                .Replace("Int64", "long")
                .Replace("Decimal", "decimal")
                .Replace("Double", "double")
                .Replace("Boolean", "bool")
                .Replace("String", "string")
                .Replace("Object", "object")
                .Replace("Byte", "byte")
                .Replace("byte<>", "byte[]");
        }

        /// <summary>Strips strings from a source.</summary>
        /// <param name="source">  Source for the.</param>
        /// <param name="toStrips">A variable-length parameters list containing to strips.</param>
        /// <returns>A string.</returns>
        public static string Strip(string source, params string[] toStrips)
        {
            var modded = source;
            foreach (var toStrip in toStrips)
            {
                modded = modded.Replace(toStrip, string.Empty);
            }
            return modded;
        }

        /// <summary>Converts a source to a markup.</summary>
        /// <param name="source">Source for the.</param>
        /// <returns>A string.</returns>
        public static string AsMarkup(string source)
        {
            return source
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");
        }

        /// <summary>Pluralize name.</summary>
        /// <param name="t">The Type to process.</param>
        /// <returns>A string.</returns>
        public static string? PluralizeName(Type? t)
        {
            return t is null ? null : PluralizeName(t.Name);
        }

        /// <summary>C# to type script type.</summary>
        /// <param name="t">The string to process.</param>
        /// <returns>A string.</returns>
        public static string CSharpToTypeScriptType(string t)
        {
            var retVal = t
                // C# Object based types to their simple types
                .Replace("Int32", "int")
                .Replace("Int64", "long")
                .Replace("Decimal", "decimal")
                .Replace("Boolean", "bool")
                .Replace("String", "string")
                .Replace("Byte", "byte")
                .Replace("byte<>", "byte[]");
            // C# types to TypeScript types
            if (retVal is "int" or "int?")
            {
                retVal = retVal.Replace("int", "number");
            }
            if (retVal is "long" or "long?")
            {
                retVal = retVal.Replace("long", "number");
            }
            if (retVal is "decimal" or "decimal?")
            {
                retVal = retVal.Replace("decimal", "number");
            }
            if (retVal is "double" or "double?")
            {
                retVal = retVal.Replace("double", "number");
            }
            if (retVal is "bool" or "bool?")
            {
                retVal = retVal.Replace("bool", "boolean");
            }
            if (retVal.StartsWith("List<") || retVal.StartsWith("List&lt;"))
            {
                retVal = retVal.Replace("List", "Array");
            }
            // Return the result
            return retVal;
        }

        /// <summary>C# to type script type.</summary>
        /// <param name="t">The Type to process.</param>
        /// <returns>A string.</returns>
        public static string CSharpToTypeScriptType(Type t)
        {
            var typeString = ProcessType(t);
            return CSharpToTypeScriptType(typeString);
        }

        /// <summary>Pluralize name.</summary>
        /// <param name="name">The name.</param>
        /// <returns>A string.</returns>
        public static string? PluralizeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            if (name.EndsWith("Category")
                || name.EndsWith("Country")
                || name.EndsWith("Inventory")
                || name.EndsWith("Company")
                || name.EndsWith("Opportunity")
                || name.EndsWith("Priority")
                || name.EndsWith("Library")
                || name.EndsWith("Property")
                || name.EndsWith("History")
                || name.EndsWith("Currency"))
            {
                return name[..^1] + "ies";
            }
            if (name.EndsWith("Address")
                || name.EndsWith("Status")
                || name.EndsWith("Hash")
                || name.EndsWith("Access"))
            {
                return name + "es";
            }
            return name + "s";
        }

        /// <summary>De pluralize name.</summary>
        /// <param name="name">The name.</param>
        /// <returns>A string.</returns>
        public static string? DePluralizeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            if (name.EndsWith("Categories")
                || name.EndsWith("Countries")
                || name.EndsWith("Inventories")
                || name.EndsWith("Companies")
                || name.EndsWith("Opportunities")
                || name.EndsWith("Priorities")
                || name.EndsWith("Libraries")
                || name.EndsWith("Properties")
                || name.EndsWith("Histories")
                || name.EndsWith("Currencies"))
            {
                return name[..^3] + "y";
            }
            if (name.EndsWith("Addresses")
                || name.EndsWith("Statuses")
                || name.EndsWith("Hashes"))
            {
                return name[..^2];
            }
            return name[..^1];
        }

        /// <summary>Swap to simple type.</summary>
        /// <param name="t">The Type to process.</param>
        /// <returns>A string.</returns>
        public static string? SwapToSimpleType(Type t)
        {
            // ReSharper disable once InvertIf
            if (t.GenericTypeArguments.Any())
            {
                var name = t.Name;
                name = name.Replace("`1", "<" + t.GenericTypeArguments[0].Name + ">");
                return SwapToSimpleType(name);
            }
            return SwapToSimpleType(t.Name);
        }

        /// <summary>Swap to simple type.</summary>
        /// <param name="name">The name.</param>
        /// <returns>A string.</returns>
        public static string? SwapToSimpleType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            return name switch
            {
                "Int32" => "int",
                "Nullable<Int32>" => "int?",
                "Int64" => "long",
                "Nullable<Int64>" => "long?",
                "Boolean" => "bool",
                "Nullable<Boolean>" => "bool?",
                "Decimal" => "decimal",
                "Nullable<Decimal>" => "decimal?",
                "Guid" => "Guid",
                "Nullable<Guid>" => "Guid?",
                "DateTime" => "DateTime",
                "Nullable<DateTime>" => "DateTime?",
                "Byte" => "byte",
                "Nullable<Byte>" => "byte?",
                "Byte[]" => "byte[]",
                "String" => "string",
                "Nullable<string>" => "string?",
                "Nullable<String>" => "string?",
                _ => name,
            };
        }

        /// <summary>Upper first character.</summary>
        /// <param name="source">Source for the.</param>
        /// <returns>A string.</returns>
        public static string UpperFirstCharacter(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return source;
            }
            if (source.Length == 1)
            {
                return source.ToUpper();
            }
            return source[..1].ToUpper() + source[1..];
        }

        /// <summary>Lower first character.</summary>
        /// <param name="source">Source for the.</param>
        /// <returns>A string.</returns>
        public static string LowerFirstCharacter(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return source;
            }
            if (source.Length == 1)
            {
                return source.ToLower();
            }
            return source[..1].ToLower() + source[1..];
        }

        /// <summary>Resolve class name.</summary>
        /// <param name="type">The type.</param>
        /// <returns>A string.</returns>
        public static string ResolveClassName(Type type)
        {
            var retVal = ResolveInterfaceName(type);
            if (retVal.StartsWith("I"))
            {
                retVal = retVal[1..];
            }
            return retVal;
        }

        /// <summary>Resolve interface name.</summary>
        /// <param name="type">The type.</param>
        /// <returns>A string.</returns>
        public static string ResolveInterfaceName(Type type)
        {
            var retVal = type.Name;
            // ReSharper disable once InvertIf
            if (retVal.Contains("`"))
            {
                var split = retVal.Split('`');
                retVal = split[0] + "<";
                var genArgCount = int.Parse(split[1]);
                for (var i = 0; i < genArgCount; i++)
                {
                    if (i > 0)
                    {
                        retVal += ",";
                    }
                    var genArg = type.GetGenericArguments()[i];
                    retVal += genArg.Name;
                }
                retVal += ">";
            }
            return retVal;
        }

        /// <summary>Resolve class name no generics.</summary>
        /// <param name="type">The type.</param>
        /// <returns>A string.</returns>
        public static string ResolveClassNameNoGenerics(Type type)
        {
            var retVal = ResolveInterfaceNameNoGenerics(type);
            if (retVal.StartsWith("I"))
            {
                retVal = retVal[1..];
            }
            return retVal;
        }

        /// <summary>Resolve interface name no generics.</summary>
        /// <param name="type">The type.</param>
        /// <returns>A string.</returns>
        public static string ResolveInterfaceNameNoGenerics(Type type)
        {
            var retVal = type.Name;
            // ReSharper disable once InvertIf
            if (retVal.Contains("`"))
            {
                var split = retVal.Split('`');
                retVal = split[0];
            }
            return retVal;
        }

        /// <summary>Splits camel case.</summary>
        /// <param name="str">The string.</param>
        /// <returns>A string.</returns>
        public static string SplitCamelCase(string str)
        {
            return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        /// <summary>A Type extension method that gets public properties.</summary>
        /// <param name="type">       The type to act on.</param>
        /// <param name="flags">      The flags.</param>
        /// <param name="skipIgnored">True if skip ignored.</param>
        /// <returns>An array of property information.</returns>
        public static PropertyInfo[] GetPublicProperties(
            this Type type,
            BindingFlags flags = BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance,
            bool skipIgnored = false)
        {
            if (PreviouslyLoadedProperties.ContainsKey(type))
            {
                return PreviouslyLoadedProperties[type];
            }
            if (!type.IsInterface)
            {
                if (!skipIgnored)
                {
                    PreviouslyLoadedProperties[type] = type.GetProperties(flags);
                    return PreviouslyLoadedProperties[type];
                }
                var props = type.GetProperties(flags)
                    .Where(x => !x.GetCustomAttributes()
                        .Any(y => AttributesForIgnored.Contains(y.GetType())))
                    .ToArray();
                PreviouslyLoadedProperties[type] = props;
                return props;
            }
            // ReSharper disable once CollectionNeverUpdated.Local
            var propertyInfos = new List<PropertyInfo>();
            var considered = new List<Type>();
            var queue = new Queue<Type>();
            considered.Add(type);
            queue.Enqueue(type);
            while (queue.Count > 0)
            {
                var subType = queue.Dequeue();
                foreach (var subInterface in subType.GetInterfaces())
                {
                    if (considered.Contains(subInterface))
                    {
                        continue;
                    }
                    considered.Add(subInterface);
                    queue.Enqueue(subInterface);
                }
                var typeProperties = subType.GetProperties(flags);
                var newPropertyInfos = typeProperties.Where(x => !propertyInfos.Contains(x));
                propertyInfos.InsertRange(0, newPropertyInfos);
            }
            PreviouslyLoadedProperties[type] = propertyInfos.ToArray();
            return PreviouslyLoadedProperties[type];
        }

        /// <summary>Query if 'field' is nullable.</summary>
        /// <param name="field">The field.</param>
        /// <returns>True if nullable, false if not.</returns>
        public static bool IsNullable(FieldInfo field)
            => IsNullableHelper(field.FieldType, field.DeclaringType, field.CustomAttributes);

        /// <summary>Query if 'property' is nullable.</summary>
        /// <param name="property">The property.</param>
        /// <returns>True if nullable, false if not.</returns>
        public static bool IsNullable(PropertyInfo property)
            => IsNullableHelper(property.PropertyType, property.DeclaringType, property.CustomAttributes);

        /// <summary>Query if 'parameter' is nullable.</summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>True if nullable, false if not.</returns>
        public static bool IsNullable(ParameterInfo parameter)
            => IsNullableHelper(parameter.ParameterType, parameter.Member, parameter.CustomAttributes);

        /// <summary>Query if 'memberType' is nullable helper.</summary>
        /// <param name="memberType">      Type of the member.</param>
        /// <param name="declaringType">   Type of the declaring.</param>
        /// <param name="customAttributes">The custom attributes.</param>
        /// <returns>True if nullable helper, false if not.</returns>
        private static bool IsNullableHelper(
            Type memberType,
            MemberInfo? declaringType,
            IEnumerable<CustomAttributeData> customAttributes)
        {
            if (memberType.IsValueType)
            {
                return Nullable.GetUnderlyingType(memberType) is not null;
            }
            var nullable = customAttributes.FirstOrDefault(
                x => x.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute");
            if (nullable is not null && nullable.ConstructorArguments.Count == 1)
            {
                var attributeArgument = nullable.ConstructorArguments[0];
                if (attributeArgument.ArgumentType == typeof(byte[]))
                {
                    var args = (ReadOnlyCollection<CustomAttributeTypedArgument>)attributeArgument.Value!;
                    if (args.Count > 0 && args[0].ArgumentType == typeof(byte))
                    {
                        return (byte)args[0].Value! == 2;
                    }
                }
                else if (attributeArgument.ArgumentType == typeof(byte))
                {
                    return (byte)attributeArgument.Value! == 2;
                }
            }
            for (var type = declaringType; type is not null; type = type.DeclaringType)
            {
                var context = type.CustomAttributes.FirstOrDefault(
                    x => x.AttributeType.FullName == "System.Runtime.CompilerServices.NullableContextAttribute");
                if (context is not null
                    && context.ConstructorArguments.Count == 1
                    && context.ConstructorArguments[0].ArgumentType == typeof(byte))
                {
                    return (byte)context.ConstructorArguments[0].Value! == 2;
                }
            }
            // Couldn't find a suitable attribute
            return false;
        }
    }
}
