// <copyright file="FedExShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FedEx shipping provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.FedEx
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;
    using Utilities;

    /// <content>Dictionary of FedEx CEF configurations.</content>
    [PublicAPI, GeneratesAppSettings]
    internal static partial class FedExShippingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="FedExShippingProviderConfig" /> class.</summary>
        static FedExShippingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets the account number.</summary>
        /// <value>The account number.</value>
        [AppSettingsKey("Clarity.Shipping.FedEx.AccountNumber"),
         DefaultValue("510087984")]
        internal static string? AccountNumber
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(FedExShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets the username.</summary>
        /// <value>The username.</value>
        [AppSettingsKey("Clarity.Shipping.FedEx.Username"),
         DefaultValue("a4NyFokkcUdkDJep")]
        internal static string UserName
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(FedExShippingProviderConfig)) ? asValue : "a4NyFokkcUdkDJep";
            private set => CEFConfigDictionary.TrySet(value, typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets the password.</summary>
        /// <value>The password.</value>
        [AppSettingsKey("Clarity.Shipping.FedEx.Password"),
         DefaultValue("pjqbKIUGlTwwExGqiLgmetdyw")]
        internal static string Password
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(FedExShippingProviderConfig)) ? asValue : "pjqbKIUGlTwwExGqiLgmetdyw";
            private set => CEFConfigDictionary.TrySet(value, typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets the meter number.</summary>
        /// <value>The meter number.</value>
        [AppSettingsKey("Clarity.Shipping.FedEx.MeterNumber"),
         DefaultValue("118681677")]
        internal static string MeterNumber
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(FedExShippingProviderConfig)) ? asValue : "118681677";
            private set => CEFConfigDictionary.TrySet(value, typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether this Provider use default minimum pricing.</summary>
        /// <value>True if use default minimum pricing, false if not.</value>
        /// <value>The meter number.</value>
        [AppSettingsKey("Clarity.Shipping.FedEx.UseDefaultMinimumPricing"),
         DefaultValue(false)]
        internal static bool UseDefaultMinimumPricing
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(FedExShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets the default minimum price.</summary>
        /// <value>The default minimum price.</value>
        [AppSettingsKey("Clarity.Shipping.FedEx.DefaultMinimumPrice"),
         DefaultValue(25)]
        internal static decimal DefaultMinimumPrice
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(FedExShippingProviderConfig)) ? asValue : 25m;
            private set => CEFConfigDictionary.TrySet(value, typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets the subtotal amount needed to include free shipping.</summary>
        /// <value>The subtotal amount needed to include free shipping.</value>
        [AppSettingsKey("Clarity.Shipping.FedEx.AmountToIncludeFreeShipping"),
         DefaultValue(0)]
        internal static decimal AmountToIncludeFreeShipping
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(FedExShippingProviderConfig)) ? asValue : 0m;
            private set => CEFConfigDictionary.TrySet(value, typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether this Provider use production.</summary>
        /// <value>True if use production, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.FedEx.UseProduction"),
         DefaultValue(false)]
        internal static bool UseProduction
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(FedExShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether if all packages in a shipment are combined and quoted as one package.</summary>
        /// <value>true if combine packages when getting shipping rate, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.FedEx.CombinePackagesWhenGettingShippingRate"),
         DefaultValue(true)]
        internal static bool CombinePackagesWhenGettingShippingRate
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(FedExShippingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether usage of Dimensional Weight for rate calculation.</summary>
        /// <value>true if use dimensional weight, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.FedEx.UseDimensionalWeight"),
         DefaultValue(false)]
        internal static bool UseDimensionalWeight
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(FedExShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets a value indicating whether Free Shipping will be included in the rates</summary>
        /// <value>True if free shipping is included, false if not.</value>
        [AppSettingsKey("Clarity.Shipping.FedEx.IncludeFreeShipping"),
         DefaultValue(false)]
        internal static bool IncludeFreeShipping
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(FedExShippingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets a list of specific rates to show, based on codes from FedEx.</summary>
        /// <value>A List of rate type includes.</value>
        [AppSettingsKey("Clarity.Shipping.FedEx.RateTypeIncludeList"),
         DefaultValue(null),
         SplitOn(new[] { ',', ';' })]
        internal static string[]? RateTypeIncludeList
        {
            get => CEFConfigDictionary.TryGet(out string[] asValue, typeof(FedExShippingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(FedExShippingProviderConfig));
        }

        /// <summary>Gets or sets a value indicating whether the provider on for testing should be forced.</summary>
        /// <value>True if force provider on for testing, false if not.</value>
        internal static bool ForceProviderOnForTesting { get; set; }

        /// <summary>Gets or sets a value indicating whether the no cache for testing should be forced.</summary>
        /// <value>True if force no cache for testing, false if not.</value>
        internal static bool ForceNoCacheForTesting { get; set; }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ForceProviderOnForTesting || ProviderConfig.CheckIsEnabledBySettings<FedExShippingProvider>() || isDefaultAndActivated)
                && Contract.CheckAllValidKeys(AccountNumber, UserName, Password, MeterNumber);

        /// <summary>Loads this FedExShippingProviderConfig.</summary>
        internal static void Load()
        {
            CEFConfigDictionary.Load(typeof(FedExShippingProviderConfig));
        }
    }
}
