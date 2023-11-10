// <copyright file="TargetQuoteSubmitProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target quote submit provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Checkouts.TargetQuote
{
    using Interfaces.Providers;

    /// <summary>A target quote submit provider configuration.</summary>
    internal static class TargetQuoteSubmitProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<TargetQuoteSubmitProvider>() || isDefaultAndActivated;
    }
}
