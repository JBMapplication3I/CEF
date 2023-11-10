// <copyright file="TNTShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tnt shipping provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Shipping.TNT
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>A tnt shipping provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class TNTShippingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="TNTShippingProviderConfig" /> class.</summary>
        static TNTShippingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(TNTShippingProviderConfig));
        }

        /// <summary>Gets the identifier of the customer.</summary>
        /// <value>The identifier of the customer.</value>
        [AppSettingsKey("Clarity.Shipping.TNT.CustomerID"),
         DefaultValue("A07923")]
        internal static string CustomerID
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(TNTShippingProviderConfig)) ? asValue : "A07923";
            private set => CEFConfigDictionary.TrySet(value, typeof(TNTShippingProviderConfig));
        }

        /// <summary>Gets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [AppSettingsKey("Clarity.Shipping.TNT.CustomerID"),
         DefaultValue("EXPRESSLABEL")]
        internal static string UserID
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(TNTShippingProviderConfig)) ? asValue : "EXPRESSLABEL";
            private set => CEFConfigDictionary.TrySet(value, typeof(TNTShippingProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Shipping.TNT.CustomerID"),
         DefaultValue("EXPRESSLABEL")]
        internal static string Password
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(TNTShippingProviderConfig)) ? asValue : "EXPRESSLABEL";
            private set => CEFConfigDictionary.TrySet(value, typeof(TNTShippingProviderConfig));
        }

        /// <summary>Gets URL of the document.</summary>
        /// <value>The URL.</value>
        [AppSettingsKey("Clarity.Shipping.TNT.CustomerID"),
         DefaultValue("https://www.mytnt.it/XMLServices")]
        internal static string URL
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(TNTShippingProviderConfig)) ? asValue : "https://www.mytnt.it/XMLServices";
            private set => CEFConfigDictionary.TrySet(value, typeof(TNTShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether this Config use default minimum pricing.</summary>
        /// <value>True if use default minimum pricing, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.TNT.UseDefaultMinimumPricing"),
         DefaultValue(false)]
        internal static bool UseDefaultMinimumPricing
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(TNTShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(TNTShippingProviderConfig));
        }

        /// <summary>Gets the default minimum price.</summary>
        /// <value>The default minimum price.</value>
        [AppSettingsKey("Clarity.Shipping.TNT.DefaultMinimumPrice"),
         DefaultValue(0)]
        internal static decimal DefaultMinimumPrice
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(TNTShippingProviderConfig)) ? asValue : 0;
            private set => CEFConfigDictionary.TrySet(value, typeof(TNTShippingProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<TNTShippingProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(CustomerID, UserID, Password, URL);
    }
}
