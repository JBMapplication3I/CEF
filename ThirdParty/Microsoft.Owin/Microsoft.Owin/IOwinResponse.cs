// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.IOwinResponse
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>This wraps OWIN environment dictionary and provides strongly typed accessors.</summary>
    public interface IOwinResponse
    {
        /// <summary>Gets or sets the owin.ResponseBody Stream.</summary>
        /// <value>The owin.ResponseBody Stream.</value>
        Stream Body { get; set; }

        /// <summary>Gets or sets the Content-Length header.</summary>
        /// <value>The Content-Length header.</value>
        long? ContentLength { get; set; }

        /// <summary>Gets or sets the Content-Type header.</summary>
        /// <value>The Content-Type header.</value>
        string ContentType { get; set; }

        /// <summary>Gets the request context.</summary>
        /// <value>The request context.</value>
        IOwinContext Context { get; }

        /// <summary>Gets a collection used to manipulate the Set-Cookie header.</summary>
        /// <value>A collection used to manipulate the Set-Cookie header.</value>
        ResponseCookieCollection Cookies { get; }

        /// <summary>Gets the OWIN environment.</summary>
        /// <value>The OWIN environment.</value>
        IDictionary<string, object> Environment { get; }

        /// <summary>Gets or sets the E-Tag header.</summary>
        /// <value>The E-Tag header.</value>
        string ETag { get; set; }

        /// <summary>Gets or sets the Expires header.</summary>
        /// <value>The Expires header.</value>
        DateTimeOffset? Expires { get; set; }

        /// <summary>Gets the response header collection.</summary>
        /// <value>The response header collection.</value>
        IHeaderDictionary Headers { get; }

        /// <summary>Gets or sets the owin.ResponseProtocol.</summary>
        /// <value>The owin.ResponseProtocol.</value>
        string Protocol { get; set; }

        /// <summary>Gets or sets the the optional owin.ResponseReasonPhrase.</summary>
        /// <value>The the optional owin.ResponseReasonPhrase.</value>
        string ReasonPhrase { get; set; }

        /// <summary>Gets or sets the optional owin.ResponseStatusCode.</summary>
        /// <value>The optional owin.ResponseStatusCode, or 200 if not set.</value>
        int StatusCode { get; set; }

        /// <summary>Gets a value from the OWIN environment, or returns default(T) if not present.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key or the default(T) if not present.</returns>
        T Get<T>(string key);

        /// <summary>Registers for an event that fires when the response headers are sent.</summary>
        /// <param name="callback">The callback method.</param>
        /// <param name="state">   The callback state.</param>
        void OnSendingHeaders(Action<object> callback, object state);

        /// <summary>Sets a 302 response status code and the Location header.</summary>
        /// <param name="location">The location where to redirect the client.</param>
        void Redirect(string location);

        /// <summary>Sets the given key and value in the OWIN environment.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">  The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>This instance.</returns>
        IOwinResponse Set<T>(string key, T value);

        /// <summary>Writes the given text to the response body stream using UTF-8.</summary>
        /// <param name="text">The response data.</param>
        void Write(string text);

        /// <summary>Writes the given bytes to the response body stream.</summary>
        /// <param name="data">The response data.</param>
        void Write(byte[] data);

        /// <summary>Writes the given bytes to the response body stream.</summary>
        /// <param name="data">  The response data.</param>
        /// <param name="offset">The zero-based byte offset in the <paramref name="data" /> parameter at which to begin
        ///                      copying bytes.</param>
        /// <param name="count"> The number of bytes to write.</param>
        void Write(byte[] data, int offset, int count);

        /// <summary>Asynchronously writes the given text to the response body stream using UTF-8.</summary>
        /// <param name="text">The response data.</param>
        /// <returns>A Task tracking the state of the write operation.</returns>
        Task WriteAsync(string text);

        /// <summary>Asynchronously writes the given text to the response body stream using UTF-8.</summary>
        /// <param name="text"> The response data.</param>
        /// <param name="token">A token used to indicate cancellation.</param>
        /// <returns>A Task tracking the state of the write operation.</returns>
        Task WriteAsync(string text, CancellationToken token);

        /// <summary>Asynchronously writes the given bytes to the response body stream.</summary>
        /// <param name="data">The response data.</param>
        /// <returns>A Task tracking the state of the write operation.</returns>
        Task WriteAsync(byte[] data);

        /// <summary>Asynchronously writes the given bytes to the response body stream.</summary>
        /// <param name="data"> The response data.</param>
        /// <param name="token">A token used to indicate cancellation.</param>
        /// <returns>A Task tracking the state of the write operation.</returns>
        Task WriteAsync(byte[] data, CancellationToken token);

        /// <summary>Asynchronously writes the given bytes to the response body stream.</summary>
        /// <param name="data">  The response data.</param>
        /// <param name="offset">The zero-based byte offset in the <paramref name="data" /> parameter at which to begin
        ///                      copying bytes.</param>
        /// <param name="count"> The number of bytes to write.</param>
        /// <param name="token"> A token used to indicate cancellation.</param>
        /// <returns>A Task tracking the state of the write operation.</returns>
        Task WriteAsync(byte[] data, int offset, int count, CancellationToken token);
    }
}
