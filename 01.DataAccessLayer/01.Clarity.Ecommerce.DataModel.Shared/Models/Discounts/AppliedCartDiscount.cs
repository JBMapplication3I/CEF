// <copyright file="AppliedCartDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied cart discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedCartDiscount
        : IAppliedDiscountBase<Cart, AppliedCartDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "CartDiscounts")]
    public class AppliedCartDiscount
        : AppliedDiscountBase<Cart, AppliedCartDiscount>, IAppliedCartDiscount
    {
    }
}
