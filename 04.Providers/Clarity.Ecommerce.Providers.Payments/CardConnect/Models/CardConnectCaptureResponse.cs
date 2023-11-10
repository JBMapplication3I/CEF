// <copyright file="CardConnectCaptureResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the card connect capture response class</summary>
// ReSharper disable CommentTypo, StringLiteralTypo
namespace Clarity.Ecommerce.Providers.Payments.CardConnect.Models
{
    using Newtonsoft.Json;

    /// <summary>The capture response.</summary>
    public class CardConnectCaptureResponse
    {
        /// <summary>Copied from the capture request.</summary>
        /// <value>The identifier of the merchant.</value>
        [JsonProperty("merchid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? MerchantId { get; set; }

        /// <summary>Masked account number.</summary>
        /// <value>The account number.</value>
        [JsonProperty("account", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AccountNumber { get; set; }

        /// <summary>The amount included in the capture request.</summary>
        /// <value>The amount in cents.</value>
        [JsonProperty("amount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AmountInCents { get; set; }

        /// <summary>The retref included in the capture request.</summary>
        /// <value>The retrieval reference.</value>
        [JsonProperty("retref", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? RetrievalReference { get; set; }

        /// <summary>Automatically created and assigned unless otherwise specified.</summary>
        /// <value>The identifier of the batch.</value>
        [JsonProperty("batchid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? BatchId { get; set; }

        /// <summary>The current settlement status.</summary>
        /// <value>The settlement status.</value>
        [JsonProperty("setlstat", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? SettlementStatus { get; set; }
    }
}
