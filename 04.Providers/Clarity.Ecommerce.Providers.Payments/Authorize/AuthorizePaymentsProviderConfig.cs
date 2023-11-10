// <copyright file="AuthorizePaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authorize configuration class</summary>
#if !NET5_0_OR_GREATER // Authorize.NET doesn't have .net 5.0+ builds
namespace Clarity.Ecommerce.Providers.Payments.Authorize
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>An authorize configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class AuthorizePaymentsProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="AuthorizePaymentsProviderConfig" /> class.</summary>
        static AuthorizePaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(AuthorizePaymentsProviderConfig));
        }

        /// <summary>Gets the login.</summary>
        /// <value>The login.</value>
        [AppSettingsKey("Clarity.Payment.AuthorizeNet.Login"),
         DefaultValue("6tR6hJ74")]
        internal static string Login
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(AuthorizePaymentsProviderConfig)) ? asValue : "6tR6hJ74";
            private set => CEFConfigDictionary.TrySet(value, typeof(AuthorizePaymentsProviderConfig));
        }

        /// <summary>Gets the transaction key.</summary>
        /// <value>The transaction key.</value>
        [AppSettingsKey("Clarity.Payment.AuthorizeNet.TransactionKey"),
         DefaultValue("993W6a7bTJSt3ZjG")]
        internal static string TransactionKey
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(AuthorizePaymentsProviderConfig)) ? asValue : "993W6a7bTJSt3ZjG";
            private set => CEFConfigDictionary.TrySet(value, typeof(AuthorizePaymentsProviderConfig));
        }

        /// <summary>Gets the secret key.</summary>
        /// <value>The secret key.</value>
        [AppSettingsKey("Clarity.Payment.AuthorizeNet.SecretKey"),
         DefaultValue("Simon")]
        internal static string SecretKey
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(AuthorizePaymentsProviderConfig)) ? asValue : "Simon";
            private set => CEFConfigDictionary.TrySet(value, typeof(AuthorizePaymentsProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<AuthorizePaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(Login, TransactionKey);
    }
}
#endif
