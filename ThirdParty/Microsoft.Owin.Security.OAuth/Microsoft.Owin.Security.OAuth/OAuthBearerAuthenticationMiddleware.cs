// <copyright file="OAuthBearerAuthenticationMiddleware.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication bearer authentication middleware class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using DataHandler;
    using DataProtection;
    using Infrastructure;
    using Logging;

    /// <summary>Bearer authentication middleware component which is added to an OWIN pipeline. This class is not
    /// created by application code directly, instead it is added by calling the the IAppBuilder
    /// UseOAuthBearerAuthentication extension method.</summary>
    /// <seealso cref="AuthenticationMiddleware{OAuthBearerAuthenticationOptions}"/>
    public class OAuthBearerAuthenticationMiddleware : AuthenticationMiddleware<OAuthBearerAuthenticationOptions>
    {
        /// <summary>The challenge.</summary>
        private readonly string _challenge;

        /// <summary>The logger.</summary>
        private readonly ILogger _logger;

        /// <summary>Bearer authentication component which is added to an OWIN pipeline. This constructor is not called
        /// by application code directly, instead it is added by calling the the IAppBuilder
        /// UseOAuthBearerAuthentication extension method.</summary>
        /// <param name="next">   The next.</param>
        /// <param name="app">    The application.</param>
        /// <param name="options">Options for controlling the operation.</param>
        public OAuthBearerAuthenticationMiddleware(
            OwinMiddleware next,
            global::Owin.IAppBuilder app,
            OAuthBearerAuthenticationOptions options) : base(next, options)
        {
            _logger = app.CreateLogger<OAuthBearerAuthenticationMiddleware>();
            if (!string.IsNullOrWhiteSpace(Options.Challenge))
            {
                _challenge = Options.Challenge;
            }
            else if (!string.IsNullOrWhiteSpace(Options.Realm))
            {
                _challenge = string.Concat("Bearer realm=\"", Options.Realm, "\"");
            }
            else
            {
                _challenge = "Bearer";
            }
            if (Options.Provider == null)
            {
                Options.Provider = new OAuthBearerAuthenticationProvider();
            }
            if (Options.AccessTokenFormat == null)
            {
                var dataProtector = app.CreateDataProtector(
                    typeof(OAuthBearerAuthenticationMiddleware).Namespace,
                    "Access_Token",
                    "v1");
                Options.AccessTokenFormat = new TicketDataFormat(dataProtector);
            }
            if (Options.AccessTokenProvider == null)
            {
                Options.AccessTokenProvider = new AuthenticationTokenProvider();
            }
        }

        /// <summary>Called by the AuthenticationMiddleware base class to create a per-request handler.</summary>
        /// <returns>A new instance of the request handler.</returns>
        protected override AuthenticationHandler<OAuthBearerAuthenticationOptions> CreateHandler()
        {
            return new OAuthBearerAuthenticationHandler(_logger, _challenge);
        }
    }
}
