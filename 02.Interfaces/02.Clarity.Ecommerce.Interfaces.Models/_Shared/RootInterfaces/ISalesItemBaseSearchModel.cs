// <copyright file="ISalesItemBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesItemBaseSearchModel interface</summary>
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for sales item base search model.</summary>
    public interface ISalesItemBaseSearchModel
        : INameableBaseSearchModel,
            IAmFilterableByProductSearchModel,
            IAmFilterableByUserSearchModel
    {
        /// <summary>Gets or sets the SKU.</summary>
        /// <value>The SKU.</value>
        string? Sku { get; set; }

        /// <summary>Gets or sets the identifier of the user external.</summary>
        /// <value>The identifier of the user external.</value>
        string? UserExternalID { get; set; }

        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int? MasterID { get; set; }

        /// <summary>Gets or sets the master key.</summary>
        /// <value>The master key.</value>
        string? MasterKey { get; set; }

        /// <summary>Gets or sets the name of the master type.</summary>
        /// <value>The name of the master type.</value>
        string? MasterTypeName { get; set; }

        /// <summary>Gets or sets the identifier of the cart session.</summary>
        /// <value>The identifier of the cart session.</value>
        Guid? CartSessionID { get; set; }

        /// <summary>Gets or sets the identifier of the original currency.</summary>
        /// <value>The identifier of the original currency.</value>
        int? OriginalCurrencyID { get; set; }

        /// <summary>Gets or sets the identifier of the selling currency.</summary>
        /// <value>The identifier of the selling currency.</value>
        int? SellingCurrencyID { get; set; }

        /// <summary>Gets or sets the identifier of the cart user.</summary>
        /// <value>The identifier of the cart user.</value>
        int? CartUserID { get; set; }

        /// <summary>Gets or sets the tracking number.</summary>
        /// <value>The tracking number.</value>
        string? TrackingNumber { get; set; }

        /// <summary>Gets or sets the force unique line item key.</summary>
        /// <value>The force unique line item key.</value>
        string? ForceUniqueLineItemKey { get; set; }

        /// <summary>Gets or sets the force unique line item key match nulls.</summary>
        /// <value>The force unique line item key match nulls.</value>
        bool? ForceUniqueLineItemKeyMatchNulls { get; set; }

        /// <summary>Gets or sets the minimum quantity.</summary>
        /// <value>The minimum quantity.</value>
        decimal? MinQuantity { get; set; }

        /// <summary>Gets or sets the maximum quantity.</summary>
        /// <value>The maximum quantity.</value>
        decimal? MaxQuantity { get; set; }

        /// <summary>Gets or sets the match quantity.</summary>
        /// <value>The match quantity.</value>
        decimal? MatchQuantity { get; set; }

        /// <summary>Gets or sets the minimum unit core price.</summary>
        /// <value>The minimum unit core price.</value>
        decimal? MinUnitCorePrice { get; set; }

        /// <summary>Gets or sets the maximum unit core price.</summary>
        /// <value>The maximum unit core price.</value>
        decimal? MaxUnitCorePrice { get; set; }

        /// <summary>Gets or sets the match unit core price.</summary>
        /// <value>The match unit core price.</value>
        decimal? MatchUnitCorePrice { get; set; }

        /// <summary>Gets or sets the original currency identifier include null.</summary>
        /// <value>The original currency identifier include null.</value>
        bool? OriginalCurrencyIDIncludeNull { get; set; }

        /// <summary>Gets or sets the product identifier include null.</summary>
        /// <value>The product identifier include null.</value>
        bool? ProductIDIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum quantity back ordered.</summary>
        /// <value>The minimum quantity back ordered.</value>
        decimal? MinQuantityBackOrdered { get; set; }

        /// <summary>Gets or sets the maximum quantity back ordered.</summary>
        /// <value>The maximum quantity back ordered.</value>
        decimal? MaxQuantityBackOrdered { get; set; }

        /// <summary>Gets or sets the match quantity back ordered.</summary>
        /// <value>The match quantity back ordered.</value>
        decimal? MatchQuantityBackOrdered { get; set; }

        /// <summary>Gets or sets the match quantity back ordered include null.</summary>
        /// <value>The match quantity back ordered include null.</value>
        bool? MatchQuantityBackOrderedIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum quantity pre sold.</summary>
        /// <value>The minimum quantity pre sold.</value>
        decimal? MinQuantityPreSold { get; set; }

        /// <summary>Gets or sets the maximum quantity pre sold.</summary>
        /// <value>The maximum quantity pre sold.</value>
        decimal? MaxQuantityPreSold { get; set; }

        /// <summary>Gets or sets the match quantity pre sold.</summary>
        /// <value>The match quantity pre sold.</value>
        decimal? MatchQuantityPreSold { get; set; }

        /// <summary>Gets or sets the match quantity pre sold include null.</summary>
        /// <value>The match quantity pre sold include null.</value>
        bool? MatchQuantityPreSoldIncludeNull { get; set; }

        /// <summary>Gets or sets the selling currency identifier include null.</summary>
        /// <value>The selling currency identifier include null.</value>
        bool? SellingCurrencyIDIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum unit core price in selling currency.</summary>
        /// <value>The minimum unit core price in selling currency.</value>
        decimal? MinUnitCorePriceInSellingCurrency { get; set; }

        /// <summary>Gets or sets the maximum unit core price in selling currency.</summary>
        /// <value>The maximum unit core price in selling currency.</value>
        decimal? MaxUnitCorePriceInSellingCurrency { get; set; }

        /// <summary>Gets or sets the match unit core price in selling currency.</summary>
        /// <value>The match unit core price in selling currency.</value>
        decimal? MatchUnitCorePriceInSellingCurrency { get; set; }

        /// <summary>Gets or sets the match unit core price in selling currency include null.</summary>
        /// <value>The match unit core price in selling currency include null.</value>
        bool? MatchUnitCorePriceInSellingCurrencyIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum unit sold price.</summary>
        /// <value>The minimum unit sold price.</value>
        decimal? MinUnitSoldPrice { get; set; }

        /// <summary>Gets or sets the maximum unit sold price.</summary>
        /// <value>The maximum unit sold price.</value>
        decimal? MaxUnitSoldPrice { get; set; }

        /// <summary>Gets or sets the match unit sold price.</summary>
        /// <value>The match unit sold price.</value>
        decimal? MatchUnitSoldPrice { get; set; }

        /// <summary>Gets or sets the match unit sold price include null.</summary>
        /// <value>The match unit sold price include null.</value>
        bool? MatchUnitSoldPriceIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum unit sold price in selling currency.</summary>
        /// <value>The minimum unit sold price in selling currency.</value>
        decimal? MinUnitSoldPriceInSellingCurrency { get; set; }

        /// <summary>Gets or sets the maximum unit sold price in selling currency.</summary>
        /// <value>The maximum unit sold price in selling currency.</value>
        decimal? MaxUnitSoldPriceInSellingCurrency { get; set; }

        /// <summary>Gets or sets the match unit sold price in selling currency.</summary>
        /// <value>The match unit sold price in selling currency.</value>
        decimal? MatchUnitSoldPriceInSellingCurrency { get; set; }

        /// <summary>Gets or sets the match unit sold price in selling currency include null.</summary>
        /// <value>The match unit sold price in selling currency include null.</value>
        bool? MatchUnitSoldPriceInSellingCurrencyIncludeNull { get; set; }

        /// <summary>Gets or sets the force unique line item key strict.</summary>
        /// <value>The force unique line item key strict.</value>
        bool? ForceUniqueLineItemKeyStrict { get; set; }

        /// <summary>Gets or sets the force unique line item key include null.</summary>
        /// <value>The force unique line item key include null.</value>
        bool? ForceUniqueLineItemKeyIncludeNull { get; set; }

        /// <summary>Gets or sets the SKU strict.</summary>
        /// <value>The SKU strict.</value>
        bool? SkuStrict { get; set; }

        /// <summary>Gets or sets the SKU include null.</summary>
        /// <value>The SKU include null.</value>
        bool? SkuIncludeNull { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        string? UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the unit of measure strict.</summary>
        /// <value>The unit of measure strict.</value>
        bool? UnitOfMeasureStrict { get; set; }

        /// <summary>Gets or sets the unit of measure include null.</summary>
        /// <value>The unit of measure include null.</value>
        bool? UnitOfMeasureIncludeNull { get; set; }

        #region Cart Items only
        /// <summary>Gets or sets the minimum unit sold price modifier.</summary>
        /// <value>The minimum unit sold price modifier.</value>
        decimal? MinUnitSoldPriceModifier { get; set; }

        /// <summary>Gets or sets the maximum unit sold price modifier.</summary>
        /// <value>The maximum unit sold price modifier.</value>
        decimal? MaxUnitSoldPriceModifier { get; set; }

        /// <summary>Gets or sets the match unit sold price modifier.</summary>
        /// <value>The match unit sold price modifier.</value>
        decimal? MatchUnitSoldPriceModifier { get; set; }

        /// <summary>Gets or sets the match unit sold price modifier include null.</summary>
        /// <value>The match unit sold price modifier include null.</value>
        bool? MatchUnitSoldPriceModifierIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum unit sold price modifier mode.</summary>
        /// <value>The minimum unit sold price modifier mode.</value>
        int? MinUnitSoldPriceModifierMode { get; set; }

        /// <summary>Gets or sets the maximum unit sold price modifier mode.</summary>
        /// <value>The maximum unit sold price modifier mode.</value>
        int? MaxUnitSoldPriceModifierMode { get; set; }

        /// <summary>Gets or sets the match unit sold price modifier mode.</summary>
        /// <value>The match unit sold price modifier mode.</value>
        int? MatchUnitSoldPriceModifierMode { get; set; }

        /// <summary>Gets or sets the match unit sold price modifier mode include null.</summary>
        /// <value>The match unit sold price modifier mode include null.</value>
        bool? MatchUnitSoldPriceModifierModeIncludeNull { get; set; }
        #endregion

        #region Purchase Order Items Only
        /// <summary>Gets or sets the Date/Time of the minimum date received.</summary>
        /// <value>The minimum date received.</value>
        DateTime? MinDateReceived { get; set; }

        /// <summary>Gets or sets the Date/Time of the maximum date received.</summary>
        /// <value>The maximum date received.</value>
        DateTime? MaxDateReceived { get; set; }

        /// <summary>Gets or sets the Date/Time of the match date received.</summary>
        /// <value>The match date received.</value>
        DateTime? MatchDateReceived { get; set; }

        /// <summary>Gets or sets the match date received include null.</summary>
        /// <value>The match date received include null.</value>
        bool? MatchDateReceivedIncludeNull { get; set; }
        #endregion

        #region Sales Return Items Only
        /// <summary>Gets or sets the minimum restocking fee amount.</summary>
        /// <value>The minimum restocking fee amount.</value>
        decimal? MinRestockingFeeAmount { get; set; }

        /// <summary>Gets or sets the maximum restocking fee amount.</summary>
        /// <value>The maximum restocking fee amount.</value>
        decimal? MaxRestockingFeeAmount { get; set; }

        /// <summary>Gets or sets the match restocking fee amount.</summary>
        /// <value>The match restocking fee amount.</value>
        decimal? MatchRestockingFeeAmount { get; set; }

        /// <summary>Gets or sets the match restocking fee amount include null.</summary>
        /// <value>The match restocking fee amount include null.</value>
        bool? MatchRestockingFeeAmountIncludeNull { get; set; }

        /// <summary>Gets or sets the identifier of the sales return reason.</summary>
        /// <value>The identifier of the sales return reason.</value>
        int? SalesReturnReasonID { get; set; }

        /// <summary>Gets or sets the sales return reason identifier include null.</summary>
        /// <value>The sales return reason identifier include null.</value>
        bool? SalesReturnReasonIDIncludeNull { get; set; }
        #endregion
    }
}
