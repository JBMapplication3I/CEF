// <copyright file="PayeezyTokenPaymentRequestResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy token payment request response class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a payeezy token payment request response.</summary>
    [PublicAPI]
    [Serializable]
    public class PayeezyTokenPaymentRequestResponse
    {
        /// <summary>Gets or sets the transaction status.</summary>
        /// <value>The transaction status.</value>
        public string? transaction_status { get; set; }

        /// <summary>Gets or sets the identifier of the transaction.</summary>
        /// <value>The identifier of the transaction.</value>
        public string? transaction_id { get; set; }

        /// <summary>Gets or sets the transaction tag.</summary>
        /// <value>The transaction tag.</value>
        public string? transaction_tag { get; set; }

        /// <summary>Gets or sets the gateway resp code.</summary>
        /// <value>The gateway resp code.</value>
        public string? gateway_resp_code { get; set; }

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        public string? amount { get; set; }

        /// <summary>Gets or sets the method.</summary>
        /// <value>The method.</value>
        public string? method { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        public string? currency { get; set; }

        /// <summary>Gets or sets the avs.</summary>
        /// <value>The avs.</value>
        public string? avs { get; set; }

        /// <summary>Gets or sets the cvv 2.</summary>
        /// <value>The cvv 2.</value>
        public string? cvv2 { get; set; }

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        public PayeezyTokenDataWrapper? token { get; set; }

        /// <summary>Gets or sets the validation status.</summary>
        /// <value>The validation status.</value>
        public string? validation_status { get; set; }

        /// <summary>Gets or sets the type of the transaction.</summary>
        /// <value>The type of the transaction.</value>
        public string? transaction_type { get; set; }

        /// <summary>Gets or sets the bank resp code.</summary>
        /// <value>The bank resp code.</value>
        public string? bank_resp_code { get; set; }

        /// <summary>Gets or sets a message describing the bank.</summary>
        /// <value>A message describing the bank.</value>
        public string? bank_message { get; set; }

        /// <summary>Gets or sets a message describing the gateway.</summary>
        /// <value>A message describing the gateway.</value>
        public string? gateway_message { get; set; }

        /// <summary>Gets or sets the identifier of the correlation.</summary>
        /// <value>The identifier of the correlation.</value>
        public string? correlation_id { get; set; }

        /// <summary>Gets or sets the special payment.</summary>
        /// <value>The special payment.</value>
        public string? special_payment { get; set; }

        /// <summary>Gets or sets the identifier of the card brand original transaction.</summary>
        /// <value>The identifier of the card brand original transaction.</value>
        public string? card_brand_original_transaction_id { get; set; }

        /// <summary>Gets or sets the card.</summary>
        /// <value>The card.</value>
        public PayeezyCreditCard? card { get; set; }

        /// <summary>Gets or sets the error.</summary>
        /// <value>The error.</value>
        public PayeezyApiError? Error { get; set; }
    }
}
