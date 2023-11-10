// <copyright file="OAuthBearerAuthenticationProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication bearer authentication provider class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System;
    using System.Threading.Tasks;

    /// <summary>OAuth bearer token middleware provider.</summary>
    /// <seealso cref="IOAuthBearerAuthenticationProvider"/>
    public class OAuthBearerAuthenticationProvider : IOAuthBearerAuthenticationProvider
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationProvider" />
        /// class.</summary>
        public OAuthBearerAuthenticationProvider()
        {
            OnRequestToken = context => Task.FromResult<object>(null);
            OnValidateIdentity = context => Task.FromResult<object>(null);
            OnApplyChallenge = context =>
            {
                context.OwinContext.Response.Headers.AppendValues("WWW-Authenticate", context.Challenge);
                return Task.FromResult(0);
            };
        }

        /// <summary>Handles applying the authentication challenge to the response message.</summary>
        /// <value>The on apply challenge.</value>
        public Func<OAuthChallengeContext, Task> OnApplyChallenge
        {
            get;
            set;
        }

        /// <summary>Handles processing OAuth bearer token.</summary>
        /// <value>The on request token.</value>
        public Func<OAuthRequestTokenContext, Task> OnRequestToken
        {
            get;
            set;
        }

        /// <summary>Handles validating the identity produced from an OAuth bearer token.</summary>
        /// <value>The on validate identity.</value>
        public Func<OAuthValidateIdentityContext, Task> OnValidateIdentity
        {
            get;
            set;
        }

        /// <summary>Handles applying the authentication challenge to the response message.</summary>
        /// <param name="context">.</param>
        /// <returns>A Task.</returns>
        public Task ApplyChallenge(OAuthChallengeContext context)
        {
            return OnApplyChallenge(context);
        }

        /// <summary>Handles processing OAuth bearer token.</summary>
        /// <param name="context">.</param>
        /// <returns>A Task.</returns>
        public virtual Task RequestToken(OAuthRequestTokenContext context)
        {
            return OnRequestToken(context);
        }

        /// <summary>Handles validating the identity produced from an OAuth bearer token.</summary>
        /// <param name="context">.</param>
        /// <returns>A Task.</returns>
        public virtual Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            return OnValidateIdentity(context);
        }
    }
}
