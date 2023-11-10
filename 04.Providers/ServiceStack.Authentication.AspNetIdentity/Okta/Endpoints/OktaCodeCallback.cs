// <copyright file="OktaCodeCallback.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the open identifier service class</summary>
namespace ServiceStack.Auth
{
    using System.Runtime.Serialization;
    using Clarity.Ecommerce.Service;
    using JetBrains.Annotations;
    using Newtonsoft.Json;

    /// <summary>An open identifier connect code callback.</summary>
    /// <seealso cref="IReturnVoid"/>
    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInAdmin,
        Route("/Authentication/OktaCodeCallback", "POST",
            Summary = "Consumes the code response from Open ID Connect")]
    public class OktaCodeCallback : IReturnVoid
    {
        /// <summary>Gets or sets the identifier token.</summary>
        /// <value>The identifier token.</value>
        [DataMember(Name = "id_token"), JsonProperty("id_token"),
            ApiMember(Name = "id_token", DataType = "string", ParameterType = "body")]
        public string? IdToken { get; set; }

        /// <summary>Gets or sets the access token.</summary>
        /// <value>The access token.</value>
        [DataMember(Name = "access_token"), JsonProperty("access_token"),
            ApiMember(Name = "access_token", DataType = "string", ParameterType = "body")]
        public string? AccessToken { get; set; }

        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        [DataMember(Name = "code"), JsonProperty("code"),
            ApiMember(Name = "code", DataType = "string", ParameterType = "body")]
        public string? Code { get; set; }

        /// <summary>Gets or sets the expires in.</summary>
        /// <value>The expires in.</value>
        [DataMember(Name = "expires_in"), JsonProperty("expires_in"),
            ApiMember(Name = "expires_in", DataType = "string", ParameterType = "body")]
        public int ExpiresIn { get; set; }

        /// <summary>Gets or sets the resource.</summary>
        /// <value>The resource.</value>
        [DataMember(Name = "resource"), JsonProperty("resource"),
            ApiMember(Name = "resource", DataType = "string", ParameterType = "body")]
        public string? Resource { get; set; }

        /// <summary>Gets or sets the refresh token.</summary>
        /// <value>The refresh token.</value>
        [DataMember(Name = "refresh_token"), JsonProperty("refresh_token"),
            ApiMember(Name = "refresh_token", DataType = "string", ParameterType = "body")]
        public string? RefreshToken { get; set; }

        /// <summary>Gets or sets the refresh token expires in.</summary>
        /// <value>The refresh token expires in.</value>
        [DataMember(Name = "refresh_token_expires_in"), JsonProperty("refresh_token_expires_in"),
            ApiMember(Name = "refresh_token_expires_in", DataType = "string", ParameterType = "body")]
        public int RefreshTokenExpiresIn { get; set; }

        /// <summary>Gets or sets the scope.</summary>
        /// <value>The scope.</value>
        [DataMember(Name = "scope"), JsonProperty("scope"),
            ApiMember(Name = "scope", DataType = "string", ParameterType = "body")]
        public string? Scope { get; set; }

        /// <summary>Gets or sets the error.</summary>
        /// <value>The error.</value>
        [DataMember(Name = "error"), JsonProperty("error"),
            ApiMember(Name = "error", DataType = "string", ParameterType = "body")]
        public string? Error { get; set; }

        /// <summary>Gets or sets information describing the error.</summary>
        /// <value>Information describing the error.</value>
        [DataMember(Name = "errorDescription"), JsonProperty("errorDescription"),
            ApiMember(Name = "errorDescription", DataType = "string", ParameterType = "body")]
        public string? ErrorDescription { get; set; }
    }
}
