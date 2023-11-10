namespace ServiceStack
{
    using System;
    using System.IO;
    using Web;

    public class CompressResponseAttribute : ResponseFilterAttribute
    {
        public override void Execute(IRequest req, IResponse res, object response)
        {
            if (response is Exception or CompressedResult)
            {
                // The result is either an error or already compressed
                return;
            }
            var httpResult = response as IHttpResult;
            var src = httpResult != null ? httpResult.Response : response;
            if (src == null)
            {
                var concreteResult = response as HttpResult;
                src = concreteResult?.ResponseStream
                    ?? (object)concreteResult?.ResponseText;
            }
#pragma warning disable 618
            if (src is null or IPartialWriter or IStreamWriter)
            {
                return;
            }
#pragma warning restore 618
            var encoding = req.GetCompressionType();
            if (encoding == null)
            {
                // Client doesn't support compression
                return;
            }
            var responseBytes = src as byte[];
            responseBytes ??= src switch
            {
                string rawStr => rawStr.ToUtf8Bytes(),
                Stream stream => stream.ReadFully(),
                _ => null,
            };
            if (responseBytes != null || req.ResponseContentType.IsBinary())
            {
                responseBytes ??= HostContext.ContentTypes.SerializeToBytes(req, src);
                res.AddHeader(HttpHeaders.ContentEncoding, encoding);
                responseBytes = responseBytes.CompressBytes(encoding);
            }
            else
            {
                var serializedDto = req.SerializeToString(src);
                if (req.ResponseContentType.MatchesContentType(MimeTypes.Json))
                {
                    var jsonp = req.GetJsonpCallback();
                    if (jsonp != null)
                    {
                        serializedDto = jsonp + "(" + serializedDto + ")";
                    }
                }
                responseBytes = serializedDto.ToUtf8Bytes();
                res.AddHeader(HttpHeaders.ContentEncoding, encoding);
                responseBytes = responseBytes.CompressBytes(encoding);
            }
            if (httpResult != null)
            {
                foreach (var header in httpResult.Headers)
                {
                    res.AddHeader(header.Key, header.Value);
                }
            }
#pragma warning disable 618
            res.WriteBytesToResponse(responseBytes, req.ResponseContentType);
#pragma warning restore 618
            using (response as IDisposable) { }
        }
    }
}
