// <copyright file="StringExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the string extensions class</summary>
namespace Clarity.Ecommerce
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Utilities;

    /// <summary>A string extensions.</summary>
    public static class StringExtensions
    {
        /// <summary>A string extension method that converts the value 'To Title Case'.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="value">The value to act on.</param>
        /// <returns>value as a string.</returns>
        public static string ToTitleCase(this string value)
        {
            Contract.RequiresNotNull(value);
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        /// <summary>A string extension method that converts a source to a seo URL.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>source as a string.</returns>
        public static string ToSeoUrl(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return string.Empty;
            }
            var chunk = source.ToTitleCase();
            var crlf = chunk.Replace("\r\n", "-");
            var parts = crlf.Split(" ®-+&#!@%^*()[]|$_,.~;/\\\'\"\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            try
            {
                return parts.Aggregate((current, next) => current + "-" + next);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sequence error: {source}. {ex.Message}");
                return chunk;
            }
        }

        /// <summary>A string extension method that truncates.</summary>
        /// <param name="source">   The source to act on.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>A string.</returns>
        public static string Truncate(this string source, int maxLength)
        {
            if (maxLength == 0)
            {
                return string.Empty;
            }
            if (source.Length <= maxLength)
            {
                return source;
            }
            var parts = source.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var word in parts)
            {
                var previous = sb.ToString();
                sb.Append(word).Append(' ');
                if (sb.Length > maxLength)
                {
                    return previous.Trim();
                }
            }
            return sb.ToString()[..maxLength].Trim();
        }

        /// <summary>A string extension method that splits camel case.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="str">The str to act on.</param>
        /// <returns>A string.</returns>
        public static string SplitCamelCase(this string str)
        {
            Contract.RequiresValidKey(str);
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2");
        }

        /// <summary>
        /// Collapses consecutive duplicates of the given character down to one instance of the character. For example
        /// "a::b:c".CollapseDuplicates(':') will return "a:b:c".
        /// </summary>
        /// <param name="str">The string to collapse duplicates in.</param>
        /// <param name="toCollapse">The character to kill duplicates of.</param>
        /// <returns>The input string with duplicates collapsed.</returns>
        public static string CollapseDuplicates(this string str, char toCollapse)
        {
            var result = new StringBuilder(str.Length);
            for (int i = 0; i < str.Length; ++i)
            {
                if (result.Length == 0)
                {
                    result.Append(str[i]);
                    continue;
                }
                if (result[^1] == toCollapse && str[i] == toCollapse)
                {
                    continue;
                }
                result.Append(str[i]);
            }
            return result.ToString();
        }
    }
}
