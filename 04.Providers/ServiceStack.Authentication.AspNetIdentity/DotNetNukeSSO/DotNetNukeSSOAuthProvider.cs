// <copyright file="DotNetNukeSSOAuthProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the DotNetNuke SSO authentication provider class</summary>
namespace ServiceStack.Auth
{
    using System.Collections.Generic;
    using System.Globalization;
#if NET5_0_OR_GREATER
    using System.Threading;
    using System.Threading.Tasks;
#endif
    using Clarity.Ecommerce.JSConfigs;
    using Clarity.Ecommerce.Utilities;
    using Configuration;
    using FluentValidation;
    using JetBrains.Annotations;
    using Web;

    /// <summary>A DotNetNuke SSO authentication provider.</summary>
    /// <seealso cref="CEFAuthProviderBase"/>
    [PublicAPI]
    public class DotNetNukeSSOAuthProvider : CEFAuthProviderBase
    {
        /// <summary>Initializes a new instance of the <see cref="DotNetNukeSSOAuthProvider"/> class.</summary>
        public DotNetNukeSSOAuthProvider()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="DotNetNukeSSOAuthProvider"/> class.</summary>
        /// <param name="appSettings">The application settings.</param>
        public DotNetNukeSSOAuthProvider(IAppSettings appSettings)
            : base(appSettings, StaticRealm, StaticName)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="DotNetNukeSSOAuthProvider"/> class.</summary>
        /// <param name="appSettings">  The application settings.</param>
        /// <param name="authRealm">    The authentication realm.</param>
        /// <param name="oAuthProvider">The authentication provider.</param>
        protected DotNetNukeSSOAuthProvider(
            IAppSettings appSettings,
            string authRealm,
            string oAuthProvider)
            : base(appSettings, authRealm, oAuthProvider)
        {
        }

        /// <summary>Gets the name of the static.</summary>
        /// <value>The name of the static.</value>
        public static string StaticName => StaticRealm;

        /// <summary>Gets the static realm.</summary>
        /// <value>The static realm.</value>
        public static string StaticRealm => "/auth/dnnSSO";

        /// <inheritdoc/>
        public override string Name => StaticName;

        /// <inheritdoc/>
        public override string Realm => "/auth/dnnSSO";

        /// <inheritdoc/>
#if NET5_0_OR_GREATER
        public override async Task<bool> TryAuthenticateAsync(
            IServiceBase authService,
            string userName,
            string password,
            CancellationToken token = default)
#else
        public override bool TryAuthenticate(
            IServiceBase authService,
            string userName,
            string password)
#endif
        {
            // Step 1: Get the Repository of Users
#if NET5_0_OR_GREATER
            var authRepo = authService.TryResolve<IAuthRepositoryAsync>();
#else
            var authRepo = authService.TryResolve<IAuthRepository>();
#endif
            // Step 2: Get the current Session
            var session = authService.GetSession();
            // Step 3: Make sure the user exists and the password checks out
#if NET5_0_OR_GREATER
            var userAuth = await authRepo.TryAuthenticateAsync(userName, password, token: token).ConfigureAwait(false);
            if (userAuth == null)
#else
            if (!authRepo.TryAuthenticate(userName, password, out var userAuth))
#endif
            {
                return false;
            }
            /*// Step 4: Make sure the account isn't deactivated or locked out
            if (IsAccountLocked(authRepo, userAuth))
            {
                throw new AuthenticationException("This account has been locked");
            }*/
            // Step 5: Success! Apply the user's information to the Session
            if (Contract.CheckValidKey(((Authenticate)authService.Request.Dto).AccessToken))
            {
                session.Id = ((Authenticate)authService.Request.Dto).AccessToken;
            }
            var holdSessionId = session.Id;
            session.PopulateWith(userAuth); // overwrites session.Id
            session.Id = holdSessionId;
            session.IsAuthenticated = true;
            session.UserAuthId = userAuth.Id.ToString(CultureInfo.InvariantCulture);
#if NET5_0_OR_GREATER
            session.ProviderOAuthAccess = (await authRepo.GetUserAuthDetailsAsync(session.UserAuthId, token).ConfigureAwait(false))
#else
            session.ProviderOAuthAccess = authRepo.GetUserAuthDetails(session.UserAuthId)
#endif
                .ConvertAll(x => (IAuthTokens)x);
            // Step 6: Return that we were successful
            return true;
        }

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
#if NET5_0_OR_GREATER
            await new IdentityAuthValidator().ValidateAndThrowAsync(request, token).ConfigureAwait(false);
            return await AuthenticateAsync(
                    authService,
                    session,
                    request.UserName,
                    request.Password,
                    request.uri,
                    originalSessionID: request.AccessToken,
                    token: token)
                .ConfigureAwait(false);
#else
            new IdentityAuthValidator().ValidateAndThrow(request);
            return Authenticate(
                authService: authService,
                session: session,
                userName: request.UserName,
                password: request.Password,
                referrerUrl: request.Continue,
                originalSessionID: request.AccessToken);
#endif
        }
#pragma warning restore AsyncFixer01 // Unnecessary async/await usage

