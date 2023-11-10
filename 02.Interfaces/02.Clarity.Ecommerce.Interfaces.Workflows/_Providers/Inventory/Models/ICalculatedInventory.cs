// <copyright file="ICalculatedInventory.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICalculatedInventory interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>A calculated inventory.</summary>
    public interface ICalculatedInventory
    {
        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        int ProductID { get; set; }

        /// <summary>Gets or sets the products UOM.</summary>
        /// <value>The UOM of the product.</value>
        string? ProductUOM { get; set; }

        /// <summary>Gets or sets a value indicating whether this product is discontinued.</summary>
        /// <value>True if this product is discontinued, false if not.</value>
        bool IsDiscontinued { get; set; }

        /// <summary>Gets or sets a value indicating whether this product has unlimited stock.</summary>
        /// <value>True if this product has unlimited stock, false if not.</value>
        bool IsUnlimitedStock { get; set; }

        /// <summary>Gets or sets a value indicating whether this product is out of stock.</summary>
        /// <value>True if this product is out of stock, false if not.</value>
        bool IsOutOfStock { get; set; }

        /// <summary>Gets or sets the quantity present.</summary>
        /// <value>The quantity present.</value>
        decimal? QuantityPresent { get; set; }

        /// <summary>Gets or sets the quantity allocated.</summary>
        /// <value>The quantity allocated.</value>
        decimal? QuantityAllocated { get; set; }

        /// <summary>Gets or sets the quantity on hand.</summary>
        /// <value>The quantity on hand.</value>
        decimal? QuantityOnHand { get; set; }

        /// <summary>Gets or sets the maximum purchase quantity.</summary>
        /// <value>The maximum purchase quantity.</value>
        decimal? MaximumPurchaseQuantity { get; set; }

        /// <summary>Gets or sets the maximum purchase quantity if past purchased.</summary>
        /// <value>The maximum purchase quantity if past purchased.</value>
        decimal? MaximumPurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets a value indicating whether we allow back order for this product.</summary>
        /// <value>True if allow back order for this product, false if not.</value>
        bool AllowBackOrder { get; set; }

        /// <summary>Gets or sets the maximum back order purchase quantity.</summary>
        /// <value>The maximum back order purchase quantity.</value>
        decimal? MaximumBackOrderPurchaseQuantity { get; set; }

        /// <summary>Gets or sets the maximum back order purchase quantity if past purchased.</summary>
        /// <value>The maximum back order purchase quantity if past purchased.</value>
        decimal? MaximumBackOrderPurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the maximum back order purchase quantity global.</summary>
        /// <value>The maximum back order purchase quantity global.</value>
        decimal? MaximumBackOrderPurchaseQuantityGlobal { get; set; }

        /// <summary>Gets or sets a value indicating whether we allow pre sale for this product.</summary>
        /// <value>True if allow pre sale, false if not for this product.</value>
        bool AllowPreSale { get; set; }

        /// <summary>Gets or sets the pre sell end date.</summary>
        /// <value>The pre sell end date.</value>
        DateTime? PreSellEndDate { get; set; }

        /// <summary>Gets or sets the quantity pre-sell-able.</summary>
        /// <value>The quantity pre sell-able.</value>
        decimal? QuantityPreSellable { get; set; }

        /// <summary>Gets or sets the quantity for pre sold.</summary>
        /// <value>The quantity for pre sold.</value>
        decimal? QuantityPreSold { get; set; }

        /// <summary>Gets or sets the maximum pre purchase quantity.</summary>
        /// <value>The maximum pre purchase quantity.</value>
        decimal? MaximumPrePurchaseQuantity { get; set; }

        /// <summary>Gets or sets the maximum pre purchase quantity if past purchased.</summary>
        /// <value>The maximum pre purchase quantity if past purchased.</value>
        decimal? MaximumPrePurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the maximum pre purchase quantity global.</summary>
        /// <value>The maximum pre purchase quantity global.</value>
        decimal? MaximumPrePurchaseQuantityGlobal { get; set; }

        /// <summary>Gets or sets the relevant locations.</summary>
        /// <value>The relevant locations.</value>
        List<IProductInventoryLocationSectionModel>? RelevantLocations { get; set; }
    }
}
