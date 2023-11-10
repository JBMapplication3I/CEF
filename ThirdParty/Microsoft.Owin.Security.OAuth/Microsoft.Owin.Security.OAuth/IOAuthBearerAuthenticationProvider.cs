// <copyright file="IOAuthBearerAuthenticationProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the i/o authentication bearer authentication provider class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System.Threading.Tasks;

    /// <summary>Specifies callback methods which the
    /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationMiddleware"></see> invokes to enable
    /// developer
    /// control over the authentication process. /&gt;</summary>
    public interface IOAuthBearerAuthenticationProvider
    {
        /// <summary>Called each time a challenge is being sent to the client. By implementing this method the
        /// application may modify the challenge as needed.</summary>
        /// <param name="context">Contains the default challenge.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing the completed operation.</returns>
        Task ApplyChallenge(OAuthChallengeContext context);

        /// <summary>Invoked before the <see cref="T:System.Security.Claims.ClaimsIdentity" /> is created. Gives the
        /// application an opportunity to find the identity from a different location, adjust, or reject the token.</summary>
        /// <param name="context">Contains the token string.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing the completed operation.</returns>
        Task RequestToken(OAuthRequestTokenContext context);

        /// <summary>Called each time a request identity has been validated by the middleware. By implementing this
        /// method the application may alter or reject the identity which has arrived with the request.</summary>
        /// <param name="context">Contains information about the login session as well as the user
        ///                       <see cref="T:System.Security.Claims.ClaimsIdentity" />.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing the completed operation.</returns>
        Task ValidateIdentity(OAuthValidateIdentityContext context);
    }
}
