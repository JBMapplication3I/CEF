// <copyright file="PayeezyTokenData.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy token data class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using JetBrains.Annotations;

    /// <summary>(Serializable)a payeezy token data.</summary>
    [PublicAPI]
    [Serializable]
    public class PayeezyTokenData
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        public string? type { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        public string? value { get; set; }

        /// <summary>Gets or sets the name of the cardholder.</summary>
        /// <value>The name of the cardholder.</value>
        public string? cardholder_name { get; set; }

        /// <summary>Gets or sets the exponent date.</summary>
        /// <value>The exponent date.</value>
        public string? exp_date { get; set; }
    }
}
