// <copyright file="AppliedSalesReturnDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied sales order discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedSalesReturnDiscount
        : IAppliedDiscountBase<SalesReturn, AppliedSalesReturnDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "SalesReturnDiscounts")]
    public class AppliedSalesReturnDiscount
        : AppliedDiscountBase<SalesReturn, AppliedSalesReturnDiscount>, IAppliedSalesReturnDiscount
    {
    }
}
