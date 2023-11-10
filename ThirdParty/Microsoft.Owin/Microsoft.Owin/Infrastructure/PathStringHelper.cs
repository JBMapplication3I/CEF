// <copyright file="PathStringHelper.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the path string helper class</summary>
namespace Microsoft.Owin.Infrastructure
{
    /// <summary>A path string helper.</summary>
    internal static class PathStringHelper
    {
        /// <summary>The valid path characters.</summary>
        private static readonly bool[] ValidPathChars;

        /// <summary>Initializes static members of the Microsoft.Owin.Infrastructure.PathStringHelper class.</summary>
        static PathStringHelper()
        {
            ValidPathChars = new[]
            {
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                true,
                false,
                false,
                true,
                false,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                false,
                true,
                false,
                false,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                false,
                false,
                false,
                false,
                true,
                false,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                true,
                false,
                false,
                false,
                true,
                false,
            };
        }

        /// <summary>Query if 'c' is hexadecimal character.</summary>
        /// <param name="c">The character.</param>
        /// <returns>True if hexadecimal character, false if not.</returns>
        public static bool IsHexadecimalChar(char c)
        {
            if (48 <= c && c <= '9' || 65 <= c && c <= 'F')
            {
                return true;
            }
            if (97 > c)
            {
                return false;
            }
            return c <= 'f';
        }

        /// <summary>Query if 'str' is percent encoded character.</summary>
        /// <param name="str">  The string.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <returns>True if percent encoded character, false if not.</returns>
        public static bool IsPercentEncodedChar(string str, int index)
        {
            if (index >= str.Length - 2 || str[index] != '%' || !IsHexadecimalChar(str[index + 1]))
            {
                return false;
            }
            return IsHexadecimalChar(str[index + 2]);
        }

        /// <summary>Query if 'c' is valid path character.</summary>
        /// <param name="c">The character.</param>
        /// <returns>True if valid path character, false if not.</returns>
        public static bool IsValidPathChar(char c)
        {
            if (c >= (char)ValidPathChars.Length)
            {
                return false;
            }
            return ValidPathChars[c];
        }
    }
}
