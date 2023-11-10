// <copyright file="AppliedSalesQuoteDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied sales quote discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedSalesQuoteDiscount
        : IAppliedDiscountBase<SalesQuote, AppliedSalesQuoteDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "SalesQuoteDiscounts")]
    public class AppliedSalesQuoteDiscount
        : AppliedDiscountBase<SalesQuote, AppliedSalesQuoteDiscount>, IAppliedSalesQuoteDiscount
    {
    }
}
