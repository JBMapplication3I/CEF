// <copyright file="USPSShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the usps shipping provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Shipping.USPS
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <summary>The usps shipping provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class USPSShippingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="USPSShippingProviderConfig" /> class.</summary>
        static USPSShippingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(USPSShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether to determine if all packages in a shipment are combined and quoted as one package.</summary>
        /// <value>true if combine packages when getting shipping rate, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.USPS.CombinePackagesWhenGettingShippingRate"),
         DefaultValue(true)]
        internal static bool CombinePackagesWhenGettingShippingRate
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(USPSShippingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(USPSShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether to determine usage of Dimensional Weight for rate calculation.</summary>
        /// <value>true if use dimensional weight, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.USPS.UseDimensionalWeight"),
         DefaultValue(false)]
        internal static bool UseDimensionalWeight
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(USPSShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(USPSShippingProviderConfig));
        }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        [AppSettingsKey("Clarity.Shipping.USPS.USPS.Username"),
         DefaultValue(null)]
        internal static string? UserName
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(USPSShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(USPSShippingProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Shipping.USPS.USPS.Password"),
         DefaultValue(null)]
        internal static string? Password
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(USPSShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(USPSShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether this Config use default minimum pricing.</summary>
        /// <value>True if use default minimum pricing, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.USPS.UseDefaultMinimumPricing"),
         DefaultValue(false)]
        internal static bool UseDefaultMinimumPricing
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(USPSShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(USPSShippingProviderConfig));
        }

        /// <summary>Gets the default minimum price.</summary>
        /// <value>The default minimum price.</value>
        [AppSettingsKey("Clarity.Shipping.USPS.USPS.Password"),
         DefaultValue(25)]
        internal static decimal DefaultMinimumPrice
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(USPSShippingProviderConfig)) ? asValue : 25;
            private set => CEFConfigDictionary.TrySet(value, typeof(USPSShippingProviderConfig));
        }

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<USPSShippingProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(UserName, Password);
    }
}
