// <copyright file="AuthenticationResponseRevoke.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication response revoke class</summary>
namespace Microsoft.Owin.Security
{
    /// <summary>Exposes the security.SignOut and security.SignOutProperties environment values as a strong type.</summary>
    public class AuthenticationResponseRevoke
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="AuthenticationResponseRevoke" /> class.</summary>
        /// <param name="authenticationTypes">.</param>
        public AuthenticationResponseRevoke(string[] authenticationTypes)
            : this(authenticationTypes, new AuthenticationProperties())
        {
        }

        /// <summary>Initializes a new instance of the
        /// <see cref="AuthenticationResponseRevoke" /> class.</summary>
        /// <param name="authenticationTypes">.</param>
        /// <param name="properties">         .</param>
        public AuthenticationResponseRevoke(string[] authenticationTypes, AuthenticationProperties properties)
        {
            AuthenticationTypes = authenticationTypes;
            Properties = properties;
        }

        /// <summary>List of the authentication types that should be revoked on sign out.</summary>
        /// <value>A list of types of the authentications.</value>
        public string[] AuthenticationTypes
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
