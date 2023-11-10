// <copyright file="ShipToStoreShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ship to store shipping provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.ShipToStore
{
    using Interfaces.Providers;

    /// <summary>A ship to store shipping provider configuration.</summary>
    internal static class ShipToStoreShippingProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<ShipToStoreShippingProvider>() || isDefaultAndActivated;
    }
}
