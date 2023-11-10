// <copyright file="SalesOrderEventModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order event model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the sales order event.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="Interfaces.Models.ISalesOrderEventModel"/>
    public partial class SalesOrderEventModel
    {
        /// <inheritdoc/>
        public decimal? OldBalanceDue { get; set; }

        /// <inheritdoc/>
        public decimal? NewBalanceDue { get; set; }
    }
}
