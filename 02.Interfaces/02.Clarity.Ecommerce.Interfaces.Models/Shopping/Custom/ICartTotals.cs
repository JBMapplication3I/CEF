// <copyright file="ICartTotals.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICartTotals interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for cart totals.</summary>
    public interface ICartTotals
    {
        /// <summary>Gets or sets the sub total.</summary>
        /// <value>The sub total.</value>
        decimal SubTotal { get; set; }

        /// <summary>Gets or sets the shipping.</summary>
        /// <value>The shipping.</value>
        decimal Shipping { get; set; }

        /// <summary>Gets or sets the handling.</summary>
        /// <value>The handling.</value>
        decimal Handling { get; set; }

        /// <summary>Gets or sets the fees.</summary>
        /// <value>The fees.</value>
        decimal Fees { get; set; }

        /// <summary>Gets or sets the discounts.</summary>
        /// <value>The discounts.</value>
        decimal Discounts { get; set; }

        /// <summary>Gets or sets the tax.</summary>
        /// <value>The tax.</value>
        decimal Tax { get; set; }

        /// <summary>Gets the total of the other values.</summary>
        /// <value>The total.</value>
        decimal Total { get; }
    }
}
