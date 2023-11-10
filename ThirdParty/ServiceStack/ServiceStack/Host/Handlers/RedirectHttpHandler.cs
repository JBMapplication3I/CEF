using System;
using System.Net;
using ServiceStack.Web;

namespace ServiceStack.Host.Handlers
{
    public class RedirectHttpHandler : HttpAsyncTaskHandler
    {
        public RedirectHttpHandler()
        {
            RequestName = nameof(RedirectHttpHandler);
            StatusCode = HttpStatusCode.Redirect;
        }

        public string RelativeUrl { get; set; }

        public string AbsoluteUrl { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public static string MakeRelative(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
            {
                return null;
            }

            if (relativeUrl.StartsWith("~/"))
            {
                return relativeUrl;
            }

            return relativeUrl.StartsWith("/")
                ? "~" + relativeUrl
                : "~/" + relativeUrl;
        }

        public override void ProcessRequest(IRequest request, IResponse response, string operationName)
        {
            if (string.IsNullOrEmpty(RelativeUrl) && string.IsNullOrEmpty(AbsoluteUrl))
            {
                throw new ArgumentException("RelativeUrl and AbsoluteUrl is Required");
            }

            if (!string.IsNullOrEmpty(AbsoluteUrl))
            {
                response.StatusCode = (int)StatusCode;
                response.AddHeader(HttpHeaders.Location, AbsoluteUrl);
            }
            else
            {
                if (RelativeUrl.StartsWith("http://") || RelativeUrl.StartsWith("https://"))
                {
                    throw new ArgumentException($"'{RelativeUrl}' is not a RelativeUrl, use AbsoluteUrl instead");
                }

                var absoluteUrl = RelativeUrl.StartsWith("/")
                    ? request.GetApplicationUrl().CombineWith(RelativeUrl) //preserve compat
                    : request.ResolveAbsoluteUrl(MakeRelative(RelativeUrl));

                response.StatusCode = (int)StatusCode;
                response.AddHeader(HttpHeaders.Location, absoluteUrl);
            }

            response.EndHttpHandlerRequest(skipClose: true);
        }
    }
}