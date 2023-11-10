// <copyright file="ISalesInvoiceModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesInvoiceModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for sales invoice model.</summary>
    public partial interface ISalesInvoiceModel
    {
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
        /// <summary>Gets or sets the associated sales orders.</summary>
        /// <value>The associated sales orders.</value>
        List<ISalesOrderSalesInvoiceModel>? AssociatedSalesOrders { get; set; }

        /// <summary>Gets or sets the sales invoice payments.</summary>
        /// <value>The sales invoice payments.</value>
        List<ISalesInvoicePaymentModel>? SalesInvoicePayments { get; set; }
        #endregion
    }
}
