// <copyright file="OAuthAuthorizationServerOptions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication authorization server options class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System;
    using Infrastructure;
    using Owin.Infrastructure;

    /// <summary>Options class provides information needed to control Authorization Server middleware behavior.</summary>
    /// <seealso cref="AuthenticationOptions"/>
    public class OAuthAuthorizationServerOptions : AuthenticationOptions
    {
        /// <summary>Creates an instance of authorization server options with default values.</summary>
        public OAuthAuthorizationServerOptions() : base("Bearer")
        {
            AuthorizationCodeExpireTimeSpan = TimeSpan.FromMinutes(5);
            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20);
            SystemClock = new SystemClock();
        }

        /// <summary>The period of time the access token remains valid after being issued. The default is twenty minutes.
        /// The client application is expected to refresh or acquire a new access token after the token has expired.</summary>
        /// <value>The access token expire time span.</value>
        public TimeSpan AccessTokenExpireTimeSpan
        {
            get;
            set;
        }

        /// <summary>The data format used to protect the information contained in the access token. If not provided by
        /// the application the default data protection provider depends on the host server. The SystemWeb host on IIS
        /// will use ASP.NET machine key data protection, and HttpListener and other self-hosted servers will use DPAPI
        /// data protection. If a different access token provider or format is assigned, a compatible instance must be
        /// assigned to the OAuthBearerAuthenticationOptions.AccessTokenProvider or
        /// OAuthBearerAuthenticationOptions.AccessTokenFormat property of the resource server.</summary>
        /// <value>The access token format.</value>
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat
        {
            get;
            set;
        }

        /// <summary>Produces a bearer token the client application will typically be providing to resource server as the
        /// authorization bearer http request header. If not provided the token produced on the server's default data
        /// protection. If a different access token provider or format is assigned, a compatible instance must be
        /// assigned to the OAuthBearerAuthenticationOptions.AccessTokenProvider or
        /// OAuthBearerAuthenticationOptions.AccessTokenFormat property of the resource server.</summary>
        /// <value>The access token provider.</value>
        public IAuthenticationTokenProvider AccessTokenProvider
        {
            get;
            set;
        }

        /// <summary>True to allow authorize and token requests to arrive on http URI addresses, and to allow incoming
        /// redirect_uri authorize request parameter to have http URI addresses.</summary>
        /// <value>True if allow insecure http, false if not.</value>
        public bool AllowInsecureHttp
        {
            get;
            set;
        }

        /// <summary>Set to true if the web application is able to render error messages on the /Authorize endpoint. This
        /// is only needed for cases where the browser is not redirected back to the client application, for example,
        /// when the client_id or redirect_uri are incorrect. The /Authorize endpoint should expect to see "oauth.Error",
        /// "oauth.ErrorDescription", "oauth.ErrorUri" properties added to the owin environment.</summary>
        /// <value>True if application can display errors, false if not.</value>
        public bool ApplicationCanDisplayErrors
        {
            get;
            set;
        }

        /// <summary>The period of time the authorization code remains valid after being issued. The default is five
        /// minutes. This time span must also take into account clock synchronization between servers in a web farm, so
        /// a very brief value could result in unexpectedly expired tokens.</summary>
        /// <value>The authorization code expire time span.</value>
        public TimeSpan AuthorizationCodeExpireTimeSpan
        {
            get;
            set;
        }

        /// <summary>The data format used to protect and unprotect the information contained in the authorization code.
        /// If not provided by the application the default data protection provider depends on the host server. The
        /// SystemWeb host on IIS will use ASP.NET machine key data protection, and HttpListener and other self-hosted
        /// servers will use DPAPI data protection.</summary>
        /// <value>The authorization code format.</value>
        public ISecureDataFormat<AuthenticationTicket> AuthorizationCodeFormat
        {
            get;
            set;
        }

        /// <summary>Produces a single-use authorization code to return to the client application. For the OAuth server
        /// to be secure the application MUST provide an instance for AuthorizationCodeProvider where the token produced
        /// by the OnCreate or OnCreateAsync event is considered valid for only one call to OnReceive or OnReceiveAsync.</summary>
        /// <value>The authorization code provider.</value>
        public IAuthenticationTokenProvider AuthorizationCodeProvider
        {
            get;
            set;
        }

        /// <summary>The request path where client applications will redirect the user-agent in order to obtain user
        /// consent to issue a token. Must begin with a leading slash, like "/Authorize".</summary>
        /// <value>The full pathname of the authorize endpoint file.</value>
        public PathString AuthorizeEndpointPath
        {
            get;
            set;
        }

        /// <summary>Endpoint responsible for Form Post Response Mode See also,
        /// http://openid.net/specs/oauth-v2-form-post-response-mode-1_0.html.</summary>
        /// <value>The form post endpoint.</value>
        public PathString FormPostEndpoint
        {
            get;
            set;
        }

        /// <summary>The object provided by the application to process events raised by the Authorization Server
        /// middleware. The application may implement the interface fully, or it may create an instance of
        /// OAuthAuthorizationServerProvider and assign delegates only to the events it wants to process.</summary>
        /// <value>The provider.</value>
        public IOAuthAuthorizationServerProvider Provider
        {
            get;
            set;
        }

        /// <summary>The data format used to protect and unprotect the information contained in the refresh token. If not
        /// provided by the application the default data protection provider depends on the host server. The SystemWeb
        /// host on IIS will use ASP.NET machine key data protection, and HttpListener and other self-hosted servers
        /// will use DPAPI data protection.</summary>
        /// <value>The refresh token format.</value>
        public ISecureDataFormat<AuthenticationTicket> RefreshTokenFormat
        {
            get;
            set;
        }

        /// <summary>Produces a refresh token which may be used to produce a new access token when needed. If not
        /// provided the authorization server will not return refresh tokens from the /Token endpoint.</summary>
        /// <value>The refresh token provider.</value>
        public IAuthenticationTokenProvider RefreshTokenProvider
        {
            get;
            set;
        }

        /// <summary>Used to know what the current clock time is when calculating or validating token expiration. When
        /// not assigned default is based on DateTimeOffset.UtcNow. This is typically needed only for unit testing.</summary>
        /// <value>The system clock.</value>
        public ISystemClock SystemClock
        {
            get;
            set;
        }

        /// <summary>The request path client applications communicate with directly as part of the OAuth protocol. Must
        /// begin with a leading slash, like "/Token". If the client is issued a client_secret, it must be provided to
        /// this endpoint.</summary>
        /// <value>The full pathname of the token endpoint file.</value>
        public PathString TokenEndpointPath
        {
            get;
            set;
        }
    }
}
