// <copyright file="RouteCodeGeneration.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the route code generation class</summary>
namespace ServiceStack.CodeGenerator.TypeScript
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    internal class RouteCodeGeneration
    {
        public RouteCodeGeneration(RouteAttribute route, Type routeType, string? returnTsType, bool isReact = false)
        {
            Route = route;
            RouteType = routeType;
            UrlPath = new();
            if (!isReact)
            {
                UrlPath.Add("this.rootUrl");
            }
            MethodParameters = new();
            PropertiesProcessed = new(new[] { "noCache" });
            RouteInputPropertyLines = new();
            Verbs = string.IsNullOrEmpty(route.Verbs) ? new[] { "get" } : route.Verbs.ToLower().Split(',');
            ReturnTsType = returnTsType;
            RouteInputHasOnlyOptionalParams = true;
        }

        public List<string> MethodParameters { get; }

        public string? ReturnTsType { get; }

        public List<string> RouteInputPropertyLines { get; }

        public Type RouteType { get; }

        public List<string> UrlPath { get; }

        public string[] Verbs { get; }

        /// <summary>Gets a value indicating whether every input DTO property is optional.</summary>
        /// <value>true if route input has only optional parameters, false if not.</value>
        public bool RouteInputHasOnlyOptionalParams { get; private set; }

        public string RouteInputDTOName => RouteType.Name + "Dto";

        private HashSet<string> PropertiesProcessed { get; }

        private RouteAttribute Route { get; }

        /// <summary>Parse route path.</summary>
        /// <param name="tcg">The tcg.</param>
        public void ParseRoutePath(TypescriptCodeGenerator tcg)
        {
            foreach (var param in Route.Path.Trim('/').Split('/'))
            {
                if (IsRouteParam(param))
                {
                    ProcessRouteParameter(param, tcg);
                }
                else
                {
                    UrlPath.Add($"\"{param}\"");
                }
            }
        }

        /// <summary>Process the route properties.</summary>
        /// <param name="tcg">The tcg.</param>
        public void ProcessRouteProperties(TypescriptCodeGenerator tcg)
        {
            foreach (var property in RouteType
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                .Where(p => (p.HasAttribute<ApiMemberAttribute>() || p.CanRead && p.CanWrite)
                         && !p.HasAttribute<IgnoreDataMemberAttribute>()
                         && !p.HasAttribute<JsonIgnoreAttribute>()
                         && p.Name != "noCache"
                         && !PropertiesProcessed.Contains(p.Name)))
            {
                ProcessClrProperty(property, false, false, tcg);
            }
            foreach (var property in RouteType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => (p.HasAttribute<ApiMemberAttribute>() || p.CanRead && p.CanWrite)
                         && !p.HasAttribute<IgnoreDataMemberAttribute>()
                         && !p.HasAttribute<JsonIgnoreAttribute>()
                         && p.Name != "noCache"
                         && !PropertiesProcessed.Contains(p.Name)))
            {
                ProcessClrProperty(property, true, false, tcg);
            }
        }

        /// <summary>Query if 'param' is a route parameter.</summary>
        /// <param name="param">The parameter.</param>
        /// <returns>true if route parameter, false if not.</returns>
        private static bool IsRouteParam(string param)
        {
            return param.StartsWith("{") && param.EndsWith("}");
        }

        /// <summary>Emit comment.</summary>
        /// <param name="docAttr">The document attribute.</param>
        /// <returns>A string.</returns>
        private static string EmitComment(ApiMemberAttribute docAttr)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(docAttr.Description))
            {
                result += "/** " + docAttr.Description + " */";
            }
            return result;
        }

        /// <summary>Generates typescript code for a given property.</summary>
        /// <param name="property">    The property.</param>
        /// <param name="isInherited"> true if this object is inherited.</param>
        /// <param name="isRouteParam">true if this object is route parameter.</param>
        /// <param name="tcg">         The TypescriptCodeGenerator.</param>
        // ReSharper disable once CyclomaticComplexity
        private void ProcessClrProperty(
            PropertyInfo property,
            bool isInherited,
            bool isRouteParam,
            TypescriptCodeGenerator tcg)
        {
            PropertiesProcessed.Add(property.Name);
            // TODO: Add comments for ApiMember properties
            var docAttr = property.GetCustomAttribute<ApiMemberAttribute>();
            var returnType = property.GetMethod!.ReturnType;
            // Optional parameters
            if (!isRouteParam
#if NET5_0_OR_GREATER
                && (returnType.IsNullableType() || returnType.IsClass)
#else
                && (returnType.IsNullableType() || returnType.IsClass())
#endif
                && docAttr?.IsRequired != true)
            {
                // Optional param. Could be string or a DTO type.
                if (isInherited)
                {
                    RouteInputPropertyLines.Add("Inherited");
                    return;
                }
                if (docAttr != null)
                {
                    RouteInputPropertyLines.Add(EmitComment(docAttr));
                }
                var apiMemberAttr1 = property.GetCustomAttribute<ApiMemberAttribute>();
                var nullable1 = returnType == typeof(string)
                    || returnType.IsNullableType()
#if NET5_0_OR_GREATER
                    || returnType.IsClass
                    || returnType.IsInterface
#else
                    || returnType.IsClass()
                    || returnType.IsInterface()
#endif
                    || apiMemberAttr1 is { IsRequired: true }
                        ? "?"
                        : string.Empty;
                var hasApiMemberAttr1 = apiMemberAttr1 != null
                    && !string.IsNullOrWhiteSpace(apiMemberAttr1.Name)
                    && apiMemberAttr1.Name != property.Name;
                var name1 = apiMemberAttr1 != null && hasApiMemberAttr1
                    ? apiMemberAttr1.Name
                    : property.Name;
                RouteInputPropertyLines.Add(
                    $"{name1}{nullable1}: "
                    + $"{tcg.DetermineTSType(returnType, true)?.Replace("IStoreModel", "StoreModel")}"
                    + $";{(hasApiMemberAttr1 ? " // Name format overridden" : string.Empty)}");
                return;
            }
            // Required parameter
            if (isRouteParam)
            {
                MethodParameters.Add(property.Name.ToCamelCase() + ": " + tcg.DetermineTSType(returnType, true));
                return;
            }
            if (isInherited)
            {
                RouteInputPropertyLines.Add("Inherited");
                return;
            }
            if (docAttr != null)
            {
                RouteInputPropertyLines.Add(EmitComment(docAttr));
            }
            RouteInputHasOnlyOptionalParams = false;
            ////RouteInputPropertyLines.Add(property.Name + ": " + TypescriptCodeGenerator.DetermineTsType(returnType, true) + ";");
            var apiMemberAttr = property.GetCustomAttribute<ApiMemberAttribute>();
            var nullable = returnType == typeof(string)
                || returnType.IsNullableType()
#if NET5_0_OR_GREATER
                || returnType.IsClass
                || returnType.IsInterface
#else
                || returnType.IsClass()
                || returnType.IsInterface()
#endif
                || apiMemberAttr is { IsRequired: true }
                    ? "?"
                    : string.Empty;
            var hasApiMemberAttr = apiMemberAttr != null
                && !string.IsNullOrWhiteSpace(apiMemberAttr.Name)
                && apiMemberAttr.Name != property.Name;
            if (nullable == "?" && apiMemberAttr?.IsRequired == true)
            {
                nullable = string.Empty;
            }
            var name = hasApiMemberAttr && apiMemberAttr != null
                ? apiMemberAttr.Name
                : property.Name;
            RouteInputPropertyLines.Add(
                $"{name}{nullable}: "
                + $"{tcg.DetermineTSType(returnType, true)?.Replace("IStoreModel", "StoreModel")}"
                + $";{(hasApiMemberAttr ? " // Name format overridden" : string.Empty)}");
        }

        /// <summary>Process the route parameter described by param.</summary>
        /// <param name="param">The parameter.</param>
        /// <param name="tcg">  The tcg.</param>
        private void ProcessRouteParameter(string param, TypescriptCodeGenerator tcg)
        {
            param = param.Trim('{', '}', '*');
            PropertyInfo? property = null;
            try
            {
                property = RouteType.GetProperty(param);
            }
            catch
            {
                // Do Nothing
            }
            if (property == null)
            {
                MethodParameters.Add($"\n\t\t/* BUG ERROR: CANNOT FIND {param} */\n\t");
                return;
            }
            param = param.ToCamelCase();
            ProcessClrProperty(property, false, true, tcg);
            // Uri Encode string parameters.
            UrlPath.Add(
                property.GetGetMethod()!.ReturnType == typeof(string)
                ? "encodeURIComponent(" + param + ")"
                : param);
        }
    }
}
