// <copyright file="WebUtilities.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the web utilities class</summary>
namespace Microsoft.Owin.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>Response generation utilities.</summary>
    public static class WebUtilities
    {
        /// <summary>Append the given query to the uri.</summary>
        /// <param name="uri">        The base uri.</param>
        /// <param name="queryString">The query string to append, if any.</param>
        /// <returns>The combine result.</returns>
        public static string AddQueryString(string uri, string queryString)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (string.IsNullOrEmpty(queryString))
            {
                return uri;
            }
            var flag = uri.IndexOf('?') != -1;
            return string.Concat(uri, flag ? "&" : "?", queryString);
        }

        /// <summary>Append the given query key and value to the uri.</summary>
        /// <param name="uri">  The base uri.</param>
        /// <param name="name"> The name of the query key.</param>
        /// <param name="value">The query value.</param>
        /// <returns>The combine result.</returns>
        public static string AddQueryString(string uri, string name, string value)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            var flag = uri.IndexOf('?') != -1;
            string[] strArrays = { uri, null, null, null, null };
            strArrays[1] = flag ? "&" : "?";
            strArrays[2] = Uri.EscapeDataString(name);
            strArrays[3] = "=";
            strArrays[4] = Uri.EscapeDataString(value);
            return string.Concat(strArrays);
        }

        /// <summary>Append the given query keys and values to the uri.</summary>
        /// <param name="uri">        The base uri.</param>
        /// <param name="queryString">A collection of name value query pairs to append.</param>
        /// <returns>The combine result.</returns>
        public static string AddQueryString(string uri, IDictionary<string, string> queryString)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (queryString == null)
            {
                throw new ArgumentNullException(nameof(queryString));
            }
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(uri);
            var flag = uri.IndexOf('?') != -1;
            foreach (var keyValuePair in queryString)
            {
                stringBuilder.Append(flag ? '&' : '?');
                stringBuilder.Append(Uri.EscapeDataString(keyValuePair.Key));
                stringBuilder.Append('=');
                stringBuilder.Append(Uri.EscapeDataString(keyValuePair.Value));
                flag = true;
            }
            return stringBuilder.ToString();
        }
    }
}
