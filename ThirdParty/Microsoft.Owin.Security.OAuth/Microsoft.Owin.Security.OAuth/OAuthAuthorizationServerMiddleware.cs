// <copyright file="OAuthAuthorizationServerMiddleware.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication authorization server middleware class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using DataHandler;
    using DataProtection;
    using Infrastructure;
    using Logging;

    /// <summary>Authorization Server middleware component which is added to an OWIN pipeline. This class is not
    /// created by application code directly, instead it is added by calling the the IAppBuilder
    /// UseOAuthAuthorizationServer extension method.</summary>
    /// <seealso cref="AuthenticationMiddleware{OAuthAuthorizationServerOptions}"/>
    public class OAuthAuthorizationServerMiddleware : AuthenticationMiddleware<OAuthAuthorizationServerOptions>
    {
        /// <summary>The logger.</summary>
        private readonly ILogger _logger;

        /// <summary>Authorization Server middleware component which is added to an OWIN pipeline. This constructor is
        /// not called by application code directly, instead it is added by calling the the IAppBuilder
        /// UseOAuthAuthorizationServer extension method.</summary>
        /// <param name="next">   The next.</param>
        /// <param name="app">    The application.</param>
        /// <param name="options">Options for controlling the operation.</param>
        public OAuthAuthorizationServerMiddleware(
            OwinMiddleware next,
            global::Owin.IAppBuilder app,
            OAuthAuthorizationServerOptions options) : base(next, options)
        {
            _logger = app.CreateLogger<OAuthAuthorizationServerMiddleware>();
            if (Options.Provider == null)
            {
                Options.Provider = new OAuthAuthorizationServerProvider();
            }
            if (Options.AuthorizationCodeFormat == null)
            {
                var dataProtector = app.CreateDataProtector(
                    typeof(OAuthAuthorizationServerMiddleware).FullName,
                    "Authentication_Code",
                    "v1");
                Options.AuthorizationCodeFormat = new TicketDataFormat(dataProtector);
            }
            if (Options.AccessTokenFormat == null)
            {
                var dataProtector1 = app.CreateDataProtector(
                    typeof(OAuthAuthorizationServerMiddleware).Namespace,
                    "Access_Token",
                    "v1");
                Options.AccessTokenFormat = new TicketDataFormat(dataProtector1);
            }
            if (Options.RefreshTokenFormat == null)
            {
                var dataProtector2 = app.CreateDataProtector(
                    typeof(OAuthAuthorizationServerMiddleware).Namespace,
                    "Refresh_Token",
                    "v1");
                Options.RefreshTokenFormat = new TicketDataFormat(dataProtector2);
            }
            if (Options.AuthorizationCodeProvider == null)
            {
                Options.AuthorizationCodeProvider = new AuthenticationTokenProvider();
            }
            if (Options.AccessTokenProvider == null)
            {
                Options.AccessTokenProvider = new AuthenticationTokenProvider();
            }
            if (Options.RefreshTokenProvider == null)
            {
                Options.RefreshTokenProvider = new AuthenticationTokenProvider();
            }
        }

        /// <summary>Called by the AuthenticationMiddleware base class to create a per-request handler.</summary>
        /// <returns>A new instance of the request handler.</returns>
        protected override AuthenticationHandler<OAuthAuthorizationServerOptions> CreateHandler()
        {
            return new OAuthAuthorizationServerHandler(_logger);
        }
    }
}
