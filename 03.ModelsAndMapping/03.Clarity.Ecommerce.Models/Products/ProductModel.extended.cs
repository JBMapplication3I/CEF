// <copyright file="ProductModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product model class</summary>
// ReSharper disable MemberCanBePrivate.Global, MissingXmlDoc
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data model for the product.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IProductModel"/>
    public partial class ProductModel
    {
        #region HaveNullableDimensions Properties
        /// <inheritdoc/>
        public decimal? Weight { get; set; }

        /// <inheritdoc/>
        public string? WeightUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public decimal? Width { get; set; }

        /// <inheritdoc/>
        public string? WidthUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public decimal? Depth { get; set; }

        /// <inheritdoc/>
        public string? DepthUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public decimal? Height { get; set; }

        /// <inheritdoc/>
        public string? HeightUnitOfMeasure { get; set; }
        #endregion

        // Flags/Toggles

        /// <inheritdoc/>
        public bool IsVisible { get; set; }

        /// <inheritdoc/>
        public bool IsDiscontinued { get; set; }

        /// <inheritdoc/>
        public bool IsEligibleForReturn { get; set; }

        /// <inheritdoc/>
        public bool IsTaxable { get; set; }

        /// <inheritdoc/>
        public bool AllowBackOrder { get; set; }

        /// <inheritdoc/>
        public bool AllowPreSale { get; set; }

        /// <inheritdoc/>
        public bool IsUnlimitedStock { get; set; }

        /// <inheritdoc/>
        public bool IsFreeShipping { get; set; }

        /// <inheritdoc/>
        public bool NothingToShip { get; set; }

        /// <inheritdoc/>
        public bool DropShipOnly { get; set; }

        /// <inheritdoc/>
        public bool ShippingLeadTimeIsCalendarDays { get; set; }

        // Descriptors

        /// <inheritdoc/>
        public string? HCPCCode { get; set; }

        /// <inheritdoc/>
        public string? ShortDescription { get; set; }

        /// <inheritdoc/>
        public string? ManufacturerPartNumber { get; set; }

        /// <inheritdoc/>
        public string? BrandName { get; set; }

        /// <inheritdoc/>
        public string? TaxCode { get; set; }

        /// <inheritdoc/>
        public string? IndexSynonyms { get; set; }

        /// <inheritdoc/>
        public string? UnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public int? SortOrder { get; set; }

        // Fees

        /// <inheritdoc/>
        public decimal? HandlingCharge { get; set; }

        /// <inheritdoc/>
        public decimal? FlatShippingCharge { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RestockingFeePercent), DataType = "decimal?", ParameterType = "body", IsRequired = false,
            Description = "Restocking Fee Percentage of Product selling at")]
        public decimal? RestockingFeePercent { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RestockingFeeAmount), DataType = "decimal?", ParameterType = "body", IsRequired = false,
            Description = "Restocking Fee Amount")]
        public decimal? RestockingFeeAmount { get; set; }

        // Availability, Stock, Shipping Requirements

        /// <inheritdoc/>
        public DateTime? AvailableStartDate { get; set; }

        /// <inheritdoc/>
        public DateTime? AvailableEndDate { get; set; }

        /// <inheritdoc/>
        public DateTime? PreSellEndDate { get; set; }

        /// <inheritdoc/>
        public decimal? QuantityPerMasterPack { get; set; }

        /// <inheritdoc/>
        public decimal? QuantityMasterPackPerLayer { get; set; }

        /// <inheritdoc/>
        public decimal? QuantityMasterPackLayersPerPallet { get; set; }

        /// <inheritdoc/>
        public decimal? QuantityMasterPackPerPallet { get; set; }

        /// <inheritdoc/>
        public decimal? QuantityPerLayer { get; set; }

        /// <inheritdoc/>
        public decimal? QuantityLayersPerPallet { get; set; }

        /// <inheritdoc/>
        public decimal? QuantityPerPallet { get; set; }

        /// <inheritdoc/>
        public decimal? KitBaseQuantityPriceMultiplier { get; set; }

        /// <inheritdoc/>
        public int? ShippingLeadTimeDays { get; set; }

        // Min/Max Purchase Per Order

        /// <inheritdoc/>
        public decimal? MinimumPurchaseQuantity { get; set; }

        /// <inheritdoc/>
        public decimal? MinimumPurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        public decimal? MaximumPurchaseQuantity { get; set; }

        /// <inheritdoc/>
        public decimal? MaximumPurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        public decimal? MaximumBackOrderPurchaseQuantity { get; set; }

        /// <inheritdoc/>
        public decimal? MaximumBackOrderPurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        public decimal? MaximumBackOrderPurchaseQuantityGlobal { get; set; }

        /// <inheritdoc/>
        public decimal? MaximumPrePurchaseQuantity { get; set; }

        /// <inheritdoc/>
        public decimal? MaximumPrePurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        public decimal? MaximumPrePurchaseQuantityGlobal { get; set; }

        // Required Document

        /// <inheritdoc/>
        public string? DocumentRequiredForPurchase { get; set; }

        /// <inheritdoc/>
        public string? DocumentRequiredForPurchaseMissingWarningMessage { get; set; }

        /// <inheritdoc/>
        public string? DocumentRequiredForPurchaseExpiredWarningMessage { get; set; }

        /// <inheritdoc/>
        public decimal? DocumentRequiredForPurchaseOverrideFee { get; set; }

        /// <inheritdoc/>
        public bool DocumentRequiredForPurchaseOverrideFeeIsPercent { get; set; }

        /// <inheritdoc/>
        public string? DocumentRequiredForPurchaseOverrideFeeWarningMessage { get; set; }

        /// <inheritdoc/>
        public string? DocumentRequiredForPurchaseOverrideFeeAcceptedMessage { get; set; }

        // Must Purchase In Multiples Of

        /// <inheritdoc/>
        public decimal? MustPurchaseInMultiplesOfAmount { get; set; }

        /// <inheritdoc/>
        public string? MustPurchaseInMultiplesOfAmountWarningMessage { get; set; }

        /// <inheritdoc/>
        public decimal? MustPurchaseInMultiplesOfAmountOverrideFee { get; set; }

        /// <inheritdoc/>
        public bool MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent { get; set; }

        /// <inheritdoc/>
        public string? MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage { get; set; }

        /// <inheritdoc/>
        public string? MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage { get; set; }

        // Analytics filled data

        /// <inheritdoc/>
        public decimal? TotalPurchasedAmount { get; set; }

        /// <inheritdoc/>
        public int? TotalPurchasedAmountCurrencyID { get; set; }

        /// <inheritdoc/>
        public string? TotalPurchasedAmountCurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? TotalPurchasedAmountCurrencyName { get; set; }

        /// <inheritdoc cref="IProductModel.TotalPurchasedAmountCurrency"/>
        public CurrencyModel? TotalPurchasedAmountCurrency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? IProductModel.TotalPurchasedAmountCurrency { get => TotalPurchasedAmountCurrency; set => TotalPurchasedAmountCurrency = (CurrencyModel?)value; }

        /// <inheritdoc/>
        public decimal? TotalPurchasedQuantity { get; set; }

        // Convenience Properties

        /// <inheritdoc/>
        public bool IsShippingRestricted { get; set; }

        // Related Objects

        /// <inheritdoc/>
        public int? PackageID { get; set; }

        /// <inheritdoc cref="IProductModel.Package"/>
        public PackageModel? Package { get; set; }

        /// <inheritdoc/>
        IPackageModel? IProductModel.Package { get => Package; set => Package = (PackageModel?)value; }

        /// <inheritdoc/>
        public string? PackageKey { get; set; }

        /// <inheritdoc/>
        public string? PackageName { get; set; }

        /// <inheritdoc/>
        public int? MasterPackID { get; set; }

        /// <inheritdoc cref="IProductModel.MasterPack"/>
        public PackageModel? MasterPack { get; set; }

        /// <inheritdoc/>
        IPackageModel? IProductModel.MasterPack { get => MasterPack; set => MasterPack = (PackageModel?)value; }

        /// <inheritdoc/>
        public string? MasterPackKey { get; set; }

        /// <inheritdoc/>
        public string? MasterPackName { get; set; }

        /// <inheritdoc/>
        public int? PalletID { get; set; }

        /// <inheritdoc cref="IProductModel.Pallet"/>
        public PackageModel? Pallet { get; set; }

        /// <inheritdoc/>
        IPackageModel? IProductModel.Pallet { get => Pallet; set => Pallet = (PackageModel?)value; }

        /// <inheritdoc/>
        public string? PalletKey { get; set; }

        /// <inheritdoc/>
        public string? PalletName { get; set; }

        /// <inheritdoc/>
        public string? RestockingFeeAmountCurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? RestockingFeeAmountCurrencyName { get; set; }

        /// <inheritdoc/>
        public int? RestockingFeeAmountCurrencyID { get; set; }

        /// <inheritdoc cref="IProductModel.RestockingFeeAmountCurrency"/>
        public CurrencyModel? RestockingFeeAmountCurrency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? IProductModel.RestockingFeeAmountCurrency { get => RestockingFeeAmountCurrency; set => RestockingFeeAmountCurrency = (CurrencyModel?)value; }

        // Associated Objects

        /// <inheritdoc cref="IProductModel.ProductCategories"/>
        public List<ProductCategoryModel>? ProductCategories { get; set; }

        /// <inheritdoc/>
        List<IProductCategoryModel>? IProductModel.ProductCategories { get => ProductCategories?.ToList<IProductCategoryModel>(); set => ProductCategories = value?.Cast<ProductCategoryModel>().ToList(); }

        /// <inheritdoc cref="IProductModel.ProductShipCarrierMethods"/>
        public List<ProductShipCarrierMethodModel>? ProductShipCarrierMethods { get; set; }

        /// <inheritdoc/>
        List<IProductShipCarrierMethodModel>? IProductModel.ProductShipCarrierMethods { get => ProductShipCarrierMethods?.ToList<IProductShipCarrierMethodModel>(); set => ProductShipCarrierMethods = value?.Cast<ProductShipCarrierMethodModel>().ToList(); }

        /// <inheritdoc cref="IProductModel.ProductDownloads"/>
        public List<ProductDownloadModel>? ProductDownloads { get; set; }

        /// <inheritdoc/>
        List<IProductDownloadModel>? IProductModel.ProductDownloads { get => ProductDownloads?.ToList<IProductDownloadModel>(); set => ProductDownloads = value?.Cast<ProductDownloadModel>().ToList(); }

        /// <inheritdoc cref="IProductModel.ProductAssociations"/>
        public List<ProductAssociationModel>? ProductAssociations { get; set; }

        /// <inheritdoc/>
        List<IProductAssociationModel>? IProductModel.ProductAssociations { get => ProductAssociations?.ToList<IProductAssociationModel>(); set => ProductAssociations = value?.Cast<ProductAssociationModel>().ToList(); }

        /// <inheritdoc cref="IProductModel.ProductsAssociatedWith"/>
        public List<ProductAssociationModel>? ProductsAssociatedWith { get; set; }

        /// <inheritdoc/>
        List<IProductAssociationModel>? IProductModel.ProductsAssociatedWith { get => ProductsAssociatedWith?.ToList<IProductAssociationModel>(); set => ProductsAssociatedWith = value?.Cast<ProductAssociationModel>().ToList(); }

        /// <inheritdoc cref="IProductModel.ProductRestrictions"/>
        public List<ProductRestrictionModel>? ProductRestrictions { get; set; }

        /// <inheritdoc/>
        List<IProductRestrictionModel>? IProductModel.ProductRestrictions { get => ProductRestrictions?.ToList<IProductRestrictionModel>(); set => ProductRestrictions = value?.Cast<ProductRestrictionModel>().ToList(); }

        /// <inheritdoc cref="IProductModel.ProductSubscriptionTypes"/>
        public List<ProductSubscriptionTypeModel>? ProductSubscriptionTypes { get; set; }

        /// <inheritdoc/>
        List<IProductSubscriptionTypeModel>? IProductModel.ProductSubscriptionTypes { get => ProductSubscriptionTypes?.ToList<IProductSubscriptionTypeModel>(); set => ProductSubscriptionTypes = value?.Cast<ProductSubscriptionTypeModel>().ToList(); }

        /// <inheritdoc cref="IProductModel.ProductMembershipLevels"/>
        public List<ProductMembershipLevelModel>? ProductMembershipLevels { get; set; }

        /// <inheritdoc/>
        List<IProductMembershipLevelModel>? IProductModel.ProductMembershipLevels { get => ProductMembershipLevels?.ToList<IProductMembershipLevelModel>(); set => ProductMembershipLevels = value?.Cast<ProductMembershipLevelModel>().ToList(); }

        /// <inheritdoc cref="IProductModel.ProductNotifications"/>
        public List<ProductNotificationModel>? ProductNotifications { get; set; }

        /// <inheritdoc/>
        List<IProductNotificationModel>? IProductModel.ProductNotifications { get => ProductNotifications?.ToList<IProductNotificationModel>(); set => ProductNotifications = value?.Cast<ProductNotificationModel>().ToList(); }

        // Private Flags

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsVendorAdmin), DataType = "bool?", ParameterType = "body", IsRequired = false,
            Description = "When true, will only return values as part of the vendor's admin's filter. This value is set by the server and cannot be overriden.")]
        public bool? IsVendorAdmin { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(VendorAdminID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "When set, will only return values as part of the vendor's admin's filter. This value is set by the server and cannot be overriden.")]
        public int? VendorAdminID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsManufacturerAdmin), DataType = "bool?", ParameterType = "body", IsRequired = false,
            Description = "When true, will only return values as part of the manufacturer's admin's filter. This value is set by the server and cannot be overriden.")]
        public bool? IsManufacturerAdmin { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ManufacturerAdminID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "When set, will only return values as part of the manufacturer's admin's filter. This value is set by the server and cannot be overriden.")]
        public int? ManufacturerAdminID { get; set; }
    }
}
