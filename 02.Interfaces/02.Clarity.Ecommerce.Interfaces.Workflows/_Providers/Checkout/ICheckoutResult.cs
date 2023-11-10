// <copyright file="ICheckoutResult.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICheckoutResult interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for checkout result.</summary>
    public interface ICheckoutResult
    {
        /// <summary>Gets or sets a value indicating whether the succeeded.</summary>
        /// <value>True if succeeded, false if not.</value>
        bool Succeeded { get; set; }

        #region Single Order return
        /// <summary>Gets or sets a message describing the warning.</summary>
        /// <value>A message describing the warning.</value>
        string? WarningMessage { get; set; }

        /// <summary>Gets or sets a message describing the error.</summary>
        /// <value>A message describing the error.</value>
        string? ErrorMessage { get; set; }

        /// <summary>Gets or sets the identifier of the order.</summary>
        /// <value>The identifier of the order.</value>
        int? OrderID { get; set; }

        /// <summary>Gets or sets the identifier of the payment transaction.</summary>
        /// <value>The identifier of the payment transaction.</value>
        string? PaymentTransactionID { get; set; }

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        string? Token { get; set; }
        #endregion

        #region Multi-Order return (New 2017 4.7)
        /// <summary>Gets or sets the warning messages.</summary>
        /// <value>The warning messages.</value>
        List<string> WarningMessages { get; set; }

        /// <summary>Gets or sets the error messages.</summary>
        /// <value>The error messages.</value>
        List<string> ErrorMessages { get; set; }

        /// <summary>Gets or sets the order IDs.</summary>
        /// <value>The order i ds.</value>
        int[]? OrderIDs { get; set; }

        /// <summary>Gets or sets the payment transaction IDs.</summary>
        /// <value>The payment transaction IDs.</value>
        string?[]? PaymentTransactionIDs { get; set; }

        /// <summary>Gets or sets the tokens.</summary>
        /// <value>The tokens.</value>
        string?[]? Tokens { get; set; }
        #endregion

        #region Single Quote return
        /// <summary>Gets or sets the identifier of the quote.</summary>
        /// <value>The identifier of the quote.</value>
        int? QuoteID { get; set; }
        #endregion

        #region Multi-Quote return
        /// <summary>Gets or sets the quote IDs.</summary>
        /// <value>The quote IDs.</value>
        int[]? QuoteIDs { get; set; }
        #endregion
    }
}
