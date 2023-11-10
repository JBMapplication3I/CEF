// <copyright file="TriggerTestFundsReleasedFromEscrowByBuyerEventWebhookDto.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the trigger test funds released from escrow by buyer event webhook dto class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>A trigger test funds released from escrow by buyer event webhook data transfer object.</summary>
    [DataContract]
    internal class TriggerTestFundsReleasedFromEscrowByBuyerEventWebhookDto
    {
        /// <summary>Gets or sets the action.</summary>
        /// <value>The action.</value>
        [JsonProperty("action")]
        [DataMember(Name = "action")]
        public string? Action { get; set; } // "release"

        /// <summary>Gets or sets a value indicating whether the confirm.</summary>
        /// <value>True if confirm, false if not.</value>
        [JsonProperty("confirm")]
        [DataMember(Name = "confirm")]
        public bool Confirm { get; set; } // true
    }
}
