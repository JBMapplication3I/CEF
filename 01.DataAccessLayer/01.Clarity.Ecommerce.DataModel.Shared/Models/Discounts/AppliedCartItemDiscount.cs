// <copyright file="AppliedCartItemDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied cart item discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedCartItemDiscount
        : IAppliedDiscountBase<CartItem, AppliedCartItemDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "CartItemDiscounts")]
    public class AppliedCartItemDiscount
        : AppliedDiscountBase<CartItem, AppliedCartItemDiscount>, IAppliedCartItemDiscount
    {
    }
}
