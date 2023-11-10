// <copyright file="SalesInvoiceModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the sales invoice.</summary>
    /// <seealso cref="SalesCollectionBaseModel"/>
    /// <seealso cref="ISalesInvoiceModel"/>
    public partial class SalesInvoiceModel
    {
        #region Related Objects
        /// <inheritdoc/>
        public int? SalesGroupID { get; set; }

        /// <inheritdoc/>
        public string? SalesGroupKey { get; set; }

        /// <inheritdoc cref="ISalesInvoiceModel.SalesGroup"/>
        public SalesGroupModel? SalesGroup { get; set; }

        /// <inheritdoc/>
        ISalesGroupModel? ISalesInvoiceModel.SalesGroup { get => SalesGroup; set => SalesGroup = (SalesGroupModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="ISalesInvoiceModel.AssociatedSalesOrders"/>
        public List<SalesOrderSalesInvoiceModel>? AssociatedSalesOrders { get; set; }

        /// <inheritdoc/>
        List<ISalesOrderSalesInvoiceModel>? ISalesInvoiceModel.AssociatedSalesOrders { get => AssociatedSalesOrders?.ToList<ISalesOrderSalesInvoiceModel>(); set => AssociatedSalesOrders = value?.Cast<SalesOrderSalesInvoiceModel>().ToList(); }

        /// <inheritdoc cref="ISalesInvoiceModel.SalesInvoicePayments"/>
        public List<SalesInvoicePaymentModel>? SalesInvoicePayments { get; set; }

        /// <inheritdoc/>
        List<ISalesInvoicePaymentModel>? ISalesInvoiceModel.SalesInvoicePayments { get => SalesInvoicePayments?.ToList<ISalesInvoicePaymentModel>(); set => SalesInvoicePayments = value?.Cast<SalesInvoicePaymentModel>().ToList(); }
        #endregion
    }
}
