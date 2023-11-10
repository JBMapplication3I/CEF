// <copyright file="OAuthAuthorizationServerHandler.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication authorization server handler class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Infrastructure;
    using Logging;
    using Messages;
    using Newtonsoft.Json;
    using Owin.Infrastructure;

    /// <summary>An authentication authorization server handler.</summary>
    /// <seealso cref="AuthenticationHandler{OAuthAuthorizationServerOptions}"/>
    internal class OAuthAuthorizationServerHandler : AuthenticationHandler<OAuthAuthorizationServerOptions>
    {
        /// <summary>The logger.</summary>
        private readonly ILogger _logger;

        /// <summary>The authorize endpoint request.</summary>
        private AuthorizeEndpointRequest _authorizeEndpointRequest;

        /// <summary>Context for the client.</summary>
        private OAuthValidateClientRedirectUriContext _clientContext;

        /// <summary>Initializes a new instance of the
        /// <see cref="OAuthAuthorizationServerHandler"/> class.</summary>
        /// <param name="logger">The logger.</param>
        public OAuthAuthorizationServerHandler(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public override async Task<bool> InvokeAsync()
        {
            bool flag;
            var oAuthMatchEndpointContext = new OAuthMatchEndpointContext(Context, Options);
            if (Options.AuthorizeEndpointPath.HasValue && Options.AuthorizeEndpointPath == Request.Path)
            {
                oAuthMatchEndpointContext.MatchesAuthorizeEndpoint();
            }
            else if (Options.TokenEndpointPath.HasValue && Options.TokenEndpointPath == Request.Path)
            {
                oAuthMatchEndpointContext.MatchesTokenEndpoint();
            }
            await Options.Provider.MatchEndpoint(oAuthMatchEndpointContext).ConfigureAwait(false);
            if (!oAuthMatchEndpointContext.IsRequestCompleted)
            {
                if (oAuthMatchEndpointContext.IsAuthorizeEndpoint || oAuthMatchEndpointContext.IsTokenEndpoint)
                {
                    if (!Options.AllowInsecureHttp
                        && string.Equals(Request.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.WriteWarning(
                            "Authorization server ignoring http request because AllowInsecureHttp is false.");
                        flag = false;
                        return flag;
                    }
                    if (oAuthMatchEndpointContext.IsAuthorizeEndpoint)
                    {
                        flag = await InvokeAuthorizeEndpointAsync().ConfigureAwait(false);
                        return flag;
                    }
                    if (oAuthMatchEndpointContext.IsTokenEndpoint)
                    {
                        await InvokeTokenEndpointAsync().ConfigureAwait(false);
                        flag = true;
                        return flag;
                    }
                }
                flag = false;
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        /// <inheritdoc/>
        protected override async Task ApplyResponseGrantAsync()
        {
            TimeSpan? nullable;
            string token;
            AuthenticationTokenCreateContext authenticationTokenCreateContext;
            OAuthAuthorizationEndpointResponseContext oAuthAuthorizationEndpointResponseContext;
            if (_clientContext != null && _authorizeEndpointRequest != null && Response.StatusCode == 200)
            {
                var helper = Helper;
                var clientId = helper.LookupSignIn(Options.AuthenticationType);
                if (clientId != null)
                {
                    var strs = new Dictionary<string, string>();
                    if (_authorizeEndpointRequest.IsAuthorizationCodeGrantType)
                    {
                        var utcNow = Options.SystemClock.UtcNow;
                        clientId.Properties.IssuedUtc = utcNow;
                        clientId.Properties.ExpiresUtc = utcNow.Add(Options.AuthorizationCodeExpireTimeSpan);
                        clientId.Properties.Dictionary["client_id"] = _authorizeEndpointRequest.ClientId;
                        if (!string.IsNullOrEmpty(_authorizeEndpointRequest.RedirectUri))
                        {
                            clientId.Properties.Dictionary["redirect_uri"] = _authorizeEndpointRequest.RedirectUri;
                        }
                        authenticationTokenCreateContext = new AuthenticationTokenCreateContext(
                            Context,
                            Options.AuthorizationCodeFormat,
                            new AuthenticationTicket(clientId.Identity, clientId.Properties));
                        await Options.AuthorizationCodeProvider.CreateAsync(authenticationTokenCreateContext).ConfigureAwait(false);
                        token = authenticationTokenCreateContext.Token;
                        if (!string.IsNullOrEmpty(token))
                        {
                            oAuthAuthorizationEndpointResponseContext = new OAuthAuthorizationEndpointResponseContext(
                                Context,
                                Options,
                                new AuthenticationTicket(clientId.Identity, clientId.Properties),
                                _authorizeEndpointRequest,
                                null,
                                token);
                            await Options.Provider.AuthorizationEndpointResponse(
                                oAuthAuthorizationEndpointResponseContext).ConfigureAwait(false);
                            foreach (var additionalResponseParameter in oAuthAuthorizationEndpointResponseContext
                                .AdditionalResponseParameters)
                            {
                                strs[additionalResponseParameter.Key] = additionalResponseParameter.Value.ToString();
                            }
                            strs["code"] = token;
                            if (!string.IsNullOrEmpty(_authorizeEndpointRequest.State))
                            {
                                strs["state"] = _authorizeEndpointRequest.State;
                            }
                            string empty;
                            if (!_authorizeEndpointRequest.IsFormPostResponseMode)
                            {
                                empty = _clientContext.RedirectUri;
                            }
                            else
                            {
                                empty = Options.FormPostEndpoint.ToString();
                                strs["redirect_uri"] = _clientContext.RedirectUri;
                            }
                            foreach (var key in strs.Keys)
                            {
                                empty = WebUtilities.AddQueryString(empty, key, strs[key]);
                            }
                            Response.Redirect(empty);
                            authenticationTokenCreateContext = null;
                            token = null;
                            oAuthAuthorizationEndpointResponseContext = null;
                        }
                        else
                        {
                            _logger.WriteError(
                                "response_type code requires an Options.AuthorizationCodeProvider implementing a single-use token.");
                            var oAuthValidateAuthorizeRequestContext = new OAuthValidateAuthorizeRequestContext(
                                Context,
                                Options,
                                _authorizeEndpointRequest,
                                _clientContext);
                            oAuthValidateAuthorizeRequestContext.SetError("unsupported_response_type");
                            await SendErrorRedirectAsync(_clientContext, oAuthValidateAuthorizeRequestContext).ConfigureAwait(false);
                        }
                    }
                    else if (_authorizeEndpointRequest.IsImplicitGrantType)
                    {
                        token = _clientContext.RedirectUri;
                        var dateTimeOffset = Options.SystemClock.UtcNow;
                        clientId.Properties.IssuedUtc = dateTimeOffset;
                        clientId.Properties.ExpiresUtc = dateTimeOffset.Add(Options.AccessTokenExpireTimeSpan);
                        clientId.Properties.Dictionary["client_id"] = _authorizeEndpointRequest.ClientId;
                        authenticationTokenCreateContext = new AuthenticationTokenCreateContext(
                            Context,
                            Options.AccessTokenFormat,
                            new AuthenticationTicket(clientId.Identity, clientId.Properties));
                        await Options.AccessTokenProvider.CreateAsync(authenticationTokenCreateContext).ConfigureAwait(false);
                        var str = authenticationTokenCreateContext.Token;
                        if (string.IsNullOrEmpty(str))
                        {
                            str = authenticationTokenCreateContext.SerializeTicket();
                        }
                        var expiresUtc = authenticationTokenCreateContext.Ticket.Properties.ExpiresUtc;
                        var appender = new Appender(token, '#');
                        appender.Append("access_token", str).Append("token_type", "bearer");
                        if (expiresUtc.HasValue)
                        {
                            var nullable1 = expiresUtc;
                            var dateTimeOffset1 = dateTimeOffset;
                            if (nullable1.HasValue)
                            {
                                nullable = nullable1.GetValueOrDefault() - dateTimeOffset1;
                            }
                            else
                            {
                                nullable = null;
                            }
                            var totalSeconds = (long)(nullable.Value.TotalSeconds + 0.5);
                            appender.Append("expires_in", totalSeconds.ToString(CultureInfo.InvariantCulture));
                        }
                        if (!string.IsNullOrEmpty(_authorizeEndpointRequest.State))
                        {
                            appender.Append("state", _authorizeEndpointRequest.State);
                        }
                        oAuthAuthorizationEndpointResponseContext = new OAuthAuthorizationEndpointResponseContext(
                            Context,
                            Options,
                            new AuthenticationTicket(clientId.Identity, clientId.Properties),
                            _authorizeEndpointRequest,
                            str,
                            null);
                        await Options.Provider.AuthorizationEndpointResponse(oAuthAuthorizationEndpointResponseContext).ConfigureAwait(false);
                        foreach (var keyValuePair in oAuthAuthorizationEndpointResponseContext
                            .AdditionalResponseParameters)
                        {
                            appender.Append(keyValuePair.Key, keyValuePair.Value.ToString());
                        }
                        Response.Redirect(appender.ToString());
                        token = null;
                        dateTimeOffset = new DateTimeOffset();
                        authenticationTokenCreateContext = null;
                        appender = null;
                        oAuthAuthorizationEndpointResponseContext = null;
                    }
                }
            }
        }

        /// <inheritdoc/>
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            return Task.FromResult<AuthenticationTicket>(null);
        }

        /// <summary>Returns an outcome.</summary>
        /// <param name="validatingContext">Context for the validating.</param>
        /// <param name="grantContext">     Context for the grant.</param>
        /// <param name="ticket">           The ticket.</param>
        /// <param name="defaultError">     The default error.</param>
        /// <returns>The outcome.</returns>
        private static AuthenticationTicket ReturnOutcome(
            OAuthValidateTokenRequestContext validatingContext,
            BaseValidatingContext<OAuthAuthorizationServerOptions> grantContext,
            AuthenticationTicket ticket,
            string defaultError)
        {
            if (!validatingContext.IsValidated)
            {
                return null;
            }
            if (grantContext.IsValidated)
            {
                if (ticket != null)
                {
                    return ticket;
                }
                validatingContext.SetError(defaultError);
                return null;
            }
            if (!grantContext.HasError)
            {
                validatingContext.SetError(defaultError);
            }
            else
            {
                validatingContext.SetError(grantContext.Error, grantContext.ErrorDescription, grantContext.ErrorUri);
            }
            return null;
        }

        /// <summary>Executes the authorize endpoint asynchronous on a different thread, and waits for the result.</summary>
        /// <returns>A Task{bool}</returns>
        private async Task<bool> InvokeAuthorizeEndpointAsync()
        {
            bool isRequestCompleted;
            var authorizeEndpointRequest = new AuthorizeEndpointRequest(Request.Query);
            var oAuthValidateClientRedirectUriContext = new OAuthValidateClientRedirectUriContext(
                Context,
                Options,
                authorizeEndpointRequest.ClientId,
                authorizeEndpointRequest.RedirectUri);
            if (!string.IsNullOrEmpty(authorizeEndpointRequest.RedirectUri))
            {
                var flag = true;
                if (!Uri.TryCreate(authorizeEndpointRequest.RedirectUri, UriKind.Absolute, out var uri))
                {
                    flag = false;
                }
                else if (!string.IsNullOrEmpty(uri.Fragment))
                {
                    flag = false;
                }
                else if (!Options.AllowInsecureHttp
                    && string.Equals(uri.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
                {
                    flag = false;
                }
                if (!flag)
                {
                    oAuthValidateClientRedirectUriContext.SetError("invalid_request");
                    isRequestCompleted = await SendErrorRedirectAsync(
                                             oAuthValidateClientRedirectUriContext,
                                             oAuthValidateClientRedirectUriContext).ConfigureAwait(false);
                    return isRequestCompleted;
                }
            }
            await Options.Provider.ValidateClientRedirectUri(oAuthValidateClientRedirectUriContext).ConfigureAwait(false);
            if (oAuthValidateClientRedirectUriContext.IsValidated)
            {
                var oAuthValidateAuthorizeRequestContext = new OAuthValidateAuthorizeRequestContext(
                    Context,
                    Options,
                    authorizeEndpointRequest,
                    oAuthValidateClientRedirectUriContext);
                if (string.IsNullOrEmpty(authorizeEndpointRequest.ResponseType))
                {
                    _logger.WriteVerbose("Authorize endpoint request missing required response_type parameter");
                    oAuthValidateAuthorizeRequestContext.SetError("invalid_request");
                }
                else if (authorizeEndpointRequest.IsAuthorizationCodeGrantType
                    || authorizeEndpointRequest.IsImplicitGrantType)
                {
                    await Options.Provider.ValidateAuthorizeRequest(oAuthValidateAuthorizeRequestContext).ConfigureAwait(false);
                }
                else
                {
                    _logger.WriteVerbose("Authorize endpoint request contains unsupported response_type parameter");
                    oAuthValidateAuthorizeRequestContext.SetError("unsupported_response_type");
                }
                if (oAuthValidateAuthorizeRequestContext.IsValidated)
                {
                    _clientContext = oAuthValidateClientRedirectUriContext;
                    _authorizeEndpointRequest = authorizeEndpointRequest;
                    var oAuthAuthorizeEndpointContext = new OAuthAuthorizeEndpointContext(
                        Context,
                        Options,
                        authorizeEndpointRequest);
                    await Options.Provider.AuthorizeEndpoint(oAuthAuthorizeEndpointContext).ConfigureAwait(false);
                    isRequestCompleted = oAuthAuthorizeEndpointContext.IsRequestCompleted;
                }
                else
                {
                    isRequestCompleted = await SendErrorRedirectAsync(
                                             oAuthValidateClientRedirectUriContext,
                                             oAuthValidateAuthorizeRequestContext).ConfigureAwait(false);
                }
            }
            else
            {
                _logger.WriteVerbose("Unable to validate client information");
                isRequestCompleted = await SendErrorRedirectAsync(
                                         oAuthValidateClientRedirectUriContext,
                                         oAuthValidateClientRedirectUriContext).ConfigureAwait(false);
            }
            return isRequestCompleted;
        }

        /// <summary>Executes the token endpoint asynchronous on a different thread, and waits for the result.</summary>
        /// <returns>A Task.</returns>
        private async Task InvokeTokenEndpointAsync()
        {
            byte[] array;
            TimeSpan? nullable;
            var utcNow = Options.SystemClock.UtcNow;
            utcNow = utcNow.Subtract(TimeSpan.FromMilliseconds(utcNow.Millisecond));
            var formCollection = await Request.ReadFormAsync().ConfigureAwait(false);
            var oAuthValidateClientAuthenticationContext =
                new OAuthValidateClientAuthenticationContext(Context, Options, formCollection);
            await Options.Provider.ValidateClientAuthentication(oAuthValidateClientAuthenticationContext).ConfigureAwait(false);
            if (oAuthValidateClientAuthenticationContext.IsValidated)
            {
                var tokenEndpointRequest = new TokenEndpointRequest(formCollection);
                var oAuthValidateTokenRequestContext = new OAuthValidateTokenRequestContext(
                    Context,
                    Options,
                    tokenEndpointRequest,
                    oAuthValidateClientAuthenticationContext);
                AuthenticationTicket authenticationTicket = null;
                if (tokenEndpointRequest.IsAuthorizationCodeGrantType)
                {
                    authenticationTicket = await InvokeTokenEndpointAuthorizationCodeGrantAsync(
                                               oAuthValidateTokenRequestContext,
                                               utcNow).ConfigureAwait(false);
                }
                else if (tokenEndpointRequest.IsResourceOwnerPasswordCredentialsGrantType)
                {
                    authenticationTicket =
                        await InvokeTokenEndpointResourceOwnerPasswordCredentialsGrantAsync(
                            oAuthValidateTokenRequestContext,
                            utcNow).ConfigureAwait(false);
                }
                else if (tokenEndpointRequest.IsClientCredentialsGrantType)
                {
                    authenticationTicket = await InvokeTokenEndpointClientCredentialsGrantAsync(
                                               oAuthValidateTokenRequestContext,
                                               utcNow).ConfigureAwait(false);
                }
                else if (tokenEndpointRequest.IsRefreshTokenGrantType)
                {
                    authenticationTicket = await InvokeTokenEndpointRefreshTokenGrantAsync(
                                               oAuthValidateTokenRequestContext,
                                               utcNow).ConfigureAwait(false);
                }
                else if (!tokenEndpointRequest.IsCustomExtensionGrantType)
                {
                    _logger.WriteError("grant type is not recognized");
                    oAuthValidateTokenRequestContext.SetError("unsupported_grant_type");
                }
                else
                {
                    authenticationTicket = await InvokeTokenEndpointCustomGrantAsync(
                                               oAuthValidateTokenRequestContext,
                                               utcNow).ConfigureAwait(false);
                }
                if (authenticationTicket != null)
                {
                    authenticationTicket.Properties.IssuedUtc = utcNow;
                    authenticationTicket.Properties.ExpiresUtc = utcNow.Add(Options.AccessTokenExpireTimeSpan);
                    var oAuthTokenEndpointContext = new OAuthTokenEndpointContext(
                        Context,
                        Options,
                        authenticationTicket,
                        tokenEndpointRequest);
                    await Options.Provider.TokenEndpoint(oAuthTokenEndpointContext).ConfigureAwait(false);
                    if (!oAuthTokenEndpointContext.TokenIssued)
                    {
                        _logger.WriteError("Token was not issued to tokenEndpointContext");
                        oAuthValidateTokenRequestContext.SetError("invalid_grant");
                        await SendErrorAsJsonAsync(oAuthValidateTokenRequestContext).ConfigureAwait(false);
                    }
                    else
                    {
                        authenticationTicket = new AuthenticationTicket(
                            oAuthTokenEndpointContext.Identity,
                            oAuthTokenEndpointContext.Properties);
                        var authenticationTokenCreateContext = new AuthenticationTokenCreateContext(
                            Context,
                            Options.AccessTokenFormat,
                            authenticationTicket);
                        await Options.AccessTokenProvider.CreateAsync(authenticationTokenCreateContext).ConfigureAwait(false);
                        var token = authenticationTokenCreateContext.Token;
                        if (string.IsNullOrEmpty(token))
                        {
                            token = authenticationTokenCreateContext.SerializeTicket();
                        }
                        var expiresUtc = authenticationTicket.Properties.ExpiresUtc;
                        var authenticationTokenCreateContext1 = new AuthenticationTokenCreateContext(
                            Context,
                            Options.RefreshTokenFormat,
                            authenticationTokenCreateContext.Ticket);
                        await Options.RefreshTokenProvider.CreateAsync(authenticationTokenCreateContext1).ConfigureAwait(false);
                        var str = authenticationTokenCreateContext1.Token;
                        var oAuthTokenEndpointResponseContext = new OAuthTokenEndpointResponseContext(
                            Context,
                            Options,
                            authenticationTicket,
                            tokenEndpointRequest,
                            token,
                            oAuthTokenEndpointContext.AdditionalResponseParameters);
                        await Options.Provider.TokenEndpointResponse(oAuthTokenEndpointResponseContext).ConfigureAwait(false);
                        var memoryStream = new MemoryStream();
                        using (var jsonTextWriter = new JsonTextWriter(new StreamWriter(memoryStream)))
                        {
                            jsonTextWriter.WriteStartObject();
                            jsonTextWriter.WritePropertyName("access_token");
                            jsonTextWriter.WriteValue(token);
                            jsonTextWriter.WritePropertyName("token_type");
                            jsonTextWriter.WriteValue("bearer");
                            if (expiresUtc.HasValue)
                            {
                                var nullable1 = expiresUtc;
                                var dateTimeOffset = utcNow;
                                if (nullable1.HasValue)
                                {
                                    nullable = nullable1.GetValueOrDefault() - dateTimeOffset;
                                }
                                else
                                {
                                    nullable = null;
                                }
                                var totalSeconds = (long)nullable.Value.TotalSeconds;
                                if (totalSeconds > 0)
                                {
                                    jsonTextWriter.WritePropertyName("expires_in");
                                    jsonTextWriter.WriteValue(totalSeconds);
                                }
                            }
                            if (!string.IsNullOrEmpty(str))
                            {
                                jsonTextWriter.WritePropertyName("refresh_token");
                                jsonTextWriter.WriteValue(str);
                            }
                            foreach (var additionalResponseParameter in oAuthTokenEndpointResponseContext
                                .AdditionalResponseParameters)
                            {
                                jsonTextWriter.WritePropertyName(additionalResponseParameter.Key);
                                jsonTextWriter.WriteValue(additionalResponseParameter.Value);
                            }
                            jsonTextWriter.WriteEndObject();
                            jsonTextWriter.Flush();
                            array = memoryStream.ToArray();
                        }
                        Response.ContentType = "application/json;charset=UTF-8";
                        Response.Headers.Set("Cache-Control", "no-cache");
                        Response.Headers.Set("Pragma", "no-cache");
                        Response.Headers.Set("Expires", "-1");
                        Response.ContentLength = array.Length;
                        await Response.WriteAsync(array, Request.CallCancelled).ConfigureAwait(false);
                    }
                }
                else
                {
                    await SendErrorAsJsonAsync(oAuthValidateTokenRequestContext).ConfigureAwait(false);
                }
            }
            else
            {
                _logger.WriteError("clientID is not valid.");
                if (!oAuthValidateClientAuthenticationContext.HasError)
                {
                    oAuthValidateClientAuthenticationContext.SetError("invalid_client");
                }
                await SendErrorAsJsonAsync(oAuthValidateClientAuthenticationContext).ConfigureAwait(false);
            }
        }

        /// <summary>Executes the token endpoint authorization code grant asynchronous on a different thread, and waits
        /// for the result.</summary>
        /// <param name="validatingContext">Context for the validating.</param>
        /// <param name="currentUtc">       The current UTC.</param>
        /// <returns>A Task{AuthenticationTicket}</returns>
        private async Task<AuthenticationTicket> InvokeTokenEndpointAuthorizationCodeGrantAsync(
            OAuthValidateTokenRequestContext validatingContext,
            DateTimeOffset currentUtc)
        {
            AuthenticationTicket authenticationTicket;
            var tokenRequest = validatingContext.TokenRequest;
            var authenticationTokenReceiveContext = new AuthenticationTokenReceiveContext(
                Context,
                Options.AuthorizationCodeFormat,
                tokenRequest.AuthorizationCodeGrant.Code);
            await Options.AuthorizationCodeProvider.ReceiveAsync(authenticationTokenReceiveContext).ConfigureAwait(false);
            var ticket = authenticationTokenReceiveContext.Ticket;
            if (ticket == null)
            {
                _logger.WriteError("invalid authorization code");
                validatingContext.SetError("invalid_grant");
                authenticationTicket = null;
                return null;
            }
            var expiresUtc = ticket.Properties.ExpiresUtc;
            if (expiresUtc.HasValue)
            {
                expiresUtc = ticket.Properties.ExpiresUtc;
                var dateTimeOffset = currentUtc;
                var flag = expiresUtc.HasValue && expiresUtc.GetValueOrDefault() < dateTimeOffset;
                if (!flag)
                {
                    if (!ticket.Properties.Dictionary.TryGetValue("client_id", out var str)
                        || !string.Equals(str, validatingContext.ClientContext.ClientId, StringComparison.Ordinal))
                    {
                        _logger.WriteError("authorization code does not contain matching client_id");
                        validatingContext.SetError("invalid_grant");
                        authenticationTicket = null;
                        return null;
                    }
                    if (ticket.Properties.Dictionary.TryGetValue("redirect_uri", out var str1))
                    {
                        ticket.Properties.Dictionary.Remove("redirect_uri");
                        if (!string.Equals(
                            str1,
                            tokenRequest.AuthorizationCodeGrant.RedirectUri,
                            StringComparison.Ordinal))
                        {
                            _logger.WriteError("authorization code does not contain matching redirect_uri");
                            validatingContext.SetError("invalid_grant");
                            authenticationTicket = null;
                            return null;
                        }
                    }
                    await Options.Provider.ValidateTokenRequest(validatingContext).ConfigureAwait(false);
                    var oAuthGrantAuthorizationCodeContext =
                        new OAuthGrantAuthorizationCodeContext(Context, Options, ticket);
                    if (validatingContext.IsValidated)
                    {
                        await Options.Provider.GrantAuthorizationCode(oAuthGrantAuthorizationCodeContext).ConfigureAwait(false);
                    }
                    authenticationTicket = ReturnOutcome(
                        validatingContext,
                        oAuthGrantAuthorizationCodeContext,
                        oAuthGrantAuthorizationCodeContext.Ticket,
                        "invalid_grant");
                    return authenticationTicket;
                }
            }
            _logger.WriteError("expired authorization code");
            validatingContext.SetError("invalid_grant");
            authenticationTicket = null;
            return null;
        }

        /// <summary>Executes the token endpoint client credentials grant asynchronous on a different thread, and waits
        /// for the result.</summary>
        /// <param name="validatingContext">Context for the validating.</param>
        /// <param name="currentUtc">       The current UTC.</param>
        /// <returns>A Task{AuthenticationTicket}</returns>
        private async Task<AuthenticationTicket> InvokeTokenEndpointClientCredentialsGrantAsync(
            OAuthValidateTokenRequestContext validatingContext,
            DateTimeOffset currentUtc)
        {
            AuthenticationTicket authenticationTicket;
            var tokenRequest = validatingContext.TokenRequest;
            await Options.Provider.ValidateTokenRequest(validatingContext).ConfigureAwait(false);
            if (validatingContext.IsValidated)
            {
                var oAuthGrantClientCredentialsContext = new OAuthGrantClientCredentialsContext(
                    Context,
                    Options,
                    validatingContext.ClientContext.ClientId,
                    tokenRequest.ClientCredentialsGrant.Scope);
                await Options.Provider.GrantClientCredentials(oAuthGrantClientCredentialsContext).ConfigureAwait(false);
                authenticationTicket = ReturnOutcome(
                    validatingContext,
                    oAuthGrantClientCredentialsContext,
                    oAuthGrantClientCredentialsContext.Ticket,
                    "unauthorized_client");
            }
            else
            {
                authenticationTicket = null;
            }
            return authenticationTicket;
        }

        /// <summary>Executes the token endpoint custom grant asynchronous on a different thread, and waits for the
        /// result.</summary>
        /// <param name="validatingContext">Context for the validating.</param>
        /// <param name="currentUtc">       The current UTC.</param>
        /// <returns>A Task{AuthenticationTicket}</returns>
        private async Task<AuthenticationTicket> InvokeTokenEndpointCustomGrantAsync(
            OAuthValidateTokenRequestContext validatingContext,
            DateTimeOffset currentUtc)
        {
            var tokenRequest = validatingContext.TokenRequest;
            await Options.Provider.ValidateTokenRequest(validatingContext).ConfigureAwait(false);
            var oAuthGrantCustomExtensionContext = new OAuthGrantCustomExtensionContext(
                Context,
                Options,
                validatingContext.ClientContext.ClientId,
                tokenRequest.GrantType,
                tokenRequest.CustomExtensionGrant.Parameters);
            if (validatingContext.IsValidated)
            {
                await Options.Provider.GrantCustomExtension(oAuthGrantCustomExtensionContext).ConfigureAwait(false);
            }
            return ReturnOutcome(
                validatingContext,
                oAuthGrantCustomExtensionContext,
                oAuthGrantCustomExtensionContext.Ticket,
                "unsupported_grant_type");
        }

        /// <summary>Executes the token endpoint refresh token grant asynchronous on a different thread, and waits for
        /// the result.</summary>
        /// <param name="validatingContext">Context for the validating.</param>
        /// <param name="currentUtc">       The current UTC.</param>
        /// <returns>A Task{AuthenticationTicket}</returns>
        private async Task<AuthenticationTicket> InvokeTokenEndpointRefreshTokenGrantAsync(
            OAuthValidateTokenRequestContext validatingContext,
            DateTimeOffset currentUtc)
        {
            AuthenticationTicket authenticationTicket;
            bool flag;
            var tokenRequest = validatingContext.TokenRequest;
            var authenticationTokenReceiveContext = new AuthenticationTokenReceiveContext(
                Context,
                Options.RefreshTokenFormat,
                tokenRequest.RefreshTokenGrant.RefreshToken);
            await Options.RefreshTokenProvider.ReceiveAsync(authenticationTokenReceiveContext).ConfigureAwait(false);
            var ticket = authenticationTokenReceiveContext.Ticket;
            if (ticket != null)
            {
                var expiresUtc = ticket.Properties.ExpiresUtc;
                if (expiresUtc.HasValue)
                {
                    expiresUtc = ticket.Properties.ExpiresUtc;
                    var dateTimeOffset = currentUtc;
                    flag = expiresUtc.HasValue && expiresUtc.GetValueOrDefault() < dateTimeOffset;
                    if (!flag)
                    {
                        await Options.Provider.ValidateTokenRequest(validatingContext).ConfigureAwait(false);
                        var oAuthGrantRefreshTokenContext = new OAuthGrantRefreshTokenContext(
                            Context,
                            Options,
                            ticket,
                            validatingContext.ClientContext.ClientId);
                        if (validatingContext.IsValidated)
                        {
                            await Options.Provider.GrantRefreshToken(oAuthGrantRefreshTokenContext).ConfigureAwait(false);
                        }
                        authenticationTicket = ReturnOutcome(
                            validatingContext,
                            oAuthGrantRefreshTokenContext,
                            oAuthGrantRefreshTokenContext.Ticket,
                            "invalid_grant");
                        return authenticationTicket;
                    }
                }
                _logger.WriteError("expired refresh token");
                validatingContext.SetError("invalid_grant");
                authenticationTicket = null;
            }
            else
            {
                _logger.WriteError("invalid refresh token");
                validatingContext.SetError("invalid_grant");
                authenticationTicket = null;
            }
            return authenticationTicket;
        }

        /// <summary>Executes the token endpoint resource owner password credentials grant asynchronous on a different
        /// thread, and waits for the result.</summary>
        /// <param name="validatingContext">Context for the validating.</param>
        /// <param name="currentUtc">       The current UTC.</param>
        /// <returns>A Task{AuthenticationTicket}</returns>
        private async Task<AuthenticationTicket> InvokeTokenEndpointResourceOwnerPasswordCredentialsGrantAsync(
            OAuthValidateTokenRequestContext validatingContext,
            DateTimeOffset currentUtc)
        {
            var tokenRequest = validatingContext.TokenRequest;
            await Options.Provider.ValidateTokenRequest(validatingContext).ConfigureAwait(false);
            var oAuthGrantResourceOwnerCredentialsContext = new OAuthGrantResourceOwnerCredentialsContext(
                Context,
                Options,
                validatingContext.ClientContext.ClientId,
                tokenRequest.ResourceOwnerPasswordCredentialsGrant.UserName,
                tokenRequest.ResourceOwnerPasswordCredentialsGrant.Password,
                tokenRequest.ResourceOwnerPasswordCredentialsGrant.Scope);
            if (validatingContext.IsValidated)
            {
                await Options.Provider.GrantResourceOwnerCredentials(oAuthGrantResourceOwnerCredentialsContext).ConfigureAwait(false);
            }
            return ReturnOutcome(
                validatingContext,
                oAuthGrantResourceOwnerCredentialsContext,
                oAuthGrantResourceOwnerCredentialsContext.Ticket,
                "invalid_grant");
        }

        /// <summary>Sends an error as JSON asynchronous.</summary>
        /// <param name="validatingContext">Context for the validating.</param>
        /// <returns>A Task.</returns>
        private Task SendErrorAsJsonAsync(BaseValidatingContext<OAuthAuthorizationServerOptions> validatingContext)
        {
            byte[] array;
            string errorDescription;
            string errorUri;
            var str = validatingContext.HasError ? validatingContext.Error : "invalid_request";
            if (validatingContext.HasError)
            {
                errorDescription = validatingContext.ErrorDescription;
            }
            else
            {
                errorDescription = null;
            }
            var str1 = errorDescription;
            if (validatingContext.HasError)
            {
                errorUri = validatingContext.ErrorUri;
            }
            else
            {
                errorUri = null;
            }
            var str2 = errorUri;
            var memoryStream = new MemoryStream();
            using (var jsonTextWriter = new JsonTextWriter(new StreamWriter(memoryStream)))
            {
                jsonTextWriter.WriteStartObject();
                jsonTextWriter.WritePropertyName("error");
                jsonTextWriter.WriteValue(str);
                if (!string.IsNullOrEmpty(str1))
                {
                    jsonTextWriter.WritePropertyName("error_description");
                    jsonTextWriter.WriteValue(str1);
                }
                if (!string.IsNullOrEmpty(str2))
                {
                    jsonTextWriter.WritePropertyName("error_uri");
                    jsonTextWriter.WriteValue(str2);
                }
                jsonTextWriter.WriteEndObject();
                jsonTextWriter.Flush();
                array = memoryStream.ToArray();
            }
            Response.StatusCode = 400;
            Response.ContentType = "application/json;charset=UTF-8";
            Response.Headers.Set("Cache-Control", "no-cache");
            Response.Headers.Set("Pragma", "no-cache");
            Response.Headers.Set("Expires", "-1");
            var headers = Response.Headers;
            var length = array.Length;
            headers.Set("Content-Length", length.ToString(CultureInfo.InvariantCulture));
            return Response.WriteAsync(array, Request.CallCancelled);
        }

        /// <summary>Sends an error page asynchronous.</summary>
        /// <param name="error">           The error.</param>
        /// <param name="errorDescription">Information describing the error.</param>
        /// <param name="errorUri">        URI of the error.</param>
        /// <returns>A Task{bool}</returns>
        private async Task<bool> SendErrorPageAsync(string error, string errorDescription, string errorUri)
        {
            bool flag;
            byte[] array;
            Response.StatusCode = 400;
            Response.Headers.Set("Cache-Control", "no-cache");
            Response.Headers.Set("Pragma", "no-cache");
            Response.Headers.Set("Expires", "-1");
            if (!Options.ApplicationCanDisplayErrors)
            {
                var memoryStream = new MemoryStream();
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.WriteLine("error: {0}", error);
                    if (!string.IsNullOrEmpty(errorDescription))
                    {
                        streamWriter.WriteLine("error_description: {0}", errorDescription);
                    }
                    if (!string.IsNullOrEmpty(errorUri))
                    {
                        streamWriter.WriteLine("error_uri: {0}", errorUri);
                    }
                    streamWriter.Flush();
                    array = memoryStream.ToArray();
                }
                Response.ContentType = "text/plain;charset=UTF-8";
                var headers = Response.Headers;
                var length = array.Length;
                headers.Set("Content-Length", length.ToString(CultureInfo.InvariantCulture));
                await Response.WriteAsync(array, Request.CallCancelled).ConfigureAwait(false);
                flag = true;
            }
            else
            {
                Context.Set("oauth.Error", error);
                Context.Set("oauth.ErrorDescription", errorDescription);
                Context.Set("oauth.ErrorUri", errorUri);
                flag = false;
            }
            return flag;
        }

        /// <summary>Sends an error redirect asynchronous.</summary>
        /// <param name="clientContext">    Context for the client.</param>
        /// <param name="validatingContext">Context for the validating.</param>
        /// <returns>A Task{bool}</returns>
        private Task<bool> SendErrorRedirectAsync(
            OAuthValidateClientRedirectUriContext clientContext,
            BaseValidatingContext<OAuthAuthorizationServerOptions> validatingContext)
        {
            string errorDescription;
            string errorUri;
            if (clientContext == null)
            {
                throw new ArgumentNullException(nameof(clientContext));
            }
            var str = validatingContext.HasError ? validatingContext.Error : "invalid_request";
            if (validatingContext.HasError)
            {
                errorDescription = validatingContext.ErrorDescription;
            }
            else
            {
                errorDescription = null;
            }
            var str1 = errorDescription;
            if (validatingContext.HasError)
            {
                errorUri = validatingContext.ErrorUri;
            }
            else
            {
                errorUri = null;
            }
            var str2 = errorUri;
            if (!clientContext.IsValidated)
            {
                return SendErrorPageAsync(str, str1, str2);
            }
            var str3 = WebUtilities.AddQueryString(clientContext.RedirectUri, "error", str);
            if (!string.IsNullOrEmpty(str1))
            {
                str3 = WebUtilities.AddQueryString(str3, "error_description", str1);
            }
            if (!string.IsNullOrEmpty(str2))
            {
                str3 = WebUtilities.AddQueryString(str3, "error_uri", str2);
            }
            var values = clientContext.Request.Query.GetValues("state");
            if (values != null && values.Count == 1)
            {
                str3 = WebUtilities.AddQueryString(str3, "state", values[0]);
            }
            Response.Redirect(str3);
            return Task.FromResult(true);
        }

        /// <summary>An appender.</summary>
        private class Appender
        {
            /// <summary>The delimiter.</summary>
            private readonly char _delimiter;

            /// <summary>The sb.</summary>
            private readonly StringBuilder _sb;

            /// <summary>True if this Appender has delimiter.</summary>
            private bool _hasDelimiter;

            /// <summary>Initializes a new instance of the
            /// <see cref="Appender"/> class.</summary>
            /// <param name="value">    The value.</param>
            /// <param name="delimiter">The delimiter.</param>
            public Appender(string value, char delimiter)
            {
                _sb = new StringBuilder(value);
                _delimiter = delimiter;
                _hasDelimiter = value.IndexOf(delimiter) != -1;
            }

            /// <summary>Appends a name.</summary>
            /// <param name="name"> The name.</param>
            /// <param name="value">The value.</param>
            /// <returns>An Appender.</returns>
            public Appender Append(string name, string value)
            {
                _sb.Append(_hasDelimiter ? '&' : _delimiter)
                    .Append(Uri.EscapeDataString(name))
                    .Append('=')
                    .Append(Uri.EscapeDataString(value));
                _hasDelimiter = true;
                return this;
            }

            /// <inheritdoc/>
            public override string ToString()
            {
                return _sb.ToString();
            }
        }
    }
}
