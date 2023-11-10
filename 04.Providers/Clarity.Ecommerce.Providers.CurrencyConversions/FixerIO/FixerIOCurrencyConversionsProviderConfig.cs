// <copyright file="FixerIOCurrencyConversionsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the fixer i/o currency conversions provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.CurrencyConversions.FixerIO
{
    using Interfaces.Providers;

    /// <summary>A fixer i/o currency conversions provider configuration.</summary>
    internal static class FixerIOCurrencyConversionsProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<FixerIOCurrencyConversionsProvider>() || isDefaultAndActivated;
    }
}
