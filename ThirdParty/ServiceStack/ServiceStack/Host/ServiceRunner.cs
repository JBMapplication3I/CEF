namespace ServiceStack.Host
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Logging;
    using Messaging;
    using Text;
    using Web;

    public class ServiceRunner<TRequest> : IServiceRunner<TRequest>
    {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(ServiceRunner<>));

        protected readonly IAppHost AppHost;
        protected readonly ActionContext ActionContext;
        protected readonly ActionInvokerFn ServiceAction;
        protected readonly IHasRequestFilter[] RequestFilters;
        protected readonly IHasResponseFilter[] ResponseFilters;

        public ServiceRunner(IAppHost appHost, ActionContext actionContext)
        {
            AppHost = appHost;
            ActionContext = actionContext;
            ServiceAction = actionContext.ServiceAction;
            if (actionContext.RequestFilters != null)
            {
                RequestFilters = actionContext.RequestFilters.OrderBy(x => x.Priority).ToArray();
            }
            if (actionContext.ResponseFilters != null)
            {
                ResponseFilters = actionContext.ResponseFilters.OrderBy(x => x.Priority).ToArray();
            }
        }

        public T ResolveService<T>(IRequest requestContext)
        {
            var service = AppHost.TryResolve<T>();
            if (service is IRequiresRequest requiresContext)
            {
                requiresContext.Request = requestContext;
            }
            return service;
        }

        public virtual void BeforeEachRequest(IRequest req, TRequest request)
        {
            var requestLogger = AppHost.TryResolve<IRequestLogger>();
            if (requestLogger != null)
            {
                req.SetItem(Keywords.RequestDuration, Stopwatch.StartNew());
            }
            OnBeforeExecute(req, request);
        }

        public virtual object AfterEachRequest(IRequest req, TRequest request, object response)
        {
            // only call OnAfterExecute if no exception occurred
            return response.IsErrorResponse() ? response : OnAfterExecute(req, response);
        }

        public virtual void OnBeforeExecute(IRequest req, TRequest request) { }

        public virtual object OnAfterExecute(IRequest req, object response)
        {
            return response;
        }

        public virtual object Execute(IRequest req, object instance, TRequest requestDto)
        {
            try
            {
                BeforeEachRequest(req, requestDto);
                var container = HostContext.Container;
                if (RequestFilters != null)
                {
                    foreach (var requestFilter in RequestFilters)
                    {
                        var attrInstance = requestFilter.Copy();
                        container.AutoWire(attrInstance);
                        attrInstance.RequestFilter(req, req.Response, requestDto);
                        AppHost.Release(attrInstance);
                        if (req.Response.IsClosed)
                        {
                            return null;
                        }
                    }
                }
                var response = AfterEachRequest(req, requestDto, ServiceAction(instance, requestDto));
                if (HostContext.StrictMode && response?.GetType().IsValueType() == true)
                {
                    throw new StrictModeException(
                        $"'{requestDto.GetType().Name}' Service cannot return Value Types for its Service Responses. "
                        + $"You can embed its '{response.GetType().Name}' return value in a Response DTO or return as raw data in a string or byte[]",
                        StrictModeCodes.ReturnsValueType);
                }
                if (response is Task taskResponse)
                {
                    if (taskResponse.Status == TaskStatus.Created)
                    {
                        taskResponse.Start();
                    }
                    return HostContext.Async.ContinueWith(req, taskResponse, task =>
                    {
                        if (task.IsFaulted)
                        {
                            var ex = task.Exception.UnwrapIfSingleException();
                            // Async Exception Handling
                            var result = HandleException(req, requestDto, ex);
                            return result ?? ex;
                        }
                        response = task.GetResult();
                        LogRequest(req, requestDto, response);
                        if (ResponseFilters == null)
                        {
                            return response;
                        }
                        // Async Exec ResponseFilters
                        foreach (var responseFilter in ResponseFilters)
                        {
                            var attrInstance = responseFilter.Copy();
                            container.AutoWire(attrInstance);
                            attrInstance.ResponseFilter(req, req.Response, response);
                            AppHost.Release(attrInstance);
                            if (req.Response.IsClosed)
                            {
                                return null;
                            }
                        }
                        return response;
                    });
                }
                LogRequest(req, requestDto, response);
                if (response is IHttpError error)
                {
                    var ex = (Exception)error;
                    var result = HandleException(req, requestDto, ex);
                    if (result == null)
                    {
                        throw ex;
                    }
                    return result;
                }
                // Sync Exec ResponseFilters
                if (ResponseFilters == null)
                {
                    return response;
                }
                foreach (var responseFilter in ResponseFilters)
                {
                    var attrInstance = responseFilter.Copy();
                    container.AutoWire(attrInstance);
                    attrInstance.ResponseFilter(req, req.Response, response);
                    AppHost.Release(attrInstance);
                    if (req.Response.IsClosed)
                    {
                        return null;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                // Sync Exception Handling
                var result = HandleException(req, requestDto, ex);
                if (result == null)
                {
                    throw;
                }
                return result;
            }
        }

        private void LogRequest(IRequest req, object requestDto, object response)
        {
            var requestLogger = AppHost.TryResolve<IRequestLogger>();
            if (requestLogger == null || req.Items.ContainsKey(Keywords.HasLogged))
            {
                return;
            }
            try
            {
                req.Items[Keywords.HasLogged] = true;
                var stopWatch = req.GetItem(Keywords.RequestDuration) as Stopwatch;
                requestLogger.Log(req, requestDto, response, stopWatch?.Elapsed ?? TimeSpan.Zero);
            }
            catch (Exception ex)
            {
                Log.Error("Error while logging req: " + req.Dump(), ex);
            }
        }

        public virtual object Execute(IRequest req, object instance, IMessage<TRequest> request)
        {
            return Execute(req, instance, request.GetBody());
        }

        public virtual object HandleException(IRequest request, TRequest requestDto, Exception ex)
        {
            var errorResponse = HostContext.RaiseServiceException(request, requestDto, ex)
                ?? DtoUtils.CreateErrorResponse(requestDto, ex);
            AfterEachRequest(request, requestDto, errorResponse ?? ex);
            return errorResponse;
        }

        public object ExecuteOneWay(IRequest requestContext, object instance, TRequest request)
        {
            var msgFactory = AppHost.TryResolve<IMessageFactory>();
            if (msgFactory == null)
            {
                return Execute(requestContext, instance, request);
            }
            // Capture and persist this async req on this Services 'In Queue'
            // for execution after this req has been completed
            using (var producer = msgFactory.CreateMessageProducer())
            {
                producer.Publish(request);
            }
            return WebRequestUtils.GetErrorResponseDtoType(request).CreateInstance();
        }

        // signature matches ServiceExecFn
        public object Process(IRequest requestContext, object instance, object request)
        {
            return requestContext?.RequestAttributes.Has(RequestAttributes.OneWay) == true
                ? ExecuteOneWay(requestContext, instance, (TRequest)request)
                : Execute(requestContext, instance, (TRequest)request);
        }
    }
}
