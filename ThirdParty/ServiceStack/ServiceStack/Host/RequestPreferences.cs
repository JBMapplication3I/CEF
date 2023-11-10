namespace ServiceStack.Host
{
    using System;
    using System.Linq;
    using System.Web;
    using Text;
    using Web;

    public class RequestPreferences : IRequestPreferences
    {
        private readonly HttpContextBase httpContext;
        private string acceptEncoding;
        private HttpWorkerRequest httpWorkerRequest;

        public RequestPreferences(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
            acceptEncoding = httpContext.Request.Headers[HttpHeaders.AcceptEncoding];
            if (acceptEncoding.IsNullOrEmpty()/*
                || httpContext.Request.UserAgent?.Contains("Safari/") == true
                && !httpContext.Request.UserAgent.Contains("Chrome/")*/)
            {
                acceptEncoding = "none";
                return;
            }
            acceptEncoding = acceptEncoding.ToLower();
        }

        public RequestPreferences(IRequest httpRequest)
        {
            acceptEncoding = httpRequest.Headers[HttpHeaders.AcceptEncoding];
            if (acceptEncoding.IsNullOrEmpty()/*
                || httpRequest.UserAgent?.Contains("Safari/") == true
                && !httpRequest.UserAgent.Contains("Chrome/")*/)
            {
                acceptEncoding = "none";
                return;
            }
            acceptEncoding = acceptEncoding.ToLower();
        }

        public bool AcceptsGzip => AcceptsEncodingCheck("gzip");

        public bool AcceptsDeflate => AcceptsEncodingCheck("deflate");

#if NET60_OR_GREATER
        public bool AcceptsBrotli => AcceptsEncodingCheck("br");
#endif

        public string AcceptEncoding
        {
            get
            {
                if (acceptEncoding != null)
                {
                    return acceptEncoding;
                }
                if (Env.IsMono)
                {
                    return acceptEncoding;
                }
                acceptEncoding = HttpWorkerRequest.GetKnownRequestHeader(HttpWorkerRequest.HeaderAcceptEncoding)?.ToLower();
                return acceptEncoding;
            }
        }

        private HttpWorkerRequest HttpWorkerRequest => httpWorkerRequest ??= GetWorker(httpContext);

        public static HttpWorkerRequest GetWorker(HttpContextBase context)
        {
            var provider = (IServiceProvider)context;
            var worker = (HttpWorkerRequest)provider.GetService(typeof(HttpWorkerRequest));
            return worker;
        }

        private bool AcceptsEncodingCheck(string encoding)
        {
            return AcceptEncoding
                ?.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Contains(encoding) == true;
        }
    }
}
