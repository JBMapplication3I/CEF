// <copyright file="AuthenticationOptions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication options class</summary>
namespace Microsoft.Owin.Security
{
    /// <summary>Base Options for all authentication middleware.</summary>
    public abstract class AuthenticationOptions
    {
        /// <summary>Type of the authentication.</summary>
        private string _authenticationType;

        /// <summary>Initialize properties of AuthenticationOptions base class.</summary>
        /// <param name="authenticationType">Assigned to the AuthenticationType property.</param>
        protected AuthenticationOptions(string authenticationType)
        {
            Description = new AuthenticationDescription();
            AuthenticationType = authenticationType;
            AuthenticationMode = AuthenticationMode.Active;
        }

        /// <summary>If Active the authentication middleware alter the request user coming in and alter 401 Unauthorized
        /// responses going out. If Passive the authentication middleware will only provide identity and alter responses
        /// when explicitly indicated by the AuthenticationType.</summary>
        /// <value>The authentication mode.</value>
        public AuthenticationMode AuthenticationMode
        {
            get;
            set;
        }

        /// <summary>The AuthenticationType in the options corresponds to the IIdentity AuthenticationType property. A
        /// different value may be assigned in order to use the same authentication middleware type more than once in a
        /// pipeline.</summary>
        /// <value>The type of the authentication.</value>
        public string AuthenticationType
        {
            get => _authenticationType;
            set
            {
                _authenticationType = value;
                Description.AuthenticationType = value;
            }
        }

        /// <summary>Additional information about the authentication type which is made available to the application.</summary>
        /// <value>The description.</value>
        public AuthenticationDescription Description
        {
            get;
            set;
        }
    }
}
