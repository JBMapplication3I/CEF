// <copyright file="PathString.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the path string class</summary>
namespace Microsoft.Owin
{
    using System;
    using System.Text;
    using Infrastructure;

    /// <summary>Provides correct escaping for Path and PathBase values when needed to reconstruct a request or
    /// redirect URI string.</summary>
    public struct PathString : IEquatable<PathString>
    {
        /// <summary>Represents the empty path. This field is read-only.</summary>
        public static readonly PathString Empty = new(string.Empty);

        /// <summary>True if the path is not empty.</summary>
        /// <value>True if this PathString has value, false if not.</value>
        public bool HasValue => !string.IsNullOrEmpty(Value);

        /// <summary>The unescaped path value.</summary>
        /// <value>The value.</value>
        public string Value { get; }

        /// <summary>Initialize the path string with a given value. This value must be in un-escaped format. Use
        /// PathString.FromUriComponent(value) if you have a path value which is in an escaped format.</summary>
        /// <param name="value">The unescaped path to be assigned to the Value property.</param>
        public PathString(string value)
        {
            if (!string.IsNullOrEmpty(value) && value[0] != '/')
            {
                throw new ArgumentException(Resources.Exception_PathMustStartWithSlash, nameof(value));
            }
            Value = value;
        }

        /// <summary>Adds two PathString instances into a combined PathString value.</summary>
        /// <param name="other">The other to add.</param>
        /// <returns>The combined PathString value.</returns>
        public PathString Add(PathString other)
        {
            return new PathString(string.Concat(Value, other.Value));
        }

        /// <summary>Combines a PathString and QueryString into the joined URI formatted string value.</summary>
        /// <param name="other">The other to add.</param>
        /// <returns>The joined URI formatted string value.</returns>
        public string Add(QueryString other)
        {
            return string.Concat(ToUriComponent(), other.ToUriComponent());
        }

