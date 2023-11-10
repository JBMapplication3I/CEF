// <copyright file="CookieManager.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cookie manager class</summary>
namespace Microsoft.Owin.Infrastructure
{
    using System;

    /// <summary>An implementation of ICookieManager that writes directly to IOwinContext.Response.Cookies.</summary>
    /// <seealso cref="ICookieManager"/>
    /// <seealso cref="ICookieManager"/>
    public class CookieManager : ICookieManager
    {
        /// <summary>Appends a new response cookie to the Set-Cookie header.</summary>
        /// <param name="context">.</param>
        /// <param name="key">    .</param>
        /// <param name="value">  .</param>
        /// <param name="options">.</param>
        public void AppendResponseCookie(IOwinContext context, string key, string value, CookieOptions options)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            context.Response.Cookies.Append(key, value, options);
        }

        /// <summary>Deletes the cookie with the given key by appending an expired cookie.</summary>
        /// <param name="context">.</param>
        /// <param name="key">    .</param>
        /// <param name="options">.</param>
        public void DeleteCookie(IOwinContext context, string key, CookieOptions options)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            context.Response.Cookies.Delete(key, options);
        }

        /// <summary>Read a cookie with the given name from the request.</summary>
        /// <param name="context">.</param>
        /// <param name="key">    .</param>
        /// <returns>The request cookie.</returns>
        public string GetRequestCookie(IOwinContext context, string key)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return context.Request.Cookies[key];
        }
    }
}
