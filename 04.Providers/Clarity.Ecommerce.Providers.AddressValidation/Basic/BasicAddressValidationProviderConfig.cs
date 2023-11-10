// <copyright file="BasicAddressValidationProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic address validation provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.Basic
{
    using Interfaces.Providers;

    /// <summary>A basic address validation provider configuration.</summary>
    internal static class BasicAddressValidationProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Config is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<BasicAddressValidationProvider>() || isDefaultAndActivated;
    }
}
