// <copyright file="AuthenticationResponseGrant.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication response grant class</summary>
namespace Microsoft.Owin.Security
{
    using System;
    using System.Linq;
    using System.Security.Claims;

    /// <summary>Exposes the security.SignIn environment value as a strong type.</summary>
    public class AuthenticationResponseGrant
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="AuthenticationResponseGrant" /> class.</summary>
        /// <param name="identity">  .</param>
        /// <param name="properties">.</param>
        public AuthenticationResponseGrant(ClaimsIdentity identity, AuthenticationProperties properties)
        {
            Principal = new ClaimsPrincipal(identity);
            Identity = identity;
            Properties = properties;
        }

        /// <summary>Initializes a new instance of the
        /// <see cref="AuthenticationResponseGrant" /> class.</summary>
        /// <param name="principal"> .</param>
        /// <param name="properties">.</param>
        public AuthenticationResponseGrant(ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            Principal = principal ?? throw new ArgumentNullException(nameof(principal));
            Identity = principal.Identities.FirstOrDefault();
            Properties = properties;
        }

        /// <summary>The identity associated with the user sign in.</summary>
        /// <value>The identity.</value>
        public ClaimsIdentity Identity
        {
            get;
        }

        /// <summary>The security principal associated with the user sign in.</summary>
        /// <value>The principal.</value>
        public ClaimsPrincipal Principal
        {
            get;
        }

        /// <summary>Dictionary used to store state values about the authentication session.</summary>
        /// <value>The properties.</value>
        public AuthenticationProperties Properties
        {
            get;
        }
    }
}
