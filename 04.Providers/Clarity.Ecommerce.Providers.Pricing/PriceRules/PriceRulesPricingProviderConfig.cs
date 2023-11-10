// <copyright file="PriceRulesPricingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rules pricing provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Pricing.PriceRule
{
    using Interfaces.Providers;

    /// <summary>The prices rule pricing provider configuration.</summary>
    internal static class PriceRulesPricingProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<PriceRulesPricingProvider>() || isDefaultAndActivated;
    }
}
