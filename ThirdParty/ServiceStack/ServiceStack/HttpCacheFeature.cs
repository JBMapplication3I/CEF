namespace ServiceStack
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using Text;
    using Web;

    public class HttpCacheFeature : IPlugin
    {
        public TimeSpan DefaultMaxAge { get; set; }
        public TimeSpan DefaultExpiresIn { get; set; }

        public Func<string, string> CacheControlFilter { get; set; }

        public string CacheControlForOptimizedResults { get; set; }

        public HttpCacheFeature()
        {
            DefaultMaxAge = TimeSpan.FromMinutes(10);
            DefaultExpiresIn = TimeSpan.FromMinutes(10);
            CacheControlForOptimizedResults = "max-age=0";
        }

        public void Register(IAppHost appHost)
        {
            appHost.GlobalResponseFilters.Add(HandleCacheResponses);
        }

        public void HandleCacheResponses(IRequest req, IResponse res, object response)
        {
            if (req.IsInProcessRequest())
            {
                return;
            }
            if (response is Exception || res.StatusCode >= 300)
            {
                return;
            }
            var cacheInfo = req.GetItem(Keywords.CacheInfo) as CacheInfo;
            if (cacheInfo?.CacheKey != null
                && CacheAndWriteResponse(cacheInfo, req, res, response))
            {
                return;
            }
            if (response is not HttpResult httpResult)
            {
                return;
            }
            cacheInfo = httpResult.ToCacheInfo();
            if (req.Verb != HttpMethods.Get && req.Verb != HttpMethods.Head
                || httpResult.StatusCode != HttpStatusCode.OK && httpResult.StatusCode != HttpStatusCode.NotModified)
            {
                return;
            }
            if (httpResult.LastModified != null)
            {
                httpResult.Headers[HttpHeaders.LastModified] = httpResult.LastModified.Value.ToUniversalTime().ToString("r");
            }
            if (httpResult.ETag != null)
            {
                httpResult.Headers[HttpHeaders.ETag] = httpResult.ETag.Quoted();
            }
            if (httpResult.Expires != null)
            {
                httpResult.Headers[HttpHeaders.Expires] = httpResult.Expires.Value.ToUniversalTime().ToString("r");
            }
            if (httpResult.Age != null)
            {
                httpResult.Headers[HttpHeaders.Age] = httpResult.Age.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture);
            }
            var alreadySpecifiedCacheControl = httpResult.Headers.ContainsKey(HttpHeaders.CacheControl);
            if (!alreadySpecifiedCacheControl)
            {
                var cacheControl = BuildCacheControlHeader(cacheInfo);
                if (cacheControl != null)
                {
                    httpResult.Headers[HttpHeaders.CacheControl] = cacheControl;
                }
            }
            if (req.ETagMatch(httpResult.ETag) || req.NotModifiedSince(httpResult.LastModified))
            {
                res.EndNotModified();
                httpResult.Dispose();
            }
        }

        private bool CacheAndWriteResponse(CacheInfo cacheInfo, IRequest req, IResponse res, object response)
        {
            var httpResult = response as IHttpResult;
            var dto = httpResult != null ? httpResult.Response : response;
            if (dto is null
#pragma warning disable CS0618 // Type or member is obsolete
                or IPartialWriter
                or IStreamWriter
#pragma warning restore CS0618 // Type or member is obsolete
                or IPartialWriterAsync
                or IStreamWriterAsync)
            {
                return false;
            }
            var expiresIn = cacheInfo.ExpiresIn.GetValueOrDefault(DefaultExpiresIn);
            var cache = cacheInfo.LocalCache ? HostContext.AppHost.GetMemoryCacheClient(req) : HostContext.AppHost.GetCacheClient(req);
            if (dto is not byte[] responseBytes)
            {
                responseBytes = dto switch
                {
                    string rawStr => rawStr.ToUtf8Bytes(),
                    Stream stream => stream.ReadFully(),
                    _ => null,
                };
            }
            var encoding = !cacheInfo.NoCompression
                ? req.GetCompressionType()
                : null;
            if (response is HttpResult customResult)
            {
                if (customResult.View != null)
                {
                    req.Items["View"] = customResult.View;
                }
                if (customResult.Template != null)
                {
                    req.Items["Template"] = customResult.Template;
                }
            }
            var cacheKeyEncoded = encoding != null ? cacheInfo.CacheKey + "." + encoding : null;
            if (responseBytes != null || req.ResponseContentType.IsBinary())
            {
                responseBytes ??= HostContext.ContentTypes.SerializeToBytes(req, dto);
                cache.Set(cacheInfo.CacheKey, responseBytes, expiresIn);
                if (encoding != null)
                {
                    res.AddHeader(HttpHeaders.ContentEncoding, encoding);
                    responseBytes = responseBytes.CompressBytes(encoding);
                    cache.Set(cacheKeyEncoded, responseBytes, expiresIn);
                }
            }
            else
            {
                var serializedDto = req.SerializeToString(dto);
                if (req.ResponseContentType.MatchesContentType(MimeTypes.Json))
                {
                    var jsonp = req.GetJsonpCallback();
                    if (jsonp != null)
                    {
                        serializedDto = jsonp + "(" + serializedDto + ")";
                    }
                }
                responseBytes = serializedDto.ToUtf8Bytes();
                cache.Set(cacheInfo.CacheKey, responseBytes, expiresIn);
                if (encoding != null)
                {
                    res.AddHeader(HttpHeaders.ContentEncoding, encoding);
                    responseBytes = responseBytes.CompressBytes(encoding);
                    cache.Set(cacheKeyEncoded, responseBytes, expiresIn);
                }
            }
            var doHttpCaching = cacheInfo.MaxAge != null || cacheInfo.CacheControl != CacheControl.None;
            if (doHttpCaching)
            {
                var cacheControl = BuildCacheControlHeader(cacheInfo);
                if (cacheControl != null)
                {
                    var lastModified = cacheInfo.LastModified.GetValueOrDefault(DateTime.UtcNow);
                    cache.Set("date:" + cacheInfo.CacheKey, lastModified, expiresIn);
                    res.AddHeaderLastModified(lastModified);
                    res.AddHeader(HttpHeaders.CacheControl, cacheControl);
                    if (encoding != null)
                    {
                        res.AddHeader(HttpHeaders.Vary, HttpHeaders.AcceptEncoding);
                    }
                    if (cacheInfo.VaryByUser)
                    {
                        res.AddHeader(HttpHeaders.Vary, HttpHeaders.Cookie);
                    }
                }
            }
            if (httpResult != null)
            {
                foreach (var header in httpResult.Headers)
                {
                    res.AddHeader(header.Key, header.Value);
                }
            }
#pragma warning disable CS0618 // Type or member is obsolete
            res.WriteBytesToResponse(responseBytes, req.ResponseContentType);
#pragma warning restore CS0618 // Type or member is obsolete
            return true;
        }

        public string BuildCacheControlHeader(CacheInfo cacheInfo)
        {
            var maxAge = cacheInfo.MaxAge;
            if (maxAge == null && (cacheInfo.LastModified != null || cacheInfo.ETag != null))
            {
                maxAge = DefaultMaxAge;
            }
            var cacheHeader = new List<string>();
            if (maxAge != null)
            {
                cacheHeader.Add("max-age=" + maxAge.Value.TotalSeconds);
            }
            if (cacheInfo.CacheControl != CacheControl.None)
            {
                var cache = cacheInfo.CacheControl;
                if (cache.Has(CacheControl.Public))
                {
                    cacheHeader.Add("public");
                }
                else if (cache.Has(CacheControl.Private))
                {
                    cacheHeader.Add("private");
                }
                if (cache.Has(CacheControl.MustRevalidate))
                {
                    cacheHeader.Add("must-revalidate");
                }
                if (cache.Has(CacheControl.NoCache))
                {
                    cacheHeader.Add("no-cache");
                }
                if (cache.Has(CacheControl.NoStore))
                {
                    cacheHeader.Add("no-store");
                }
                if (cache.Has(CacheControl.NoTransform))
                {
                    cacheHeader.Add("no-transform");
                }
                if (cache.Has(CacheControl.ProxyRevalidate))
                {
                    cacheHeader.Add("proxy-revalidate");
                }
            }
            if (cacheHeader.Count <= 0)
            {
                return null;
            }
            var cacheControl = cacheHeader.ToArray().Join(", ");
            return CacheControlFilter != null
                ? CacheControlFilter(cacheControl)
                : cacheControl;
        }
    }

    public static class HttpCacheExtensions
    {
        public static bool Has(this CacheControl cache, CacheControl flag)
        {
            return (flag & cache) != 0;
        }

        public static void EndNotModified(this IResponse res, string description = null)
        {
            res.StatusCode = 304;
            res.StatusDescription = description ?? HostContext.ResolveLocalizedString(LocalizedStrings.NotModified);
            res.EndRequest();
        }

        public static bool ETagMatch(this IRequest req, string eTag)
        {
            if (string.IsNullOrEmpty(eTag))
            {
                return false;
            }
            return eTag.StripWeakRef().Quoted() == req.Headers[HttpHeaders.IfNoneMatch].StripWeakRef().Quoted();
        }

        public static bool NotModifiedSince(this IRequest req, DateTime? lastModified)
        {
            if (lastModified == null)
            {
                return false;
            }
            var ifModifiedSince = req.Headers[HttpHeaders.IfModifiedSince];
            if (ifModifiedSince == null
                || !DateTime.TryParse(ifModifiedSince, new DateTimeFormatInfo(), DateTimeStyles.RoundtripKind, out var modifiedSinceDate))
            {
                return false;
            }
            var lastModifiedUtc = lastModified.Value.Truncate(TimeSpan.FromSeconds(1)).ToUniversalTime();
            return modifiedSinceDate >= lastModifiedUtc;
        }

        public static bool HasValidCache(this IRequest req, string eTag)
        {
            return req.ETagMatch(eTag);
        }

        public static bool HasValidCache(this IRequest req, DateTime? lastModified)
        {
            return req.NotModifiedSince(lastModified);
        }

        public static bool HasValidCache(this IRequest req, string eTag, DateTime? lastModified)
        {
            return req.ETagMatch(eTag) || req.NotModifiedSince(lastModified);
        }

        public static bool ShouldAddLastModifiedToOptimizedResults(this HttpCacheFeature feature)
        {
            return feature?.CacheControlForOptimizedResults != null;
        }

        internal static string StripWeakRef(this string eTag)
        {
            return eTag?.StartsWith("W/") == true
                ? eTag.Substring(2)
                : eTag;
        }
    }
}
