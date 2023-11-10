#if !NETSTANDARD1_6

// Copyright (c) ServiceStack, Inc. All Rights Reserved.
// License: https://raw.github.com/ServiceStack/ServiceStack/master/license.txt

namespace ServiceStack.Host.AspNet
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Web;
    using Configuration;
    using Funq;
    using IO;
    using Logging;
    using Text;
    using Web;

    public class AspNetRequest
        : IHttpRequest, IHasResolver, IHasVirtualFiles
    {
        private Dictionary<string, Cookie> cookies;
        private Dictionary<string, object> items;
        private IHttpFile[] httpFiles;
        private IResolver resolver;
        private NameValueCollectionWrapper formData;
        private NameValueCollectionWrapper headers;
        private NameValueCollectionWrapper queryString;
        private string httpMethod;
        private string remoteIp;
        private string responseContentType;

        public static ILog log = LogManager.GetLogger(typeof(AspNetRequest));

        [Obsolete("Use Resolver")]
        public Container Container => throw new NotSupportedException("Use Resolver");

        public IResolver Resolver
        {
            get => resolver ?? Service.GlobalResolver;
            set => resolver = value;
        }

        public AspNetRequest(HttpContextBase httpContext, string operationName = null)
            : this(httpContext, operationName, RequestAttributes.None)
        {
            RequestAttributes = this.GetAttributes();
        }

        public AspNetRequest(HttpContextBase httpContext, string operationName, RequestAttributes requestAttributes)
        {
            OperationName = operationName;
            RequestAttributes = requestAttributes;
            HttpRequest = httpContext.Request;
            try
            {
                HttpResponse = new AspNetResponse(httpContext.Response, this);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            RequestPreferences = new RequestPreferences(httpContext);
            if (httpContext.Items?.Count > 0)
            {
                foreach (var key in httpContext.Items.Keys)
                {
                    if (key is not string strKey)
                    {
                        continue;
                    }
                    Items[strKey] = httpContext.Items[key];
                }
            }
            PathInfo = OriginalPathInfo = GetPathInfo();
            PathInfo = HostContext.AppHost.ResolvePathInfo(this, OriginalPathInfo, out var isDirectory);
            IsDirectory = isDirectory;
            IsFile = !isDirectory && HostContext.VirtualFileSources.FileExists(PathInfo);
        }

        public HttpRequestBase HttpRequest { get; }

        public object OriginalRequest => HttpRequest;

        public IResponse Response => HttpResponse;

        public IHttpResponse HttpResponse { get; }

        public RequestAttributes RequestAttributes { get; set; }

        public IRequestPreferences RequestPreferences { get; }

        public string OperationName { get; set; }

        public object Dto { get; set; }

        public string ContentType => HttpRequest.ContentType;

        public string HttpMethod => httpMethod ??= this.GetParamInRequestHeader(HttpHeaders.XHttpMethodOverride)
            ?? HttpRequest.HttpMethod;

        public string Verb => HttpMethod;

        public bool IsLocal => HttpRequest.IsLocal;

        public string UserAgent => HttpRequest.UserAgent;

        public Dictionary<string, object> Items
        {
            get
            {
                items ??= new();
                return items;
            }
        }

        public string ResponseContentType
        {
            get
            {
                responseContentType ??= this.GetResponseContentType();
                return responseContentType;
            }
            set
            {
                responseContentType = value;
                HasExplicitResponseContentType = true;
            }
        }

        public bool HasExplicitResponseContentType { get; private set; }

        public IDictionary<string, Cookie> Cookies
        {
            get
            {
                if (cookies != null)
                {
                    return cookies;
                }
                cookies = new();
                for (var i = 0; i < HttpRequest.Cookies.Count; i++)
                {
                    var httpCookie = HttpRequest.Cookies[i];
                    if (httpCookie == null)
                    {
                        continue;
                    }
                    Cookie cookie = null;
                    // try-catch needed as malformed cookie names (e.g. '$Version') can be returned
                    // from Cookie.Name, but the Cookie constructor will throw for these names.
                    try
                    {
                        cookie = new(httpCookie.Name, httpCookie.Value, httpCookie.Path, httpCookie.Domain)
                        {
                            HttpOnly = httpCookie.HttpOnly,
                            Secure = httpCookie.Secure,
                            Expires = httpCookie.Expires,
                        };
                    }
                    catch (Exception ex)
                    {
                        log.Warn("Error trying to create System.Net.Cookie: " + httpCookie.Name, ex);
                    }
                    if (cookie != null)
                    {
                        cookies[httpCookie.Name] = cookie;
                    }
                }
                return cookies;
            }
        }

        public INameValueCollection Headers => headers ??= new(HttpRequest.Headers);

        public INameValueCollection QueryString => queryString ??= new(HttpRequest.QueryString);

        public INameValueCollection FormData => formData ??= new(HttpRequest.Form);

        public string RawUrl => HttpRequest.RawUrl;

        public string AbsoluteUri
        {
            get
            {
                try
                {
                    return HostContext.Config.StripApplicationVirtualPath
                        ? HttpRequest.Url.GetLeftAuthority()
                            .CombineWith(HostContext.Config.HandlerFactoryPath)
                            .CombineWith(PathInfo)
                            .TrimEnd('/')
                        : HttpRequest.Url.AbsoluteUri.TrimEnd('/');
                }
                catch (Exception)
                {
                    //fastcgi mono, do a 2nd rounds best efforts
                    return "http://" + HttpRequest.UserHostName + HttpRequest.RawUrl;
                }
            }
        }

        public string UserHostAddress
        {
            get
            {
                try
                {
                    return HttpRequest.UserHostAddress;
                }
                catch (Exception)
                {
                    return null; //Can throw in Mono FastCGI Host
                }
            }
        }

        public string XForwardedFor =>
            string.IsNullOrEmpty(HttpRequest.Headers[HttpHeaders.XForwardedFor]) ? null : HttpRequest.Headers[HttpHeaders.XForwardedFor];

        public int? XForwardedPort =>
            string.IsNullOrEmpty(HttpRequest.Headers[HttpHeaders.XForwardedPort]) ? (int?)null : int.Parse(HttpRequest.Headers[HttpHeaders.XForwardedPort]);

        public string XForwardedProtocol =>
            string.IsNullOrEmpty(HttpRequest.Headers[HttpHeaders.XForwardedProtocol]) ? null : HttpRequest.Headers[HttpHeaders.XForwardedProtocol];

        public string XRealIp =>
            string.IsNullOrEmpty(HttpRequest.Headers[HttpHeaders.XRealIp]) ? null : HttpRequest.Headers[HttpHeaders.XRealIp];

        public string Accept =>
            string.IsNullOrEmpty(HttpRequest.Headers[HttpHeaders.Accept]) ? null : HttpRequest.Headers[HttpHeaders.Accept];

        public string RemoteIp =>
            remoteIp ??= XForwardedFor ?? (XRealIp ?? HttpRequest.UserHostAddress);

        public string Authorization =>
            string.IsNullOrEmpty(HttpRequest.Headers[HttpHeaders.Authorization]) ? null : HttpRequest.Headers[HttpHeaders.Authorization];

        public bool IsSecureConnection =>
            HttpRequest.IsSecureConnection || XForwardedProtocol == "https";

        public string[] AcceptTypes => HttpRequest.AcceptTypes;

        public string PathInfo { get; }

        public string OriginalPathInfo { get; }

        public string UrlHostName => HttpRequest.GetUrlHostName();

        public bool UseBufferedStream
        {
            get => BufferedStream != null;
            set => BufferedStream = value
                ? BufferedStream ?? new MemoryStream(HttpRequest.InputStream.ReadFully())
                : null;
        }

        public MemoryStream BufferedStream { get; set; }

        public Stream InputStream => this.GetInputStream(BufferedStream ?? HttpRequest.InputStream);

        public long ContentLength => HttpRequest.ContentLength;

        public IHttpFile[] Files
        {
            get
            {
                if (httpFiles != null)
                {
                    return httpFiles;
                }
                httpFiles = new IHttpFile[HttpRequest.Files.Count];
                for (var i = 0; i < HttpRequest.Files.Count; i++)
                {
                    var reqFile = HttpRequest.Files[i];
                    httpFiles[i] = new HttpFile
                    {
                        Name = HttpRequest.Files.AllKeys[i],
                        ContentType = reqFile.ContentType,
                        ContentLength = reqFile.ContentLength,
                        FileName = reqFile.FileName,
                        InputStream = reqFile.InputStream,
                    };
                }
                return httpFiles;
            }
        }

        public Uri UrlReferrer => HttpRequest.UrlReferrer;

        public bool IsDirectory { get; }

        public bool IsFile { get; }

        public T TryResolve<T>()
        {
            return this.TryResolveInternal<T>();
        }

        public string Param(string name)
        {
            return Headers[name]
                ?? QueryString[name]
                ?? FormData[name];
        }

        public string GetRawBody()
        {
            if (BufferedStream != null)
            {
                return BufferedStream.ToArray().FromUtf8Bytes();
            }
            using var reader = new StreamReader(InputStream);
            return reader.ReadToEnd();
        }

        public string GetPathInfo()
        {
            if (!string.IsNullOrEmpty(HttpRequest.PathInfo))
            {
                return HttpRequest.PathInfo;
            }
            var mode = HostContext.Config.HandlerFactoryPath;
            var appPath = string.IsNullOrEmpty(HttpRequest.ApplicationPath)
                ? HttpRequestExtensions.WebHostDirectoryName
                : HttpRequest.ApplicationPath.TrimStart('/');
            // mod_mono: /CustomPath35/api//default.htm
            var path = Env.IsMono ? HttpRequest.Path.Replace("//", "/") : HttpRequest.Path;
            return HttpRequestExtensions.GetPathInfo(path, mode, appPath);
        }

        public IVirtualFile GetFile()
        {
            return HostContext.VirtualFileSources.GetFile(PathInfo);
        }

        public IVirtualDirectory GetDirectory()
        {
            return HostContext.VirtualFileSources.GetDirectory(PathInfo);
        }
    }
}
#endif
