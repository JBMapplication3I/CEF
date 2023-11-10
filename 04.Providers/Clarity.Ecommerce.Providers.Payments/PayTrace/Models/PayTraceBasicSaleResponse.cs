// <copyright file="PayTraceBasicSaleResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTraceBasicSaleResponse class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Newtonsoft.Json;

    /// <summary>PayTraceBasicSaleResponse Class following properties are Available on most of the Sale Responses
    /// with the API.</summary>
    /// <seealso cref="PayTraceExternalTransResponse"/>
    public class PayTraceBasicSaleResponse : PayTraceExternalTransResponse
    {
        /// <summary>Gets or sets the approval code.</summary>
        /// <value>The approval code.</value>
        [JsonProperty("approval_code")]
        public string? ApprovalCode { get; set; }

        /// <summary>Gets or sets a message describing the approval.</summary>
        /// <value>A message describing the approval.</value>
        [JsonProperty("approval_message")]
        public string? ApprovalMessage { get; set; }

        /// <summary>Gets or sets the avs response.</summary>
        /// <value>The avs response.</value>
        [JsonProperty("avs_response")]
        public string? AvsResponse { get; set; }

        /// <summary>Gets or sets the csc response.</summary>
        /// <value>The csc response.</value>
        [JsonProperty("csc_response")]
        public string? CscResponse { get; set; }
    }
}
