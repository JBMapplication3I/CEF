// <copyright file="TieredPricingFallbackProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tiered pricing provider with fallback class</summary>
namespace Clarity.Ecommerce.Providers.Pricing.Tiered
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;
    using Models;

    /// <summary>A tiered pricing provider.</summary>
    /// <seealso cref="TieredPricingProvider"/>
    public class TieredPricingFallbackProvider : TieredPricingProvider
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => TieredPricingFallbackProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override Task<ICalculatedPrice> CalculatePriceAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext,
            string? contextProfileName,
            bool? forCart = false)
        {
            return CalculateTieredPriceAsync(factoryProduct, factoryContext, contextProfileName);
        }

        /// <inheritdoc/>
        public override Task<string> GetPriceKeyAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext)
        {
            return Task.FromResult(
                nameof(TieredPricingFallbackProvider)
                    + $":P={factoryProduct.ProductID}"
                    + $",PP={factoryContext.PricePoint}"
                    + $",U={factoryProduct.ProductUnitOfMeasure}"
                    + $",S={factoryContext.StoreID ?? 0}"
                    + $",F={factoryContext.FranchiseID ?? 0}"
                    + $",B={factoryContext.BrandID ?? 0}"
                    + $",Q={factoryContext.Quantity:0.00000}"
                    + $",D={DateTime.Today:yyyyMMdd}");
        }

        /// <inheritdoc/>
        public override Task<string> GetPriceKeyAltAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext)
        {
            return Task.FromResult(
                Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    PriceKeyAltProvider = nameof(TieredPricingFallbackProvider),
                    PriceKeyAltProductID = $"{factoryProduct.ProductID}",
                    PriceKeyAltPricePoint = $"{factoryContext.PricePoint}",
                    PriceKeyAltProductUnitOfMeasure = $"{factoryProduct.ProductUnitOfMeasure}",
                    PriceKeyAltStoreID = $"{factoryContext.StoreID ?? 0}",
                    PriceKeyAltFranchiseID = $"{factoryContext.FranchiseID ?? 0}",
                    PriceKeyAltBrandID = $"{factoryContext.BrandID ?? 0}",
                    PriceKeyAltQuantity = $"{factoryContext.Quantity:0.00000}",
                    PriceKeyAltDate = $"{DateTime.Today:yyyyMMdd}",
                }));
        }

        /// <summary>Calculates the tiered price, but fall back to price base if none available.</summary>
        /// <inheritdoc/>
        protected override async Task<ICalculatedPrice> CalculateTieredPriceAsync(
            IPricingFactoryProductModel? factoryProduct,
            IPricingFactoryContextModel factoryContext,
            string? contextProfileName)
        {
            var parentResult = await base.CalculateTieredPriceAsync(
                    factoryProduct,
                    factoryContext,
                    contextProfileName)
                .ConfigureAwait(false);
            if (parentResult.IsValid)
            {
                return parentResult;
            }
            return GetPriceForNoValidPoints(factoryProduct);
        }

        /// <summary>Gets price for no valid points.</summary>
        /// <param name="product">The product.</param>
        /// <returns>The price for no valid points.</returns>
        private CalculatedPrice GetPriceForNoValidPoints(IPricingFactoryProductModel? product)
        {
            // No valid Points
            var unitPrice = product?.PriceBase ?? 0m;
            var salePrice = product?.PriceSale > 0 ? product.PriceSale : null;
            return new(Name, (double)unitPrice, (double?)salePrice);
        }
    }
}
