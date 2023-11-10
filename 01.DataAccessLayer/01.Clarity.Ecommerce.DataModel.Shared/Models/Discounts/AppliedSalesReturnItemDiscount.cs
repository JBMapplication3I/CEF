// <copyright file="AppliedSalesReturnItemDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied sales order item discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedSalesReturnItemDiscount
        : IAppliedDiscountBase<SalesReturnItem, AppliedSalesReturnItemDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "SalesReturnItemDiscounts")]
    public class AppliedSalesReturnItemDiscount
        : AppliedDiscountBase<SalesReturnItem, AppliedSalesReturnItemDiscount>, IAppliedSalesReturnItemDiscount
    {
    }
}
