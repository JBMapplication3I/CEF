// <copyright file="OraclePricingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Oracle pricing provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Pricing.Oracle
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A Oracle pricing provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    public static class OraclePricingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="OraclePricingProviderConfig" /> class.</summary>
        static OraclePricingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(OraclePricingProviderConfig));
        }

        /// <summary>Gets the url.</summary>
        /// <value>The url.</value>
        [AppSettingsKey("Clarity.Pricing.Oracle.BaseUrl"),
         DefaultValue(null)]
        internal static string? BaseUrl
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(OraclePricingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(OraclePricingProviderConfig));
        }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        [AppSettingsKey("Clarity.Pricing.Oracle.Username"),
         DefaultValue(null)]
        internal static string? Username
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(OraclePricingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(OraclePricingProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Pricing.Oracle.Password"),
         DefaultValue(null)]
        internal static string? Password
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(OraclePricingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(OraclePricingProviderConfig));
        }

        /// <summary>Gets the applied currency code.</summary>
        /// <value>The applied currency code.</value>
        [AppSettingsKey("Clarity.Pricing.Oracle.AppliedCurrencyCode"),
         DefaultValue(null)]
        internal static string? AppliedCurrencyCode
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(OraclePricingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(OraclePricingProviderConfig));
        }

        /// <summary>Gets the selling business unit name.</summary>
        /// <value>The selling business unit name.</value>
        [AppSettingsKey("Clarity.Pricing.Oracle.SellingBusinessUnitName"),
         DefaultValue(null)]
        internal static string? SellingBusinessUnitName
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(OraclePricingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(OraclePricingProviderConfig));
        }

        /// <summary>Gets the inventory organization code.</summary>
        /// <value>The inventory organization code.</value>
        [AppSettingsKey("Clarity.Pricing.Oracle.InventoryOrganizationCode"),
         DefaultValue(null)]
        internal static string? InventoryOrganizationCode
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(OraclePricingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(OraclePricingProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<OraclePricingProvider>() || isDefaultAndActivated
                && Contract.CheckAllValidKeys(BaseUrl, Username, Password);
    }
}
