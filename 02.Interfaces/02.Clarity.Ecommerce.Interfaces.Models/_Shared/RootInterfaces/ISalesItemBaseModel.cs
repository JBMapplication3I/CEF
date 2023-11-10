// <copyright file="ISalesItemBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesItemBaseModel interface</summary>
// ReSharper disable MissingBlankLines
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for sales item base model.</summary>
    /// <typeparam name="TIDiscountModel">Type of the discount model's interface.</typeparam>
    public interface ISalesItemBaseModel<TIDiscountModel>
        : ISalesItemBaseModel,
            IHaveAppliedDiscountsBaseModel<TIDiscountModel>
        where TIDiscountModel : IAppliedDiscountBaseModel
    {
    }

    /// <summary>Interface for sales item base model.</summary>
    public partial interface ISalesItemBaseModel
    {
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        decimal Quantity { get; set; }

        /// <summary>Gets or sets the quantity back ordered.</summary>
        /// <value>The quantity back ordered.</value>
        decimal? QuantityBackOrdered { get; set; }

        /// <summary>Gets or sets the quantity pre sold.</summary>
        /// <value>The quantity pre sold.</value>
        decimal? QuantityPreSold { get; set; }

        /// <summary>Gets the total number of quantity.</summary>
        /// <value>The total number of quantity.</value>
        decimal TotalQuantity { get; }

        /// <summary>Gets or sets the unit core price.</summary>
        /// <value>The unit core price.</value>
        decimal UnitCorePrice { get; set; }

        /// <summary>Gets or sets the unit core price in selling currency.</summary>
        /// <value>The unit core price in selling currency.</value>
        decimal? UnitCorePriceInSellingCurrency { get; set; }

        /// <summary>Gets or sets the unit sold price.</summary>
        /// <value>The unit sold price.</value>
        decimal? UnitSoldPrice { get; set; }

        /// <summary>Gets or sets the unit sold price modifier.</summary>
        /// <value>The unit sold price modifier.</value>
        decimal? UnitSoldPriceModifier { get; set; }

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

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        string? Status { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int MasterID { get; set; }

        /// <summary>Gets or sets the product primary image.</summary>
        /// <value>The product primary image.</value>
        string? ProductPrimaryImage { get; set; }

        /// <summary>Gets or sets information describing the product.</summary>
        /// <value>Information describing the product.</value>
        string? ProductDescription { get; set; }

        /// <summary>Gets or sets the product requires roles.</summary>
        /// <value>The product requires roles.</value>
        string? ProductRequiresRoles { get; set; }

        /// <summary>Gets or sets the product requires roles alternate.</summary>
        /// <value>The product requires roles alternate.</value>
        string? ProductRequiresRolesAlt { get; set; }

        /// <summary>Gets or sets a value indicating whether the product is discontinued.</summary>
        /// <value>True if product is discontinued, false if not.</value>
        bool ProductIsDiscontinued { get; set; }

        /// <summary>Gets or sets the product maximum purchase quantity.</summary>
        /// <value>The product maximum purchase quantity.</value>
        decimal? ProductMaximumPurchaseQuantity { get; set; }

        /// <summary>Gets or sets the product maximum purchase quantity if past purchased.</summary>
        /// <value>The product maximum purchase quantity if past purchased.</value>
        decimal? ProductMaximumPurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the product minimum purchase quantity.</summary>
        /// <value>The product minimum purchase quantity.</value>
        decimal? ProductMinimumPurchaseQuantity { get; set; }

        /// <summary>Gets or sets the product minimum purchase quantity if past purchased.</summary>
        /// <value>The product minimum purchase quantity if past purchased.</value>
        decimal? ProductMinimumPurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the product maximum back order purchase quantity.</summary>
        /// <value>The product maximum back order purchase quantity.</value>
        decimal? ProductMaximumBackOrderPurchaseQuantity { get; set; }

        /// <summary>Gets or sets the product maximum back order purchase quantity if past purchased.</summary>
        /// <value>The product maximum back order purchase quantity if past purchased.</value>
        decimal? ProductMaximumBackOrderPurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the product maximum back order purchase quantity global.</summary>
        /// <value>The product maximum back order purchase quantity global.</value>
        decimal? ProductMaximumBackOrderPurchaseQuantityGlobal { get; set; }

        /// <summary>Gets or sets the product maximum pre purchase quantity.</summary>
        /// <value>The product maximum pre purchase quantity.</value>
        decimal? ProductMaximumPrePurchaseQuantity { get; set; }

        /// <summary>Gets or sets the product maximum pre purchase quantity if past purchased.</summary>
        /// <value>The product maximum pre purchase quantity if past purchased.</value>
        decimal? ProductMaximumPrePurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the product maximum pre purchase quantity global.</summary>
        /// <value>The product maximum pre purchase quantity global.</value>
        decimal? ProductMaximumPrePurchaseQuantityGlobal { get; set; }

        /// <summary>Gets or sets a value indicating whether the product is unlimited stock.</summary>
        /// <value>True if product is unlimited stock, false if not.</value>
        bool ProductIsUnlimitedStock { get; set; }

        /// <summary>Gets or sets a value indicating whether the product allow back order.</summary>
        /// <value>True if product allow back order, false if not.</value>
        bool ProductAllowBackOrder { get; set; }

        /// <summary>Gets or sets a value indicating whether the product allow pre sale.</summary>
        /// <value>True if product allow pre sale, false if not.</value>
        bool ProductAllowPreSale { get; set; }

        /// <summary>Gets or sets the product pre sell end date.</summary>
        /// <value>The product pre sell end date.</value>
        DateTime? ProductPreSellEndDate { get; set; }

        /// <summary>Gets or sets a value indicating whether the product is eligible for return.</summary>
        /// <value>True if product is eligible for return, false if not.</value>
        bool ProductIsEligibleForReturn { get; set; }

        /// <summary>Gets or sets the product restocking fee percent.</summary>
        /// <value>The product restocking fee percent.</value>
        decimal? ProductRestockingFeePercent { get; set; }

        /// <summary>Gets or sets the product restocking fee amount.</summary>
        /// <value>The product restocking fee amount.</value>
        decimal? ProductRestockingFeeAmount { get; set; }

        /// <summary>Gets or sets a value indicating whether the product nothing to ship.</summary>
        /// <value>True if product nothing to ship, false if not.</value>
        bool ProductNothingToShip { get; set; }

        /// <summary>Gets or sets a value indicating whether the product drop ship only.</summary>
        /// <value>True if product drop ship only, false if not.</value>
        bool ProductDropShipOnly { get; set; }

        /// <summary>Gets or sets a value indicating whether this ISalesItemBaseModel is product is taxable.</summary>
        /// <value>True if product is taxable, false if not.</value>
        bool ProductIsTaxable { get; set; }

        /// <summary>Gets or sets the product tax code.</summary>
        /// <value>The product tax code.</value>
        string? ProductTaxCode { get; set; }

        /// <summary>Gets or sets information describing the product short.</summary>
        /// <value>Information describing the product short.</value>
        string? ProductShortDescription { get; set; }

        /// <summary>Gets or sets the product unit of measure.</summary>
        /// <value>The product unit of measure.</value>
        string? ProductUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the product serializable attributes.</summary>
        /// <value>The product serializable attributes.</value>
        SerializableAttributesDictionary? ProductSerializableAttributes { get; set; }

        /// <summary>Gets or sets the identifier of the product type.</summary>
        /// <value>The identifier of the product type.</value>
        int? ProductTypeID { get; set; }

        /// <summary>Gets or sets the product type key.</summary>
        /// <value>The product type key.</value>
        string? ProductTypeKey { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        string? UserUserName { get; set; }

        /// <summary>Gets or sets the identifier of the original currency.</summary>
        /// <value>The identifier of the original currency.</value>
        int? OriginalCurrencyID { get; set; }

        /// <summary>Gets or sets the original currency key.</summary>
        /// <value>The original currency key.</value>
        string? OriginalCurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the original currency.</summary>
        /// <value>The name of the original currency.</value>
        string? OriginalCurrencyName { get; set; }

        /// <summary>Gets or sets the original currency.</summary>
        /// <value>The original currency.</value>
        ICurrencyModel? OriginalCurrency { get; set; }

        /// <summary>Gets or sets the identifier of the selling currency.</summary>
        /// <value>The identifier of the selling currency.</value>
        int? SellingCurrencyID { get; set; }

        /// <summary>Gets or sets the selling currency key.</summary>
        /// <value>The selling currency key.</value>
        string? SellingCurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the selling currency.</summary>
        /// <value>The name of the selling currency.</value>
        string? SellingCurrencyName { get; set; }

        /// <summary>Gets or sets the selling currency.</summary>
        /// <value>The selling currency.</value>
        ICurrencyModel? SellingCurrency { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the targets.</summary>
        /// <value>The targets.</value>
        List<ISalesItemTargetBaseModel>? Targets { get; set; }
        #endregion
    }

    /// <summary>Specifics from each kind.</summary>
    public partial interface ISalesItemBaseModel
    {
        #region Sales Item Base Properties
        /// <summary>Gets or sets the unit sold price modifier mode.</summary>
        /// <value>The unit sold price modifier mode.</value>
        int? UnitSoldPriceModifierMode { get; set; }

        /// <summary>Gets the extended price.</summary>
        /// <value>The extended price.</value>
        decimal ExtendedPrice { get; }

        /// <summary>Gets or sets the extended tax amount.</summary>
        /// <value>The extended tax amount.</value>
        decimal? ExtendedTaxAmount { get; set; }

        /// <summary>Gets or sets the extended shipping amount.</summary>
        /// <value>The extended shipping amount.</value>
        decimal? ExtendedShippingAmount { get; set; }

        /// <summary>Gets or sets the extended price in selling currency.</summary>
        /// <value>The extended price in selling currency.</value>
        decimal? ExtendedPriceInSellingCurrency { get; set; }

        /// <summary>Gets or sets the type of the item.</summary>
        /// <value>The type of the item.</value>
        Enums.ItemType ItemType { get; set; }

        /// <summary>Gets or sets the maximum allowed in cart.</summary>
        /// <value>The maximum allowed in cart.</value>
        int? MaxAllowedInCart { get; set; }

        /// <summary>Gets or sets the Date/Time of the date received.</summary>
        /// <value>The date received.</value>
        DateTime? DateReceived { get; set; }

        /// <summary>Gets or sets the restocking fee amount.</summary>
        /// <value>The restocking fee amount.</value>
        decimal? RestockingFeeAmount { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the product downloads.</summary>
        /// <value>The product downloads.</value>
        List<string>? ProductDownloads { get; set; }

        /// <summary>Gets or sets the product downloads new.</summary>
        /// <value>The product downloads new.</value>
        List<string>? ProductDownloadsNew { get; set; }
        #endregion
    }
}
