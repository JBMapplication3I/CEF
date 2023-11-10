// <copyright file="TokenEndpointRequestRefreshToken.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the token endpoint request refresh token class</summary>
namespace Microsoft.Owin.Security.OAuth.Messages
{
    using System.Collections.Generic;

    /// <summary>Data object used by TokenEndpointRequest when the "grant_type" parameter is "refresh_token".</summary>
    public class TokenEndpointRequestRefreshToken
    {
        /// <summary>The value passed to the Token endpoint in the "refresh_token" parameter.</summary>
        /// <value>The refresh token.</value>
        public string RefreshToken
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
    }
}
