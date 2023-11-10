// <copyright file="FlatPricingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the flat pricing provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Pricing.Flat
{
    using Interfaces.Providers;

    /// <summary>A flat pricing provider configuration.</summary>
    internal static class FlatPricingProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<FlatPricingProvider>() || isDefaultAndActivated;
    }
}
