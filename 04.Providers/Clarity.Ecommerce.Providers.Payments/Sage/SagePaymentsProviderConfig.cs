// <copyright file="SagePaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage payments provider configuration class</summary>
#nullable enable
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A sage payments provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class SagePaymentsProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="SagePaymentsProviderConfig" /> class.</summary>
        static SagePaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(SagePaymentsProviderConfig));
        }

        /// <summary>Gets the identifier of the client.</summary>
        /// <value>The identifier of the client.</value>
        [AppSettingsKey("Clarity.Payment.Sage.ClientId"),
         DefaultValue(null)]
        internal static string? ClientID
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(SagePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(SagePaymentsProviderConfig));
        }

        /// <summary>Gets the client secret.</summary>
        /// <value>The client secret.</value>
        [AppSettingsKey("Clarity.Payment.Sage.ClientSecret"),
         DefaultValue(null)]
        internal static string? ClientSecret
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(SagePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(SagePaymentsProviderConfig));
        }

        /// <summary>Gets the identifier of the merchant.</summary>
        /// <value>The identifier of the merchant.</value>
        [AppSettingsKey("Clarity.Payment.Sage.MerchantID"),
         DefaultValue(null)]
        internal static string? MerchantID
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(SagePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(SagePaymentsProviderConfig));
        }

        /// <summary>Gets the merchant key.</summary>
        /// <value>The merchant key.</value>
        [AppSettingsKey("Clarity.Payment.Sage.MerchantKey"),
         DefaultValue(null)]
        internal static string? MerchantKey
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(SagePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(SagePaymentsProviderConfig));
        }

        /// <summary>Gets URL of the Cc.</summary>
        /// <value>The Cc URL.</value>
        [AppSettingsKey("Clarity.Payment.Sage.Cc_Url"),
         DefaultValue(null)]
        internal static string? CreditCardUrl
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(SagePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(SagePaymentsProviderConfig));
        }

        /// <summary>Gets URL of the check.</summary>
        /// <value>The e check URL.</value>
        // ReSharper disable once InconsistentNaming
        [AppSettingsKey("Clarity.Payment.Sage.ECheck_Url"),
         DefaultValue(null)]
        // ReSharper disable once InconsistentNaming
        internal static string? eCheckUrl
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(SagePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(SagePaymentsProviderConfig));
        }

        /// <summary>Gets URL of the token.</summary>
        /// <value>The token URL.</value>
        [AppSettingsKey("Clarity.Payment.Sage.Token_Url"),
         DefaultValue(null)]
        internal static string? TokenUrl
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(SagePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(SagePaymentsProviderConfig));
        }

        /// <summary>Gets URL of the delete.</summary>
        /// <value>The delete URL.</value>
        [AppSettingsKey("Clarity.Payment.Sage.Delete_Url"),
         DefaultValue(null)]
        internal static string? DeleteUrl
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(SagePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(SagePaymentsProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<SagePaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(ClientID, ClientSecret, MerchantID, MerchantKey);
    }
}
