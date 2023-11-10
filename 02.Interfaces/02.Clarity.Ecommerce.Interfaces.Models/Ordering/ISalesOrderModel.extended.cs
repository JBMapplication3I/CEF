// <copyright file="ISalesOrderModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesOrderModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for sales order model.</summary>
    public partial interface ISalesOrderModel
    {
        /// <summary>Gets or sets the tracking number.</summary>
        /// <value>The tracking number.</value>
        string? TrackingNumber { get; set; }

        /// <summary>Gets or sets the purchase order number.</summary>
        /// <value>The purchase order number.</value>
        string? PurchaseOrderNumber { get; set; }

        /// <summary>Gets or sets the identifier of the tax transaction.</summary>
        /// <value>The identifier of the tax transaction.</value>
        string? TaxTransactionID { get; set; }

        /// <summary>Gets or sets the identifier of the payment transaction.</summary>
        /// <value>The identifier of the payment transaction.</value>
        string? PaymentTransactionID { get; set; }

        /// <summary>Gets or sets the actual ship date.</summary>
        /// <value>The actual ship date.</value>
        DateTime? ActualShipDate { get; set; }

        /// <summary>Gets or sets the order approved date.</summary>
        /// <value>The order approved date.</value>
        DateTime? OrderApprovedDate { get; set; }

        /// <summary>Gets or sets the order commitment date.</summary>
        /// <value>The order commitment date.</value>
        DateTime? OrderCommitmentDate { get; set; }

        /// <summary>Gets or sets the requested ship date.</summary>
        /// <value>The requested ship date.</value>
        DateTime? RequestedShipDate { get; set; }

        /// <summary>Gets or sets the required ship date.</summary>
        /// <value>The required ship date.</value>
        DateTime? RequiredShipDate { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the sales group as master.</summary>
        /// <value>The identifier of the sales group as master.</value>
        int? SalesGroupAsMasterID { get; set; }

        /// <summary>Gets or sets the sales group as master key.</summary>
        /// <value>The sales group as master key.</value>
        string? SalesGroupAsMasterKey { get; set; }

        /// <summary>Gets or sets the sales group as master.</summary>
        /// <value>The sales group as master.</value>
        ISalesGroupModel? SalesGroupAsMaster { get; set; }

        /// <summary>Gets or sets the identifier of the sales group as sub.</summary>
        /// <value>The identifier of the sales group as sub.</value>
        int? SalesGroupAsSubID { get; set; }

        /// <summary>Gets or sets the sales group as sub key.</summary>
        /// <value>The sales group as sub key.</value>
        string? SalesGroupAsSubKey { get; set; }

        /// <summary>Gets or sets the sales group as sub.</summary>
        /// <value>The sales group as sub.</value>
        ISalesGroupModel? SalesGroupAsSub { get; set; }

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
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the associated purchase orders.</summary>
        /// <value>The associated purchase orders.</value>
        List<ISalesOrderPurchaseOrderModel>? AssociatedPurchaseOrders { get; set; }

        /// <summary>Gets or sets the associated sales invoices.</summary>
        /// <value>The associated sales invoices.</value>
        List<ISalesOrderSalesInvoiceModel>? AssociatedSalesInvoices { get; set; }

        /// <summary>Gets or sets the associated sales quotes.</summary>
        /// <value>The associated sales quotes.</value>
        List<ISalesQuoteSalesOrderModel>? AssociatedSalesQuotes { get; set; }

        /// <summary>Gets or sets the associated sales returns.</summary>
        /// <value>The associated sales returns.</value>
        List<ISalesReturnSalesOrderModel>? AssociatedSalesReturns { get; set; }

        /// <summary>Gets or sets the sales order payments.</summary>
        /// <value>The sales order payments.</value>
        List<ISalesOrderPaymentModel>? SalesOrderPayments { get; set; }
        #endregion
    }
}
