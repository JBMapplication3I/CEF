// <copyright file="CartTotals.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart totals class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Interfaces.Models;

    /// <summary>A cart totals.</summary>
    /// <seealso cref="ICartTotals"/>
    public class CartTotals : ICartTotals
    {
        /// <inheritdoc/>
        public decimal SubTotal { get; set; }

        /// <inheritdoc/>
        public decimal Shipping { get; set; }

        /// <inheritdoc/>
        public decimal Handling { get; set; }

        /// <inheritdoc/>
        public decimal Fees { get; set; }

        /// <inheritdoc/>
        public decimal Discounts { get; set; }

        /// <inheritdoc/>
        public decimal Tax { get; set; }

        /// <inheritdoc/>
        public decimal Total => GetTotal();

        private decimal GetTotal()
        {
            return typeof(CartTotals)
                .GetTypeInfo()
                .DeclaredProperties
                .Where(x => !x.Name.Equals(nameof(Total)))
                .Sum(x => Convert.ToDecimal(x.GetValue(this, null)));
        }
    }
}
