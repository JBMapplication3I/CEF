// <copyright file="TokenEndpointRequestResourceOwnerPasswordCredentials.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the token endpoint request resource owner password credentials class</summary>
namespace Microsoft.Owin.Security.OAuth.Messages
{
    using System.Collections.Generic;

    /// <summary>Data object used by TokenEndpointRequest when the "grant_type" is "password".</summary>
    public class TokenEndpointRequestResourceOwnerPasswordCredentials
    {
        /// <summary>The value passed to the Token endpoint in the "password" parameter.</summary>
        /// <value>The password.</value>
        public string Password
        {
            get;
            set;
        }

        /// <summary>The value passed to the Token endpoint in the "scope" parameter.</summary>
        /// <value>The scope.</value>
        public IList<string> Scope
        {
            get;
            set;
        }

        /// <summary>The value passed to the Token endpoint in the "username" parameter.</summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get;
            set;
        }
    }
}
