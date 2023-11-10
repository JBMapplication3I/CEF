using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.Web;

namespace ServiceStack
{
    public class CompressedResult
        : IStreamWriterAsync, IHttpResult
    {
        public const int Adler32ChecksumLength = 4;

        public const string DefaultContentType = MimeTypes.Xml;

        public byte[] Contents { get; }

        public string ContentType { get; set; }

        public Dictionary<string, string> Headers { get; }
        public List<Cookie> Cookies { get; }

        public int Status { get; set; }

        public HttpStatusCode StatusCode
        {
            get => (HttpStatusCode)Status;
            set => Status = (int)value;
        }

        public string StatusDescription { get; set; }

        public object Response
        {
            get => Contents;
            set => throw new NotImplementedException();
        }

        public IContentTypeWriter ResponseFilter { get; set; }

        public IRequest RequestContext { get; set; }

        public int PaddingLength { get; set; }

        public Func<IDisposable> ResultScope { get; set; }

        public IDictionary<string, string> Options => Headers;

        public DateTime? LastModified
        {
            set
            {
                if (value == null)
                {
                    return;
                }

                Headers[HttpHeaders.LastModified] = value.Value.ToUniversalTime().ToString("r");

                var feature = HostContext.GetPlugin<HttpCacheFeature>();
                if (feature?.CacheControlForOptimizedResults != null)
                {
                    Headers[HttpHeaders.CacheControl] = feature.CacheControlForOptimizedResults;
                }
            }
        }

        public CompressedResult(byte[] contents)
            : this(contents, CompressionTypes.Deflate)
        { }

        public CompressedResult(byte[] contents, string compressionType)
            : this(contents, compressionType, DefaultContentType)
        { }

        public CompressedResult(byte[] contents, string compressionType, string contentMimeType)
        {
            if (!CompressionTypes.IsValid(compressionType))
            {
                throw new ArgumentException("Must be either 'deflate' or 'gzip'", compressionType);
            }

            StatusCode = HttpStatusCode.OK;
            ContentType = contentMimeType;

            Contents = contents;
            Headers = new()
            {
                { HttpHeaders.ContentEncoding, compressionType },
            };
            Cookies = new();
        }

        public async Task WriteToAsync(Stream responseStream, CancellationToken token = new())
        {
            var response = RequestContext?.Response;
            response?.SetContentLength(Contents.Length + PaddingLength);

            await responseStream.WriteAsync(Contents, token);
            //stream.Write(this.Contents, Adler32ChecksumLength, this.Contents.Length - Adler32ChecksumLength);

            await responseStream.FlushAsync(token);
        }
    }
}