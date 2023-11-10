// <copyright file="AuthenticateResult.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authenticate result class</summary>
namespace Microsoft.Owin.Security
{
    using System;
    using System.Security.Claims;
    using System.Security.Principal;

    /// <summary>Acts as the return value from calls to the IAuthenticationManager's AuthenticeAsync methods.</summary>
    public class AuthenticateResult
    {
        /// <summary>Create an instance of the result object.</summary>
        /// <param name="identity">   Assigned to Identity. May be null.</param>
        /// <param name="properties"> Assigned to Properties. Contains extra information carried along with the identity.</param>
        /// <param name="description">Assigned to Description. Contains information describing the authentication provider.</param>
        public AuthenticateResult(
            IIdentity identity,
            AuthenticationProperties properties,
            AuthenticationDescription description)
        {
            if (identity != null)
            {
                Identity = identity as ClaimsIdentity ?? new ClaimsIdentity(identity);
            }
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>Contains description properties for the middleware authentication type in general. Does not vary per
        /// request.</summary>
        /// <value>The description.</value>
        public AuthenticationDescription Description
        {
            get;
        }

        /// <summary>Contains the claims that were authenticated by the given AuthenticationType. If the authentication
        /// type was not successful the Identity property will be null.</summary>
        /// <value>The identity.</value>
        public ClaimsIdentity Identity
        {
            get;
        }

        /// <summary>Contains extra values that were provided with the original SignIn call.</summary>
        /// <value>The properties.</value>
        public AuthenticationProperties Properties
        {
            get;
        }
    }
}
