// <copyright file="ISalesGroupModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesGroupModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    public partial interface ISalesGroupModel
    {
        // NOTE: These properties are listed in order of the expected standard workflow

        /// <summary>Gets or sets the billing contact for this group.</summary>
        /// <value>The identifier of the billing contact.</value>
        int? BillingContactID { get; set; }

        /// <summary>Gets or sets the billing contact key.</summary>
        /// <value>The billing contact key.</value>
        string? BillingContactKey { get; set; }

        /// <summary>Gets or sets the billing contact for this group.</summary>
        /// <value>The billing contact.</value>
        IContactModel? BillingContact { get; set; }

        /// <summary>Gets or sets the sales quote masters.</summary>
        /// <remarks>There should only ever be 1.</remarks>
        /// <value>The sales quote masters.</value>
        List<ISalesQuoteModel>? SalesQuoteRequestMasters { get; set; }

        /// <summary>Gets or sets the sales quote request subs.</summary>
        /// <value>The sales quote request subs.</value>
        List<ISalesQuoteModel>? SalesQuoteRequestSubs { get; set; }

        /// <summary>Gets or sets responses by individual sellers and how they would/could fulfill the quote if made into
        /// one or more orders.</summary>
        /// <value>The sales quote responses.</value>
        List<ISalesQuoteModel>? SalesQuoteResponseMasters { get; set; }

        /// <summary>Gets or sets the sales quote response subs.</summary>
        /// <value>The sales quote response subs.</value>
        List<ISalesQuoteModel>? SalesQuoteResponseSubs { get; set; }

        /// <summary>Gets or sets the sales order masters.</summary>
        /// <remarks>There should only ever be 1.</remarks>
        /// <value>The sales order masters.</value>
        List<ISalesOrderModel>? SalesOrderMasters { get; set; }

        /// <summary>Gets or sets the sub orders which go to individual sellers and/or shipping destinations, etc.
        /// according to Split Order rules.</summary>
        /// <value>The sub sales orders.</value>
        List<ISalesOrderModel>? SubSalesOrders { get; set; }

        /// <summary>Gets or sets any Purchase Orders from this company to it's suppliers in order to increase stock to
        /// cover the requested quantities of items ordered. Includes Drop-Ship orders that would be sent directly to
        /// the customer's shipping destination(s).</summary>
        /// <value>The purchase orders.</value>
        List<IPurchaseOrderModel>? PurchaseOrders { get; set; }

        /// <summary>Gets or sets the initial and any follow-on invoices (such as NET30 monthly payments) that are issued
        /// for this group.</summary>
        /// <value>The sales invoices.</value>
        List<ISalesInvoiceModel>? SalesInvoices { get; set; }

        /// <summary>Gets or sets any RMAs requested by the customer for orders issued in this group, according to Split
        /// Return rules.</summary>
        /// <value>The sales returns.</value>
        List<ISalesReturnModel>? SalesReturns { get; set; }

        /// <summary>Gets or sets the sample requests.</summary>
        /// <value>The sample requests.</value>
        List<ISampleRequestModel>? SampleRequests { get; set; }
    }
}
