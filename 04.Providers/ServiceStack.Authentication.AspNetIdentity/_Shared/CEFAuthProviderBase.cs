// <copyright file="CEFAuthProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF authentication provider base class</summary>
// ReSharper disable StyleCop.SA1202
#pragma warning disable 1584
namespace ServiceStack.Auth
{
    using System.Collections.Generic;
    using System.Globalization;
#if NET5_0_OR_GREATER
    using System.Threading;
#endif
    using System.Threading.Tasks;
    using Clarity.Ecommerce;
    using Clarity.Ecommerce.DataModel;
    using Clarity.Ecommerce.JSConfigs;
    using Clarity.Ecommerce.Utilities;
    using Configuration;
    using FluentValidation;
    using JetBrains.Annotations;
    using Web;

    /// <summary>An ASP net identity authentication provider.</summary>
    /// <seealso cref="AuthProvider"/>
    /// <seealso cref="ICEFAuthProvider"/>
    [PublicAPI]
    public abstract class CEFAuthProviderBase : AuthProvider, ICEFAuthProvider
    {
        /// <summary>(Immutable) Name of the session cookie.</summary>
        public const string SessionCookieName = "ss-pid";

        /// <summary>(Immutable) Name of the session header.</summary>
        public const string SessionHeaderName = "X-ss-pid";

        /// <summary>(Immutable) Name of the session cookie option.</summary>
        public const string SessionCookieOptName = "ss-opt";

        /// <summary>(Immutable) Name of the session header option.</summary>
        public const string SessionHeaderOptName = "X-ss-opt";

        /// <summary>(Immutable) The session option permission.</summary>
        public const string SessionOptPerm = "perm";

        /// <summary>Initializes a new instance of the <see cref="CEFAuthProviderBase"/> class.</summary>
        protected CEFAuthProviderBase()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Provider = Name;
        }

        /// <summary>Initializes a new instance of the <see cref="CEFAuthProviderBase"/> class.</summary>
        /// <param name="appSettings">  The application settings.</param>
        /// <param name="authRealm">    The authentication realm.</param>
        /// <param name="oAuthProvider">The authentication provider.</param>
        protected CEFAuthProviderBase(
            IAppSettings appSettings,
            string authRealm,
            string oAuthProvider)
            : base(appSettings, authRealm, oAuthProvider)
        {
            Provider = oAuthProvider;
        }

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>Gets the realm.</summary>
        /// <value>The realm.</value>
        public abstract string Realm { get; }

        /// <inheritdoc/>
#if NET5_0_OR_GREATER
        public virtual async Task<bool> TryAuthenticateAsync(
            IServiceBase authService,
            string userName,
            string password,
            CancellationToken token = default)
#else
        public virtual bool TryAuthenticate(
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
        public override bool IsAuthorized(IAuthSession session, IAuthTokens tokens, Authenticate? request = null)
        {
            if (request != null && !LoginMatchesSession(session, request.UserName))
            {
                return false;
            }
            return session.IsAuthenticated && !session.UserAuthName.IsNullOrEmpty();
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
                    token: token)
                .ConfigureAwait(false);
#else
            new IdentityAuthValidator().ValidateAndThrow(request);
            return Authenticate(
                authService: authService,
                session: session,
                userName: request.UserName,
                password: request.Password,
                referrerUrl: request.Continue);
#endif
        }
#pragma warning restore AsyncFixer01 // Unnecessary async/await usage

