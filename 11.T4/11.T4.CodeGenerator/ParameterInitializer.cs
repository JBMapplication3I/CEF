// <copyright file="ParameterInitializer.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the parameter initializer class</summary>
// ReSharper disable MemberCanBePrivate.Global, MissingXmlDoc, UnusedAutoPropertyAccessor.Global, UnusedMember.Global
// ReSharper disable InvertIf
namespace CodeGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Clarity.Ecommerce.Utilities;

    /// <summary>A parameter initializer.</summary>
    public class ParameterInitializer
    {
        /// <summary>Initializes a new instance of the <see cref="ParameterInitializer"/> class.</summary>
        /// <param name="parameter">The parameter.</param>
        public ParameterInitializer(ParameterInfo parameter)
        {
            Parameter = parameter;
            MockLine = $"var {parameter.Name} = Mock.Of<{ResolveInterfaceName(parameter.ParameterType)}>();";
            InjectionParameterLine = $"{parameter.ParameterType} {parameter.Name}"
                .Replace("Clarity.Ecommerce.Interfaces.", string.Empty)
                .Replace("DataModel.", string.Empty)
                .Replace("Models.", string.Empty)
                .Replace("Workflow.", string.Empty);
            InjectedParameterLine = $"{parameter.Name}";
            string initArgs;
            var className = ResolveClassName(parameter.ParameterType);
            SubParametersToInitialize = new();
            if (className.StartsWith("Associate"))
            {
                initArgs = className.EndsWith("ItemShipmentsWorkflow") && !className.Contains("Return")
                    ? "null"
                    : string.Empty;
                InitializerLine = $"var {parameter.Name} = new {className}({initArgs});";
                return;
            }
            var classNameCommaGenerics = ResolveClassNameWithGenericCommasOnly(parameter.ParameterType);
            if (classNameCommaGenerics == "Boolean")
            {
                InitializerLine = $"var {parameter.Name} = true;";
                return;
            }
            var workflowClass = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName!.StartsWith("Clarity.Ecommerce."))
                .SelectMany(x => x.GetTypes().Where(t => t.Name == classNameCommaGenerics))
                .FirstOrDefault();
            Contract.RequiresNotNull(workflowClass, $"Could not find {classNameCommaGenerics}'s class via reflection!");
            SubParametersToInitialize = workflowClass!.GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length)
                .FirstOrDefault(x => x.GetParameters().Any())
                ?.GetParameters()
                .Select(x => new ParameterInitializer(x))
                .ToList();
            initArgs = SubParametersToInitialize
                ?.Select(x => x.InjectedParameterLine)
                .Aggregate((c, n) => c + ", " + n)
                ?? string.Empty;
            InitializerLine = $"var {parameter.Name} = new {className}({initArgs});";
        }

        /// <summary>Gets the parameter.</summary>
        /// <value>The parameter.</value>
        public ParameterInfo Parameter { get; }

        /// <summary>Gets the sub parameters to initialize.</summary>
        /// <value>The sub parameters to initialize.</value>
        public List<ParameterInitializer>? SubParametersToInitialize { get; }

        /// <summary>Gets or sets the mock line.</summary>
        /// <value>The mock line.</value>
        public string MockLine { get; set; }

        /// <summary>Gets or sets the initializer line.</summary>
        /// <value>The initializer line.</value>
        public string InitializerLine { get; set; }

        /// <summary>Gets or sets the injection parameter line.</summary>
        /// <value>The injection parameter line.</value>
        public string InjectionParameterLine { get; set; }

        /// <summary>Gets or sets the injected parameter line.</summary>
        /// <value>The injected parameter line.</value>
        public string InjectedParameterLine { get; set; }

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

        // ReSharper disable once UnusedMember.Local
        private static string ResolveClassNameNoGenerics(Type type)
        {
            var retVal = ResolveInterfaceNameNoGenerics(type);
            if (retVal.StartsWith("I"))
            {
                retVal = retVal[1..];
            }
            return retVal;
        }

        private static string ResolveClassNameWithGenericCommasOnly(Type type)
        {
            var retVal = ResolveInterfaceNameWithGenericCommasOnly(type);
            if (retVal.StartsWith("I"))
            {
                retVal = retVal[1..];
            }
            return retVal;
        }

        private static string ResolveInterfaceNameNoGenerics(Type type)
        {
            var retVal = type.Name;
            if (retVal.Contains("`"))
            {
                var split = retVal.Split('`');
                retVal = split[0];
            }
            return retVal;
        }

        private static string ResolveInterfaceNameWithGenericCommasOnly(Type type)
        {
            var retVal = type.Name;
            if (retVal.Contains("`"))
            {
                var split = retVal.Split('`');
                retVal = split[0] + "<";
                var count = int.Parse(split[1]);
                for (var i = 0; i < count - 1; i++)
                {
                    retVal += ",";
                }
                retVal += ">";
            }
            return retVal;
        }

        private static string ResolveInterfaceName(Type type)
        {
            var retVal = type.Name;
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
    }
}
