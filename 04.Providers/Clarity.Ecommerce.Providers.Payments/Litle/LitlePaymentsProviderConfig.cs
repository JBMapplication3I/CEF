// <copyright file="LitlePaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the litle configuration class</summary>
#if !NET5_0_OR_GREATER // Litle doesn't have .net 5.0+ builds (alternative available)
namespace Clarity.Ecommerce.Providers.Payments.LitleShip
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A litle configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class LitlePaymentsProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="LitlePaymentsProviderConfig" /> class.</summary>
        static LitlePaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(LitlePaymentsProviderConfig));
        }

        /// <summary>Gets the URL of the document.</summary>
        /// <value>The URL.</value>
        [AppSettingsKey("Clarity.Payment.Litle.URL"),
         DefaultValue(null)]
        internal static string? URL
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(LitlePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(LitlePaymentsProviderConfig));
        }

        /// <summary>Gets the group the report belongs to.</summary>
        /// <value>The report group.</value>
        [AppSettingsKey("Clarity.Payment.Litle.ReportGroup"),
         DefaultValue(null)]
        internal static string? ReportGroup
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(LitlePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(LitlePaymentsProviderConfig));
        }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        [AppSettingsKey("Clarity.Payment.Litle.Username"),
         DefaultValue(null)]
        internal static string? Username
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(LitlePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(LitlePaymentsProviderConfig));
        }

        /// <summary>Gets the version.</summary>
        /// <value>The version.</value>
        [AppSettingsKey("Clarity.Payment.Litle.Version"),
         DefaultValue(null)]
        internal static string? Version
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(LitlePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(LitlePaymentsProviderConfig));
        }

        /// <summary>Gets the timeout.</summary>
        /// <value>The timeout.</value>
        [AppSettingsKey("Clarity.Payment.Litle.Timeout"),
         DefaultValue(null)]
        internal static string? Timeout
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(LitlePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(LitlePaymentsProviderConfig));
        }

        /// <summary>Gets the identifier of the merchant.</summary>
        /// <value>The identifier of the merchant.</value>
        [AppSettingsKey("Clarity.Payment.Litle.MerchantId"),
         DefaultValue(null)]
        internal static string? MerchantId
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(LitlePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(LitlePaymentsProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Payment.Litle.Password"),
         DefaultValue(null)]
        internal static string? Password
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(LitlePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(LitlePaymentsProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<LitlePaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(URL, ReportGroup, Username, Version, Timeout, MerchantId, Password);
    }
}
#endif
