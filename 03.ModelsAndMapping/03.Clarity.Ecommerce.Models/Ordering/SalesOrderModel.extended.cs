// <copyright file="SalesOrderModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order model class</summary>
// ReSharper disable MemberCanBePrivate.Global
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the sales order.</summary>
    /// <seealso cref="SalesCollectionBaseModel{ITypeModel, TypeModel, ISalesOrderFileModel, SalesOrderFileModel, ISalesOrderContactModel, SalesOrderContactModel, ISalesOrderEventModel, SalesOrderEventModel, IAppliedSalesOrderDiscountModel, AppliedSalesOrderDiscountModel, IAppliedSalesOrderItemDiscountModel, AppliedSalesOrderItemDiscountModel}"/>
    /// <seealso cref="ISalesOrderModel"/>
    public partial class SalesOrderModel
    {
        #region SalesOrder Properties
        /// <inheritdoc/>
        public string? TrackingNumber { get; set; }

        /// <inheritdoc/>
        public string? TaxTransactionID { get; set; }

        /// <inheritdoc/>
        public string? PaymentTransactionID { get; set; }

        /// <inheritdoc/>
        public string? PurchaseOrderNumber { get; set; }

        /// <inheritdoc/>
        public DateTime? ActualShipDate { get; set; }

        /// <inheritdoc/>
        public DateTime? OrderApprovedDate { get; set; }

        /// <inheritdoc/>
        public DateTime? OrderCommitmentDate { get; set; }

        /// <inheritdoc/>
        public DateTime? RequestedShipDate { get; set; }

        /// <inheritdoc/>
        public DateTime? RequiredShipDate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? SalesGroupAsMasterID { get; set; }

        /// <inheritdoc/>
        public string? SalesGroupAsMasterKey { get; set; }

        /// <inheritdoc cref="ISalesOrderModel.SalesGroupAsMaster"/>
        public SalesGroupModel? SalesGroupAsMaster { get; set; }

        /// <inheritdoc/>
        ISalesGroupModel? ISalesOrderModel.SalesGroupAsMaster { get => SalesGroupAsMaster; set => SalesGroupAsMaster = (SalesGroupModel?)value; }

        /// <inheritdoc/>
        public int? SalesGroupAsSubID { get; set; }

        /// <inheritdoc/>
        public string? SalesGroupAsSubKey { get; set; }

        /// <inheritdoc cref="ISalesOrderModel.SalesGroupAsSub"/>
        public SalesGroupModel? SalesGroupAsSub { get; set; }

        /// <inheritdoc/>
        ISalesGroupModel? ISalesOrderModel.SalesGroupAsSub { get => SalesGroupAsSub; set => SalesGroupAsSub = (SalesGroupModel?)value; }

        /// <inheritdoc/>
        public int? InventoryLocationID { get; set; }

        /// <inheritdoc/>
        public string? InventoryLocationKey { get; set; }

        /// <inheritdoc/>
        public string? InventoryLocationName { get; set; }

        /// <inheritdoc cref="ISalesOrderModel.InventoryLocation"/>
        public InventoryLocationModel? InventoryLocation { get; set; }

        /// <inheritdoc/>
        IInventoryLocationModel? ISalesOrderModel.InventoryLocation { get => InventoryLocation; set => InventoryLocation = (InventoryLocationModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="ISalesOrderModel.AssociatedPurchaseOrders"/>
        public List<SalesOrderPurchaseOrderModel>? AssociatedPurchaseOrders { get; set; }

        /// <inheritdoc/>
        List<ISalesOrderPurchaseOrderModel>? ISalesOrderModel.AssociatedPurchaseOrders { get => AssociatedPurchaseOrders?.ToList<ISalesOrderPurchaseOrderModel>(); set => AssociatedPurchaseOrders = value?.Cast<SalesOrderPurchaseOrderModel>().ToList(); }

        /// <inheritdoc cref="ISalesOrderModel.AssociatedSalesInvoices"/>
        public List<SalesOrderSalesInvoiceModel>? AssociatedSalesInvoices { get; set; }

        /// <inheritdoc/>
        List<ISalesOrderSalesInvoiceModel>? ISalesOrderModel.AssociatedSalesInvoices { get => AssociatedSalesInvoices?.ToList<ISalesOrderSalesInvoiceModel>(); set => AssociatedSalesInvoices = value?.Cast<SalesOrderSalesInvoiceModel>().ToList(); }

        /// <inheritdoc cref="ISalesOrderModel.AssociatedSalesQuotes"/>
        public List<SalesQuoteSalesOrderModel>? AssociatedSalesQuotes { get; set; }

        /// <inheritdoc/>
        List<ISalesQuoteSalesOrderModel>? ISalesOrderModel.AssociatedSalesQuotes { get => AssociatedSalesQuotes?.ToList<ISalesQuoteSalesOrderModel>(); set => AssociatedSalesQuotes = value?.Cast<SalesQuoteSalesOrderModel>().ToList(); }

        /// <inheritdoc cref="ISalesOrderModel.AssociatedSalesReturns"/>
        public List<SalesReturnSalesOrderModel>? AssociatedSalesReturns { get; set; }

        /// <inheritdoc/>
        List<ISalesReturnSalesOrderModel>? ISalesOrderModel.AssociatedSalesReturns { get => AssociatedSalesReturns?.ToList<ISalesReturnSalesOrderModel>(); set => AssociatedSalesReturns = value?.Cast<SalesReturnSalesOrderModel>().ToList(); }

        /// <inheritdoc cref="ISalesOrderModel.SalesOrderPayments"/>
        public List<SalesOrderPaymentModel>? SalesOrderPayments { get; set; }

        /// <inheritdoc/>
        List<ISalesOrderPaymentModel>? ISalesOrderModel.SalesOrderPayments { get => SalesOrderPayments?.ToList<ISalesOrderPaymentModel>(); set => SalesOrderPayments = value?.Cast<SalesOrderPaymentModel>().ToList(); }
        #endregion
    }
}
