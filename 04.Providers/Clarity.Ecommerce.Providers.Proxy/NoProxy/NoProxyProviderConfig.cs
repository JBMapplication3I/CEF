// <copyright file="NoProxyProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the No Proxy provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Proxy.NoProxy
{
    using Interfaces.Providers;

    /// <summary>A no Proxy provider configuration.</summary>
    public static class NoProxyProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<NoProxyProvider>() || isDefaultAndActivated;
    }
}
