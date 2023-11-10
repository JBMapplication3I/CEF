// <copyright file="BraintreePaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// ReSharper disable UnusedMember.Local
namespace Clarity.Ecommerce.Providers.Payments.BraintreePayments
{
    using System.ComponentModel;
    using Braintree;
    using Interfaces.Providers;
    using JSConfigs;
    using Utilities;

    /// <summary>A PayPal Payflow Pro configuration.</summary>
    [GeneratesAppSettings]
    internal static class BraintreePaymentsProviderConfig
    {
        /// <summary>The environment used by the Braintree gateway.</summary>
        /// <value>The environment.</value>
        [AppSettingsKey("Clarity.Payments.Braintree.Environment"), DefaultValue("SANDBOX")]
        internal static Environment Environment
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(BraintreePaymentsProviderConfig)) && asValue == "PRODUCTION" ? Environment.PRODUCTION : Environment.SANDBOX;
            private set => CEFConfigDictionary.TrySet(value, typeof(BraintreePaymentsProviderConfig));
        }

        /// <summary>The merchant ID used by the Braintree gateway.</summary>
        /// <value>The identifier of the merchant.</value>
        [AppSettingsKey("Clarity.Payments.Braintree.MerchantID"), DefaultValue(null)]
        internal static string? MerchantId
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(BraintreePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(BraintreePaymentsProviderConfig));
        }

        /// <summary>The mechant account to be used by the Braintree gateway.</summary>
        /// <value>The identifier of the merchant account.</value>
        [AppSettingsKey("Clarity.Payments.Braintree.MerchantAccountID"), DefaultValue(null)]
        internal static string? MerchantAccountID
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(BraintreePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(BraintreePaymentsProviderConfig));
        }

        /// <summary>The public key used by the Braintree gateway.</summary>
        /// <value>The public key.</value>
        [AppSettingsKey("Clarity.Payments.Braintree.PublicKey"), DefaultValue(null)]
        internal static string? PublicKey
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(BraintreePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(BraintreePaymentsProviderConfig));
        }

        /// <summary>The private key used by the Braintree gateway.</summary>
        /// <value>The private key.</value>
        [AppSettingsKey("Clarity.Payments.Braintree.PrivateKey"), DefaultValue(null)]
        internal static string? PrivateKey
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(BraintreePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(BraintreePaymentsProviderConfig));
        }

        /// <summary>The currency ISO Code indicating which type of currency is being used.</summary>
        /// <value>The currency ISO code.</value>
        [AppSettingsKey("Clarity.Payments.Braintree.CurrencyIsoCode"), DefaultValue(null)]
        internal static string? CurrencyIsoCode
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(BraintreePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(BraintreePaymentsProviderConfig));
        }

        /// <summary>The provider has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<BraintreePaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(MerchantId, PrivateKey, PublicKey);
    }
}
