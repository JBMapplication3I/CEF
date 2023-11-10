// <copyright file="CartItem.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart item class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ICartItem
        : ISalesItemBase<CartItem, AppliedCartItemDiscount, CartItemTarget>
    {
        #region CartItem Properties
        /// <summary>Gets or sets the unit sold price modifier.</summary>
        /// <value>The unit sold price modifier.</value>
        decimal? UnitSoldPriceModifier { get; set; }

        /// <summary>Gets or sets the unit sold price modifier mode.</summary>
        /// <value>The unit sold price modifier mode.</value>
        int? UnitSoldPriceModifierMode { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using Interfaces.DataModel;

    [SqlSchema("Shopping", "CartItem")]
    public class CartItem
        : SalesItemBase<Cart, CartItem, AppliedCartItemDiscount, CartItemTarget>,
            ICartItem
    {
        #region CartItem Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? UnitSoldPriceModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? UnitSoldPriceModifierMode { get; set; }
        #endregion
    }
}
