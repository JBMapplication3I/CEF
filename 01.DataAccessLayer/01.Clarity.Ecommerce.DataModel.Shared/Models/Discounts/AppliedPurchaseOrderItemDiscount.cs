// <copyright file="AppliedPurchaseOrderItemDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied purchase order item discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedPurchaseOrderItemDiscount
        : IAppliedDiscountBase<PurchaseOrderItem, AppliedPurchaseOrderItemDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "PurchaseOrderItemDiscounts")]
    public class AppliedPurchaseOrderItemDiscount
        : AppliedDiscountBase<PurchaseOrderItem, AppliedPurchaseOrderItemDiscount>, IAppliedPurchaseOrderItemDiscount
    {
    }
}
