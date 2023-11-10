// <copyright file="YRCShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRC shipping provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Shipping.YRC
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A yrc shipping provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class YRCShippingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="YRCShippingProviderConfig" /> class.</summary>
        static YRCShippingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(YRCShippingProviderConfig));
        }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        [AppSettingsKey("Clarity.Shipping.YRC.Username"),
         DefaultValue(null)]
        internal static string? UserName
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(YRCShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(YRCShippingProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Shipping.YRC.Password"),
         DefaultValue(null)]
        internal static string? Password
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(YRCShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(YRCShippingProviderConfig));
        }

        /// <summary>Gets URL of the document.</summary>
        /// <value>The URL.</value>
        [AppSettingsKey("Clarity.Shipping.YRC.Url"),
         DefaultValue(null)]
        internal static string? Url
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(YRCShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(YRCShippingProviderConfig));
        }

        /// <summary>Gets the identifier of the bus.</summary>
        /// <value>The identifier of the bus.</value>
        [AppSettingsKey("Clarity.Shipping.YRC.BusId"),
         DefaultValue(null)]
        internal static string? BusId
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(YRCShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(YRCShippingProviderConfig));
        }

        /// <summary>Gets the bus role.</summary>
        /// <value>The bus role.</value>
        [AppSettingsKey("Clarity.Shipping.YRC.BusRole"),
         DefaultValue(null)]
        internal static string? BusRole
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(YRCShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(YRCShippingProviderConfig));
        }

        /// <summary>Gets the payment terms.</summary>
        /// <value>The payment terms.</value>
        [AppSettingsKey("Clarity.Shipping.YRC.PaymentTerms"),
         DefaultValue(null)]
        internal static string? PaymentTerms
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(YRCShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(YRCShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether this ShippingProvider should use default minimum pricing.</summary>
        /// <value>True if use default minimum pricing, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.YRC.UseDefaultMinimumPrice"),
         DefaultValue(false)]
        internal static bool UseDefaultMinimumPricing
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(YRCShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(YRCShippingProviderConfig));
        }

        /// <summary>Gets the default minimum price.</summary>
        /// <value>The default minimum price.</value>
        [AppSettingsKey("Clarity.Shipping.YRC.DefaultMinimumPrice"),
         DefaultValue(0)]
        internal static decimal DefaultMinimumPrice
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(YRCShippingProviderConfig)) ? asValue : 0;
            private set => CEFConfigDictionary.TrySet(value, typeof(YRCShippingProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<YRCShippingProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(UserName, Password, Url, BusId, BusRole, PaymentTerms);
    }
}
