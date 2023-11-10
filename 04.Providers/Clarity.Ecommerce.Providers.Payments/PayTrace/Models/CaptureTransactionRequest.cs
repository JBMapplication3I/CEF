// <copyright file="CaptureTransactionRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace CaptureTransactionRequest class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Newtonsoft.Json;

    /// <summary>Class to Capture Transaction request - include other optional inputs from the PayTrace Capture page
    /// as needed.</summary>
    public class CaptureTransactionRequest
    {
        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        [JsonProperty("amount")]
        public double Amount { get; set; }

        /// <summary>Gets or sets the identifier of the transaction.</summary>
        /// <value>The identifier of the transaction.</value>
        [JsonProperty("transaction_id")]
        public long TransactionId { get; set; }

        /// <summary>Gets or sets the identifier of the integrator.</summary>
        /// <value>The identifier of the integrator.</value>
        [JsonProperty("integrator_id")]
        public string? IntegratorId { get; set; }
    }
}
