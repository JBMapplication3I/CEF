// <copyright file="CookieResponseSignOutContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cookie response sign out context class</summary>
namespace Microsoft.Owin.Security.Cookies
{
    using Provider;

    /// <summary>Context object passed to the ICookieAuthenticationProvider method ResponseSignOut.</summary>
    /// <seealso cref="BaseContext{CookieAuthenticationOptions}"/>
    /// <seealso cref="BaseContext{CookieAuthenticationOptions}"/>
    public class CookieResponseSignOutContext : BaseContext<CookieAuthenticationOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="CookieResponseSignOutContext" /> class.</summary>
        /// <param name="context">      .</param>
        /// <param name="options">      .</param>
        /// <param name="cookieOptions">.</param>
        public CookieResponseSignOutContext(
            IOwinContext context,
            CookieAuthenticationOptions options,
            CookieOptions cookieOptions) : base(context, options)
        {
            CookieOptions = cookieOptions;
        }

        /// <summary>The options for creating the outgoing cookie. May be replace or altered during the ResponseSignOut
        /// call.</summary>
        /// <value>Options that control the cookie.</value>
        public CookieOptions CookieOptions
        {
            get;
            set;
        }
    }
}
