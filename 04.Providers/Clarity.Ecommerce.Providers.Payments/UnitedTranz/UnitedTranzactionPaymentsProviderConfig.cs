// <copyright file="UnitedTranzactionPaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the united tranzaction payments provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Payments.UnitedTranzaction
{
    using System.ComponentModel;
    using System.IO;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>An united tranzaction payments provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class UnitedTranzactionPaymentsProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="UnitedTranzactionPaymentsProviderConfig" /> class.</summary>
        static UnitedTranzactionPaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(UnitedTranzactionPaymentsProviderConfig));
        }

        /// <summary>Gets URL of the document.</summary>
        /// <value>The URL.</value>
        [AppSettingsKey("Clarity.Payments.UnitedTranzaction.Url"),
         DefaultValue(null)]
        internal static string? URL
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(UnitedTranzactionPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(UnitedTranzactionPaymentsProviderConfig));
        }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        [AppSettingsKey("Clarity.Payments.UnitedTranzaction.UserName"),
         DefaultValue(null)]
        internal static string? UserName
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(UnitedTranzactionPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(UnitedTranzactionPaymentsProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Payments.UnitedTranzaction.Password"),
         DefaultValue(null)]
        internal static string? Password
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(UnitedTranzactionPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(UnitedTranzactionPaymentsProviderConfig));
        }

        /// <summary>Gets the merchant no.</summary>
        /// <value>The merchant no.</value>
        [AppSettingsKey("Clarity.Payments.UnitedTranzaction.MerchantNo"),
         DefaultValue(null)]
        internal static string? MerchantNo
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(UnitedTranzactionPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(UnitedTranzactionPaymentsProviderConfig));
        }

        /// <summary>Gets the full pathname of the Cc template file.</summary>
        /// <value>The full pathname of the Cc template file.</value>
        internal static string CCTemplatePath { get; }
            = Path.Combine(CEFConfigDictionary.PluginsPath, "UT_CCSaleTemplate.xml");

        /// <summary>Gets the full pathname of the Cc authentication template file.</summary>
        /// <value>The full pathname of the Cc authentication template file.</value>
        internal static string CCAuthTemplatePath { get; }
            = Path.Combine(CEFConfigDictionary.PluginsPath, "UT_CCAuthorizeTemplate.xml");

        /// <summary>Gets the full pathname of the Cc capture template file.</summary>
        /// <value>The full pathname of the Cc capture template file.</value>
        internal static string CCCaptureTemplatePath { get; }
            = Path.Combine(CEFConfigDictionary.PluginsPath, "UT_CCCaptureTemplate.xml");

        /// <summary>Gets the full pathname of the Cc token template file.</summary>
        /// <value>The full pathname of the Cc token template file.</value>
        internal static string CCTokenTemplatePath { get; }
            = Path.Combine(CEFConfigDictionary.PluginsPath, "UT_CCSalesWithTokenTemplate.xml");

        /// <summary>Gets the full pathname of the add customer profile template file.</summary>
        /// <value>The full pathname of the add customer profile template file.</value>
        internal static string AddCustomerProfileTemplatePath { get; }
            = Path.Combine(CEFConfigDictionary.PluginsPath, "UT_AddCustomerProfile.xml");

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<UnitedTranzactionPaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(URL, UserName, Password, MerchantNo);
    }
}
