// <copyright file="DynamicsAXPackagingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Dynamics AX packaging provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Packaging.DynamicsAx
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A Dynamics AX packaging provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class DynamicsAXPackagingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="DynamicsAXPackagingProviderConfig" /> class.</summary>
        static DynamicsAXPackagingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(DynamicsAXPackagingProviderConfig));
        }

        /// <summary>Gets URL of the freight service.</summary>
        /// <value>The freight service URL.</value>
        [AppSettingsKey("Clarity.Providers.PackagingProvider.PackageServiceUrl"),
         DefaultValue(null)]
        internal static string? FreightServiceUrl
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(DynamicsAXPackagingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(DynamicsAXPackagingProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<DynamicsAXPackagingProvider>() || isDefaultAndActivated)
            && Contract.CheckValidKey(FreightServiceUrl);
    }
}
