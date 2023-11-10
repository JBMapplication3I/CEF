// <copyright file="ICartModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICartModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for cart model.</summary>
    public partial interface ICartModel
    {
        /// <summary>Gets a value indicating whether the nothing to ship.</summary>
        /// <value>True if nothing to ship, false if not.</value>
        bool NothingToShip { get; }

        /// <summary>Gets or sets the requested ship date.</summary>
        /// <value>The requested ship date.</value>
        DateTime? RequestedShipDate { get; set; }

        /// <summary>Gets or sets the subtotal shipping modifier.</summary>
        /// <value>The subtotal shipping modifier.</value>
        decimal? SubtotalShippingModifier { get; set; }

        /// <summary>Gets or sets the subtotal shipping modifier mode.</summary>
        /// <value>The subtotal shipping modifier mode.</value>
        int? SubtotalShippingModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal taxes modifier.</summary>
        /// <value>The subtotal taxes modifier.</value>
        decimal? SubtotalTaxesModifier { get; set; }

        /// <summary>Gets or sets the subtotal taxes modifier mode.</summary>
        /// <value>The subtotal taxes modifier mode.</value>
        int? SubtotalTaxesModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal fees modifier.</summary>
        /// <value>The subtotal fees modifier.</value>
        decimal? SubtotalFeesModifier { get; set; }

        /// <summary>Gets or sets the subtotal fees modifier mode.</summary>
        /// <value>The subtotal fees modifier mode.</value>
        int? SubtotalFeesModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal handling modifier.</summary>
        /// <value>The subtotal handling modifier.</value>
        decimal? SubtotalHandlingModifier { get; set; }

        /// <summary>Gets or sets the subtotal handling modifier mode.</summary>
        /// <value>The subtotal handling modifier mode.</value>
        int? SubtotalHandlingModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal discounts modifier.</summary>
        /// <value>The subtotal discounts modifier.</value>
        decimal? SubtotalDiscountsModifier { get; set; }

        /// <summary>Gets or sets the subtotal discounts modifier mode.</summary>
        /// <value>The subtotal discounts modifier mode.</value>
        int? SubtotalDiscountsModifierMode { get; set; }

        #region Convenience Properties
        /// <summary>Gets or sets the item discounts.</summary>
        /// <value>The item discounts.</value>
        List<IAppliedCartItemDiscountModel>? ItemDiscounts { get; set; }
        #endregion
    }
}
