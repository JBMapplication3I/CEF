// <copyright file="DiscountsForQuote.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discounts for quote class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Queries.Services.Endpoints.DTOs
{
    using System.Collections.Generic;
    using Models;

    /// <summary>Information about the discounts for a quote.</summary>
    public class DiscountsForQuote
    {
        /// <summary>Gets or sets the discounts.</summary>
        /// <value>The discounts.</value>
        public List<AppliedSalesQuoteDiscountModel>? Discounts { get; set; }

        /// <summary>Gets or sets the item discounts.</summary>
        /// <value>The item discounts.</value>
        public List<AppliedSalesQuoteItemDiscountModel>? ItemDiscounts { get; set; }
    }
}
