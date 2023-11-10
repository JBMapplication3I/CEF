// <copyright file="SalesItemBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales item base model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the sales item base.</summary>
    /// <typeparam name="TIItemDiscountModel">Type of the item discount model's interface.</typeparam>
    /// <typeparam name="TItemDiscountModel"> Type of the item discount model.</typeparam>
    /// <seealso cref="SalesItemBaseModel"/>
    /// <seealso cref="ISalesItemBaseModel{TIItemDiscountModel}"/>
    public class SalesItemBaseModel<TIItemDiscountModel, TItemDiscountModel>
        : SalesItemBaseModel,
            ISalesItemBaseModel<TIItemDiscountModel>
        where TIItemDiscountModel : IAppliedDiscountBaseModel
        where TItemDiscountModel : class, TIItemDiscountModel
    {
        #region Associated Objects
        /// <inheritdoc cref="IHaveAppliedDiscountsBaseModel{TIItemDiscountModel}.Discounts"/>
        public List<TItemDiscountModel>? Discounts { get; set; }

        /// <inheritdoc/>
        List<TIItemDiscountModel>? IHaveAppliedDiscountsBaseModel<TIItemDiscountModel>.Discounts { get => Discounts?.ToList<TIItemDiscountModel>(); set => Discounts = value?.Cast<TItemDiscountModel>().ToList(); }
        #endregion
    }

    /// <summary>A data Model for the sales item base.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="ISalesItemBaseModel"/>
    public partial class SalesItemBaseModel
    {
        /// <inheritdoc/>
        [DefaultValue(0)]
        public decimal Quantity { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? QuantityBackOrdered { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? QuantityPreSold { get; set; }

        /// <inheritdoc/>
        public decimal TotalQuantity => Quantity + (QuantityBackOrdered ?? 0m) + (QuantityPreSold ?? 0m);

        /// <inheritdoc/>
        [DefaultValue(0)]
        public decimal UnitCorePrice { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? UnitSoldPrice { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? UnitSoldPriceModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? UnitSoldPriceModifierMode { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public decimal ExtendedPrice => (UnitSoldPrice ?? UnitCorePrice) * TotalQuantity;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ExtendedShippingAmount { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ExtendedTaxAmount { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? UnitCorePriceInSellingCurrency { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? UnitSoldPriceInSellingCurrency { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ExtendedPriceInSellingCurrency { get; set; }

        /// <inheritdoc/>
        [DefaultValue(Enums.ItemType.Item)]
        public Enums.ItemType ItemType { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? UnitOfMeasure { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? Sku { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? RestockingFeeAmount { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? ForceUniqueLineItemKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? Status { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? ProductPrimaryImage { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? ProductDescription { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? ProductRequiresRoles { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? ProductRequiresRolesAlt { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool ProductIsDiscontinued { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ProductMaximumPurchaseQuantity { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ProductMaximumPurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ProductMinimumPurchaseQuantity { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ProductMinimumPurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ProductMaximumBackOrderPurchaseQuantity { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ProductMaximumBackOrderPurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ProductMaximumBackOrderPurchaseQuantityGlobal { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ProductMaximumPrePurchaseQuantity { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ProductMaximumPrePurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ProductMaximumPrePurchaseQuantityGlobal { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool ProductIsUnlimitedStock { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool ProductAllowBackOrder { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool ProductAllowPreSale { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? ProductPreSellEndDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool ProductIsEligibleForReturn { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ProductRestockingFeePercent { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? ProductRestockingFeeAmount { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool ProductNothingToShip { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool ProductDropShipOnly { get; set; }

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool ProductIsTaxable { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? ProductTaxCode { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? ProductShortDescription { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? ProductUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public SerializableAttributesDictionary? ProductSerializableAttributes { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ProductTypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? ProductTypeKey { get; set; }

        /// <inheritdoc/>
        public string? UserUserName { get; set; }

        /// <inheritdoc/>
        public int MasterID { get; set; }

        /// <summary>Gets or sets the master key.</summary>
        /// <value>The master key.</value>
        public string? MasterKey { get; set; }

        /// <inheritdoc/>
        public int? OriginalCurrencyID { get; set; }

        /// <inheritdoc/>
        public string? OriginalCurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? OriginalCurrencyName { get; set; }

        /// <inheritdoc cref="ISalesItemBaseModel.OriginalCurrency"/>
        public CurrencyModel? OriginalCurrency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? ISalesItemBaseModel.OriginalCurrency { get => OriginalCurrency; set => OriginalCurrency = (CurrencyModel?)value; }

        /// <inheritdoc/>
        public int? SellingCurrencyID { get; set; }

        /// <inheritdoc/>
        public string? SellingCurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? SellingCurrencyName { get; set; }

        /// <inheritdoc cref="ISalesItemBaseModel.SellingCurrency"/>
        public CurrencyModel? SellingCurrency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? ISalesItemBaseModel.SellingCurrency { get => SellingCurrency; set => SellingCurrency = (CurrencyModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="ISalesItemBaseModel.Targets"/>
        public List<SalesItemTargetBaseModel>? Targets { get; set; }

        /// <inheritdoc/>
        List<ISalesItemTargetBaseModel>? ISalesItemBaseModel.Targets { get => Targets?.ToList<ISalesItemTargetBaseModel>(); set => Targets = value?.Cast<SalesItemTargetBaseModel>().ToList(); }
        #endregion

        #region Specifics
        /// <inheritdoc/>
        public DateTime? DateReceived { get; set; }

        /// <inheritdoc/>
        public int? MaxAllowedInCart { get; set; }
        #endregion

        #region Downloads
        /// <inheritdoc/>
        public List<string>? ProductDownloads { get; set; }

        /// <inheritdoc/>
        public List<string>? ProductDownloadsNew { get; set; }
        #endregion
    }
}
