// <copyright file="OwinHelpers.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the owin helpers class</summary>
namespace Microsoft.Owin.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>An owin helpers.</summary>
    internal static class OwinHelpers
    {
        /// <summary>The add cookie callback.</summary>
        private static readonly Action<string, string, object> AddCookieCallback;

        /// <summary>The ampersand semicolon.</summary>
        private static readonly char[] AmpersandAndSemicolon;

        /// <summary>The append item callback.</summary>
        private static readonly Action<string, string, object> AppendItemCallback;

        /// <summary>The semicolon and comma.</summary>
        private static readonly char[] SemicolonAndComma;

        /// <summary>Initializes static members of the Microsoft.Owin.Infrastructure.OwinHelpers class.</summary>
        static OwinHelpers()
        {
            AddCookieCallback = (name, value, state) =>
            {
                var strs = (IDictionary<string, string>)state;
                if (!strs.ContainsKey(name))
                {
                    strs.Add(name, value);
                }
            };
            SemicolonAndComma = new[] { ';', ',' };
            AppendItemCallback = (name, value, state) =>
            {
                var strs1 = (IDictionary<string, List<string>>)state;
                if (strs1.TryGetValue(name, out var strs))
                {
                    strs.Add(value);
                    return;
                }
                strs1.Add(name, new List<string>(1) { value });
            };
            AmpersandAndSemicolon = new[] { '&', ';' };
        }

        /// <summary>Appends a header.</summary>
        /// <param name="headers">The headers.</param>
        /// <param name="key">    The key.</param>
        /// <param name="values"> The values.</param>
        public static void AppendHeader(IDictionary<string, string[]> headers, string key, string values)
        {
            if (string.IsNullOrWhiteSpace(values))
            {
                return;
            }
            var header = GetHeader(headers, key);
            if (header == null)
            {
                SetHeader(headers, key, values);
                return;
            }
            headers[key] = new[] { string.Concat(header, ",", values) };
        }

        /// <summary>Appends a header joined.</summary>
        /// <param name="headers">The headers.</param>
        /// <param name="key">    The key.</param>
        /// <param name="values"> A variable-length parameters list containing values.</param>
        public static void AppendHeaderJoined(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return;
            }
            var header = GetHeader(headers, key);
            if (header == null)
            {
                SetHeaderJoined(headers, key, values);
                return;
            }
            headers[key] = new[]
            {
                string.Concat(header, ",", string.Join(",", from value in values select QuoteIfNeeded(value))),
            };
        }

        /// <summary>Appends a header unmodified.</summary>
        /// <param name="headers">The headers.</param>
        /// <param name="key">    The key.</param>
        /// <param name="values"> A variable-length parameters list containing values.</param>
        public static void AppendHeaderUnmodified(
            IDictionary<string, string[]> headers,
            string key,
            params string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return;
            }
            var headerUnmodified = GetHeaderUnmodified(headers, key);
            if (headerUnmodified == null)
            {
                SetHeaderUnmodified(headers, key, values);
                return;
            }
            SetHeaderUnmodified(headers, key, headerUnmodified.Concat(values));
        }

        /// <summary>Gets a header.</summary>
        /// <param name="headers">The headers.</param>
        /// <param name="key">    The key.</param>
        /// <returns>The header.</returns>
        public static string GetHeader(IDictionary<string, string[]> headers, string key)
        {
            var headerUnmodified = GetHeaderUnmodified(headers, key);
            if (headerUnmodified == null)
            {
                return null;
            }
            return string.Join(",", headerUnmodified);
        }

        /// <summary>Gets the header splits in this collection.</summary>
        /// <param name="headers">The headers.</param>
        /// <param name="key">    The key.</param>
        /// <returns>An enumerator that allows foreach to be used to process the header splits in this collection.</returns>
        public static IEnumerable<string> GetHeaderSplit(IDictionary<string, string[]> headers, string key)
        {
            var headerUnmodified = GetHeaderUnmodified(headers, key);
            if (headerUnmodified == null)
            {
                return null;
            }
            return GetHeaderSplitImplementation(headerUnmodified);
        }

        /// <summary>Gets header unmodified.</summary>
        /// <param name="headers">The headers.</param>
        /// <param name="key">    The key.</param>
        /// <returns>An array of string.</returns>
        public static string[] GetHeaderUnmodified(IDictionary<string, string[]> headers, string key)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            if (!headers.TryGetValue(key, out var strArrays))
            {
                return null;
            }
            return strArrays;
        }

        /// <summary>Sets a header.</summary>
        /// <param name="headers">The headers.</param>
        /// <param name="key">    The key.</param>
        /// <param name="value">  The value.</param>
        public static void SetHeader(IDictionary<string, string[]> headers, string key, string value)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                headers.Remove(key);
                return;
            }
            headers[key] = new[] { value };
        }

        /// <summary>Sets header joined.</summary>
        /// <param name="headers">The headers.</param>
        /// <param name="key">    The key.</param>
        /// <param name="values"> A variable-length parameters list containing values.</param>
        public static void SetHeaderJoined(IDictionary<string, string[]> headers, string key, params string[] values)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (values == null || values.Length == 0)
            {
                headers.Remove(key);
                return;
            }
            headers[key] = new[] { string.Join(",", from value in values select QuoteIfNeeded(value)) };
        }

        /// <summary>Sets header unmodified.</summary>
        /// <param name="headers">The headers.</param>
        /// <param name="key">    The key.</param>
        /// <param name="values"> A variable-length parameters list containing values.</param>
        public static void SetHeaderUnmodified(
            IDictionary<string, string[]> headers,
            string key,
            params string[] values)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (values != null && values.Length != 0)
            {
                headers[key] = values;
                return;
            }
            headers.Remove(key);
        }

        /// <summary>Sets header unmodified.</summary>
        /// <param name="headers">The headers.</param>
        /// <param name="key">    The key.</param>
        /// <param name="values"> The values.</param>
        public static void SetHeaderUnmodified(
            IDictionary<string, string[]> headers,
            string key,
            IEnumerable<string> values)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            headers[key] = values.ToArray();
        }

        /// <summary>Gets the cookies.</summary>
        /// <param name="request">The request.</param>
        /// <returns>The cookies.</returns>
        internal static IDictionary<string, string> GetCookies(IOwinRequest request)
        {
            var strs = request.Get<IDictionary<string, string>>("Microsoft.Owin.Cookies#dictionary");
            if (strs == null)
            {
                strs = new Dictionary<string, string>(StringComparer.Ordinal);
                request.Set("Microsoft.Owin.Cookies#dictionary", strs);
            }
            var header = GetHeader(request.Headers, "Cookie");
            if (request.Get<string>("Microsoft.Owin.Cookies#text") != header)
            {
                strs.Clear();
                ParseDelimited(header, SemicolonAndComma, AddCookieCallback, false, false, strs);
                request.Set("Microsoft.Owin.Cookies#text", header);
            }
            return strs;
        }

        /// <summary>Gets a form.</summary>
        /// <param name="text">The text.</param>
        /// <returns>The form.</returns>
        internal static IFormCollection GetForm(string text)
        {
            IDictionary<string, string[]> strs = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
            var strs1 = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            ParseDelimited(text, new[] { '&' }, AppendItemCallback, true, true, strs1);
            foreach (var str in strs1)
            {
                strs.Add(str.Key, str.Value.ToArray());
            }
            return new FormCollection(strs);
        }

        /// <summary>Gets a host.</summary>
        /// <param name="request">The request.</param>
        /// <returns>The host.</returns>
        internal static string GetHost(IOwinRequest request)
        {
            var header = GetHeader(request.Headers, "Host");
            if (!string.IsNullOrWhiteSpace(header))
            {
                return header;
            }
            var localIpAddress = request.LocalIpAddress ?? "localhost";
            var str = request.Get<string>("server.LocalPort");
            if (string.IsNullOrWhiteSpace(str))
            {
                return localIpAddress;
            }
            return string.Concat(localIpAddress, ":", str);
        }

        /// <summary>Gets joined value.</summary>
        /// <param name="store">The store.</param>
        /// <param name="key">  The key.</param>
        /// <returns>The joined value.</returns>
        internal static string GetJoinedValue(IDictionary<string, string[]> store, string key)
        {
            var unmodifiedValues = GetUnmodifiedValues(store, key);
            if (unmodifiedValues == null)
            {
                return null;
            }
            return string.Join(",", unmodifiedValues);
        }

        /// <summary>Gets a query.</summary>
        /// <param name="request">The request.</param>
        /// <returns>The query.</returns>
        internal static IDictionary<string, string[]> GetQuery(IOwinRequest request)
        {
            var strs = request.Get<IDictionary<string, string[]>>("Microsoft.Owin.Query#dictionary");
            if (strs == null)
            {
                strs = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
                request.Set("Microsoft.Owin.Query#dictionary", strs);
            }
            var value = request.QueryString.Value;
            if (request.Get<string>("Microsoft.Owin.Query#text") != value)
            {
                strs.Clear();
                var strs1 = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
                ParseDelimited(value, AmpersandAndSemicolon, AppendItemCallback, true, true, strs1);
                foreach (var str in strs1)
                {
                    strs.Add(str.Key, str.Value.ToArray());
                }
                request.Set("Microsoft.Owin.Query#text", value);
            }
            return strs;
        }

        /// <summary>Gets unmodified values.</summary>
        /// <param name="store">The store.</param>
        /// <param name="key">  The key.</param>
        /// <returns>An array of string.</returns>
        internal static string[] GetUnmodifiedValues(IDictionary<string, string[]> store, string key)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            if (!store.TryGetValue(key, out var strArrays))
            {
                return null;
            }
            return strArrays;
        }

        /// <summary>Parse delimited.</summary>
        /// <param name="text">      The text.</param>
        /// <param name="delimiters">The delimiters.</param>
        /// <param name="callback">  The callback.</param>
        /// <param name="decodePlus">True to decode plus.</param>
        /// <param name="decodeKey"> True to decode key.</param>
        /// <param name="state">     The state.</param>
        internal static void ParseDelimited(
            string text,
            char[] delimiters,
            Action<string, string, object> callback,
            bool decodePlus,
            bool decodeKey,
            object state)
        {
            var num = 0;
            var length = text.Length;
            var num1 = text.IndexOf('=');
            if (num1 == -1)
            {
                num1 = length;
            }
            for (var i = 0; i < length; i = num + 1)
            {
                num = text.IndexOfAny(delimiters, i);
                if (num == -1)
                {
                    num = length;
                }
                if (num1 < num)
                {
                    while (i != num1 && char.IsWhiteSpace(text[i]))
                    {
                        i++;
                    }
                    var str = text[i..num1];
                    var str1 = text.Substring(num1 + 1, num - num1 - 1);
                    if (decodePlus)
                    {
                        str = str.Replace('+', ' ');
                        str1 = str1.Replace('+', ' ');
                    }
                    if (decodeKey)
                    {
                        str = Uri.UnescapeDataString(str);
                    }
                    str1 = Uri.UnescapeDataString(str1);
                    callback(str, str1, state);
                    num1 = text.IndexOf('=', num);
                    if (num1 == -1)
                    {
                        num1 = length;
                    }
                }
            }
        }

        /// <summary>De quote.</summary>
        /// <param name="value">The value.</param>
        /// <returns>A string.</returns>
        private static string DeQuote(string value)
        {
            if (!string.IsNullOrWhiteSpace(value)
                && value.Length > 1
                && value[0] == '\"'
                && value[^1] == '\"')
            {
                value = value[1..^1];
            }
            return value;
        }

        /// <summary>Gets the header split implementations in this collection.</summary>
        /// <param name="values">The values.</param>
        /// <returns>An enumerator that allows foreach to be used to process the header split implementations in this
        /// collection.</returns>
        private static IEnumerable<string> GetHeaderSplitImplementation(string[] values)
        {
            foreach (var headerSegment in new HeaderSegmentCollection(values))
            {
                if (!headerSegment.Data.HasValue)
                {
                    continue;
                }
                yield return DeQuote(headerSegment.Data.Value);
            }
        }

        /// <summary>Quote if needed.</summary>
        /// <param name="value">The value.</param>
        /// <returns>A string.</returns>
        private static string QuoteIfNeeded(string value)
        {
            if (!string.IsNullOrWhiteSpace(value)
                && value.Contains(',')
                && (value[0] != '\"' || value[^1] != '\"'))
            {
                value = string.Concat("\"", value, "\"");
            }
            return value;
        }
    }
}
