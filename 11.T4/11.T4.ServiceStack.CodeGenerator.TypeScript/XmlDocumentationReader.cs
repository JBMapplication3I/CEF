// <copyright file="XmlDocumentationReader.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the XML documentation reader class</summary>
namespace ServiceStack.CodeGenerator.TypeScript.XmlDocumentationReader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;
    using Clarity.Ecommerce.Utilities;

    /// <summary>Utility class to provide documentation for various types where available with the
    /// assembly.</summary>
    public static class XmlDocumentationReader
    {
        /// <summary>(Immutable) A cache used to remember Xml documentation for assemblies.</summary>
        private static readonly Dictionary<Assembly, XmlDocument?> Cache = new();

        /// <summary>(Immutable) A cache used to store failure exceptions for assembly lookups.</summary>
        private static readonly Dictionary<Assembly, Exception> FailCache = new();

        /// <summary>Provides the documentation comments for a specific method.</summary>
        /// <param name="methodInfo">The MethodInfo (reflection data ) of the member to find documentation for.</param>
        /// <returns>The XML fragment describing the method.</returns>
        public static XmlElement? XMLFromMember(MethodInfo methodInfo)
        {
            Contract.RequiresNotNull(methodInfo);
            // Calculate the parameter string as this is in the member name in the XML
            var parameters = string.Empty;
            foreach (var parameterInfo in methodInfo.GetParameters())
            {
                if (parameters.Length > 0)
                {
                    parameters += ",";
                }
                parameters += parameterInfo.ParameterType.FullName;
            }
            var element = XMLFromName(
                methodInfo.DeclaringType!,
                'M',
                methodInfo.Name + (parameters.Length > 0 ? "(" + parameters + ")" : string.Empty));
            return element;
        }

        /// <summary>Provides the documentation comments for a specific member.</summary>
        /// <param name="memberInfo">The MemberInfo (reflection data) or the member to find documentation for.</param>
        /// <returns>The XML fragment describing the member.</returns>
        public static XmlElement? XMLFromMember(MemberInfo memberInfo)
        {
            Contract.RequiresNotNull(memberInfo);
            // First character [0] of member type is prefix character in the name in the XML
            var element = XMLFromName(
                memberInfo.DeclaringType!,
                memberInfo.MemberType.ToString()[0],
                memberInfo.Name);
            return element;
        }

        /// <summary>XML from property.</summary>
        /// <param name="propertyInfo">Information describing the property.</param>
        /// <returns>An XmlElement.</returns>
        public static XmlElement? XMLFromProperty(PropertyInfo propertyInfo)
        {
            Contract.RequiresNotNull(propertyInfo);
            // First character [0] of member type is prefix character in the name in the XML
            var element = XMLFromName(
                propertyInfo.DeclaringType!,
                'P',
                propertyInfo.Name);
            return element;
        }

        /// <summary>Provides the documentation comments for a specific type.</summary>
        /// <param name="type">Type to find the documentation for.</param>
        /// <returns>The XML fragment that describes the type.</returns>
        public static XmlElement? XMLFromType(Type type)
        {
            // Prefix in type names is T
            return XMLFromName(Contract.RequiresNotNull(type), 'T', string.Empty);
        }

        /// <summary>Obtains the XML Element that describes a reflection element by searching the members
        /// for a member that has a name that describes the element.</summary>
        /// <exception cref="MultipleMatchedElementsException">Thrown when a Multiple Matched Elements
        ///                                                    error condition occurs.</exception>
        /// <exception cref="NoDocumentationFoundException">   Thrown when a No Documentation Found
        ///                                                    error condition occurs.</exception>
        /// <param name="type">  The type or parent type, used to fetch the assembly.</param>
        /// <param name="prefix">The prefix as seen in the name attribute in the documentation XML.</param>
        /// <param name="name">  Where relevant, the full name qualifier for the element.</param>
        /// <returns>The member that has a name that describes the specified reflection element.</returns>
        private static XmlElement? XMLFromName(Type type, char prefix, string name)
        {
            var fullName = $"{prefix}:{type.FullName}{(string.IsNullOrEmpty(name) ? string.Empty : "." + name)}";
            return XMLFromAssembly(type.Assembly)
                ?["doc"]
                ?["members"]
                ?.OfType<XmlElement>()
                .FirstOrDefault(x => x.Attributes["name"]!.Value == fullName);
        }

        /// <summary>Obtains the documentation file for the specified assembly.</summary>
        /// <remarks>This version uses a cache to preserve the assemblies, so that the XML file is not loaded and parsed
        /// on every single lookup.</remarks>
        /// <param name="assembly">The assembly to find the XML document for.</param>
        /// <returns>The XML document.</returns>
        public static XmlDocument? XMLFromAssembly(Assembly assembly)
        {
            Contract.RequiresNotNull(assembly);
            if (FailCache.ContainsKey(assembly))
            {
                throw FailCache[assembly];
            }
            try
            {
                if (!Cache.ContainsKey(assembly))
                {
                    // load the document into the cache
                    Cache[assembly] = XMLFromAssemblyNonCached(assembly);
                }
                return Cache[assembly];
            }
            catch (Exception ex)
            {
                FailCache[assembly] = ex;
                throw;
            }
        }

        /// <summary>Loads and parses the documentation file for the specified assembly.</summary>
        /// <param name="assembly">The assembly to find the XML document for.</param>
        /// <returns>The XML document.</returns>
        private static XmlDocument? XMLFromAssemblyNonCached(Assembly assembly)
        {
#if NET5_0_OR_GREATER
            var assemblyFilename = assembly.Location;
#else
            var assemblyFilename = assembly.CodeBase;
#endif
            const string Prefix = "file:///";
            if (!assemblyFilename.StartsWith(Prefix))
            {
                throw new($"Could not ascertain assembly filename from {assemblyFilename} - prefix {Prefix}", null);
            }
            if (!File.Exists(Path.ChangeExtension(assemblyFilename[Prefix.Length..], ".xml")))
            {
                return null;
            }
            StreamReader streamReader;
            try
            {
                streamReader = new(Path.ChangeExtension(assemblyFilename[Prefix.Length..], ".xml"));
            }
            catch
            {
                return null;
            }
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(streamReader);
            return xmlDocument;
        }
    }
}
