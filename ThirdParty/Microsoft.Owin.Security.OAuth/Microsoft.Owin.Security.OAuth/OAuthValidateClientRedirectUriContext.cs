// <copyright file="OAuthValidateClientRedirectUriContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication validate client redirect URI context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System;

    /// <summary>Contains data about the OAuth client redirect URI.</summary>
    /// <seealso cref="BaseValidatingClientContext"/>
    public class OAuthValidateClientRedirectUriContext : BaseValidatingClientContext
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthValidateClientRedirectUriContext" /> class.</summary>
        /// <param name="context">    .</param>
        /// <param name="options">    .</param>
        /// <param name="clientId">   .</param>
        /// <param name="redirectUri">.</param>
        public OAuthValidateClientRedirectUriContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            string clientId,
            string redirectUri) : base(context, options, clientId)
        {
            RedirectUri = redirectUri;
        }

        /// <summary>Gets the client redirect URI.</summary>
        /// <value>The redirect URI.</value>
        public string RedirectUri
        {
            get;
            private set;
        }

        /// <summary>Marks this context as validated by the application. IsValidated becomes true and HasError becomes
        /// false as a result of calling.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public override bool Validated()
        {
            if (string.IsNullOrEmpty(RedirectUri))
            {
                return false;
            }
            return base.Validated();
        }

        /// <summary>Checks the redirect URI to determine whether it equals
        /// <see cref="P:Microsoft.Owin.Security.OAuth.OAuthValidateClientRedirectUriContext.RedirectUri" />.</summary>
        /// <param name="redirectUri">.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool Validated(string redirectUri)
        {
            if (redirectUri == null)
            {
                throw new ArgumentNullException(nameof(redirectUri));
            }
            if (!string.IsNullOrEmpty(RedirectUri)
                && !string.Equals(RedirectUri, redirectUri, StringComparison.Ordinal))
            {
                return false;
            }
            RedirectUri = redirectUri;
            return Validated();
        }
    }
}
