// <copyright file="OwinResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the owin response class</summary>
namespace Microsoft.Owin
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Infrastructure;

    /// <summary>This wraps OWIN environment dictionary and provides strongly typed accessors.</summary>
    /// <seealso cref="IOwinResponse"/>
    /// <seealso cref="IOwinResponse"/>
    public class OwinResponse : IOwinResponse
    {
        /// <summary>Create a new context with only request and response header collections.</summary>
        public OwinResponse()
        {
            IDictionary<string, object> strs = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["owin.RequestHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase),
                ["owin.ResponseHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase),
            };
            Environment = strs;
        }

        /// <summary>Creates a new environment wrapper exposing response properties.</summary>
        /// <param name="environment">OWIN environment dictionary which stores state information about the request,
        ///                           response and relevant server state.</param>
        public OwinResponse(IDictionary<string, object> environment)
        {
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        /// <summary>Gets or sets the owin.ResponseBody Stream.</summary>
        /// <value>The owin.ResponseBody Stream.</value>
        public virtual Stream Body
        {
            get => Get<Stream>("owin.ResponseBody");
            set => Set("owin.ResponseBody", value);
        }

        /// <summary>Gets or sets the Content-Length header.</summary>
        /// <value>The Content-Length header.</value>
        public virtual long? ContentLength
        {
            get
            {
                if (long.TryParse(OwinHelpers.GetHeader(RawHeaders, "Content-Length"), out var num))
                {
                    return num;
                }
                return null;
            }

            set
            {
                if (!value.HasValue)
                {
                    RawHeaders.Remove("Content-Length");
                    return;
                }
                var rawHeaders = RawHeaders;
                var num = value.Value;
                OwinHelpers.SetHeader(rawHeaders, "Content-Length", num.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>Gets or sets the Content-Type header.</summary>
        /// <value>The Content-Type header.</value>
        public virtual string ContentType
        {
            get => OwinHelpers.GetHeader(RawHeaders, "Content-Type");
            set => OwinHelpers.SetHeader(RawHeaders, "Content-Type", value);
        }

        /// <summary>Gets the request context.</summary>
        /// <value>The request context.</value>
        public virtual IOwinContext Context => new OwinContext(Environment);

        /// <summary>Gets a collection used to manipulate the Set-Cookie header.</summary>
        /// <value>A collection used to manipulate the Set-Cookie header.</value>
        public virtual ResponseCookieCollection Cookies => new(Headers);

        /// <summary>Gets or sets the OWIN environment.</summary>
        /// <value>The OWIN environment.</value>
        public virtual IDictionary<string, object> Environment { get; set; }

        /// <summary>Gets or sets the E-Tag header.</summary>
        /// <value>The E-Tag header.</value>
        public virtual string ETag
        {
            get => OwinHelpers.GetHeader(RawHeaders, "ETag");
            set => OwinHelpers.SetHeader(RawHeaders, "ETag", value);
        }

        /// <summary>Gets or sets the Expires header.</summary>
        /// <value>The Expires header.</value>
        public virtual DateTimeOffset? Expires
        {
            get
            {
                if (DateTimeOffset.TryParse(
                    OwinHelpers.GetHeader(RawHeaders, "Expires"),
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal,
                    out var dateTimeOffset))
                {
                    return dateTimeOffset;
                }
                return null;
            }

            set
            {
                if (!value.HasValue)
                {
                    RawHeaders.Remove("Expires");
                    return;
                }
                var rawHeaders = RawHeaders;
                var dateTimeOffset = value.Value;
                OwinHelpers.SetHeader(
                    rawHeaders,
                    "Expires",
                    dateTimeOffset.ToString("r", CultureInfo.InvariantCulture));
            }
        }

        /// <summary>Gets the response header collection.</summary>
        /// <value>The response header collection.</value>
        public virtual IHeaderDictionary Headers => new HeaderDictionary(RawHeaders);

        /// <summary>Gets or sets the owin.ResponseProtocol.</summary>
        /// <value>The owin.ResponseProtocol.</value>
        public virtual string Protocol
        {
            get => Get<string>("owin.ResponseProtocol");
            set => Set("owin.ResponseProtocol", value);
        }

        /// <summary>Gets or sets the the optional owin.ResponseReasonPhrase.</summary>
        /// <value>The the optional owin.ResponseReasonPhrase.</value>
        public virtual string ReasonPhrase
        {
            get => Get<string>("owin.ResponseReasonPhrase");
            set => Set("owin.ResponseReasonPhrase", value);
        }

        /// <summary>Gets or sets the optional owin.ResponseStatusCode.</summary>
        /// <value>The optional owin.ResponseStatusCode, or 200 if not set.</value>
        public virtual int StatusCode
        {
            get => Get("owin.ResponseStatusCode", 200);
            set => Set("owin.ResponseStatusCode", value);
        }

        /// <summary>Gets the raw headers.</summary>
        /// <value>The raw headers.</value>
        private IDictionary<string, string[]> RawHeaders => Get<IDictionary<string, string[]>>("owin.ResponseHeaders");

        /// <summary>Gets a value from the OWIN environment, or returns default(T) if not present.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key or the default(T) if not present.</returns>
        public virtual T Get<T>(string key)
        {
            return Get(key, default(T));
        }

        /// <summary>Registers for an event that fires when the response headers are sent.</summary>
        /// <param name="callback">The callback method.</param>
        /// <param name="state">   The callback state.</param>
        public virtual void OnSendingHeaders(Action<object> callback, object state)
        {
            var action = Get<Action<Action<object>, object>>("server.OnSendingHeaders");
            if (action == null)
            {
                throw new NotSupportedException(Resources.Exception_MissingOnSendingHeaders);
            }
            action(callback, state);
        }

        /// <summary>Sets a 302 response status code and the Location header.</summary>
        /// <param name="location">The location where to redirect the client.</param>
        public virtual void Redirect(string location)
        {
            StatusCode = 302;
            OwinHelpers.SetHeader(RawHeaders, "Location", location);
        }

        /// <summary>Sets the given key and value in the OWIN environment.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">  The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>This instance.</returns>
        public virtual IOwinResponse Set<T>(string key, T value)
        {
            Environment[key] = value;
            return this;
        }

        /// <summary>Writes the given text to the response body stream using UTF-8.</summary>
        /// <param name="text">The response data.</param>
        public virtual void Write(string text)
        {
            Write(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>Writes the given bytes to the response body stream.</summary>
        /// <param name="data">The response data.</param>
        public virtual void Write(byte[] data)
        {
            Write(data, 0, data == null ? 0 : data.Length);
        }

        /// <summary>Writes the given bytes to the response body stream.</summary>
        /// <param name="data">  The response data.</param>
        /// <param name="offset">The zero-based byte offset in the <paramref name="data" /> parameter at which to begin
        ///                      copying bytes.</param>
        /// <param name="count"> The number of bytes to write.</param>
        public virtual void Write(byte[] data, int offset, int count)
        {
            Body.Write(data, offset, count);
        }

        /// <summary>Asynchronously writes the given text to the response body stream using UTF-8.</summary>
        /// <param name="text">The response data.</param>
        /// <returns>A Task tracking the state of the write operation.</returns>
        public virtual Task WriteAsync(string text)
        {
            return WriteAsync(text, CancellationToken.None);
        }

        /// <summary>Asynchronously writes the given text to the response body stream using UTF-8.</summary>
        /// <param name="text"> The response data.</param>
        /// <param name="token">A token used to indicate cancellation.</param>
        /// <returns>A Task tracking the state of the write operation.</returns>
        public virtual Task WriteAsync(string text, CancellationToken token)
        {
            return WriteAsync(Encoding.UTF8.GetBytes(text), token);
        }

        /// <summary>Asynchronously writes the given bytes to the response body stream.</summary>
        /// <param name="data">The response data.</param>
        /// <returns>A Task tracking the state of the write operation.</returns>
        public virtual Task WriteAsync(byte[] data)
        {
            return WriteAsync(data, CancellationToken.None);
        }

        /// <summary>Asynchronously writes the given bytes to the response body stream.</summary>
        /// <param name="data"> The response data.</param>
        /// <param name="token">A token used to indicate cancellation.</param>
        /// <returns>A Task tracking the state of the write operation.</returns>
        public virtual Task WriteAsync(byte[] data, CancellationToken token)
        {
            return WriteAsync(data, 0, data == null ? 0 : data.Length, token);
        }

        /// <summary>Asynchronously writes the given bytes to the response body stream.</summary>
        /// <param name="data">  The response data.</param>
        /// <param name="offset">The zero-based byte offset in the <paramref name="data" /> parameter at which to begin
        ///                      copying bytes.</param>
        /// <param name="count"> The number of bytes to write.</param>
        /// <param name="token"> A token used to indicate cancellation.</param>
        /// <returns>A Task tracking the state of the write operation.</returns>
        public virtual Task WriteAsync(byte[] data, int offset, int count, CancellationToken token)
        {
            return Body.WriteAsync(data, offset, count, token);
        }

        /// <summary>Gets.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="key">     The key.</param>
        /// <param name="fallback">The fallback.</param>
        /// <returns>A T.</returns>
        private T Get<T>(string key, T fallback)
        {
            if (!Environment.TryGetValue(key, out var obj))
            {
                return fallback;
            }
            return (T)obj;
        }
    }
}
