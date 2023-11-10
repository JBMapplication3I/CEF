// <copyright file="CancelTaxRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cancel tax request class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;

    /// <summary>(Serializable)a cancel tax request.</summary>
    [Serializable]
    public class CancelTaxRequest
    {
        /// <summary>Required for CancelTax operation.</summary>
        /// <value>The cancel code.</value>
        public CancelCode CancelCode { get; set; }

        /// <summary>Gets or sets the type of the document.</summary>
        /// <value>The type of the document.</value>
        public DocType DocType { get; set; } // Note that the only *meaningful* values for this property here are SalesInvoice, ReturnInvoice, PurchaseInvoice.

        /// <summary>The document needs to be identified by either DocCode/CompanyCode (recommended) OR DocId (not
        /// recommended).</summary>
        /// <value>The company code.</value>
        public string? CompanyCode { get; set; }

        /// <summary>Gets or sets the document code.</summary>
        /// <value>The document code.</value>
        public string? DocCode { get; set; }
    }
}
