// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.OwinContext
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Security;

    /// <summary>This wraps OWIN environment dictionary and provides strongly typed accessors.</summary>
    /// <seealso cref="IOwinContext"/>
    /// <seealso cref="IOwinContext"/>
    public class OwinContext : IOwinContext
    {
        /// <summary>Create a new context with only request and response header collections.</summary>
        public OwinContext()
        {
            IDictionary<string, object> environment = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["owin.RequestHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase),
                ["owin.ResponseHeaders"] = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase),
            };
            Environment = environment;
            Request = new OwinRequest(environment);
            Response = new OwinResponse(environment);
        }

        /// <summary>Create a new wrapper.</summary>
        /// <param name="environment">OWIN environment dictionary which stores state information about the request,
        ///                           response and relevant server state.</param>
        public OwinContext(IDictionary<string, object> environment)
        {
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            Request = new OwinRequest(environment);
            Response = new OwinResponse(environment);
        }

        /// <summary>Gets the Authentication middleware functionality available on the current request.</summary>
        /// <value>The authentication middleware functionality available on the current request.</value>
        public IAuthenticationManager Authentication => new AuthenticationManager(this);

        /// <summary>Gets the OWIN environment.</summary>
        /// <value>The OWIN environment.</value>
        public virtual IDictionary<string, object> Environment { get; }

        /// <summary>Gets a wrapper exposing request specific properties.</summary>
        /// <value>A wrapper exposing request specific properties.</value>
        public virtual IOwinRequest Request { get; }

        /// <summary>Gets a wrapper exposing response specific properties.</summary>
        /// <value>A wrapper exposing response specific properties.</value>
        public virtual IOwinResponse Response { get; }

        /// <summary>Gets or sets the host.TraceOutput environment value.</summary>
        /// <value>The host.TraceOutput TextWriter.</value>
        public virtual TextWriter TraceOutput
        {
            get => Get<TextWriter>("host.TraceOutput");
            set => Set("host.TraceOutput", value);
        }

        /// <summary>Gets a value from the OWIN environment, or returns default(T) if not present.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key or the default(T) if not present.</returns>
        public virtual T Get<T>(string key)
        {
            if (Environment.TryGetValue(key, out var obj))
            {
                return (T)obj;
            }
            return default;
        }

        /// <summary>Sets the given key and value in the OWIN environment.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">  The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>This instance.</returns>
        public virtual IOwinContext Set<T>(string key, T value)
        {
            Environment[key] = value;
            return this;
        }
    }
}
