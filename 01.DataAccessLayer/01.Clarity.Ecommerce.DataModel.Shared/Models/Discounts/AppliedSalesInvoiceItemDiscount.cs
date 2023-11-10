// <copyright file="AppliedSalesInvoiceItemDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied sales invoice item discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedSalesInvoiceItemDiscount
        : IAppliedDiscountBase<SalesInvoiceItem, AppliedSalesInvoiceItemDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "SalesInvoiceItemDiscounts")]
    public class AppliedSalesInvoiceItemDiscount
        : AppliedDiscountBase<SalesInvoiceItem, AppliedSalesInvoiceItemDiscount>, IAppliedSalesInvoiceItemDiscount
    {
    }
}
