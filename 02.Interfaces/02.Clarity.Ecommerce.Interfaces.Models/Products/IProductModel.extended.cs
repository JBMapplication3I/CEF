// <copyright file="IProductModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for product model.</summary>
    public partial interface IProductModel
        : IHaveNullableDimensionsModel, IAmVendorAdminModified, IAmManufacturerAdminModified
    {
        #region Flags/Toggles
        /// <summary>Gets or sets a value indicating whether this IProductModel is visible.</summary>
        /// <value>True if this IProductModel is visible, false if not.</value>
        bool IsVisible { get; set; }

        /// <summary>Gets or sets a value indicating whether this IProductModel is discontinued.</summary>
        /// <value>True if this IProductModel is discontinued, false if not.</value>
        bool IsDiscontinued { get; set; }

        /// <summary>Gets or sets a value indicating whether this IProductModel is eligible for return.</summary>
        /// <value>True if this IProductModel is eligible for return, false if not.</value>
        bool IsEligibleForReturn { get; set; }

        /// <summary>Gets or sets a value indicating whether this IProductModel is taxable.</summary>
        /// <value>True if this IProductModel is taxable, false if not.</value>
        bool IsTaxable { get; set; }

        /// <summary>Gets or sets a value indicating whether we allow back order.</summary>
        /// <value>True if allow back order, false if not.</value>
        bool AllowBackOrder { get; set; }

        /// <summary>Gets or sets a value indicating whether we allow pre sale.</summary>
        /// <value>True if allow pre sale, false if not.</value>
        bool AllowPreSale { get; set; }

        /// <summary>Gets or sets a value indicating whether this IProductModel is unlimited stock.</summary>
        /// <value>True if this IProductModel is unlimited stock, false if not.</value>
        bool IsUnlimitedStock { get; set; }

        /// <summary>Gets or sets a value indicating whether this IProductModel is free shipping.</summary>
        /// <value>True if this IProductModel is free shipping, false if not.</value>
        bool IsFreeShipping { get; set; }

        /// <summary>Gets or sets a value indicating whether the nothing to ship.</summary>
        /// <value>True if nothing to ship, false if not.</value>
        bool NothingToShip { get; set; }

        /// <summary>Gets or sets a value indicating whether the drop ship only.</summary>
        /// <value>True if drop ship only, false if not.</value>
        bool DropShipOnly { get; set; }

        /// <summary>Gets or sets a value indicating whether the shipping lead time is calendar days.</summary>
        /// <value>True if shipping lead time is calendar days, false if not.</value>
        bool ShippingLeadTimeIsCalendarDays { get; set; }
        #endregion

        #region Descriptors
        /// <summary>Gets or sets HCPC Code..</summary>
        /// <value>The HCPC Code.</value>
        string? HCPCCode { get; set; }

        /// <summary>Gets or sets information describing the short.</summary>
        /// <value>Information describing the short.</value>
        string? ShortDescription { get; set; }

        /// <summary>Gets or sets the manufacturer part number.</summary>
        /// <value>The manufacturer part number.</value>
        string? ManufacturerPartNumber { get; set; }

        /// <summary>Gets or sets the name of the brand.</summary>
        /// <value>The name of the brand.</value>
        string? BrandName { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        string? UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the tax code.</summary>
        /// <value>The tax code.</value>
        string? TaxCode { get; set; }

        /// <summary>Gets or sets the index synonyms.</summary>
        /// <value>The index synonyms.</value>
        string? IndexSynonyms { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }
        #endregion

        #region Fees
        /// <summary>Gets or sets the handling charge.</summary>
        /// <value>The handling charge.</value>
        decimal? HandlingCharge { get; set; }

        /// <summary>Gets or sets the flat shipping charge.</summary>
        /// <value>The flat shipping charge.</value>
        decimal? FlatShippingCharge { get; set; }

        /// <summary>Gets or sets the restocking fee percent.</summary>
        /// <value>The restocking fee percent.</value>
        decimal? RestockingFeePercent { get; set; }

        /// <summary>Gets or sets the restocking fee amount.</summary>
        /// <value>The restocking fee amount.</value>
        decimal? RestockingFeeAmount { get; set; }
        #endregion

        #region Availability, Shipping Requirements
        /// <summary>Gets or sets the available start date.</summary>
        /// <value>The available start date.</value>
        DateTime? AvailableStartDate { get; set; }

        /// <summary>Gets or sets the available end date.</summary>
        /// <value>The available end date.</value>
        DateTime? AvailableEndDate { get; set; }

        /// <summary>Gets or sets the pre sell end date.</summary>
        /// <value>The pre sell end date.</value>
        DateTime? PreSellEndDate { get; set; }

        /// <summary>Gets or sets the quantity per master pack.</summary>
        /// <value>The quantity per master pack.</value>
        decimal? QuantityPerMasterPack { get; set; }

        /// <summary>Gets or sets the quantity master pack per layer.</summary>
        /// <value>The quantity master pack per layer.</value>
        decimal? QuantityMasterPackPerLayer { get; set; }

        /// <summary>Gets or sets the quantity master pack layers per pallet.</summary>
        /// <value>The quantity master pack layers per pallet.</value>
        decimal? QuantityMasterPackLayersPerPallet { get; set; }

        /// <summary>Gets or sets the quantity master pack per pallet.</summary>
        /// <value>The quantity master pack per pallet.</value>
        decimal? QuantityMasterPackPerPallet { get; set; }

        /// <summary>Gets or sets the quantity per layer.</summary>
        /// <value>The quantity per layer.</value>
        decimal? QuantityPerLayer { get; set; }

        /// <summary>Gets or sets the quantity layers per pallet.</summary>
        /// <value>The quantity layers per pallet.</value>
        decimal? QuantityLayersPerPallet { get; set; }

        /// <summary>Gets or sets the quantity per pallet.</summary>
        /// <value>The quantity per pallet.</value>
        decimal? QuantityPerPallet { get; set; }

        /// <summary>Gets or sets the shipping lead time days.</summary>
        /// <value>The shipping lead time days.</value>
        int? ShippingLeadTimeDays { get; set; }
        #endregion

        #region Min/Max Purchase Per Order
        /// <summary>Gets or sets the minimum purchase quantity.</summary>
        /// <value>The minimum purchase quantity.</value>
        decimal? MinimumPurchaseQuantity { get; set; }

        /// <summary>Gets or sets the minimum purchase quantity if past purchased.</summary>
        /// <value>The minimum purchase quantity if past purchased.</value>
        decimal? MinimumPurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the maximum purchase quantity.</summary>
        /// <value>The maximum purchase quantity.</value>
        decimal? MaximumPurchaseQuantity { get; set; }

        /// <summary>Gets or sets the maximum purchase quantity if past purchased.</summary>
        /// <value>The maximum purchase quantity if past purchased.</value>
        decimal? MaximumPurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the maximum back order purchase quantity.</summary>
        /// <value>The maximum back order purchase quantity.</value>
        decimal? MaximumBackOrderPurchaseQuantity { get; set; }

        /// <summary>Gets or sets the maximum back order purchase quantity if past purchased.</summary>
        /// <value>The maximum back order purchase quantity if past purchased.</value>
        decimal? MaximumBackOrderPurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the maximum back order purchase quantity global.</summary>
        /// <value>The maximum back order purchase quantity global.</value>
        decimal? MaximumBackOrderPurchaseQuantityGlobal { get; set; }

        /// <summary>Gets or sets the maximum pre purchase quantity.</summary>
        /// <value>The maximum pre purchase quantity.</value>
        decimal? MaximumPrePurchaseQuantity { get; set; }

        /// <summary>Gets or sets the maximum pre purchase quantity if past purchased.</summary>
        /// <value>The maximum pre purchase quantity if past purchased.</value>
        decimal? MaximumPrePurchaseQuantityIfPastPurchased { get; set; }

        /// <summary>Gets or sets the maximum pre purchase quantity global.</summary>
        /// <value>The maximum pre purchase quantity global.</value>
        decimal? MaximumPrePurchaseQuantityGlobal { get; set; }
        #endregion

        #region Required Document
        /// <summary>Gets or sets the document required for purchase.</summary>
        /// <value>The document required for purchase.</value>
        string? DocumentRequiredForPurchase { get; set; }

        /// <summary>Gets or sets a message describing the document required for purchase missing warning.</summary>
        /// <value>A message describing the document required for purchase missing warning.</value>
        string? DocumentRequiredForPurchaseMissingWarningMessage { get; set; }

        /// <summary>Gets or sets a message describing the document required for purchase expired warning.</summary>
        /// <value>A message describing the document required for purchase expired warning.</value>
        string? DocumentRequiredForPurchaseExpiredWarningMessage { get; set; }

        /// <summary>Gets or sets the document required for purchase override fee.</summary>
        /// <value>The document required for purchase override fee.</value>
        decimal? DocumentRequiredForPurchaseOverrideFee { get; set; }

        /// <summary>Gets or sets a value indicating whether the document required for purchase override fee is
        /// percent.</summary>
        /// <value>True if document required for purchase override fee is percent, false if not.</value>
        bool DocumentRequiredForPurchaseOverrideFeeIsPercent { get; set; }

        /// <summary>Gets or sets a message describing the document required for purchase override fee warning.</summary>
        /// <value>A message describing the document required for purchase override fee warning.</value>
        string? DocumentRequiredForPurchaseOverrideFeeWarningMessage { get; set; }

        /// <summary>Gets or sets a message describing the document required for purchase override fee accepted.</summary>
        /// <value>A message describing the document required for purchase override fee accepted.</value>
        string? DocumentRequiredForPurchaseOverrideFeeAcceptedMessage { get; set; }
        #endregion

        #region Must Purchase In Multiples Of
        /// <summary>Gets or sets the must purchase in multiples of amount.</summary>
        /// <value>The must purchase in multiples of amount.</value>
        decimal? MustPurchaseInMultiplesOfAmount { get; set; }

        /// <summary>Gets or sets a message describing the must purchase in multiples of amount warning.</summary>
        /// <value>A message describing the must purchase in multiples of amount warning.</value>
        string? MustPurchaseInMultiplesOfAmountWarningMessage { get; set; }

        /// <summary>Gets or sets the must purchase in multiples of amount override fee.</summary>
        /// <value>The must purchase in multiples of amount override fee.</value>
        decimal? MustPurchaseInMultiplesOfAmountOverrideFee { get; set; }

        /// <summary>Gets or sets a value indicating whether the must purchase in multiples of amount override fee is
        /// percent.</summary>
        /// <value>True if we must purchase in multiples of amount override fee is percent, false if not.</value>
        bool MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent { get; set; }

        /// <summary>Gets or sets a message describing the must purchase in multiples of amount override fee warning.</summary>
        /// <value>A message describing the must purchase in multiples of amount override fee warning.</value>
        string? MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage { get; set; }

        /// <summary>Gets or sets a message describing the must purchase in multiples of amount override fee accepted.</summary>
        /// <value>A message describing the must purchase in multiples of amount override fee accepted.</value>
        string? MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage { get; set; }
        #endregion

        #region Analytics filled data
        /// <summary>Gets or sets the total number of purchased amount.</summary>
        /// <value>The total number of purchased amount.</value>
        decimal? TotalPurchasedAmount { get; set; }

        /// <summary>Gets or sets the total number of purchased amount currency identifier.</summary>
        /// <value>The total number of purchased amount currency identifier.</value>
        int? TotalPurchasedAmountCurrencyID { get; set; }

        /// <summary>Gets or sets the total number of purchased amount currency key.</summary>
        /// <value>The total number of purchased amount currency key.</value>
        string? TotalPurchasedAmountCurrencyKey { get; set; }

        /// <summary>Gets or sets the total number of purchased amount currency name.</summary>
        /// <value>The total number of purchased amount currency name.</value>
        string? TotalPurchasedAmountCurrencyName { get; set; }

        /// <summary>Gets or sets the total number of purchased amount currency.</summary>
        /// <value>The total number of purchased amount currency.</value>
        ICurrencyModel? TotalPurchasedAmountCurrency { get; set; }

        /// <summary>Gets or sets the total number of purchased quantity.</summary>
        /// <value>The total number of purchased quantity.</value>
        decimal? TotalPurchasedQuantity { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the package.</summary>
        /// <value>The identifier of the package.</value>
        int? PackageID { get; set; }

        /// <summary>Gets or sets the package.</summary>
        /// <value>The package.</value>
        IPackageModel? Package { get; set; }

        /// <summary>Gets or sets the package key.</summary>
        /// <value>The package key.</value>
        string? PackageKey { get; set; }

        /// <summary>Gets or sets the name of the package.</summary>
        /// <value>The name of the package.</value>
        string? PackageName { get; set; }

        /// <summary>Gets or sets the identifier of the master pack.</summary>
        /// <value>The identifier of the master pack.</value>
        int? MasterPackID { get; set; }

        /// <summary>Gets or sets the master pack.</summary>
        /// <value>The master pack.</value>
        IPackageModel? MasterPack { get; set; }

        /// <summary>Gets or sets the master pack key.</summary>
        /// <value>The master pack key.</value>
        string? MasterPackKey { get; set; }

        /// <summary>Gets or sets the name of the master pack.</summary>
        /// <value>The name of the master pack.</value>
        string? MasterPackName { get; set; }

        /// <summary>Gets or sets the identifier of the pallet.</summary>
        /// <value>The identifier of the pallet.</value>
        int? PalletID { get; set; }

        /// <summary>Gets or sets the pallet.</summary>
        /// <value>The pallet.</value>
        IPackageModel? Pallet { get; set; }

        /// <summary>Gets or sets the pallet key.</summary>
        /// <value>The pallet key.</value>
        string? PalletKey { get; set; }

        /// <summary>Gets or sets the name of the pallet.</summary>
        /// <value>The name of the pallet.</value>
        string? PalletName { get; set; }

        /// <summary>Gets or sets the identifier of the restocking fee amount currency.</summary>
        /// <value>The identifier of the restocking fee amount currency.</value>
        int? RestockingFeeAmountCurrencyID { get; set; }

        /// <summary>Gets or sets the restocking fee amount currency.</summary>
        /// <value>The restocking fee amount currency.</value>
        ICurrencyModel? RestockingFeeAmountCurrency { get; set; }

        /// <summary>Gets or sets the restocking fee amount currency key.</summary>
        /// <value>The restocking fee amount currency key.</value>
        string? RestockingFeeAmountCurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the restocking fee amount currency.</summary>
        /// <value>The name of the restocking fee amount currency.</value>
        string? RestockingFeeAmountCurrencyName { get; set; }

        /// <summary>Gets or sets the kit base quantity price multiplier.</summary>
        /// <value>The kit base quantity price multiplier.</value>
        decimal? KitBaseQuantityPriceMultiplier { get; set; }
        #endregion

        #region Convenience Properties
        /// <summary>Gets or sets a value indicating whether this IProductModel is shipping restricted.</summary>
        /// <value>True if this IProductModel is shipping restricted, false if not.</value>
        bool IsShippingRestricted { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the product ship carrier methods.</summary>
        /// <value>The product ship carrier methods.</value>
        List<IProductShipCarrierMethodModel>? ProductShipCarrierMethods { get; set; }

        /// <summary>Gets or sets the product associations.</summary>
        /// <value>The product associations.</value>
        List<IProductAssociationModel>? ProductAssociations { get; set; }

        /// <summary>Gets or sets the categories the product belongs to.</summary>
        /// <value>The product categories.</value>
        List<IProductCategoryModel>? ProductCategories { get; set; }

        /// <summary>Gets or sets the product downloads.</summary>
        /// <value>The product downloads.</value>
        List<IProductDownloadModel>? ProductDownloads { get; set; }

        /// <summary>Gets or sets the products associated with.</summary>
        /// <value>The products associated with.</value>
        List<IProductAssociationModel>? ProductsAssociatedWith { get; set; }

        /// <summary>Gets or sets the product notifications.</summary>
        /// <value>The product notifications.</value>
        List<IProductNotificationModel>? ProductNotifications { get; set; }
        #endregion

        #region Sales
        /// <summary>Gets or sets a list of types of the product subscriptions.</summary>
        /// <value>A list of types of the product subscriptions.</value>
        List<IProductSubscriptionTypeModel>? ProductSubscriptionTypes { get; set; }

        /// <summary>Gets or sets the product membership levels.</summary>
        /// <value>The product membership levels.</value>
        List<IProductMembershipLevelModel>? ProductMembershipLevels { get; set; }

        /// <summary>Gets or sets the product restrictions.</summary>
        /// <value>The product restrictions.</value>
        List<IProductRestrictionModel>? ProductRestrictions { get; set; }
        #endregion
    }
}
