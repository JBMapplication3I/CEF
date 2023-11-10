// <copyright file="AppliedSalesInvoiceDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied sales invoice discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedSalesInvoiceDiscount
        : IAppliedDiscountBase<SalesInvoice, AppliedSalesInvoiceDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "SalesInvoiceDiscounts")]
    public class AppliedSalesInvoiceDiscount
        : AppliedDiscountBase<SalesInvoice, AppliedSalesInvoiceDiscount>, IAppliedSalesInvoiceDiscount
    {
    }
}
