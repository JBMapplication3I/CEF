// <copyright file="CEFCacheClientExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF HTTP cache feature class</summary>
namespace Clarity.Ecommerce.Service.Core
{
    using System;
    using JetBrains.Annotations;
    using ServiceStack;
    using ServiceStack.Caching;
    using ServiceStack.Text;
    using ServiceStack.Web;
    using Utilities;

    /// <summary>A CEF cache client extensions.</summary>
    public static class CEFCacheClientExtensions
    {
        /// <summary>An ICacheClient extension method that cef has valid cache.</summary>
        /// <param name="cacheClient">      The cacheClient to act on.</param>
        /// <param name="req">              The request.</param>
        /// <param name="cacheKey">         The cache key.</param>
        /// <param name="checkLastModified">The check last modified.</param>
        /// <param name="lastModified">     The last modified.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [PublicAPI]
        public static bool CEFHasValidCache(
            this ICacheClient cacheClient,
#pragma warning disable IDE0060 // Remove unused parameter
            IRequest req,
#pragma warning restore IDE0060 // Remove unused parameter
            string cacheKey,
            DateTime? checkLastModified,
            out DateTime? lastModified)
        {
            lastModified = null;
            if (!HostContext.GetPlugin<CEFHttpCacheFeature>().CEFShouldAddLastModifiedToOptimizedResults())
            {
                return false;
            }
            var ticks = cacheClient.Get<long>(DateCacheKey(cacheKey));
            if (ticks <= 0)
            {
                return false;
            }
            lastModified = new DateTime(ticks, DateTimeKind.Utc);
            return checkLastModified != null
                && checkLastModified.Value <= lastModified.Value;
        }

        /// <summary>An ICacheClient extension method that cef resolve from cache.</summary>
        /// <param name="cacheClient">The cacheClient to act on.</param>
        /// <param name="cacheKey">   The cache key.</param>
        /// <param name="request">    The request.</param>
        /// <returns>A nullable object.</returns>
        public static object? CEFResolveFromCache(
            this ICacheClient cacheClient,
            string cacheKey,
            IRequest request)
        {
            var checkModifiedSince = request.GetIfModifiedSince();
            string? modifiers = null;
            if (!request.ResponseContentType.IsBinary())
            {
                if (request.ResponseContentType == MimeTypes.Json)
                {
                    var jsonp = request.GetJsonpCallback();
                    if (jsonp != null)
                    {
                        modifiers = ".jsonp," + jsonp.SafeVarName();
                    }
                }
                var cacheKeySerialized = CacheClientExtensions.GetCacheKeyForSerialized(cacheKey, request.ResponseContentType, modifiers);
                /*
                var compressionType = request.GetCompressionType();
                var doCompression = compressionType != null;
                if (doCompression)
                {
                    var cacheKeySerializedZip = CacheClientExtensions.GetCacheKeyForCompressed(cacheKeySerialized, compressionType);
                    if (cacheClient.CEFHasValidCache(request, cacheKeySerializedZip, checkModifiedSince, out var lastModified))
                    {
                        return HttpResult.NotModified();
                    }
                    if (request.Response.GetHeader(HttpHeaders.CacheControl) != null)
                    {
                        lastModified = null;
                    }
                    var compressedResult = cacheClient.Get<byte[]>(cacheKeySerializedZip);
                    if (compressedResult != null)
                    {
                        return new CompressedResult(
                            compressedResult,
                            compressionType,
                            request.ResponseContentType)
                        {
                            LastModified = lastModified,
                        };
                    }
                }
                else
                {
                */
                if (cacheClient.CEFHasValidCache(request, cacheKeySerialized, checkModifiedSince, out _))
                {
                    return HttpResult.NotModified();
                }
                var retries = 0;
                var serializedResult = string.Empty;
                while (true && retries <= 3)
                {
                    serializedResult = cacheClient.Get<string>(cacheKeySerialized);
                    if (serializedResult != null)
                    {
                        try
                        {
                            JsonObject.Parse(serializedResult);
                            break;
                        }
                        catch (Exception err)
                        {
                            retries++;
                        }
                    }
                    else if (serializedResult == null)
                    {
                        return serializedResult;
                    }
                }
                return serializedResult;
            }
            else
            {
                var cacheKeySerialized = CacheClientExtensions.GetCacheKeyForSerialized(cacheKey, request.ResponseContentType, modifiers);
                if (cacheClient.CEFHasValidCache(request, cacheKeySerialized, checkModifiedSince, out _))
                {
                    return HttpResult.NotModified();
                }
                var serializedResult = cacheClient.Get<byte[]>(cacheKeySerialized);
                if (serializedResult != null)
                {
                    return serializedResult;
                }
            }
            return null;
        }

