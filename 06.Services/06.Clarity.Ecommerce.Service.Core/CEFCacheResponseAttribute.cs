// <copyright file="CEFCacheResponseAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cached handler class</summary>
namespace Clarity.Ecommerce.Service.Core
{
    using System;
    using System.Text;
    using JSConfigs;
    using ServiceStack;
    using ServiceStack.Web;
    using Utilities;

    /// <summary>Cache the Response of a Service.</summary>
    public class CEFCacheResponseAttribute : RequestFilterAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="CEFCacheResponseAttribute"/> class.</summary>
        public CEFCacheResponseAttribute() => MaxAge = -1;

        /// <summary>Gets or sets the cache expiry in seconds. This is how long the cache will save inside the server
        /// before checking the actual data again.</summary>
        /// <value>The duration.</value>
        public int Duration { get; set; }

        /// <summary>Gets or sets the MaxAge in seconds. This is how long the browser waits before rechecking with the
        /// server.</summary>
        /// <value>The maximum age.</value>
        public int MaxAge { get; set; }

        /// <summary>Gets or sets the Cache-Control HTTP Headers.</summary>
        /// <value>The cache control.</value>
        public CacheControl CacheControl { get; set; }

        /// <summary>Gets or sets a value indicating whether to vary cache per user.</summary>
        /// <value>True if vary by user, false if not.</value>
        public bool VaryByUser { get; set; }

        /// <summary>Gets or sets vary cache for users in these roles.</summary>
        /// <value>The vary by roles.</value>
        public string[]? VaryByRoles { get; set; }

        /// <summary>Gets or sets vary cache for different HTTP Headers.</summary>
        /// <value>The vary by headers.</value>
        public string[]? VaryByHeaders { get; set; }

        /// <summary>Gets or sets a value indicating whether to use HostContext.LocalCache or HostContext.Cache.</summary>
        /// <value>True if local cache, false if not.</value>
        public bool LocalCache { get; set; }

        /// <summary>Gets or sets a value indicating whether to skip compression for this Cache Result.</summary>
        /// <value>True if no compression, false if not.</value>
        public bool NoCompression { get; set; }

        /// <summary>Builds a key.</summary>
        /// <param name="req">              The request.</param>
        /// <param name="requestDto">       The request DTO.</param>
        /// <param name="localCache">       True to local cache.</param>
        /// <param name="noCompression">    True to no compression.</param>
        /// <param name="expiresIn">        The expires in.</param>
        /// <param name="maxAge">           The maximum age.</param>
        /// <param name="varyByUser">       True to vary by user.</param>
        /// <param name="varyByRoles">      The vary by roles.</param>
        /// <param name="varyByHeaders">    The vary by headers.</param>
        /// <param name="cacheControlFlags">The cache control flags.</param>
        /// <returns>A string.</returns>
        public static string? BuildKey(
            IRequest req,
            object requestDto,
            bool localCache = false,
            bool noCompression = false,
            TimeSpan? expiresIn = null,
            TimeSpan? maxAge = null,
            bool varyByUser = false,
            string[]? varyByRoles = null,
            string[]? varyByHeaders = null,
            CacheControl cacheControlFlags = CacheControl.Public | CacheControl.MustRevalidate)
        {
            if (req.Verb != "GET" && req.Verb != "HEAD" || req.IsInProcessRequest())
            {
                return null;
            }
            if (HostContext.GetPlugin<CEFHttpCacheFeature>() == null)
            {
                throw new NotSupportedException(ErrorMessages.CacheFeatureMustBeEnabled.Fmt("[CEFCacheResponse]"));
            }
            var modifiers = new StringBuilder();
            AppendJsonP(req, modifiers);
            AppendUserSessionToKey(req, modifiers, varyByUser);
            AppendRolesToKey(req, modifiers, varyByRoles);
            AppendHeadersToKey(req, modifiers, varyByHeaders);
            AppendParametersToKey(requestDto, modifiers);
            if (CEFConfigDictionary.BrandsEnabled)
            {
                AppendReferralUrlToKey(req, modifiers);
            }
            modifiers
                .AppendIf(modifiers.Length > 0, ":")
                .Append(MimeTypes.GetExtension(req.ResponseContentType));
            var cacheInfo = new CacheInfo
            {
                KeyBase = UrnId.Create(requestDto.GetType(), string.Empty),
                KeyModifiers = modifiers.ToString(),
                ExpiresIn = expiresIn?.TotalSeconds > 0
                    ? expiresIn.Value
                    : default(TimeSpan?),
                MaxAge = maxAge?.TotalSeconds >= 0 ? maxAge.Value : default(TimeSpan?),
                CacheControl = cacheControlFlags,
                VaryByUser = varyByUser,
                LocalCache = localCache,
                NoCompression = noCompression,
            };
            return cacheInfo.CacheKey;
        }

