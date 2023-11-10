// <copyright file="NoInventoryProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the No inventory provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Inventory.No
{
    using Interfaces.Providers;

    /// <summary>A No inventory provider configuration.</summary>
    internal static class NoInventoryProviderConfig
    {
        /// <summary>Query if 'isDefaultAndActivated' is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Config is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<NoInventoryProvider>() || isDefaultAndActivated;
    }
}
