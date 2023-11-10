// <copyright file="PayeezyPaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Payments.Payeezy
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A cyber source configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class PayeezyPaymentsProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="PayeezyPaymentsProviderConfig" /> class.</summary>
        static PayeezyPaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(PayeezyPaymentsProviderConfig));
        }

        /// <summary>Gets the identifier of the exact.</summary>
        /// <value>The identifier of the exact.</value>
        [AppSettingsKey("Clarity.Payment.Payeezy.ExactID"),
         DefaultValue(null)]
        internal static string? ExactID
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayeezyPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayeezyPaymentsProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Payment.Payeezy.Password"),
         DefaultValue(null)]
        internal static string? Password
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayeezyPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayeezyPaymentsProviderConfig));
        }

        /// <summary>Gets a value indicating whether the level 3.</summary>
        /// <value>True if level 3, false if not.</value>
        [AppSettingsKey("Clarity.Payment.Payeezy.Level3"),
         DefaultValue(false)]
        internal static bool Level3
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(PayeezyPaymentsProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayeezyPaymentsProviderConfig));
        }

        /// <summary>Gets URL for test.</summary>
        /// <value>The test URL.</value>
        [AppSettingsKey("Clarity.Payment.Payeezy.TestUrl"),
         DefaultValue(null)]
        internal static string? TestUrl
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayeezyPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayeezyPaymentsProviderConfig));
        }

        /// <summary>Gets URL for production.</summary>
        /// <value>The production URL.</value>
        [AppSettingsKey("Clarity.Payment.Payeezy.ProdUrl"),
         DefaultValue(null)]
        internal static string? ProdUrl
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayeezyPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayeezyPaymentsProviderConfig));
        }

        /// <summary>Gets the hmac.</summary>
        /// <value>The hmac.</value>
        [AppSettingsKey("Clarity.Payment.Payeezy.Hmac"),
         DefaultValue(null)]
        internal static string? Hmac
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayeezyPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayeezyPaymentsProviderConfig));
        }

        /// <summary>Gets the identifier of the key.</summary>
        /// <value>The identifier of the key.</value>
        [AppSettingsKey("Clarity.Payment.Payeezy.KeyID"),
         DefaultValue(null)]
        internal static string? KeyID
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayeezyPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayeezyPaymentsProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<PayeezyPaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(ExactID, Password);
    }
}
