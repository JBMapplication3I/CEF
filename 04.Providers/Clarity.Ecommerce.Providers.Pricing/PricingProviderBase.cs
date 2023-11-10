// <copyright file="PricingProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pricing provider base class</summary>
namespace Clarity.Ecommerce.Providers.Pricing
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;

    /// <summary>A pricing provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="IPricingProviderBase"/>
    public abstract class PricingProviderBase : ProviderBase, IPricingProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Pricing;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <summary>Gets the cache.</summary>
        /// <value>The cache.</value>
        protected ICalculatedPriceCache? Cache { get; private set; }

        /// <summary>Sets a cache.</summary>
        /// <param name="cache">The cache.</param>
        public void SetCache(ICalculatedPriceCache cache)
        {
            Cache = cache;
        }

        /// <inheritdoc/>
        public abstract Task<ICalculatedPrice> CalculatePriceAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext,
            string? contextProfileName,
            bool? forCart = false);

        /// <inheritdoc/>
        public abstract Task<string> GetPriceKeyAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext);

        /// <inheritdoc/>
        public abstract Task<string> GetPriceKeyAltAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext);
    }
}
