// <copyright file="SalesInvoiceItem.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice item class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesInvoiceItem
        : ISalesItemBase<SalesInvoiceItem, AppliedSalesInvoiceItemDiscount, SalesInvoiceItemTarget>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Invoicing", "SalesInvoiceItem")]
    public class SalesInvoiceItem
        : SalesItemBase<SalesInvoice, SalesInvoiceItem, AppliedSalesInvoiceItemDiscount, SalesInvoiceItemTarget>,
            ISalesInvoiceItem
    {
    }
}
