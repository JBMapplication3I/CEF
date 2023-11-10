using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.Host;
using ServiceStack.Web;

namespace ServiceStack.Testing
{
    public class MockHttpResponse : IHttpResponse
    {
        public MockHttpResponse(IRequest request = null)
        {
            Request = request;
            Headers = PclExportClient.Instance.NewNameValueCollection();
            OutputStream = new MemoryStream();
            TextWritten = new();
            Cookies = HostContext.AssertAppHost().GetCookies(this);
            Items = new();
        }

        public IRequest Request { get; private set; }
        public object OriginalResponse { get; private set; }
        public int StatusCode { set; get; }
        public string StatusDescription { set; get; }
        public string ContentType { get; set; }

        public StringBuilder TextWritten { get; set; }

        public INameValueCollection Headers { get; set; }

        public ICookies Cookies { get; set; }

        public void AddHeader(string name, string value)
        {
            Headers.Add(name, value);
        }

        public void RemoveHeader(string name)
        {
            Headers.Remove(name);
        }

        public string GetHeader(string name)
        {
            return Headers[name];
        }

        public void Redirect(string url)
        {
            Headers.Add(HttpHeaders.Location, url.MapServerPath());
        }

        public Stream OutputStream { get; }

        public object Dto { get; set; }

        public void Write(string text)
        {
            TextWritten.Append(text);
        }

        public bool UseBufferedStream { get; set; }

        public void Close()
        {
            IsClosed = true;
        }

        public void End()
        {
            Close();
        }

        public void Flush()
        {
            OutputStream.Flush();
        }

        public Task FlushAsync(CancellationToken token = default(CancellationToken)) => OutputStream.FlushAsync(token);

        public string ReadAsString()
        {
            if (!IsClosed)
            {
                OutputStream.Seek(0, SeekOrigin.Begin);
            }

            var bytes = ((MemoryStream)OutputStream).ToArray();
            return bytes.FromUtf8Bytes();
        }

        public byte[] ReadAsBytes()
        {
            if (!IsClosed)
            {
                OutputStream.Seek(0, SeekOrigin.Begin);
            }

            var ms = (MemoryStream)OutputStream;
            return ms.ToArray();
        }

        public bool IsClosed { get; private set; }

        public void SetContentLength(long contentLength)
        {
            Headers[HttpHeaders.ContentLength] = contentLength.ToString(CultureInfo.InvariantCulture);
        }

        public bool KeepAlive { get; set; }

        public Dictionary<string, object> Items { get; }

        public void SetCookie(Cookie cookie)
        {
        }

        public void ClearCookies()
        {
        }
    }
}
