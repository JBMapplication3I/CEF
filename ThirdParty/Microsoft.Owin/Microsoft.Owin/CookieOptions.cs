// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.CookieOptions
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System;

    /// <summary>Options used to create a new cookie.</summary>
    public class CookieOptions
    {
        /// <summary>Creates a default cookie with a path of '/'.</summary>
        public CookieOptions()
        {
            Path = "/";
        }

        /// <summary>Gets or sets the domain to associate the cookie with.</summary>
        /// <value>The domain to associate the cookie with.</value>
        public string Domain { get; set; }

        /// <summary>Gets or sets the expiration date and time for the cookie.</summary>
        /// <value>The expiration date and time for the cookie.</value>
        public DateTime? Expires { get; set; }

        /// <summary>Gets or sets a value that indicates whether a cookie is accessible by client-side script.</summary>
        /// <value>true if a cookie is accessible by client-side script; otherwise, false.</value>
        public bool HttpOnly { get; set; }

        /// <summary>Gets or sets the cookie path.</summary>
        /// <value>The cookie path.</value>
        public string Path { get; set; }

        /// <summary>Gets or sets a value that indicates on which requests client should or should not send cookie back
        /// to the server. Set to null to do not include SameSite attribute at all.</summary>
        /// <value>SameSite attribute value or null if attribute must not be set.</value>
        public SameSiteMode? SameSite { get; set; }

        /// <summary>Gets or sets a value that indicates whether to transmit the cookie using Secure Sockets Layer
        /// (SSL)—that is, over HTTPS only.</summary>
        /// <value>true to transmit the cookie only over an SSL connection (HTTPS); otherwise, false.</value>
        public bool Secure { get; set; }
    }
}
