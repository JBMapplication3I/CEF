// <copyright file="DiscountsForInvoice.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discounts for invoice class</summary>
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Queries.Services.Endpoints.DTOs
{
    using System.Collections.Generic;
    using Models;

    /// <summary>Information about the discounts for an invoice.</summary>
    public class DiscountsForInvoice
    {
        /// <summary>Gets or sets the discounts.</summary>
        /// <value>The discounts.</value>
        public List<AppliedSalesInvoiceDiscountModel>? Discounts { get; set; }

        /// <summary>Gets or sets the item discounts.</summary>
        /// <value>The item discounts.</value>
        public List<AppliedSalesInvoiceItemDiscountModel>? ItemDiscounts { get; set; }
    }
}
