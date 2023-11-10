// <copyright file="OAuthBearerAuthenticationHandler.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication bearer authentication handler class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure;
    using Logging;

    /// <summary>An authentication bearer authentication handler.</summary>
    /// <seealso cref="Microsoft.Owin.Security.Infrastructure.AuthenticationHandler{OAuthBearerAuthenticationOptions}"/>
    internal class OAuthBearerAuthenticationHandler : AuthenticationHandler<OAuthBearerAuthenticationOptions>
    {
        /// <summary>The challenge.</summary>
        private readonly string _challenge;

        /// <summary>The logger.</summary>
        private readonly ILogger _logger;

        /// <summary>Initializes a new instance of the
        /// <see cref="Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationHandler"/> class.</summary>
        /// <param name="logger">   The logger.</param>
        /// <param name="challenge">The challenge.</param>
        public OAuthBearerAuthenticationHandler(ILogger logger, string challenge)
        {
            _logger = logger;
            _challenge = challenge;
        }

        /// <inheritdoc/>
        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode != 401)
            {
                return Task.FromResult<object>(null);
            }
            if (Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode) != null)
            {
                var oAuthChallengeContext = new OAuthChallengeContext(Context, _challenge);
                Options.Provider.ApplyChallenge(oAuthChallengeContext);
            }
            return Task.FromResult<object>(null);
        }

        /// <inheritdoc/>
        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var authenticationHandler = this;
            try
            {
                string token = null;
                var str = authenticationHandler.Request.Headers.Get("Authorization");
                if (!string.IsNullOrEmpty(str) && str.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    token = str["Bearer ".Length..].Trim();
                }
                var requestTokenContext = new OAuthRequestTokenContext(authenticationHandler.Context, token);
                await authenticationHandler.Options.Provider.RequestToken(requestTokenContext).ConfigureAwait(false);
                if (string.IsNullOrEmpty(requestTokenContext.Token))
                {
                    return null;
                }
                var tokenReceiveContext = new AuthenticationTokenReceiveContext(
                    authenticationHandler.Context,
                    authenticationHandler.Options.AccessTokenFormat,
                    requestTokenContext.Token);
                await authenticationHandler.Options.AccessTokenProvider.ReceiveAsync(tokenReceiveContext).ConfigureAwait(false);
                if (tokenReceiveContext.Ticket == null)
                {
                    tokenReceiveContext.DeserializeTicket(tokenReceiveContext.Token);
                }
                var ticket = tokenReceiveContext.Ticket;
                if (ticket == null)
                {
                    authenticationHandler._logger.WriteWarning("invalid bearer token received");
                    return null;
                }
                var utcNow = authenticationHandler.Options.SystemClock.UtcNow;
                var expiresUtc = ticket.Properties.ExpiresUtc;
                if (expiresUtc.HasValue)
                {
                    expiresUtc = ticket.Properties.ExpiresUtc;
                    if (expiresUtc.Value < utcNow)
                    {
                        authenticationHandler._logger.WriteWarning("expired bearer token received");
                        return null;
                    }
                }
                var context = new OAuthValidateIdentityContext(
                    authenticationHandler.Context,
                    authenticationHandler.Options,
                    ticket);
                if (ticket != null && ticket.Identity != null && ticket.Identity.IsAuthenticated)
                {
                    context.Validated();
                }
                if (authenticationHandler.Options.Provider != null)
                {
                    await authenticationHandler.Options.Provider.ValidateIdentity(context).ConfigureAwait(false);
                }
                return context.IsValidated ? context.Ticket : null;
            }
            catch (Exception ex)
            {
                authenticationHandler._logger.WriteError("Authentication failed", ex);
                return null;
            }
        }
    }
}
