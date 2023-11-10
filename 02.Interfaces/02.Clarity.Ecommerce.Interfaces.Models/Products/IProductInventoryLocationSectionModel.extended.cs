// <copyright file="IProductInventoryLocationSectionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductInventoryLocationSectionModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for product inventory location section model.</summary>
    public partial interface IProductInventoryLocationSectionModel
    {
        #region ProductInventoryLocationSection Properties
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        decimal? Quantity { get; set; }

        /// <summary>Gets or sets the quantity allocated.</summary>
        /// <value>The quantity allocated.</value>
        decimal? QuantityAllocated { get; set; }

        /// <summary>Gets or sets the quantity pre sold.</summary>
        /// <value>The quantity pre sold.</value>
        decimal? QuantityPreSold { get; set; }

        /// <summary>Gets or sets the quantity broken.</summary>
        /// <value>The quantity broken.</value>
        decimal? QuantityBroken { get; set; }

        /// <summary>Gets or sets the flat quantity.</summary>
        /// <value>The flat quantity.</value>
        decimal? FlatQuantity { get; set; }

        /// <summary>Gets or sets the flat quantity allocated.</summary>
        /// <value>The flat quantity allocated.</value>
        decimal? FlatQuantityAllocated { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the inventory location section inventory location.</summary>
        /// <value>The identifier of the inventory location section inventory location.</value>
        int? InventoryLocationSectionInventoryLocationID { get; set; }

        /// <summary>Gets or sets the inventory location section inventory location key.</summary>
        /// <value>The inventory location section inventory location key.</value>
        string? InventoryLocationSectionInventoryLocationKey { get; set; }

        /// <summary>Gets or sets the name of the inventory location section inventory location.</summary>
        /// <value>The name of the inventory location section inventory location.</value>
        string? InventoryLocationSectionInventoryLocationName { get; set; }
        #endregion
    }
}
