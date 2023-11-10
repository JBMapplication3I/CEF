using System;
using System.Web;
using ServiceStack.Web;

namespace ServiceStack.Host.Handlers
{
    public class CustomActionHandler : HttpAsyncTaskHandler
    {
        public Action<IRequest, IResponse> Action { get; set; }

        public CustomActionHandler(Action<IRequest, IResponse> action)
        {
            Action = action ?? throw new NullReferenceException("action");
            RequestName = GetType().Name;
        }

        public override void ProcessRequest(IRequest httpReq, IResponse httpRes, string operationName)
        {
            if (HostContext.ApplyCustomHandlerRequestFilters(httpReq, httpRes))
            {
                return;
            }

            Action(httpReq, httpRes);
            httpRes.EndHttpHandlerRequest(skipHeaders: true);
        }
    }
}