        /// <inheritdoc cref="AuthProvider"/>
#if NET5_0_OR_GREATER
        public override async Task<IHttpResult> OnAuthenticatedAsync(
            IServiceBase authService,
            IAuthSession session,
            IAuthTokens? tokens,
            Dictionary<string, string>? authInfo,
            CancellationToken token = default)
#else
        public override IHttpResult OnAuthenticated(
            IServiceBase authService,
            IAuthSession session,
            IAuthTokens? tokens,
            Dictionary<string, string>? authInfo)
#endif
        {
            // Step 1: Get the session into a convenient variable of the correct Type and validate it
            if (session is AuthUserSession userSession)
            {
#if NET5_0_OR_GREATER
                await LoadUserAuthInfoAsync(userSession, tokens, authInfo, token).ConfigureAwait(false);
#else
                LoadUserAuthInfo(userSession, tokens, authInfo);
#endif
                HostContext.TryResolve<IAuthMetadataProvider>().SafeAddMetadata(tokens, authInfo);
            }
            // Step 2: Get the Repository of Users
#if NET5_0_OR_GREATER
            var authRepo = authService.TryResolve<IAuthRepositoryAsync>();
#else
            var authRepo = authService.TryResolve<IAuthRepository>();
#endif
            if (authRepo != null)
            {
                if (tokens != null)
                {
                    authInfo.ForEach((x, y) => tokens.Items[x] = y);
#if NET5_0_OR_GREATER
                    session.UserAuthId = (await authRepo.CreateOrMergeAuthSessionAsync(session, tokens, token).ConfigureAwait(false)).UserAuthId.ToString();
#else
                    session.UserAuthId = authRepo.CreateOrMergeAuthSession(session, tokens).UserAuthId.ToString();
#endif
                }
                foreach (var oAuthToken in session.ProviderOAuthAccess)
                {
                    var authProvider = AuthenticateService.GetAuthProvider(oAuthToken.Provider);
                    var userAuthProvider = authProvider as OAuthProvider;
                    userAuthProvider?.LoadUserOAuthProvider(session, oAuthToken);
                }
                // Step 3: Set up the Cookie and Validate the Account
                var httpRes = authService.Request.Response as IHttpResponse;
                httpRes?.Cookies.AddPermanentCookie(
                    HttpHeaders.XUserAuthId,
                    session.UserAuthId,
                    CEFConfigDictionary.CookiesRequireSecure);
                httpRes?.AddHeader(HttpHeaders.XUserAuthId, session.UserAuthId);
                httpRes?.AddHeader(SessionHeaderOptName, SessionOptPerm);
                httpRes?.AddHeader(SessionHeaderName, session.Id);
#if NET5_0_OR_GREATER
                var failed = await ValidateAccountAsync(authService, authRepo, session, tokens, token: token).ConfigureAwait(false);
#else
                var failed = ValidateAccount(authService, authRepo, session, tokens);
#endif
                if (failed != null)
                {
                    return failed;
                }
            }
            // Step 4: Try to apply the authenticated state data to the session and fire events
            try
            {
                session.IsAuthenticated = true;
                session.OnAuthenticated(authService, session, tokens, authInfo);
                AuthEvents.OnAuthenticated(authService.Request, session, authService, tokens, authInfo);
                var userID = int.Parse(session.UserAuthId);
                // Clear the current logins for this provider and user
                var awaiter = RemoveLoginsAsync(userID, Name).GetAwaiter();
                _ = awaiter.GetResult();
                // Add a login now
                var awaiterToAddLogin = AddLoginAsync(userID, Name, Realm).GetAwaiter();
                var addResult = awaiterToAddLogin.GetResult();
                if (!addResult)
                {
                    // TODO@JTG: Do something if !addResult
                }
            }
            finally
            {
#if NET5_0_OR_GREATER
                await authService.SaveSessionAsync(session, SessionExpiry, token).ConfigureAwait(false);
#else
                authService.SaveSession(session, SessionExpiry);
#endif
            }
            return null!;
        }
    }
}
