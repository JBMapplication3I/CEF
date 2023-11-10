// <copyright file="CyberSourcePaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cyber source payments provider configuration class</summary>
#if NET5_0_OR_GREATER
#else
namespace Clarity.Ecommerce.Providers.Payments.CyberSource
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A cyber source payments provider configuration.</summary>
    [PublicAPI]
    internal static class CyberSourcePaymentsProviderConfig
    {
        /// <summary>Dictionary of configurations.</summary>
        private static Dictionary<string, string>? configDictionary;

        /// <summary>Initializes static members of the <see cref="CyberSourcePaymentsProviderConfig"/> class.</summary>
        static CyberSourcePaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(CyberSourcePaymentsProviderConfig));
        }

        /// <summary>Gets the identifier of the merchant.</summary>
        /// <value>The identifier of the merchant.</value>
        [AppSettingsKey("Clarity.Payments.CyberSource.MerchantID.Test"),
            DefaultValue(null)]
        internal static string? MerchantIDTest
        {
            get => CEFConfigDictionary.TryGet<string?>(out var asValue, typeof(CyberSourcePaymentsProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CyberSourcePaymentsProviderConfig));
        }

        /// <summary>Gets the identifier of the merchant.</summary>
        /// <value>The identifier of the merchant.</value>
        [AppSettingsKey("Clarity.Payments.CyberSource.MerchantID.Prod"),
            DefaultValue(null)]
        internal static string? MerchantIDProd
        {
            get => CEFConfigDictionary.TryGet<string?>(out var asValue, typeof(CyberSourcePaymentsProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CyberSourcePaymentsProviderConfig));
        }

        /// <summary>Gets the merchant key.</summary>
        /// <value>The merchant key.</value>
        [AppSettingsKey("Clarity.Payments.CyberSource.MerchantKey.Test"),
            DefaultValue(null)]
        internal static string? MerchantKeyTest
        {
            get => CEFConfigDictionary.TryGet<string?>(out var asValue, typeof(CyberSourcePaymentsProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CyberSourcePaymentsProviderConfig));
        }

        /// <summary>Gets the merchant key.</summary>
        /// <value>The merchant key.</value>
        [AppSettingsKey("Clarity.Payments.CyberSource.MerchantKey.Prod"),
            DefaultValue(null)]
        internal static string? MerchantKeyProd
        {
            get => CEFConfigDictionary.TryGet<string?>(out var asValue, typeof(CyberSourcePaymentsProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CyberSourcePaymentsProviderConfig));
        }

        /// <summary>Gets the shared secret.</summary>
        /// <value>The shared secret.</value>
        [AppSettingsKey("Clarity.Payments.CyberSource.SharedSecret.Test"),
            DefaultValue(null)]
        internal static string? SharedSecretTest
        {
            get => CEFConfigDictionary.TryGet<string?>(out var asValue, typeof(CyberSourcePaymentsProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CyberSourcePaymentsProviderConfig));
        }

        /// <summary>Gets the shared secret.</summary>
        /// <value>The shared secret.</value>
        [AppSettingsKey("Clarity.Payments.CyberSource.SharedSecret.Prod"),
            DefaultValue(null)]
        internal static string? SharedSecretProd
        {
            get => CEFConfigDictionary.TryGet<string?>(out var asValue, typeof(CyberSourcePaymentsProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CyberSourcePaymentsProviderConfig));
        }

        /// <summary>Gets the shared secret.</summary>
        /// <value>The shared secret.</value>
        [AppSettingsKey("Clarity.Payments.CyberSource.KeysDirectory"),
            DefaultValue(null)]
        internal static string? KeysDirectory
        {
            get => CEFConfigDictionary.TryGet<string?>(out var asValue, typeof(CyberSourcePaymentsProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CyberSourcePaymentsProviderConfig));
        }

        /// <summary>Gets the shared secret.</summary>
        /// <value>The shared secret.</value>
        [AppSettingsKey("Clarity.Payments.CyberSource.KeyFileName.Test"),
            DefaultValue(null)]
        internal static string? KeyFileNameTest
        {
            get => CEFConfigDictionary.TryGet<string?>(out var asValue, typeof(CyberSourcePaymentsProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CyberSourcePaymentsProviderConfig));
        }

        /// <summary>Gets the shared secret.</summary>
        /// <value>The shared secret.</value>
        [AppSettingsKey("Clarity.Payments.CyberSource.KeyFileName.Prod"),
            DefaultValue(null)]
        internal static string? KeyFileNameProd
        {
            get => CEFConfigDictionary.TryGet<string?>(out var asValue, typeof(CyberSourcePaymentsProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(CyberSourcePaymentsProviderConfig));
        }

        /// <summary>Gets configuration dictionary.</summary>
        /// <returns>The configuration dictionary.</returns>
        internal static Dictionary<string, string> GetConfigDictionary()
        {
            if (configDictionary is not null)
            {
                return configDictionary;
            }
            configDictionary = new()
            {
                ["authenticationType"] = "HTTP_SIGNATURE",
                ["timeout"] = "300000",
                // Configs related to meta key
                ["portfolioID"] = string.Empty,
                ["useMetaKey"] = "false",
                // Configs related to OAuth
                ["enableClientCert"] = "false",
                ["clientCertDirectory"] = "Resource",
                ["clientCertFile"] = string.Empty,
                ["clientCertPassword"] = string.Empty,
                ["clientId"] = string.Empty,
                ["clientSecret"] = string.Empty,
            };
            if (CEFConfigDictionary.PaymentsProviderMode == Enums.PaymentProviderMode.Production)
            {
                configDictionary["runEnvironment"] = "api.cybersource.com";
                configDictionary["merchantID"] = Contract.RequiresValidKey(MerchantIDProd);
                configDictionary["merchantKeyId"] = Contract.RequiresValidKey(MerchantKeyProd);
                configDictionary["merchantsecretKey"] = Contract.RequiresValidKey(SharedSecretProd);
            }
            else
            {
                configDictionary["runEnvironment"] = "apitest.cybersource.com";
                configDictionary["merchantID"] = Contract.RequiresValidKey(MerchantIDTest);
                configDictionary["merchantKeyId"] = Contract.RequiresValidKey(MerchantKeyTest);
                configDictionary["merchantsecretKey"] = Contract.RequiresValidKey(SharedSecretTest);
            }
            return configDictionary;
        }

        /// <summary>Query if this payment provider settings are valid.</summary>
        /// <param name="isDefaultAndActivated">True if this provider is the default and default is activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<CyberSourcePaymentsProvider>() || isDefaultAndActivated)
                && (CEFConfigDictionary.PaymentsProviderMode == Enums.PaymentProviderMode.Production
                    && Contract.CheckAllValidKeys(MerchantIDProd, MerchantKeyProd, SharedSecretProd)
                    || Contract.CheckAllValidKeys(MerchantIDTest, MerchantKeyTest, SharedSecretTest));
    }
}
#endif
