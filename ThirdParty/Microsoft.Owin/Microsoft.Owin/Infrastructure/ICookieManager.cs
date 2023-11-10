// <copyright file="ICookieManager.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICookieManager interface</summary>
namespace Microsoft.Owin.Infrastructure
{
    /// <summary>An abstraction for reading request cookies and writing response cookies.</summary>
    public interface ICookieManager
    {
        /// <summary>Append a cookie to the response.</summary>
        /// <param name="context">.</param>
        /// <param name="key">    .</param>
        /// <param name="value">  .</param>
        /// <param name="options">.</param>
        void AppendResponseCookie(IOwinContext context, string key, string value, CookieOptions options);

        /// <summary>Append a delete cookie to the response.</summary>
        /// <param name="context">.</param>
        /// <param name="key">    .</param>
        /// <param name="options">.</param>
        void DeleteCookie(IOwinContext context, string key, CookieOptions options);

        /// <summary>Read a cookie with the given name from the request.</summary>
        /// <param name="context">.</param>
        /// <param name="key">    .</param>
        /// <returns>The request cookie.</returns>
        string GetRequestCookie(IOwinContext context, string key);
    }
}
