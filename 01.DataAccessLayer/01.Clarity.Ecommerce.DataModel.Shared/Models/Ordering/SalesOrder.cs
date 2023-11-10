// <copyright file="SalesOrder.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ISalesOrder
        : ISalesCollectionBase<SalesOrder,
                SalesOrderStatus,
                SalesOrderType,
                SalesOrderItem,
                AppliedSalesOrderDiscount,
                SalesOrderState,
                SalesOrderFile,
                SalesOrderContact,
                SalesOrderEvent,
                SalesOrderEventType>,
            IHaveNotesBase
    {
        #region Sales Order Properties
        /// <summary>Gets or sets the purchase order number.</summary>
        /// <value>The purchase order number.</value>
        string? PurchaseOrderNumber { get; set; }

        /// <summary>Gets or sets the balance due.</summary>
        /// <value>The balance due.</value>
        decimal? BalanceDue { get; set; }

        /// <summary>Gets or sets the tracking number.</summary>
        /// <value>The tracking number.</value>
        string? TrackingNumber { get; set; }

        /// <summary>Gets or sets the identifier of the payment transaction.</summary>
        /// <value>The identifier of the payment transaction.</value>
        string? PaymentTransactionID { get; set; }

        /// <summary>Gets or sets the identifier of the tax transaction.</summary>
        /// <value>The identifier of the tax transaction.</value>
        string? TaxTransactionID { get; set; }

        /// <summary>Gets or sets the order approved date.</summary>
        /// <value>The order approved date.</value>
        DateTime? OrderApprovedDate { get; set; }

        /// <summary>Gets or sets the order commitment date.</summary>
        /// <value>The order commitment date.</value>
        DateTime? OrderCommitmentDate { get; set; }

        /// <summary>Gets or sets the required ship date.</summary>
        /// <value>The required ship date.</value>
        DateTime? RequiredShipDate { get; set; }

        /// <summary>Gets or sets the requested ship date.</summary>
        /// <value>The requested ship date.</value>
        DateTime? RequestedShipDate { get; set; }

        /// <summary>Gets or sets the actual ship date.</summary>
        /// <value>The actual ship date.</value>
        DateTime? ActualShipDate { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the sales group as master.</summary>
        /// <value>The identifier of the sales group as master.</value>
        int? SalesGroupAsMasterID { get; set; }

        /// <summary>Gets or sets the sales group as master.</summary>
        /// <value>The sales group as master.</value>
        SalesGroup? SalesGroupAsMaster { get; set; }

        /// <summary>Gets or sets the identifier of the sales group as sub.</summary>
        /// <value>The identifier of the sales group as sub.</value>
        int? SalesGroupAsSubID { get; set; }

        /// <summary>Gets or sets the sales group as sub.</summary>
        /// <value>The sales group as sub.</value>
        SalesGroup? SalesGroupAsSub { get; set; }

        /// <summary>Gets or sets the identifier of the inventory location.</summary>
        /// <value>The identifier of the inventory location.</value>
        int? InventoryLocationID { get; set; }

        /// <summary>Gets or sets the inventory location.</summary>
        /// <value>The inventory location.</value>
        InventoryLocation? InventoryLocation { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the sales order payments.</summary>
        /// <value>The sales order payments.</value>
        ICollection<SalesOrderPayment>? SalesOrderPayments { get; set; }

        /// <summary>Gets or sets the associated sales quotes.</summary>
        /// <value>The associated sales quotes.</value>
        ICollection<SalesQuoteSalesOrder>? AssociatedSalesQuotes { get; set; }

        /// <summary>Gets or sets the associated sales invoices.</summary>
        /// <value>The associated sales invoices.</value>
        ICollection<SalesOrderSalesInvoice>? AssociatedSalesInvoices { get; set; }

        /// <summary>Gets or sets the associated sales returns.</summary>
        /// <value>The associated sales returns.</value>
        ICollection<SalesReturnSalesOrder>? AssociatedSalesReturns { get; set; }

        /// <summary>Gets or sets the associated purchase orders.</summary>
        /// <value>The associated purchase orders.</value>
        ICollection<SalesOrderPurchaseOrder>? AssociatedPurchaseOrders { get; set; }
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

    [SqlSchema("Ordering", "SalesOrder")]
    public class SalesOrder
        : SalesCollectionBase<SalesOrder,
            SalesOrderStatus,
            SalesOrderType,
            SalesOrderItem,
            AppliedSalesOrderDiscount,
            SalesOrderState,
            SalesOrderFile,
            SalesOrderContact,
            SalesOrderEvent,
            SalesOrderEventType>,
        ISalesOrder
    {
        private ICollection<Note>? notes;
        private ICollection<SalesOrderPayment>? salesOrderPayments;
        private ICollection<SalesQuoteSalesOrder>? associatedSalesQuotes;
        private ICollection<SalesReturnSalesOrder>? associatedSalesReturns;
        private ICollection<SalesOrderSalesInvoice>? associatedSalesInvoices;
        private ICollection<SalesOrderPurchaseOrder>? associatedPurchaseOrders;

        public SalesOrder()
        {
            // HaveNotesBase
            notes = new HashSet<Note>();
            // SalesOrder
            salesOrderPayments = new HashSet<SalesOrderPayment>();
            associatedSalesQuotes = new HashSet<SalesQuoteSalesOrder>();
            associatedSalesReturns = new HashSet<SalesReturnSalesOrder>();
            associatedSalesInvoices = new HashSet<SalesOrderSalesInvoice>();
            associatedPurchaseOrders = new HashSet<SalesOrderPurchaseOrder>();
        }

        #region HaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region SalesOrder Properties
        /// <inheritdoc/>
        [StringLength(100)]
        public string? PurchaseOrderNumber { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4)]
        public decimal? BalanceDue { get; set; }

        /// <inheritdoc/>
        public string? TrackingNumber { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(256)]
        public string? PaymentTransactionID { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(256)]
        public string? TaxTransactionID { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime? OrderApprovedDate { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime? OrderCommitmentDate { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime? RequiredShipDate { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime? RequestedShipDate { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime? ActualShipDate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        // NOTE: Relationship handled in ModelBuilder
        public int? SalesGroupAsMasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroupAsMaster { get; set; }

        /// <inheritdoc/>
        // NOTE: Relationship handled in ModelBuilder
        public int? SalesGroupAsSubID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroupAsSub { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(InventoryLocation)), DefaultValue(null), JsonIgnore]
        public int? InventoryLocationID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual InventoryLocation? InventoryLocation { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesOrderPayment>? SalesOrderPayments { get => salesOrderPayments; set => salesOrderPayments = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesQuoteSalesOrder>? AssociatedSalesQuotes { get => associatedSalesQuotes; set => associatedSalesQuotes = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesReturnSalesOrder>? AssociatedSalesReturns { get => associatedSalesReturns; set => associatedSalesReturns = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesOrderSalesInvoice>? AssociatedSalesInvoices { get => associatedSalesInvoices; set => associatedSalesInvoices = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesOrderPurchaseOrder>? AssociatedPurchaseOrders { get => associatedPurchaseOrders; set => associatedPurchaseOrders = value; }
        #endregion
    }
}
