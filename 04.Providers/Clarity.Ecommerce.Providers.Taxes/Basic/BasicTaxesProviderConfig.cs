// <copyright file="BasicTaxesProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic taxes provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.Basic
{
    using Interfaces.Providers;

    /// <summary>An basic taxes provider configuration.</summary>
    internal static class BasicTaxesProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<BasicTaxesProvider>() || isDefaultAndActivated;
    }
}
