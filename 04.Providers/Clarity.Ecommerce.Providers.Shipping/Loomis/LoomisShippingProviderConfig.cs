// <copyright file="LoomisShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the loomis configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Shipping.Loomis
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>The loomis shipping provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class LoomisShippingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="LoomisShippingProviderConfig" /> class.</summary>
        static LoomisShippingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(LoomisShippingProviderConfig));
        }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        [AppSettingsKey("Clarity.Shipping.Loomis.Username"),
         DefaultValue(null)]
        internal static string? UserName
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(LoomisShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(LoomisShippingProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Shipping.Loomis.Password"),
         DefaultValue(null)]
        internal static string? Password
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(LoomisShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(LoomisShippingProviderConfig));
        }

        /// <summary>Gets the account number.</summary>
        /// <value>The account number.</value>
        [AppSettingsKey("Clarity.Shipping.Loomis.AccountNumber"),
         DefaultValue(null)]
        internal static string? AccountNumber
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(LoomisShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(LoomisShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether this ShippingProvider should use default minimum pricing.</summary>
        /// <value>True if use default minimum pricing, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.Loomis.UseDefaultMinimumPricing"),
         DefaultValue(false)]
        internal static bool UseDefaultMinimumPricing
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(LoomisShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(LoomisShippingProviderConfig));
        }

        /// <summary>Gets the default minimum price.</summary>
        /// <value>The default minimum price.</value>
        [AppSettingsKey("Clarity.Shipping.Loomis.DefaultMinimumPrice"),
         DefaultValue(null)]
        internal static string? DefaultMinimumPrice
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(LoomisShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(LoomisShippingProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<LoomisShippingProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(UserName, Password, AccountNumber);
    }
}
