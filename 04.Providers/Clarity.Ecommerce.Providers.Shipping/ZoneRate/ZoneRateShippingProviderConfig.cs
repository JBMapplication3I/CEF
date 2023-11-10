// <copyright file="ZoneRateShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the zone rate shipping provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Zone
{
    using Interfaces.Providers;

    /// <summary>A zone rate shipping provider configuration.</summary>
    internal static class ZoneRateShippingProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<ZoneRateShippingProvider>() || isDefaultAndActivated;
    }
}
