namespace ServiceStack
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using Caching;
    using Configuration;
    using Host;
    using IO;
    using Web;

    public static class RequestExtensions
    {
        public static AuthUserSession ReloadSession(this IRequest request)
        {
            return request.GetSession() as AuthUserSession;
        }

        public static string GetCompressionType(this IRequest request)
        {
            if (request.RequestPreferences.AcceptsDeflate)
            {
                return CompressionTypes.Deflate;
            }
            if (request.RequestPreferences.AcceptsGzip)
            {
                return CompressionTypes.GZip;
            }
#if NET60_OR_GREATER
            if (request.RequestPreferences.AcceptsBrotli)
            {
                return CompressionTypes.Brotli;
            }
#endif
            return null;
        }

        public static string GetContentEncoding(this IRequest request)
        {
            return request.Headers.Get(HttpHeaders.ContentEncoding);
        }

        public static Stream GetInputStream(this IRequest req, Stream stream)
        {
            var enc = req.GetContentEncoding();
            if (enc == CompressionTypes.Deflate)
            {
                return new DeflateStream(stream, CompressionMode.Decompress);
            }
            if (enc == CompressionTypes.GZip)
            {
                return new GZipStream(stream, CompressionMode.Decompress);
            }
#if NET60_OR_GREATER
            if (enc == CompressionTypes.Brotli)
            {
                return new GZipStream(stream, CompressionMode.Decompress);
            }
#endif
            return stream;
        }

        public static string GetHeader(this IRequest request, string headerName)
        {
            return request.Headers.Get(headerName);
        }

        public static string GetParamInRequestHeader(this IRequest request, string name)
        {
            // Avoid reading request body for non x-www-form-urlencoded requests
            return request.Headers[name]
                ?? request.QueryString[name]
                ?? (!HostContext.Config.SkipFormDataInCreatingRequest && request.ContentType.MatchesContentType(MimeTypes.FormUrlEncoded)
                        ? request.FormData[name]
                        : null);
        }

        /// <summary>Returns the optimized result for the IRequestContext. Does not use or store results in any cache.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="request">The request to act on.</param>
        /// <param name="dto">    The data transfer object.</param>
        /// <returns>The given data converted to an object.</returns>
        public static object ToOptimizedResult<T>(this IRequest request, T dto)
        {
            request.Response.Dto = dto;
            var compressionType = request.GetCompressionType();
            if (compressionType == null)
            {
                return HostContext.ContentTypes.SerializeToString(request, dto);
            }
            using var ms = new MemoryStream();
            using var compressionStream = GetCompressionStream(ms, compressionType);
            HostContext.ContentTypes.SerializeToStream(request, dto, compressionStream);
            compressionStream.Close();
            var compressedBytes = ms.ToArray();
            return new CompressedResult(compressedBytes, compressionType, request.ResponseContentType)
            {
                Status = request.Response.StatusCode,
            };
        }

        private static Stream GetCompressionStream(Stream outputStream, string compressionType)
        {
            if (compressionType == CompressionTypes.Deflate)
            {
                return StreamExt.DeflateProvider.CompressStreamToStream(outputStream);
            }
            if (compressionType == CompressionTypes.GZip)
            {
                return StreamExt.GZipProvider.CompressStreamToStream(outputStream);
            }
#if NET60_OR_GREATER
            if (compressionType == CompressionTypes.Brotli)
            {
                return StreamExt.BrotliProvider.CompressStreamToStream(outputStream);
            }
#endif
            throw new NotSupportedException(compressionType);
        }

        /// <summary>Overload for the <see cref="ContentCacheManager.Resolve"/> method returning the most optimized
        /// result based on the MimeType and CompressionType from the IRequestContext.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="requestContext">The requestContext to act on.</param>
        /// <param name="cacheClient">   The cache client.</param>
        /// <param name="cacheKey">      The cache key.</param>
        /// <param name="factoryFn">     The factory function.</param>
        /// <returns>The given data converted to an object.</returns>
        public static object ToOptimizedResultUsingCache<T>(
            this IRequest requestContext,
            ICacheClient cacheClient,
            string cacheKey,
            Func<T> factoryFn)
        {
            return requestContext.ToOptimizedResultUsingCache(cacheClient, cacheKey, null, factoryFn);
        }

        /// <summary>Overload for the <see cref="ContentCacheManager.Resolve"/> method returning the most optimized
        /// result based on the MimeType and CompressionType from the IRequestContext.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="requestContext">The requestContext to act on.</param>
        /// <param name="cacheClient">   The cache client.</param>
        /// <param name="cacheKey">      The cache key.</param>
        /// <param name="expireCacheIn"> How long to cache for, null is no expiration.</param>
        /// <param name="factoryFn">     The factory function.</param>
        /// <returns>The given data converted to an object.</returns>
        public static object ToOptimizedResultUsingCache<T>(
            this IRequest requestContext, ICacheClient cacheClient, string cacheKey,
            TimeSpan? expireCacheIn, Func<T> factoryFn)
        {
            var cacheResult = cacheClient.ResolveFromCache(cacheKey, requestContext);
            if (cacheResult != null)
            {
                return cacheResult;
            }
            cacheResult = cacheClient.Cache(cacheKey, factoryFn(), requestContext, expireCacheIn);
            return cacheResult;
        }

        /// <summary>Clears all the serialized and compressed caches set by the 'Resolve' method for the cacheKey
        /// provided.</summary>
        /// <param name="requestContext">The requestContext to act on.</param>
        /// <param name="cacheClient">   The cache client.</param>
        /// <param name="cacheKeys">     A variable-length parameters list containing cache keys.</param>
        public static void RemoveFromCache(
            this IRequest requestContext,
            ICacheClient cacheClient,
            params string[] cacheKeys)
        {
            cacheClient.ClearCaches(cacheKeys);
        }

        /// <summary>Store an entry in the IHttpRequest.Items Dictionary.</summary>
        /// <param name="httpReq">The httpReq to act on.</param>
        /// <param name="key">    The key.</param>
        /// <param name="value">  The value.</param>
        public static void SetItem(this IRequest httpReq, string key, object value)
        {
            if (httpReq == null)
            {
                return;
            }
            httpReq.Items[key] = value;
        }

        /// <summary>Get an entry from the IHttpRequest.Items Dictionary.</summary>
        /// <param name="httpReq">The httpReq to act on.</param>
        /// <param name="key">    The key.</param>
        /// <returns>The item.</returns>
        public static object GetItem(this IRequest httpReq, string key)
        {
            if (httpReq == null)
            {
                return null;
            }
            httpReq.Items.TryGetValue(key, out var value);
            return value;
        }

#if !NETSTANDARD1_6
        public static RequestBaseWrapper ToHttpRequestBase(this IRequest httpReq)
        {
            return new((IHttpRequest)httpReq);
        }
#endif

        public static void SetInProcessRequest(this IRequest httpReq)
        {
            if (httpReq == null)
            {
                return;
            }
            httpReq.RequestAttributes |= RequestAttributes.InProcess;
        }

        public static bool IsInProcessRequest(this IRequest httpReq)
        {
            return (RequestAttributes.InProcess & httpReq?.RequestAttributes) == RequestAttributes.InProcess;
        }

        public static void ReleaseIfInProcessRequest(this IRequest httpReq)
        {
            if (httpReq == null)
            {
                return;
            }
            httpReq.RequestAttributes &= ~RequestAttributes.InProcess;
        }

        internal static T TryResolveInternal<T>(this IRequest request)
        {
            if (typeof(T) == typeof(IRequest))
            {
                return (T)request;
            }
            if (typeof(T) == typeof(IResponse))
            {
                return (T)request.Response;
            }
            return request is IHasResolver hasResolver
                ? hasResolver.Resolver.TryResolve<T>()
                : Service.GlobalResolver.TryResolve<T>();
        }

        public static IVirtualFile GetFile(this IRequest request)
        {
            return request is IHasVirtualFiles vfs ? vfs.GetFile() : null;
        }

        public static IVirtualDirectory GetDirectory(this IRequest request)
        {
            return request is IHasVirtualFiles vfs ? vfs.GetDirectory() : null;
        }

        public static bool IsFile(this IRequest request)
        {
            return request is IHasVirtualFiles { IsFile: true };
        }

        public static bool IsDirectory(this IRequest request)
        {
            return request is IHasVirtualFiles { IsDirectory: true };
        }
    }
}
