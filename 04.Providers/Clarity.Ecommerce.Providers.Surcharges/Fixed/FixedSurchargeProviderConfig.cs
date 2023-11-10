// <copyright file="FixedSurchargeProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the fixed surcharge provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Surcharges.Fixed
{
    using System.ComponentModel;
    using JSConfigs;

    /// <summary>Surcharge provider that always returns $0.00 and for which completions/cancellations are no-ops.</summary>
    public static class FixedSurchargeProviderConfig
    {
        /// <summary>Otherwise the settings won't load.</summary>
        static FixedSurchargeProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(FixedSurchargeProviderConfig));
        }

        /// <summary>If set, the fixed surcharge provider will always give a surcharge of this percentage.</summary>
        /// <value>The standard percent.</value>
        [AppSettingsKey("Clarity.FeatureSet.Surcharges.Fixed.StandardPercent"),
            DefaultValue(null)]
        public static decimal? StandardPercent { get; set; }

        /// <summary>Check if the mock surcharge provider can be used.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated) => true;
    }
}
