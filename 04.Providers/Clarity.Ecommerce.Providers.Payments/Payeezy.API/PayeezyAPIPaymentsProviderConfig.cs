// <copyright file="PayeezyAPIPaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy API payments provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A payeezy API payments provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class PayeezyAPIPaymentsProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="PayeezyAPIPaymentsProviderConfig" /> class.</summary>
        static PayeezyAPIPaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(PayeezyAPIPaymentsProviderConfig));
        }

        /// <summary>Gets the API key.</summary>
        /// <value>The API key.</value>
        [AppSettingsKey("Clarity.Payments.Evo.ApiKey"),
         DefaultValue(null)]
        internal static string? ApiKey
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayeezyAPIPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayeezyAPIPaymentsProviderConfig));
        }

        /// <summary>Gets the token.</summary>
        /// <value>The token.</value>
        [AppSettingsKey("Clarity.Payments.Evo.Token"),
         DefaultValue(null)]
        internal static string? Token
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayeezyAPIPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayeezyAPIPaymentsProviderConfig));
        }

        /// <summary>Gets the secret.</summary>
        /// <value>The secret.</value>
        [AppSettingsKey("Clarity.Payments.Evo.Secret"),
         DefaultValue(null)]
        internal static string? Secret
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayeezyAPIPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayeezyAPIPaymentsProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<PayeezyAPIPaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(ApiKey, Token, Secret);
    }
}
