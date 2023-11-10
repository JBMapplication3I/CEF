// <copyright file="TokenEndpointRequestClientCredentials.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the token endpoint request client credentials class</summary>
namespace Microsoft.Owin.Security.OAuth.Messages
{
    using System.Collections.Generic;

    /// <summary>Data object used by TokenEndpointRequest when the "grant_type" is "client_credentials".</summary>
    public class TokenEndpointRequestClientCredentials
    {
        /// <summary>The value passed to the Token endpoint in the "scope" parameter.</summary>
        /// <value>The scope.</value>
        public IList<string> Scope
        {
            get;
            set;
        }
    }
}
