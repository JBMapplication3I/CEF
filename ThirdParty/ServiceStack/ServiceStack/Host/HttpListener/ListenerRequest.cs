#if !NETSTANDARD1_6 

//Copyright (c) ServiceStack, Inc. All Rights Reserved.
//License: https://raw.github.com/ServiceStack/ServiceStack/master/license.txt

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Funq;
using ServiceStack.Configuration;
using ServiceStack.IO;
using ServiceStack.Web;

namespace ServiceStack.Host.HttpListener
{
    public partial class ListenerRequest : IHttpRequest, IHasResolver, IHasVirtualFiles
    {
        [Obsolete("Use Resolver")]
        public Container Container => throw new NotSupportedException("Use Resolver");

        private IResolver resolver;
        public IResolver Resolver
        {
            get => resolver ?? Service.GlobalResolver;
            set => resolver = value;
        }

        private readonly HttpListenerRequest request;
        private readonly IHttpResponse response;

        public ListenerRequest(HttpListenerContext httpContext, string operationName, RequestAttributes requestAttributes)
        {
            OperationName = operationName;
            RequestAttributes = requestAttributes;
            request = httpContext.Request;
            response = new ListenerResponse(httpContext.Response, this);

            RequestPreferences = new RequestPreferences(this);
            PathInfo = OriginalPathInfo = GetPathInfo();
            PathInfo = HostContext.AppHost.ResolvePathInfo(this, OriginalPathInfo, out var isDirectory);
            IsDirectory = isDirectory;
            IsFile = !isDirectory && HostContext.VirtualFileSources.FileExists(PathInfo);
        }

        private string GetPathInfo()
        {
            var mode = HostContext.Config.HandlerFactoryPath;

            string pathInfo;
            var pos = request.RawUrl.IndexOf("?", StringComparison.Ordinal);
            if (pos != -1)
            {
                var path = request.RawUrl.Substring(0, pos);
                pathInfo = HttpRequestExtensions.GetPathInfo(
                    path,
                    mode,
                    mode ?? "");
            }
            else
            {
                pathInfo = request.RawUrl;
            }

            pathInfo = pathInfo.UrlDecode();
            return pathInfo;
        }

        public HttpListenerRequest HttpRequest => request;

        public object OriginalRequest => request;

        public IResponse Response => response;

        public IHttpResponse HttpResponse => response;

        public RequestAttributes RequestAttributes { get; set; }

        public IRequestPreferences RequestPreferences { get; private set; }

        public T TryResolve<T>()
        {
            return this.TryResolveInternal<T>();
        }

        public string OperationName { get; set; }

        public object Dto { get; set; }

        public string GetRawBody()
        {
            if (BufferedStream != null)
            {
                return BufferedStream.ToArray().FromUtf8Bytes();
            }

            var reader = new StreamReader(InputStream);
            return reader.ReadToEnd();
        }

        public string RawUrl => request.RawUrl;

        public string AbsoluteUri => request.Url.AbsoluteUri.TrimEnd('/');

        public string UserHostAddress => request.RemoteEndPoint?.Address.ToString() ?? request.UserHostAddress;

        public string XForwardedFor => string.IsNullOrEmpty(request.Headers[HttpHeaders.XForwardedFor]) ? null : request.Headers[HttpHeaders.XForwardedFor];

        public int? XForwardedPort => string.IsNullOrEmpty(request.Headers[HttpHeaders.XForwardedPort]) ? (int?)null : int.Parse(request.Headers[HttpHeaders.XForwardedPort]);

        public string XForwardedProtocol => string.IsNullOrEmpty(request.Headers[HttpHeaders.XForwardedProtocol]) ? null : request.Headers[HttpHeaders.XForwardedProtocol];

        public string XRealIp => string.IsNullOrEmpty(request.Headers[HttpHeaders.XRealIp]) ? null : request.Headers[HttpHeaders.XRealIp];

        public string Accept => string.IsNullOrEmpty(request.Headers[HttpHeaders.Accept]) ? null : request.Headers[HttpHeaders.Accept];

        private string remoteIp;
        public string RemoteIp => remoteIp ??= XForwardedFor ??
                                              (XRealIp ?? request.RemoteEndPoint?.Address.ToString());

        public string Authorization => string.IsNullOrEmpty(request.Headers[HttpHeaders.Authorization]) ? null : request.Headers[HttpHeaders.Authorization];

        public bool IsSecureConnection => request.IsSecureConnection || XForwardedProtocol == "https";

        public string[] AcceptTypes => request.AcceptTypes;

        private Dictionary<string, object> items;
        public Dictionary<string, object> Items => items ??= new();

        private string responseContentType;
        public string ResponseContentType
        {
            get => responseContentType ??= this.GetResponseContentType();
            set
            {
                responseContentType = value;
                HasExplicitResponseContentType = true;
            }
        }

