// <copyright file="PurchaseOrderModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the purchase order model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the purchase order.</summary>
    /// <seealso cref="SalesCollectionBaseModel"/>
    /// <seealso cref="IPurchaseOrderModel"/>
    public partial class PurchaseOrderModel
    {
        #region PurchaseOrder Properties
        /// <inheritdoc/>
        public decimal? Quantity { get; set; }

        /// <inheritdoc/>
        public DateTime? EstimatedReceiptDate { get; set; }

        /// <inheritdoc/>
        public List<int>? SalesOrderIDs { get; set; }

        /// <inheritdoc/>
        public DateTime? ReleaseDate { get; set; }

        /// <inheritdoc/>
        public DateTime? ActualReceiptDate { get; set; }

        /// <inheritdoc/>
        public bool? FOB { get; set; }

        /// <inheritdoc/>
        public string? TrackingNumber { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? ShipCarrierID { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierKey { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierName { get; set; }

        /// <inheritdoc cref="IPurchaseOrderModel.ShipCarrier"/>
        public ShipCarrierModel? ShipCarrier { get; set; }

        /// <inheritdoc/>
        IShipCarrierModel? IPurchaseOrderModel.ShipCarrier { get => ShipCarrier; set => ShipCarrier = (ShipCarrierModel?)value; }

        /// <inheritdoc/>
        public int? VendorID { get; set; }

        /// <inheritdoc/>
        public string? VendorKey { get; set; }

        /// <inheritdoc/>
        public string? VendorName { get; set; }

        /// <inheritdoc cref="IPurchaseOrderModel.Vendor"/>
        public VendorModel? Vendor { get; set; }

        /// <inheritdoc/>
        IVendorModel? IPurchaseOrderModel.Vendor { get => Vendor; set => Vendor = (VendorModel?)value; }

        /// <inheritdoc/>
        public int? InventoryLocationID { get; set; }

        /// <inheritdoc/>
        public string? InventoryLocationKey { get; set; }

        /// <inheritdoc/>
        public string? InventoryLocationName { get; set; }

        /// <inheritdoc cref="IPurchaseOrderModel.InventoryLocation"/>
        public InventoryLocationModel? InventoryLocation { get; set; }

        /// <inheritdoc/>
        IInventoryLocationModel? IPurchaseOrderModel.InventoryLocation { get => InventoryLocation; set => InventoryLocation = (InventoryLocationModel?)value; }

        /// <inheritdoc/>
        public int? SalesGroupID { get; set; }

        /// <inheritdoc/>
        public string? SalesGroupKey { get; set; }

        /// <inheritdoc cref="IPurchaseOrderModel.SalesGroup"/>
        public SalesGroupModel? SalesGroup { get; set; }

        /// <inheritdoc/>
        ISalesGroupModel? IPurchaseOrderModel.SalesGroup { get => SalesGroup; set => SalesGroup = (SalesGroupModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IPurchaseOrderModel.AssociatedSalesOrders"/>
        public List<SalesOrderPurchaseOrderModel>? AssociatedSalesOrders { get; set; }

        /// <inheritdoc/>
        List<ISalesOrderPurchaseOrderModel>? IPurchaseOrderModel.AssociatedSalesOrders { get => AssociatedSalesOrders?.ToList<ISalesOrderPurchaseOrderModel>(); set => AssociatedSalesOrders = value?.Cast<SalesOrderPurchaseOrderModel>().ToList(); }
        #endregion
    }
}
