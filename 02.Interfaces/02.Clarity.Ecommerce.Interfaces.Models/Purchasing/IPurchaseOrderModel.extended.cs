// <copyright file="IPurchaseOrderModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPurchaseOrderModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for purchase order model.</summary>
    public partial interface IPurchaseOrderModel
    {
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        decimal? Quantity { get; set; }

        /// <summary>Gets or sets the estimated receipt date.</summary>
        /// <value>The estimated receipt date.</value>
        DateTime? EstimatedReceiptDate { get; set; }

        /// <summary>Gets or sets the release date.</summary>
        /// <value>The release date.</value>
        DateTime? ReleaseDate { get; set; }

        /// <summary>Gets or sets the actual receipt date.</summary>
        /// <value>The actual receipt date.</value>
        DateTime? ActualReceiptDate { get; set; }

        /// <summary>Gets or sets the fob.</summary>
        /// <value>The fob.</value>
        bool? FOB { get; set; }

        /// <summary>Gets or sets the tracking number.</summary>
        /// <value>The tracking number.</value>
        string? TrackingNumber { get; set; }

        #region Related Objects
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

        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int? VendorID { get; set; }

        /// <summary>Gets or sets the vendor key.</summary>
        /// <value>The vendor key.</value>
        string? VendorKey { get; set; }

        /// <summary>Gets or sets the name of the vendor.</summary>
        /// <value>The name of the vendor.</value>
        string? VendorName { get; set; }

        /// <summary>Gets or sets the vendor.</summary>
        /// <value>The vendor.</value>
        IVendorModel? Vendor { get; set; }

        /// <summary>Gets or sets the identifier of the inventory location.</summary>
        /// <value>The identifier of the inventory location.</value>
        int? InventoryLocationID { get; set; }

        /// <summary>Gets or sets the inventory location key.</summary>
        /// <value>The inventory location key.</value>
        string? InventoryLocationKey { get; set; }

        /// <summary>Gets or sets the name of the inventory location.</summary>
        /// <value>The name of the inventory location.</value>
        string? InventoryLocationName { get; set; }

        /// <summary>Gets or sets the inventory location.</summary>
        /// <value>The inventory location.</value>
        IInventoryLocationModel? InventoryLocation { get; set; }

        /// <summary>Gets or sets the identifier of the sales group.</summary>
        /// <value>The identifier of the sales group.</value>
        int? SalesGroupID { get; set; }

        /// <summary>Gets or sets the sales group key.</summary>
        /// <value>The sales group key.</value>
        string? SalesGroupKey { get; set; }

        /// <summary>Gets or sets the group the sales belongs to.</summary>
        /// <value>The sales group.</value>
        ISalesGroupModel? SalesGroup { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the sales order i ds.</summary>
        /// <value>The sales order i ds.</value>
        List<int>? SalesOrderIDs { get; set; }

        /// <summary>Gets or sets the associated sales orders.</summary>
        /// <value>The associated sales orders.</value>
        List<ISalesOrderPurchaseOrderModel>? AssociatedSalesOrders { get; set; }
        #endregion
    }
}
