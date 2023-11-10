// <copyright file="IShipmentModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IShipmentModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for shipment model.</summary>
    public partial interface IShipmentModel
    {
        #region Shipment Properties
        /// <summary>Gets or sets the reference 1.</summary>
        /// <value>The reference 1.</value>
        string? Reference1 { get; set; }

        /// <summary>Gets or sets the reference 2.</summary>
        /// <value>The reference 2.</value>
        string? Reference2 { get; set; }

        /// <summary>Gets or sets the reference 3.</summary>
        /// <value>The reference 3.</value>
        string? Reference3 { get; set; }

        /// <summary>Gets or sets the tracking number.</summary>
        /// <value>The tracking number.</value>
        string? TrackingNumber { get; set; }

        /// <summary>Gets or sets the Destination for the.</summary>
        /// <value>The destination.</value>
        string? Destination { get; set; }

        /// <summary>Gets or sets the target shipping date.</summary>
        /// <value>The target shipping date.</value>
        DateTime? TargetShippingDate { get; set; }

        /// <summary>Gets or sets the estimated delivery date.</summary>
        /// <value>The estimated delivery date.</value>
        DateTime? EstimatedDeliveryDate { get; set; }

        /// <summary>Gets or sets the ship date.</summary>
        /// <value>The ship date.</value>
        DateTime? ShipDate { get; set; }

        /// <summary>Gets or sets the Date/Time of the date delivered.</summary>
        /// <value>The date delivered.</value>
        DateTime? DateDelivered { get; set; }

        /// <summary>Gets or sets the negotiated rate.</summary>
        /// <value>The negotiated rate.</value>
        decimal? NegotiatedRate { get; set; }

        /// <summary>Gets or sets the published rate.</summary>
        /// <value>The published rate.</value>
        decimal? PublishedRate { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the origin contact.</summary>
        /// <value>The identifier of the origin contact.</value>
        int OriginContactID { get; set; }

        /// <summary>Gets or sets the origin contact key.</summary>
        /// <value>The origin contact key.</value>
        string? OriginContactKey { get; set; }

        /// <summary>Gets or sets the origin contact.</summary>
        /// <value>The origin contact.</value>
        IContactModel? OriginContact { get; set; }

        /// <summary>Gets or sets the identifier of the destination contact.</summary>
        /// <value>The identifier of the destination contact.</value>
        int DestinationContactID { get; set; }

        /// <summary>Gets or sets destination contact key.</summary>
        /// <value>The destination contact key.</value>
        string? DestinationContactKey { get; set; }

        /// <summary>Gets or sets destination contact.</summary>
        /// <value>The destination contact.</value>
        IContactModel? DestinationContact { get; set; }

        /// <summary>Gets or sets the identifier of the inventory location section.</summary>
        /// <value>The identifier of the inventory location section.</value>
        int? InventoryLocationSectionID { get; set; }

        /// <summary>Gets or sets the inventory location section key.</summary>
        /// <value>The inventory location section key.</value>
        string? InventoryLocationSectionKey { get; set; }

        /// <summary>Gets or sets the name of the inventory location section.</summary>
        /// <value>The name of the inventory location section.</value>
        string? InventoryLocationSectionName { get; set; }

        /// <summary>Gets or sets the inventory location section.</summary>
        /// <value>The inventory location section.</value>
        IInventoryLocationSectionModel? InventoryLocationSection { get; set; }

        /// <summary>Gets or sets the identifier of the ship carrier.</summary>
        /// <value>The identifier of the ship carrier.</value>
        int? ShipCarrierID { get; set; }

        /// <summary>Gets or sets the ship carrier key.</summary>
        /// <value>The ship carrier key.</value>
        string? ShipCarrierKey { get; set; }

        /// <summary>Gets or sets the name of the ship carrier.</summary>
        /// <value>The name of the ship carrier.</value>
        string? ShipCarrierName { get; set; }

        /// <summary>Gets or sets the ship carrier.</summary>
        /// <value>The ship carrier.</value>
        IShipCarrierModel? ShipCarrier { get; set; }

        /// <summary>Gets or sets the identifier of the ship carrier method.</summary>
        /// <value>The identifier of the ship carrier method.</value>
        int? ShipCarrierMethodID { get; set; }

        /// <summary>Gets or sets the ship carrier method key.</summary>
        /// <value>The ship carrier method key.</value>
        string? ShipCarrierMethodKey { get; set; }

        /// <summary>Gets or sets the name of the ship carrier method.</summary>
        /// <value>The name of the ship carrier method.</value>
        string? ShipCarrierMethodName { get; set; }

        /// <summary>Gets or sets the ship carrier method.</summary>
        /// <value>The ship carrier method.</value>
        IShipCarrierMethodModel? ShipCarrierMethod { get; set; }

        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int? VendorID { get; set; }

        /// <summary>Gets or sets the vendor key.</summary>
        /// <value>The vendor key.</value>
        string? VendorKey { get; set; }

        /// <summary>Gets or sets the name of the vendor.</summary>
        /// <value>The name of the vendor.</value>
        string? VendorName { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the shipment events.</summary>
        /// <value>The shipment events.</value>
        List<IShipmentEventModel>? ShipmentEvents { get; set; }

        /// <summary>Gets or sets the shipment lines.</summary>
        /// <value>The shipment lines.</value>
        List<IShipmentLineModel>? ShipmentLines { get; set; }
        #endregion

        #region Properties from Related Objects, stored here for Flattening
        /// <summary>Gets or sets the name of the carrier.</summary>
        /// <value>The name of the carrier.</value>
        string? CarrierName { get; set; }

        /// <summary>Gets or sets the ship method.</summary>
        /// <value>The ship method.</value>
        string? ShipMethod { get; set; }

        /// <summary>Gets or sets the last location.</summary>
        /// <value>The last location.</value>
        string? LastLocation { get; set; }

        /// <summary>Gets or sets the name of the company.</summary>
        /// <value>The name of the company.</value>
        string? CompanyName { get; set; }

        /// <summary>Gets or sets the variance level.</summary>
        /// <value>The variance level.</value>
        int VarianceLevel { get; set; }

        /// <summary>Gets or sets the sales order i ds.</summary>
        /// <value>The sales order i ds.</value>
        List<int>? SalesOrderIDs { get; set; }

        /// <summary>Gets or sets the sales order item i ds.</summary>
        /// <value>The sales order item i ds.</value>
        List<int>? SalesOrderItemIDs { get; set; }
        #endregion
    }
}
