// <copyright file="ISalesInvoiceEventModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesInvoiceEventModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface ISalesInvoiceEventModel
    {
        /// <summary>Gets or sets the old balance due.</summary>
        /// <value>The old balance due.</value>
        decimal? OldBalanceDue { get; set; }

        /// <summary>Gets or sets the new balance due.</summary>
        /// <value>The new balance due.</value>
        decimal? NewBalanceDue { get; set; }
    }
}
