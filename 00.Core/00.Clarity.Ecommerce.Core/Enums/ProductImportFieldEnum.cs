// <copyright file="ProductImportFieldEnum.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product import field enum class</summary>
// ReSharper disable StyleCop.SA1602
namespace Clarity.Ecommerce.Enums
{
    /// <summary>Values that represent product import field enums.</summary>
    public enum ProductImportFieldEnum
    {
        /// <summary>An enum constant representing the do not import option.</summary>
        DoNotImport,

        /// <summary>An enum constant representing the import action option.</summary>
        ImportAction,

        /// <summary>An enum constant representing the attribute option.</summary>
        Attribute,

        /// <summary>An enum constant representing the custom key option.</summary>
        CustomKey,

        /// <summary>An enum constant representing the name option.</summary>
        Name,

        /// <summary>An enum constant representing the description option.</summary>
        Description,

        /// <summary>An enum constant representing the seo keywords option.</summary>
        SeoKeywords,

        /// <summary>An enum constant representing the seo URL option.</summary>
        SeoUrl,

        /// <summary>An enum constant representing the seo page title option.</summary>
        SeoPageTitle,

        /// <summary>An enum constant representing the seo description option.</summary>
        SeoDescription,

        /// <summary>An enum constant representing the manufacturer part number option.</summary>
        ManufacturerPartNumber,

        /// <summary>An enum constant representing the short description option.</summary>
        ShortDescription,

        /// <summary>An enum constant representing the price base option.</summary>
        PriceBase,

        /// <summary>An enum constant representing the price msrp option.</summary>
        PriceMsrp,

        /// <summary>An enum constant representing the price reduction option.</summary>
        PriceReduction,

        /// <summary>An enum constant representing the price sale option.</summary>
        PriceSale,

        /// <summary>An enum constant representing the handling charge option.</summary>
        HandlingCharge,

        /// <summary>An enum constant representing the is visible option.</summary>
        IsVisible,

        /// <summary>An enum constant representing the is taxable option.</summary>
        IsTaxable,

        /// <summary>An enum constant representing the is free shipping option.</summary>
        IsFreeShipping,

        /// <summary>An enum constant representing the available start date option.</summary>
        AvailableStartDate,

        /// <summary>An enum constant representing the available end date option.</summary>
        AvailableEndDate,

        /// <summary>An enum constant representing the stock quantity option.</summary>
        StockQuantity,

        /// <summary>An enum constant representing the is unlimited stock option.</summary>
        IsUnlimitedStock,

        /// <summary>An enum constant representing the allow back order option.</summary>
        AllowBackOrder,

        /// <summary>An enum constant representing the is discontinued option.</summary>
        IsDiscontinued,

        /// <summary>An enum constant representing the unit of measure option.</summary>
        UnitOfMeasure,

        /// <summary>An enum constant representing the minimum purchase quantity option.</summary>
        MinimumPurchaseQuantity,

        /// <summary>An enum constant representing the minimum purchase quantity if past purchased option.</summary>
        MinimumPurchaseQuantityIfPastPurchased,

        /// <summary>An enum constant representing the maximum purchase quantity option.</summary>
        MaximumPurchaseQuantity,

        /// <summary>An enum constant representing the maximum purchase quantity if past purchased option.</summary>
        MaximumPurchaseQuantityIfPastPurchased,

        /// <summary>An enum constant representing the kit base quantity price multiplier option.</summary>
        KitBaseQuantityPriceMultiplier,

        /// <summary>An enum constant representing the weight option.</summary>
        Weight,

        /// <summary>An enum constant representing the weight unit of measure option.</summary>
        WeightUnitOfMeasure,

        /// <summary>An enum constant representing the width option.</summary>
        Width,

        /// <summary>An enum constant representing the width unit of measure option.</summary>
        WidthUnitOfMeasure,

        /// <summary>An enum constant representing the depth option.</summary>
        Depth,

        /// <summary>An enum constant representing the depth unit of measure option.</summary>
        DepthUnitOfMeasure,

        /// <summary>An enum constant representing the height option.</summary>
        Height,

        /// <summary>An enum constant representing the height unit of measure option.</summary>
        HeightUnitOfMeasure,

        /// <summary>An enum constant representing the sort order option.</summary>
        SortOrder,

