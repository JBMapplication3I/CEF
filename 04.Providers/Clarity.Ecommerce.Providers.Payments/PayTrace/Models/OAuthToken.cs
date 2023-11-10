// <copyright file="OAuthToken.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace OAuthToken class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>class for the holding  oAuth Data.</summary>
    public class OAuthToken
    {
        /// <summary>Gets or sets the access token.</summary>
        /// <value>The access token.</value>
        [DataMember(Name = "access_token"), JsonProperty("access_token"), ApiMember(Name = "access_token")]
        public string? AccessToken { get; set; }

        /// <summary>Gets or sets the type of the token.</summary>
        /// <value>The type of the token.</value>
        [DataMember(Name = "token_type"), JsonProperty("token_type"), ApiMember(Name = "token_type")]
        public string? TokenType { get; set; }

        /// <summary>Gets or sets the expires in.</summary>
        /// <value>The expires in.</value>
        [DataMember(Name = "expires_in"), JsonProperty("expires_in"), ApiMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>for Errors, Object for PayTrace Error Json Key.</summary>
        /// <value>The error.</value>
        public OAuthError? Error { get; set; }

        /// <summary>Optional - flag for error.</summary>
        /// <value>True if error flag, false if not.</value>
        public bool ErrorFlag { get; set; }
    }
}
