// <copyright file="TokenizedCredentialsAuthProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tokenized credentials authentication provider class</summary>
#pragma warning disable SA1600 // Elements should be documented
namespace ServiceStack.Auth
{
    using System;
    using System.Collections.Generic;
#if NET5_0_OR_GREATER
    using System.Threading;
    using System.Threading.Tasks;
#endif
    using System.Web;
    using Clarity.Ecommerce.JSConfigs;
    using Configuration;
    using JetBrains.Annotations;
    using ServiceStack;
    using Web;

    /// <summary>A Tokenized credentials authentication provider.</summary>
    /// <seealso cref="CEFAuthProviderBase"/>
    [PublicAPI]
    public class TokenizedCredentialsAuthProvider : CEFAuthProviderBase
    {
        /// <summary>Initializes a new instance of the <see cref="TokenizedCredentialsAuthProvider"/> class.</summary>
        public TokenizedCredentialsAuthProvider()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TokenizedCredentialsAuthProvider"/> class.</summary>
        /// <param name="appSettings">The application settings.</param>
        public TokenizedCredentialsAuthProvider(IAppSettings appSettings)
            : this(appSettings, StaticRealm, StaticName)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TokenizedCredentialsAuthProvider"/> class.</summary>
        /// <param name="appSettings">  The application settings.</param>
        /// <param name="authRealm">    The authentication realm.</param>
        /// <param name="oAuthProvider">The authentication provider.</param>
        public TokenizedCredentialsAuthProvider(
            IAppSettings appSettings,
            string authRealm,
            string oAuthProvider)
            : base(appSettings, authRealm, oAuthProvider)
        {
        }

        /// <summary>Gets the name of the static.</summary>
        /// <value>The name of the static.</value>
        public static string StaticName => "tokenized";

        /// <summary>Gets the static realm.</summary>
        /// <value>The static realm.</value>
        public static string StaticRealm => "auth/tokenized";

        /// <inheritdoc/>
        public override string Name => "tokenized";

        /// <inheritdoc/>
        public override string Realm => "auth/tokenized";

#if NET5_0_OR_GREATER
        /// <summary>Authenticates based on the values provided.</summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="session">    The session.</param>
        /// <param name="request">    The request.</param>
        /// <param name="token">      The cancellation token.</param>
        /// <returns>An object.</returns>
        public override async Task<object> AuthenticateAsync(
            IServiceBase authService,
            IAuthSession session,
            Authenticate request,
            CancellationToken token = default)
#else
        /// <summary>Authenticates based on the values provided.</summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="session">    The session.</param>
        /// <param name="request">    The request.</param>
        /// <returns>An object.</returns>
        public override object Authenticate(
            IServiceBase authService,
            IAuthSession session,
            Authenticate request)
#endif
        {
            try
            {
                var alternativeTokenCheckUrlFormat = CEFConfigDictionary.AuthProviderTokenizedAlternativeCheckUrl;
                if (string.IsNullOrWhiteSpace(alternativeTokenCheckUrlFormat))
                {
                    throw new InvalidOperationException("Could not locate token check url. Please contact Support for assistance.");
                }
                var alternativeTokenCheckUrl = string.Format(alternativeTokenCheckUrlFormat, HttpUtility.UrlEncode(request.Password));
                var webClient = new System.Net.WebClient();
                var validationResult = webClient.DownloadString(alternativeTokenCheckUrl);
                const string PassResultMatch = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<boolean xmlns=\"http://tempuri.org/\">true</boolean>";
                if (validationResult != PassResultMatch)
                {
                    throw new UnauthorizedAccessException();
                }
#if NET5_0_OR_GREATER
                await authService.SaveSessionAsync(session, SessionExpiry, token: token).ConfigureAwait(false);
#else
                authService.SaveSession(session, SessionExpiry);
#endif
                session.UserAuthName ??= request.UserName;
#if NET5_0_OR_GREATER
                var response = await OnAuthenticatedAsync(authService, session, null!, null!, token: token).ConfigureAwait(false);
#else
                var response = OnAuthenticated(authService, session, null, null);
#endif
                if (response != null!)
                {
                    return response;
                }
                return new AuthenticateResponse
                {
                    UserId = "-1",
                    UserName = request.UserName,
                    SessionId = session.Id,
                    ReferrerUrl = RedirectUrl,
                };
            }
            catch (Exception)
            {
                session.IsAuthenticated = false;
                session.UserName = null;
                session.Id = null;
                session.UserAuthId = null;
                session.UserAuthName = null;
#if NET5_0_OR_GREATER
                await authService.SaveSessionAsync(session, token: token).ConfigureAwait(false);
#else
                authService.SaveSession(session);
#endif
                throw;
            }
        }

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
                LoadUserAuthFilter?.Invoke(userSession, tokens, authInfo);
            }
            // Step 2: Get the Repository of Users
#if NET5_0_OR_GREATER
            var authRepo = authService.TryResolve<IAuthRepositoryAsync>();
#else
            var authRepo = authService.TryResolve<IAuthRepository>();
#endif
            if (CustomValidationFilter != null)
            {
                var ctx = new AuthContext
                {
                    Service = authService,
                    AuthProvider = this,
                    Session = session,
                    AuthTokens = tokens,
                    AuthInfo = authInfo,
#if NET5_0_OR_GREATER
                    AuthRepositoryAsync = authRepo,
#else
                    AuthRepository = authRepo,
#endif
                };
                var response = CustomValidationFilter(ctx);
                if (response != null)
                {
                    authService.RemoveSession();
                    return response;
                }
            }
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
