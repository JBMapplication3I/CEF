// <copyright file="XmlTools.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the XML tools class</summary>
namespace Clarity.Ecommerce.Utilities
{
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>An XML tools.</summary>
    public static class XmlTools
    {
        /// <summary>A T extension method that converts an input to an XML string.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="input">The input to act on.</param>
        /// <returns>Input as a string.</returns>
        public static string ToXmlString<T>(this T input)
        {
            using var writer = new StringWriter();
            input.ToXml(writer);
            return writer.ToString();
        }

        /// <summary>A T extension method that converts this XmlTools to an XML.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="objectToSerialize">The objectToSerialize to act on.</param>
        /// <param name="stream">           The stream.</param>
        public static void ToXml<T>(this T objectToSerialize, Stream stream)
        {
            new XmlSerializer(typeof(T)).Serialize(stream, Contract.RequiresNotNull(objectToSerialize)!);
        }

        /// <summary>A T extension method that converts this XmlTools to an XML.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="objectToSerialize">The objectToSerialize to act on.</param>
        /// <param name="writer">           The writer.</param>
        public static void ToXml<T>(this T objectToSerialize, StringWriter writer)
        {
            new XmlSerializer(typeof(T)).Serialize(writer, Contract.RequiresNotNull(objectToSerialize)!);
        }
    }
}
