// <copyright file="BasicMembershipProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic membership provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Memberships.Basic
{
    using Interfaces.Providers;

    /// <summary>A basic membership provider configuration.</summary>
    internal static class BasicMembershipProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<BasicMembershipProvider>() || isDefaultAndActivated;
    }
}
