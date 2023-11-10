// <copyright file="CartValidator.MaxCartItems.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart validator. maximum cart items class</summary>
namespace Clarity.Ecommerce.Providers.CartValidation
{
    using System.Linq;
    using Interfaces.Models;
    using JSConfigs;

    public partial class CartValidator
    {
        /// <summary>Validates the number of items is less than maximum described by cart.</summary>
        /// <param name="cart">The cart.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        // ReSharper disable once UnusedMember.Local
        private static bool ValidateNumberOfItemsIsLessThanMax(ICartModel cart)
        {
            return cart.SalesItems!.Sum(item => item.Quantity) <= CEFConfigDictionary.CartValidationMaximumCartItems;
        }
    }
}
