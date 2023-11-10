// <copyright file="ISalesReturnModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesReturnModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for sales return model.</summary>
    public partial interface ISalesReturnModel
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

        /// <summary>Gets or sets the identifier of the refund transaction.</summary>
        /// <value>The identifier of the refund transaction.</value>
        string? RefundTransactionID { get; set; }

        /// <summary>Gets or sets the actual ship date.</summary>
        /// <value>The actual ship date.</value>
        DateTime? ActualShipDate { get; set; }

        /// <summary>Gets or sets the return approved date.</summary>
        /// <value>The return approved date.</value>
        DateTime? ReturnApprovedDate { get; set; }

        /// <summary>Gets or sets the return commitment date.</summary>
        /// <value>The return commitment date.</value>
        DateTime? ReturnCommitmentDate { get; set; }

        /// <summary>Gets or sets the requested ship date.</summary>
        /// <value>The requested ship date.</value>
        DateTime? RequestedShipDate { get; set; }

        /// <summary>Gets or sets the required ship date.</summary>
        /// <value>The required ship date.</value>
        DateTime? RequiredShipDate { get; set; }

        /// <summary>Gets or sets the refund amount.</summary>
        /// <value>The refund amount.</value>
        decimal? RefundAmount { get; set; }

        #region Related Objects
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
        List<ISalesReturnSalesOrderModel>? AssociatedSalesOrders { get; set; }

        /// <summary>Gets or sets the sales return payments.</summary>
        /// <value>The sales return payments.</value>
        List<ISalesReturnPaymentModel>? SalesReturnPayments { get; set; }
        #endregion
    }
}
