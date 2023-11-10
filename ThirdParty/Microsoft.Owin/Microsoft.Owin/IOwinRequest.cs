// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.IOwinRequest
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>This wraps OWIN environment dictionary and provides strongly typed accessors.</summary>
    public interface IOwinRequest
    {
        /// <summary>Gets or sets or set the Accept header.</summary>
        /// <value>The Accept header.</value>
        string Accept { get; set; }

        /// <summary>Gets or sets or set the owin.RequestBody Stream.</summary>
        /// <value>The owin.RequestBody Stream.</value>
        Stream Body { get; set; }

        /// <summary>Gets or sets the Cache-Control header.</summary>
        /// <value>The Cache-Control header.</value>
        string CacheControl { get; set; }

        /// <summary>Gets or sets the cancellation token for the request.</summary>
        /// <value>The cancellation token for the request.</value>
        CancellationToken CallCancelled { get; set; }

        /// <summary>Gets or sets the Content-Type header.</summary>
        /// <value>The Content-Type header.</value>
        string ContentType { get; set; }

        /// <summary>Gets the request context.</summary>
        /// <value>The request context.</value>
        IOwinContext Context { get; }

        /// <summary>Gets the collection of Cookies for this request.</summary>
        /// <value>The collection of Cookies for this request.</value>
        RequestCookieCollection Cookies { get; }

        /// <summary>Gets the OWIN environment.</summary>
        /// <value>The OWIN environment.</value>
        IDictionary<string, object> Environment { get; }

        /// <summary>Gets the request headers.</summary>
        /// <value>The request headers.</value>
        IHeaderDictionary Headers { get; }

        /// <summary>Gets or sets or set the Host header. May include the port.</summary>
        /// <value>The host.</value>
        HostString Host { get; set; }

        /// <summary>Returns true if the owin.RequestScheme is https.</summary>
        /// <value>true if this request is using https; otherwise, false.</value>
        bool IsSecure { get; }

        /// <summary>Gets or sets or set the server.LocalIpAddress.</summary>
        /// <value>The server.LocalIpAddress.</value>
        string LocalIpAddress { get; set; }

        /// <summary>Gets or sets or set the server.LocalPort.</summary>
        /// <value>The server.LocalPort.</value>
        int? LocalPort { get; set; }

        /// <summary>Gets or sets the Media-Type header.</summary>
        /// <value>The Media-Type header.</value>
        string MediaType { get; set; }

        /// <summary>Gets or sets or set the HTTP method.</summary>
        /// <value>The HTTP method.</value>
        string Method { get; set; }

        /// <summary>Gets or sets or set the request path from owin.RequestPath.</summary>
        /// <value>The request path from owin.RequestPath.</value>
        PathString Path { get; set; }

        /// <summary>Gets or sets or set the owin.RequestPathBase.</summary>
        /// <value>The owin.RequestPathBase.</value>
        PathString PathBase { get; set; }

        /// <summary>Gets or sets or set the owin.RequestProtocol.</summary>
        /// <value>The owin.RequestProtocol.</value>
        string Protocol { get; set; }

        /// <summary>Gets the query value collection parsed from owin.RequestQueryString.</summary>
        /// <value>The query value collection parsed from owin.RequestQueryString.</value>
        IReadableStringCollection Query { get; }

        /// <summary>Gets or sets or set the query string from owin.RequestQueryString.</summary>
        /// <value>The query string from owin.RequestQueryString.</value>
        QueryString QueryString { get; set; }

        /// <summary>Gets or sets or set the server.RemoteIpAddress.</summary>
        /// <value>The server.RemoteIpAddress.</value>
        string RemoteIpAddress { get; set; }

        /// <summary>Gets or sets or set the server.RemotePort.</summary>
        /// <value>The server.RemotePort.</value>
        int? RemotePort { get; set; }

        /// <summary>Gets or sets or set the HTTP request scheme from owin.RequestScheme.</summary>
        /// <value>The HTTP request scheme from owin.RequestScheme.</value>
        string Scheme { get; set; }

        /// <summary>Gets the uniform resource identifier (URI) associated with the request.</summary>
        /// <value>The uniform resource identifier (URI) associated with the request.</value>
        Uri Uri { get; }

        /// <summary>Gets or sets or set the server.User.</summary>
        /// <value>The server.User.</value>
        IPrincipal User { get; set; }

        /// <summary>Gets a value from the OWIN environment, or returns default(T) if not present.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key or the default(T) if not present.</returns>
        T Get<T>(string key);

        /// <summary>Asynchronously reads and parses the request body as a form.</summary>
        /// <returns>The parsed form data.</returns>
        Task<IFormCollection> ReadFormAsync();

        /// <summary>Sets the given key and value in the OWIN environment.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">  The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>This instance.</returns>
        IOwinRequest Set<T>(string key, T value);
    }
}
