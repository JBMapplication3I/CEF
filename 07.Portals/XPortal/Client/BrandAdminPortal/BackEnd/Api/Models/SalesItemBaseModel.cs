// <copyright file="SalesItemBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales item base model class</summary>
// ReSharper disable UnusedTypeParameter
// ReSharper disable MissingXmlDoc
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;

    /// <summary>A data Model for the sales item base.</summary>
    /// <typeparam name="TIItemDiscountModel">Type of the ti item discount model.</typeparam>
    /// <typeparam name="TItemDiscountModel"> Type of the item discount model.</typeparam>
    /// <seealso cref="SalesItemBaseModel"/>
    public class SalesItemBaseModel<TIItemDiscountModel, TItemDiscountModel> : SalesItemBaseModel
    {
        #region Associated Objects
        /// <summary>Gets or sets the discounts.</summary>
        /// <value>The discounts.</value>
        public List<TItemDiscountModel>? Discounts { get; set; }
        #endregion
    }

    /// <summary>A data Model for the sales item base.</summary>
    /// <seealso cref="NameableBaseModel"/>
    public partial class SalesItemBaseModel : NameableBaseModel
    {
        //// /// <inheritdoc/>
        //// public decimal TotalQuantity => Quantity + (QuantityBackOrdered ?? 0m) + (QuantityPreSold ?? 0m);

        //// /// <inheritdoc/>
        //// [DefaultValue(0)]
        //// public decimal ExtendedPrice => (UnitSoldPrice ?? UnitCorePrice) * TotalQuantity;

        /// <summary>Gets or sets the discount total.</summary>
        /// <value>The discount total.</value>
        public decimal? DiscountTotal { get; set; }

        public int? ProductID { get; set; }

        public string? ProductKey { get; set; }

        public string? ProductName { get; set; }

        public string? ProductSeoUrl { get; set; }

        public ProductModel? Product { get; set; }

        public int? UserID { get; set; }

        public string? UserKey { get; set; }

        public string? UserName { get; set; }

        public UserModel? User { get; set; }

        [ApiMember(Name = "Notes", DataType = "List<NoteModel>", ParameterType = "body", IsRequired = false,
            Description = "Notes for the object, optional")]
        public List<NoteModel>? Notes { get; set; }

        public decimal Quantity { get; set; }

        public decimal? QuantityBackOrdered { get; set; }

        public decimal? QuantityPreSold { get; set; }

        public decimal TotalQuantity { get; set; }

        public decimal UnitCorePrice { get; set; }

        public decimal? UnitSoldPrice { get; set; }

        public decimal? UnitSoldPriceModifier { get; set; }

        public int? UnitSoldPriceModifierMode { get; set; }

        public decimal ExtendedPrice { get; set; }

        public decimal? ExtendedShippingAmount { get; set; }

        public decimal? ExtendedTaxAmount { get; set; }

        public decimal? UnitCorePriceInSellingCurrency { get; set; }

        public decimal? UnitSoldPriceInSellingCurrency { get; set; }

        public decimal? ExtendedPriceInSellingCurrency { get; set; }

        public ItemType ItemType { get; set; }

        public string? UnitOfMeasure { get; set; }

        public string? Sku { get; set; }

        public decimal? RestockingFeeAmount { get; set; }

        public string? ForceUniqueLineItemKey { get; set; }

        public int? SalesReturnReasonID { get; set; }

        public string? SalesReturnReasonKey { get; set; }

        public string? SalesReturnReasonName { get; set; }

        public SalesReturnReasonModel? SalesReturnReason { get; set; }

        public string? ProductPrimaryImage { get; set; }

        public string? ProductDescription { get; set; }

        public string? ProductRequiresRoles { get; set; }

        public string? ProductRequiresRolesAlt { get; set; }

        public bool ProductIsDiscontinued { get; set; }

        public decimal? ProductMaximumPurchaseQuantity { get; set; }

        public decimal? ProductMaximumPurchaseQuantityIfPastPurchased { get; set; }

        public decimal? ProductMinimumPurchaseQuantity { get; set; }

        public decimal? ProductMinimumPurchaseQuantityIfPastPurchased { get; set; }

        public decimal? ProductMaximumBackOrderPurchaseQuantity { get; set; }

        public decimal? ProductMaximumBackOrderPurchaseQuantityIfPastPurchased { get; set; }

        public decimal? ProductMaximumBackOrderPurchaseQuantityGlobal { get; set; }

        public decimal? ProductMaximumPrePurchaseQuantity { get; set; }

        public decimal? ProductMaximumPrePurchaseQuantityIfPastPurchased { get; set; }

        public decimal? ProductMaximumPrePurchaseQuantityGlobal { get; set; }

        public bool ProductIsUnlimitedStock { get; set; }

        public bool ProductAllowBackOrder { get; set; }

        public bool ProductAllowPreSale { get; set; }

        public DateTime? ProductPreSellEndDate { get; set; }

        public bool ProductIsEligibleForReturn { get; set; }

        public decimal? ProductRestockingFeePercent { get; set; }

        public decimal? ProductRestockingFeeAmount { get; set; }

        public bool ProductNothingToShip { get; set; }

        public bool ProductDropShipOnly { get; set; }

        public bool ProductIsTaxable { get; set; }

        public string? ProductTaxCode { get; set; }

        public string? ProductShortDescription { get; set; }

        public string? ProductUnitOfMeasure { get; set; }

        public SerializableAttributesDictionary? ProductSerializableAttributes { get; set; }

        public int? ProductTypeID { get; set; }

        public string? ProductTypeKey { get; set; }

        public string? UserUserName { get; set; }

        public int MasterID { get; set; }

        public string? MasterKey { get; set; }

        public int? OriginalCurrencyID { get; set; }

        public string? OriginalCurrencyKey { get; set; }

        public string? OriginalCurrencyName { get; set; }

        public CurrencyModel? OriginalCurrency { get; set; }

        public int? SellingCurrencyID { get; set; }

        public string? SellingCurrencyKey { get; set; }

        public string? SellingCurrencyName { get; set; }

        public CurrencyModel? SellingCurrency { get; set; }

        public List<SalesItemTargetBaseModel>? Targets { get; set; }

        public DateTime? DateReceived { get; set; }

        public int? MaxAllowedInCart { get; set; }

        public List<string>? ProductDownloads { get; set; }

        public List<string>? ProductDownloadsNew { get; set; }
    }
}
