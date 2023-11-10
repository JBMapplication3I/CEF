//Copyright (c) ServiceStack, Inc. All Rights Reserved.
//License: https://raw.github.com/ServiceStack/ServiceStack/master/license.txt

namespace ServiceStack
{
#if !(XBOX || SL5 || NETFX_CORE || WP || PCL || NETSTANDARD1_1 || NETSTANDARD1_6)
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using ServiceStack;
    using ServiceStack.Web;
#if !(ANDROID || __IOS__ || __MAC__)
    using System.Web;

    public class Net40PclExportClient : PclExportClient
    {
        public static Net40PclExportClient Provider = new();

        public static PclExportClient Configure()
        {
            Configure(Provider ?? (Provider = new()));
            Net40PclExport.Configure();
            return Provider;
        }

        public override INameValueCollection NewNameValueCollection()
        {
            return new NameValueCollectionWrapper(new());
        }

        public override INameValueCollection ParseQueryString(string query)
        {
            return HttpUtility.ParseQueryString(query).InWrapper();
        }

        public override string UrlEncode(string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        public override string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        public override string HtmlEncode(string html)
        {
            return HttpUtility.HtmlEncode(html);
        }

        public override string HtmlDecode(string html)
        {
            return HttpUtility.HtmlDecode(html);
        }

        public override string GetHeader(WebHeaderCollection headers, string name, Func<string, bool> valuePredicate)
        {
            var values = headers.GetValues(name);
            return values == null ? null : values.FirstOrDefault(valuePredicate);
        }

        public override ITimer CreateTimer(TimerCallback cb, TimeSpan timeOut, object state)
        {
            return new AsyncTimer(new(s => cb(s), state, (int)timeOut.TotalMilliseconds, Timeout.Infinite));
        }

        public override Exception CreateTimeoutException(Exception ex, string errorMsg)
        {
            return new WebException("The request timed out", ex, WebExceptionStatus.Timeout, null);
        }

        public override bool IsWebException(WebException webEx)
        {
            return webEx != null && webEx.Response != null
                && webEx.Status == WebExceptionStatus.ProtocolError;
        }
    }
#endif

    public class AsyncTimer : ITimer
    {
        public System.Threading.Timer Timer;

        public AsyncTimer(System.Threading.Timer timer)
        {
            Timer = timer;
        }

        public void Cancel()
        {
            if (Timer == null)
            {
                return;
            }

            Timer.Change(Timeout.Infinite, Timeout.Infinite);
            Dispose();
        }

        public void Dispose()
        {
            if (Timer == null)
            {
                return;
            }

            Timer.Dispose();
            Timer = null;
        }
    }
}
#endif
