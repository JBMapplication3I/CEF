// <copyright file="CheckoutResult.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout result class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using Interfaces.Models;

    /// <summary>Encapsulates the result of a checkout.</summary>
    /// <seealso cref="ICheckoutResult"/>
    public class CheckoutResult : ICheckoutResult
    {
        /// <summary>Initializes a new instance of the <see cref="CheckoutResult"/> class.</summary>
        public CheckoutResult()
        {
            WarningMessages = new();
            ErrorMessages = new();
        }

        /// <inheritdoc/>
        public bool Succeeded { get; set; }

        // Single Order return (Deprecated)

        /// <inheritdoc/>
        public string? WarningMessage { get; set; }

        /// <inheritdoc/>
        public string? ErrorMessage { get; set; }

        /// <inheritdoc/>
        public int? OrderID { get; set; }

        /// <inheritdoc/>
        public int? QuoteID { get; set; }

        /// <inheritdoc/>
        public string? PaymentTransactionID { get; set; }

        /// <inheritdoc/>
        public string? Token { get; set; }

        // Multi-Order return (New 2017 4.7)

        /// <inheritdoc/>
        public List<string> WarningMessages { get; set; }

        /// <inheritdoc/>
        public List<string> ErrorMessages { get; set; }

        /// <inheritdoc/>
        public int[]? OrderIDs { get; set; }

        /// <inheritdoc/>
        public int[]? QuoteIDs { get; set; }

        /// <inheritdoc/>
        public string?[]? PaymentTransactionIDs { get; set; }

        /// <inheritdoc/>
        public string?[]? Tokens { get; set; }
    }
}
