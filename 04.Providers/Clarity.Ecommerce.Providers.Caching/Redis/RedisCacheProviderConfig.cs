// <copyright file="RedisCacheProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the redis cache provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Caching.Redis
{
    using Interfaces.Providers;

    /// <summary>A redis cache provider configuration.</summary>
    internal static class RedisCacheProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<RedisCacheProvider>() || isDefaultAndActivated;
    }
}
