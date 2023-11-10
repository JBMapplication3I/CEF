// <copyright file="CookieApplyRedirectContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cookie apply redirect context class</summary>
namespace Microsoft.Owin.Security.Cookies
{
    using Provider;

    /// <summary>Context passed when a Challenge, SignIn, or SignOut causes a redirect in the cookie middleware.</summary>
    /// <seealso cref="BaseContext{CookieAuthenticationOptions}"/>
    /// <seealso cref="BaseContext{CookieAuthenticationOptions}"/>
    public class CookieApplyRedirectContext : BaseContext<CookieAuthenticationOptions>
    {
        /// <summary>Creates a new context object.</summary>
        /// <param name="context">    The OWIN request context.</param>
        /// <param name="options">    The cookie middleware options.</param>
        /// <param name="redirectUri">The initial redirect URI.</param>
        public CookieApplyRedirectContext(
            IOwinContext context,
            CookieAuthenticationOptions options,
            string redirectUri) : base(context, options)
        {
            RedirectUri = redirectUri;
        }

        /// <summary>Gets or sets or Sets the URI used for the redirect operation.</summary>
        /// <value>The redirect URI.</value>
        public string RedirectUri
        {
            get;
            set;
        }
    }
}
