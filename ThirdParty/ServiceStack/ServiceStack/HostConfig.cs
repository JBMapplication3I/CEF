using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using MarkdownSharp;
using ServiceStack.Host;
using ServiceStack.Logging;
using ServiceStack.Markdown;
using ServiceStack.Metadata;
using ServiceStack.Text;

namespace ServiceStack
{
    public class HostConfig
    {
        public const string DefaultWsdlNamespace = "http://schemas.servicestack.net/types";
        public static string ServiceStackPath = null;

        private static HostConfig instance;
        public static HostConfig Instance => instance ??= NewInstance();

        public static HostConfig ResetInstance()
        {
            return instance = NewInstance();
        }

        public static HostConfig NewInstance()
        {
            var config = new HostConfig
            {
                WsdlServiceNamespace = DefaultWsdlNamespace,
                ApiVersion = "1.0",
                EmbeddedResourceSources = new(),
                EmbeddedResourceBaseTypes = new[] { HostContext.AppHost.GetType(), typeof(Service) }.ToList(),
                EmbeddedResourceTreatAsFiles = new(),
                LogFactory = new NullLogFactory(),
                EnableAccessRestrictions = true,
                WebHostPhysicalPath = "~".MapServerPath(),
                HandlerFactoryPath = ServiceStackPath,
                MetadataRedirectPath = null,
                DefaultContentType = null,
                PreferredContentTypes = new()
                {
                    MimeTypes.Html, MimeTypes.Json, MimeTypes.Xml, MimeTypes.Jsv
                },
                AllowJsonpRequests = true,
                AllowRouteContentTypeExtensions = true,
                AllowNonHttpOnlyCookies = false,
                DebugMode = false,
                DefaultDocuments = new()
                {
                    "default.htm",
                    "default.html",
                    "default.cshtml",
                    "default.md",
                    "index.htm",
                    "index.html",
                    "default.aspx",
                    "default.ashx",
                },
                GlobalResponseHeaders = new()
                {
                    { "Vary", "Accept" },
                    { "X-Powered-By", Env.ServerUserAgent },
                },
                IgnoreFormatsInMetadata = new(StringComparer.OrdinalIgnoreCase)
                {
                },
                AllowFileExtensions = new(StringComparer.OrdinalIgnoreCase)
                {
                    "js", "ts", "tsx", "jsx", "css", "htm", "html", "shtm", "txt", "xml", "rss", "csv", "pdf",
                    "jpg", "jpeg", "gif", "png", "bmp", "ico", "tif", "tiff", "svg",
                    "avi", "divx", "m3u", "mov", "mp3", "mpeg", "mpg", "qt", "vob", "wav", "wma", "wmv",
                    "flv", "swf", "xap", "xaml", "ogg", "ogv", "mp4", "webm", "eot", "ttf", "woff", "woff2", "map"
                },
                CompressFilesWithExtensions = new(),
                AllowFilePaths = new()
                {
                    "jspm_packages/**/*.json"
                },
                ForbiddenPaths = new(),
                DebugAspNetHostEnvironment = Env.IsMono ? "FastCGI" : "IIS7",
                DebugHttpListenerHostEnvironment = Env.IsMono ? "XSP" : "WebServer20",
                EnableFeatures = Feature.All,
                WriteErrorsToResponse = true,
                ReturnsInnerException = true,
                DisposeDependenciesAfterUse = true,
                LogUnobservedTaskExceptions = true,
                MarkdownOptions = new(),
                MarkdownBaseType = typeof(MarkdownViewBase),
                MarkdownGlobalHelpers = new(),
                HtmlReplaceTokens = new(),
                AddMaxAgeForStaticMimeTypes = new()
                {
                    { "image/gif", TimeSpan.FromHours(1) },
                    { "image/png", TimeSpan.FromHours(1) },
                    { "image/jpeg", TimeSpan.FromHours(1) },
                },
                AppendUtf8CharsetOnContentTypes = new() { MimeTypes.Json, },
                RouteNamingConventions = new()
                {
                    RouteNamingConvention.WithRequestDtoName,
                    RouteNamingConvention.WithMatchingAttributes,
                    RouteNamingConvention.WithMatchingPropertyNames
                },
                MapExceptionToStatusCode = new(),
                OnlySendSessionCookiesSecurely = false,
                AllowSessionIdsInHttpParams = false,
                AllowSessionCookies = true,
                RestrictAllCookiesToDomain = null,
                DefaultJsonpCacheExpiration = new(0, 20, 0),
                MetadataVisibility = RequestAttributes.Any,
                Return204NoContentForEmptyResponse = true,
                AllowJsConfig = true,
                AllowPartialResponses = true,
                AllowAclUrlReservation = true,
                AddRedirectParamsToQueryString = false,
                RedirectToDefaultDocuments = false,
                RedirectDirectoriesToTrailingSlashes = true,
                StripApplicationVirtualPath = false,
                ScanSkipPaths = new()
                {
                    "obj/",
                    "bin/",
                    "node_modules/",
                    "jspm_packages/",
                    "bower_components/",
                    "wwwroot_build/",
#if !NETSTANDARD1_6 
                    "wwwroot/", //Need to allow VirtualFiles access from ContentRoot Folder
#endif
                },
                RedirectPaths = new()
                {
                    { "/metadata/", "/metadata" },
                },
                IgnoreWarningsOnPropertyNames = new()
                {
                    Keywords.Format, Keywords.Callback, Keywords.Debug, Keywords.AuthSecret, Keywords.JsConfig,
                    Keywords.IgnorePlaceHolder, Keywords.Version, Keywords.VersionAbbr, Keywords.Version.ToPascalCase(),
                    Keywords.ApiKeyParam, Keywords.Code,
                },
                XmlWriterSettings = new()
                {
                    Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
                },
                FallbackRestPath = null,
                UseHttpsLinks = false,
#if !NETSTANDARD1_6
                UseCamelCase = false,
                EnableOptimizations = false,
#else
                UseCamelCase = true,
                EnableOptimizations = true,
#endif
                DisableChunkedEncoding = false
            };

            Platform.Instance.InitHostConifg(config);

            return config;
        }

