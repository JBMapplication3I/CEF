// <copyright file="GetAccessTokenResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get access token response class</summary>
// ReSharper disable InconsistentNaming
namespace Clarity.Ecommerce.Providers.Chatting.WeChatInt
{
    using Newtonsoft.Json;

    /// <summary>A get access token response.</summary>
    /// <seealso cref="ErrorResponse"/>
    public class GetAccessTokenResponse : ErrorResponse
    {
        /// <summary>Gets or sets the access token.</summary>
        /// <value>The access token.</value>
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        /// <summary>Gets or sets the expires in.</summary>
        /// <value>The expires in.</value>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