        /// <inheritdoc cref="AuthProvider"/>
#if NET5_0_OR_GREATER
        public override async Task<IHttpResult> OnAuthenticatedAsync(
            IServiceBase authService,
            IAuthSession session,
            IAuthTokens tokens,
            Dictionary<string, string> authInfo,
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

        /// <inheritdoc/>
#if NET5_0_OR_GREATER
        public override async Task<object> LogoutAsync(IServiceBase authService, Authenticate request, CancellationToken token = default)
#else
        public override object Logout(IServiceBase authService, Authenticate request)
#endif
        {
#if NET5_0_OR_GREATER
            var session = await authService.GetSessionAsync(token: token).ConfigureAwait(false);
            var referrerUrl = request?.uri/*.Continue*/
#else
            var session = authService.GetSession();
            var referrerUrl = request?.Continue
#endif
                ?? session.ReferrerUrl
                ?? authService.Request.GetHeader("Referer")
                ?? CallbackUrl;
            var userID = int.Parse(session.UserAuthId);
            session.OnLogout(authService);
            AuthEvents.OnLogout(authService.Request, session, authService);
#if NET5_0_OR_GREATER
            await authService.RemoveSessionAsync(token: token).ConfigureAwait(false);
#else
            authService.RemoveSession();
#endif
            var feature = HostContext.GetPlugin<AuthFeature>();
            if (feature?.DeleteSessionCookiesOnLogout == true)
            {
                authService.Request.Response.DeleteSessionCookies();
            }
            var awaiter = RemoveLoginsAsync(userID, Name).GetAwaiter();
            _ = awaiter.GetResult();
            if (authService.Request.ResponseContentType == MimeTypes.Html
                && !string.IsNullOrEmpty(referrerUrl))
            {
#if NET5_0_OR_GREATER
                return authService.Redirect(LogoutUrlFilter(CreateAuthContext(authService, session), referrerUrl.SetParam("s", "-1")));
#else
                return authService.Redirect(LogoutUrlFilter(this, referrerUrl.SetParam("s", "-1")));
#endif
            }
            return new AuthenticateResponse();
        }

#if NET5_0_OR_GREATER
        /// <summary>Validates the account.</summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="authRepo">   The authentication repository.</param>
        /// <param name="session">    The session.</param>
        /// <param name="tokens">     The tokens.</param>
        /// <param name="token">      The cancellation token.</param>
        /// <returns>An IHttpResult.</returns>
        protected override async Task<IHttpResult?> ValidateAccountAsync(
            IServiceBase authService,
            IAuthRepositoryAsync authRepo,
            IAuthSession session,
            IAuthTokens? tokens,
            CancellationToken token = default)
        {
            var userAuth2 = await authRepo.GetUserAuthAsync(session, tokens, token: token).ConfigureAwait(false);
            var ctx = CreateAuthContext(authService, session);
            var authFeature2 = HostContext.GetPlugin<AuthFeature>();
            if (authFeature2?.ValidateUniqueUserNames == true && await UserNameAlreadyExistsAsync(authRepo, userAuth2, tokens, token).ConfigureAwait(false))
            {
                return authService.Redirect(FailedRedirectUrlFilter(ctx, GetReferrerUrl(authService, session).SetParam("f", "UserNameAlreadyExists")));
            }
            var userAuth = await authRepo.GetUserAuthAsync(session, tokens, token: token).ConfigureAwait(false);
            var authFeature = HostContext.GetPlugin<AuthFeature>();
            if (authFeature?.ValidateUniqueUserNames == true && await UserNameAlreadyExistsAsync(authRepo, userAuth, tokens, token).ConfigureAwait(false))
            {
                return authService.Redirect(FailedRedirectUrlFilter(ctx, GetReferrerUrl(authService, session).SetParam("f", "UserNameAlreadyExists")));
            }
            if (authFeature?.ValidateUniqueEmails == true && await EmailAlreadyExistsAsync(authRepo, userAuth, tokens, token).ConfigureAwait(false))
            {
                return authService.Redirect(FailedRedirectUrlFilter(ctx, GetReferrerUrl(authService, session).SetParam("f", "EmailAlreadyExists")));
            }
            /*if (IsAccountLocked(authRepo, userAuth, tokens))
            {
                return authService.Redirect(FailedRedirectUrlFilter(ctx, GetReferrerUrl(authService, session).SetParam("f", "AccountLocked")));
            }*/
            return null!;
        }
#else
        /// <summary>Validates the account.</summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="authRepo">   The authentication repository.</param>
        /// <param name="session">    The session.</param>
        /// <param name="tokens">     The tokens.</param>
        /// <returns>An IHttpResult.</returns>
        protected override IHttpResult? ValidateAccount(
            IServiceBase authService,
            IAuthRepository authRepo,
            IAuthSession session,
            IAuthTokens? tokens)
        {
            var userAuth2 = authRepo.GetUserAuth(session, tokens);
            var authFeature2 = HostContext.GetPlugin<AuthFeature>();
            if (authFeature2?.ValidateUniqueUserNames == true && UserNameAlreadyExists(authRepo, userAuth2, tokens))
            {
                return authService.Redirect(FailedRedirectUrlFilter(this, GetReferrerUrl(authService, session).SetParam("f", "UserNameAlreadyExists")));
            }
            var userAuth = authRepo.GetUserAuth(session, tokens);
            var authFeature = HostContext.GetPlugin<AuthFeature>();
            if (authFeature?.ValidateUniqueUserNames == true && UserNameAlreadyExists(authRepo, userAuth, tokens))
            {
                return authService.Redirect(FailedRedirectUrlFilter(this, GetReferrerUrl(authService, session).SetParam("f", "UserNameAlreadyExists")));
            }
            if (authFeature?.ValidateUniqueEmails == true && EmailAlreadyExists(authRepo, userAuth, tokens))
            {
                return authService.Redirect(FailedRedirectUrlFilter(this, GetReferrerUrl(authService, session).SetParam("f", "EmailAlreadyExists")));
            }
            /*if (IsAccountLocked(authRepo, userAuth, tokens))
            {
                return authService.Redirect(FailedRedirectUrlFilter(this, GetReferrerUrl(authService, session).SetParam("f", "AccountLocked")));
            }*/
            return null;
        }
#endif

        /* IsAccountLocked is now `internal virtual` in SS, created a pull request for them to restore to protected
        /// <summary>Query if 'authRepo' is account locked.</summary>
        /// <exception cref="HttpError">Thrown when a HTTP error error condition occurs.</exception>
        /// <param name="authRepo">The authentication repository.</param>
        /// <param name="userAuth">The user authentication.</param>
        /// <param name="tokens">  The tokens.</param>
        /// <returns>true if account locked, false if not.</returns>
        protected override bool IsAccountLocked(IAuthRepository authRepo, IUserAuth userAuth, IAuthTokens tokens = null)
        {
            if (userAuth == null) { return false; }
            try
            {
                using (var context = RegistryLoaderWrapper.GetContext(null))
                {
                    using (var userStore = new UserStore(context))
                    {
                        using (var userManager = new UserManager<User, int>(userStore))
                        {
                            var user = userManager.Users.SingleOrDefault(x => x.UserName == userAuth.UserName);
                            if (user == null)
                            {
                                throw new HttpError(HttpStatusCode.BadRequest, "102", "User not found");
                            }
                            return user.LockoutEnabled || base.IsAccountLocked(authRepo, userAuth, tokens);
                        }
                    }
                }
            }
            catch
            {
                return base.IsAccountLocked(authRepo, userAuth, tokens);
            }
        }//*/

        /// <summary>Adds a login asynchronous.</summary>
        /// <param name="userID">      Identifier for the user.</param>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="providerKey"> The provider key.</param>
        /// <returns>A Task{bool}</returns>
        protected virtual async Task<bool> AddLoginAsync(
            int userID,
            string providerName,
            string providerKey)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(null);
                using var userStore = new CEFUserStore(context);
                using var userManager = new CEFUserManager(userStore);
                if (!userManager.SupportsUserLogin)
                {
                    return true;
                }
                var login = new Microsoft.AspNet.Identity.UserLoginInfo(providerName, providerKey);
                var addResult = await userManager.AddLoginAsync(userID, login).ConfigureAwait(false);
                if (addResult.Succeeded)
                {
                    return true;
                }
                System.Diagnostics.Debug.WriteLine(
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        addResult));
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Removes the logins asynchronous.</summary>
        /// <param name="userID">      Identifier for the user.</param>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns>A Task{bool}</returns>
        protected virtual async Task<bool> RemoveLoginsAsync(int userID, string providerName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(null);
                using var userStore = new CEFUserStore(context);
                using var userManager = new CEFUserManager(userStore);
                if (!userManager.SupportsUserLogin)
                {
                    return true;
                }
                var logins = await userManager.GetLoginsAsync(userID).ConfigureAwait(false);
                if (!Contract.CheckNotEmpty(logins))
                {
                    return true;
                }
                for (var i = 0; i < logins.Count;)
                {
                    if (logins[i].LoginProvider != providerName)
                    {
                        i++;
                        continue;
                    }
                    var removeResult = await userManager.RemoveLoginAsync(
                            userID,
                            logins[i])
                        .ConfigureAwait(false);
                    if (removeResult.Succeeded)
                    {
                        logins.RemoveAt(i);
                        continue;
                    }
                    System.Diagnostics.Debug.WriteLine(
                        Newtonsoft.Json.JsonConvert.SerializeObject(removeResult));
                    i++;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

#if NET5_0_OR_GREATER
        /// <summary>The entry point for all AuthProvider providers. Runs inside the AuthService so exceptions are
        /// treated normally. Overridable so you can provide your own Auth implementation.</summary>
        /// <param name="authService">      The authentication service.</param>
        /// <param name="session">          The session.</param>
        /// <param name="userName">         Name of the user.</param>
        /// <param name="password">         The password.</param>
        /// <param name="referrerUrl">      URL of the referrer.</param>
        /// <param name="originalSessionID">Identifier for the original session.</param>
        /// <param name="token">            The cancellation token.</param>
        /// <returns>An object.</returns>
        protected async Task<object> AuthenticateAsync(
            IServiceBase authService,
            IAuthSession session,
            string userName,
            string password,
            string referrerUrl,
            string? originalSessionID = null,
            CancellationToken token = default)
#else
        /// <summary>The entry point for all AuthProvider providers. Runs inside the AuthService so exceptions are
        /// treated normally. Overridable so you can provide your own Auth implementation.</summary>
        /// <param name="authService">      The authentication service.</param>
        /// <param name="session">          The session.</param>
        /// <param name="userName">         Name of the user.</param>
        /// <param name="password">         The password.</param>
        /// <param name="referrerUrl">      URL of the referrer.</param>
        /// <param name="originalSessionID">Identifier for the original session.</param>
        /// <returns>An object.</returns>
        protected object Authenticate(
            IServiceBase authService,
            IAuthSession session,
            string userName,
            string password,
            string referrerUrl,
            string? originalSessionID = null)
#endif
        {
            if (Contract.CheckValidKey(originalSessionID))
            {
                session.Id = originalSessionID;
            }
            if (!LoginMatchesSession(session, userName))
            {
                authService.RemoveSession();
                session = authService.GetSession();
            }
#if NET5_0_OR_GREATER
            if (!await TryAuthenticateAsync(authService, userName, password, token: token).ConfigureAwait(false))
#else
            if (!TryAuthenticate(authService, userName, password))
#endif
            {
                throw HttpError.Unauthorized(ErrorMessages.InvalidUsernameOrPassword);
            }
            session.IsAuthenticated = true;
            session.UserAuthName ??= userName;
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
                UserId = session.UserAuthId,
                UserName = userName,
                SessionId = session.Id,
                ReferrerUrl = referrerUrl,
            };
        }

        /// <summary>An identity authentication validator.</summary>
        /// <seealso cref="AbstractValidator{Authenticate}"/>
        protected class IdentityAuthValidator : AbstractValidator<Authenticate>
        {
            public IdentityAuthValidator()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
}
