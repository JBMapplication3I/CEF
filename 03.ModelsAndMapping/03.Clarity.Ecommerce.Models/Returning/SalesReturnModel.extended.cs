// <copyright file="SalesReturnModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the sales order.</summary>
    /// <seealso cref="SalesCollectionBaseModel{ITypeModel, TypeModel, ISalesReturnFileModel, SalesReturnFileModel, ISalesReturnContactModel, SalesReturnContactModel, ISalesReturnEventModel, SalesReturnEventModel, IAppliedSalesReturnDiscountModel, AppliedSalesReturnDiscountModel, IAppliedSalesReturnItemDiscountModel, AppliedSalesReturnItemDiscountModel}"/>
    /// <seealso cref="ISalesReturnModel"/>
    public partial class SalesReturnModel
    {
        #region SalesReturn Properties
        /// <inheritdoc/>
        public string? TrackingNumber { get; set; }

        /// <inheritdoc/>
        public string? TaxTransactionID { get; set; }

        /// <inheritdoc/>
        public string? RefundTransactionID { get; set; }

        /// <inheritdoc/>
        public string? PurchaseOrderNumber { get; set; }

        /// <inheritdoc/>
        public DateTime? ActualShipDate { get; set; }

        /// <inheritdoc/>
        public DateTime? ReturnApprovedDate { get; set; }

        /// <inheritdoc/>
        public DateTime? ReturnCommitmentDate { get; set; }

        /// <inheritdoc/>
        public DateTime? RequestedShipDate { get; set; }

        /// <inheritdoc/>
        public DateTime? RequiredShipDate { get; set; }

        /// <inheritdoc/>
        public decimal? RefundAmount { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? SalesGroupID { get; set; }

        /// <inheritdoc/>
        public string? SalesGroupKey { get; set; }

        /// <inheritdoc cref="ISalesReturnModel.SalesGroup"/>
        public SalesGroupModel? SalesGroup { get; set; }

        /// <inheritdoc/>
        ISalesGroupModel? ISalesReturnModel.SalesGroup { get => SalesGroup; set => SalesGroup = (SalesGroupModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        public List<int>? SalesOrderIDs { get; set; }

        /// <inheritdoc cref="ISalesReturnModel.AssociatedSalesOrders"/>
        public List<SalesReturnSalesOrderModel>? AssociatedSalesOrders { get; set; }

        /// <inheritdoc/>
        List<ISalesReturnSalesOrderModel>? ISalesReturnModel.AssociatedSalesOrders { get => AssociatedSalesOrders?.ToList<ISalesReturnSalesOrderModel>(); set => AssociatedSalesOrders = value?.Cast<SalesReturnSalesOrderModel>().ToList(); }

        /// <inheritdoc cref="ISalesReturnModel.SalesReturnPayments"/>
        public List<SalesReturnPaymentModel>? SalesReturnPayments { get; set; }

        /// <inheritdoc/>
        List<ISalesReturnPaymentModel>? ISalesReturnModel.SalesReturnPayments { get => SalesReturnPayments?.ToList<ISalesReturnPaymentModel>(); set => SalesReturnPayments = value?.Cast<SalesReturnPaymentModel>().ToList(); }
        #endregion
    }
}
