// <copyright file="DocTypeExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the document type extensions class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    /// <summary>A document type extensions.</summary>
    public static class DocTypeExtensions
    {
        /// <summary>A TaxEntityType extension method that converts this DocTypeExtensions to an avalara document type.</summary>
        /// <param name="type">     The type to act on.</param>
        /// <param name="isInvoice">True if this DocTypeExtensions is invoice.</param>
        /// <returns>The given data converted to a DocType.</returns>
        public static DocType ToAvalaraDocType(this Enums.TaxEntityType type, bool isInvoice = false)
        {
            return type switch
            {
                Enums.TaxEntityType.Cart => isInvoice ? DocType.SalesInvoice : DocType.SalesOrder,
                Enums.TaxEntityType.SalesOrder => isInvoice ? DocType.SalesInvoice : DocType.SalesOrder,
                Enums.TaxEntityType.PurchaseOrder => isInvoice ? DocType.PurchaseInvoice : DocType.PurchaseOrder,
                Enums.TaxEntityType.Return => isInvoice ? DocType.ReturnInvoice : DocType.ReturnOrder,
                _ => DocType.SalesOrder,
            };
        }
    }
}
