// <copyright file="TokenEndpointRequestAuthorizationCode.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the token endpoint request authorization code class</summary>
namespace Microsoft.Owin.Security.OAuth.Messages
{
    /// <summary>Data object used by TokenEndpointRequest when the "grant_type" is "authorization_code".</summary>
    public class TokenEndpointRequestAuthorizationCode
    {
        /// <summary>The value passed to the Token endpoint in the "code" parameter.</summary>
        /// <value>The code.</value>
        public string Code
        {
            get;
            set;
        }

        /// <summary>The value passed to the Token endpoint in the "redirect_uri" parameter. This MUST be provided by the
        /// caller if the original visit to the Authorize endpoint contained a "redirect_uri" parameter.</summary>
        /// <value>The redirect URI.</value>
        public string RedirectUri
        {
            get;
            set;
        }
    }
}
