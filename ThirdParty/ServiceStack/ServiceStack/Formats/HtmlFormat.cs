using System;
using System.Linq;
using ServiceStack.Serialization;
using ServiceStack.Templates;
using ServiceStack.Web;

namespace ServiceStack.Formats
{
    public class HtmlFormat : IPlugin
    {
        public static string TitleFormat
            = @"{0} Snapshot of {1}";

        public static string HtmlTitleFormat
            = @"Snapshot of <i>{0}</i> generated by <a href=""https://servicestack.net"">ServiceStack</a> on <b>{1}</b>";

        public static bool Humanize = true;

        private IAppHost AppHost { get; set; }

        public const string ModelKey = "Model";
        public const string ErrorStatusKey = "__errorStatus";

        public void Register(IAppHost appHost)
        {
            AppHost = appHost;
            //Register this in ServiceStack with the custom formats
            appHost.ContentTypes.Register(MimeTypes.Html, SerializeToStream, null);
            appHost.ContentTypes.Register(MimeTypes.JsonReport, SerializeToStream, null);

            appHost.Config.DefaultContentType = MimeTypes.Html;
            appHost.Config.IgnoreFormatsInMetadata.Add(MimeTypes.Html.ToContentFormat());
            appHost.Config.IgnoreFormatsInMetadata.Add(MimeTypes.JsonReport.ToContentFormat());
        }

        public void SerializeToStream(IRequest req, object response, IResponse res)
        {
            var httpResult = req.GetItem("HttpResult") as IHttpResult;
            if (httpResult?.Headers.ContainsKey(HttpHeaders.Location) == true 
                && httpResult.StatusCode != System.Net.HttpStatusCode.Created)
            {
                return;
            }

            try
            {
                if (res.StatusCode >= 400)
                {
                    var responseStatus = response.GetResponseStatus();
                    req.Items[ErrorStatusKey] = responseStatus;
                }

                if (response is CompressedResult)
                {
                    if (res.Dto != null)
                    {
                        response = res.Dto;
                    }
                    else
                    {
                        throw new ArgumentException("Cannot use Cached Result as ViewModel");
                    }
                }

                if (AppHost.ViewEngines.Any(x => x.ProcessRequest(req, res, response)))
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                if (res.StatusCode < 400)
                {
                    throw;
                }

                //If there was an exception trying to render a Error with a View, 
                //It can't handle errors so just write it out here.
                response = DtoUtils.CreateErrorResponse(req.Dto, ex);
            }

            //Handle Exceptions returning string
            if (req.ResponseContentType == MimeTypes.PlainText)
            {
                req.ResponseContentType = MimeTypes.Html;
                res.ContentType = MimeTypes.Html;
            }

            if (req.ResponseContentType != MimeTypes.Html
                && req.ResponseContentType != MimeTypes.JsonReport)
            {
                return;
            }

            var dto = response.GetDto();
            if (dto is not string html)
            {
                // Serialize then escape any potential script tags to avoid XSS when displaying as HTML
                var json = JsonDataContractSerializer.Instance.SerializeToString(dto) ?? "null";
                json = json.Replace("<", "&lt;").Replace(">", "&gt;");

                var url = req.ResolveAbsoluteUrl()
                    .Replace("format=html", "")
                    .Replace("format=shtm", "")
                    .TrimEnd('?', '&');

                url += url.Contains("?") ? "&" : "?";

                var now = DateTime.UtcNow;
                var requestName = req.OperationName ?? dto.GetType().GetOperationName();

                html = HtmlTemplates.GetHtmlFormatTemplate()
                    .Replace("${Dto}", json)
                    .Replace("${Title}", string.Format(TitleFormat, requestName, now))
                    .Replace("${MvcIncludes}", MiniProfiler.Profiler.RenderIncludes().ToString())
                    .Replace("${Header}", string.Format(HtmlTitleFormat, requestName, now))
                    .Replace("${ServiceUrl}", url)
                    .Replace("${Humanize}", Humanize.ToString().ToLower());
            }

            var utf8Bytes = html.ToUtf8Bytes();
            res.OutputStream.Write(utf8Bytes, 0, utf8Bytes.Length);
        }
    }

}