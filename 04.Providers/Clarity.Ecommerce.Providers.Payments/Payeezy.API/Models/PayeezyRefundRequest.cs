// <copyright file="PayeezyRefundRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy refund request class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a payeezy refund request.</summary>
    [PublicAPI]
    [Serializable]
    public class PayeezyRefundRequest
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
        public string? amount { get; set; }

        /// <summary>Gets or sets the currency code.</summary>
        /// <value>The currency code.</value>
        public string? currency_code { get; set; }

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        public PayeezyTokenDataWrapper? token { get; set; }
    }
}
