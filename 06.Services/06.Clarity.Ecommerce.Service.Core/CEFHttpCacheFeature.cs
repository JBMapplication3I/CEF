// <copyright file="CEFHttpCacheFeature.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF HTTP cache feature class</summary>
namespace Clarity.Ecommerce.Service.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using DocumentFormat.OpenXml.ExtendedProperties;
    using ServiceStack;
    using ServiceStack.Html;
    using ServiceStack.Web;
    using Utilities;

    /// <summary>A cef HTTP cache feature.</summary>
    /// <seealso cref="IPlugin"/>
    public class CEFHttpCacheFeature : IPlugin
    {
        /// <summary>Initializes a new instance of the <see cref="CEFHttpCacheFeature"/> class.</summary>
        public CEFHttpCacheFeature()
        {
            DefaultMaxAge = TimeSpan.FromMinutes(10.0);
            DefaultExpiresIn = TimeSpan.FromMinutes(10.0);
            CacheControlForOptimizedResults = "max-age=0";
        }

        /// <summary>Gets or sets the default maximum age.</summary>
        /// <value>The default maximum age.</value>
        public TimeSpan DefaultMaxAge { get; set; }

        /// <summary>Gets or sets the default expires in.</summary>
        /// <value>The default expires in.</value>
        public TimeSpan DefaultExpiresIn { get; set; }

        /// <summary>Gets or sets the cache control filter.</summary>
        /// <value>The cache control filter.</value>
        public Func<string, string>? CacheControlFilter { get; set; }

        /// <summary>Gets or sets the cache control for optimized results.</summary>
        /// <value>The cache control for optimized results.</value>
        public string CacheControlForOptimizedResults { get; set; }

        /// <summary>Registers this CEFHttpCacheFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            appHost.GlobalResponseFilters.Add(HandleCacheResponses);
        }

        /// <summary>Handles the cache responses.</summary>
        /// <param name="req">     The request.</param>
        /// <param name="res">     The resource.</param>
        /// <param name="response">The response.</param>
        public void HandleCacheResponses(IRequest req, IResponse res, object response)
        {
            if (req.IsInProcessRequest()
                || response is Exception
                || res.StatusCode >= 300
                || req.GetItem(Keywords.CacheInfo) is CacheInfo { CacheKey: { } } cacheInfo
                    && CacheAndWriteResponse(cacheInfo, req, res, response)
                || response is not HttpResult httpResult)
            {
                return;
            }
            res.RemoveHeader(HttpHeaders.CacheControl);
            res.AddHeader(HttpHeaders.CacheControl, "max-age=1800, must-revalidate, public");
            //var cacheInfo1 = httpResult.ToCacheInfo();
            //if (req.Verb != "GET" && req.Verb != "HEAD"
            //    || httpResult.StatusCode != HttpStatusCode.OK
            //        && httpResult.StatusCode != HttpStatusCode.NotModified)
            //{
            //    return;
            //}
            //if (httpResult.LastModified.HasValue)
            //{
            //    httpResult.Headers["Last-Modified"] = httpResult.LastModified.Value.ToUniversalTime().ToString("r");
            //}
            //if (httpResult.ETag != null)
            //{
            //    httpResult.Headers["ETag"] = httpResult.ETag.Quoted();
            //}
            //if (httpResult.Expires.HasValue)
            //{
            //    httpResult.Headers["Expires"] = httpResult.Expires.Value.ToUniversalTime().ToString("r");
            //}
            //if (httpResult.Age.HasValue)
            //{
            //    httpResult.Headers["Age"] = httpResult.Age.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture);
            //}
            //if (!httpResult.Headers.ContainsKey("Cache-Control"))
            //{
            //    var cacheControl = BuildCacheControlHeader(cacheInfo1);
            //    if (cacheControl != null)
            //    {
            //        httpResult.Headers["Cache-Control"] = cacheControl;
            //    }
            //}
            //if (!req.CEFETagMatch(httpResult.ETag)
            //    && !req.CEFNotModifiedSince(httpResult.LastModified))
            //{
            //    return;
            //}
            //res.CEFEndNotModified();
            //httpResult.Dispose();
        }

        /// <summary>Builds cache control header.</summary>
        /// <param name="cacheInfo">Information describing the cache.</param>
        /// <returns>A string.</returns>
        public string? BuildCacheControlHeader(CacheInfo cacheInfo)
        {
            var maxAge = cacheInfo.MaxAge;
            if (!maxAge.HasValue && (cacheInfo.LastModified.HasValue || cacheInfo.ETag != null))
            {
                maxAge = DefaultMaxAge;
            }
            var stringList = new List<string>();
            if (maxAge.HasValue)
            {
                stringList.Add("max-age=" + maxAge.Value.TotalSeconds);
            }
            if (cacheInfo.CacheControl != CacheControl.None)
            {
                if (cacheInfo.CacheControl.CEFHas(CacheControl.Public))
                {
                    stringList.Add("public");
                }
                else if (cacheInfo.CacheControl.CEFHas(CacheControl.Private))
                {
                    stringList.Add("private");
                }
                if (cacheInfo.CacheControl.CEFHas(CacheControl.MustRevalidate))
                {
                    stringList.Add("must-revalidate");
                }
                if (cacheInfo.CacheControl.CEFHas(CacheControl.NoCache))
                {
                    stringList.Add("no-cache");
                }
                if (cacheInfo.CacheControl.CEFHas(CacheControl.NoStore))
                {
                    stringList.Add("no-store");
                }
                if (cacheInfo.CacheControl.CEFHas(CacheControl.NoTransform))
                {
                    stringList.Add("no-transform");
                }
                if (cacheInfo.CacheControl.CEFHas(CacheControl.ProxyRevalidate))
                {
                    stringList.Add("proxy-revalidate");
                }
            }
            if (stringList.Count <= 0)
            {
                return null;
            }
            var str = stringList.ToArray().Join(", ");
            return CacheControlFilter == null ? str : CacheControlFilter(str);
        }

        private bool CacheAndWriteResponse(
            CacheInfo cacheInfo,
            IRequest req,
            IResponse res,
            object response)
        {
            var obj = response is IHttpResult httpResult ? httpResult.Response : response;
            switch (obj)
            {
                case null:
#pragma warning disable CS0618
                case IPartialWriter _:
                case IStreamWriter _:
#pragma warning restore CS0618
                case IPartialWriterAsync _:
                case IStreamWriterAsync _:
                {
                    return false;
                }
                default:
                {
                    var expiresIn = cacheInfo.ExpiresIn.GetValueOrDefault(DefaultExpiresIn);
                    var cacheClient = cacheInfo.LocalCache
                        ? HostContext.AppHost.GetMemoryCacheClient(req)
                        : HostContext.AppHost.GetCacheClient(req);
                    var bytes = obj switch
                    {
                        string str2 => str2.ToUtf8Bytes(),
                        Stream input => input.ReadFully(),
                        _ => null,
                    };
                    var compressionType = !cacheInfo.NoCompression ? req.GetCompressionType() : null;
                    if (response is HttpResult httpResult2)
                    {
                        if (httpResult2.View != null)
                        {
                            req.Items["View"] = httpResult2.View;
                        }
                        if (httpResult2.Template != null)
                        {
                            req.Items["Template"] = httpResult2.Template;
                        }
                    }
                    var str1 = compressionType != null ? cacheInfo.CacheKey + "." + compressionType : null;
                    if (bytes != null || req.ResponseContentType.IsBinary())
                    {
                        bytes ??= HostContext.ContentTypes.SerializeToBytes(req, obj);
                        cacheClient.Set(cacheInfo.CacheKey, bytes, expiresIn);
                        if (compressionType != null)
                        {
                            res.AddHeader("Content-Encoding", compressionType);
                            bytes = bytes.CompressBytes(compressionType);
                            cacheClient.Set(str1, bytes, expiresIn);
                        }
                    }
                    else
                    {
                        var str2 = req.CEFSerializeToString(obj);
                        if (req.ResponseContentType.MatchesContentType("application/json"))
                        {
                            var jsonpCallback = req.GetJsonpCallback();
                            if (jsonpCallback != null)
                            {
                                str2 = jsonpCallback + "(" + str2 + ")";
                            }
                        }
                        bytes = str2.ToUtf8Bytes();
                        cacheClient.Set(cacheInfo.CacheKey, bytes, expiresIn);
                        if (compressionType != null)
                        {
                            res.AddHeader("Content-Encoding", compressionType);
                            bytes = bytes.CompressBytes(compressionType);
                            cacheClient.Set(str1, bytes, expiresIn);
                        }
                    }
                    if (cacheInfo.MaxAge.HasValue || (ulong)cacheInfo.CacheControl > 0UL)
                    {
                        var str2 = BuildCacheControlHeader(cacheInfo);
                        if (str2 != null)
                        {
                            var valueOrDefault2 = cacheInfo.LastModified.GetValueOrDefault(DateTime.UtcNow);
                            cacheClient.Set("date:" + cacheInfo.CacheKey, valueOrDefault2, expiresIn);
                            res.AddHeaderLastModified(valueOrDefault2);
                            if (!Contract.CheckValidKey(res.GetHeader("Cache-Control")))
                            {
                                res.AddHeader("Cache-Control", str2);
                            }
                            if (compressionType != null)
                            {
                                res.AddHeader("Vary", "Accept-Encoding");
                            }
                            if (cacheInfo.VaryByUser)
                            {
                                res.AddHeader("Vary", "Cookie");
                            }
                        }
                    }
                    if (response is IHttpResult httpResult3)
                    {
                        foreach (var header in httpResult3.Headers)
                        {
                            res.AddHeader(header.Key, header.Value);
                        }
                    }
#pragma warning disable CS0618 // Cannot Satisfy as there isn't an async response filter list available
                    res.WriteBytesToResponse(bytes, req.ResponseContentType);
#pragma warning restore CS0618
                    return true;
                }
            }
        }
    }
}
