// <copyright file="ISalesOrderEventModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesOrderEventModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for sales order event model.</summary>
    public partial interface ISalesOrderEventModel
    {
        /// <summary>Gets or sets the old balance due.</summary>
        /// <value>The old balance due.</value>
        decimal? OldBalanceDue { get; set; }

        /// <summary>Gets or sets the new balance due.</summary>
        /// <value>The new balance due.</value>
        decimal? NewBalanceDue { get; set; }
    }
}
