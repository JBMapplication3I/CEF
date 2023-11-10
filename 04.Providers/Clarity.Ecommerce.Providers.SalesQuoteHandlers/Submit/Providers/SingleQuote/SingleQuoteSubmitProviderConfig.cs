// <copyright file="SingleQuoteSubmitProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the single quote submit provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Checkouts.SingleQuote
{
    using Interfaces.Providers;

    /// <summary>A single quote checkout provider configuration.</summary>
    internal static class SingleQuoteSubmitProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<SingleQuoteSubmitProvider>() || isDefaultAndActivated;
    }
}
