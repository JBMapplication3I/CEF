// <copyright file="CookieAuthenticationHandler.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cookie authentication handler class</summary>
namespace Microsoft.Owin.Security.Cookies
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Infrastructure;
    using Logging;

    /// <summary>A cookie authentication handler.</summary>
    /// <seealso cref="AuthenticationHandler{CookieAuthenticationOptions}"/>
    /// <seealso cref="AuthenticationHandler{CookieAuthenticationOptions}"/>
    internal class CookieAuthenticationHandler : AuthenticationHandler<CookieAuthenticationOptions>
    {
        /// <summary>The header name cache control.</summary>
        private const string HeaderNameCacheControl = "Cache-Control";

        /// <summary>The header name expires.</summary>
        private const string HeaderNameExpires = "Expires";

        /// <summary>The header name pragma.</summary>
        private const string HeaderNamePragma = "Pragma";

        /// <summary>The header value minus one.</summary>
        private const string HeaderValueMinusOne = "-1";

        /// <summary>The header value no cache.</summary>
        private const string HeaderValueNoCache = "no-cache";

        /// <summary>The session identifier claim.</summary>
        private const string SessionIdClaim = "Microsoft.Owin.Security.Cookies-SessionId";

        /// <summary>The logger.</summary>
        private readonly ILogger logger;

        /// <summary>The renew expires UTC.</summary>
        private DateTimeOffset renewExpiresUtc;

        /// <summary>The renew issued UTC.</summary>
        private DateTimeOffset renewIssuedUtc;

        /// <summary>The session key.</summary>
        private string sessionKey;

        /// <summary>True if should renew.</summary>
        private bool shouldRenew;

        /// <summary>Initializes a new instance of the
        /// <see cref="CookieAuthenticationHandler" /> class.</summary>
        /// <param name="logger">The logger.</param>
        public CookieAuthenticationHandler(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode != 401 || !Options.LoginPath.HasValue)
            {
                return Task.FromResult(0);
            }
            var helper = Helper;
            var authenticationResponseChallenge = helper.LookupChallenge(
                Options.AuthenticationType,
                Options.AuthenticationMode);
            try
            {
                if (authenticationResponseChallenge != null)
                {
                    var redirectUri = authenticationResponseChallenge.Properties.RedirectUri;
                    if (string.IsNullOrWhiteSpace(redirectUri))
                    {
                        var pathBase = Request.PathBase + Request.Path + Request.QueryString;
                        redirectUri = string.Concat(
                            Request.Scheme,
                            Uri.SchemeDelimiter,
                            Request.Host,
                            Request.PathBase,
                            Options.LoginPath,
                            new QueryString(Options.ReturnUrlParameter, pathBase));
                    }
                    var cookieApplyRedirectContext = new CookieApplyRedirectContext(Context, Options, redirectUri);
                    Options.Provider.ApplyRedirect(cookieApplyRedirectContext);
                }
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                var cookieExceptionContext = new CookieExceptionContext(
                    Context,
                    Options,
                    CookieExceptionContext.ExceptionLocation.ApplyResponseChallenge,
                    exception,
                    null);
                Options.Provider.Exception(cookieExceptionContext);
                if (cookieExceptionContext.Rethrow)
                {
                    throw;
                }
            }
            return Task.FromResult<object>(null);
        }

        /// <inheritdoc/>
        protected override async Task ApplyResponseGrantAsync()
        {
            var authenticationHandler = this;
            var signin = authenticationHandler.Helper.LookupSignIn(authenticationHandler.Options.AuthenticationType);
            var shouldSignin = signin != null;
            var shouldSignout = authenticationHandler.Helper.LookupSignOut(
                    authenticationHandler.Options.AuthenticationType,
                    authenticationHandler.Options.AuthenticationMode)
                != null;
            if (!(shouldSignin | shouldSignout) && !authenticationHandler.shouldRenew)
            {
                return;
            }
            var model = await authenticationHandler.AuthenticateAsync();
            try
            {
                var cookieOptions = new CookieOptions
                {
                    Domain = authenticationHandler.Options.CookieDomain,
                    HttpOnly = authenticationHandler.Options.CookieHttpOnly,
                    SameSite = authenticationHandler.Options.CookieSameSite,
                    Path = authenticationHandler.Options.CookiePath ?? "/",
                    Secure = authenticationHandler.Options.CookieSecure != CookieSecureOption.SameAsRequest
                        ? authenticationHandler.Options.CookieSecure == CookieSecureOption.Always
                        : authenticationHandler.Request.IsSecure,
                };
                if (shouldSignin)
                {
                    var signInContext = new CookieResponseSignInContext(
                        authenticationHandler.Context,
                        authenticationHandler.Options,
                        authenticationHandler.Options.AuthenticationType,
                        signin.Identity,
                        signin.Properties,
                        cookieOptions);
                    var nullable = signInContext.Properties.IssuedUtc;
                    DateTimeOffset utcNow;
                    if (nullable.HasValue)
                    {
                        nullable = signInContext.Properties.IssuedUtc;
                        utcNow = nullable.Value;
                    }
                    else
                    {
                        utcNow = authenticationHandler.Options.SystemClock.UtcNow;
                        signInContext.Properties.IssuedUtc = utcNow;
                    }
                    nullable = signInContext.Properties.ExpiresUtc;
                    if (!nullable.HasValue)
                    {
                        signInContext.Properties.ExpiresUtc = utcNow.Add(authenticationHandler.Options.ExpireTimeSpan);
                    }
                    authenticationHandler.Options.Provider.ResponseSignIn(signInContext);
                    if (signInContext.Properties.IsPersistent)
                    {
                        nullable = signInContext.Properties.ExpiresUtc;
                        signInContext.CookieOptions.Expires =
                            (nullable ?? utcNow.Add(authenticationHandler.Options.ExpireTimeSpan)).UtcDateTime;
                    }
                    model = new AuthenticationTicket(signInContext.Identity, signInContext.Properties);
                    if (authenticationHandler.Options.SessionStore != null)
                    {
                        if (authenticationHandler.sessionKey != null)
                        {
                            await authenticationHandler.Options.SessionStore.RemoveAsync(
                                authenticationHandler.sessionKey);
                        }
                        var str = await authenticationHandler.Options.SessionStore.StoreAsync(model);
                        authenticationHandler.sessionKey = str;
                        model = new AuthenticationTicket(
                            new ClaimsIdentity(
                                new[]
                                {
                                    new Claim(SessionIdClaim, authenticationHandler.sessionKey),
                                },
                                authenticationHandler.Options.AuthenticationType),
                            null);
                    }
                    var str1 = authenticationHandler.Options.TicketDataFormat.Protect(model);
                    authenticationHandler.Options.CookieManager.AppendResponseCookie(
                        authenticationHandler.Context,
                        authenticationHandler.Options.CookieName,
                        str1,
                        signInContext.CookieOptions);
                    var context = new CookieResponseSignedInContext(
                        authenticationHandler.Context,
                        authenticationHandler.Options,
                        authenticationHandler.Options.AuthenticationType,
                        signInContext.Identity,
                        signInContext.Properties);
                    authenticationHandler.Options.Provider.ResponseSignedIn(context);
                    signInContext = null;
                }
                else if (shouldSignout)
                {
                    if (authenticationHandler.Options.SessionStore != null && authenticationHandler.sessionKey != null)
                    {
                        await authenticationHandler.Options.SessionStore.RemoveAsync(authenticationHandler.sessionKey);
                    }
                    var context = new CookieResponseSignOutContext(
                        authenticationHandler.Context,
                        authenticationHandler.Options,
                        cookieOptions);
                    authenticationHandler.Options.Provider.ResponseSignOut(context);
                    authenticationHandler.Options.CookieManager.DeleteCookie(
                        authenticationHandler.Context,
                        authenticationHandler.Options.CookieName,
                        context.CookieOptions);
                }
                else if (authenticationHandler.shouldRenew)
                {
                    var properties = model.Properties;
                    properties.IssuedUtc = authenticationHandler.renewIssuedUtc;
                    properties.ExpiresUtc = authenticationHandler.renewExpiresUtc;
                    if (authenticationHandler.Options.SessionStore != null && authenticationHandler.sessionKey != null)
                    {
                        await authenticationHandler.Options.SessionStore.RenewAsync(
                            authenticationHandler.sessionKey,
                            model);
                        model = new AuthenticationTicket(
                            new ClaimsIdentity(
                                new[]
                                {
                                    new Claim(SessionIdClaim, authenticationHandler.sessionKey),
                                },
                                authenticationHandler.Options.AuthenticationType),
                            null);
                    }
                    var str = authenticationHandler.Options.TicketDataFormat.Protect(model);
                    if (properties.IsPersistent)
                    {
                        cookieOptions.Expires = authenticationHandler.renewExpiresUtc.UtcDateTime;
                    }
                    authenticationHandler.Options.CookieManager.AppendResponseCookie(
                        authenticationHandler.Context,
                        authenticationHandler.Options.CookieName,
                        str,
                        cookieOptions);
                    properties = null;
                }
                authenticationHandler.Response.Headers.Set(HeaderNameCacheControl, HeaderValueNoCache);
                authenticationHandler.Response.Headers.Set(HeaderNamePragma, HeaderValueNoCache);
                authenticationHandler.Response.Headers.Set(HeaderNameExpires, HeaderValueMinusOne);
                if (((!shouldSignin || !authenticationHandler.Options.LoginPath.HasValue
                            ? 0
                            : authenticationHandler.Request.Path == authenticationHandler.Options.LoginPath
                                ? 1
                                : 0)
                        | (!shouldSignout || !authenticationHandler.Options.LogoutPath.HasValue
                            ? 0
                            : authenticationHandler.Request.Path == authenticationHandler.Options.LogoutPath
                                ? 1
                                : 0))
                    != 0
                    && authenticationHandler.Response.StatusCode == 200)
                {
                    var str = authenticationHandler.Request.Query.Get(authenticationHandler.Options.ReturnUrlParameter);
                    if (!string.IsNullOrWhiteSpace(str) && IsHostRelative(str))
                    {
                        var context = new CookieApplyRedirectContext(
                            authenticationHandler.Context,
                            authenticationHandler.Options,
                            str);
                        authenticationHandler.Options.Provider.ApplyRedirect(context);
                    }
                }
                cookieOptions = null;
            }
            catch (Exception ex)
            {
                var context = new CookieExceptionContext(
                    authenticationHandler.Context,
                    authenticationHandler.Options,
                    CookieExceptionContext.ExceptionLocation.ApplyResponseGrant,
                    ex,
                    model);
                authenticationHandler.Options.Provider.Exception(context);
                if (!context.Rethrow)
                {
                    return;
                }
                throw;
            }
        }

        /// <inheritdoc/>
        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var authenticationHandler = this;
            AuthenticationTicket ticket = null;
            try
            {
                var requestCookie = authenticationHandler.Options.CookieManager.GetRequestCookie(
                    authenticationHandler.Context,
                    authenticationHandler.Options.CookieName);
                if (string.IsNullOrWhiteSpace(requestCookie))
                {
                    return null;
                }
                ticket = authenticationHandler.Options.TicketDataFormat.Unprotect(requestCookie);
                if (ticket == null)
                {
                    authenticationHandler.logger.WriteWarning("Unprotect ticket failed");
                    return null;
                }
                if (authenticationHandler.Options.SessionStore != null)
                {
                    var claim = ticket.Identity.Claims.FirstOrDefault(
                        c => c.Type.Equals(SessionIdClaim));
                    if (claim == null)
                    {
                        authenticationHandler.logger.WriteWarning("SessionId missing");
                        return null;
                    }
                    authenticationHandler.sessionKey = claim.Value;
                    ticket = await authenticationHandler.Options.SessionStore.RetrieveAsync(
                        authenticationHandler.sessionKey);
                    if (ticket == null)
                    {
                        authenticationHandler.logger.WriteWarning("Identity missing in session store");
                        return null;
                    }
                }
                var utcNow = authenticationHandler.Options.SystemClock.UtcNow;
                var issuedUtc = ticket.Properties.IssuedUtc;
                var expiresUtc = ticket.Properties.ExpiresUtc;
                if (expiresUtc.HasValue && expiresUtc.Value < utcNow)
                {
                    if (authenticationHandler.Options.SessionStore != null)
                    {
                        await authenticationHandler.Options.SessionStore.RemoveAsync(authenticationHandler.sessionKey);
                    }
                    return null;
                }
                var allowRefresh = ticket.Properties.AllowRefresh;
                if (issuedUtc.HasValue
                    && expiresUtc.HasValue
                    && authenticationHandler.Options.SlidingExpiration
                    && (!allowRefresh.HasValue || allowRefresh.Value))
                {
                    var timeSpan1 = utcNow.Subtract(issuedUtc.Value);
                    var dateTimeOffset = expiresUtc.Value;
                    if (dateTimeOffset.Subtract(utcNow) < timeSpan1)
                    {
                        authenticationHandler.shouldRenew = true;
                        authenticationHandler.renewIssuedUtc = utcNow;
                        dateTimeOffset = expiresUtc.Value;
                        var timeSpan2 = dateTimeOffset.Subtract(issuedUtc.Value);
                        authenticationHandler.renewExpiresUtc = utcNow.Add(timeSpan2);
                    }
                }
                var context = new CookieValidateIdentityContext(
                    authenticationHandler.Context,
                    ticket,
                    authenticationHandler.Options);
                await authenticationHandler.Options.Provider.ValidateIdentity(context);
                if (context.Identity != null)
                {
                    return new AuthenticationTicket(context.Identity, context.Properties);
                }
                authenticationHandler.shouldRenew = false;
                return null;
            }
            catch (Exception ex)
            {
                var context = new CookieExceptionContext(
                    authenticationHandler.Context,
                    authenticationHandler.Options,
                    CookieExceptionContext.ExceptionLocation.AuthenticateAsync,
                    ex,
                    ticket);
                authenticationHandler.Options.Provider.Exception(context);
                if (!context.Rethrow)
                {
                    return context.Ticket;
                }
                throw;
            }
        }

        /// <summary>Query if 'path' is host relative.</summary>
        /// <param name="path">Full pathname of the file.</param>
        /// <returns>True if host relative, false if not.</returns>
        private static bool IsHostRelative(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            if (path.Length == 1)
            {
                return path[0] == '/';
            }
            if (path[0] != '/' || path[1] == '/')
            {
                return false;
            }
            return path[1] != '\\';
        }
    }
}
