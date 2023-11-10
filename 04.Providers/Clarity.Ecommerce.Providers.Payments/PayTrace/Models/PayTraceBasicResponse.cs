// <copyright file="PayTraceBasicResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTraceBasicResponse class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>Properties Available on most of the transaction responses with the API.</summary>
    [PublicAPI, Serializable]
    public class PayTraceBasicResponse
    {
        /// <summary>Gets or sets a value indicating whether the success.</summary>
        /// <value>True if success, false if not.</value>
        [DataMember(Name = "success"), JsonProperty("success"), ApiMember(Name = "success")]
        public bool Success { get; set; }

        /// <summary>Gets or sets the response code.</summary>
        /// <value>The response code.</value>
        [DataMember(Name = "response_code"), JsonProperty("response_code"), ApiMember(Name = "response_code")]
        public int ResponseCode { get; set; }

        /// <summary>Gets or sets the status message.</summary>
        /// <value>A message describing the status.</value>
        [DataMember(Name = "status_message"), JsonProperty("status_message"), ApiMember(Name = "status_message")]
        public string? StatusMessage { get; set; }

        /// <summary>transaction_id is not a part of Error Response , Create Customer Profile Requests.</summary>
        /// <value>The identifier of the transaction.</value>
        [DataMember(Name = "transaction_id"), JsonProperty("transaction_id"), ApiMember(Name = "transaction_id")]
        public long TransactionId { get; set; }

        /// <summary>Optional : To hold HTTP error or any unexpected error, this is not a part of PayTrace API Error
        /// Response.</summary>
        /// <value>A message describing the HTTP error.</value>
        public string? HttpErrorMessage { get; set; }

        /// <summary>Store the error Key with PayTrace API Response.</summary>
        /// <value>The transaction errors.</value>
        [DataMember(Name = "errors"), JsonProperty("errors"), ApiMember(Name = "errors")]
        public Dictionary<string, string[]>? TransactionErrors { get; set; }
    }
}
