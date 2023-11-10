// <copyright file="FlatInventoryProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the flat inventory provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Inventory.Flat
{
    using Interfaces.Providers;

    /// <summary>A flat inventory provider configuration.</summary>
    internal static class FlatInventoryProviderConfig
    {
        /// <summary>Query if 'isDefaultAndActivated' is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Config is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<FlatInventoryProvider>() || isDefaultAndActivated;
    }
}
