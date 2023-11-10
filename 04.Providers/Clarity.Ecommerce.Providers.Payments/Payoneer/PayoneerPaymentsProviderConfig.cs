// <copyright file="PayoneerPaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payoneer payments provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A payoneer payments provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class PayoneerPaymentsProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="PayoneerPaymentsProviderConfig" /> class.</summary>
        static PayoneerPaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(PayoneerPaymentsProviderConfig));
        }

        /// <summary>Gets the API key.</summary>
        /// <value>The API key.</value>
        [AppSettingsKey("Clarity.Payment.Payoneer.APIKey"),
         DefaultValue("7000e66bb0119800a4a96fef005e7a60")]
        internal static string APIKey
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(PayoneerPaymentsProviderConfig)) ? asValue : "7000e66bb0119800a4a96fef005e7a60";
            private set => CEFConfigDictionary.TrySet(value, typeof(PayoneerPaymentsProviderConfig));
        }

        /// <summary>Gets the API secret.</summary>
        /// <value>The API secret.</value>
        [AppSettingsKey("Clarity.Payment.Payoneer.APISecret"),
         DefaultValue("e4fbb5ede97ab7833e7e48ac522c39558b07be9ddcb0f91bc1d6d8f747126f0f")]
        internal static string APISecret
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(PayoneerPaymentsProviderConfig)) ? asValue : "e4fbb5ede97ab7833e7e48ac522c39558b07be9ddcb0f91bc1d6d8f747126f0f";
            private set => CEFConfigDictionary.TrySet(value, typeof(PayoneerPaymentsProviderConfig));
        }

        /// <summary>Gets the URL based on test mode.</summary>
        /// <value>The api URL.</value>
        internal static string GetURL => TestingMode == Enums.PaymentProviderMode.Production
            ? "https://api.armorpayments.com"
            : "https://sandbox.armorpayments.com";

        /// <summary>Gets the testing mode.</summary>
        /// <value>The testing mode.</value>
        private static Enums.PaymentProviderMode TestingMode { get; }
            = CEFConfigDictionary.PaymentsProviderMode;

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<PayoneerPaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(APIKey, APISecret);
    }
}
