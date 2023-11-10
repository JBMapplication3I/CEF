// <copyright file="ISalesItemBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesItemBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for sales item base.</summary>
    /// <typeparam name="TThis">    Type of this.</typeparam>
    /// <typeparam name="TDiscount">Type of the discount entity.</typeparam>
    /// <typeparam name="TTarget">  Type of the shipment target entity.</typeparam>
    public interface ISalesItemBase<TThis, TDiscount, TTarget>
        : INameableBase,
            IHaveNotesBase,
            IHaveAppliedDiscountsBase<TThis, TDiscount>,
            IAmFilterableByNullableProduct,
            IAmFilterableByNullableUser
        where TThis : IHaveAppliedDiscountsBase<TThis, TDiscount>
        where TDiscount : IAppliedDiscountBase<TThis, TDiscount>
        where TTarget : ISalesItemTargetBase
    {
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        decimal Quantity { get; set; }

        /// <summary>Gets or sets the quantity back ordered.</summary>
        /// <value>The quantity back ordered.</value>
        decimal? QuantityBackOrdered { get; set; }

        /// <summary>Gets or sets the quantity pre-sold.</summary>
        /// <value>The quantity pre-sold.</value>
        decimal? QuantityPreSold { get; set; }

        /// <summary>Gets the total quantity.</summary>
        /// <value>The total quantity.</value>
        decimal TotalQuantity { get; }

        /// <summary>Gets or sets the unit core price.</summary>
        /// <value>The unit core price.</value>
        decimal UnitCorePrice { get; set; }

        /// <summary>Gets or sets the unit sold price.</summary>
        /// <value>The unit sold price.</value>
        decimal? UnitSoldPrice { get; set; }

        /// <summary>Gets or sets the unit core price in selling currency.</summary>
        /// <value>The unit core price in selling currency.</value>
        decimal? UnitCorePriceInSellingCurrency { get; set; }

        /// <summary>Gets or sets the unit sold price in selling currency.</summary>
        /// <value>The unit sold price in selling currency.</value>
        decimal? UnitSoldPriceInSellingCurrency { get; set; }

        /// <summary>Gets or sets the SKU.</summary>
        /// <value>The SKU.</value>
        string? Sku { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        string? UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the force unique line item key. When set, other line items that would have merged on to
        /// this one will not be allowed to do so unless they have a matching key.</summary>
        /// <value>The force unique line item key.</value>
        string? ForceUniqueLineItemKey { get; set; }

        /// <summary>Gets or sets the status of the sales item base.</summary>
        /// <value>The status.</value>
        string? Status { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int MasterID { get; set; }

        /// <summary>Gets or sets the master.</summary>
        /// <value>The master.</value>
        /// <remarks>Note: This is only for searching.</remarks>
        ISalesCollectionBase? Master { get; set; }

        /// <summary>Gets or sets the identifier of the original currency.</summary>
        /// <value>The identifier of the original currency.</value>
        int? OriginalCurrencyID { get; set; }

        /// <summary>Gets or sets the original currency.</summary>
        /// <value>The original currency.</value>
        Currency? OriginalCurrency { get; set; }

        /// <summary>Gets or sets the identifier of the selling currency.</summary>
        /// <value>The identifier of the selling currency.</value>
        int? SellingCurrencyID { get; set; }

        /// <summary>Gets or sets the selling currency.</summary>
        /// <value>The selling currency.</value>
        Currency? SellingCurrency { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the targets.</summary>
        /// <value>The targets.</value>
        ICollection<TTarget>? Targets { get; set; }
        #endregion
    }
}
