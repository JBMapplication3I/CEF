﻿#if !NETSTANDARD1_6

// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Web;

namespace ServiceStack.Html.AntiXsrf
{
    // Saves anti-XSRF tokens split between HttpRequest.Cookies and HttpRequest.Form
    internal sealed class AntiForgeryTokenStore : ITokenStore
    {
        private readonly IAntiForgeryConfig _config;
        private readonly IAntiForgeryTokenSerializer _serializer;

        internal AntiForgeryTokenStore(IAntiForgeryConfig config, IAntiForgeryTokenSerializer serializer)
        {
            _config = config;
            _serializer = serializer;
        }

        public AntiForgeryToken GetCookieToken(HttpContextBase httpContext)
        {
            var cookie = httpContext.Request.Cookies[_config.CookieName];
            if (string.IsNullOrEmpty(cookie?.Value))
            {
                // did not exist
                return null;
            }

            return _serializer.Deserialize(cookie.Value);
        }

        public AntiForgeryToken GetFormToken(HttpContextBase httpContext)
        {
            var value = httpContext.Request.Form[_config.FormFieldName];
            if (string.IsNullOrEmpty(value))
            {
                // did not exist
                return null;
            }

            return _serializer.Deserialize(value);
        }

        public void SaveCookieToken(HttpContextBase httpContext, AntiForgeryToken token)
        {
            var serializedToken = _serializer.Serialize(token);
            var newCookie = new HttpCookie(_config.CookieName, serializedToken)
            {
                HttpOnly = true
            };

            // Note: don't use "newCookie.Secure = _config.RequireSSL;" since the default
            // value of newCookie.Secure is automatically populated from the <httpCookies>
            // config element.
            if (_config.RequireSSL)
            {
                newCookie.Secure = true;
            }

            httpContext.Response.Cookies.Set(newCookie);
        }
    }
}

#endif