        /// <summary>An ICacheClient extension method that CEF cache.</summary>
        /// <param name="cacheClient">     The cacheClient to act on.</param>
        /// <param name="cacheKey">        The cache key.</param>
        /// <param name="responseDto">     The response data transfer object.</param>
        /// <param name="request">         The request.</param>
        /// <param name="lastModifiedData">Information describing the last modified.</param>
        /// <param name="expireCacheIn">   The expire cache in.</param>
        /// <returns>An object that represents the response to send.</returns>
        public static object? CEFCache(
            this ICacheClient cacheClient,
            string cacheKey,
            object responseDto,
            IRequest request,
            DateTime? lastModifiedData,
            TimeSpan? expireCacheIn = null)
        {
            request.Response.Dto = responseDto;
            cacheClient.Set(cacheKey, responseDto, expireCacheIn);
            string? modifiers = null;
            string cacheKeySerialized;
            if (request.ResponseContentType.IsBinary())
            {
                var serializedDtoBytes = HostContext.ContentTypes.SerializeToBytes(request, responseDto);
                cacheKeySerialized = CacheClientExtensions.GetCacheKeyForSerialized(
                    cacheKey,
                    request.ResponseContentType,
                    modifiers);
                cacheClient.Set(cacheKeySerialized, serializedDtoBytes, expireCacheIn);
                return serializedDtoBytes;
            }
            var serializedDto = request.CEFSerializeToString(responseDto);
            if (!Contract.CheckValidKey(serializedDto))
            {
                return null;
            }
            if (request.ResponseContentType.MatchesContentType(MimeTypes.Json))
            {
                var jsonpCallback = request.GetJsonpCallback();
                if (jsonpCallback is not null)
                {
                    // Wrap the response
                    serializedDto = jsonpCallback + "(" + serializedDto + ")";
                    // Add to the key
                    modifiers = ".jsonp," + jsonpCallback.SafeVarName();
                    // Add a default expire timespan for jsonp requests,
                    // because they aren't cleared when calling ClearCaches()
                    expireCacheIn ??= HostContext.Config.DefaultJsonpCacheExpiration;
                }
            }
            var mimeTypeKeyExtension = MimeTypes.GetExtension(request.ResponseContentType);
            if (cacheKey.EndsWith(mimeTypeKeyExtension) || cacheKey.Contains(mimeTypeKeyExtension + ":"))
            {
                // It's already that type, don't serialize it twice
                cacheKeySerialized = cacheKey;
            }
            else
            {
                cacheKeySerialized = CacheClientExtensions.GetCacheKeyForSerialized(
                    cacheKey,
                    request.ResponseContentType,
                    modifiers);
                cacheClient.Set(cacheKeySerialized, serializedDto, expireCacheIn);
            }
            var lastModified = HostContext.GetPlugin<CEFHttpCacheFeature>().CEFShouldAddLastModifiedToOptimizedResults()
                && string.IsNullOrEmpty(request.Response.GetHeader(HttpHeaders.CacheControl))
                    ? lastModifiedData ?? DateTime.UtcNow
                    : (DateTime?)null;
            if (lastModified != null)
            {
                cacheClient.Set(DateCacheKey(cacheKeySerialized), lastModified.Value.Ticks, expireCacheIn);
            }
            return serializedDto;
            /*
            var compressionType = request.GetCompressionType();
            if (compressionType is null)
            {
                return serializedDto;
            }
            var cacheKeySerializedZip = CacheClientExtensions.GetCacheKeyForCompressed(cacheKeySerialized, compressionType);
            var compressedSerializedDto = serializedDto.Compress(compressionType);
            cacheClient.Set(cacheKeySerializedZip, compressedSerializedDto, expireCacheIn);
            return compressedSerializedDto == null
                ? null
                : new CompressedResult(compressedSerializedDto, compressionType, request.ResponseContentType)
                {
                    Status = request.Response.StatusCode,
                    LastModified = lastModified,
                };
            */
        }

        /// <summary>An IRequest extension method that serialize to string.</summary>
        /// <param name="request">    The request to act on.</param>
        /// <param name="responseDto">The response dto.</param>
        /// <returns>A string.</returns>
        internal static string CEFSerializeToString(this IRequest request, object responseDto)
            => responseDto as string
            ?? HostContext.ContentTypes.SerializeToString(request, responseDto);

        private static string DateCacheKey(string cacheKey) => cacheKey + ".created";
    }
}
