// <copyright file="ForwardProxyProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Forward Proxy provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Proxy.ForwardProxy
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A Forward Proxy provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    public static class ForwardProxyProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="ForwardProxyProviderConfig" /> class.</summary>
        static ForwardProxyProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(ForwardProxyProviderConfig));
        }

        /// <summary>Gets the identifier of the setup.</summary>
        /// <value>The identifier of the setup.</value>
        [AppSettingsKey("Clarity.FeatureSet.Proxy.ForwardProxyURL"),
         DefaultValue(null)]
        internal static string? ForwardProxyURL
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(ForwardProxyProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ForwardProxyProviderConfig));
        }

        /// <summary>Gets the identifier of the setup.</summary>
        /// <value>The identifier of the setup.</value>
        [AppSettingsKey("Clarity.FeatureSet.Proxy.ForwardProxyPort"),
         DefaultValue(0)]
        internal static int ForwardProxyPort
        {
            get => CEFConfigDictionary.TryGet(out int asValue, typeof(ForwardProxyProviderConfig)) ? asValue : 0;
            private set => CEFConfigDictionary.TrySet(value, typeof(ForwardProxyProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<ForwardProxyProvider>() || isDefaultAndActivated
            && Contract.CheckAllValidKeys(ForwardProxyURL, ForwardProxyPort.ToString());
    }
}
