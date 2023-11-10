// <copyright file="ShipmentModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the shipment.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IShipmentModel"/>
    public partial class ShipmentModel
    {
        #region Shipment Properties
        /// <inheritdoc/>
        public string? Reference1 { get; set; }

        /// <inheritdoc/>
        public string? Reference2 { get; set; }

        /// <inheritdoc/>
        public string? Reference3 { get; set; }

        /// <inheritdoc/>
        public string? TrackingNumber { get; set; }

        /// <inheritdoc/>
        public string? Destination { get; set; }

        /// <inheritdoc/>
        public DateTime? TargetShippingDate { get; set; }

        /// <inheritdoc/>
        public DateTime? EstimatedDeliveryDate { get; set; }

        /// <inheritdoc/>
        public DateTime? ShipDate { get; set; }

        /// <inheritdoc/>
        public DateTime? DateDelivered { get; set; }

        /// <inheritdoc/>
        public decimal? NegotiatedRate { get; set; }

        /// <inheritdoc/>
        public decimal? PublishedRate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int OriginContactID { get; set; }

        /// <inheritdoc/>
        public string? OriginContactKey { get; set; }

        /// <inheritdoc cref="IShipmentModel.OriginContact"/>
        public ContactModel? OriginContact { get; set; }

        /// <inheritdoc/>
        IContactModel? IShipmentModel.OriginContact { get => OriginContact; set => OriginContact = (ContactModel?)value; }

        /// <inheritdoc/>
        public int DestinationContactID { get; set; }

        /// <inheritdoc/>
        public string? DestinationContactKey { get; set; }

        /// <inheritdoc cref="IShipmentModel.DestinationContact"/>
        public ContactModel? DestinationContact { get; set; }

        /// <inheritdoc/>
        IContactModel? IShipmentModel.DestinationContact { get => DestinationContact; set => DestinationContact = (ContactModel?)value; }

        /// <inheritdoc/>
        public int? InventoryLocationSectionID { get; set; }

        /// <inheritdoc/>
        public string? InventoryLocationSectionKey { get; set; }

        /// <inheritdoc/>
        public string? InventoryLocationSectionName { get; set; }

        /// <inheritdoc cref="IShipmentModel.InventoryLocationSection"/>
        public InventoryLocationSectionModel? InventoryLocationSection { get; set; }

        /// <inheritdoc/>
        IInventoryLocationSectionModel? IShipmentModel.InventoryLocationSection { get => InventoryLocationSection; set => InventoryLocationSection = (InventoryLocationSectionModel?)value; }

        /// <inheritdoc/>
        public int? ShipCarrierID { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierKey { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierName { get; set; }

        /// <inheritdoc cref="IShipmentModel.ShipCarrier"/>
        public ShipCarrierModel? ShipCarrier { get; set; }

        /// <inheritdoc/>
        IShipCarrierModel? IShipmentModel.ShipCarrier { get => ShipCarrier; set => ShipCarrier = (ShipCarrierModel?)value; }

        /// <inheritdoc/>
        public int? ShipCarrierMethodID { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierMethodKey { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierMethodName { get; set; }

        /// <inheritdoc cref="IShipmentModel.ShipCarrierMethod"/>
        public ShipCarrierMethodModel? ShipCarrierMethod { get; set; }

        /// <inheritdoc/>
        IShipCarrierMethodModel? IShipmentModel.ShipCarrierMethod { get => ShipCarrierMethod; set => ShipCarrierMethod = (ShipCarrierMethodModel?)value; }

        /// <inheritdoc/>
        public int? VendorID { get; set; }

        /// <inheritdoc/>
        public string? VendorKey { get; set; }

        /// <inheritdoc/>
        public string? VendorName { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IShipmentModel.ShipmentEvents"/>
        public List<ShipmentEventModel>? ShipmentEvents { get; set; }

        /// <inheritdoc/>
        List<IShipmentEventModel>? IShipmentModel.ShipmentEvents { get => ShipmentEvents?.ToList<IShipmentEventModel>(); set => ShipmentEvents = value?.Cast<ShipmentEventModel>().ToList(); }

        public List<ShipmentLineModel>? ShipmentLines { get; set; }

        /// <inheritdoc/>
        List<IShipmentLineModel>? IShipmentModel.ShipmentLines { get => ShipmentLines?.ToList<IShipmentLineModel>(); set => ShipmentLines = value?.Cast<ShipmentLineModel>().ToList(); }
        #endregion

        #region Properties from Related Objects, stored here for Flattening
        /// <inheritdoc/>
        public string? CarrierName { get; set; }

        /// <inheritdoc/>
        public string? ShipMethod { get; set; }

        /// <inheritdoc/>
        public string? LastLocation { get; set; }

        /// <inheritdoc/>
        public string? CompanyName { get; set; }

        /// <inheritdoc/>
        public int VarianceLevel { get; set; }
        #endregion

        #region Properties from Associated Objects, stored here for Flattening
        /// <inheritdoc/>
        public List<int>? SalesOrderIDs { get; set; }

        /// <inheritdoc/>
        public List<int>? SalesOrderItemIDs { get; set; }
        #endregion
    }
}