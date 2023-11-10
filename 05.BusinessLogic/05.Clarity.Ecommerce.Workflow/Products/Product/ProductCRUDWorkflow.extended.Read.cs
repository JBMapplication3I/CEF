// <copyright file="ProductCRUDWorkflow.extended.Read.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product workflow class</summary>
// ReSharper disable StyleCop.SA1202, StyleCop.SA1204
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Security;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using MoreLinq;
    using Newtonsoft.Json;
    using Utilities;

    public partial class ProductWorkflow
    {
        /// <inheritdoc/>
        public async Task<IProductModel?> GetFullAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetFullAsync(
                    await CheckExistsAsync(Contract.RequiresValidID(id), contextProfileName).ConfigureAwait(false),
                    context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IProductModel?> GetFullAsync(string key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetFullAsync(
                    await CheckExistsAsync(Contract.RequiresValidKey(key), contextProfileName).ConfigureAwait(false),
                    context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IProductModel?> GetAsync(
            int id,
            string? contextProfileName,
            bool isVendorAdmin,
            int? vendorAdminID,
            int? previewID)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetAsync(
                    id: id,
                    context: context,
                    isVendorAdmin: isVendorAdmin,
                    vendorAdminID: vendorAdminID,
                    previewID: previewID)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IProductModel?> GetAsync(
            int id,
            IClarityEcommerceEntities context,
            bool isVendorAdmin,
            int? vendorAdminID,
            int? previewID)
        {
            var entityID = await CheckExistsAsync(Contract.RequiresValidID(id), context).ConfigureAwait(false);
            if (Contract.CheckInvalidID(entityID))
            {
                return null;
            }
            await CleanProductsAsync(new[] { entityID!.Value }, context).ConfigureAwait(false);
            return await MapProductForStorefrontAsync(
                    context: context,
                    productID: entityID.Value,
                    isVendorAdmin: isVendorAdmin,
                    vendorAdminID: vendorAdminID,
                    previewID: previewID)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<IProductModel?> GetAsync(string key, IClarityEcommerceEntities context)
        {
            var entityID = await CheckExistsAsync(Contract.RequiresValidKey(key), context).ConfigureAwait(false);
            if (Contract.CheckInvalidID(entityID))
            {
                return null;
            }
            return context.Products
                // .AsNoTracking()
                .FilterByID(entityID)
                .SelectSingleFullProductAndMapToProductModel(context.ContextProfileName);
        }

        /// <inheritdoc/>
        public async Task<IProductModel?> GetAsync(
            string key,
            string? contextProfileName,
            bool isVendorAdmin,
            int? vendorAdminID)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetAsync(
                    key: key,
                    context: context,
                    isVendorAdmin: isVendorAdmin,
                    vendorAdminID: vendorAdminID)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IProductModel?> GetAsync(
            string key,
            IClarityEcommerceEntities context,
            bool isVendorAdmin,
            int? vendorAdminID)
        {
            var entityID = await CheckExistsAsync(Contract.RequiresValidKey(key), context).ConfigureAwait(false);
            if (Contract.CheckInvalidID(entityID))
            {
                return null;
            }
            await CleanProductsAsync(new[] { entityID!.Value }, context).ConfigureAwait(false);
            return await MapProductForStorefrontAsync(
                    context: context,
                    productID: entityID.Value,
                    isVendorAdmin: isVendorAdmin,
                    vendorAdminID: vendorAdminID,
                    previewID: null)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IProductModel?> GetByNameAsync(
            string name,
            IClarityEcommerceEntities context,
            bool isVendorAdmin,
            int? vendorAdminID)
        {
            var entityID = await CheckExistsByNameAsync(Contract.RequiresValidKey(name), context).ConfigureAwait(false);
            if (Contract.CheckInvalidID(entityID))
            {
                return null;
            }
            await CleanProductsAsync(new[] { entityID!.Value }, context).ConfigureAwait(false);
            return await MapProductForStorefrontAsync(
                    context: context,
                    productID: entityID.Value,
                    isVendorAdmin: isVendorAdmin,
                    vendorAdminID: vendorAdminID,
                    previewID: null)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IProductModel?> GetProductBySeoUrlAsync(
            string seoUrl,
            string? contextProfileName,
            bool isVendorAdmin,
            int? vendorAdminID)
        {
            Contract.RequiresValidKey<InvalidOperationException>(seoUrl, "SEO URL is required");
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entityID = await CheckExistsBySeoUrlAsync(seoUrl, contextProfileName).ConfigureAwait(false);
            if (Contract.CheckInvalidID(entityID))
            {
                return null;
            }
            await CleanProductsAsync(new[] { entityID!.Value }, context).ConfigureAwait(false);
            return await MapProductForStorefrontAsync(
                    context: context,
                    productID: entityID.Value,
                    isVendorAdmin: isVendorAdmin,
                    vendorAdminID: vendorAdminID,
                    previewID: null)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetLastModifiedForBySeoUrlForMetaDataResultAsync(
            string seoUrl,
            string? contextProfileName)
        {
            Contract.RequiresValidKey<InvalidOperationException>(seoUrl, "SEO URL is required");
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entityID = await CheckExistsBySeoUrlAsync(seoUrl, context).ConfigureAwait(false);
            if (Contract.CheckInvalidID(entityID))
            {
                return null;
            }
            return await context.Products
                .AsNoTracking()
                .FilterByID(entityID)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IProductModel?> GetProductBySeoUrlForMetaDataAsync(
            string seoUrl,
            string? contextProfileName)
        {
            Contract.RequiresValidKey<InvalidOperationException>(seoUrl, "SEO URL is required");
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entityID = await CheckExistsBySeoUrlAsync(seoUrl, context).ConfigureAwait(false);
            if (Contract.CheckInvalidID(entityID))
            {
                return null;
            }
            var entity = await context.Products
                .AsNoTracking()
                .FilterByID(entityID)
                .Select(x => new
                {
                    x.ID,
                    x.SeoMetaData,
                    x.SeoPageTitle,
                    x.SeoDescription,
                    x.SeoKeywords,
                    x.SeoUrl,
                    x.Name,
                    x.ShortDescription,
                })
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            var model = RegistryLoaderWrapper.GetInstance<IProductModel>(contextProfileName);
            model.ID = entity.ID;
            model.Name = entity.Name;
            model.ShortDescription = entity.ShortDescription;
            model.SeoMetaData = entity.SeoMetaData;
            model.SeoPageTitle = entity.SeoPageTitle;
            model.SeoDescription = entity.SeoDescription;
            model.SeoKeywords = entity.SeoKeywords;
            model.SeoUrl = entity.SeoUrl;
            return model;
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetLastModifiedForByIDsResultAsync(
            List<int> productIDs,
            int? brandID,
            int? storeID,
            bool isVendorAdmin,
            int? vendorAdminID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Products
                .AsNoTracking()
                .FilterByIDs(productIDs)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .FirstAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<IProductModel>> GetByIDsAsync(
            List<int> productIDs,
            int? brandID,
            int? storeID,
            bool isVendorAdmin,
            int? vendorAdminID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await CleanProductsAsync(productIDs.ToArray(), context).ConfigureAwait(false);
            var products = new List<IProductModel>();
            foreach (var productID in productIDs)
            {
                var p = await MapProductForStorefrontAsync(
                        context: context,
                        productID: productID,
                        isVendorAdmin: isVendorAdmin,
                        vendorAdminID: vendorAdminID,
                        previewID: null)
                    .ConfigureAwait(false);
                if (p is null)
                {
                    continue;
                }
                products.Add(p);
            }
            return products;
        }

        /// <inheritdoc/>
        public async Task<IProductModel?> GetByManufacturerNumberAsync(
            string manufacturerNumber,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByManufacturerPartNumber(manufacturerNumber)
                .SelectFirstFullProductAndMapToProductModel(contextProfileName);
        }

        /// <inheritdoc/>
        public async Task<IProductModel?> RemoveHiddenFromStorefrontAttributesAsync(
            IProductModel product,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await RemoveHiddenFromStorefrontAttributesAsync(product, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IProductModel?> RemoveHiddenFromStorefrontAttributesAsync(
            IProductModel? product,
            IClarityEcommerceEntities context)
        {
            if (product is null)
            {
                return null;
            }
            var keys = await GetAttributeKeysThatShouldBeHiddenAsync(context.ContextProfileName).ConfigureAwait(false);
            foreach (var key in keys.Where(key => product.SerializableAttributes!.ContainsKey(key)))
            {
                if (product.SerializableAttributes!.TryRemove(key, out var dummy))
                {
                    continue;
                }
                throw new SecurityException("Cannot remove attribute key");
            }
            // ReSharper disable once InvertIf
            if (product.ProductCategories?.Count > 0)
            {
                foreach (var productCategory in product.ProductCategories)
                {
                    foreach (var key in keys
                        .Where(_ => productCategory.Slave?.SerializableAttributes != null)
                        .Where(key => productCategory.Slave!.SerializableAttributes!.ContainsKey(key)))
                    {
                        if (productCategory.Slave!.SerializableAttributes!.TryRemove(key, out var dummy))
                        {
                            continue;
                        }
                        throw new SecurityException("Cannot remove attribute key");
                    }
                }
            }
            // Return a clean product
            return product;
        }

        /// <inheritdoc/>
        public override async Task<(List<IProductModel> results, int totalPages, int totalCount)> SearchAsync(
            IProductSearchModel search,
            bool asListing,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(search);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            (List<IProductModel> results, int totalPages, int totalCount) results;
            var query = context.Products
                .FilterByNameableBaseSearchModel(search)
                .FilterByIDs(search.ProductIDs)
                .FilterByIDs(search.ComparisonIDs)
                .FilterBySeoUrl(search.SeoUrl, search.SeoUrlStrict, search.SeoUrlIncludeNull)
                .FilterByHaveATypeSearchModel<Product, ProductType>(search)
                .FilterIAmFilterableByMasterBrandsBySearchModel<Product, BrandProduct>(search)
                .FilterIAmFilterableByMasterStoresBySearchModel<Product, StoreProduct>(search)
                .FilterIAmFilterableByMasterVendorsBySearchModel<Product, VendorProduct>(search)
                .FilterProductsByBrandCategoryIDs<Product, BrandProduct>(search.BrandCategoryIDs)
                .FilterProductsByNothingToShip(search.NothingToShip)
                .FilterProductsByPrice(search.Price)
                .FilterProductsByTypeNames(search.TypeNames)
                .FilterProductsByShortDescription(search.SeoDescription)
                .FilterProductsByCategorySeoUrl(search.CategorySeoUrl)
                .FilterProductsByIsVisible(search.IsVisible)
                .FilterProductsByIsDiscontinued(search.IsDiscontinued)
                .FilterProductsByCategoryID(search.CategoryID)
                .FilterProductsByCategoryJsonAttributesByValues(search.CategoryJsonAttributes)
                .FilterProductsByAncestorCategoryIDs(search.ParentCategories)
                .FilterProductsByAncestorCategoryID(search.HasAnyAncestorCategoryID)
                .FilterProductsByAncestorCategoryName(search.CategoryName)
                .FilterByHaveAStatusSearchModel<Product, ProductStatus>(search);
            query = (await FilterByCategoryNameAsync(search, query, context).ConfigureAwait(false))
                .FilterByModifiedSince(search.ModifiedSince)
                .ApplySorting(search.Sorts, search.Groupings, contextProfileName)
                .FilterByPaging(search.Paging, out results.totalPages, out results.totalCount);
            await CleanProductsAsync(
                    await query.Select(x => x.ID).ToArrayAsync().ConfigureAwait(false),
                    context)
                .ConfigureAwait(false);
            var query3 = query
                .Select(x => new
                {
                    // Base Properties
                    x.ID,
                    x.CustomKey,
                    x.Active,
                    x.CreatedDate,
                    x.UpdatedDate,
                    x.Hash,
                    // NameableBase Properties
                    x.Name,
                    // IHaveSeoBase Properties
                    x.SeoUrl,
                    x.SeoPageTitle,
                    x.SeoKeywords,
                    x.SeoDescription,
                    x.SeoMetaData,
                    // IHaveNullableDimensions Properties
                    x.Weight,
                    x.WeightUnitOfMeasure,
                    x.Width,
                    x.WidthUnitOfMeasure,
                    x.Depth,
                    x.DepthUnitOfMeasure,
                    x.Height,
                    x.HeightUnitOfMeasure,
                    // Flags/Toggles
                    x.IsVisible,
                    x.IsDiscontinued,
                    x.IsEligibleForReturn,
                    x.IsTaxable,
                    x.AllowBackOrder,
                    x.AllowPreSale,
                    x.IsUnlimitedStock,
                    x.IsFreeShipping,
                    x.NothingToShip,
                    x.ShippingLeadTimeIsCalendarDays,
                    // Descriptors
                    x.ShortDescription,
                    x.ManufacturerPartNumber,
                    x.BrandName,
                    x.TaxCode,
                    x.UnitOfMeasure,
                    x.SortOrder,
                    // Pricing & Fees
                    x.PriceBase,
                    x.PriceReduction,
                    x.PriceSale,
                    x.PriceMsrp,
                    x.HandlingCharge,
                    x.FlatShippingCharge,
                    x.RestockingFeePercent,
                    x.RestockingFeeAmount,
                    // Availability, Stock, Shipping Requirements
                    x.AvailableStartDate,
                    x.AvailableEndDate,
                    x.PreSellEndDate,
                    StockQuantity = x.IsUnlimitedStock || x.Type!.Name == "Variant Master"
                        ? null
                        : x.StockQuantity,
                    StockQuantityAllocated = x.IsUnlimitedStock || x.Type!.Name == "Variant Master"
                        ? null
                        : x.StockQuantityAllocated,
                    x.QuantityPerMasterPack,
                    x.QuantityMasterPackPerLayer,
                    x.QuantityMasterPackLayersPerPallet,
                    x.QuantityMasterPackPerPallet,
                    x.QuantityPerLayer,
                    x.QuantityLayersPerPallet,
                    x.QuantityPerPallet,
                    x.KitBaseQuantityPriceMultiplier,
                    x.ShippingLeadTimeDays,
                    x.RequiresRoles,
                    x.RequiresRolesAlt,
                    // Min/Max Purchase Per Order
                    x.MinimumPurchaseQuantity,
                    x.MinimumPurchaseQuantityIfPastPurchased,
                    x.MaximumPurchaseQuantity,
                    x.MaximumPurchaseQuantityIfPastPurchased,
                    x.MaximumBackOrderPurchaseQuantity,
                    x.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                    x.MaximumBackOrderPurchaseQuantityGlobal,
                    x.MaximumPrePurchaseQuantity,
                    x.MaximumPrePurchaseQuantityIfPastPurchased,
                    x.MaximumPrePurchaseQuantityGlobal,
                    // Required Document
                    x.DocumentRequiredForPurchase,
                    x.DocumentRequiredForPurchaseMissingWarningMessage,
                    x.DocumentRequiredForPurchaseExpiredWarningMessage,
                    x.DocumentRequiredForPurchaseOverrideFee,
                    x.DocumentRequiredForPurchaseOverrideFeeAcceptedMessage,
                    x.DocumentRequiredForPurchaseOverrideFeeIsPercent,
                    x.DocumentRequiredForPurchaseOverrideFeeWarningMessage,
                    // Must Purchase In Multiples Of
                    x.MustPurchaseInMultiplesOfAmount,
                    x.MustPurchaseInMultiplesOfAmountWarningMessage,
                    x.MustPurchaseInMultiplesOfAmountOverrideFee,
                    x.MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent,
                    x.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage,
                    x.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage,
                    // Analytics filled data
                    x.TotalPurchasedAmount,
                    x.TotalPurchasedAmountCurrencyID,
                    x.TotalPurchasedQuantity,
                    // Convenience Properties
                    PrimaryImageFileName = x.Images!
                        .Where(img => img.Active)
                        .OrderByDescending(img => img.IsPrimary)
                        .ThenByDescending(img => img.OriginalWidth)
                        .Take(1)
                        .Select(img => img.ThumbnailFileName ?? img.OriginalFileName)
                        .FirstOrDefault(),
                    // Related Objects
                    x.TypeID,
                    x.StatusID,
                    x.PackageID,
                    x.MasterPackID,
                    x.PalletID,
                    Type = new
                    {
                        x.Type!.CustomKey,
                        x.Type.Name,
                        x.Type.DisplayName,
                        x.Type.SortOrder,
                    },
                    Status = new
                    {
                        x.Status!.CustomKey,
                        x.Status.Name,
                        x.Status.DisplayName,
                        x.Status.SortOrder,
                    },
                    Package = x.Package == null ? null : new { x.Package.CustomKey, x.Package.Name, },
                    MasterPack = x.MasterPack == null ? null : new { x.MasterPack.CustomKey, x.MasterPack.Name, },
                    Pallet = x.Pallet == null ? null : new { x.Pallet.CustomKey, x.Pallet.Name, },
                    // Associated Objects
                    ////x.JsonAttributes,
                    // SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                });
            var query4 = (await query3.ToListAsync().ConfigureAwait(false))
                .Select(x => new ProductModel
                {
                    // Base Properties
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    Active = x.Active,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    Hash = x.Hash,
                    // NameableBase Properties
                    Name = x.Name,
                    ////Description = x.Description,
                    // IHaveSeoBase Properties
                    SeoUrl = x.SeoUrl,
                    SeoPageTitle = x.SeoPageTitle,
                    SeoKeywords = x.SeoKeywords,
                    SeoDescription = x.SeoDescription,
                    SeoMetaData = x.SeoMetaData,
                    // IHaveNullableDimensions Properties
                    Weight = x.Weight,
                    WeightUnitOfMeasure = x.WeightUnitOfMeasure,
                    Width = x.Width,
                    WidthUnitOfMeasure = x.WidthUnitOfMeasure,
                    Depth = x.Depth,
                    DepthUnitOfMeasure = x.DepthUnitOfMeasure,
                    Height = x.Height,
                    HeightUnitOfMeasure = x.HeightUnitOfMeasure,
                    // Flags/Toggles
                    IsVisible = x.IsVisible,
                    IsDiscontinued = x.IsDiscontinued,
                    IsEligibleForReturn = x.IsEligibleForReturn,
                    IsTaxable = x.IsTaxable,
                    AllowBackOrder = x.AllowBackOrder,
                    AllowPreSale = x.AllowPreSale,
                    IsUnlimitedStock = x.IsUnlimitedStock,
                    IsFreeShipping = x.IsFreeShipping,
                    NothingToShip = x.NothingToShip,
                    ShippingLeadTimeIsCalendarDays = x.ShippingLeadTimeIsCalendarDays,
                    // Descriptors
                    ShortDescription = x.ShortDescription,
                    ManufacturerPartNumber = x.ManufacturerPartNumber,
                    BrandName = x.BrandName,
                    TaxCode = x.TaxCode,
                    UnitOfMeasure = x.UnitOfMeasure ?? "EACH",
                    SortOrder = x.SortOrder,
                    // Fees
                    HandlingCharge = x.HandlingCharge,
                    FlatShippingCharge = x.FlatShippingCharge,
                    RestockingFeePercent = x.RestockingFeePercent,
                    RestockingFeeAmount = x.RestockingFeeAmount,
                    // Availability, Shipping Requirements
                    AvailableStartDate = x.AvailableStartDate,
                    AvailableEndDate = x.AvailableEndDate,
                    PreSellEndDate = x.PreSellEndDate,
                    QuantityPerMasterPack = x.QuantityPerMasterPack,
                    QuantityMasterPackPerLayer = x.QuantityMasterPackPerLayer,
                    QuantityMasterPackLayersPerPallet = x.QuantityMasterPackLayersPerPallet,
                    QuantityMasterPackPerPallet = x.QuantityMasterPackPerPallet,
                    QuantityPerLayer = x.QuantityPerLayer,
                    QuantityLayersPerPallet = x.QuantityLayersPerPallet,
                    QuantityPerPallet = x.QuantityPerPallet,
                    KitBaseQuantityPriceMultiplier = x.KitBaseQuantityPriceMultiplier ?? 1m,
                    ShippingLeadTimeDays = x.ShippingLeadTimeDays,
                    RequiresRoles = x.RequiresRoles,
                    RequiresRolesAlt = x.RequiresRolesAlt,
                    // Min/Max Purchase Per Order
                    MinimumPurchaseQuantity = x.MinimumPurchaseQuantity,
                    MinimumPurchaseQuantityIfPastPurchased = x.MinimumPurchaseQuantityIfPastPurchased,
                    MaximumPurchaseQuantity = x.MaximumPurchaseQuantity,
                    MaximumPurchaseQuantityIfPastPurchased = x.MaximumPurchaseQuantityIfPastPurchased,
                    MaximumBackOrderPurchaseQuantity = x.MaximumBackOrderPurchaseQuantity,
                    MaximumBackOrderPurchaseQuantityIfPastPurchased = x.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                    MaximumBackOrderPurchaseQuantityGlobal = x.MaximumBackOrderPurchaseQuantityGlobal,
                    MaximumPrePurchaseQuantity = x.MaximumPrePurchaseQuantity,
                    MaximumPrePurchaseQuantityIfPastPurchased = x.MaximumPrePurchaseQuantityIfPastPurchased,
                    MaximumPrePurchaseQuantityGlobal = x.MaximumPrePurchaseQuantityGlobal,
                    // Required Document
                    DocumentRequiredForPurchase = x.DocumentRequiredForPurchase,
                    DocumentRequiredForPurchaseMissingWarningMessage = x.DocumentRequiredForPurchaseMissingWarningMessage,
                    DocumentRequiredForPurchaseExpiredWarningMessage = x.DocumentRequiredForPurchaseExpiredWarningMessage,
                    DocumentRequiredForPurchaseOverrideFee = x.DocumentRequiredForPurchaseOverrideFee,
                    DocumentRequiredForPurchaseOverrideFeeAcceptedMessage = x.DocumentRequiredForPurchaseOverrideFeeAcceptedMessage,
                    DocumentRequiredForPurchaseOverrideFeeIsPercent = x.DocumentRequiredForPurchaseOverrideFeeIsPercent,
                    DocumentRequiredForPurchaseOverrideFeeWarningMessage = x.DocumentRequiredForPurchaseOverrideFeeWarningMessage,
                    // Must Purchase In Multiples Of
                    MustPurchaseInMultiplesOfAmount = x.MustPurchaseInMultiplesOfAmount,
                    MustPurchaseInMultiplesOfAmountWarningMessage = x.MustPurchaseInMultiplesOfAmountWarningMessage,
                    MustPurchaseInMultiplesOfAmountOverrideFee = x.MustPurchaseInMultiplesOfAmountOverrideFee,
                    MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent = x.MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent,
                    MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage = x.MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage,
                    MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage = x.MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage,
                    // Analytics filled data
                    TotalPurchasedAmount = x.TotalPurchasedAmount,
                    TotalPurchasedAmountCurrencyID = x.TotalPurchasedAmountCurrencyID,
                    TotalPurchasedQuantity = x.TotalPurchasedQuantity,
                    // Convenience Properties
                    PrimaryImageFileName = x.PrimaryImageFileName,
                    // Related Objects
                    TypeID = x.TypeID,
                    StatusID = x.StatusID,
                    PackageID = x.PackageID,
                    MasterPackID = x.MasterPackID,
                    PalletID = x.PalletID,
                    TypeKey = x.Type.CustomKey,
                    TypeName = x.Type.Name,
                    TypeDisplayName = x.Type.DisplayName,
                    TypeSortOrder = x.Type.SortOrder,
                    StatusKey = x.Status.CustomKey,
                    StatusName = x.Status.Name,
                    StatusDisplayName = x.Status.DisplayName,
                    StatusSortOrder = x.Status.SortOrder,
                    PackageKey = x.Package?.CustomKey,
                    PackageName = x.Package?.Name,
                    MasterPackKey = x.MasterPack?.CustomKey,
                    MasterPackName = x.MasterPack?.Name,
                    PalletKey = x.Pallet?.CustomKey,
                    PalletName = x.Pallet?.Name,
                    // Associated Objects
                    ////SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                });
            results.results = query4.ToList<IProductModel>();
            return results;
        }

        /// <inheritdoc/>
        public async Task<List<IProductModel>> SearchRecentlyViewedProductsAsync(
            List<int> productIDs,
            string? contextProfileName)
        {
            if (productIDs?.Any() != true)
            {
                return new();
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await Task.WhenAll(
                context.Products
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByIDs(productIDs.ToArray())
                    .AsEnumerable()
                    .Select(p => p.ToProductCatalogItemAltAsync(
                        this,
                        true, ////!CEFConfigDictionary.SearchingProductIndexResultsIncludeAssociatedProducts,
                        contextProfileName)))
                    .ConfigureAwait(false))
                .ToList()!;
        }

        /// <inheritdoc/>
        public async Task<(List<IProductModel> results, int totalPages, int totalCount)> SearchPreviouslyOrderedAsync(
            IProductSearchModel search,
            bool asListing,
            string? contextProfileName)
        {
            var (query, totalPages, totalCount) = await SearchPreviouslyOrderedCoreAsync(
                    Contract.RequiresNotNull(search),
                    contextProfileName)
                .ConfigureAwait(false);
            return (
                asListing
                    ? query.OrderBy(x => x.Name).Select(ModelMapperForProduct.MapListingProductOld).ToList()
                    : query.OrderBy(x => x.Name).Select(ModelMapperForProduct.MapLiteProductOld).ToList(),
                totalPages,
                totalCount);
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetLatestProductsLastModifiedAsync(
            int days,
            string? categorySeoUrl,
            string? contextProfileName)
        {
            var timeLimit = DateTime.Today.AddDays(-days);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByCategorySeoUrl(categorySeoUrl)
                .Where(x => x.CreatedDate >= timeLimit)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .OrderByDescending(x => x)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<IProductModel>> GetLatestProductsAsync(
            int count,
            int days,
            IPricingFactoryContextModel pricingFactoryContext,
            string? categorySeoUrl,
            string? contextProfileName)
        {
            var timeLimit = DateTime.Today.AddDays(-days);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByCategorySeoUrl(categorySeoUrl)
                .Where(x => x.CreatedDate >= timeLimit)
                .OrderBy(x => x.Name)
                .Take(count)
                .Select(ModelMapperForProduct.MapLiteProductOld)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetUpdatedProductsLastModifiedAsync(
            int days,
            string categorySeoUrl,
            string? contextProfileName)
        {
            var timeLimit = DateTime.Today.AddDays(-days);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByCategorySeoUrl(categorySeoUrl)
                .Where(x => x.UpdatedDate >= timeLimit)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .OrderByDescending(x => x)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<IProductModel>> GetUpdatedProductsAsync(
            int days,
            IPricingFactoryContextModel pricingFactoryContext,
            string? categorySeoUrl,
            string? contextProfileName)
        {
            var timeLimit = DateTime.Today.AddDays(-days);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByCategorySeoUrl(categorySeoUrl)
                .Where(x => x.UpdatedDate >= timeLimit)
                .OrderBy(x => x.Name)
                .Select(ModelMapperForProduct.MapLiteProductOld)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetCustomersFavoriteProductsLastModifiedAsync(
            int days,
            string? categorySeoUrl,
            string? contextProfileName)
        {
            // TODO: Cache the hash so it only has to run periodically
            var hash = new Dictionary<int, int>();
            var timeLimit = DateTime.Today.AddDays(-days);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var oi in context.SalesOrders
                .AsNoTracking()
                .Where(x => x.CreatedDate >= timeLimit)
                .SelectMany(x => x.SalesItems!)
                .Where(x => x.ProductID.HasValue)
                .Select(x => new { ProductID = x.ProductID!.Value }))
            {
                if (!hash.ContainsKey(oi.ProductID))
                {
                    hash[oi.ProductID] = 0;
                }
                hash[oi.ProductID]++; // count, not sum
            }
            return await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByCategorySeoUrl(categorySeoUrl)
                .FilterByIDs(hash.OrderByDescending(x => x.Value).Select(x => x.Key))
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .OrderByDescending(x => x)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<IProductModel>> GetCustomersFavoriteProductsAsync(
            int count,
            int days,
            IPricingFactoryContextModel pricingFactoryContext,
            string? categorySeoUrl,
            string? contextProfileName)
        {
            var hash = new Dictionary<int, int>();
            var timeLimit = DateTime.Today.AddDays(-days);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var oi in context.SalesOrders
                .AsNoTracking()
                .Where(so => so.CreatedDate >= timeLimit)
                .SelectMany(so => so.SalesItems!)
                .Where(soi => soi.ProductID.HasValue)
                .Select(soi => new { ProductID = soi.ProductID!.Value }))
            {
                if (!hash.ContainsKey(oi.ProductID))
                {
                    hash[oi.ProductID] = 0;
                }
                hash[oi.ProductID]++; // count, not sum
            }
            return context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByCategorySeoUrl(categorySeoUrl)
                .FilterByIDs(hash.OrderByDescending(x => x.Value).Select(x => x.Key).Take(count))
                .Take(count)
                .Select(ModelMapperForProduct.MapLiteProductOld)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetBestSellingProductsLastModifiedAsync(
            int days,
            string? categorySeoUrl,
            string? contextProfileName)
        {
            // TODO: Cache the hash so it only has to run periodically
            var hash = new Dictionary<int, decimal>();
            var timeLimit = DateTime.Today.AddDays(-days);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var oi in context.SalesOrders
                .AsNoTracking()
                .Where(so => so.CreatedDate >= timeLimit)
                .SelectMany(so => so.SalesItems!)
                .Where(soi => soi.ProductID.HasValue)
                .Select(soi => new
                {
                    ProductID = soi.ProductID!.Value,
                    Quantity = soi.Quantity + (soi.QuantityBackOrdered ?? 0m) + (soi.QuantityPreSold ?? 0m),
                }))
            {
                if (!hash.ContainsKey(oi.ProductID))
                {
                    hash[oi.ProductID] = 0;
                }
                hash[oi.ProductID] += oi.Quantity; // sum, not count
            }
            return await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByCategorySeoUrl(categorySeoUrl)
                .FilterByIDs(hash.OrderByDescending(x => x.Value).Select(x => x.Key).ToList())
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .OrderByDescending(x => x)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<IProductModel>> GetBestSellingProductsAsync(
            int count,
            int days,
            IPricingFactoryContextModel pricingFactoryContext,
            string? categorySeoUrl,
            string? contextProfileName)
        {
            var hash = new Dictionary<int, decimal>();
            var timeLimit = DateTime.Today.AddDays(-days);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var oi in context.SalesOrders
                .AsNoTracking()
                .Where(so => so.CreatedDate >= timeLimit)
                .SelectMany(so => so.SalesItems!)
                .Where(soi => soi.ProductID.HasValue)
                .Select(soi => new
                {
                    ProductID = soi.ProductID!.Value,
                    Quantity = soi.Quantity + (soi.QuantityBackOrdered ?? 0m) + (soi.QuantityPreSold ?? 0m),
                }))
            {
                if (!hash.ContainsKey(oi.ProductID))
                {
                    hash[oi.ProductID] = 0;
                }
                hash[oi.ProductID] += oi.Quantity; // sum, not count
            }
            return context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByCategorySeoUrl(categorySeoUrl)
                .FilterByIDs(hash.OrderByDescending(x => x.Value).Select(x => x.Key).Take(count))
                .Take(count)
                .Select(ModelMapperForProduct.MapLiteProductOld)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetTrendingProductsLastModifiedAsync(
            int days,
            string? categorySeoUrl,
            string? contextProfileName)
        {
            // TODO: Cache the hash so it only has to run periodically
            var hash = new Dictionary<int, decimal>();
            var timeLimit = DateTime.Today.AddDays(-days);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var oi in context.SalesOrders
                .AsNoTracking()
                .Where(so => so.CreatedDate >= timeLimit)
                .SelectMany(so => so.SalesItems!)
                .Where(soi => soi.ProductID.HasValue)
                .Select(soi => new
                {
                    ProductID = soi.ProductID!.Value,
                    Quantity = soi.Quantity + (soi.QuantityBackOrdered ?? 0m) + (soi.QuantityPreSold ?? 0m),
                }))
            {
                if (!hash.ContainsKey(oi.ProductID))
                {
                    hash[oi.ProductID] = 0;
                }
                hash[oi.ProductID] += oi.Quantity; // sum, not count
            }
            return await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByCategorySeoUrl(categorySeoUrl)
                .FilterByIDs(hash.OrderByDescending(x => x.Value).Select(x => x.Key))
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .OrderByDescending(x => x)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<IProductModel>> GetTrendingProductsAsync(
            int count,
            int days,
            IPricingFactoryContextModel pricingFactoryContext,
            string? categorySeoUrl,
            string? contextProfileName)
        {
            var hash = new Dictionary<int, decimal>();
            var timeLimit = DateTime.Today.AddDays(-days);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var oi in context.SalesOrders
                .AsNoTracking()
                .Where(so => so.CreatedDate >= timeLimit)
                .SelectMany(so => so.SalesItems!)
                .Where(soi => soi.ProductID.HasValue)
                .Select(soi => new
                {
                    ProductID = soi.ProductID!.Value,
                    Quantity = soi.Quantity + (soi.QuantityBackOrdered ?? 0m) + (soi.QuantityPreSold ?? 0m),
                }))
            {
                if (!hash.ContainsKey(oi.ProductID))
                {
                    hash[oi.ProductID] = 0;
                }
                hash[oi.ProductID] += oi.Quantity; // sum, not count
            }
            return context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByCategorySeoUrl(categorySeoUrl)
                .FilterByIDs(hash.OrderByDescending(x => x.Value).Select(x => x.Key).Take(count))
                .Take(count)
                .Select(ModelMapperForProduct.MapLiteProductOld)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<List<IProductModel>> GetAllActiveAsListingAsync(string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .OrderBy(x => x.Name)
                .Take(1000)
                .Select(ModelMapperForProduct.MapListingProductOld)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<IQuickOrderFormProductsModel?> GetProductsByCategoryAsync(
            List<int>? productTypes,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            // Get all categories {ID, NAME}
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var categories = await context.Categories
                .AsNoTracking()
                .Where(c => c.IsVisible)
                .Select(c => new { c.ID, c.Name, c.SortOrder })
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .ToListAsync()
                .ConfigureAwait(false);
            if (categories.Count == 0)
            {
                return null;
            }
            var list = new List<ProductByCategoryModel>();
            list.AddRange(
                from category in categories
                let productIDs = context.ProductCategories
                    .AsNoTracking()
                    .FilterByActive(true)
                    .Where(x => x.SlaveID == category.ID
                        && x.Master!.Active
                        && x.Master.IsVisible)
                    .Select(x => x.MasterID)
                    .Distinct()
                    .ToList()
                where productIDs.Count != 0
                select new ProductByCategoryModel
                {
                    CategoryID = category.ID,
                    CategoryName = category.Name,
                    SortOrder = category.SortOrder,
                    Products = context.Products
                            .FilterByIDs(productIDs)
                            .Select(x => new
                            {
                                x.ID,
                                x.CustomKey,
                                x.Active,
                                x.CreatedDate,
                                x.Name,
                                x.ManufacturerPartNumber,
                                x.BrandName,
                                x.SeoUrl,
                                x.TypeID,
                                x.StatusID,
                                x.JsonAttributes,
                                PrimaryImageFileName = x.Images!
                                    .Where(y => y.Active)
                                    .OrderByDescending(y => y.IsPrimary)
                                    .ThenByDescending(y => y.OriginalWidth)
                                    .Select(y => y.ThumbnailFileName ?? y.OriginalFileName)
                                    .FirstOrDefault(),
                            })
                            .ToList()
                        .Select(x => new ProductModel
                        {
                            ID = x.ID,
                            CustomKey = x.CustomKey,
                            Active = x.Active,
                            CreatedDate = x.CreatedDate,
                            Name = x.Name,
                            ManufacturerPartNumber = x.ManufacturerPartNumber,
                            BrandName = x.BrandName,
                            IsVisible = true,
                            SeoUrl = x.SeoUrl,
                            TypeID = x.TypeID,
                            StatusID = x.StatusID,
                            SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                            PrimaryImageFileName = x.PrimaryImageFileName,
                        })
                        .ToList(),
                });
            return new QuickOrderFormProductsModel
            {
                Headers = new(),
                ProductsByCategory = list,
            };
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetKeyWordsAsync(string? term, List<string>? types, string? contextProfileName)
        {
            const int MaxTake = 30;
            var results = new List<string>();
            /*Set default if no type is submitted*/
            if (types == null || types.Count == 0)
            {
                types = new() { "product" };
            }
            if (string.IsNullOrWhiteSpace(term))
            {
                return results.OrderBy(t => t).Take(MaxTake).ToList();
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var type in types)
            {
                switch (type.ToLower())
                {
                    case "product":
                    {
                        results.AddRange(context.Products
                            .AsNoTracking()
                            .Where(x => x.Active && x.IsVisible && x.Name!.Contains(term!))
                            .Select(x => x.Name)
                            .Take(MaxTake)!);
                        break;
                    }
                    case "product_code":
                    {
                        results.AddRange(context.Products
                            .AsNoTracking()
                            .Where(x => x.Active && x.IsVisible && x.CustomKey!.Contains(term!))
                            .Select(x => x.CustomKey)
                            .Take(MaxTake)!);
                        break;
                    }
                    default:
                    {
                        results.AddRange(context.Categories
                            .AsNoTracking()
                            .Where(c => c.Name!.Contains(term!) && c.Type!.Name == type)
                            .Take(30)
                            .Select(c => c.Name)!);
                        break;
                    }
                }
            }
            return results.OrderBy(t => t).Take(MaxTake).ToList();
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetPrimaryImageLastModifiedAsync(int productID, string? contextProfileName)
        {
            Contract.RequiresValidID(productID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Products
                .AsNoTracking()
                .FilterByID(productID)
                .SelectMany(x => x.Images!.Where(y => y.Active))
                .OrderByDescending(x => x.IsPrimary)
                .ThenByDescending(x => x.OriginalWidth)
                .ThenByDescending(x => x.OriginalHeight)
                .Take(1)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IProductImageModel?> GetPrimaryImageAsync(int productID, string? contextProfileName)
        {
            Contract.RequiresValidID(productID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.Products
                .AsNoTracking()
                .FilterByID(productID)
                .SelectMany(x => x.Images!.Where(y => y.Active))
                .OrderByDescending(x => x.IsPrimary)
                .ThenByDescending(x => x.OriginalWidth)
                .ThenByDescending(x => x.OriginalHeight)
                .Take(1)
                .SelectFirstFullProductImageAndMapToProductImageModel(contextProfileName);
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetProductReviewInformationLastModifiedAsync(
            int productID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Reviews
                .FilterByActive(true)
                .Where(x => x.Approved && x.ProductID == productID && x.Product!.Active)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .OrderByDescending(x => x)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IProductReviewInformationModel> GetProductReviewInformationAsync(
            int productID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var reviews = context.Reviews
                .Where(x => x.Active && x.Approved && x.ProductID == productID && x.Product!.Active)
                .SelectListReviewAndMapToReviewModel(contextProfileName)
                .ToList();
            var retVal = new ProductReviewInformationModel
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime,
                ProductID = productID,
            };
            if (reviews.Count > 0)
            {
                retVal.Count = reviews.Count;
                retVal.Value = reviews.Where(x => x.Value > 0).Select(x => x.Value).DefaultIfEmpty(0).Average();
                retVal.Reviews = reviews.Cast<ReviewModel>().ToList();
            }
            else
            {
                retVal.Count = 0;
                retVal.Value = 0;
                retVal.Reviews = new();
            }
            return retVal;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateProductReviewAsync(IReviewModel model, string? contextProfileName)
        {
            Contract.RequiresNotNull(model);
            var timestamp = DateExtensions.GenDateTime;
            var review = RegistryLoaderWrapper.GetInstance<Review>(contextProfileName);
            review.Value = model.Value;
            review.Comment = model.Comment;
            review.CreatedDate = timestamp;
            review.ApprovedDate = timestamp;
            review.Approved = true;
            review.Active = true;
            review.Title = model.Title;
            review.Name = model.Name;
            review.TypeID = model.TypeID;
            review.SubmittedByUserID = model.SubmittedByUserID;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            context.Reviews.Add(review);
            return await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<IProductInventoryLocationSectionModel>> GetInventoryLocationHistoryAsync(
            int productID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // Specifically includes Active=False entries
            return context.ProductInventoryLocationSections
                .AsNoTracking()
                .FilterPILSByProductID(Contract.RequiresValidID(productID))
                .SelectLiteProductInventoryLocationSectionAndMapToProductInventoryLocationSectionModel(contextProfileName)
                .ToList();
        }

        /// <inheritdoc/>
        public Task<bool> IsShippingRestrictedAsync(
            SerializableAttributesDictionary? attributes,
            string? state,
            string? city)
        {
            return Task.FromResult(false);
        }

        /// <inheritdoc/>
        public virtual async Task<SerializableAttributesDictionary> GetProductMetadataBySeoUrlAsync(
            string seoUrl,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetProductMetadataBySeoUrlAsync(seoUrl, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<SerializableAttributesDictionary> GetProductMetadataByIDAsync(
            int id,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetProductMetadataByIDAsync(id, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IMinMaxCalculatedPrices?> CalculatePriceRangeForVariantsAsync(
            int productID,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var variantIDs = await context.ProductAssociations
                .AsNoTracking()
                .FilterByActive(true)
                .FilterIAmARelationshipTableByMasterID<ProductAssociation, Product, Product>(productID)
                .FilterByTypeKey<ProductAssociation, ProductAssociationType>("VARIANT-OF-MASTER")
                .Select(x => x.SlaveID)
                .ToListAsync()
                .ConfigureAwait(false);
            if (!variantIDs.Any())
            {
                return null;
            }
            var results = await variantIDs
                .ForEachAsync(
                    4,
                    variantID => Workflows.PricingFactory.CalculatePriceAsync(
                        variantID,
                        null,
                        pricingFactoryContext,
                        contextProfileName))
                .ConfigureAwait(false);
            var min = results
                .Where(x => x.Value.IsValid)
                .MinBy(x => x.Value.SalePrice ?? x.Value.BasePrice)
                .FirstOrDefault();
            var max = results
                .Where(x => x.Value.IsValid)
                .MaxBy(x => x.Value.SalePrice ?? x.Value.BasePrice)
                .FirstOrDefault();
            return new IMinMaxCalculatedPrices(min.Value, max.Value);
        }

        /// <summary>Gets product metadata by seo URL.</summary>
        /// <param name="seoUrl"> URL of the seo.</param>
        /// <param name="context">The context.</param>
        /// <returns>The product metadata by seo URL.</returns>
        protected virtual Task<SerializableAttributesDictionary> GetProductMetadataBySeoUrlAsync(
            string seoUrl,
            IClarityEcommerceEntities context)
        {
            return GetProductMetadataByQueryAsync(
                context.Products.FilterByActive(true).FilterBySeoUrl(seoUrl, true, false),
                false,
                context.ContextProfileName);
        }

        /// <summary>Gets product metadata by identifier.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="context">The context.</param>
        /// <returns>The product metadata by identifier.</returns>
        protected virtual Task<SerializableAttributesDictionary> GetProductMetadataByIDAsync(
            int id,
            IClarityEcommerceEntities context)
        {
            return GetProductMetadataByQueryAsync(
                context.Products.FilterByActive(true).FilterByID(id),
                true,
                context.ContextProfileName);
        }

        /// <summary>Gets product metadata by query.</summary>
        /// <param name="query">             The query.</param>
        /// <param name="excludeRelated">    True to exclude, false to include the related.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The product metadata by query.</returns>
        protected virtual async Task<SerializableAttributesDictionary> GetProductMetadataByQueryAsync(
            IQueryable<Product> query,
            bool excludeRelated,
            string? contextProfileName)
        {
            var entity = (excludeRelated
                ? (await query
                        .Select(x => new
                        {
                            x.ID,
                            x.CustomKey,
                            x.Name,
                            x.ShortDescription,
                            x.SeoPageTitle,
                            x.SeoDescription,
                            x.SeoKeywords,
                            x.SeoUrl,
                            x.SeoMetaData,
                            x.PriceSale,
                            x.PriceBase,
                            Images = x.Images!.Where(y => y.Active).Select(y => y.OriginalFileName),
                        }
                        !)
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .Select(x => new
                    {
                        x.ID,
                        x.CustomKey,
                        x.Name,
                        x.ShortDescription,
                        x.SeoPageTitle,
                        x.SeoDescription,
                        x.SeoKeywords,
                        x.SeoUrl,
                        x.SeoMetaData,
                        x.PriceSale,
                        x.PriceBase,
                        RelatedProductIDs = new List<int>(),
                        Images = x.Images.ToList(),
                    }
                    !)
                : (await query
                        .Select(x => new
                        {
                            x.ID,
                            x.CustomKey,
                            x.Name,
                            x.ShortDescription,
                            x.SeoPageTitle,
                            x.SeoDescription,
                            x.SeoKeywords,
                            x.SeoUrl,
                            x.SeoMetaData,
                            x.PriceSale,
                            x.PriceBase,
                            RelatedProductIDs = x.ProductAssociations!
                                .Where(y => y.Active && y.Slave!.Active && y.Type!.CustomKey == "VARIANT-OF-MASTER")
                                .Select(y => y.SlaveID),
                            Images = x.Images!.Where(y => y.Active).Select(y => y.OriginalFileName),
                        }
                        !)
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .Select(x => new
                    {
                        x.ID,
                        x.CustomKey,
                        x.Name,
                        x.ShortDescription,
                        x.SeoPageTitle,
                        x.SeoDescription,
                        x.SeoKeywords,
                        x.SeoUrl,
                        x.SeoMetaData,
                        x.PriceSale,
                        x.PriceBase,
                        RelatedProductIDs = x.RelatedProductIDs.ToList(),
                        Images = x.Images.ToList(),
                    }
                    !))
                .SingleOrDefault();
            if (entity == null)
            {
                return new();
            }
            SerializableAttributesDictionary dict = new()
            {
                ["ID"] = new()
                {
                    Key = "ID",
                    Value = entity.ID.ToString(),
                },
                ["CustomKey"] = new()
                {
                    Key = "CustomKey",
                    Value = entity.CustomKey!,
                },
                ["Name"] = new()
                {
                    Key = "Name",
                    Value = entity.Name!,
                },
                ["SeoPageTitle"] = new()
                {
                    Key = "SeoPageTitle",
                    Value = entity.SeoPageTitle!,
                },
                ["SeoDescription"] = new()
                {
                    Key = "SeoDescription",
                    Value = entity.SeoDescription!,
                },
                ["SeoShortDescription"] = new()
                {
                    Key = "SeoShortDescription",
                    Value = entity.ShortDescription!,
                },
                ["SeoKeywords"] = new()
                {
                    Key = "SeoKeywords",
                    Value = entity.SeoKeywords!,
                },
                ["SeoUrl"] = new()
                {
                    Key = "SeoUrl",
                    Value = entity.SeoUrl!,
                },
                ["SeoMetaData"] = new()
                {
                    Key = "SeoMetaData",
                    Value = entity.SeoMetaData!,
                },
                ["PriceSale"] = new()
                {
                    Key = "PriceSale",
                    Value = entity.PriceSale?.ToString("n4")!,
                },
                ["PriceBase"] = new()
                {
                    Key = "PriceBase",
                    Value = entity.PriceBase?.ToString("n4")!,
                },
                ["imageUrls"] = new()
                {
                    Key = "imageUrls",
                    Value = entity.Images.DefaultIfEmpty(string.Empty).Aggregate((c, n) => c + "," + n)!,
                },
            };
            // ReSharper disable once InvertIf
            if (Contract.CheckNotEmpty(entity.RelatedProductIDs))
            {
                var relatedData = await Task.WhenAll(
                    entity.RelatedProductIDs
                        .Select(x => GetProductMetadataByIDAsync(
                            x,
                            contextProfileName)));
                foreach (var related in relatedData
                    .Where(x => !x.ContainsKey("SeoUrl") || !Contract.CheckValidKey(x["SeoUrl"].Value)))
                {
                    related["SeoUrl"] = new()
                    {
                        Key = "SeoUrl",
                        Value = entity.SeoUrl!,
                    };
                }
                dict["RelatedProducts"] = new()
                {
                    Key = "RelatedProducts",
                    Value = JsonConvert.SerializeObject(relatedData),
                };
            }
            return dict;
        }

        /// <summary>Gets the attribute keys that should be hidden in this collection.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process the attribute keys that should be hidden in
        /// this collection.</returns>
        private static async Task<List<string>> GetAttributeKeysThatShouldBeHiddenAsync(string? contextProfileName)
        {
            if (Contract.CheckValidKey(contextProfileName))
            {
                // Don't run in testing
                return new();
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await context.GeneralAttributes
                .AsNoTracking()
                .FilterByActive(true)
                .FilterGeneralAttributesByHideFromStorefront(true)
                .Select(x => x.CustomKey)
                .Distinct()
                .Where(x => x != null && x != string.Empty)
                .ToListAsync()
                .ConfigureAwait(false))!;
        }

        /// <summary>Searches for the first previously ordered core.</summary>
        /// <param name="search">            The search model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The found previously ordered core.</returns>
        private static async Task<(IQueryable<Product> results, int totalPages, int totalCount)> SearchPreviouslyOrderedCoreAsync(
            IProductSearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Products
                .FilterByNameableBaseSearchModel(Contract.RequiresNotNull(search))
                .FilterByIDs(search.ProductIDs)
                .FilterByHaveATypeSearchModel<Product, ProductType>(search)
                .FilterIAmFilterableByMasterBrandsBySearchModel<Product, BrandProduct>(search)
                .FilterIAmFilterableByMasterStoresBySearchModel<Product, StoreProduct>(search)
                .FilterBySeoUrl(search.SeoUrl, search.SeoUrlStrict, search.SeoUrlIncludeNull)
                .FilterProductsByNothingToShip(search.NothingToShip)
                .FilterProductsByPrice(search.Price)
                .FilterProductsByTypeNames(search.TypeNames)
                .FilterProductsByCategorySeoUrl(search.CategorySeoUrl)
                .FilterProductsByIsVisible(search.IsVisible)
                .FilterProductsByIsDiscontinued(search.IsDiscontinued)
                .FilterProductsByVendorID(search.VendorID)
                .FilterProductsByCategoryID(search.CategoryID)
                .FilterProductsByCategoryJsonAttributesByValues(search.CategoryJsonAttributes)
                .FilterProductsByAncestorCategoryIDs(search.ParentCategories)
                .FilterProductsByBrandCategoryIDs<Product, BrandProduct>(search.BrandCategoryIDs);
            return ((await FilterByCategoryNameAsync(
                            search,
                            await FilterByKeywordsAsync(query, search.Keywords).ConfigureAwait(false),
                            context)
                        .ConfigureAwait(false))
                    .FilterByModifiedSince(search.ModifiedSince)
                    .ApplySorting(search.Sorts, search.Groupings, contextProfileName)
                    .FilterByPaging(search.Paging, out var totalPages, out var totalCount),
                totalPages,
                totalCount)!;
        }

        /// <summary>Filter by category masks.</summary>
        /// <param name="search"> The search model.</param>
        /// <param name="query">  The query.</param>
        /// <param name="context">The context.</param>
        /// <returns>An IQueryable{Product}.</returns>
        private static async Task<IQueryable<Product>> FilterByCategoryNameAsync(
            IProductSearchModel search,
            IQueryable<Product> query,
            IClarityEcommerceEntities context)
        {
            if (string.IsNullOrWhiteSpace(Contract.RequiresNotNull(search).CategoryName))
            {
                return query;
            }
            var prodIDs = new List<int>();
            var matchedCategory = await context.Categories
                .AsNoTracking()
                .FilterByName(search.CategoryName, true)
                .FirstOrDefaultAsync();
            if (matchedCategory != null)
            {
                prodIDs.AddRange(await GetProductIDsFromCategoriesAsync(matchedCategory).ConfigureAwait(false));
                return query.Where(p => prodIDs.Contains(p.ID));
            }
            foreach (var curCategory in context.Categories.AsNoTracking().FilterByActive(search.Active))
            {
                prodIDs.AddRange(await GetProductIDsFromCategoriesAsync(curCategory).ConfigureAwait(false));
            }
            return query.Where(p => prodIDs.Contains(p.ID));
        }

        /// <summary>Filter by keywords.</summary>
        /// <param name="query">   The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns>An IQueryable{Product}.</returns>
        private static Task<IQueryable<Product>> FilterByKeywordsAsync(
            IQueryable<Product> query,
            string? keywords)
        {
            if (!Contract.CheckValidKey(keywords))
            {
                return Task.FromResult(query);
            }
            var split = keywords!
                .ToLower()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();
            return Task.FromResult(
                Contract.RequiresNotNull(query)
                    .Where(p => split.Any(x => p.Description!.Contains(x))
                        || split.Any(x => p.ShortDescription!.Contains(x))
                        || split.Any(x => p.Name!.Contains(x))
                        || split.Any(x => p.CustomKey!.Contains(x))));
        }

        /// <summary>Finds all product ids based on the category entered.</summary>
        /// <param name="cat">The category.</param>
        /// <returns>A list of product ids.</returns>
        private static async Task<List<int>> GetProductIDsFromCategoriesAsync(ICategory cat)
        {
            var results = new List<int>();
            if (Contract.CheckNotEmpty(cat.Products))
            {
                results.AddRange(cat.Products!.Select(x => x.MasterID).ToList());
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckNotEmpty(cat.Children))
            {
                foreach (var productIDs in (await Task.WhenAll(
                            cat.Children!.Select(GetProductIDsFromCategoriesAsync))
                        .ConfigureAwait(false))
                    .Where(productIDs => productIDs.Count > 0))
                {
                    results.AddRange(productIDs);
                }
            }
            return results;
        }

        /// <summary>Gets a full.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="context">           The context.</param>
        /// <returns>The full.</returns>
        private async Task<IProductModel?> GetFullAsync(int? id, IClarityEcommerceEntities context)
        {
            if (Contract.CheckInvalidID(id))
            {
                return null;
            }
            await CleanProductsAsync(new[] { id!.Value }, context).ConfigureAwait(false);
            return context.Products
                .FilterByID(id.Value)
                .SelectSingleFullProductAndMapToProductModel(context.ContextProfileName);
        }

        /// <summary>Map the product with additional requirements.</summary>
        /// <param name="context">      The context.</param>
        /// <param name="productID">    Identifier for the product.</param>
        /// <param name="isVendorAdmin">The is vendor admin.</param>
        /// <param name="vendorAdminID">Identifier for the vendor admin.</param>
        /// <param name="previewID">    Identifier for the preview.</param>
        /// <returns>An IProductModel.</returns>
        private async Task<IProductModel?> MapProductForStorefrontAsync(
            IClarityEcommerceEntities context,
            int productID,
            bool? isVendorAdmin,
            int? vendorAdminID,
            int? previewID)
        {
            var model = ModelMapperForProduct.CreateProductModelFromEntityForStorefront(context, productID);
            if (model is null)
            {
                return null;
            }
            await HandleSKURestrictionsAsync(model).ConfigureAwait(false);
            model = await RemoveHiddenFromStorefrontAttributesAsync(model, context).ConfigureAwait(false);
            if (Contract.CheckValidID(previewID))
            {
                var recordVersion = context.RecordVersions
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(previewID!.Value)
                    .SelectFirstFullRecordVersionAndMapToRecordVersionModel(context.ContextProfileName);
                if (recordVersion == null)
                {
                    throw new InvalidOperationException(
                        "Cannot preview a record version that doesn't exist");
                }
                if (!Contract.CheckValidKey(recordVersion.SerializedRecord))
                {
                    throw new InvalidOperationException(
                        "Cannot preview a record version that doesn't have a serialized record");
                }
                IProductModel obj = JsonConvert.DeserializeObject<ProductModel>(recordVersion.SerializedRecord!)!;
                // Assign data that wasn't in the serialization
                // Base Properties
                obj.ID = model!.ID;
                obj.CustomKey = model.CustomKey;
                obj.CreatedDate = model.CreatedDate;
                obj.UpdatedDate = model.UpdatedDate;
                obj.Hash = model.Hash;
                // NameableBase Properties
                // IHaveSeoBase Properties
                // IHaveNullableDimensions Properties
                // Flags/Toggles
                // Descriptors
                // Pricing & Fees
                // Availability, Shipping Requirements
                // Min/Max Purchase Per Order
                // Required Document
                // Must Purchase In Multiples Of
                // Analytics filled data
                obj.TotalPurchasedAmount = model.TotalPurchasedAmount;
                obj.TotalPurchasedQuantity = model.TotalPurchasedQuantity;
                obj.TotalPurchasedAmountCurrency = model.TotalPurchasedAmountCurrency;
                obj.TotalPurchasedAmountCurrencyID = model.TotalPurchasedAmountCurrencyID;
                obj.TotalPurchasedAmountCurrencyKey = model.TotalPurchasedAmountCurrencyKey;
                obj.TotalPurchasedAmountCurrencyName = model.TotalPurchasedAmountCurrencyName;
                // Convenience Properties
                obj.PrimaryImageFileName = model.PrimaryImageFileName;
                // Related Objects
                var type = await Workflows.ProductTypes.GetAsync(obj.TypeID, context).ConfigureAwait(false);
                obj.Type = type;
                obj.TypeKey = type!.CustomKey;
                obj.TypeName = type.Name;
                obj.TypeDisplayName = type.DisplayName;
                obj.TypeTranslationKey = type.TranslationKey;
                obj.TypeSortOrder = type.SortOrder;
                var status = await Workflows.ProductStatuses.GetAsync(obj.StatusID, context).ConfigureAwait(false);
                obj.Status = status;
                obj.StatusKey = status!.CustomKey;
                obj.StatusName = status.Name;
                obj.StatusDisplayName = status.DisplayName;
                obj.StatusTranslationKey = status.TranslationKey;
                obj.StatusSortOrder = status.SortOrder;
                var package = Contract.CheckValidID(obj.PackageID)
                    ? await Workflows.Packages.GetAsync(obj.PackageID!.Value, context).ConfigureAwait(false)
                    : null;
                obj.Package = package;
                obj.PackageKey = package?.CustomKey;
                obj.PackageName = package?.Name;
                var masterPack = Contract.CheckValidID(obj.MasterPackID)
                    ? await Workflows.Packages.GetAsync(obj.MasterPackID!.Value, context).ConfigureAwait(false)
                    : null;
                obj.MasterPack = masterPack;
                obj.MasterPackKey = masterPack?.CustomKey;
                obj.MasterPackName = masterPack?.Name;
                var pallet = Contract.CheckValidID(obj.PalletID)
                    ? await Workflows.Packages.GetAsync(obj.PalletID!.Value, context).ConfigureAwait(false)
                    : null;
                obj.Pallet = pallet;
                obj.PalletKey = pallet?.CustomKey;
                obj.PalletName = pallet?.Name;
                // Associated Objects
                obj.ProductNotifications = model.ProductNotifications;
                obj.Reviews = model.Reviews;
                obj.VendorAdminID = model.VendorAdminID;
                obj.SerializableAttributes!["version"] = new()
                {
                    Value = recordVersion.Name!,
                };
                // Override what is being returned
                model = obj;
            }
            if (isVendorAdmin == true && Contract.CheckValidID(vendorAdminID))
            {
                if (Contract.CheckEmpty(model!.Vendors)
                    || !model.Vendors!.Any(x => x.Active && x.MasterID == vendorAdminID!.Value))
                {
                    throw new UnauthorizedAccessException("This product is not assigned to the current vendor");
                }
                model.Vendors = null; // Clear the list and it can't reassign it
            }
            if (!(model!.ProductAssociations?.Count > 0))
            {
                return model;
            }
            foreach (var assoc in model.ProductAssociations)
            {
                assoc.Slave = await RemoveHiddenFromStorefrontAttributesAsync(assoc.Slave!, context).ConfigureAwait(false);
            }
            return model;
        }

        /// <summary>Handles the SKU restrictions.</summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        private async Task HandleSKURestrictionsAsync(IProductModel model)
        {
            SerializableAttributeObject? modelSkuRestrictions = null;
            var isShippingRestricted = await IsShippingRestrictedAsync(
                    model.SerializableAttributes,
                    state: null,
                    city: null)
                .ConfigureAwait(false);
            if (isShippingRestricted)
            {
                modelSkuRestrictions = model.SerializableAttributes!["SKU-Restrictions"];
            }
            if (model.ProductAssociations == null || model.ProductAssociations.Count == 0)
            {
                return;
            }
            foreach (var ap in model.ProductAssociations)
            {
                if (!isShippingRestricted
                    || await IsShippingRestrictedAsync(
                            ap.Slave!.SerializableAttributes,
                            state: null,
                            city: null)
                        .ConfigureAwait(false))
                {
                    continue;
                }
                ap.Slave.SerializableAttributes!.TryAdd("SKU-Restrictions", modelSkuRestrictions!);
            }
        }
    }
}
