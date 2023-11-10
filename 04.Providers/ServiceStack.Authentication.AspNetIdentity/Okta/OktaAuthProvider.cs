// <copyright file="OktaAuthProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Okta authentication provider class</summary>
// ReSharper disable NotAccessedVariable, StyleCop.SA1202, UnusedVariable
#pragma warning disable IDE0060
namespace ServiceStack.Auth
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
#if NET5_0_OR_GREATER
    using System.Threading;
    using System.Threading.Tasks;
#else
    using System.Web;
    using System.Web.Configuration;
    using ComponentSpace.SAML2;
#endif
    using Clarity.Ecommerce;
    using Clarity.Ecommerce.Interfaces.Models;
    using Configuration;
    using JetBrains.Annotations;

    /// <summary>An okta authentication provider.</summary>
    /// <seealso cref="CEFAuthProviderBase"/>
    [PublicAPI]
    public class OktaAuthProvider : CEFAuthProviderBase
    {
        /// <summary>Initializes a new instance of the <see cref="OktaAuthProvider"/> class.</summary>
        public OktaAuthProvider()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OktaAuthProvider"/> class.</summary>
        /// <param name="appSettings">The application settings.</param>
        public OktaAuthProvider(IAppSettings appSettings)
            : this(appSettings, StaticRealm, StaticName)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OktaAuthProvider"/> class.</summary>
        /// <param name="appSettings">  The application settings.</param>
        /// <param name="authRealm">    The authentication realm.</param>
        /// <param name="oAuthProvider">The authentication provider.</param>
        protected OktaAuthProvider(IAppSettings appSettings, string authRealm, string oAuthProvider)
            : base(appSettings, authRealm, oAuthProvider)
        {
        }

        public static string StaticName => "okta";

        public static string StaticRealm => "/auth/okta";

        /// <inheritdoc/>
        public override string Name => StaticName;

        /// <inheritdoc/>
        public override string Realm => StaticRealm;

        /// <inheritdoc/>
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
#if NET5_0_OR_GREATER
        public override async Task<object> AuthenticateAsync(
            IServiceBase authService,
            IAuthSession session,
            Authenticate request,
            CancellationToken token = default)
#else
        public override object Authenticate(
            IServiceBase authService,
            IAuthSession session,
            Authenticate request)
#endif
        {
            var tokens = Init(authService, ref session);
            if (authService.Request.Verb == "POST")
            {
                // Receive and process the SAML assertion contained in the SAML response.
                // The SAML response is received either as part of IdP-initiated or SP-initiated SSO.
#if NET5_0_OR_GREATER
                // TODO: Find a .NET5 implementation
                var userName = string.Empty;
#else
                SAMLServiceProvider.ReceiveSSO(
                    HttpContext.Current.Request,
                    out var isInResponseTo,
                    out var partnerIdP,
                    out var authnContext,
                    out var userName,
                    out IDictionary<string, string> attributes,
                    out _);
#endif
                // If no target URL is provided, provide a default.
                var authInfo = new Dictionary<string, string>
                {
                    { "username", userName },
                };
                using var context = RegistryLoaderWrapper.GetContext(null);
#if NET5_0_OR_GREATER
                var userId = await context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterUsersByUserName(userName, true)
                    .Select(x => x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
#else
                var userId = context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterUsersByUserName(userName, true)
                    .Select(x => x.ID)
                    .SingleOrDefault();
#endif
                tokens.UserId = userId.ToString();
                session.UserAuthId = userId.ToString();
                tokens.UserName = authInfo["username"];
                session.UserName = authInfo["username"];
                session.UserAuthName = authInfo["username"];
#if NET5_0_OR_GREATER
                var response = await OnAuthenticatedAsync(authService, session, tokens, authInfo, token).ConfigureAwait(false);
                await authService.SaveSessionAsync(session, SessionExpiry, token).ConfigureAwait(false);
#else
                var response = OnAuthenticated(authService, session, tokens, authInfo);
                authService.SaveSession(session, SessionExpiry);
#endif
                return response;
            }
            else
            {
#if NET5_0_OR_GREATER
                // TODO: Locate .NET 5 implementation
#else
                var partnerIdP = WebConfigurationManager.AppSettings["PartnerIdP"];
                SAMLServiceProvider.InitiateSSO(HttpContext.Current.Response, null, partnerIdP);
#endif
                return new HttpResult();
            }
        }

        /// <summary>Initializes this OktaAuthProvider.</summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="session">    The session.</param>
        /// <returns>The IAuthTokens.</returns>
        protected IAuthTokens Init(IServiceBase authService, ref IAuthSession session)
        {
            var requestUri = authService.Request.AbsoluteUri;
            if (this.CallbackUrl.IsNullOrEmpty())
            {
                this.CallbackUrl = requestUri;
            }
            session.ReferrerUrl = "https://cmmc-local.clarityclient.com";
            var tokens = session.GetAuthTokens(this.Provider);
            if (tokens == null)
            {
                session.AddAuthToken(tokens = new AuthTokens { Provider = this.Provider });
            }
            return tokens;
        }
    }
}