        /// <summary>An enum constant representing the package width option.</summary>
        PackageWidth,

        /// <summary>An enum constant representing the package width unit of measure option.</summary>
        PackageWidthUnitOfMeasure,

        /// <summary>An enum constant representing the package depth option.</summary>
        PackageDepth,

        /// <summary>An enum constant representing the package depth unit of measure option.</summary>
        PackageDepthUnitOfMeasure,

        /// <summary>An enum constant representing the package height option.</summary>
        PackageHeight,

        /// <summary>An enum constant representing the package height unit of measure option.</summary>
        PackageHeightUnitOfMeasure,

        /// <summary>An enum constant representing the package weight option.</summary>
        PackageWeight,

        /// <summary>An enum constant representing the package weight unit of measure option.</summary>
        PackageWeightUnitOfMeasure,

        /// <summary>An enum constant representing the master pack width option.</summary>
        MasterPackWidth,

        /// <summary>An enum constant representing the master pack width unit of measure option.</summary>
        MasterPackWidthUnitOfMeasure,

        /// <summary>An enum constant representing the master pack depth option.</summary>
        MasterPackDepth,

        /// <summary>An enum constant representing the master pack depth unit of measure option.</summary>
        MasterPackDepthUnitOfMeasure,

        /// <summary>An enum constant representing the master pack height option.</summary>
        MasterPackHeight,

        /// <summary>An enum constant representing the master pack height unit of measure option.</summary>
        MasterPackHeightUnitOfMeasure,

        /// <summary>An enum constant representing the master pack weight option.</summary>
        MasterPackWeight,

        /// <summary>An enum constant representing the master pack weight unit of measure option.</summary>
        MasterPackWeightUnitOfMeasure,

        /// <summary>An enum constant representing the pallet width option.</summary>
        PalletWidth,

        /// <summary>An enum constant representing the pallet width unit of measure option.</summary>
        PalletWidthUnitOfMeasure,

        /// <summary>An enum constant representing the pallet depth option.</summary>
        PalletDepth,

        /// <summary>An enum constant representing the pallet depth unit of measure option.</summary>
        PalletDepthUnitOfMeasure,

        /// <summary>An enum constant representing the pallet height option.</summary>
        PalletHeight,

        /// <summary>An enum constant representing the pallet height unit of measure option.</summary>
        PalletHeightUnitOfMeasure,

        /// <summary>An enum constant representing the pallet weight option.</summary>
        PalletWeight,

        /// <summary>An enum constant representing the pallet weight unit of measure option.</summary>
        PalletWeightUnitOfMeasure,

        /// <summary>An enum constant representing the quantity per master pack option.</summary>
        QuantityPerMasterPack,

        /// <summary>An enum constant representing the quantity master pack per pallet option.</summary>
        QuantityMasterPackPerPallet,

        /// <summary>An enum constant representing the quantity per pallet option.</summary>
        QuantityPerPallet,

        /// <summary>An enum constant representing the vendors option.</summary>
        Vendors,

        /// <summary>An enum constant representing the manufacturers option.</summary>
        Manufacturers,

        /// <summary>An enum constant representing the price points option.</summary>
        PricePoints,

        /// <summary>An enum constant representing the inventory location sections option.</summary>
        InventoryLocationSections,

        /// <summary>An enum constant representing the categories option.</summary>
        Categories,

        /// <summary>An enum constant representing the images option.</summary>
        Images,

        /// <summary>An enum constant representing the related products option.</summary>
        RelatedProducts,

        /// <summary>An enum constant representing the related products type option.</summary>
        RelatedProductsType,

        /// <summary>An enum constant representing the related products 2 option.</summary>
        RelatedProducts2,

        /// <summary>An enum constant representing the related products type 2 option.</summary>
        RelatedProductsType2,

        /// <summary>An enum constant representing the related products 3 option.</summary>
        RelatedProducts3,

        /// <summary>An enum constant representing the related products type 3 option.</summary>
        RelatedProductsType3,

        /// <summary>An enum constant representing the related products 4 option.</summary>
        RelatedProducts4,

        /// <summary>An enum constant representing the related products type 4 option.</summary>
        RelatedProductsType4,

        /// <summary>An enum constant representing the product type option.</summary>
        ProductType,
    }
}
