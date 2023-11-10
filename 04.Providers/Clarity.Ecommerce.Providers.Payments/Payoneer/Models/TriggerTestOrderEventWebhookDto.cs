// <copyright file="TriggerTestOrderEventWebhookDto.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the trigger test order event webhook dto class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>A trigger test order event webhook data transfer object.</summary>
    [DataContract]
    internal class TriggerTestOrderEventWebhookDto
    {
        /// <summary>Gets or sets the action.</summary>
        /// <value>The action.</value>
        [JsonProperty("action")]
        [DataMember(Name = "action")]
        public string? Action { get; set; } // "add_payment"

        /// <summary>Gets or sets a value indicating whether the confirm.</summary>
        /// <value>True if confirm, false if not.</value>
        [JsonProperty("confirm")]
        [DataMember(Name = "confirm")]
        public bool Confirm { get; set; } // true

        /// <summary>Gets or sets the identifier of the source account.</summary>
        /// <value>The identifier of the source account.</value>
        [JsonProperty("source_account_id")]
        [DataMember(Name = "source_account_id")]
        public long SourceAccountID { get; set; } // 452981

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        [JsonProperty("amount")]
        [DataMember(Name = "amount")]
        public decimal Amount { get; set; } // 10000
    }
}
