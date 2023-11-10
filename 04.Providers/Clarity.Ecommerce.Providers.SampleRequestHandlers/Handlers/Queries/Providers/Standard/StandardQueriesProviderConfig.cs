// <copyright file="StandardQueriesProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard queries provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Queries.Standard
{
    using Interfaces.Providers;

    /// <summary>A standard sample request queries provider configuration.</summary>
    internal static class StandardSampleRequestQueriesProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<StandardSampleRequestQueriesProvider>() || isDefaultAndActivated;
    }
}
