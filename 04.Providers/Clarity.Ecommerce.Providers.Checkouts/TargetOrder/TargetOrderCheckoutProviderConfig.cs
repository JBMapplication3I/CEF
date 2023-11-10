// <copyright file="TargetOrderCheckoutProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target order checkout provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Checkouts.TargetOrder
{
    using Interfaces.Providers;

    /// <summary>A target order checkout provider configuration.</summary>
    internal static class TargetOrderCheckoutProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<TargetOrderCheckoutProvider>() || isDefaultAndActivated;
    }
}
