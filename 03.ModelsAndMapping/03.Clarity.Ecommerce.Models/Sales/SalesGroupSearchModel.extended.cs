// <copyright file="SalesGroupSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales group search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;

    /// <summary>A data Model for the sales group search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.ISalesGroupSearchModel"/>
    public partial class SalesGroupSearchModel
    {
        /// <inheritdoc/>
        public string? AccountIDOrCustomKeyOrName { get; set; }

        /// <inheritdoc/>
        public string? BillingContactKey { get; set; }

        /// <inheritdoc/>
        public int? SalesQuoteID { get; set; }

        /// <inheritdoc/>
        public string? SalesQuoteKey { get; set; }

        /// <inheritdoc/>
        public int? SalesOrderID { get; set; }

        /// <inheritdoc/>
        public string? SalesOrderKey { get; set; }

        /// <inheritdoc/>
        public int? PurchaseOrderID { get; set; }

        /// <inheritdoc/>
        public string? PurchaseOrderKey { get; set; }

        /// <inheritdoc/>
        public int? SalesInvoiceID { get; set; }

        /// <inheritdoc/>
        public string? SalesInvoiceKey { get; set; }

        /// <inheritdoc/>
        public int? SalesReturnID { get; set; }

        /// <inheritdoc/>
        public string? SalesReturnKey { get; set; }

        /// <inheritdoc/>
        public int? SampleRequestID { get; set; }

        /// <inheritdoc/>
        public string? SampleRequestKey { get; set; }

        /// <inheritdoc/>
        public DateTime? MinDate { get; set; }

        /// <inheritdoc/>
        public DateTime? MaxDate { get; set; }
    }
}
