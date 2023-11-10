// <copyright file="AuthenticationProperties.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication properties class</summary>
namespace Microsoft.Owin.Security
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>Dictionary used to store state values about the authentication session.</summary>
    public class AuthenticationProperties
    {
        /// <summary>The expires UTC key.</summary>
        internal const string ExpiresUtcKey = ".expires";

        /// <summary>The is persistent key.</summary>
        internal const string IsPersistentKey = ".persistent";

        /// <summary>The issued UTC key.</summary>
        internal const string IssuedUtcKey = ".issued";

        /// <summary>The redirect URI key.</summary>
        internal const string RedirectUriKey = ".redirect";

        /// <summary>The refresh key.</summary>
        internal const string RefreshKey = ".refresh";

        /// <summary>The UTC date time format.</summary>
        internal const string UtcDateTimeFormat = "r";

        /// <summary>Initializes a new instance of the <see cref="AuthenticationProperties" />
        /// class.</summary>
        public AuthenticationProperties()
        {
            Dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
        }

        /// <summary>Initializes a new instance of the <see cref="AuthenticationProperties" />
        /// class.</summary>
        /// <param name="dictionary">.</param>
        public AuthenticationProperties(IDictionary<string, string> dictionary)
        {
            object strs = dictionary;
            if (strs == null)
            {
                strs = new Dictionary<string, string>(StringComparer.Ordinal);
            }
            Dictionary = (IDictionary<string, string>)strs;
        }

        /// <summary>Gets or sets if refreshing the authentication session should be allowed.</summary>
        /// <value>The allow refresh.</value>
        public bool? AllowRefresh
        {
            get
            {
                if (Dictionary.TryGetValue(".refresh", out var str) && bool.TryParse(str, out var flag))
                {
                    return flag;
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    var str = Dictionary;
                    var flag = value.Value;
                    str[".refresh"] = flag.ToString(CultureInfo.InvariantCulture);
                    return;
                }
                if (Dictionary.ContainsKey(".refresh"))
                {
                    Dictionary.Remove(".refresh");
                }
            }
        }

        /// <summary>State values about the authentication session.</summary>
        /// <value>The dictionary.</value>
        public IDictionary<string, string> Dictionary { get; }

        /// <summary>Gets or sets the time at which the authentication ticket expires.</summary>
        /// <value>The expires UTC.</value>
        public DateTimeOffset? ExpiresUtc
        {
            get
            {
                if (Dictionary.TryGetValue(".expires", out var str)
                    && DateTimeOffset.TryParseExact(
                        str,
                        "r",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.RoundtripKind,
                        out var dateTimeOffset))
                {
                    return dateTimeOffset;
                }
                return null;
            }
            set
            {
                if (!value.HasValue)
                {
                    if (Dictionary.ContainsKey(".expires"))
                    {
                        Dictionary.Remove(".expires");
                    }
                    return;
                }
                var str = Dictionary;
                var dateTimeOffset = value.Value;
                str[".expires"] = dateTimeOffset.ToString("r", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>Gets or sets whether the authentication session is persisted across multiple requests.</summary>
        /// <value>True if this AuthenticationProperties is persistent, false if not.</value>
        public bool IsPersistent
        {
            get => Dictionary.ContainsKey(".persistent");
            set
            {
                if (Dictionary.ContainsKey(".persistent"))
                {
                    if (!value)
                    {
                        Dictionary.Remove(".persistent");
                    }
                }
                else if (value)
                {
                    Dictionary.Add(".persistent", string.Empty);
                }
            }
        }

        /// <summary>Gets or sets the time at which the authentication ticket was issued.</summary>
        /// <value>The issued UTC.</value>
        public DateTimeOffset? IssuedUtc
        {
            get
            {
                if (Dictionary.TryGetValue(".issued", out var str)
                    && DateTimeOffset.TryParseExact(
                        str,
                        "r",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.RoundtripKind,
                        out var dateTimeOffset))
                {
                    return dateTimeOffset;
                }
                return null;
            }
            set
            {
                if (!value.HasValue)
                {
                    if (Dictionary.ContainsKey(".issued"))
                    {
                        Dictionary.Remove(".issued");
                    }
                    return;
                }
                var str = Dictionary;
                var dateTimeOffset = value.Value;
                str[".issued"] = dateTimeOffset.ToString("r", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>Gets or sets the full path or absolute URI to be used as an http redirect response value.</summary>
        /// <value>The redirect URI.</value>
        public string RedirectUri
        {
            get
            {
                if (!Dictionary.TryGetValue(".redirect", out var str))
                {
                    return null;
                }
                return str;
            }
            set
            {
                if (value != null)
                {
                    Dictionary[".redirect"] = value;
                    return;
                }
                if (Dictionary.ContainsKey(".redirect"))
                {
                    Dictionary.Remove(".redirect");
                }
            }
        }
    }
}
