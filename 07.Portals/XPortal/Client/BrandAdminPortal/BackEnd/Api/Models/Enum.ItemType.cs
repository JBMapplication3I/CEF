// <copyright file="ItemType.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the item type class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    /// <summary>Values that represent item types.</summary>
    public enum ItemType
    {
        /// <summary>An enum constant representing the item option.</summary>
        Item,

        /// <summary>An enum constant representing the order discount option.</summary>
        OrderDiscount,

        /// <summary>An enum constant representing the product discount option.</summary>
        ProductDiscount,

        /// <summary>An enum constant representing the shipping discount option.</summary>
        ShippingDiscount,
    }
}
