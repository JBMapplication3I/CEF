// <copyright file="OrbitalPaymentechPaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the orbital paymentech payments provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Payments.OrbitalPaymentech
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>An orbital paymentech payments provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class OrbitalPaymentechPaymentsProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="OrbitalPaymentechPaymentsProviderConfig" /> class.</summary>
        static OrbitalPaymentechPaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(OrbitalPaymentechPaymentsProviderConfig));
        }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        [AppSettingsKey("Clarity.Payment.OrbitalPaymentech.Username"),
         DefaultValue("T1812345678USD")]
        internal static string Username
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(OrbitalPaymentechPaymentsProviderConfig)) ? asValue : "T1812345678USD";
            private set => CEFConfigDictionary.TrySet(value, typeof(OrbitalPaymentechPaymentsProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Payment.OrbitalPaymentech.Password"),
         DefaultValue("3P9Q4444444413")]
        internal static string Password
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(OrbitalPaymentechPaymentsProviderConfig)) ? asValue : "3P9Q4444444413";
            private set => CEFConfigDictionary.TrySet(value, typeof(OrbitalPaymentechPaymentsProviderConfig));
        }

        /// <summary>Gets the identifier of the merchant.</summary>
        /// <value>The identifier of the merchant.</value>
        [AppSettingsKey("Clarity.Payment.OrbitalPaymentech.MerchantID"),
         DefaultValue("123456")]
        internal static string MerchantID
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(OrbitalPaymentechPaymentsProviderConfig)) ? asValue : "123456";
            private set => CEFConfigDictionary.TrySet(value, typeof(OrbitalPaymentechPaymentsProviderConfig));
        }

        /// <summary>Gets URL of the test.</summary>
        /// <value>The test URL.</value>
        [AppSettingsKey("Clarity.Payment.OrbitalPaymentech.TestUrl"),
         DefaultValue("https://wsvar1.chasepaymentech.com/PaymentechGateway")]
        internal static string TestUrl
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(OrbitalPaymentechPaymentsProviderConfig))
                ? asValue
                : "https://wsvar1.chasepaymentech.com/PaymentechGateway";
            private set => CEFConfigDictionary.TrySet(value, typeof(OrbitalPaymentechPaymentsProviderConfig));
        }

        /// <summary>Gets URL of the product.</summary>
        /// <value>The product URL.</value>
        [AppSettingsKey("Clarity.Payment.OrbitalPaymentech.ProductionUrl"),
         DefaultValue("https://ws1.chasepaymentech.com/PaymentechGateway")]
        internal static string ProdUrl
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(OrbitalPaymentechPaymentsProviderConfig))
                ? asValue
                : "https://ws1.chasepaymentech.com/PaymentechGateway";
            private set => CEFConfigDictionary.TrySet(value, typeof(OrbitalPaymentechPaymentsProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<OrbitalPaymentechPaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(Username, Password, MerchantID, TestUrl, ProdUrl);
    }
}
