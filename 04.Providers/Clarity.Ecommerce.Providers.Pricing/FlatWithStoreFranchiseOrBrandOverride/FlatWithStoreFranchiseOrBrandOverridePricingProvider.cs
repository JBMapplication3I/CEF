// <copyright file="FlatWithStoreFranchiseOrBrandOverridePricingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the flat with store, franchise or brand override pricing provider class</summary>
namespace Clarity.Ecommerce.Providers.Pricing.FlatWithStoreFranchiseOrBrandOverride
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;
    using JSConfigs;
    using Models;

    /// <summary>A flat with store override pricing provider.</summary>
    /// <seealso cref="PricingProviderBase"/>
    public class FlatWithStoreFranchiseOrBrandOverridePricingProvider : PricingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => FlatWithStoreFranchiseOrBrandOverridePricingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<ICalculatedPrice> CalculatePriceAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext,
            string? contextProfileName,
            bool? forCart = false)
        {
            // The prices on the product itself are used unless the Store has a price for this product.
            // With the extra option enabled, the store must also have inventory
            // (ignoring allocated/broken) as well to count
            var haveStoreID = factoryContext.StoreID.HasValue;
            var haveFranchiseID = factoryContext.FranchiseID.HasValue;
            var haveBrandID = factoryContext.BrandID.HasValue;
            if (haveStoreID || haveBrandID)
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var storeProduct = !haveStoreID ? null : await context.StoreProducts
                    .FilterByActive(true)
                    .FilterRelationshipsByMasterStoreID<StoreProduct, Product>(factoryContext.StoreID)
                    .FilterStoreProductsByProductID(factoryProduct.ProductID)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                var franchiseProduct = !haveFranchiseID ? null : await context.FranchiseProducts
                    .FilterByActive(true)
                    .FilterRelationshipsByMasterFranchiseID<FranchiseProduct, Product>(factoryContext.BrandID)
                    .FilterFranchiseProductsByProductID(factoryProduct.ProductID)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                var brandProduct = !haveBrandID ? null : await context.BrandProducts
                    .FilterByActive(true)
                    .FilterRelationshipsByMasterBrandID<BrandProduct, Product>(factoryContext.BrandID)
                    .FilterBrandProductsByProductID(factoryProduct.ProductID)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                var storeHasProduct = storeProduct != null;
                var franchiseHasProduct = franchiseProduct != null;
                var brandHasProduct = brandProduct != null;
                var requiresInventory = CEFConfigDictionary.PricingProviderFlatWithStoreFranchiseOrBrandOverridePricingModeRequireInventoryToOverride;
                var storeHasInventory = haveStoreID && requiresInventory
                    && await context.ProductInventoryLocationSections
                        .FilterByActive(true)
                        .FilterPILSByProductID(factoryProduct.ProductID)
                        .FilterPILSByStoreID(factoryContext.StoreID!.Value)
                        .FilterPILSByHasQuantity(true)
                        .AnyAsync()
                        .ConfigureAwait(false);
                var franchiseHasInventory = haveFranchiseID && requiresInventory
                    && await context.ProductInventoryLocationSections
                        .FilterByActive(true)
                        .FilterPILSByProductID(factoryProduct.ProductID)
                        .FilterPILSByFranchiseID(factoryContext.FranchiseID!.Value)
                        .FilterPILSByHasQuantity(true)
                        .AnyAsync()
                        .ConfigureAwait(false);
                var brandHasInventory = haveBrandID && requiresInventory
                    && await context.ProductInventoryLocationSections
                        .FilterByActive(true)
                        .FilterPILSByProductID(factoryProduct.ProductID)
                        .FilterPILSByBrandID(factoryContext.BrandID!.Value)
                        .FilterPILSByHasQuantity(true)
                        .AnyAsync()
                        .ConfigureAwait(false);
                if (storeHasProduct && (!requiresInventory || storeHasInventory))
                {
                    var storeUnitPrice = storeProduct!.PriceBase ?? 0m;
                    var storeSalePrice = storeProduct.PriceSale > 0 ? storeProduct.PriceSale : null;
                    return new CalculatedPrice(Name, (double)storeUnitPrice, (double?)storeSalePrice)
                    {
                        PriceKey = await GetPriceKeyAsync(factoryProduct, factoryContext).ConfigureAwait(false),
                        PriceKeyAlt = await GetPriceKeyAltAsync(factoryProduct, factoryContext).ConfigureAwait(false),
                    };
                }
                if (franchiseHasProduct && (!requiresInventory || franchiseHasInventory))
                {
                    var franchiseUnitPrice = franchiseProduct!.PriceBase ?? 0m;
                    var franchiseSalePrice = franchiseProduct.PriceSale > 0 ? franchiseProduct.PriceSale : null;
                    return new CalculatedPrice(Name, (double)franchiseUnitPrice, (double?)franchiseSalePrice)
                    {
                        PriceKey = await GetPriceKeyAsync(factoryProduct, factoryContext).ConfigureAwait(false),
                        PriceKeyAlt = await GetPriceKeyAltAsync(factoryProduct, factoryContext).ConfigureAwait(false),
                    };
                }
                if (brandHasProduct && (!requiresInventory || brandHasInventory))
                {
                    var brandUnitPrice = brandProduct!.PriceBase ?? 0m;
                    var brandSalePrice = brandProduct.PriceSale > 0 ? brandProduct.PriceSale : null;
                    return new CalculatedPrice(Name, (double)brandUnitPrice, (double?)brandSalePrice)
                    {
                        PriceKey = await GetPriceKeyAsync(factoryProduct, factoryContext).ConfigureAwait(false),
                        PriceKeyAlt = await GetPriceKeyAltAsync(factoryProduct, factoryContext).ConfigureAwait(false),
                    };
                }
            }
            var unitPrice = factoryProduct.PriceBase ?? 0m;
            var salePrice = factoryProduct.PriceSale > 0 ? factoryProduct.PriceSale : null;
            return new CalculatedPrice(Name, (double)unitPrice, (double?)salePrice)
            {
                PriceKey = await GetPriceKeyAsync(factoryProduct, factoryContext).ConfigureAwait(false),
                PriceKeyAlt = await GetPriceKeyAltAsync(factoryProduct, factoryContext).ConfigureAwait(false),
            };
        }

        /// <inheritdoc/>
        public override Task<string> GetPriceKeyAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext)
        {
            return Task.FromResult(
                nameof(FlatWithStoreFranchiseOrBrandOverridePricingProvider)
                    + $":P={factoryProduct.ProductID}"
                    + $",S={factoryContext.StoreID}"
                    + $",F={factoryContext.FranchiseID}"
                    + $",B={factoryContext.BrandID}");
        }

        /// <inheritdoc/>
        public override Task<string> GetPriceKeyAltAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext)
        {
            return Task.FromResult(
                Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    PriceAltKeyProvider = nameof(FlatWithStoreFranchiseOrBrandOverridePricingProvider),
                    PriceAltKeyProductID = $"{factoryProduct.ProductID}",
                    PriceAltKeyStoreID = $"{factoryContext.StoreID}",
                    PriceAltKeyFranchiseID = $"{factoryContext.FranchiseID}",
                    PriceAltKeyBrandID = $"{factoryContext.BrandID}",
                }));
        }
    }
}
