// <copyright file="QueryString.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the query string class</summary>
namespace Microsoft.Owin
{
    using System;

    /// <summary>Provides correct handling for QueryString value when needed to reconstruct a request or redirect URI
    /// string.</summary>
    public struct QueryString : IEquatable<QueryString>
    {
        /// <summary>Represents the empty query string. This field is read-only.</summary>
        public static readonly QueryString Empty = new(string.Empty);

        /// <summary>True if the query string is not empty.</summary>
        /// <value>True if this QueryString has value, false if not.</value>
        public bool HasValue => !string.IsNullOrWhiteSpace(Value);

        /// <summary>The escaped query string without the leading '?' character.</summary>
        /// <value>The value.</value>
        public string Value { get; }

        /// <summary>Initialize the query string with a given value. This value must be in escaped and delimited format
        /// without a leading '?' character.</summary>
        /// <param name="value">The query string to be assigned to the Value property.</param>
        public QueryString(string value)
        {
            Value = value;
        }

        /// <summary>Initialize a query string with a single given parameter name and value. The value is.</summary>
        /// <param name="name"> The unencoded parameter name.</param>
        /// <param name="value">The unencoded parameter value.</param>
        public QueryString(string name, string value)
        {
            Value = string.Concat(Uri.EscapeDataString(name), "=", Uri.EscapeDataString(value));
        }

        /// <summary>Indicates whether the current instance is equal to the other instance.</summary>
        /// <param name="other">.</param>
        /// <returns>True if the objects are considered equal, false if they are not.</returns>
        public bool Equals(QueryString other)
        {
            return string.Equals(Value, other.Value);
        }

        /// <summary>Indicates whether the current instance is equal to the other instance.</summary>
        /// <param name="obj">.</param>
        /// <returns>True if the objects are considered equal, false if they are not.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is QueryString))
            {
                return false;
            }
            return Equals((QueryString)obj);
        }

        /// <summary>Returns an QueryString given the query as it is escaped in the URI format. The string MUST NOT
        /// contain any value that is not a query.</summary>
        /// <param name="uriComponent">The escaped query as it appears in the URI format.</param>
        /// <returns>The resulting QueryString.</returns>
        public static QueryString FromUriComponent(string uriComponent)
        {
            if (string.IsNullOrEmpty(uriComponent))
            {
                return new QueryString(string.Empty);
            }
            if (uriComponent[0] != '?')
            {
                throw new ArgumentException(
                    Resources.Exception_QueryStringMustStartWithDelimiter,
                    nameof(uriComponent));
            }
            return new QueryString(uriComponent[1..]);
        }

        /// <summary>Returns an QueryString given the query as from a Uri object. Relative Uri objects are not supported.</summary>
        /// <param name="uri">The Uri object.</param>
        /// <returns>The resulting QueryString.</returns>
        public static QueryString FromUriComponent(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            return new QueryString(uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped));
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A hash code for this QueryString.</returns>
        public override int GetHashCode()
        {
            if (Value == null)
            {
                return 0;
            }
            return Value.GetHashCode();
        }

        /// <summary>Compares the two instances for equality.</summary>
        /// <param name="left"> .</param>
        /// <param name="right">.</param>
        /// <returns>The result of the operation.</returns>
        public static bool operator ==(QueryString left, QueryString right)
        {
            return left.Equals(right);
        }

        /// <summary>Compares the two instances for inequality.</summary>
        /// <param name="left"> .</param>
        /// <param name="right">.</param>
        /// <returns>The result of the operation.</returns>
        public static bool operator !=(QueryString left, QueryString right)
        {
            return !left.Equals(right);
        }

        /// <summary>Provides the query string escaped in a way which is correct for combining into the URI
        /// representation. A leading '?' character will be prepended unless the Value is null or empty. Characters
        /// which are potentially dangerous are escaped.</summary>
        /// <returns>The query string value.</returns>
        public override string ToString()
        {
            return ToUriComponent();
        }

        /// <summary>Provides the query string escaped in a way which is correct for combining into the URI
        /// representation. A leading '?' character will be prepended unless the Value is null or empty. Characters
        /// which are potentially dangerous are escaped.</summary>
        /// <returns>The query string value.</returns>
        public string ToUriComponent()
        {
            if (!HasValue)
            {
                return string.Empty;
            }
            return string.Concat("?", Value.Replace("#", "%23"));
        }
    }
}
