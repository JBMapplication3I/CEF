// <copyright file="EncodingExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the encoding extensions class</summary>
namespace Clarity.Ecommerce.Utilities
{
    /// <summary>An encoding extensions.</summary>
    public static class EncodingExtensions
    {
        /// <summary>A byte[] extension method that gets UTF 8 decoded string.</summary>
        /// <param name="source">The source to act on.</param>
        /// <returns>The UTF 8 decoded string.</returns>
        public static string? GetUTF8DecodedString(this byte[]? source)
        {
            return source == null || source.Length == 0
                ? null
                : System.Text.Encoding.UTF8.GetString(source, 0, source.Length);
        }
    }
}
