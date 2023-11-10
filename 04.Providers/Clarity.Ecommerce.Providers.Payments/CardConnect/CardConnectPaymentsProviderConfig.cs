// <copyright file="CardConnectPaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CardConnect payments provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Payments.CardConnect
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A CardConnect payments provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class CardConnectPaymentsProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="CardConnectPaymentsProviderConfig" /> class.</summary>
        static CardConnectPaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(CardConnectPaymentsProviderConfig));
        }

        /// <summary>Gets URL for test.</summary>
        /// <value>The test URL.</value>
        [AppSettingsKey("Clarity.Payments.CardConnect.TestUrl"),
         DefaultValue(null)]
        internal static string? TestUrl
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(CardConnectPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CardConnectPaymentsProviderConfig));
        }

        /// <summary>Gets URL for production.</summary>
        /// <value>The production URL.</value>
        [AppSettingsKey("Clarity.Payments.CardConnect.ProdUrl"),
         DefaultValue(null)]
        internal static string? ProdUrl
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(CardConnectPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CardConnectPaymentsProviderConfig));
        }

        /// <summary>Gets the Username.</summary>
        /// <value>The Username.</value>
        [AppSettingsKey("Clarity.Payments.CardConnect.Username"),
         DefaultValue(null)]
        internal static string? Username
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(CardConnectPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CardConnectPaymentsProviderConfig));
        }

        /// <summary>Gets the Password.</summary>
        /// <value>The Password.</value>
        [AppSettingsKey("Clarity.Payments.CardConnect.Password"),
         DefaultValue(null)]
        internal static string? Password
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(CardConnectPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CardConnectPaymentsProviderConfig));
        }

        /// <summary>Gets the MerchantId.</summary>
        /// <value>The MerchantId.</value>
        [AppSettingsKey("Clarity.Payments.CardConnect.MerchantId"),
         DefaultValue(null)]
        internal static string? MerchantId
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(CardConnectPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CardConnectPaymentsProviderConfig));
        }

        /// <summary>Gets a value indicating whether the test mode is on.</summary>
        /// <value>True if test mode, false if not.</value>
        [AppSettingsKey("Clarity.Payments.CardConnect.TestMode"),
         DefaultValue(false)]
        internal static bool TestMode
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(CardConnectPaymentsProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(CardConnectPaymentsProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<CardConnectPaymentsProvider>() || isDefaultAndActivated)
                && Contract.CheckAllValidKeys(Username, Password, MerchantId);
    }
}