        public HostConfig()
        {
            if (instance == null)
            {
                return;
            }

            //Get a copy of the singleton already partially configured
            WsdlServiceNamespace = instance.WsdlServiceNamespace;
            ApiVersion = instance.ApiVersion;
            EmbeddedResourceSources = instance.EmbeddedResourceSources;
            EmbeddedResourceBaseTypes = instance.EmbeddedResourceBaseTypes;
            EmbeddedResourceTreatAsFiles = instance.EmbeddedResourceTreatAsFiles;
            EnableAccessRestrictions = instance.EnableAccessRestrictions;
            ServiceEndpointsMetadataConfig = instance.ServiceEndpointsMetadataConfig;
            SoapServiceName = instance.SoapServiceName;
            XmlWriterSettings = instance.XmlWriterSettings;
            LogFactory = instance.LogFactory;
            EnableAccessRestrictions = instance.EnableAccessRestrictions;
            WebHostUrl = instance.WebHostUrl;
            WebHostPhysicalPath = instance.WebHostPhysicalPath;
            DefaultRedirectPath = instance.DefaultRedirectPath;
            MetadataRedirectPath = instance.MetadataRedirectPath;
            HandlerFactoryPath = instance.HandlerFactoryPath;
            DefaultContentType = instance.DefaultContentType;
            PreferredContentTypes = instance.PreferredContentTypes;
            AllowJsonpRequests = instance.AllowJsonpRequests;
            AllowRouteContentTypeExtensions = instance.AllowRouteContentTypeExtensions;
            DebugMode = instance.DebugMode;
            StrictMode = instance.StrictMode;
            DefaultDocuments = instance.DefaultDocuments;
            GlobalResponseHeaders = instance.GlobalResponseHeaders;
            IgnoreFormatsInMetadata = instance.IgnoreFormatsInMetadata;
            AllowFileExtensions = instance.AllowFileExtensions;
            CompressFilesWithExtensions = instance.CompressFilesWithExtensions;
            CompressFilesLargerThanBytes = instance.CompressFilesLargerThanBytes;
            AllowFilePaths = instance.AllowFilePaths;
            ForbiddenPaths = instance.ForbiddenPaths;
            EnableFeatures = instance.EnableFeatures;
            WriteErrorsToResponse = instance.WriteErrorsToResponse;
            DisposeDependenciesAfterUse = instance.DisposeDependenciesAfterUse;
            LogUnobservedTaskExceptions = instance.LogUnobservedTaskExceptions;
            ReturnsInnerException = instance.ReturnsInnerException;
            MarkdownOptions = instance.MarkdownOptions;
            MarkdownBaseType = instance.MarkdownBaseType;
            MarkdownGlobalHelpers = instance.MarkdownGlobalHelpers;
            HtmlReplaceTokens = instance.HtmlReplaceTokens;
            AddMaxAgeForStaticMimeTypes = instance.AddMaxAgeForStaticMimeTypes;
            AppendUtf8CharsetOnContentTypes = instance.AppendUtf8CharsetOnContentTypes;
            RouteNamingConventions = instance.RouteNamingConventions;
            MapExceptionToStatusCode = instance.MapExceptionToStatusCode;
            OnlySendSessionCookiesSecurely = instance.OnlySendSessionCookiesSecurely;
            AllowSessionIdsInHttpParams = instance.AllowSessionIdsInHttpParams;
            AllowSessionCookies = instance.AllowSessionCookies;
            RestrictAllCookiesToDomain = instance.RestrictAllCookiesToDomain;
            DefaultJsonpCacheExpiration = instance.DefaultJsonpCacheExpiration;
            MetadataVisibility = instance.MetadataVisibility;
            Return204NoContentForEmptyResponse = instance.Return204NoContentForEmptyResponse;
            AllowNonHttpOnlyCookies = instance.AllowNonHttpOnlyCookies;
            AllowJsConfig = instance.AllowJsConfig;
            AllowPartialResponses = instance.AllowPartialResponses;
            IgnoreWarningsOnPropertyNames = instance.IgnoreWarningsOnPropertyNames;
            FallbackRestPath = instance.FallbackRestPath;
            AllowAclUrlReservation = instance.AllowAclUrlReservation;
            AddRedirectParamsToQueryString = instance.AddRedirectParamsToQueryString;
            RedirectToDefaultDocuments = instance.RedirectToDefaultDocuments;
            RedirectDirectoriesToTrailingSlashes = instance.RedirectDirectoriesToTrailingSlashes;
            StripApplicationVirtualPath = instance.StripApplicationVirtualPath;
            SkipFormDataInCreatingRequest = instance.SkipFormDataInCreatingRequest;
            ScanSkipPaths = instance.ScanSkipPaths;
            RedirectPaths = instance.RedirectPaths;
            AdminAuthSecret = instance.AdminAuthSecret;
            UseHttpsLinks = instance.UseHttpsLinks;
            UseCamelCase = instance.UseCamelCase;
            EnableOptimizations = instance.EnableOptimizations;
            DisableChunkedEncoding = instance.DisableChunkedEncoding;
        }

