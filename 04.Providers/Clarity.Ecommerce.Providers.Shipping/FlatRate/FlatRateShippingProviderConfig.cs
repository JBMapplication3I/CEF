// <copyright file="FlatRateShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the flat rate shipping provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Shipping.FlatRate
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;

    /// <summary>A flat rate shipping provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class FlatRateShippingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="FlatRateShippingProviderConfig" /> class.</summary>
        static FlatRateShippingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(FlatRateShippingProviderConfig));
        }

        /// <summary>Gets the default package threshold.</summary>
        /// <value>The default package threshold.</value>
        [AppSettingsKey("Clarity.Shipping.FlatRate.PackageThreshold"),
            DefaultValue(null)]
        internal static string? PackageThreshold
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(FlatRateShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(FlatRateShippingProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<FlatRateShippingProvider>() || isDefaultAndActivated;
    }
}
