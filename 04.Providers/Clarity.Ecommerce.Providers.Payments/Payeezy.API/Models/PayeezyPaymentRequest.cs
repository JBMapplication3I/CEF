// <copyright file="PayeezyPaymentRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy payment request class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a payeezy payment request.</summary>
    [PublicAPI]
    [Serializable]
    public class PayeezyPaymentRequest
    {
        /// <summary>Gets or sets the merchant reference.</summary>
        /// <value>The merchant reference.</value>
        public string? merchant_ref { get; set; }

        /// <summary>Gets or sets the type of the transaction.</summary>
        /// <value>The type of the transaction.</value>
        public string? transaction_type { get; set; }

        /// <summary>Gets or sets the method.</summary>
        /// <value>The method.</value>
        public string? method { get; set; }

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        public object? amount { get; set; }

        /// <summary>Gets or sets the currency code.</summary>
        /// <value>The currency code.</value>
        public string? currency_code { get; set; }

        /// <summary>Gets or sets the credit card.</summary>
        /// <value>The credit card.</value>
        public PayeezyCreditCard? credit_card { get; set; }

        /// <summary>Gets or sets a value indicating whether the partial redemption.</summary>
        /// <value>True if partial redemption, false if not.</value>
        public bool partial_redemption { get; set; }

        /// <summary>Gets or sets the type of the token.</summary>
        /// <value>The type of the token.</value>
        public PayeezyTokenType? token_type { get; set; }

        /// <summary>Gets or sets information describing the token.</summary>
        /// <value>Information describing the token.</value>
        public PayeezyTokenData? token_data { get; set; }
    }
}
