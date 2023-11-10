// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.IOwinContext
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System.Collections.Generic;
    using System.IO;
    using Security;

    /// <summary>This wraps OWIN environment dictionary and provides strongly typed accessors.</summary>
    public interface IOwinContext
    {
        /// <summary>Gets the Authentication middleware functionality available on the current request.</summary>
        /// <value>The authentication middleware functionality available on the current request.</value>
        IAuthenticationManager Authentication { get; }

        /// <summary>Gets the OWIN environment.</summary>
        /// <value>The OWIN environment.</value>
        IDictionary<string, object> Environment { get; }

        /// <summary>Gets a wrapper exposing request specific properties.</summary>
        /// <value>A wrapper exposing request specific properties.</value>
        IOwinRequest Request { get; }

        /// <summary>Gets a wrapper exposing response specific properties.</summary>
        /// <value>A wrapper exposing response specific properties.</value>
        IOwinResponse Response { get; }

        /// <summary>Gets or sets the host.TraceOutput environment value.</summary>
        /// <value>The host.TraceOutput TextWriter.</value>
        TextWriter TraceOutput { get; set; }

        /// <summary>Gets a value from the OWIN environment, or returns default(T) if not present.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key or the default(T) if not present.</returns>
        T Get<T>(string key);

        /// <summary>Sets the given key and value in the OWIN environment.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">  The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>This instance.</returns>
        IOwinContext Set<T>(string key, T value);
    }
}