        /// <inheritdoc/>
        public override void Execute(IRequest req, IResponse res, object requestDto)
        {
            if (req.Verb != "GET" && req.Verb != "HEAD" || req.IsInProcessRequest())
            {
                return;
            }
            if (HostContext.GetPlugin<CEFHttpCacheFeature>() == null)
            {
                throw new NotSupportedException(ErrorMessages.CacheFeatureMustBeEnabled.Fmt("[CEFCacheResponse]"));
            }
            var modifiers = new StringBuilder();
            AppendJsonP(req, modifiers);
            AppendUserSessionToKey(req, modifiers, VaryByUser);
            AppendRolesToKey(req, modifiers, VaryByRoles);
            AppendHeadersToKey(req, modifiers, VaryByHeaders);
            AppendParametersToKey(requestDto, modifiers);
            if (CEFConfigDictionary.BrandsEnabled)
            {
                AppendReferralUrlToKey(req, modifiers);
            }
            modifiers
                .AppendIf(modifiers.Length > 0, ":")
                .Append(MimeTypes.GetExtension(req.ResponseContentType));
            var cacheInfo = new CacheInfo
            {
                KeyBase = UrnId.Create(requestDto.GetType(), string.Empty),
                KeyModifiers = modifiers.ToString(),
                ExpiresIn = Duration > 0 ? TimeSpan.FromSeconds(Duration) : default(TimeSpan?),
                MaxAge = MaxAge >= 0 ? TimeSpan.FromSeconds(MaxAge) : default(TimeSpan?),
                CacheControl = CacheControl,
                VaryByUser = VaryByUser,
                LocalCache = LocalCache,
                NoCompression = NoCompression,
            };
            if (req.CEFHandleValidCache(cacheInfo))
            {
                return;
            }
            req.Items[Keywords.CacheInfo] = cacheInfo;
        }

        private static void AppendJsonP(IRequest req, StringBuilder modifiers)
        {
            if (req.ResponseContentType != "application/json")
            {
                return;
            }
            var jsonpCallback = req.GetJsonpCallback();
            if (jsonpCallback == null)
            {
                return;
            }
            if (modifiers.Length > 0)
            {
                modifiers.Append('+');
            }
            modifiers.Append("jsonp:").Append(jsonpCallback.SafeVarName());
        }

        private static void AppendParametersToKey(object requestDto, StringBuilder modifiers)
        {
            if (requestDto == null!)
            {
                return;
            }
            var asQueryString = requestDto.ToQueryString();
            if (string.IsNullOrWhiteSpace(asQueryString))
            {
                return;
            }
            if (modifiers.Length > 0)
            {
                modifiers.Append('+');
            }
            modifiers.Append(asQueryString.Replace("?", string.Empty).Replace(":", "{c}"));
        }

        private static void AppendHeadersToKey(IRequest req, StringBuilder modifiers, string[]? varyByHeaders)
        {
            if (varyByHeaders == null || varyByHeaders.Length == 0)
            {
                return;
            }
            foreach (var varyByHeader in varyByHeaders)
            {
                var header = req.GetHeader(varyByHeader);
                if (string.IsNullOrEmpty(header))
                {
                    continue;
                }
                if (modifiers.Length > 0)
                {
                    modifiers.Append('+');
                }
                modifiers.Append(varyByHeader).Append(':').Append(header.Replace(":", "{c}"));
            }
        }

        private static void AppendRolesToKey(IRequest req, StringBuilder modifiers, string[]? varyByRoles)
        {
            if (varyByRoles == null || varyByRoles.Length == 0)
            {
                return;
            }
            var session = req.GetSession();
            if (session == null)
            {
                return;
            }
            var authRepository = HostContext.AppHost.GetAuthRepository(req);
            // ReSharper disable once SuspiciousTypeConversion.Global
            using (authRepository as IDisposable)
            {
                foreach (var role in varyByRoles)
                {
                    if (!session.HasRole(role, authRepository))
                    {
                        continue;
                    }
                    if (modifiers.Length > 0)
                    {
                        modifiers.Append('+');
                    }
                    modifiers.Append("role:").Append(role.Replace(":", "{c}"));
                }
            }
        }

        private static void AppendUserSessionToKey(IRequest req, StringBuilder modifiers, bool varyByUser)
        {
            if (!varyByUser)
            {
                return;
            }
            if (modifiers.Length > 0)
            {
                modifiers.Append('+');
            }
            modifiers.Append("user:").Append(req.GetSessionId());
        }

        private static void AppendReferralUrlToKey(IRequest req, StringBuilder modifiers)
        {
            if (!Contract.CheckValidKey(req.UrlReferrer?.Host))
            {
                return;
            }
            if (modifiers.Length > 0)
            {
                modifiers.Append('+');
            }
            modifiers.Append("referralUrl:").Append(req.UrlReferrer!.Host);
        }
    }
}