        public bool HasExplicitResponseContentType { get; private set; }

        public string PathInfo { get; }

        public string OriginalPathInfo { get; }

        private Dictionary<string, Cookie> cookies;
        public IDictionary<string, Cookie> Cookies
        {
            get
            {
                if (cookies == null)
                {
                    cookies = new();
                    for (var i = 0; i < request.Cookies.Count; i++)
                    {
                        var httpCookie = request.Cookies[i];
                        cookies[httpCookie.Name] = httpCookie;
                    }
                }

                return cookies;
            }
        }

        public string UserAgent => request.UserAgent;

        private NameValueCollectionWrapper headers;
        public INameValueCollection Headers => headers ??= new(request.Headers);

        private NameValueCollectionWrapper queryString;
        public INameValueCollection QueryString => queryString ??= new(HttpUtility.ParseQueryString(request.Url.Query));

        private NameValueCollectionWrapper formData;
        public INameValueCollection FormData => formData ??= new(Form);

        public bool IsLocal => request.IsLocal;

        private string httpMethod;
        public string HttpMethod => httpMethod ??= this.GetParamInRequestHeader(HttpHeaders.XHttpMethodOverride)
            ?? request.HttpMethod;

        public string Verb => HttpMethod;

        public string Param(string name)
        {
            return Headers[name]
                ?? QueryString[name]
                ?? FormData[name];
        }

        public string ContentType => request.ContentType;

        public Encoding contentEncoding;
        public Encoding ContentEncoding
        {
            get => contentEncoding ?? request.ContentEncoding;
            set => contentEncoding = value;
        }

        public Uri UrlReferrer => request.UrlReferrer;

        public static Encoding GetEncoding(string contentTypeHeader)
        {
            var param = GetParameter(contentTypeHeader, "charset=");
            if (param == null)
            {
                return null;
            }

            try
            {
                return Encoding.GetEncoding(param);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public bool UseBufferedStream
        {
            get => BufferedStream != null;
            set => BufferedStream = value
                    ? BufferedStream ?? new MemoryStream(request.InputStream.ReadFully())
                    : null;
        }

        public MemoryStream BufferedStream { get; set; }
        public Stream InputStream => this.GetInputStream(BufferedStream ?? request.InputStream);

        public long ContentLength => request.ContentLength64;

        private IHttpFile[] httpFiles;
        public IHttpFile[] Files
        {
            get
            {
                if (httpFiles == null)
                {
                    if (files == null)
                    {
                        return httpFiles = TypeConstants<IHttpFile>.EmptyArray;
                    }

                    httpFiles = new IHttpFile[files.Count];
                    for (var i = 0; i < files.Count; i++)
                    {
                        var reqFile = files[i];
                        httpFiles[i] = new HttpFile
                        {
                            Name = files.AllKeys[i],
                            ContentType = reqFile.ContentType,
                            ContentLength = reqFile.ContentLength,
                            FileName = reqFile.FileName,
                            InputStream = reqFile.InputStream,
                        };
                    }
                }
                return httpFiles;
            }
        }

        private static Stream GetSubStream(Stream stream)
        {
            if (stream is MemoryStream other)
            {
                try
                {
                    return new MemoryStream(other.GetBuffer(), 0, (int)other.Length, false, true);
                }
                catch (UnauthorizedAccessException)
                {
                    return new MemoryStream(other.ToArray(), 0, (int)other.Length, false, true);
                }
            }

            return stream;
        }

        private static void EndSubStream(Stream stream)
        {
        }

        public static string GetHandlerPathIfAny(string listenerUrl)
        {
            if (listenerUrl == null)
            {
                return null;
            }

            var pos = listenerUrl.IndexOf("://", StringComparison.OrdinalIgnoreCase);
            if (pos == -1)
            {
                return null;
            }

            var startHostUrl = listenerUrl.Substring(pos + "://".Length);
            var endPos = startHostUrl.IndexOf('/');
            if (endPos == -1)
            {
                return null;
            }

            var endHostUrl = startHostUrl.Substring(endPos + 1);
            return string.IsNullOrEmpty(endHostUrl) ? null : endHostUrl.TrimEnd('/');
        }

        public static string NormalizePathInfo(string pathInfo, string handlerPath)
        {
            if (handlerPath != null && pathInfo.TrimStart('/').StartsWith(
                handlerPath, StringComparison.OrdinalIgnoreCase))
            {
                return pathInfo.TrimStart('/').Substring(handlerPath.Length);
            }

            return pathInfo;
        }

        public IVirtualFile GetFile() => HostContext.VirtualFileSources.GetFile(PathInfo);

        public IVirtualDirectory GetDirectory() => HostContext.VirtualFileSources.GetDirectory(PathInfo);

        public bool IsDirectory { get; }

        public bool IsFile { get; }
    }

}

#endif