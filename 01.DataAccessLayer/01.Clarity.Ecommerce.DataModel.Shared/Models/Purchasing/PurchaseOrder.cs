// <copyright file="PurchaseOrder.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the purchase order class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IPurchaseOrder
        : ISalesCollectionBase<PurchaseOrder,
                PurchaseOrderStatus,
                PurchaseOrderType,
                PurchaseOrderItem,
                AppliedPurchaseOrderDiscount,
                PurchaseOrderState,
                PurchaseOrderFile,
                PurchaseOrderContact,
                PurchaseOrderEvent,
                PurchaseOrderEventType>,
            IHaveNotesBase
    {
        #region PurchaseOrder Properties
        /// <summary>Gets or sets the release date.</summary>
        /// <value>The release date.</value>
        DateTime? ReleaseDate { get; set; }

        /// <summary>Gets or sets the estimated receipt date.</summary>
        /// <value>The estimated receipt date.</value>
        DateTime? EstimatedReceiptDate { get; set; }

        /// <summary>Gets or sets the actual receipt date.</summary>
        /// <value>The actual receipt date.</value>
        DateTime? ActualReceiptDate { get; set; }

        /// <summary>Gets or sets the tracking number.</summary>
        /// <value>The tracking number.</value>
        string? TrackingNumber { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the inventory location.</summary>
        /// <value>The identifier of the inventory location.</value>
        int? InventoryLocationID { get; set; }

        /// <summary>Gets or sets the inventory location.</summary>
        /// <value>The inventory location.</value>
        InventoryLocation? InventoryLocation { get; set; }

        /// <summary>Gets or sets the identifier of the ship carrier.</summary>
        /// <value>The identifier of the ship carrier.</value>
        int? ShipCarrierID { get; set; }

        /// <summary>Gets or sets the ship carrier.</summary>
        /// <value>The ship carrier.</value>
        ShipCarrier? ShipCarrier { get; set; }

        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int? VendorID { get; set; }

        /// <summary>Gets or sets the vendor.</summary>
        /// <value>The vendor.</value>
        Vendor? Vendor { get; set; }

        /// <summary>Gets or sets the identifier of the sales group.</summary>
        /// <value>The identifier of the sales group.</value>
        int? SalesGroupID { get; set; }

        /// <summary>Gets or sets the group the sales belongs to.</summary>
        /// <value>The sales group.</value>
        SalesGroup? SalesGroup { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the associated sales orders.</summary>
        /// <value>The associated sales orders.</value>
        ICollection<SalesOrderPurchaseOrder>? AssociatedSalesOrders { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Purchasing", "PurchaseOrder")]
    public class PurchaseOrder
        : SalesCollectionBase<PurchaseOrder,
            PurchaseOrderStatus,
            PurchaseOrderType,
            PurchaseOrderItem,
            AppliedPurchaseOrderDiscount,
            PurchaseOrderState,
            PurchaseOrderFile,
            PurchaseOrderContact,
            PurchaseOrderEvent,
            PurchaseOrderEventType>,
            IPurchaseOrder
    {
        private ICollection<Note>? notes;
        private ICollection<SalesOrderPurchaseOrder>? associatedSalesOrders;

        public PurchaseOrder()
        {
            // HaveNotesBase
            notes = new HashSet<Note>();
            // PurchaseOrder Properties
            associatedSalesOrders = new HashSet<SalesOrderPurchaseOrder>();
        }

        #region HaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        [StringLength(50), StringIsUnicode(false)]
        public string? TrackingNumber { get; set; }

        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime? ReleaseDate { get; set; }

        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime? EstimatedReceiptDate { get; set; }

        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime? ActualReceiptDate { get; set; }

        // Related Objects
        [InverseProperty(nameof(ID)), ForeignKey(nameof(InventoryLocation))]
        public int? InventoryLocationID { get; set; }

        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual InventoryLocation? InventoryLocation { get; set; }

        [InverseProperty(nameof(ID)), ForeignKey(nameof(ShipCarrier))]
        public int? ShipCarrierID { get; set; }

        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ShipCarrier? ShipCarrier { get; set; }

        [InverseProperty(nameof(ID)), ForeignKey(nameof(Vendor))]
        public int? VendorID { get; set; }

        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Vendor? Vendor { get; set; }

        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesGroup))]
        public int? SalesGroupID { get; set; }

        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroup { get; set; }

        // Associated Objects
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesOrderPurchaseOrder>? AssociatedSalesOrders { get => associatedSalesOrders; set => associatedSalesOrders = value; }
    }
}
