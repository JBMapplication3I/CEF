// <copyright file="StandardCheckoutProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard checkout provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Checkouts.Standard
{
    using Interfaces.Providers;

    /// <summary>A target order checkout provider configuration.</summary>
    internal static class StandardSampleRequestCheckoutProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<StandardSampleRequestCheckoutProvider>() || isDefaultAndActivated;
    }
}
