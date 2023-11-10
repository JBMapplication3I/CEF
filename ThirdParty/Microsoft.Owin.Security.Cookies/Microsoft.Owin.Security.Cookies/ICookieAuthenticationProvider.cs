// <copyright file="ICookieAuthenticationProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICookieAuthenticationProvider interface</summary>
namespace Microsoft.Owin.Security.Cookies
{
    using System.Threading.Tasks;

    /// <summary>Specifies callback methods which the
    /// <see cref="CookieAuthenticationMiddleware"></see> invokes to enable
    /// developer control over the authentication process. /&gt;</summary>
    public interface ICookieAuthenticationProvider
    {
        /// <summary>Called when a Challenge, SignIn, or SignOut causes a redirect in the cookie middleware.</summary>
        /// <param name="context">Contains information about the event.</param>
        void ApplyRedirect(CookieApplyRedirectContext context);

        /// <summary>Called when an exception occurs during request or response processing.</summary>
        /// <param name="context">Contains information about the exception that occurred.</param>
        void Exception(CookieExceptionContext context);

        /// <summary>Called when an endpoint has provided sign in information after it is converted into a cookie.</summary>
        /// <param name="context">Contains information about the login session as well as the user
        ///                       <see cref="System.Security.Claims.ClaimsIdentity" />.</param>
        void ResponseSignedIn(CookieResponseSignedInContext context);

        /// <summary>Called when an endpoint has provided sign in information before it is converted into a cookie. By
        /// implementing this method the claims and extra information that go into the ticket may be altered.</summary>
        /// <param name="context">Contains information about the login session as well as the user
        ///                       <see cref="System.Security.Claims.ClaimsIdentity" />.</param>
        void ResponseSignIn(CookieResponseSignInContext context);

        /// <summary>Called during the sign-out flow to augment the cookie cleanup process.</summary>
        /// <param name="context">Contains information about the login session as well as information about the
        ///                       authentication cookie.</param>
        void ResponseSignOut(CookieResponseSignOutContext context);

        /// <summary>Called each time a request identity has been validated by the middleware. By implementing this
        /// method the application may alter or reject the identity which has arrived with the request.</summary>
        /// <param name="context">Contains information about the login session as well as the user
        ///                       <see cref="System.Security.Claims.ClaimsIdentity" />.</param>
        /// <returns>A <see cref="Task" /> representing the completed operation.</returns>
        Task ValidateIdentity(CookieValidateIdentityContext context);
    }
}
