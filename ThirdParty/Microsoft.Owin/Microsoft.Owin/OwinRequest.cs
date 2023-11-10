// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.OwinRequest
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Infrastructure;

    /// <summary>This wraps OWIN environment dictionary and provides strongly typed accessors.</summary>
    /// <seealso cref="IOwinRequest"/>
    /// <seealso cref="IOwinRequest"/>
    public class OwinRequest : IOwinRequest
    {
        /// <summary>Create a new context with only request and response header collections.</summary>
        public OwinRequest()
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["owin.RequestHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase),
                ["owin.ResponseHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase),
            };
            Environment = dictionary;
        }

        /// <summary>Create a new environment wrapper exposing request properties.</summary>
        /// <param name="environment">OWIN environment dictionary which stores state information about the request,
        ///                           response and relevant server state.</param>
        public OwinRequest(IDictionary<string, object> environment)
        {
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        /// <summary>Gets or sets or set the Accept header.</summary>
        /// <value>The Accept header.</value>
        public virtual string Accept
        {
            get => OwinHelpers.GetHeader(RawHeaders, nameof(Accept));
            set => OwinHelpers.SetHeader(RawHeaders, nameof(Accept), value);
        }

        /// <summary>Gets or sets or set the owin.RequestBody Stream.</summary>
        /// <value>The owin.RequestBody Stream.</value>
        public virtual Stream Body
        {
            get => Get<Stream>("owin.RequestBody");
            set => Set("owin.RequestBody", value);
        }

        /// <summary>Gets or sets the Cache-Control header.</summary>
        /// <value>The Cache-Control header.</value>
        public virtual string CacheControl
        {
            get => OwinHelpers.GetHeader(RawHeaders, "Cache-Control");
            set => OwinHelpers.SetHeader(RawHeaders, "Cache-Control", value);
        }

        /// <summary>Gets or sets the cancellation token for the request.</summary>
        /// <value>The cancellation token for the request.</value>
        public virtual CancellationToken CallCancelled
        {
            get => Get<CancellationToken>("owin.CallCancelled");
            set => Set("owin.CallCancelled", value);
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

        /// <summary>Gets the collection of Cookies for this request.</summary>
        /// <value>The collection of Cookies for this request.</value>
        public RequestCookieCollection Cookies => new(OwinHelpers.GetCookies(this));

        /// <summary>Gets the OWIN environment.</summary>
        /// <value>The OWIN environment.</value>
        public virtual IDictionary<string, object> Environment { get; }

        /// <summary>Gets the request headers.</summary>
        /// <value>The request headers.</value>
        public virtual IHeaderDictionary Headers => new HeaderDictionary(RawHeaders);

        /// <summary>Gets or sets or set the Host header. May include the port.</summary>
        /// <value>The host.</value>
        public virtual HostString Host
        {
            get => new(OwinHelpers.GetHost(this));
            set => OwinHelpers.SetHeader(RawHeaders, nameof(Host), value.Value);
        }

        /// <summary>Returns true if the owin.RequestScheme is https.</summary>
        /// <value>true if this request is using https; otherwise, false.</value>
        public virtual bool IsSecure => string.Equals(Scheme, "HTTPS", StringComparison.OrdinalIgnoreCase);

        /// <summary>Gets or sets or set the server.LocalIpAddress.</summary>
        /// <value>The server.LocalIpAddress.</value>
        public virtual string LocalIpAddress
        {
            get => Get<string>("server.LocalIpAddress");
            set => Set("server.LocalIpAddress", value);
        }

        /// <summary>Gets or sets or set the server.LocalPort.</summary>
        /// <value>The server.LocalPort.</value>
        public virtual int? LocalPort
        {
            get => int.TryParse(LocalPortString, out var result) ? result : new int?();
            set
            {
                if (value.HasValue)
                {
                    LocalPortString = value.Value.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    Environment.Remove("server.LocalPort");
                }
            }
        }

        /// <summary>Gets or sets the Media-Type header.</summary>
        /// <value>The Media-Type header.</value>
        public virtual string MediaType
        {
            get => OwinHelpers.GetHeader(RawHeaders, "Media-Type");
            set => OwinHelpers.SetHeader(RawHeaders, "Media-Type", value);
        }

        /// <summary>Gets or sets or set the HTTP method.</summary>
        /// <value>The HTTP method.</value>
        public virtual string Method
        {
            get => Get<string>("owin.RequestMethod");
            set => Set("owin.RequestMethod", value);
        }

        /// <summary>Gets or sets or set the request path from owin.RequestPath.</summary>
        /// <value>The request path from owin.RequestPath.</value>
        public virtual PathString Path
        {
            get => new(Get<string>("owin.RequestPath"));
            set => Set("owin.RequestPath", value.Value);
        }

        /// <summary>Gets or sets or set the owin.RequestPathBase.</summary>
        /// <value>The owin.RequestPathBase.</value>
        public virtual PathString PathBase
        {
            get => new(Get<string>("owin.RequestPathBase"));
            set => Set("owin.RequestPathBase", value.Value);
        }

        /// <summary>Gets or sets or set the owin.RequestProtocol.</summary>
        /// <value>The owin.RequestProtocol.</value>
        public virtual string Protocol
        {
            get => Get<string>("owin.RequestProtocol");
            set => Set("owin.RequestProtocol", value);
        }

        /// <summary>Gets the query value collection parsed from owin.RequestQueryString.</summary>
        /// <value>The query value collection parsed from owin.RequestQueryString.</value>
        public virtual IReadableStringCollection Query => new ReadableStringCollection(OwinHelpers.GetQuery(this));

        /// <summary>Gets or sets or set the query string from owin.RequestQueryString.</summary>
        /// <value>The query string from owin.RequestQueryString.</value>
        public virtual QueryString QueryString
        {
            get => new(Get<string>("owin.RequestQueryString"));
            set => Set("owin.RequestQueryString", value.Value);
        }

        /// <summary>Gets or sets or set the server.RemoteIpAddress.</summary>
        /// <value>The server.RemoteIpAddress.</value>
        public virtual string RemoteIpAddress
        {
            get => Get<string>("server.RemoteIpAddress");
            set => Set("server.RemoteIpAddress", value);
        }

        /// <summary>Gets or sets or set the server.RemotePort.</summary>
        /// <value>The server.RemotePort.</value>
        public virtual int? RemotePort
        {
            get => int.TryParse(RemotePortString, out var result) ? result : new int?();
            set
            {
                if (value.HasValue)
                {
                    RemotePortString = value.Value.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    Environment.Remove("server.RemotePort");
                }
            }
        }

        /// <summary>Gets or sets or set the HTTP request scheme from owin.RequestScheme.</summary>
        /// <value>The HTTP request scheme from owin.RequestScheme.</value>
        public virtual string Scheme
        {
            get => Get<string>("owin.RequestScheme");
            set => Set("owin.RequestScheme", value);
        }

        /// <summary>Gets the uniform resource identifier (URI) associated with the request.</summary>
        /// <value>The uniform resource identifier (URI) associated with the request.</value>
        public virtual Uri Uri => new(Scheme + Uri.SchemeDelimiter + Host + PathBase + Path + QueryString);

        /// <summary>Gets or sets or set the server.User.</summary>
        /// <value>The server.User.</value>
        public virtual IPrincipal User
        {
            get => Get<IPrincipal>("server.User");
            set => Set("server.User", value);
        }

        /// <summary>Gets or sets the local port string.</summary>
        /// <value>The local port string.</value>
        private string LocalPortString
        {
            get => Get<string>("server.LocalPort");
            set => Set("server.LocalPort", value);
        }

        /// <summary>Gets the raw headers.</summary>
        /// <value>The raw headers.</value>
        private IDictionary<string, string[]> RawHeaders => Get<IDictionary<string, string[]>>("owin.RequestHeaders");

        /// <summary>Gets or sets the remote port string.</summary>
        /// <value>The remote port string.</value>
        private string RemotePortString
        {
            get => Get<string>("server.RemotePort");
            set => Set("server.RemotePort", value);
        }

        /// <summary>Gets a value from the OWIN environment, or returns default(T) if not present.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key or the default(T) if not present.</returns>
        public virtual T Get<T>(string key)
        {
            return !Environment.TryGetValue(key, out var obj) ? default : (T)obj;
        }

        /// <summary>Asynchronously reads and parses the request body as a form.</summary>
        /// <returns>The parsed form data.</returns>
        public async Task<IFormCollection> ReadFormAsync()
        {
            var form = Get<IFormCollection>("Microsoft.Owin.Form#collection");
            if (form == null)
            {
                string endAsync;
                using (var reader = new StreamReader(Body, Encoding.UTF8, true, 4096, true))
                {
                    endAsync = await reader.ReadToEndAsync();
                }
                form = OwinHelpers.GetForm(endAsync);
                Set("Microsoft.Owin.Form#collection", form);
            }
            return form;
        }

        /// <summary>Sets the given key and value in the OWIN environment.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">  The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>This instance.</returns>
        public virtual IOwinRequest Set<T>(string key, T value)
        {
            Environment[key] = value;
            return this;
        }
    }
}
