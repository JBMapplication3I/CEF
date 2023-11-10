// <copyright file="TaxLineItemResult.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the TaxLineItemResult interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Taxes
{
    /// <summary>Interface for tax line item result.</summary>
    public class TaxLineItemResult
    {
        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        public int? ProductID { get; set; }

        /// <summary>Gets or sets the SKU.</summary>
        /// <value>The SKU.</value>
        public string? SKU { get; set; }

        /// <summary>Gets or sets the identifier of the cart item.</summary>
        /// <value>The identifier of the cart item.</value>
        public int? CartItemID { get; set; }

        /// <summary>Gets or sets the tax.</summary>
        /// <value>The tax.</value>
        public decimal Tax { get; set; }
    }
}
