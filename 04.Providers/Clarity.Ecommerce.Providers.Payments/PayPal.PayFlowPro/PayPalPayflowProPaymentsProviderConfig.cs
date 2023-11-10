// <copyright file="PayPalPayflowProPaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayPal payflow pro configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Payments.PayPalPayflowPro
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A PayPal Payflow Pro configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class PayPalPayflowProPaymentsProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="PayPalPayflowProPaymentsProviderConfig" /> class.</summary>
        static PayPalPayflowProPaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(PayPalPayflowProPaymentsProviderConfig));
        }

        /// <summary>Gets the name of the login user.</summary>
        /// <value>The name of the login user.</value>
        [AppSettingsKey("Clarity.Payments.PayPalPayflowPro.LoginUserName"),
         DefaultValue("clarity")]
        internal static string LoginUserName
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(PayPalPayflowProPaymentsProviderConfig)) ? asValue : "clarity";
            private set => CEFConfigDictionary.TrySet(value, typeof(PayPalPayflowProPaymentsProviderConfig));
        }

        /// <summary>Gets the login password.</summary>
        /// <value>The login password.</value>
        [AppSettingsKey("Clarity.Payments.PayPalPayflowPro.LoginPassword"),
         DefaultValue("Clarity1819")]
        internal static string LoginPassword
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(PayPalPayflowProPaymentsProviderConfig)) ? asValue : "Clarity1819";
            private set => CEFConfigDictionary.TrySet(value, typeof(PayPalPayflowProPaymentsProviderConfig));
        }

        /// <summary>Gets the login vendor.</summary>
        /// <value>The login vendor.</value>
        [AppSettingsKey("Clarity.Payments.PayPalPayflowPro.LoginVendor"),
         DefaultValue("iipaypaldev")]
        internal static string LoginVendor
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(PayPalPayflowProPaymentsProviderConfig)) ? asValue : "iipaypaldev";
            private set => CEFConfigDictionary.TrySet(value, typeof(PayPalPayflowProPaymentsProviderConfig));
        }

        /// <summary>Gets the login partner.</summary>
        /// <value>The login partner.</value>
        [AppSettingsKey("Clarity.Payments.PayPalPayflowPro.LoginPartner"),
         DefaultValue("PayPal")]
        internal static string LoginPartner
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(PayPalPayflowProPaymentsProviderConfig)) ? asValue : "PayPal";
            private set => CEFConfigDictionary.TrySet(value, typeof(PayPalPayflowProPaymentsProviderConfig));
        }

        /// <summary>Gets a value indicating whether the test mode is on.</summary>
        /// <value>True if test mode, false if not.</value>
        [AppSettingsKey("Clarity.Payments.PayPalPayflowPro.Mode"),
         DefaultValue(true)]
        internal static bool TestMode
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(PayPalPayflowProPaymentsProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayPalPayflowProPaymentsProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<PayPalPayflowProPaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(LoginUserName, LoginPassword, LoginVendor, LoginPartner);
    }
}
