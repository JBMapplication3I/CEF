// <copyright file="PayTracePaymentsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace configuration class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A PayTrace configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    public static class PayTracePaymentsProviderConfig
    {
        /// <summary>APIVersion contains Version of API.</summary>
        public const string ApiVersion = "/v1";

        /// <summary>Url for OAuth Token.</summary>
        public const string UrlOAuth = "/oauth/token";

        /// <summary>URL for Keyed Sale.</summary>
        public const string UrlKeyedSale = ApiVersion + "/transactions/sale/keyed";

        /// <summary>URL for Swiped Sale.</summary>
        public const string UrlSwipedSale = ApiVersion + "/transactions/sale/swiped";

        /// <summary>URL for Keyed Authorization.</summary>
        public const string UrlKeyedAuthorization = ApiVersion + "/transactions/authorization/keyed";

        /// <summary>URL for Keyed Refund.</summary>
        public const string UrlKeyedRefund = ApiVersion + "/transactions/refund/keyed";

        /// <summary>URL for Capture Transaction.</summary>
        public const string UrlCapture = ApiVersion + "/transactions/authorization/capture";

        /// <summary>URL for Void Sale Transaction.</summary>
        public const string UrlVoidTransaction = ApiVersion + "/transactions/void";

        /// <summary>URL for Transactions export by date range.</summary>
        public const string UrlExportTransactions = ApiVersion + "/transactions/export/by_date_range";

        /// <summary>URL for Transactions export by id.</summary>
        public const string UrlExportTransaction = ApiVersion + "/transactions/export/by_id";

        /// <summary>URL for Vault Sale by CustomerId Method.</summary>
        public const string UrlExportCustomer = ApiVersion + "/customer/export";

        /// <summary>URL for Vault Sale by CustomerId Method.</summary>
        public const string UrlCreateCustomer = ApiVersion + "/customer/create";

        /// <summary>URL for Vault Sale by CustomerId Method.</summary>
        public const string UrlUpdateCustomer = ApiVersion + "/customer/update";

        /// <summary>URL for Vault Sale by CustomerId Method.</summary>
        public const string UrlDeleteCustomer = ApiVersion + "/customer/delete";

        /// <summary>URL for Vault Sale by CustomerId Method.</summary>
        public const string UrlVaultSaleByCustomerId = ApiVersion + "/transactions/sale/by_customer";

        /// <summary>Initializes static members of the <see cref="PayTracePaymentsProviderConfig" /> class.</summary>
        static PayTracePaymentsProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(PayTracePaymentsProviderConfig));
        }

        /// <summary>Gets identifier for the UserName.</summary>
        /// <value>The identifier of the UserName.</value>
        [AppSettingsKey("Clarity.Payment.PayTrace.UserName"),
         DefaultValue(null)]
        public static string? Username
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayTracePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayTracePaymentsProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Payment.PayTrace.Password"),
         DefaultValue(null)]
        public static string? Password
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayTracePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayTracePaymentsProviderConfig));
        }

        /// <summary>Gets Identifier for the key.</summary>
        /// <value>The identifier of the key.</value>
        [AppSettingsKey("Clarity.Payment.PayTrace.IntegratorId"),
         DefaultValue(null)]
        public static string? IntegratorId
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayTracePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayTracePaymentsProviderConfig));
        }

        /// <summary>Gets URL of the api.</summary>
        /// <value>The api URL.</value>
        [AppSettingsKey("Clarity.Payment.PayTrace.Url"),
         DefaultValue(null)]
        public static string? Url
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(PayTracePaymentsProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(PayTracePaymentsProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        public static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<PayTracePaymentsProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(Username, Password, IntegratorId);
    }
}
