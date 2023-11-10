﻿namespace ServiceStack
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Caching;
    using Web;

    public static class CacheClientExtensions
    {
        public static void Set<T>(this ICacheClient cacheClient, string cacheKey, T value, TimeSpan? expireCacheIn)
        {
            if (expireCacheIn.HasValue)
            {
                cacheClient.Set(cacheKey, value, expireCacheIn.Value);
            }
            else
            {
                cacheClient.Set(cacheKey, value);
            }
        }

        private static string DateCacheKey(string cacheKey)
        {
            return cacheKey + ".created";
        }

        public static DateTime? GetDate(this IRequest req)
        {
            var date = req.Headers[HttpHeaders.Date];
            if (date == null)
            {
                return null;
            }
            if (!DateTime.TryParse(date, new DateTimeFormatInfo(), DateTimeStyles.RoundtripKind, out var value))
            {
                return null;
            }
            return value;
        }

        public static DateTime? GetIfModifiedSince(this IRequest req)
        {
            var ifModifiedSince = req.Headers[HttpHeaders.IfModifiedSince];
            if (ifModifiedSince == null)
            {
                return null;
            }
            if (!DateTime.TryParse(ifModifiedSince, new DateTimeFormatInfo(), DateTimeStyles.RoundtripKind, out var value))
            {
                return null;
            }
            return value;
        }

        public static bool HasValidCache(
            this ICacheClient cacheClient,
            IRequest req,
            string cacheKey,
            DateTime? checkLastModified,
            out DateTime? lastModified)
        {
            lastModified = null;
            if (!HostContext.GetPlugin<HttpCacheFeature>().ShouldAddLastModifiedToOptimizedResults())
            {
                return false;
            }
            var ticks = cacheClient.Get<long>(DateCacheKey(cacheKey));
            if (ticks <= 0)
            {
                return false;
            }
            lastModified = new DateTime(ticks, DateTimeKind.Utc);
            if (checkLastModified == null)
            {
                return false;
            }
            return checkLastModified.Value <= lastModified.Value;
        }

        public static object ResolveFromCache(this ICacheClient cacheClient,
            string cacheKey,
            IRequest request)
        {
            DateTime? lastModified;
            var checkModifiedSince = GetIfModifiedSince(request);
            string modifiers = null;
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
                var cacheKeySerialized = GetCacheKeyForSerialized(cacheKey, request.ResponseContentType, modifiers);

                var compressionType = request.GetCompressionType();
                var doCompression = compressionType != null;
                if (doCompression)
                {
                    var cacheKeySerializedZip = GetCacheKeyForCompressed(cacheKeySerialized, compressionType);
                    if (cacheClient.HasValidCache(request, cacheKeySerializedZip, checkModifiedSince, out lastModified))
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
                    if (cacheClient.HasValidCache(request, cacheKeySerialized, checkModifiedSince, out lastModified))
                    {
                        return HttpResult.NotModified();
                    }
                    var serializedResult = cacheClient.Get<string>(cacheKeySerialized);
                    if (serializedResult != null)
                    {
                        return serializedResult;
                    }
                }
            }
            else
            {
                var cacheKeySerialized = GetCacheKeyForSerialized(cacheKey, request.ResponseContentType, modifiers);
                if (cacheClient.HasValidCache(request, cacheKeySerialized, checkModifiedSince, out lastModified))
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

        internal static string SerializeToString(this IRequest request, object responseDto)
        {
            var str = responseDto as string;
            return str ?? HostContext.ContentTypes.SerializeToString(request, responseDto);
        }

        public static object Cache(this ICacheClient cacheClient,
            string cacheKey,
            object responseDto,
            IRequest request,
            TimeSpan? expireCacheIn = null)
        {
            request.Response.Dto = responseDto;
            cacheClient.Set(cacheKey, responseDto, expireCacheIn);
            string modifiers = null;
            string cacheKeySerialized;
            if (request.ResponseContentType.IsBinary())
            {
                var serializedDtoBytes = HostContext.ContentTypes.SerializeToBytes(request, responseDto);
                cacheKeySerialized = GetCacheKeyForSerialized(cacheKey, request.ResponseContentType, modifiers);
                cacheClient.Set(cacheKeySerialized, serializedDtoBytes, expireCacheIn);
                return serializedDtoBytes;
            }
            var serializedDto = SerializeToString(request, responseDto);
            if (request.ResponseContentType.MatchesContentType(MimeTypes.Json))
            {
                var jsonp = request.GetJsonpCallback();
                if (jsonp != null)
                {
                    modifiers = ".jsonp," + jsonp.SafeVarName();
                    serializedDto = jsonp + "(" + serializedDto + ")";
                    // Add a default expire timespan for jsonp requests,
                    // because they aren't cleared when calling ClearCaches()
                    expireCacheIn ??= HostContext.Config.DefaultJsonpCacheExpiration;
                }
            }
            cacheKeySerialized = GetCacheKeyForSerialized(cacheKey, request.ResponseContentType, modifiers);
            cacheClient.Set(cacheKeySerialized, serializedDto, expireCacheIn);
            var compressionType = request.GetCompressionType();
            if (compressionType == null)
            {
                return serializedDto;
            }
            var lastModified = HostContext.GetPlugin<HttpCacheFeature>().ShouldAddLastModifiedToOptimizedResults()
                && string.IsNullOrEmpty(request.Response.GetHeader(HttpHeaders.CacheControl))
                    ? DateTime.UtcNow
                    : (DateTime?)null;
            var cacheKeySerializedZip = GetCacheKeyForCompressed(cacheKeySerialized, compressionType);
            var compressedSerializedDto = serializedDto.Compress(compressionType);
            cacheClient.Set(cacheKeySerializedZip, compressedSerializedDto, expireCacheIn);
            if (lastModified != null)
            {
                cacheClient.Set(DateCacheKey(cacheKeySerializedZip), lastModified.Value.Ticks, expireCacheIn);
            }
            return compressedSerializedDto != null
                ? new CompressedResult(compressedSerializedDto, compressionType, request.ResponseContentType)
                {
                    Status = request.Response.StatusCode,
                    LastModified = lastModified,
                }
                : null;
        }

        public static void ClearCaches(this ICacheClient cacheClient, params string[] cacheKeys)
        {
            var allContentTypes = new List<string>(HostContext.ContentTypes.ContentTypeFormats.Values)
            {
                MimeTypes.XmlText,
                MimeTypes.JsonText,
                MimeTypes.JsvText,
            };
            var allCacheKeys = new List<string>();
            foreach (var cacheKey in cacheKeys)
            {
                allCacheKeys.Add(cacheKey);
                foreach (var serializedCacheKey in allContentTypes.Select(serializedExt => GetCacheKeyForSerialized(cacheKey, serializedExt, null)))
                {
                    allCacheKeys.Add(serializedCacheKey);
                    allCacheKeys.AddRange(
                        CompressionTypes.AllCompressionTypes
                            .Select(ct => GetCacheKeyForCompressed(serializedCacheKey, ct)));
                }
            }
            cacheClient.RemoveAll(allCacheKeys);
        }

        public static string GetCacheKeyForSerialized(string cacheKey, string mimeType, string modifiers)
        {
            return cacheKey + MimeTypes.GetExtension(mimeType) + modifiers;
        }

        public static string GetCacheKeyForCompressed(string cacheKeySerialized, string compressionType)
        {
            return cacheKeySerialized + "." + compressionType;
        }

        /// <summary>Removes items from cache that have keys matching the specified wildcard pattern.</summary>
        /// <param name="cacheClient">Cache client.</param>
        /// <param name="pattern">    The wildcard, where "*" means any sequence of characters and "?" means any single
        ///                           character.</param>
        public static void RemoveByPattern(this ICacheClient cacheClient, string pattern)
        {
            if (cacheClient is not IRemoveByPattern canRemoveByPattern)
            {
                throw new NotImplementedException(
                    "IRemoveByPattern is not implemented on: " + cacheClient.GetType().FullName);
            }
            canRemoveByPattern.RemoveByPattern(pattern);
        }

        /// <summary>Removes items from the cache based on the specified regular expression pattern.</summary>
        /// <param name="cacheClient">Cache client.</param>
        /// <param name="regex">      Regular expression pattern to search cache keys.</param>
        public static void RemoveByRegex(this ICacheClient cacheClient, string regex)
        {
            if (cacheClient is not IRemoveByPattern canRemoveByPattern)
            {
                throw new NotImplementedException(
                    "IRemoveByPattern is not implemented by: " + cacheClient.GetType().FullName);
            }
            canRemoveByPattern.RemoveByRegex(regex);
        }

        public static IEnumerable<string> GetKeysByPattern(this ICacheClient cache, string pattern)
        {
            if (cache is not ICacheClientExtended extendedCache)
            {
                throw new NotImplementedException(
                    "ICacheClientExtended is not implemented by: " + cache.GetType().FullName);
            }
            return extendedCache.GetKeysByPattern(pattern);
        }

        public static IEnumerable<string> GetAllKeys(this ICacheClient cache)
        {
            return cache.GetKeysByPattern("*");
        }

        public static IEnumerable<string> GetKeysStartingWith(this ICacheClient cache, string prefix)
        {
            return cache.GetKeysByPattern(prefix + "*");
        }

        public static T GetOrCreate<T>(this ICacheClient cache, string key, Func<T> createFn)
        {
            var value = cache.Get<T>(key);
            if (Equals(value, default(T)))
            {
                value = createFn();
                cache.Set(key, value);
            }
            return value;
        }

        public static T GetOrCreate<T>(this ICacheClient cache, string key, TimeSpan expiresIn, Func<T> createFn)
        {
            var value = cache.Get<T>(key);
            if (Equals(value, default(T)))
            {
                value = createFn();
                cache.Set(key, value, expiresIn);
            }
            return value;
        }

        public static TimeSpan? GetTimeToLive(this ICacheClient cache, string key)
        {
            if (cache is not ICacheClientExtended extendedCache)
            {
                throw new("GetTimeToLive is not implemented by: " + cache.GetType().FullName);
            }
            return extendedCache.GetTimeToLive(key);
        }
    }
}
