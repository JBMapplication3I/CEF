// <copyright file="UPSShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the UPS shipping provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Shipping.UPS
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using UPSPackageRateService;
    using Utilities;

    /// <summary>The ups configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class UPSShippingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="UPSShippingProviderConfig" /> class.</summary>
        static UPSShippingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(UPSShippingProviderConfig));
        }

        /// <summary>Gets the access license number.</summary>
        /// <value>The access license number.</value>
        [AppSettingsKey("Clarity.Shipping.UPS.AccessLicenseNumber"),
         DefaultValue("2CF4B451E1EAF296")]
        internal static string AccessLicenseNumber
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(UPSShippingProviderConfig)) ? asValue : "2CF4B451E1EAF296";
            private set => CEFConfigDictionary.TrySet(value, typeof(UPSShippingProviderConfig));
        }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        [AppSettingsKey("Clarity.Shipping.UPS.Username"),
         DefaultValue("ClarityTim")]
        internal static string UserName
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(UPSShippingProviderConfig)) ? asValue : "ClarityTim";
            private set => CEFConfigDictionary.TrySet(value, typeof(UPSShippingProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Shipping.UPS.Password"),
         DefaultValue("Thedoor7")]
        internal static string Password
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(UPSShippingProviderConfig)) ? asValue : "Thedoor7";
            private set => CEFConfigDictionary.TrySet(value, typeof(UPSShippingProviderConfig));
        }

        /// <summary>Gets the shipper number.</summary>
        /// <value>The shipper number.</value>
        [AppSettingsKey("Clarity.Shipping.UPS.ShipperNumber"),
         DefaultValue("85953Y")]
        internal static string ShipperNumber
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(UPSShippingProviderConfig)) ? asValue : "85953Y";
            private set => CEFConfigDictionary.TrySet(value, typeof(UPSShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether this UPSShippingProviderConfig use default minimum pricing.</summary>
        /// <value>True if use default minimum pricing, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.UPS.UseDefaultMinimumPricing"),
         DefaultValue(false)]
        internal static bool UseDefaultMinimumPricing
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(UPSShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(UPSShippingProviderConfig));
        }

        /// <summary>Gets the default minimum price.</summary>
        /// <value>The default minimum price.</value>
        [AppSettingsKey("Clarity.Shipping.UPS.DefaultMinimumPrice"),
         DefaultValue(25)]
        internal static decimal DefaultMinimumPrice
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(UPSShippingProviderConfig)) ? asValue : 25;
            private set => CEFConfigDictionary.TrySet(value, typeof(UPSShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether to determine if all packages in a shipment are combined and quoted as one package.</summary>
        /// <value>true if combine packages when getting shipping rate, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.UPS.CombinePackagesWhenGettingShippingRate"),
         DefaultValue(true)]
        internal static bool CombinePackagesWhenGettingShippingRate
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(UPSShippingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(UPSShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether to determine usage of Dimensional Weight for rate calculation.</summary>
        /// <value>true if use dimensional weight, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.UPS.UseDimensionalWeight"),
         DefaultValue(false)]
        internal static bool UseDimensionalWeight
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(UPSShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(UPSShippingProviderConfig));
        }

        /// <summary>Gets a list of specific rates to show, based on codes from FedEx.</summary>
        /// <value>A List of rate type includes.</value>
        [AppSettingsKey("Clarity.Shipping.UPS.RateTypeIncludeList"),
         DefaultValue(null),
         SplitOn(new[] { ',', ';' })]
        internal static string[]? RateTypeIncludeList
        {
            get => CEFConfigDictionary.TryGet(out string[]? asValue, typeof(UPSShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(UPSShippingProviderConfig));
        }

        /// <summary>Gets the security.</summary>
        /// <value>The security.</value>
        internal static UPSSecurity Security { get; } = new()
        {
            ServiceAccessToken = new() { AccessLicenseNumber = AccessLicenseNumber },
            UsernameToken = new() { Username = UserName, Password = Password },
        };

        /// <summary>The has valid configuration.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>True if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<UPSShippingProvider>() || isDefaultAndActivated)
            && Contract.CheckAllValidKeys(AccessLicenseNumber, UserName, Password, ShipperNumber);
    }
}
