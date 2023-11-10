// <copyright file="TransactionRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace TransactionRequest class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>Class for TransactionRequest.</summary>
    public class TransactionRequest
    {
        /// <summary>Gets or sets the TransactionID.</summary>
        /// <value>The TransactionID.</value>
        [JsonProperty("transaction_id")]
        public string? TransactionID { get; set; }

        /// <summary>Gets or sets the StartDate.</summary>
        /// <value>The StartDate.</value>
        [JsonProperty("start_date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the EndDate.</summary>
        /// <value>The EndDate.</value>
        [JsonProperty("end_date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? EndDate { get; set; }

        /// <summary>Gets or sets the IntegratorID.</summary>
        /// <value>The IntegratorID.</value>
        [JsonProperty("integrator_id")]
        public string? IntegratorID { get; set; }
    }
}
