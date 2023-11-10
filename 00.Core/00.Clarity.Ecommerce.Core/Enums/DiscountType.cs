// <copyright file="DiscountType.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount type class</summary>
namespace Clarity.Ecommerce.Enums
{
    /// <summary>Values that represent discount types.</summary>
    public enum DiscountType
    {
        /// <summary>An enum constant representing the order option.</summary>
        Order,

        /// <summary>An enum constant representing the product option.</summary>
        Product,

        /// <summary>An enum constant representing the shipping option.</summary>
        Shipping,

        /// <summary>An enum constant representing the buy X coordinate get Y coordinate option.</summary>
        BuyXGetY,
    }
}
