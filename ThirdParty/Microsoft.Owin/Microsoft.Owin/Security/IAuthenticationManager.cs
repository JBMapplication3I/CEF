// <copyright file="IAuthenticationManager.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAuthenticationManager interface</summary>
namespace Microsoft.Owin.Security
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>Used to interact with authentication middleware that have been chained in the pipeline.</summary>
    public interface IAuthenticationManager
    {
        /// <summary>Exposes the security.Challenge environment value as a strong type.</summary>
        /// <value>The authentication response challenge.</value>
        AuthenticationResponseChallenge AuthenticationResponseChallenge
        {
            get;
            set;
        }

        /// <summary>Exposes the security.SignIn environment value as a strong type.</summary>
        /// <value>The authentication response grant.</value>
        AuthenticationResponseGrant AuthenticationResponseGrant
        {
            get;
            set;
        }

        /// <summary>Exposes the security.SignOut environment value as a strong type.</summary>
        /// <value>The authentication response revoke.</value>
        AuthenticationResponseRevoke AuthenticationResponseRevoke
        {
            get;
            set;
        }

        /// <summary>Returns the current user for the request.</summary>
        /// <value>The user.</value>
        ClaimsPrincipal User
        {
            get;
            set;
        }

        /// <summary>Call back through the middleware to ask for a specific form of authentication to be performed on the
        /// current request.</summary>
        /// <param name="authenticationType">Identifies which middleware should respond to the request for
        ///                                  authentication. This value is compared to the middleware's
        ///                                  Options.AuthenticationType property.</param>
        /// <returns>Returns an object with the results of the authentication. The AuthenticationResult.Identity may be
        /// null if authentication failed. Even if the Identity property is null, there may still be
        /// AuthenticationResult.properties and AuthenticationResult.Description information returned.</returns>
        Task<AuthenticateResult> AuthenticateAsync(string authenticationType);

        /// <summary>Called to perform any number of authentication mechanisms on the current request.</summary>
        /// <param name="authenticationTypes">Identifies one or more middleware which should attempt to respond.</param>
        /// <returns>Returns the AuthenticationResult information from the middleware which responded. The order is
        /// determined by the order the middleware are in the pipeline. Latest added is first in the list.</returns>
        Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes);

        /// <summary>Add information into the response environment that will cause the authentication middleware to
        /// challenge the caller to authenticate. This also changes the status code of the response to 401. The nature
        /// of that challenge varies greatly, and ranges from adding a response header or changing the 401 status code
        /// to a 302 redirect.</summary>
        /// <param name="properties">         Additional arbitrary values which may be used by particular authentication
        ///                                   types.</param>
        /// <param name="authenticationTypes">Identify which middleware should perform their alterations on the response.
        ///                                   If the authenticationTypes is null or empty, that means the
        ///                                   AuthenticationMode.Active middleware should perform their alterations on
        ///                                   the response.</param>
        void Challenge(AuthenticationProperties properties, params string[] authenticationTypes);

        /// <summary>Add information into the response environment that will cause the authentication middleware to
        /// challenge the caller to authenticate. This also changes the status code of the response to 401. The nature
        /// of that challenge varies greatly, and ranges from adding a response header or changing the 401 status code
        /// to a 302 redirect.</summary>
        /// <param name="authenticationTypes">Identify which middleware should perform their alterations on the response.
        ///                                   If the authenticationTypes is null or empty, that means the
        ///                                   AuthenticationMode.Active middleware should perform their alterations on
        ///                                   the response.</param>
        void Challenge(params string[] authenticationTypes);

        /// <summary>Lists all of the description data provided by authentication middleware that have been chained.</summary>
        /// <returns>The authentication descriptions.</returns>
        IEnumerable<AuthenticationDescription> GetAuthenticationTypes();

        /// <summary>Lists the description data of all of the authentication middleware which are true for a given
        /// predicate.</summary>
        /// <param name="predicate">A function provided by the caller which returns true for descriptions that should be
        ///                         in the returned list.</param>
        /// <returns>The authentication descriptions.</returns>
        IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate);

        /// <summary>Add information to the response environment that will cause the appropriate authentication
        /// middleware to grant a claims-based identity to the recipient of the response. The exact mechanism of this
        /// may vary. Examples include setting a cookie, to adding a fragment on the redirect url, or producing an
        /// OAuth2 access code or token response.</summary>
        /// <param name="properties">Contains additional properties the middleware are expected to persist along with the
        ///                          claims. These values will be returned as the AuthenticateResult.properties
        ///                          collection when AuthenticateAsync is called on subsequent requests.</param>
        /// <param name="identities">Determines which claims are granted to the signed in user. The
        ///                          ClaimsIdentity.AuthenticationType property is compared to the middleware's
        ///                          Options.AuthenticationType value to determine which claims are granted by which
        ///                          middleware. The recommended use is to have a single ClaimsIdentity which has the
        ///                          AuthenticationType matching a specific middleware.</param>
        void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities);

        /// <summary>Add information to the response environment that will cause the appropriate authentication
        /// middleware to grant a claims-based identity to the recipient of the response. The exact mechanism of this
        /// may vary. Examples include setting a cookie, to adding a fragment on the redirect url, or producing an
        /// OAuth2 access code or token response.</summary>
        /// <param name="identities">Determines which claims are granted to the signed in user. The
        ///                          ClaimsIdentity.AuthenticationType property is compared to the middleware's
        ///                          Options.AuthenticationType value to determine which claims are granted by which
        ///                          middleware. The recommended use is to have a single ClaimsIdentity which has the
        ///                          AuthenticationType matching a specific middleware.</param>
        void SignIn(params ClaimsIdentity[] identities);

        /// <summary>Add information to the response environment that will cause the appropriate authentication
        /// middleware to revoke any claims identity associated the the caller. The exact method varies.</summary>
        /// <param name="properties">         Additional arbitrary values which may be used by particular authentication
        ///                                   types.</param>
        /// <param name="authenticationTypes">Identifies which middleware should perform the work to sign out. Multiple
        ///                                   authentication types may be provided to clear out more than one cookie
        ///                                   at a time, or to clear cookies and redirect to an external single-sign
        ///                                   out url.</param>
        void SignOut(AuthenticationProperties properties, params string[] authenticationTypes);

        /// <summary>Add information to the response environment that will cause the appropriate authentication
        /// middleware to revoke any claims identity associated the the caller. The exact method varies.</summary>
        /// <param name="authenticationTypes">Identifies which middleware should perform the work to sign out. Multiple
        ///                                   authentication types may be provided to clear out more than one cookie
        ///                                   at a time, or to clear cookies and redirect to an external single-sign
        ///                                   out url.</param>
        void SignOut(params string[] authenticationTypes);
    }
}
