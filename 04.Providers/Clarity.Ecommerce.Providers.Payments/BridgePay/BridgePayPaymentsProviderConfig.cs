// <copyright file="BridgePayPaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bridge pay payments provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Payments.BridgePay
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A bridge pay payments provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class BridgePayPaymentsProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="BridgePayPaymentsProviderConfig" /> class.</summary>
        static BridgePayPaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(BridgePayPaymentsProviderConfig));
        }

        /// <summary>Gets the login.</summary>
        /// <value>The login.</value>
        [AppSettingsKey("Clarity.Payment.BridgePay.Login"),
         DefaultValue("clv067test")]
        internal static string Login
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(BridgePayPaymentsProviderConfig)) ? asValue : "clv067test";
            private set => CEFConfigDictionary.TrySet(value, typeof(BridgePayPaymentsProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Payment.BridgePay.Password"),
         DefaultValue("P4ssw0rd")]
        internal static string Password
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(BridgePayPaymentsProviderConfig)) ? asValue : "P4ssw0rd";
            private set => CEFConfigDictionary.TrySet(value, typeof(BridgePayPaymentsProviderConfig));
        }

        /// <summary>Gets the merchant code.</summary>
        /// <value>The merchant code.</value>
        [AppSettingsKey("Clarity.Payment.BridgePay.MerchantCode"),
         DefaultValue("788000")]
        internal static string MerchantCode
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(BridgePayPaymentsProviderConfig)) ? asValue : "788000";
            private set => CEFConfigDictionary.TrySet(value, typeof(BridgePayPaymentsProviderConfig));
        }

        /// <summary>Gets the merchant account code.</summary>
        /// <value>The merchant account code.</value>
        [AppSettingsKey("Clarity.Payment.BridgePay.MerchantAccountCode"),
         DefaultValue("788001")]
        internal static string MerchantAccountCode
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(BridgePayPaymentsProviderConfig)) ? asValue : "788001";
            private set => CEFConfigDictionary.TrySet(value, typeof(BridgePayPaymentsProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<BridgePayPaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(Login, Password, MerchantCode, MerchantAccountCode);
    }
}
