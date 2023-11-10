// <copyright file="SalesGroupModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales group model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the sales group.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="ISalesGroupModel"/>
    public partial class SalesGroupModel
    {
        // NOTE: These properties are listed in order of the expected standard workflow

        /// <inheritdoc/>
        public int? BillingContactID { get; set; }

        /// <inheritdoc/>
        public string? BillingContactKey { get; set; }

        /// <inheritdoc cref="ISalesGroupModel.BillingContact"/>
        public ContactModel? BillingContact { get; set; }

        /// <inheritdoc/>
        IContactModel? ISalesGroupModel.BillingContact { get => BillingContact; set => BillingContact = (ContactModel?)value; }

        /// <inheritdoc cref="ISalesGroupModel.SalesQuoteRequestMasters"/>
        public List<SalesQuoteModel>? SalesQuoteRequestMasters { get; set; }

        /// <inheritdoc/>
        List<ISalesQuoteModel>? ISalesGroupModel.SalesQuoteRequestMasters { get => SalesQuoteRequestMasters?.ToList<ISalesQuoteModel>(); set => SalesQuoteRequestMasters = value?.Cast<SalesQuoteModel>().ToList(); }

        /// <inheritdoc cref="ISalesGroupModel.SalesQuoteRequestSubs"/>
        public List<SalesQuoteModel>? SalesQuoteRequestSubs { get; set; }

        /// <inheritdoc/>
        List<ISalesQuoteModel>? ISalesGroupModel.SalesQuoteRequestSubs { get => SalesQuoteRequestSubs?.ToList<ISalesQuoteModel>(); set => SalesQuoteRequestSubs = value?.Cast<SalesQuoteModel>().ToList(); }

        /// <inheritdoc cref="ISalesGroupModel.SalesQuoteResponseMasters"/>
        public List<SalesQuoteModel>? SalesQuoteResponseMasters { get; set; }

        /// <inheritdoc/>
        List<ISalesQuoteModel>? ISalesGroupModel.SalesQuoteResponseMasters { get => SalesQuoteResponseMasters?.ToList<ISalesQuoteModel>(); set => SalesQuoteResponseMasters = value?.Cast<SalesQuoteModel>().ToList(); }

        /// <inheritdoc cref="ISalesGroupModel.SalesQuoteResponseSubs"/>
        public List<SalesQuoteModel>? SalesQuoteResponseSubs { get; set; }

        /// <inheritdoc/>
        List<ISalesQuoteModel>? ISalesGroupModel.SalesQuoteResponseSubs { get => SalesQuoteResponseSubs?.ToList<ISalesQuoteModel>(); set => SalesQuoteResponseSubs = value?.Cast<SalesQuoteModel>().ToList(); }

        /// <inheritdoc cref="ISalesGroupModel.SalesOrderMasters"/>
        public List<SalesOrderModel>? SalesOrderMasters { get; set; }

        /// <inheritdoc/>
        List<ISalesOrderModel>? ISalesGroupModel.SalesOrderMasters { get => SalesOrderMasters?.ToList<ISalesOrderModel>(); set => SalesOrderMasters = value?.Cast<SalesOrderModel>().ToList(); }

        /// <inheritdoc cref="ISalesGroupModel.SubSalesOrders"/>
        public List<SalesOrderModel>? SubSalesOrders { get; set; }

        /// <inheritdoc/>
        List<ISalesOrderModel>? ISalesGroupModel.SubSalesOrders { get => SubSalesOrders?.ToList<ISalesOrderModel>(); set => SubSalesOrders = value?.Cast<SalesOrderModel>().ToList(); }

        /// <inheritdoc cref="ISalesGroupModel.PurchaseOrders"/>
        public List<PurchaseOrderModel>? PurchaseOrders { get; set; }

        /// <inheritdoc/>
        List<IPurchaseOrderModel>? ISalesGroupModel.PurchaseOrders { get => PurchaseOrders?.ToList<IPurchaseOrderModel>(); set => PurchaseOrders = value?.Cast<PurchaseOrderModel>().ToList(); }

        /// <inheritdoc cref="ISalesGroupModel.SalesInvoices"/>
        public List<SalesInvoiceModel>? SalesInvoices { get; set; }

        /// <inheritdoc/>
        List<ISalesInvoiceModel>? ISalesGroupModel.SalesInvoices { get => SalesInvoices?.ToList<ISalesInvoiceModel>(); set => SalesInvoices = value?.Cast<SalesInvoiceModel>().ToList(); }

        /// <inheritdoc cref="ISalesGroupModel.SalesReturns"/>
        public List<SalesReturnModel>? SalesReturns { get; set; }

        /// <inheritdoc/>
        List<ISalesReturnModel>? ISalesGroupModel.SalesReturns { get => SalesReturns?.ToList<ISalesReturnModel>(); set => SalesReturns = value?.Cast<SalesReturnModel>().ToList(); }

        /// <inheritdoc cref="ISalesGroupModel.SampleRequests"/>
        public List<SampleRequestModel>? SampleRequests { get; set; }

        /// <inheritdoc/>
        List<ISampleRequestModel>? ISalesGroupModel.SampleRequests { get => SampleRequests?.ToList<ISampleRequestModel>(); set => SampleRequests = value?.Cast<SampleRequestModel>().ToList(); }
    }
}