        /// <summary>Compares this PathString value to another value. The default comparison is
        /// StringComparison.OrdinalIgnoreCase.</summary>
        /// <param name="other">The second PathString for comparison.</param>
        /// <returns>True if both PathString values are equal.</returns>
        public bool Equals(PathString other)
        {
            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Compares this PathString value to another value using a specific StringComparison type.</summary>
        /// <param name="other">         The second PathString for comparison.</param>
        /// <param name="comparisonType">The StringComparison type to use.</param>
        /// <returns>True if both PathString values are equal.</returns>
        public bool Equals(PathString other, StringComparison comparisonType)
        {
            return string.Equals(Value, other.Value, comparisonType);
        }

        /// <summary>Compares this PathString value to another value. The default comparison is
        /// StringComparison.OrdinalIgnoreCase.</summary>
        /// <param name="obj">The second PathString for comparison.</param>
        /// <returns>True if both PathString values are equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is PathString))
            {
                return false;
            }
            return Equals((PathString)obj, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Returns an PathString given the path as it is escaped in the URI format. The string MUST NOT contain
        /// any value that is not a path.</summary>
        /// <param name="uriComponent">The escaped path as it appears in the URI format.</param>
        /// <returns>The resulting PathString.</returns>
        public static PathString FromUriComponent(string uriComponent)
        {
            return new PathString(Uri.UnescapeDataString(uriComponent));
        }

        /// <summary>Returns an PathString given the path as from a Uri object. Relative Uri objects are not supported.</summary>
        /// <param name="uri">The Uri object.</param>
        /// <returns>The resulting PathString.</returns>
        public static PathString FromUriComponent(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return new PathString(string.Concat("/", uri.GetComponents(UriComponents.Path, UriFormat.Unescaped)));
        }

        /// <summary>Returns the hash code for the PathString value. The hash code is provided by the OrdinalIgnoreCase
        /// implementation.</summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            if (Value == null)
            {
                return 0;
            }
            return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
        }

        /// <summary>Operator call through to Add.</summary>
        /// <param name="left"> The left parameter.</param>
        /// <param name="right">The right parameter.</param>
        /// <returns>The PathString combination of both values.</returns>
        public static PathString operator +(PathString left, PathString right)
        {
            return left.Add(right);
        }

        /// <summary>Operator call through to Add.</summary>
        /// <param name="left"> The left parameter.</param>
        /// <param name="right">The right parameter.</param>
        /// <returns>The PathString combination of both values.</returns>
        public static string operator +(PathString left, QueryString right)
        {
            return left.Add(right);
        }

        /// <summary>Operator call through to Equals.</summary>
        /// <param name="left"> The left parameter.</param>
        /// <param name="right">The right parameter.</param>
        /// <returns>True if both PathString values are equal.</returns>
        public static bool operator ==(PathString left, PathString right)
        {
            return left.Equals(right, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Operator call through to Equals.</summary>
        /// <param name="left"> The left parameter.</param>
        /// <param name="right">The right parameter.</param>
        /// <returns>True if both PathString values are not equal.</returns>
        public static bool operator !=(PathString left, PathString right)
        {
            return !left.Equals(right, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Checks if this instance starts with or exactly matches the other instance. Only full segments are
        /// matched.</summary>
        /// <param name="other">.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool StartsWithSegments(PathString other)
        {
            var value = Value ?? string.Empty;
            var str = other.Value ?? string.Empty;
            if (!value.StartsWith(str, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            if (value.Length == str.Length)
            {
                return true;
            }
            return value[str.Length] == '/';
        }

        /// <summary>Checks if this instance starts with or exactly matches the other instance. Only full segments are
        /// matched.</summary>
        /// <param name="other">    .</param>
        /// <param name="remaining">Any remaining segments from this instance not included in the other instance.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool StartsWithSegments(PathString other, out PathString remaining)
        {
            var value = Value ?? string.Empty;
            var str = other.Value ?? string.Empty;
            if (!value.StartsWith(str, StringComparison.OrdinalIgnoreCase)
                || value.Length != str.Length && value[str.Length] != '/')
            {
                remaining = Empty;
                return false;
            }
            remaining = new PathString(value[str.Length..]);
            return true;
        }

        /// <summary>Provides the path string escaped in a way which is correct for combining into the URI representation.</summary>
        /// <returns>The escaped path value.</returns>
        public override string ToString()
        {
            return ToUriComponent();
        }

        /// <summary>Provides the path string escaped in a way which is correct for combining into the URI representation.</summary>
        /// <returns>The escaped path value.</returns>
        public string ToUriComponent()
        {
            if (!HasValue)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = null;
            var num = 0;
            var num1 = 0;
            var flag = false;
            var num2 = 0;
            while (num2 < Value.Length)
            {
                var flag1 = PathStringHelper.IsPercentEncodedChar(Value, num2);
                if (!(PathStringHelper.IsValidPathChar(Value[num2]) | flag1))
                {
                    if (!flag)
                    {
                        if (stringBuilder == null)
                        {
                            stringBuilder = new StringBuilder(Value.Length * 3);
                        }
                        stringBuilder.Append(Value, num, num1);
                        flag = true;
                        num = num2;
                        num1 = 0;
                    }
                    num1++;
                    num2++;
                }
                else
                {
                    if (flag)
                    {
                        if (stringBuilder == null)
                        {
                            stringBuilder = new StringBuilder(Value.Length * 3);
                        }
                        stringBuilder.Append(Uri.EscapeDataString(Value.Substring(num, num1)));
                        flag = false;
                        num = num2;
                        num1 = 0;
                    }
                    if (!flag1)
                    {
                        num1++;
                        num2++;
                    }
                    else
                    {
                        num1 += 3;
                        num2 += 3;
                    }
                }
            }
            if (num1 == Value.Length && !flag)
            {
                return Value;
            }
            if (num1 > 0)
            {
                if (stringBuilder == null)
                {
                    stringBuilder = new StringBuilder(Value.Length * 3);
                }
                if (!flag)
                {
                    stringBuilder.Append(Value, num, num1);
                }
                else
                {
                    stringBuilder.Append(Uri.EscapeDataString(Value.Substring(num, num1)));
                }
            }
            return stringBuilder.ToString();
        }
    }
}
