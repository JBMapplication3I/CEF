// <copyright file="PILSInventoryProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PILS inventory provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Inventory.PILS
{
    using Interfaces.Providers;

    /// <summary>The PILS inventory provider configuration.</summary>
    internal static class PILSInventoryProviderConfig
    {
        /// <summary>Query if 'isDefaultAndActivated' is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Config is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<PILSInventoryProvider>() || isDefaultAndActivated;
    }
}
