// <copyright file="ChunkingCookieManager.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the chunking cookie manager class</summary>
namespace Microsoft.Owin.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>This handles cookies that are limited by per cookie length. It breaks down long cookies for
    /// responses, and reassembles them from requests.</summary>
    /// <seealso cref="ICookieManager"/>
    /// <seealso cref="ICookieManager"/>
    public class ChunkingCookieManager : ICookieManager
    {
        /// <summary>Creates a new instance of ChunkingCookieManager.</summary>
        public ChunkingCookieManager()
        {
            ChunkSize = 4090;
            ThrowForPartialCookies = true;
        }

        /// <summary>The maximum size of cookie to send back to the client. If a cookie exceeds this size it will be
        /// broken down into multiple cookies. Set this value to null to disable this behavior. The default is 4090
        /// characters, which is supported by all common browsers. Note that browsers may also have limits on the total
        /// size of all cookies per domain, and on the number of cookies per domain.</summary>
        /// <value>The size of the chunk.</value>
        public int? ChunkSize
        {
            get;
            set;
        }

        /// <summary>Throw if not all chunks of a cookie are available on a request for re-assembly.</summary>
        /// <value>True if throw for partial cookies, false if not.</value>
        public bool ThrowForPartialCookies
        {
            get;
            set;
        }

        /// <summary>Appends a new response cookie to the Set-Cookie header. If the cookie is larger than the given size
        /// limit then it will be broken down into multiple cookies as follows: Set-Cookie: CookieName=chunks:3;
        /// path=/Set-Cookie: CookieNameC1=Segment1; path=/Set-Cookie: CookieNameC2=Segment2; path=/Set-Cookie:
        /// CookieNameC3=Segment3; path=/.</summary>
        /// <param name="context">.</param>
        /// <param name="key">    .</param>
        /// <param name="value">  .</param>
        /// <param name="options">.</param>
        public void AppendResponseCookie(IOwinContext context, string key, string value, CookieOptions options)
        {
            string str;
            string domain;
            string str1;
            string path;
            string str2;
            string str3;
            string str4;
            string str5;
            string str6;
            string stringRepresentationOfSameSite;
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            var flag = !string.IsNullOrEmpty(options.Domain);
            var flag1 = !string.IsNullOrEmpty(options.Path);
            var hasValue = options.Expires.HasValue;
            var hasValue1 = options.SameSite.HasValue;
            var str7 = Uri.EscapeDataString(key);
            var str8 = string.Concat(str7, "=");
            var strArrays = new string[10];
            if (!flag)
            {
                str = null;
            }
            else
            {
                str = "; domain=";
            }
            strArrays[0] = str;
            if (!flag)
            {
                domain = null;
            }
            else
            {
                domain = options.Domain;
            }
            strArrays[1] = domain;
            if (!flag1)
            {
                str1 = null;
            }
            else
            {
                str1 = "; path=";
            }
            strArrays[2] = str1;
            if (!flag1)
            {
                path = null;
            }
            else
            {
                path = options.Path;
            }
            strArrays[3] = path;
            if (!hasValue)
            {
                str2 = null;
            }
            else
            {
                str2 = "; expires=";
            }
            strArrays[4] = str2;
            if (!hasValue)
            {
                str3 = null;
            }
            else
            {
                var dateTime = options.Expires.Value;
                str3 = dateTime.ToString("ddd, dd-MMM-yyyy HH:mm:ss \\G\\M\\T", CultureInfo.InvariantCulture);
            }
            strArrays[5] = str3;
            if (!options.Secure)
            {
                str4 = null;
            }
            else
            {
                str4 = "; secure";
            }
            strArrays[6] = str4;
            if (!options.HttpOnly)
            {
                str5 = null;
            }
            else
            {
                str5 = "; HttpOnly";
            }
            strArrays[7] = str5;
            if (!hasValue1)
            {
                str6 = null;
            }
            else
            {
                str6 = "; SameSite=";
            }
            strArrays[8] = str6;
            if (!hasValue1)
            {
                stringRepresentationOfSameSite = null;
            }
            else
            {
                stringRepresentationOfSameSite = GetStringRepresentationOfSameSite(options.SameSite.Value);
            }
            strArrays[9] = stringRepresentationOfSameSite;
            var str9 = string.Concat(strArrays);
            value ??= string.Empty;
            var flag2 = false;
            if (IsQuoted(value))
            {
                flag2 = true;
                value = RemoveQuotes(value);
            }
            var str10 = Uri.EscapeDataString(value);
            var headers = context.Response.Headers;
            if (ChunkSize.HasValue)
            {
                if (ChunkSize.Value <= str8.Length + str10.Length + str9.Length + (flag2 ? 2 : 0))
                {
                    if (ChunkSize.Value < str8.Length + str9.Length + (flag2 ? 2 : 0) + 10)
                    {
                        throw new InvalidOperationException(Resources.Exception_CookieLimitTooSmall);
                    }
                    var chunkSize = ChunkSize;
                    var num = chunkSize.Value - str8.Length - str9.Length - (flag2 ? 2 : 0) - 3;
                    var num1 = (int)Math.Ceiling((double)str10.Length * 1 / num);
                    headers.AppendValues(
                        "Set-Cookie",
                        string.Concat(str8, "chunks:", num1.ToString(CultureInfo.InvariantCulture), str9));
                    var strArrays1 = new string[num1];
                    var num2 = 0;
                    for (var i = 1; i <= num1; i++)
                    {
                        var length = str10.Length - num2;
                        var num3 = Math.Min(num, length);
                        var str11 = str10.Substring(num2, num3);
                        num2 += num3;
                        var strArrays2 = strArrays1;
                        var num4 = i - 1;
                        string[] strArrays3 =
                        {
                            str7, "C", i.ToString(CultureInfo.InvariantCulture), "=", null, null, null, null,
                        };
                        strArrays3[4] = flag2 ? "\"" : string.Empty;
                        strArrays3[5] = str11;
                        strArrays3[6] = flag2 ? "\"" : string.Empty;
                        strArrays3[7] = str9;
                        strArrays2[num4] = string.Concat(strArrays3);
                    }
                    headers.AppendValues("Set-Cookie", strArrays1);
                    return;
                }
            }
            var str12 = string.Concat(str8, flag2 ? Quote(str10) : str10, str9);
            headers.AppendValues("Set-Cookie", str12);
        }

        /// <summary>Deletes the cookie with the given key by setting an expired state. If a matching chunked cookie
        /// exists on the request, delete each chunk.</summary>
        /// <param name="context">.</param>
        /// <param name="key">    .</param>
        /// <param name="options">.</param>
        public void DeleteCookie(IOwinContext context, string key, CookieOptions options)
        {
            Func<string, bool> func;
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            var str = Uri.EscapeDataString(key);
            var strs = new List<string> { string.Concat(str, "=") };
            var num = ParseChunksCount(context.Request.Cookies[key]);
            if (num > 0)
            {
                for (var i = 1; i <= num + 1; i++)
                {
                    var str1 = string.Concat(str, "C", i.ToString(CultureInfo.InvariantCulture));
                    strs.Add(string.Concat(str1, "="));
                }
            }
            var flag = !string.IsNullOrEmpty(options.Path);
            bool func1(string value) => strs.Any(k => value.StartsWith(k, StringComparison.OrdinalIgnoreCase));
            if (string.IsNullOrEmpty(options.Domain))
            {
                func = !flag
                    ? value => func1(value)
                    : new Func<string, bool>(
                        value =>
                        {
                            if (!func1(value))
                            {
                                return false;
                            }
                            return value.IndexOf(
                                    string.Concat("path=", options.Path),
                                    StringComparison.OrdinalIgnoreCase)
                                != -1;
                        });
            }
            else
            {
                func = value =>
                {
                    if (!func1(value))
                    {
                        return false;
                    }
                    return value.IndexOf(string.Concat("domain=", options.Domain), StringComparison.OrdinalIgnoreCase)
                        != -1;
                };
            }
            var headers = context.Response.Headers;
            var values = headers.GetValues("Set-Cookie");
            if (values != null)
            {
                headers.SetValues("Set-Cookie", (from value in values where !func(value) select value).ToArray());
            }
            AppendResponseCookie(
                context,
                key,
                string.Empty,
                new CookieOptions
                {
                    Path = options.Path,
                    Domain = options.Domain,
                    HttpOnly = options.HttpOnly,
                    SameSite = options.SameSite,
                    Secure = options.Secure,
                    Expires = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                });
            for (var j = 1; j <= num; j++)
            {
                AppendResponseCookie(
                    context,
                    string.Concat(key, "C", j.ToString(CultureInfo.InvariantCulture)),
                    string.Empty,
                    new CookieOptions
                    {
                        Path = options.Path,
                        Domain = options.Domain,
                        HttpOnly = options.HttpOnly,
                        SameSite = options.SameSite,
                        Secure = options.Secure,
                        Expires = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    });
            }
        }

        /// <summary>Get the reassembled cookie. Non chunked cookies are returned normally. Cookies with missing chunks
        /// just have their "chunks:XX" header returned.</summary>
        /// <param name="context">.</param>
        /// <param name="key">    .</param>
        /// <returns>The reassembled cookie, if any, or null.</returns>
        public string GetRequestCookie(IOwinContext context, string key)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var cookies = context.Request.Cookies;
            var item = cookies[key];
            var num = ParseChunksCount(item);
            if (num <= 0)
            {
                return item;
            }
            var flag = false;
            var strArrays = new string[num];
            for (var i = 1; i <= num; i++)
            {
                var str = cookies[string.Concat(key, "C", i.ToString(CultureInfo.InvariantCulture))];
                if (str == null)
                {
                    if (ThrowForPartialCookies)
                    {
                        var length = 0;
                        for (var j = 0; j < i - 1; j++)
                        {
                            length += strArrays[j].Length;
                        }
                        throw new FormatException(
                            string.Format(
                                CultureInfo.CurrentCulture,
                                Resources.Exception_ImcompleteChunkedCookie,
                                i - 1,
                                num,
                                length));
                    }
                    return item;
                }
                if (IsQuoted(str))
                {
                    flag = true;
                    str = RemoveQuotes(str);
                }
                strArrays[i - 1] = str;
            }
            var str1 = string.Join(string.Empty, strArrays);
            if (flag)
            {
                str1 = Quote(str1);
            }
            return str1;
        }

        /// <summary>Gets string representation of same site.</summary>
        /// <param name="siteMode">The site mode.</param>
        /// <returns>The string representation of same site.</returns>
        private static string GetStringRepresentationOfSameSite(SameSiteMode siteMode)
        {
            switch (siteMode)
            {
                case SameSiteMode.None:
                {
                    return "None";
                }
                case SameSiteMode.Lax:
                {
                    return "Lax";
                }
                case SameSiteMode.Strict:
                {
                    return "Strict";
                }
            }
            throw new ArgumentOutOfRangeException(
                nameof(siteMode),
                string.Format(CultureInfo.InvariantCulture, "Unexpected SameSiteMode value: {0}", siteMode));
        }

        /// <summary>Query if 'value' is quoted.</summary>
        /// <param name="value">The value.</param>
        /// <returns>True if quoted, false if not.</returns>
        private static bool IsQuoted(string value)
        {
            if (value.Length < 2 || value[0] != '\"')
            {
                return false;
            }
            return value[^1] == '\"';
        }

        /// <summary>Parse chunks count.</summary>
        /// <param name="value">The value.</param>
        /// <returns>An int.</returns>
        private static int ParseChunksCount(string value)
        {
            if (value != null
                && value.StartsWith("chunks:", StringComparison.Ordinal)
                && int.TryParse(
                    value["chunks:".Length..],
                    NumberStyles.None,
                    CultureInfo.InvariantCulture,
                    out var num))
            {
                return num;
            }
            return 0;
        }

        /// <summary>Quotes.</summary>
        /// <param name="value">The value.</param>
        /// <returns>A string.</returns>
        private static string Quote(string value)
        {
            return string.Concat("\"", value, "\"");
        }

        /// <summary>Removes the quotes described by value.</summary>
        /// <param name="value">The value.</param>
        /// <returns>A string.</returns>
        private static string RemoveQuotes(string value)
        {
            return value[1..^1];
        }
    }
}
