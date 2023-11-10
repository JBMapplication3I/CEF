// <copyright file="PricingProviderRegistry.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Pricing provider registry class</summary>
#if NET5_0_OR_GREATER
namespace Clarity.Ecommerce.Providers.Pricing
{
    using Flat;
    using FlatWithStoreFranchiseOrBrandOverride;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;
    using Lamar;
    using Models;
    using Oracle;
    using PriceRule;
    using Tiered;

    /// <summary>The Pricing provider registry.</summary>
    /// <seealso cref="ServiceRegistry"/>
    [JetBrains.Annotations.PublicAPI]
    public class PricingProviderRegistry : ServiceRegistry
    {
        /// <summary>Initializes a new instance of the <see cref="PricingProviderRegistry"/> class.</summary>
        public PricingProviderRegistry()
        {
            For<IRawProductPricesModel>().Use<RawProductPricesModel>();
            For<IUserForPricingModel>().Use<UserForPricingModel>();
            For<IPricingFactorySettings>().Use<PricingFactorySettings>();
            For<IPricingFactory>().Use<PricingFactory>();
            if (PriceRulesPricingProviderConfig.IsValid(false))
            {
                Use<PriceRulesPricingProvider>().Singleton().For<IPricingProviderBase>();
                return;
            }
            if (TieredPricingFallbackProviderConfig.IsValid(false))
            {
                Use<TieredPricingFallbackProvider>().Singleton().For<IPricingProviderBase>();
                return;
            }
            if (TieredPricingProviderConfig.IsValid(false))
            {
                Use<TieredPricingProvider>().Singleton().For<IPricingProviderBase>();
                return;
            }
            if (FlatWithStoreFranchiseOrBrandOverridePricingProviderConfig.IsValid(false))
            {
                Use<FlatWithStoreFranchiseOrBrandOverridePricingProvider>().Singleton().For<IPricingProviderBase>();
                return;
            }
            if (FlatPricingProviderConfig.IsValid(false))
            {
                Use<FlatPricingProvider>().Singleton().For<IPricingProviderBase>();
                return;
            }
            if (OraclePricingProviderConfig.IsValid(false))
            {
                Use<OraclePricingProvider>().Singleton().For<IPricingProviderBase>();
                return;
            }
            // Assign Default
            Use<FlatPricingProvider>().Singleton().For<IPricingProviderBase>();
        }
    }
}
#else
namespace Clarity.Ecommerce.Providers.Pricing
{
    using Flat;
    using FlatWithStoreFranchiseOrBrandOverride;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;
    using Models;
    using Oracle;
    using PriceRule;
    using StructureMap;
    using StructureMap.Pipeline;
    using Tiered;

    /// <summary>The Pricing provider registry.</summary>
    /// <seealso cref="Registry"/>
    [JetBrains.Annotations.PublicAPI]
    public class PricingProviderRegistry : Registry
    {
        /// <summary>Initializes a new instance of the <see cref="PricingProviderRegistry"/> class.</summary>
        public PricingProviderRegistry()
        {
            For<IRawProductPricesModel>().Use<RawProductPricesModel>();
            For<IUserForPricingModel>().Use<UserForPricingModel>();
            For<IPricingFactorySettings>().Use<PricingFactorySettings>();
            For<IPricingFactory>().Use<PricingFactory>();
            var found = false;
            if (PriceRulesPricingProviderConfig.IsValid(false))
            {
                For<IPricingProviderBase>(new SingletonLifecycle()).Add<PriceRulesPricingProvider>();
                found = true;
            }
            if (TieredPricingFallbackProviderConfig.IsValid(false))
            {
                For<IPricingProviderBase>(new SingletonLifecycle()).Add<TieredPricingFallbackProvider>();
                found = true;
            }
            if (TieredPricingProviderConfig.IsValid(false))
            {
                For<IPricingProviderBase>(new SingletonLifecycle()).Add<TieredPricingProvider>();
                found = true;
            }
            if (FlatWithStoreFranchiseOrBrandOverridePricingProviderConfig.IsValid(false))
            {
                For<IPricingProviderBase>(new SingletonLifecycle()).Add<FlatWithStoreFranchiseOrBrandOverridePricingProvider>();
                found = true;
            }
            if (FlatPricingProviderConfig.IsValid(false))
            {
                For<IPricingProviderBase>(new SingletonLifecycle()).Add<FlatPricingProvider>();
                found = true;
            }
            if (OraclePricingProviderConfig.IsValid(false))
            {
                For<IPricingProviderBase>(new SingletonLifecycle()).Add<OraclePricingProvider>();
                found = true;
            }
            if (found)
            {
                return;
            }
            // Assign Default
            For<IPricingProviderBase>(new SingletonLifecycle()).Add<FlatPricingProvider>();
        }
    }
}
#endif
