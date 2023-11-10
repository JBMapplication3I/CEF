// <copyright file="FlatPricingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the flat pricing provider class</summary>
namespace Clarity.Ecommerce.Providers.Pricing.Flat
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;
    using Models;

    /// <summary>A flat pricing provider.</summary>
    /// <seealso cref="PricingProviderBase"/>
    public class FlatPricingProvider : PricingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => FlatPricingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task<ICalculatedPrice> CalculatePriceAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext,
            string? contextProfileName,
            bool? forCart = false)
        {
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
                nameof(FlatPricingProvider)
                    + $":P={factoryProduct.ProductID}");
        }

        /// <inheritdoc/>
        public override Task<string> GetPriceKeyAltAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext)
        {
            return Task.FromResult(
                Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    PriceAltKeyProvider = nameof(FlatPricingProvider),
                    PriceAltKeyProductID = $"{factoryProduct.ProductID}",
                }));
        }
    }
}
