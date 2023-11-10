// <copyright file="BNGPaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bng payments provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Payments.BNG
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A BNG Payments Provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class BNGPaymentsProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="BNGPaymentsProviderConfig" /> class.</summary>
        static BNGPaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(BNGPaymentsProviderConfig));
        }

        /// <summary>Gets the login.</summary>
        /// <value>The login.</value>
        [AppSettingsKey("Clarity.Payment.BNG.Login"),
         DefaultValue(null)]
        internal static string? Login
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(BNGPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(BNGPaymentsProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Payment.BNG.Password"),
         DefaultValue(null)]
        internal static string? Password
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(BNGPaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(BNGPaymentsProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<BNGPaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(Login, Password);
    }
}
