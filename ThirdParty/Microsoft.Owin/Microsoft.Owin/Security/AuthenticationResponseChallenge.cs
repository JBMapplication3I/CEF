// <copyright file="AuthenticationResponseChallenge.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication response challenge class</summary>
namespace Microsoft.Owin.Security
{
    /// <summary>Exposes the security.Challenge environment value as a strong type.</summary>
    public class AuthenticationResponseChallenge
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="AuthenticationResponseChallenge" /> class.</summary>
        /// <param name="authenticationTypes">.</param>
        /// <param name="properties">         .</param>
        public AuthenticationResponseChallenge(string[] authenticationTypes, AuthenticationProperties properties)
        {
            AuthenticationTypes = authenticationTypes;
            Properties = properties ?? new AuthenticationProperties();
        }

        /// <summary>List of the authentication types that should send a challenge in the response.</summary>
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
