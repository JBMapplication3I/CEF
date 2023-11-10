using System;
using ServiceStack.Web;

namespace ServiceStack.Host.Handlers
{
    public class CustomResponseHandler : HttpAsyncTaskHandler
    {
        public Func<IRequest, IResponse, object> Action { get; set; }

        public CustomResponseHandler(Func<IRequest, IResponse, object> action, string operationName = null)
        {
            Action = action;
            RequestName = operationName ?? "CustomResponse";
        }

        public override void ProcessRequest(IRequest httpReq, IResponse httpRes, string operationName)
        {
            if (Action == null)
            {
                throw new("Action was not supplied to ActionHandler");
            }

            if (HostContext.ApplyCustomHandlerRequestFilters(httpReq, httpRes))
            {
                return;
            }

            httpReq.OperationName ??= RequestName;

            var response = Action(httpReq, httpRes);
            httpRes.WriteToResponse(httpReq, response);
        }
    }
}