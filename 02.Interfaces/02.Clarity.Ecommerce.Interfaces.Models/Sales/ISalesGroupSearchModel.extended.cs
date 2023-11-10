// <copyright file="ISalesGroupSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesGroupSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for sales group search model.</summary>
    public partial interface ISalesGroupSearchModel
    {
        /// <summary>Gets or sets a search value for the attached account.</summary>
        /// <value>The search value for the attached account.</value>
        string? AccountIDOrCustomKeyOrName { get; set; }

        /// <summary>Gets or sets the billing contact key.</summary>
        /// <value>The billing contact key.</value>
        string? BillingContactKey { get; set; }

        /// <summary>Gets or sets the identifier of the sales quote.</summary>
        /// <value>The identifier of the sales quote.</value>
        int? SalesQuoteID { get; set; }

        /// <summary>Gets or sets the sales quote key.</summary>
        /// <value>The sales quote key.</value>
        string? SalesQuoteKey { get; set; }

        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        int? SalesOrderID { get; set; }

        /// <summary>Gets or sets the sales order key.</summary>
        /// <value>The sales order key.</value>
        string? SalesOrderKey { get; set; }

        /// <summary>Gets or sets the identifier of the purchase order.</summary>
        /// <value>The identifier of the purchase order.</value>
        int? PurchaseOrderID { get; set; }

        /// <summary>Gets or sets the purchase order key.</summary>
        /// <value>The purchase order key.</value>
        string? PurchaseOrderKey { get; set; }

        /// <summary>Gets or sets the identifier of the sales invoice.</summary>
        /// <value>The identifier of the sales invoice.</value>
        int? SalesInvoiceID { get; set; }

        /// <summary>Gets or sets the sales invoice key.</summary>
        /// <value>The sales invoice key.</value>
        string? SalesInvoiceKey { get; set; }

        /// <summary>Gets or sets the identifier of the sales return.</summary>
        /// <value>The identifier of the sales return.</value>
        int? SalesReturnID { get; set; }

        /// <summary>Gets or sets the sales return key.</summary>
        /// <value>The sales return key.</value>
        string? SalesReturnKey { get; set; }

        /// <summary>Gets or sets the identifier of the sample request.</summary>
        /// <value>The identifier of the sample request.</value>
        int? SampleRequestID { get; set; }

        /// <summary>Gets or sets the sample request key.</summary>
        /// <value>The sample request key.</value>
        string? SampleRequestKey { get; set; }

        /// <summary>Gets or sets the minimum date.</summary>
        /// <value>The minimum date.</value>
        DateTime? MinDate { get; set; }

        /// <summary>Gets or sets the maximum date.</summary>
        /// <value>The maximum date.</value>
        DateTime? MaxDate { get; set; }
    }
}
