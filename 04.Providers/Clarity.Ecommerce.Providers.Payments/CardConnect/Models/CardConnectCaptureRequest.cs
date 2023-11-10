// <copyright file="CardConnectCaptureRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the card connect capture request class</summary>
// ReSharper disable CommentTypo, StringLiteralTypo
namespace Clarity.Ecommerce.Providers.Payments.CardConnect.Models
{
    using Newtonsoft.Json;

    /// <summary>The capture request.</summary>
    public class CardConnectCaptureRequest
    {
        /// <summary>Merchant ID, required for all requests. Must match merchid of transaction to be captured.</summary>
        /// <value>The identifier of the merchant.</value>
        [JsonProperty("merchid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? MerchantId { get; set; }

        /// <summary>CardConnect retrieval reference number from authorization response.</summary>
        /// <value>The retrieval reference.</value>
        [JsonProperty("retref", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? RetrievalReference { get; set; }

        /// <summary>Authorization code from original authorization request.</summary>
        /// <value>The authorization code.</value>
        [JsonProperty("authcode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AuthorizationCode { get; set; }

        /// <summary>Amount with decimal or without decimal in currency minor units (for example, USD Pennies or EUR
        /// Cents).</summary>
        /// <value>The amount in cents.</value>
        [JsonProperty("amount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AmountInCents { get; set; }

        /// <summary>Invoice ID, optional, defaults to orderid from authorization request.</summary>
        /// <value>The identifier of the invoice.</value>
        [JsonProperty("invoiceid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? InvoiceId { get; set; }
    }
}
