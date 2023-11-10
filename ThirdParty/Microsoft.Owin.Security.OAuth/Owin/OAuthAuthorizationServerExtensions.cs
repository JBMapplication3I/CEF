// <copyright file="OAuthAuthorizationServerExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication authorization server extensions class</summary>
namespace Owin
{
    using System;
    using Microsoft.Owin.Security.OAuth;

    /// <summary>Extension methods to add Authorization Server capabilities to an OWIN pipeline.</summary>
    public static class OAuthAuthorizationServerExtensions
    {
        /// <summary>Adds OAuth2 Authorization Server capabilities to an OWIN web application. This middleware performs
        /// the request processing for the Authorize and Token endpoints defined by the OAuth2 specification. See also
        /// http://tools.ietf.org/html/rfc6749.</summary>
        /// <param name="app">    The web application builder.</param>
        /// <param name="options">Options which control the behavior of the Authorization Server.</param>
        /// <returns>The application builder.</returns>
        public static IAppBuilder UseOAuthAuthorizationServer(
            this IAppBuilder app,
            OAuthAuthorizationServerOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.Use(typeof(OAuthAuthorizationServerMiddleware), app, options);
            return app;
        }
    }
}
