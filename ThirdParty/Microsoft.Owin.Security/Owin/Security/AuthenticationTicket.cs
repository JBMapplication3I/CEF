// <copyright file="AuthenticationTicket.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication ticket class</summary>
namespace Microsoft.Owin.Security
{
    using System.Security.Claims;

    /// <summary>Contains user identity information as well as additional authentication state.</summary>
    public class AuthenticationTicket
    {
        /// <summary>Initializes a new instance of the <see cref="AuthenticationTicket" /> class.</summary>
        /// <param name="identity">  .</param>
        /// <param name="properties">.</param>
        public AuthenticationTicket(ClaimsIdentity identity, AuthenticationProperties properties)
        {
            Identity = identity;
            Properties = properties ?? new AuthenticationProperties();
        }

        /// <summary>Gets the authenticated user identity.</summary>
        /// <value>The identity.</value>
        public ClaimsIdentity Identity
        {
            get;
        }

        /// <summary>Additional state values for the authentication session.</summary>
        /// <value>The properties.</value>
        public AuthenticationProperties Properties
        {
            get;
        }
    }
}
