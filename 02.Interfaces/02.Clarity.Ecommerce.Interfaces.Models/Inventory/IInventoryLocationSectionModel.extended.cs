// <copyright file="IInventoryLocationSectionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IInventoryLocationSectionModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for inventory location section model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public partial interface IInventoryLocationSectionModel
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the inventory location.</summary>
        /// <value>The identifier of the inventory location.</value>
        int InventoryLocationID { get; set; }

        /// <summary>Gets or sets the inventory location.</summary>
        /// <value>The inventory location.</value>
        IInventoryLocationModel? InventoryLocation { get; set; }

        /// <summary>Gets or sets the inventory location key.</summary>
        /// <value>The inventory location key.</value>
        string? InventoryLocationKey { get; set; }

        /// <summary>Gets or sets the name of the inventory location.</summary>
        /// <value>The name of the inventory location.</value>
        string? InventoryLocationName { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the product inventory location sections.</summary>
        /// <value>The product inventory location sections.</value>
        List<IProductInventoryLocationSectionModel>? ProductInventoryLocationSections { get; set; }

        /// <summary>Gets or sets the shipments.</summary>
        /// <value>The shipments.</value>
        List<IShipmentModel>? Shipments { get; set; }
        #endregion
    }
}
