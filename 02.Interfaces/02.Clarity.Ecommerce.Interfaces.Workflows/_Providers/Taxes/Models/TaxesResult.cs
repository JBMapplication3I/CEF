// <copyright file="TaxesResult.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the taxes result class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Taxes
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for taxes result.</summary>
    public class TaxesResult
    {
        /// <summary>Gets or sets the identifier of the cart.</summary>
        /// <value>The identifier of the cart.</value>
        public int CartID { get; set; }

        /// <summary>Gets or sets the identifier of the cart session.</summary>
        /// <value>The identifier of the cart session.</value>
        public Guid? CartSessionID { get; set; }

        /// <summary>Gets or sets the total number of taxes.</summary>
        /// <value>The total number of taxes.</value>
        public decimal TotalTaxes { get; set; }

        /// <summary>Gets or sets the tax line items.</summary>
        /// <value>The tax line items.</value>
        public List<TaxLineItemResult>? TaxLineItems { get; set; }

        /// <summary>Gets or sets the error messages.</summary>
        /// <value>The error messages.</value>
        public List<string>? ErrorMessages { get; set; }
    }
}
