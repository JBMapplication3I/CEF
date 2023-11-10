// <copyright file="CombinedShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the combined shipping provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Combined
{
    using Interfaces.Providers;

    /// <summary>A combined shipping provider configuration.</summary>
    internal static class CombinedShippingProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<CombinedShippingProvider>() || isDefaultAndActivated;
    }
}
