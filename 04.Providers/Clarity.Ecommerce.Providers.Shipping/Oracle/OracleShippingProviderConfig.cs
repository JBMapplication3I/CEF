// <copyright file="OracleShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the oracle shipping provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Shipping.Oracle
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>An oracle shipping provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class OracleShippingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="OracleShippingProviderConfig" /> class.</summary>
        static OracleShippingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(OracleShippingProviderConfig));
        }

        /// <summary>Gets URL of the document.</summary>
        /// <value>The URL.</value>
        [AppSettingsKey("Clarity.Shipping.Oracle.Url"),
         DefaultValue(null)]
        internal static string? Url
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(OracleShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(OracleShippingProviderConfig));
        }

        /// <summary>Gets the default minimum price.</summary>
        /// <value>The default minimum price.</value>
        [AppSettingsKey("Clarity.Shipping.Oracle.UseDefaultMinimumPrice"),
         DefaultValue(null)]
        internal static string? DefaultMinimumPrice
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(OracleShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(OracleShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether this OracleShippingProviderConfig use default minimum pricing.</summary>
        /// <value>True if use default minimum pricing, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.Oracle.UseDefaultMinimumPrice"),
         DefaultValue(false)]
        internal static bool UseDefaultMinimumPricing
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(OracleShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(OracleShippingProviderConfig));
        }

        /// <summary>Query if 'isDefaultAndActivated' is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this OracleShippingProviderConfig is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<OracleShippingProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(Url);
    }
}
