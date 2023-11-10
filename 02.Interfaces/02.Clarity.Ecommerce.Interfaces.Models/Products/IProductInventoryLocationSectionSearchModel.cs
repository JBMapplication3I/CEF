// <copyright file="IProductInventoryLocationSectionSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductInventoryLocationSectionSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for product inventory location section search model.</summary>
    /// <seealso cref="IBaseSearchModel"/>
    public partial interface IProductInventoryLocationSectionSearchModel
    {
        /// <summary>Gets or sets the identifier of the inventory location.</summary>
        /// <value>The identifier of the inventory location.</value>
        int? InventoryLocationID { get; set; }

        /// <summary>Gets or sets the inventory location key.</summary>
        /// <value>The inventory location key.</value>
        string? InventoryLocationKey { get; set; }

        /// <summary>Gets or sets the name of the inventory location.</summary>
        /// <value>The name of the inventory location.</value>
        string? InventoryLocationName { get; set; }

        /// <summary>Gets or sets the has pre sold quantity.</summary>
        /// <value>The has pre sold quantity.</value>
        bool? HasPreSoldQuantity { get; set; }

        /// <summary>Gets or sets the has broken quantity.</summary>
        /// <value>The has broken quantity.</value>
        bool? HasBrokenQuantity { get; set; }

        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        int? StoreID { get; set; }

        /// <summary>Gets or sets the store IDs.</summary>
        /// <value>The store IDs.</value>
        List<int>? StoreIDs { get; set; }

        /// <summary>Gets or sets the product IDs.</summary>
        /// <value>The product IDs.</value>
        List<int>? ProductIDs { get; set; }
    }
}