        public string WsdlServiceNamespace { get; set; }
        public string ApiVersion { get; set; }

        private RequestAttributes metadataVisibility;
        public RequestAttributes MetadataVisibility
        {
            get => metadataVisibility;
            set => metadataVisibility = value.ToAllowedFlagsSet();
        }

        public List<Type> EmbeddedResourceBaseTypes { get; set; }
        public List<Assembly> EmbeddedResourceSources { get; set; }
        public HashSet<string> EmbeddedResourceTreatAsFiles { get; set; }

        public string DefaultContentType { get; set; }
        public List<string> PreferredContentTypes { get; set; }
        internal string[] PreferredContentTypesArray = TypeConstants.EmptyStringArray; //use array at runtime
        public bool AllowJsonpRequests { get; set; }
        public bool AllowRouteContentTypeExtensions { get; set; }
        public bool DebugMode { get; set; }

        private bool? strictMode;
        public bool? StrictMode
        {
            get => strictMode;
            set => Env.StrictMode = (strictMode = value).GetValueOrDefault();
        }

        public string DebugAspNetHostEnvironment { get; set; }
        public string DebugHttpListenerHostEnvironment { get; set; }
        public List<string> DefaultDocuments { get; private set; }

        public List<string> IgnoreWarningsOnPropertyNames { get; private set; }

