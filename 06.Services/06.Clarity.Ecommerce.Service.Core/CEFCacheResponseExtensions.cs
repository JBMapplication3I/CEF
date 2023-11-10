// <copyright file="CEFCacheResponseExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF cache response extensions class</summary>
namespace Clarity.Ecommerce.Service.Core
{
    using System;
    using ServiceStack;
    using ServiceStack.Web;

    /// <summary>A CEF cache response extensions.</summary>
    public static class CEFCacheResponseExtensions
    {
        /// <summary>A CacheInfo extension method that CEF last modified key.</summary>
        /// <param name="cacheInfo">The cacheInfo to act on.</param>
        /// <returns>A string.</returns>
        public static string CEFLastModifiedKey(this CacheInfo cacheInfo) => "date:" + cacheInfo.CacheKey;

        /// <summary>An IRequest extension method that CEF handle valid cache.</summary>
        /// <param name="req">      The req to act on.</param>
        /// <param name="cacheInfo">Information describing the cache.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CEFHandleValidCache(this IRequest req, CacheInfo? cacheInfo)
        {
            if (cacheInfo is null)
            {
                return false;
            }
            var response = req.Response;
            var cacheClient = cacheInfo.LocalCache
                ? HostContext.AppHost.GetMemoryCacheClient(req)
                : HostContext.AppHost.GetCacheClient(req);
            var lastModified = default(DateTime?);
            var expirable = cacheInfo.MaxAge.HasValue || (ulong)cacheInfo.CacheControl > 0UL;
            if (expirable)
            {
                lastModified = cacheClient.Get<DateTime?>(cacheInfo.CEFLastModifiedKey());
                if (req.CEFHasValidCache(lastModified))
                {
                    response.CEFEndNotModified();
                    return true;
                }
            }
            var compressionType = cacheInfo.NoCompression ? null : req.GetCompressionType();
            var responseBytes = compressionType is null
                ? cacheClient.Get<byte[]>(cacheInfo.CacheKey)
                : cacheClient.Get<byte[]>(cacheInfo.CacheKey + "." + compressionType);
            if (responseBytes is null)
            {
                return false;
            }
            if (compressionType is not null)
            {
                response.AddHeader(HttpHeaders.ContentEncoding, compressionType);
            }
            if (cacheInfo.VaryByUser)
            {
                response.AddHeader(HttpHeaders.Vary, HttpHeaders.Cookie);
            }
            var cacheControlHeader = HostContext.GetPlugin<CEFHttpCacheFeature>().BuildCacheControlHeader(cacheInfo);
            if (cacheControlHeader is not null)
            {
                response.AddHeader(HttpHeaders.CacheControl, cacheControlHeader);
            }
            if (!expirable)
            {
                lastModified = cacheClient.Get<DateTime?>(cacheInfo.CEFLastModifiedKey());
            }
            if (lastModified.HasValue)
            {
                response.AddHeader(HttpHeaders.LastModified, lastModified.Value.ToUniversalTime().ToString("r"));
            }
#pragma warning disable 618 // Cannot Satisfy as there isn't an async response filter list available
            response.WriteBytesToResponse(responseBytes, req.ResponseContentType);
#pragma warning restore 618
            return true;
        }
    }
}
