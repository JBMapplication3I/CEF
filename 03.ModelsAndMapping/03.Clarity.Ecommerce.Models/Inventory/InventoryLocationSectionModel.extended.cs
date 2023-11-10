// <copyright file="InventoryLocationSectionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inventory location section model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the inventory location section.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IInventoryLocationSectionModel"/>
    public partial class InventoryLocationSectionModel
    {
        #region Related Objects
        /// <inheritdoc/>
        public int InventoryLocationID { get; set; }

        /// <inheritdoc/>
        public string? InventoryLocationKey { get; set; }

        /// <inheritdoc/>
        public string? InventoryLocationName { get; set; }

        /// <inheritdoc cref="IInventoryLocationSectionModel.InventoryLocation"/>
        public InventoryLocationModel? InventoryLocation { get; set; }

        /// <inheritdoc/>
        IInventoryLocationModel? IInventoryLocationSectionModel.InventoryLocation { get => InventoryLocation; set => InventoryLocation = (InventoryLocationModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IInventoryLocationSectionModel.ProductInventoryLocationSections"/>
        public List<ProductInventoryLocationSectionModel>? ProductInventoryLocationSections { get; set; }

        /// <inheritdoc/>
        List<IProductInventoryLocationSectionModel>? IInventoryLocationSectionModel.ProductInventoryLocationSections { get => ProductInventoryLocationSections?.ToList<IProductInventoryLocationSectionModel>(); set => ProductInventoryLocationSections = value?.Cast<ProductInventoryLocationSectionModel>().ToList(); }

        /// <inheritdoc cref="IInventoryLocationSectionModel.Shipments"/>
        public List<ShipmentModel>? Shipments { get; set; }

        /// <inheritdoc/>
        List<IShipmentModel>? IInventoryLocationSectionModel.Shipments { get => Shipments?.ToList<IShipmentModel>(); set => Shipments = value?.Cast<ShipmentModel>().ToList(); }
        #endregion
    }
}
