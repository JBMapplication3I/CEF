// <copyright file="ErrorResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the error response class</summary>
namespace Clarity.Ecommerce.Providers.Chatting.WeChatInt
{
    using Newtonsoft.Json;

    /// <summary>An error response.</summary>
    public class ErrorResponse
    {
        /// <summary>Gets or sets the error code.</summary>
        /// <value>The error code.</value>
        [JsonProperty("errcode")]
        public int? ErrorCode { get; set; }

        /// <summary>Gets or sets a message describing the error.</summary>
        /// <value>A message describing the error.</value>
        [JsonProperty("errmsg")]
        public string? ErrorMessage { get; set; }
    }
}
