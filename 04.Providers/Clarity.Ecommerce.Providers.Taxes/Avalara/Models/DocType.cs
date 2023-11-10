// <copyright file="DocType.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the document type class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    /// <summary>Values that represent Document types.</summary>
    public enum DocType
    {
        /// <summary>An enum constant representing the sales order option.</summary>
        SalesOrder,

        /// <summary>An enum constant representing the sales invoice option.</summary>
        SalesInvoice,

        /// <summary>An enum constant representing the return order option.</summary>
        ReturnOrder,

        /// <summary>An enum constant representing the return invoice option.</summary>
        ReturnInvoice,

        /// <summary>An enum constant representing the purchase order option.</summary>
        PurchaseOrder,

        /// <summary>An enum constant representing the purchase invoice option.</summary>
        PurchaseInvoice,
    }
}
