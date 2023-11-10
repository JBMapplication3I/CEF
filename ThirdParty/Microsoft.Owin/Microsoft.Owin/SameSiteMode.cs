// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.SameSiteMode
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    /// <summary>Indicates if the client should include a cookie on "same-site" or "cross-site" requests.</summary>
    public enum SameSiteMode
    {
        /// <summary>
        ///     Indicates the client should send the cookie with every requests coming from any origin.
        /// </summary>
        None,

        /// <summary>
        ///     Indicates the client should send the cookie with "same-site" requests, and with "cross-site" top-level navigations.
        /// </summary>
        Lax,

        /// <summary>
        ///     Indicates the client should only send the cookie with "same-site" requests.
        /// </summary>
        Strict,
    }
}
