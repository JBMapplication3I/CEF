// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.ResponseCookieCollection
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>A wrapper for the response Set-Cookie header.</summary>
    public class ResponseCookieCollection
    {
        /// <summary>Create a new wrapper.</summary>
        /// <param name="headers">The headers.</param>
        public ResponseCookieCollection(IHeaderDictionary headers)
        {
            Headers = headers ?? throw new ArgumentNullException(nameof(headers));
        }

        /// <summary>Gets the headers.</summary>
        /// <value>The headers.</value>
        private IHeaderDictionary Headers { get; }

        /// <summary>Add a new cookie and value.</summary>
        /// <param name="key">  The key.</param>
        /// <param name="value">The value.</param>
        public void Append(string key, string value)
        {
            Headers.AppendValues(
                "Set-Cookie",
                Uri.EscapeDataString(key) + "=" + Uri.EscapeDataString(value) + "; path=/");
        }

        /// <summary>Add a new cookie.</summary>
        /// <param name="key">    The key.</param>
        /// <param name="value">  The value.</param>
        /// <param name="options">Options for controlling the operation.</param>
        public void Append(string key, string value, CookieOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            var flag1 = !string.IsNullOrEmpty(options.Domain);
            var flag2 = !string.IsNullOrEmpty(options.Path);
            var hasValue1 = options.Expires.HasValue;
            var hasValue2 = options.SameSite.HasValue;
            Headers.AppendValues(
                "Set-Cookie",
                Uri.EscapeDataString(key)
                + "="
                + Uri.EscapeDataString(value ?? string.Empty)
                + (!flag1 ? (string)null : "; domain=")
                + (!flag1 ? (string)null : options.Domain)
                + (!flag2 ? (string)null : "; path=")
                + (!flag2 ? (string)null : options.Path)
                + (!hasValue1 ? (string)null : "; expires=")
                + (!hasValue1
                    ? (string)null
                    : options.Expires.Value.ToString(
                        "ddd, dd-MMM-yyyy HH:mm:ss \\G\\M\\T",
                        CultureInfo.InvariantCulture))
                + (!options.Secure ? (string)null : "; secure")
                + (!options.HttpOnly ? (string)null : "; HttpOnly")
                + (!hasValue2 ? (string)null : "; SameSite=")
                + (!hasValue2 ? (string)null : GetStringRepresentationOfSameSite(options.SameSite.Value)));
        }

        /// <summary>Sets an expired cookie.</summary>
        /// <param name="key">The key to delete.</param>
        public void Delete(string key)
        {
            bool predicate(string value) => value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase);
            var strArray = new[] { Uri.EscapeDataString(key) + "=; expires=Thu, 01-Jan-1970 00:00:00 GMT" };
            var values = Headers.GetValues("Set-Cookie");
            if (values == null || values.Count == 0)
            {
                Headers.SetValues("Set-Cookie", strArray);
                return;
            }
            Headers.SetValues("Set-Cookie", values.Where(value => !predicate(value)).Concat(strArray).ToArray());
        }

        /// <summary>Sets an expired cookie.</summary>
        /// <param name="key">    The key.</param>
        /// <param name="options">Options for controlling the operation.</param>
        public void Delete(string key, CookieOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            var num = !string.IsNullOrEmpty(options.Domain) ? 1 : 0;
            var flag = !string.IsNullOrEmpty(options.Path);
            var rejectPredicate = num == 0
                ? !flag
                    ? value => value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase)
                    : (Func<string, bool>)(value
                        => value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase)
                        && value.IndexOf("path=" + options.Path, StringComparison.OrdinalIgnoreCase) != -1)
                : value => value.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase)
                    && value.IndexOf("domain=" + options.Domain, StringComparison.OrdinalIgnoreCase) != -1;
            var values = Headers.GetValues("Set-Cookie");
            if (values != null)
            {
                Headers.SetValues("Set-Cookie", values.Where(value => !rejectPredicate(value)).ToArray());
            }
            Append(
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
    }
}
