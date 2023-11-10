// <copyright file="TieredShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tiered Shipping provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Shipping.Tiered
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A tiered Shipping provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class TieredShippingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="TieredShippingProviderConfig" /> class.</summary>
        static TieredShippingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(TieredShippingProviderConfig));
        }

        /// <summary>Gets the default currency key.</summary>
        /// <value>The default currency key.</value>
        [AppSettingsKey("Clarity.Shipping.Tiered.DefaultCurrencyKey"),
         DefaultValue("USD")]
        internal static string DefaultCurrencyKey
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(TieredShippingProviderConfig)) ? asValue : "USD";
            private set => CEFConfigDictionary.TrySet(value, typeof(TieredShippingProviderConfig));
        }

        /// <summary>Gets the default unit of measure.</summary>
        /// <value>The default unit of measure.</value>
        [AppSettingsKey("Clarity.Shipping.Tiered.DefaultUnitOfMeasure"),
         DefaultValue("EA")]
        internal static string DefaultUnitOfMeasure
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(TieredShippingProviderConfig)) ? asValue : "EA";
            private set => CEFConfigDictionary.TrySet(value, typeof(TieredShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether this provider use default minimum pricing.</summary>
        /// <value>True if use default minimum pricing, false if not.</value>
        [AppSettingsKey("Clarity.Modes.TieredPricingMode.DefaultCurrencyKey"),
         DefaultValue(false)]
        internal static bool UseDefaultMinimumPricing
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(TieredShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(TieredShippingProviderConfig));
        }

        /// <summary>Gets the default minimum price.</summary>
        /// <value>The default minimum price.</value>
        [AppSettingsKey("Clarity.Modes.TieredPricingMode.DefaultCurrencyKey"),
         DefaultValue(0)]
        internal static decimal DefaultMinimumPrice
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(TieredShippingProviderConfig)) ? asValue : 0;
            private set => CEFConfigDictionary.TrySet(value, typeof(TieredShippingProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<TieredShippingProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(DefaultCurrencyKey, DefaultUnitOfMeasure);
    }
}
