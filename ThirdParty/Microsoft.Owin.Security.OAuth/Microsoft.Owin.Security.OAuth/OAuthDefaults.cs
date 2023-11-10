// <copyright file="OAuthDefaults.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication defaults class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    /// <summary>Default values used by authorization server and bearer authentication.</summary>
    public static class OAuthDefaults
    {
        /// <summary>Default value for AuthenticationType property in the OAuthBearerAuthenticationOptions and
        /// OAuthAuthorizationServerOptions.</summary>
        public const string AuthenticationType = "Bearer";
    }
}
