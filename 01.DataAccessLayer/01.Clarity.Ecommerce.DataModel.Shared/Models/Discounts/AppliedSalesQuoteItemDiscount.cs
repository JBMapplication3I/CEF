// <copyright file="AppliedSalesQuoteItemDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied quote item discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedSalesQuoteItemDiscount
        : IAppliedDiscountBase<SalesQuoteItem, AppliedSalesQuoteItemDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "SalesQuoteItemDiscounts")]
    public class AppliedSalesQuoteItemDiscount
        : AppliedDiscountBase<SalesQuoteItem, AppliedSalesQuoteItemDiscount>, IAppliedSalesQuoteItemDiscount
    {
    }
}
