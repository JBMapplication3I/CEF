// <copyright file="PayeezyCreditCard.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy credit card class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a payeezy credit card.</summary>
    [PublicAPI, Serializable]
    public class PayeezyCreditCard
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        /// <remarks>values - "American Express", "Visa", "Mastercard" or "Discover"</remarks>
        public string? type { get; set; }

        /// <summary>Gets or sets the name of the cardholder.</summary>
        /// <value>The name of the cardholder.</value>
        /// <remarks>Name of the card holder</remarks>
        public string? cardholder_name { get; set; }

        /// <summary>Gets or sets the card number.</summary>
        /// <remarks>complete credit card number:<br/>
        /// VISA / Mastercard / Discover - 16 digits<br/>
        /// American Express - 15 digits</remarks>
        /// <value>The card number.</value>
        public string? card_number { get; set; }

        /// <summary>Gets or sets the exponent date.</summary>
        /// <value>The exponent date.</value>
        /// <remarks>Expiration Date on the card - MMYY format. eg:1014</remarks>
        public string? exp_date { get; set; }

        /// <summary>Gets or sets the cvv.</summary>
        /// <value>The cvv.</value>
        /// <remarks>Card Verification Value number present on card.</remarks>
        public string? cvv { get; set; }
    }
}
