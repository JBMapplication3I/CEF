// <copyright file="AppliedPurchaseOrderDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied purchase order discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedPurchaseOrderDiscount
        : IAppliedDiscountBase<PurchaseOrder, AppliedPurchaseOrderDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "PurchaseOrderDiscounts")]
    public class AppliedPurchaseOrderDiscount
        : AppliedDiscountBase<PurchaseOrder, AppliedPurchaseOrderDiscount>, IAppliedPurchaseOrderDiscount
    {
    }
}