        public HashSet<string> IgnoreFormatsInMetadata { get; set; }

        public HashSet<string> AllowFileExtensions { get; set; }
        public HashSet<string> CompressFilesWithExtensions { get; set; }
        public long? CompressFilesLargerThanBytes { get; set; }
        public List<string> ForbiddenPaths { get; set; }
        public List<string> AllowFilePaths { get; set; }

        public string WebHostUrl { get; set; }
        public string WebHostPhysicalPath { get; set; }
        public string HandlerFactoryPath { get; set; }
        public string DefaultRedirectPath { get; set; }
        public string MetadataRedirectPath { get; set; }

        public ServiceEndpointsMetadataConfig ServiceEndpointsMetadataConfig { get; set; }
        public string SoapServiceName { get; set; }
        public XmlWriterSettings XmlWriterSettings { get; set; }
        public ILogFactory LogFactory { get; set; }
        public bool EnableAccessRestrictions { get; set; }
        public bool UseBclJsonSerializers { get; set; }
        public Dictionary<string, string> GlobalResponseHeaders { get; set; }
        public Feature EnableFeatures { get; set; }
        public bool ReturnsInnerException { get; set; }
        public bool WriteErrorsToResponse { get; set; }
        public bool DisposeDependenciesAfterUse { get; set; }
        public bool LogUnobservedTaskExceptions { get; set; }

        public MarkdownOptions MarkdownOptions { get; set; }
        public Type MarkdownBaseType { get; set; }
        public Dictionary<string, Type> MarkdownGlobalHelpers { get; set; }
        public Dictionary<string, string> HtmlReplaceTokens { get; set; }

        public HashSet<string> AppendUtf8CharsetOnContentTypes { get; set; }

        public Dictionary<string, TimeSpan> AddMaxAgeForStaticMimeTypes { get; set; }

        public List<RouteNamingConventionDelegate> RouteNamingConventions { get; set; }

        public Dictionary<Type, int> MapExceptionToStatusCode { get; set; }

        public bool OnlySendSessionCookiesSecurely { get; set; }
        public bool AllowSessionIdsInHttpParams { get; set; }
        public bool AllowSessionCookies { get; set; }
        public string RestrictAllCookiesToDomain { get; set; }

        public TimeSpan DefaultJsonpCacheExpiration { get; set; }
        public bool Return204NoContentForEmptyResponse { get; set; }
        public bool AllowJsConfig { get; set; }
        public bool AllowPartialResponses { get; set; }
        public bool AllowNonHttpOnlyCookies { get; set; }
        public bool AllowAclUrlReservation { get; set; }
        public bool AddRedirectParamsToQueryString { get; set; }
        public bool RedirectToDefaultDocuments { get; set; }
        public bool StripApplicationVirtualPath { get; set; }
        public bool SkipFormDataInCreatingRequest { get; set; }
        public bool RedirectDirectoriesToTrailingSlashes { get; set; }

        //Skip scanning common VS.NET extensions
        public List<string> ScanSkipPaths { get; private set; }

        public Dictionary<string, string> RedirectPaths { get; private set; }

        public bool UseHttpsLinks { get; set; }

        public bool UseCamelCase { get; set; }
        public bool EnableOptimizations { get; set; }

        //Disables chunked encoding on Kestrel Server
        public bool DisableChunkedEncoding { get; set; }

        public string AdminAuthSecret { get; set; }

        public FallbackRestPathDelegate FallbackRestPath { get; set; }

        private HashSet<string> razorNamespaces;
        public HashSet<string> RazorNamespaces => razorNamespaces ??= Platform.Instance.GetRazorNamespaces();

    }

}
