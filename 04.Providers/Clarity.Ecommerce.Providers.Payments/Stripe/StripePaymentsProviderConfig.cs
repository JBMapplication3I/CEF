// <copyright file="StripePaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the stripe payments provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Payments.StripeInt
{
    using Interfaces.Providers;

    /// <summary>A stripe payments provider configuration.</summary>
    internal static class StripePaymentsProviderConfig
    {
        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<StripePaymentsProvider>() || isDefaultAndActivated;
    }
}
