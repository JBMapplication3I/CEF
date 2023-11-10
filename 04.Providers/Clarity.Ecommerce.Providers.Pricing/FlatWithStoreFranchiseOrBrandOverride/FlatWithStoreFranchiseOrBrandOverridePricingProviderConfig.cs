// <copyright file="FlatWithStoreFranchiseOrBrandOverridePricingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <author>Clarity Team</author>
// <date>4/20/2017</date>
// <summary>Implements the flat with store, franchise or brand override pricing provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Pricing.FlatWithStoreFranchiseOrBrandOverride
{
    using Interfaces.Providers;
    using JSConfigs;

    /// <summary>A flat with store override pricing provider configuration.</summary>
    internal static class FlatWithStoreFranchiseOrBrandOverridePricingProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<FlatWithStoreFranchiseOrBrandOverridePricingProvider>() || isDefaultAndActivated)
            && (CEFConfigDictionary.StoresEnabled || CEFConfigDictionary.FranchisesEnabled || CEFConfigDictionary.BrandsEnabled);
    }
}
