namespace ServiceStack
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Web;
    using Caching;
    using Configuration;
    using Funq;
    using Host;
    using IO;
    using Metadata;
    using MiniProfiler;
    using Web;

    public static class HostContext
    {
        public static RequestContext RequestContext => RequestContext.Instance;

        public static ServiceStackHost AppHost => ServiceStackHost.Instance;

        public static AsyncContext Async = new();

        internal static ServiceStackHost AssertAppHost()
        {
            if (ServiceStackHost.Instance == null)
            {
                throw new ConfigurationErrorsException(
                    "ServiceStack: AppHost does not exist or has not been initialized."
                    + " Make sure you have created an AppHost and started it with 'new AppHost().Init();'"
                    + " in your Global.asax Application_Start() or alternative Application StartUp");
            }
            return ServiceStackHost.Instance;
        }

#if !NETSTANDARD1_6
        public static bool IsAspNetHost => ServiceStackHost.Instance is AppHostBase;
        public static bool IsHttpListenerHost => ServiceStackHost.Instance is Host.HttpListener.HttpListenerBase;
        public static bool IsNetCore => false;
#else
        public static bool IsAspNetHost => false;
        public static bool IsHttpListenerHost => false;
        public static bool IsNetCore => true;
#endif
        public static T TryResolve<T>() => AssertAppHost().TryResolve<T>();

        public static T Resolve<T>() => AssertAppHost().Resolve<T>();

        public static Container Container => AssertAppHost().Container;

        public static ServiceController ServiceController => AssertAppHost().ServiceController;

        public static MetadataPagesConfig MetadataPagesConfig => AssertAppHost().MetadataPagesConfig;

        public static IContentTypes ContentTypes => AssertAppHost().ContentTypes;

        public static HostConfig Config => AssertAppHost().Config;

        public static IAppSettings AppSettings => AssertAppHost().AppSettings;

        public static ServiceMetadata Metadata => AssertAppHost().Metadata;

        public static string ServiceName => AssertAppHost().ServiceName;

        public static bool DebugMode => AppHost?.Config?.DebugMode == true;

        public static bool StrictMode => AppHost?.Config?.StrictMode == true;

        public static bool TestMode
        {
            get => ServiceStackHost.Instance is { TestMode: true };
            set => ServiceStackHost.Instance.TestMode = value;
        }

        public static List<HttpHandlerResolverDelegate> CatchAllHandlers => AssertAppHost().CatchAllHandlers;

        public static List<Func<IHttpRequest, IHttpHandler>> RawHttpHandlers => AssertAppHost().RawHttpHandlers;

        public static List<Action<IRequest, IResponse, object>> GlobalRequestFilters => AssertAppHost().GlobalRequestFilters;

        public static List<Action<IRequest, IResponse, object>> GlobalResponseFilters => AssertAppHost().GlobalResponseFilters;

        public static List<Action<IRequest, IResponse, object>> GlobalMessageRequestFilters => AssertAppHost().GlobalMessageRequestFilters;

        public static List<Action<IRequest, IResponse, object>> GlobalMessageResponseFilters => AssertAppHost().GlobalMessageResponseFilters;

        public static bool ApplyCustomHandlerRequestFilters(IRequest httpReq, IResponse httpRes)
        {
            return AssertAppHost().ApplyCustomHandlerRequestFilters(httpReq, httpRes);
        }

        public static bool ApplyPreRequestFilters(IRequest httpReq, IResponse httpRes)
        {
            return AssertAppHost().ApplyPreRequestFilters(httpReq, httpRes);
        }

        public static bool ApplyRequestFilters(IRequest httpReq, IResponse httpRes, object requestDto)
        {
            return AssertAppHost().ApplyRequestFilters(httpReq, httpRes, requestDto);
        }

        public static bool ApplyResponseFilters(IRequest httpReq, IResponse httpRes, object response)
        {
            return AssertAppHost().ApplyResponseFilters(httpReq, httpRes, response);
        }

        /// <summary>Read/Write Virtual FileSystem. Defaults to FileSystemVirtualPathProvider.</summary>
        /// <value>The virtual files.</value>
        public static IVirtualFiles VirtualFiles => AssertAppHost().VirtualFiles;

        /// <summary>Cascading collection of virtual file sources, inc. Embedded Resources, File System, In Memory, S3.</summary>
        /// <value>The virtual file sources.</value>
        public static IVirtualPathProvider VirtualFileSources => AssertAppHost().VirtualFileSources;

        [Obsolete("Renamed to VirtualFileSources")]
        public static IVirtualPathProvider VirtualPathProvider => AssertAppHost().VirtualFileSources;

        public static ICacheClient Cache => TryResolve<ICacheClient>();

        public static MemoryCacheClient LocalCache => TryResolve<MemoryCacheClient>();

        /// <summary>Call to signal the completion of a ServiceStack-handled Request.</summary>
        /// <param name="request">The request.</param>
        internal static void CompleteRequest(IRequest request)
        {
            try
            {
                AssertAppHost().OnEndRequest(request);
            }
            catch (Exception)
            {
                // Do Nothing
            }
        }

        public static IServiceRunner<TRequest> CreateServiceRunner<TRequest>(ActionContext actionContext)
        {
            return AssertAppHost().CreateServiceRunner<TRequest>(actionContext);
        }

        internal static object ExecuteService(object request, IRequest httpReq)
        {
            using (Profiler.Current.Step("Execute Service"))
            {
                return AssertAppHost().ServiceController.Execute(request, httpReq);
            }
        }

        public static T AssertPlugin<T>()
            where T : class, IPlugin
        {
            var appHost = AppHost;
            var plugin = appHost.GetPlugin<T>();
            if (plugin == null)
            {
                throw new NotImplementedException($"Plugin '{typeof(T).Name}' has not been registered.");
            }
            return plugin;
        }

        public static T GetPlugin<T>()
            where T : class, IPlugin
        {
            var appHost = AppHost;
            return appHost?.GetPlugin<T>();
        }

        public static bool HasPlugin<T>()
            where T : class, IPlugin
        {
            var appHost = AppHost;
            return appHost?.HasPlugin<T>() == true;
        }

        public static void Release(object service)
        {
            if (ServiceStackHost.Instance != null)
            {
                ServiceStackHost.Instance.Release(service);
            }
            else
            {
                using (service as IDisposable) { }
            }
        }

        public static UnauthorizedAccessException UnauthorizedAccess(RequestAttributes requestAttrs)
        {
            return new($"Request with '{requestAttrs}' is not allowed");
        }

        public static string ResolveLocalizedString(string text, IRequest request = null)
        {
            return AssertAppHost().ResolveLocalizedString(text, request);
        }

        public static string ResolveAbsoluteUrl(string virtualPath, IRequest httpReq)
        {
            return AssertAppHost().ResolveAbsoluteUrl(virtualPath, httpReq);
        }

        public static string ResolvePhysicalPath(string virtualPath, IRequest httpReq)
        {
            return AssertAppHost().ResolvePhysicalPath(virtualPath, httpReq);
        }

        private static string defaultOperationNamespace;
        public static string DefaultOperationNamespace
        {
            get => defaultOperationNamespace ??= GetDefaultNamespace();
            set => defaultOperationNamespace = value;
        }

        public static string GetDefaultNamespace()
        {
            if (!string.IsNullOrEmpty(defaultOperationNamespace))
            {
                return null;
            }
            foreach (var operationType in Metadata.RequestTypes)
            {
                var attrs = operationType.AllAttributes<DataContractAttribute>();
                if (attrs.Length <= 0)
                {
                    continue;
                }
                var attr = attrs[0];
                if (string.IsNullOrEmpty(attr.Namespace))
                {
                    continue;
                }
                return attr.Namespace;
            }
            return null;
        }

        public static object RaiseServiceException(IRequest httpReq, object request, Exception ex)
        {
            return AssertAppHost().OnServiceException(httpReq, request, ex);
        }

        public static void RaiseUncaughtException(IRequest httpReq, IResponse httpRes, string operationName, Exception ex)
        {
            AssertAppHost().OnUncaughtException(httpReq, httpRes, operationName, ex);
        }

        public static void RaiseAndHandleUncaughtException(IRequest httpReq, IResponse httpRes, string operationName, Exception ex)
        {
            AssertAppHost().OnUncaughtException(httpReq, httpRes, operationName, ex);
            if (httpRes.IsClosed)
            {
                return;
            }
            AssertAppHost().HandleUncaughtException(httpReq, httpRes, operationName, ex);
        }

#if !NETSTANDARD1_6
        /// <summary>Resolves and auto-wires a ServiceStack Service from a ASP.NET HttpContext.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="httpCtx">Context for the HTTP.</param>
        /// <returns>A T.</returns>
        public static T ResolveService<T>(HttpContextBase httpCtx = null)
            where T : class, IRequiresRequest
        {
            var httpReq = httpCtx != null ? httpCtx.ToRequest() : GetCurrentRequest();
            return ResolveService(httpReq, AssertAppHost().Container.Resolve<T>());
        }

        /// <summary>Resolves and auto-wires a ServiceStack Service from a HttpListenerContext.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="httpCtx">Context for the HTTP.</param>
        /// <returns>A T.</returns>
        public static T ResolveService<T>(HttpListenerContext httpCtx)
            where T : class, IRequiresRequest
        {
            return ResolveService(httpCtx.ToRequest(), AssertAppHost().Container.Resolve<T>());
        }
#endif

        /// <summary>Resolves and auto-wires a ServiceStack Service.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="httpReq">The HTTP request.</param>
        /// <returns>A T.</returns>
        public static T ResolveService<T>(IRequest httpReq) where T : class, IRequiresRequest
        {
            return ResolveService(httpReq, AssertAppHost().Container.Resolve<T>());
        }

        public static T ResolveService<T>(IRequest httpReq, T service)
        {
            if (service is IRequiresRequest hasRequest)
            {
                httpReq.SetInProcessRequest();
                hasRequest.Request = httpReq;
            }
            return service;
        }

        public static bool HasValidAuthSecret(IRequest httpReq)
        {
            return AssertAppHost().HasValidAuthSecret(httpReq);
        }

        public static bool HasFeature(Feature feature)
        {
            return AssertAppHost().HasFeature(feature);
        }

        public static IRequest GetCurrentRequest()
        {
            var req = AssertAppHost().TryGetCurrentRequest();
            if (req == null)
            {
                throw new NotImplementedException(ErrorMessages.HostDoesNotSupportSingletonRequest);
            }
            return req;
        }

        public static IRequest TryGetCurrentRequest()
        {
            return AssertAppHost().TryGetCurrentRequest();
        }

        public static int FindFreeTcpPort(int startingFrom = 5000, int endingAt = 65535)
        {
            var tcpEndPoints = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            var activePorts = new HashSet<int>();
            foreach (var endPoint in tcpEndPoints)
            {
                activePorts.Add(endPoint.Port);
            }
            for (var port = startingFrom; port < endingAt; port++)
            {
                if (!activePorts.Contains(port))
                {
                    return port;
                }
            }
            return -1;
        }
    }
}
