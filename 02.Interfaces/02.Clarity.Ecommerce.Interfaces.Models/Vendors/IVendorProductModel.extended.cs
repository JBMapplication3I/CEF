// <copyright file="IVendorProductModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IVendorProductModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for vendor product model.</summary>
    /// <seealso cref="IBaseModel"/>
    public partial interface IVendorProductModel
    {
        /// <summary>Gets or sets the bin.</summary>
        /// <value>The bin.</value>
        string? Bin { get; set; }

        /// <summary>Gets or sets the minimum inventory.</summary>
        /// <value>The minimum inventory.</value>
        int? MinimumInventory { get; set; }

        /// <summary>Gets or sets the maximum inventory.</summary>
        /// <value>The maximum inventory.</value>
        int? MaximumInventory { get; set; }

        /// <summary>Gets or sets the cost multiplier.</summary>
        /// <value>The cost multiplier.</value>
        decimal? CostMultiplier { get; set; }

        /// <summary>Gets or sets the listed price.</summary>
        /// <value>The listed price.</value>
        decimal? ListedPrice { get; set; }

        /// <summary>Gets or sets the actual cost.</summary>
        /// <value>The actual cost.</value>
        decimal? ActualCost { get; set; }

        /// <summary>Gets or sets the number of inventories.</summary>
        /// <value>The number of inventories.</value>
        int? InventoryCount { get; set; }
    }
}
