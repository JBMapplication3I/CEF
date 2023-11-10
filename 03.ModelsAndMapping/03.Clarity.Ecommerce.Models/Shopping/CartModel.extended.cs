// <copyright file="CartModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the cart.</summary>
    /// <seealso cref="SalesCollectionBaseModel"/>
    /// <seealso cref="ICartModel"/>
    public partial class CartModel
    {
        /// <inheritdoc/>
        public bool NothingToShip => SalesItems?.All(x => x.ProductNothingToShip) ?? false;

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RequestedShipDate), DataType = "DateTime?", ParameterType = "body", IsRequired = false,
            Description = "The date when a user would like to receive their products")]
        public DateTime? RequestedShipDate { get; set; }

        /// <inheritdoc/>
        public decimal? SubtotalShippingModifier { get; set; }

        /// <inheritdoc/>
        public int? SubtotalShippingModifierMode { get; set; }

        /// <inheritdoc/>
        public decimal? SubtotalTaxesModifier { get; set; }

        /// <inheritdoc/>
        public int? SubtotalTaxesModifierMode { get; set; }

        /// <inheritdoc/>
        public decimal? SubtotalFeesModifier { get; set; }

        /// <inheritdoc/>
        public int? SubtotalFeesModifierMode { get; set; }

        /// <inheritdoc/>
        public decimal? SubtotalHandlingModifier { get; set; }

        /// <inheritdoc/>
        public int? SubtotalHandlingModifierMode { get; set; }

        /// <inheritdoc/>
        public decimal? SubtotalDiscountsModifier { get; set; }

        /// <inheritdoc/>
        public int? SubtotalDiscountsModifierMode { get; set; }

        /// <inheritdoc cref="ICartModel.ItemDiscounts"/>
        public List<AppliedCartItemDiscountModel>? ItemDiscounts { get; set; }

        /// <inheritdoc/>
        List<IAppliedCartItemDiscountModel>? ICartModel.ItemDiscounts { get => ItemDiscounts?.ToList<IAppliedCartItemDiscountModel>(); set => ItemDiscounts = value?.Cast<AppliedCartItemDiscountModel>().ToList(); }
    }
}
