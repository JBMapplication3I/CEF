// <copyright file="BasicApiProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic API provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Api.Basic
{
    using Interfaces.Providers;

    /// <summary>A basic API provider configuration.</summary>
    public static class BasicApiProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<BasicApiProvider>() || isDefaultAndActivated;
    }
}
