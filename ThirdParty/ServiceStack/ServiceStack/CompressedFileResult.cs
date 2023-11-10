using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.Web;

namespace ServiceStack
{
    public class CompressedFileResult
        : IStreamWriterAsync, IHasOptions
    {
        public const int Adler32ChecksumLength = 4;

        public const string DefaultContentType = MimeTypes.Xml;

        public string FilePath { get; private set; }

        public Dictionary<string, string> Headers { get; private set; }

        public IDictionary<string, string> Options => Headers;

        public CompressedFileResult(string filePath)
            : this(filePath, CompressionTypes.Deflate)
        { }

        public CompressedFileResult(string filePath, string compressionType)
            : this(filePath, compressionType, DefaultContentType)
        { }

        public CompressedFileResult(string filePath, string compressionType, string contentMimeType)
        {
            if (!CompressionTypes.IsValid(compressionType))
            {
                throw new ArgumentException(@"Must be either 'deflate' or 'gzip'", compressionType);
            }
            FilePath = filePath;
            Headers = new()
            {
                { HttpHeaders.ContentType, contentMimeType },
                { HttpHeaders.ContentEncoding, compressionType },
            };
        }

        public async Task WriteToAsync(Stream responseStream, CancellationToken token = new())
        {
            using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            fs.Position = Adler32ChecksumLength;
            await fs.CopyToAsync(responseStream, token).ConfigureAwait(false);
            await responseStream.FlushAsync(token).ConfigureAwait(false);
        }
    }
}
