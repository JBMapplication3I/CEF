// <copyright file="SalesInvoiceEventModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice event model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the sales invoice event.</summary>
    /// <seealso cref="SalesEventBaseModel"/>
    /// <seealso cref="Interfaces.Models.ISalesInvoiceEventModel"/>
    public partial class SalesInvoiceEventModel
    {
        /// <inheritdoc/>
        public decimal? OldBalanceDue { get; set; }

        /// <inheritdoc/>
        public decimal? NewBalanceDue { get; set; }
    }
}
