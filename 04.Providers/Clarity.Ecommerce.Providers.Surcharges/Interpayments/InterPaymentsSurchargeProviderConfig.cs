// <copyright file="InterPaymentsSurchargeProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the interpayments surcharge provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Surcharges.InterPayments
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JSConfigs;

    /// <summary>Settings for <see cref="InterpaymentsSurchargeProvider"/>.</summary>
    [GeneratesAppSettings]
    public static class InterPaymentsSurchargeProviderConfig
    {
        /// <summary>Otherwise the above won't load.</summary>
        static InterPaymentsSurchargeProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(InterPaymentsSurchargeProviderConfig));
        }

        /// <summary>BaseURL. Example for testing: https://api-test.interpayments.com/v1.</summary>
        /// <value>The base URL.</value>
        [DefaultValue(null),
            AppSettingsKey("Clarity.FeatureSet.Surcharges.InterPayments.BaseURL")]
        public static string BaseURL { get; set; } = null!;

        /// <summary>API key for InterPayments.</summary>
        /// <value>The API key.</value>
        [DefaultValue(null),
            AppSettingsKey("Clarity.FeatureSet.Surcharges.InterPayments.APIKey")]
        public static string APIKey { get; set; } = null!;

        /// <summary>Check if InterPayments can be used.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<InterpaymentsSurchargeProvider>() || isDefaultAndActivated;
    }
}
