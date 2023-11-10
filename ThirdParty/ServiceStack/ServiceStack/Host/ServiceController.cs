namespace ServiceStack.Host
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Auth;
    using Configuration;
    using Funq;
    using Logging;
    using Messaging;
    using Serialization;
    using Text;
    using Web;

    public delegate object ServiceExecFn(IRequest requestContext, object request);
    public delegate object InstanceExecFn(IRequest requestContext, object instance, object request);
    public delegate object ActionInvokerFn(object instance, object request);
    public delegate void VoidActionInvokerFn(object instance, object request);

    public class ServiceController : IServiceController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ServiceController));
        private const string ResponseDtoSuffix = "Response";
        private readonly ServiceStackHost appHost;

        public ServiceController(ServiceStackHost appHost)
        {
            this.appHost = appHost;
            appHost.Container.DefaultOwner = Owner.External;
            RequestTypeFactoryMap = new();
        }

        public ServiceController(ServiceStackHost appHost, Func<IEnumerable<Type>> resolveServicesFn)
            : this(appHost)
        {
            ResolveServicesFn = resolveServicesFn;
        }

        public ServiceController(ServiceStackHost appHost, params Assembly[] assembliesWithServices)
            : this(appHost)
        {
            if (assembliesWithServices == null || assembliesWithServices.Length == 0)
            {
                throw new ArgumentException(
                    "No Assemblies provided in your AppHost's base constructor.\n"
                    + "To register your services, please provide the assemblies where your web services are defined.");
            }
            ResolveServicesFn = () => GetAssemblyTypes(assembliesWithServices);
        }

        private readonly Dictionary<Type, ServiceExecFn> requestExecMap = new();

        private readonly Dictionary<Type, RestrictAttribute> requestServiceAttrs = new();

        public Dictionary<Type, Func<IRequest, object>> RequestTypeFactoryMap { get; set; }

        public string DefaultOperationsNamespace { get; set; }

        public Func<IEnumerable<Type>> ResolveServicesFn { get; set; }

        private ContainerResolveCache typeFactory;

        public ServiceController Init()
        {
            typeFactory = new(appHost.Container);
            Register(typeFactory);
            appHost.Container.RegisterAutoWiredTypes(appHost.Metadata.ServiceTypes);
            return this;
        }

        private List<Type> GetAssemblyTypes(Assembly[] assembliesWithServices)
        {
            var results = new List<Type>();
            string assemblyName = null;
            string typeName = null;
            try
            {
                foreach (var assembly in assembliesWithServices)
                {
                    assemblyName = assembly.FullName;
                    foreach (var type in assembly.GetTypes())
                    {
                        if (appHost.ExcludeAutoRegisteringServiceTypes.Contains(type))
                        {
                            continue;
                        }
                        typeName = type.GetOperationName();
                        results.Add(type);
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                var msg = $"Failed loading types, last assembly '{assemblyName}', type: '{typeName}'";
                Log.Error(msg, ex);
                throw new(msg, ex);
            }
        }
        public void RegisterServicesInAssembly(Assembly assembly)
        {
            foreach (var serviceType in assembly.GetTypes().Where(IsServiceType))
            {
                RegisterService(serviceType);
            }
        }

        public void RegisterService(Type serviceType)
        {
            try
            {
                if (!IsServiceType(serviceType))
                {
                    throw new ArgumentException($"Type {serviceType.FullName} is not a Web Service that implements IService");
                }
                RegisterService(typeFactory, serviceType);
                appHost.Container.RegisterAutoWiredType(serviceType);
            }
            catch (Exception ex)
            {
                appHost.NotifyStartupException(ex);
                Log.Error(ex.Message, ex);
            }
        }

        public void Register(ITypeFactory serviceFactoryFn)
        {
            foreach (var serviceType in ResolveServicesFn())
            {
                RegisterService(serviceFactoryFn, serviceType);
            }
        }

        public void RegisterService(ITypeFactory serviceFactoryFn, Type serviceType)
        {
            var processedReqs = new HashSet<Type>();
            if (!IsServiceType(serviceType))
            {
                return;
            }
            foreach (var mi in serviceType.GetActions())
            {
                var requestType = mi.GetParameters()[0].ParameterType;
                if (processedReqs.Contains(requestType))
                {
                    continue;
                }
                processedReqs.Add(requestType);
                RegisterServiceExecutor(requestType, serviceType, serviceFactoryFn);
                var returnMarker = requestType.GetTypeWithGenericTypeDefinitionOf(typeof(IReturn<>));
                var responseType = returnMarker != null
                    ? returnMarker.GetGenericArguments()[0]
                    : mi.ReturnType != typeof(object) && mi.ReturnType != typeof(void)
                        ? mi.ReturnType
#if NETSTANDARD1_6
                        : Type.GetType(requestType.FullName + ResponseDtoSuffix + "," + requestType.GetAssembly().GetName().Name);
#else
                        : AssemblyUtils.FindType(requestType.FullName + ResponseDtoSuffix);
#endif
                if (responseType?.Name == "Task`1" && responseType.GetGenericArguments()[0] != typeof(object))
                {
                    responseType = responseType.GetGenericArguments()[0];
                }
                RegisterRestPaths(requestType);
                appHost.Metadata.Add(serviceType, requestType, responseType);
                if (typeof(IRequiresRequestStream).IsAssignableFrom(requestType))
                {
                    RequestTypeFactoryMap[requestType] = req =>
                    {
                        var restPath = req.GetRoute();
                        var request = restPath != null
                            ? RestHandler.CreateRequest(req, restPath, req.GetRequestParams(), requestType.CreateInstance())
                            : KeyValueDataContractDeserializer.Instance.Parse(req.QueryString, requestType);
                        var rawReq = (IRequiresRequestStream)request;
                        rawReq.RequestStream = req.InputStream;
                        return rawReq;
                    };
                }
                if (Log.IsDebugEnabled)
                {
                    Log.DebugFormat("Registering {0} service '{1}' with request '{2}'",
                        responseType != null ? "Reply" : "OneWay", serviceType.GetOperationName(), requestType.GetOperationName());
                }
            }
        }

        public static bool IsServiceType(Type serviceType)
        {
            return typeof(IService).IsAssignableFrom(serviceType)
                && !serviceType.IsAbstract()
                && !serviceType.IsGenericTypeDefinition()
                && !serviceType.ContainsGenericParameters();
        }

        public readonly Dictionary<string, List<RestPath>> RestPathMap = new();

        public void RegisterRestPaths(Type requestType)
        {
            var attrs = appHost.GetRouteAttributes(requestType);
            foreach (var attr in attrs)
            {
                var restPath = new RestPath(requestType, attr.Path, attr.Verbs, attr.Summary, attr.Notes);
                if (attr is FallbackRouteAttribute)
                {
                    if (appHost.Config.FallbackRestPath != null)
                    {
                        throw new NotSupportedException(
                            "Config.FallbackRestPath is already defined. Only 1 [FallbackRoute] is allowed.");
                    }
                    appHost.Config.FallbackRestPath = (httpMethod, pathInfo, _) =>
                    {
                        var pathInfoParts = RestPath.GetPathPartsForMatching(pathInfo);
                        return restPath.IsMatch(httpMethod, pathInfoParts) ? restPath : null;
                    };
                    continue;
                }
                if (!restPath.IsValid)
                {
                    throw new NotSupportedException(
                        $"RestPath '{attr.Path}' on Type '{requestType.GetOperationName()}' is not Valid");
                }
                RegisterRestPath(restPath);
            }
        }

        private static readonly char[] InvalidRouteChars = { '?', '&' };

        public void RegisterRestPath(RestPath restPath)
        {
            if (!restPath.Path.StartsWith("/"))
            {
                throw new ArgumentException(
                    $"Route '{restPath.Path}' on '{restPath.RequestType.GetOperationName()}' must start with a '/'");
            }
            if (restPath.Path.IndexOfAny(InvalidRouteChars) != -1)
            {
                throw new ArgumentException(
                    $"Route '{restPath.Path}' on '{restPath.RequestType.GetOperationName()}' contains invalid chars. "
                    + "See https://github.com/ServiceStack/ServiceStack/wiki/Routing for info on valid routes.");
            }
            if (!RestPathMap.TryGetValue(restPath.FirstMatchHashKey, out var pathsAtFirstMatch))
            {
                pathsAtFirstMatch = new();
                RestPathMap[restPath.FirstMatchHashKey] = pathsAtFirstMatch;
            }
            pathsAtFirstMatch.Add(restPath);
        }

        public void AfterInit()
        {
            // Register any routes configured on Metadata.Routes
            foreach (var restPath in appHost.RestPaths)
            {
                RegisterRestPath(restPath);
                // Auto add Route Attributes so they're available in T.ToUrl() extension methods
                restPath.RequestType
                    .AddAttributes(new RouteAttribute(restPath.Path, restPath.AllowedVerbs)
                    {
                        Priority = restPath.Priority,
                        Summary = restPath.Summary,
                        Notes = restPath.Notes,
                    });
            }
            // Sync the RestPaths collections
            appHost.RestPaths.Clear();
            appHost.RestPaths.AddRange(RestPathMap.Values.SelectMany(x => x));
            appHost.Metadata.AfterInit();
        }

        public IRestPath GetRestPathForRequest(string httpMethod, string pathInfo)
        {
            var matchUsingPathParts = RestPath.GetPathPartsForMatching(pathInfo);
            List<RestPath> firstMatches;
            IRestPath bestMatch = null;
            var yieldedHashMatches = RestPath.GetFirstMatchHashKeys(matchUsingPathParts);
            foreach (var potentialHashMatch in yieldedHashMatches)
            {
                if (!RestPathMap.TryGetValue(potentialHashMatch, out firstMatches))
                {
                    continue;
                }
                var bestScore = -1;
                foreach (var restPath in firstMatches)
                {
                    var score = restPath.MatchScore(httpMethod, matchUsingPathParts);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMatch = restPath;
                    }
                }
                if (bestScore > 0)
                {
                    return bestMatch;
                }
            }
            var yieldedWildcardMatches = RestPath.GetFirstMatchWildCardHashKeys(matchUsingPathParts);
            foreach (var potentialHashMatch in yieldedWildcardMatches)
            {
                if (!RestPathMap.TryGetValue(potentialHashMatch, out firstMatches))
                {
                    continue;
                }
                var bestScore = -1;
                foreach (var restPath in firstMatches)
                {
                    var score = restPath.MatchScore(httpMethod, matchUsingPathParts);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMatch = restPath;
                    }
                }
                if (bestScore > 0)
                {
                    return bestMatch;
                }
            }
            return null;
        }

        private readonly Dictionary<Type, List<Type>> serviceExecCache = new();
        public void ResetServiceExecCachesIfNeeded(Type serviceType, Type requestType)
        {
            if (!serviceExecCache.TryGetValue(serviceType, out var requestTypes))
            {
                var mi = typeof(ServiceExec<>)
                    .MakeGenericType(serviceType)
                    .GetMethod("Reset", BindingFlags.Public | BindingFlags.Static);
                mi?.Invoke(null, new object[] { });
                serviceExecCache[serviceType] = requestTypes = new();
            }
            if (!requestTypes.Contains(requestType))
            {
                var mi = typeof(ServiceExec<>)
                    .MakeGenericType(serviceType)
                    .GetMethod("CreateServiceRunnersFor", BindingFlags.Public | BindingFlags.Static)
                    ?.MakeGenericMethod(requestType);
                mi?.Invoke(null, new object[] { });
                requestTypes.Add(requestType);
            }
        }

        public void RegisterServiceExecutor(Type requestType, Type serviceType, ITypeFactory serviceFactoryFn)
        {
            ResetServiceExecCachesIfNeeded(serviceType, requestType);
            var serviceExecDef = typeof(ServiceRequestExec<,>).MakeGenericType(serviceType, requestType);
            var iServiceExec = (IServiceExec)serviceExecDef.CreateInstance();
            object HandlerFn(IRequest req, object dto)
            {
                var service = serviceFactoryFn.CreateInstance(req, serviceType);
                object ServiceExec(IRequest reqCtx, object requestDto)
                {
                    return iServiceExec.Execute(reqCtx, service, requestDto);
                }
                return ManagedServiceExec(ServiceExec, (IService)service, req, dto);
            }
            AddToRequestExecMap(requestType, serviceType, HandlerFn);
        }

        private void AddToRequestExecMap(Type requestType, Type serviceType, ServiceExecFn handlerFn)
        {
            if (requestExecMap.ContainsKey(requestType))
            {
                throw new AmbiguousMatchException(
                    $"Could not register Request '{requestType.FullName}' with service '{serviceType.FullName}' as it"
                    + " has already been assigned to another service."
                    + "\nEach Request DTO can only be handled by 1 service.");
            }
            requestExecMap.Add(requestType, handlerFn);
            var requestAttrs = requestType.AllAttributes<RestrictAttribute>();
            if (requestAttrs.Length > 0)
            {
                requestServiceAttrs[requestType] = requestAttrs[0];
            }
            else
            {
                var serviceAttrs = serviceType.AllAttributes<RestrictAttribute>();
                if (serviceAttrs.Length > 0)
                {
                    requestServiceAttrs[requestType] = serviceAttrs[0];
                }
            }
        }

        private object ManagedServiceExec(ServiceExecFn serviceExec, IService service, IRequest request, object requestDto)
        {
            try
            {
                InjectRequestContext(service, request);
                object response = null;
                try
                {
                    requestDto = appHost.OnPreExecuteServiceFilter(service, requestDto, request, request.Response);
                    request.Dto ??= requestDto;
                    // Executes the service and returns the result
                    response = serviceExec(request, requestDto);
                    response = appHost.OnPostExecuteServiceFilter(service, response, request, request.Response);
                    return response;
                }
                finally
                {
                    //Gets disposed by AppHost or ContainerAdapter if set
                    if (response is Task taskResponse)
                    {
                        HostContext.Async.ContinueWith(request, taskResponse, _ => appHost.Release(service));
                    }
                    else
                    {
                        appHost.Release(service);
                    }
                }
            }
            catch (TargetInvocationException tex)
            {
                // Mono invokes using reflection
                throw tex.InnerException ?? tex;
            }
        }

        internal static void InjectRequestContext(object service, IRequest req)
        {
            if (req == null)
            {
                return;
            }
            if (service is IRequiresRequest serviceRequiresContext)
            {
                serviceRequiresContext.Request = req;
            }
        }

        public object ApplyResponseFilters(object response, IRequest req)
        {
            if (response is Task taskResponse)
            {
                response = taskResponse.GetResult();
            }
            return ApplyResponseFiltersInternal(response, req);
        }

        private object ApplyResponseFiltersInternal(object response, IRequest req)
        {
            response = appHost.ApplyResponseConverters(req, response);
            if (appHost.ApplyResponseFilters(req, req.Response, response))
            {
                return req.Response.Dto;
            }
            return response;
        }

        /// <summary>Execute MQ.</summary>
        /// <param name="mqMessage">Message describing the mq.</param>
        /// <returns>An object.</returns>
        public object ExecuteMessage(IMessage mqMessage)
        {
            return ExecuteMessage(mqMessage, new BasicRequest(mqMessage));
        }

        /// <summary>Execute MQ with requestContext.</summary>
        /// <param name="dto">The data transfer object.</param>
        /// <param name="req">The request.</param>
        /// <returns>An object.</returns>
        public object ExecuteMessage(IMessage dto, IRequest req)
        {
            RequestContext.Instance.StartRequestContext();
            req.PopulateFromRequestIfHasSessionId(dto.Body);
            req.Dto = appHost.ApplyRequestConverters(req, dto.Body);
            if (appHost.ApplyMessageRequestFilters(req, req.Response, dto.Body))
            {
                return req.Response.Dto;
            }
            var response = Execute(dto.Body, req);
            if (response is Task taskResponse)
            {
                response = taskResponse.GetResult();
            }
            response = appHost.ApplyResponseConverters(req, response);
            if (appHost.ApplyMessageResponseFilters(req, req.Response, response))
            {
                response = req.Response.Dto;
            }
            req.Response.EndMqRequest();
            return response;
        }

        /// <summary>Execute using empty RequestContext.</summary>
        /// <param name="requestDto">The request data transfer object.</param>
        /// <returns>An object.</returns>
        public object Execute(object requestDto)
        {
            return Execute(requestDto, new BasicRequest());
        }

        /// <summary>External HTTP Request called from HTTP handlers.</summary>
        /// <param name="requestDto">The request data transfer object.</param>
        /// <param name="req">       The request.</param>
        /// <returns>An object.</returns>
        public virtual object Execute(object requestDto, IRequest req)
        {
            req.Dto = requestDto;
            var requestType = requestDto.GetType();
            req.OperationName = requestType.Name;
            if (appHost.Config.EnableAccessRestrictions)
            {
                AssertServiceRestrictions(requestType, req.RequestAttributes);
            }
            var handlerFn = GetService(requestType);
            var response = appHost.OnAfterExecute(req, requestDto, handlerFn(req, requestDto));
            return response;
        }

        // Only Used internally by TypedFilterTests
        public object Execute(object requestDto, IRequest req, bool applyFilters)
        {
            try
            {
                req.SetInProcessRequest();
                if (applyFilters)
                {
                    requestDto = appHost.ApplyRequestConverters(req, requestDto);
                    if (appHost.ApplyRequestFilters(req, req.Response, requestDto))
                    {
                        return null;
                    }
                }
                var response = Execute(requestDto, req);
                return applyFilters
                    ? ApplyResponseFilters(response, req)
                    : response;
            }
            finally
            {
                req.ReleaseIfInProcessRequest();
            }
        }

        [Obsolete("Use Execute(IRequest, applyFilters:true)")]
        public object Execute(IRequest req)
        {
            return Execute(req, applyFilters: true);
        }

        public object Execute(IRequest req, bool applyFilters)
        {
            try
            {
                req.SetInProcessRequest();
                var restPath = RestHandler.FindMatchingRestPath(req.Verb, req.PathInfo, out _);
                req.SetRoute(restPath as RestPath);
                req.OperationName = restPath.RequestType.GetOperationName();
                var requestDto = RestHandler.CreateRequest(req, restPath);
                req.Dto = requestDto;
                if (applyFilters)
                {
                    requestDto = appHost.ApplyRequestConverters(req, requestDto);
                    if (appHost.ApplyRequestFilters(req, req.Response, requestDto))
                    {
                        return null;
                    }
                }
                var response = Execute(requestDto, req);
                return applyFilters
                    ? ApplyResponseFilters(response, req)
                    : response;
            }
            finally
            {
                req.ReleaseIfInProcessRequest();
            }
        }

        public Task<object> ExecuteAsync(object requestDto, IRequest req, bool applyFilters)
        {
            req.Dto = requestDto;
            var requestType = requestDto.GetType();
            if (appHost.Config.EnableAccessRestrictions)
            {
                AssertServiceRestrictions(requestType, req.RequestAttributes);
            }
            if (applyFilters)
            {
                requestDto = appHost.ApplyRequestConverters(req, requestDto);
                if (appHost.ApplyRequestFilters(req, req.Response, requestDto))
                {
                    return TypeConstants.EmptyTask;
                }
            }
            var handlerFn = GetService(requestType);
            var response = handlerFn(req, requestDto);
            if (response is Task<object> taskObj)
            {
                return HostContext.Async.ContinueWith(req, taskObj, t =>
                {
                    if (t.Result is Task[] taskArray)
                    {
                        return Task.Factory.ContinueWhenAll(taskArray, tasks =>
                        {
                            object[] ret = null;
                            for (var i = 0; i < tasks.Length; i++)
                            {
                                var tResult = tasks[i].GetResult();
                                ret ??= (object[])Array.CreateInstance(tResult.GetType(), tasks.Length);
                                ret[i] = ApplyResponseFiltersInternal(tResult, req);
                            }
                            return (object)ret;
                        });
                    }
                    return ApplyResponseFiltersInternal(t.Result, req).AsTaskResult();
                }).Unwrap();
            }
            return applyFilters
                ? ApplyResponseFiltersInternal(response, req).AsTaskResult()
                : response.AsTaskResult();
        }

        public virtual ServiceExecFn GetService(Type requestType)
        {
            if (requestExecMap.TryGetValue(requestType, out var handlerFn))
            {
                return handlerFn;
            }
            if (!requestType.IsArray)
            {
                throw new NotImplementedException($"Unable to resolve service '{requestType.GetOperationName()}'");
            }
            var elType = requestType.GetElementType();
            if (requestExecMap.TryGetValue(elType, out handlerFn))
            {
                return CreateAutoBatchServiceExec(handlerFn);
            }
            throw new NotImplementedException($"Unable to resolve service '{requestType.GetOperationName()}'");
        }

        private static ServiceExecFn CreateAutoBatchServiceExec(ServiceExecFn handlerFn)
        {
            return (req, dtos) =>
            {
                var dtosList = ((IEnumerable)dtos).Map(x => x);
                if (dtosList.Count == 0)
                {
                    return TypeConstants.EmptyObjectArray;
                }
                var firstDto = dtosList[0];
                var firstResponse = handlerFn(req, firstDto);
                if (firstResponse is Exception)
                {
                    req.SetAutoBatchCompletedHeader(0);
                    return firstResponse;
                }
                // sync
                if (firstResponse is not Task asyncResponse)
                {
                    var ret = firstResponse != null
                        ? (object[])Array.CreateInstance(firstResponse.GetType(), dtosList.Count)
                        : new object[dtosList.Count];
                    ret[0] = firstResponse; //don't re-execute first request
                    for (var i = 1; i < dtosList.Count; i++)
                    {
                        var dto = dtosList[i];
                        var response = handlerFn(req, dto);
                        //short-circuit on first error
                        if (response is Exception)
                        {
                            req.SetAutoBatchCompletedHeader(i);
                            return response;
                        }
                        ret[i] = response;
                    }
                    req.SetAutoBatchCompletedHeader(dtosList.Count);
                    return ret;
                }
                // async
                var asyncResponses = new Task[dtosList.Count];
                Task firstAsyncError = null;
                // execute each async service sequentially
                var task = dtosList.EachAsync((dto, i) =>
                {
                    // short-circuit on first error and don't exec any more handlers
                    if (firstAsyncError != null)
                    {
                        return firstAsyncError;
                    }
                    asyncResponses[i] = i == 0
                        ? asyncResponse //don't re-execute first request
                        : (Task)handlerFn(req, dto);
                    var asyncResult = asyncResponses[i].GetResult();
                    if (asyncResult is Exception)
                    {
                        req.SetAutoBatchCompletedHeader(i);
                        return firstAsyncError = asyncResponses[i];
                    }
                    return asyncResponses[i];
                });
                return HostContext.Async.ContinueWith(req, task, _ =>
                {
                    if (firstAsyncError != null)
                    {
                        return firstAsyncError;
                    }
                    req.SetAutoBatchCompletedHeader(dtosList.Count);
                    return asyncResponses;
                }); // return error or completed responses
            };
        }

        public void AssertServiceRestrictions(Type requestType, RequestAttributes actualAttributes)
        {
            if (!appHost.Config.EnableAccessRestrictions)
            {
                return;
            }
            if ((RequestAttributes.InProcess & actualAttributes) == RequestAttributes.InProcess)
            {
                return;
            }
            var hasNoAccessRestrictions = !requestServiceAttrs.TryGetValue(requestType, out var restrictAttr)
                || restrictAttr.HasNoAccessRestrictions;
            if (hasNoAccessRestrictions)
            {
                return;
            }
            var failedScenarios = StringBuilderCache.Allocate();
            foreach (var requiredScenario in restrictAttr.AccessibleToAny)
            {
                var allServiceRestrictionsMet = (requiredScenario & actualAttributes) == actualAttributes;
                if (allServiceRestrictionsMet)
                {
                    return;
                }
                var passed = requiredScenario & actualAttributes;
                var failed = requiredScenario & ~passed;
                failedScenarios.Append($"\n -[{failed}]");
            }
            var internalDebugMsg = (RequestAttributes.InternalNetworkAccess & actualAttributes) != 0
                ? "\n Unauthorized call was made from: " + actualAttributes
                : "";
            throw new UnauthorizedAccessException(
                $"Could not execute service '{requestType.GetOperationName()}', The following restrictions were not met: " +
                $"'{StringBuilderCache.ReturnAndFree(failedScenarios)}'{internalDebugMsg}");
        }
    }
}
