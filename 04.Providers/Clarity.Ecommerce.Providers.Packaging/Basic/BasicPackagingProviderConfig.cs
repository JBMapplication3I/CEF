// <copyright file="BasicPackagingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic packaging provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Packaging.Basic
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;

    /// <summary>A basic packaging provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class BasicPackagingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="BasicPackagingProviderConfig" /> class.</summary>
        static BasicPackagingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(BasicPackagingProviderConfig));
        }

        /// <summary>Gets a value indicating whether this PackagingProvider use specific ship options.</summary>
        /// <value>True if use specific ship options, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.UseSpecificShipOptionsOnly"),
         DefaultValue(false)]
        internal static bool UseSpecificShipOptions
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(BasicPackagingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(BasicPackagingProviderConfig));
        }

        /// <summary>Gets options for controlling the ship.</summary>
        /// <value>Options that control the ship.</value>
        [AppSettingsKey("Clarity.Shipping.SpecificShipOptions"),
         DefaultValue(null)]
        internal static string? SpecificShipOptions
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(BasicPackagingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(BasicPackagingProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<BasicPackagingProvider>() || isDefaultAndActivated;
    }
}
