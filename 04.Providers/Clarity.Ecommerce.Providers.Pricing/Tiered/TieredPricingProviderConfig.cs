// <copyright file="TieredPricingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tiered pricing provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Pricing.Tiered
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A tiered pricing provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class TieredPricingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="TieredPricingProviderConfig" /> class.</summary>
        static TieredPricingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(TieredPricingProviderConfig));
        }

        /// <summary>Gets the default currency key.</summary>
        /// <value>The default currency key.</value>
        [AppSettingsKey("Clarity.Modes.TieredPricingMode.DefaultCurrencyKey"),
         DefaultValue("USD")]
        internal static string DefaultCurrencyKey
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(TieredPricingProviderConfig)) ? asValue : "USD";
            private set => CEFConfigDictionary.TrySet(value, typeof(TieredPricingProviderConfig));
        }

        /// <summary>Gets the default price point key.</summary>
        /// <value>The default price point key.</value>
        [AppSettingsKey("Clarity.Modes.TieredPricingMode.DefaultPricePointKey"),
         DefaultValue("WEB")]
        internal static string DefaultPricePointKey
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(TieredPricingProviderConfig)) ? asValue : "WEB";
            private set => CEFConfigDictionary.TrySet(value, typeof(TieredPricingProviderConfig));
        }

        /// <summary>Gets the default unit of measure.</summary>
        /// <value>The default unit of measure.</value>
        [AppSettingsKey("Clarity.Modes.TieredPricingMode.DefaultUnitOfMeasure"),
         DefaultValue("EA")]
        internal static string? DefaultUnitOfMeasure
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(TieredPricingProviderConfig)) ? asValue : "EA";
            private set => CEFConfigDictionary.TrySet(value, typeof(TieredPricingProviderConfig));
        }

        /// <summary>Gets the default markup rate.</summary>
        /// <value>The default markup rate.</value>
        [AppSettingsKey("Clarity.Modes.TieredPricingMode.DefaultMarkupRate"),
         DefaultValue(0)]
        internal static decimal? DefaultMarkupRate
        {
            get => CEFConfigDictionary.TryGet(out decimal? asValue, typeof(TieredPricingProviderConfig)) ? asValue : 0;
            private set => CEFConfigDictionary.TrySet(value, typeof(TieredPricingProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<TieredPricingProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(DefaultCurrencyKey, DefaultPricePointKey, DefaultUnitOfMeasure);
    }
}
