// <copyright file="ExternalLoginInfo.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the external login information class</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    using System.Security.Claims;

    /// <summary>Used to return information needed to associate an external login.</summary>
    public class ExternalLoginInfo
    {
        /// <summary>Suggested user name for a user.</summary>
        /// <value>The default user name.</value>
        public string DefaultUserName
        {
            get;
            set;
        }

        /// <summary>Email claim from the external identity.</summary>
        /// <value>The email.</value>
        public string Email
        {
            get;
            set;
        }

        /// <summary>The external identity.</summary>
        /// <value>The external identity.</value>
        public ClaimsIdentity ExternalIdentity
        {
            get;
            set;
        }

        /// <summary>Associated login data.</summary>
        /// <value>The login.</value>
        public UserLoginInfo Login
        {
            get;
            set;
        }
    }
}
