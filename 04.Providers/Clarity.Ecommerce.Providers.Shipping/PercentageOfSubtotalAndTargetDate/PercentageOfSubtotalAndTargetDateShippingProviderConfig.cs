// <copyright file="PercentageOfSubtotalAndTargetDateShippingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the percentage of subtotal and target date shipping provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Shipping.PercentageOfSubtotalAndTargetDate
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;

    /// <summary>A percentage of subtotal and target date shipping provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class PercentageOfSubtotalAndTargetDateShippingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="PercentageOfSubtotalAndTargetDateShippingProviderConfig" /> class.</summary>
        static PercentageOfSubtotalAndTargetDateShippingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(PercentageOfSubtotalAndTargetDateShippingProviderConfig));
        }

        /// <summary>Gets the rate percent standard US continental.</summary>
        /// <value>The rate percent standard US continental.</value>
        [AppSettingsKey("Clarity.Shipping.PercOfSubTotal.RatePercent.Standard.USContinental"),
         DefaultValue(0)]
        internal static decimal RatePercentStandardUSContinental
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(PercentageOfSubtotalAndTargetDateShippingProviderConfig)) ? asValue : 0;
            private set => CEFConfigDictionary.TrySet(value, typeof(PercentageOfSubtotalAndTargetDateShippingProviderConfig));
        }

        /// <summary>Gets the rate percent standard US: Non-continental.</summary>
        /// <value>The rate percent standard US: Non-continental.</value>
        [AppSettingsKey("Clarity.Shipping.PercOfSubTotal.RatePercent.Standard.USNonContinental"),
         DefaultValue(0)]
        internal static decimal RatePercentStandardUSNonContinental
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(PercentageOfSubtotalAndTargetDateShippingProviderConfig)) ? asValue : 0;
            private set => CEFConfigDictionary.TrySet(value, typeof(PercentageOfSubtotalAndTargetDateShippingProviderConfig));
        }

        /// <summary>Gets the rate percent 2 day.</summary>
        /// <value>The rate percent 2 day.</value>
        [AppSettingsKey("Clarity.Shipping.PercOfSubTotal.RatePercent.2Day"),
         DefaultValue(0)]
        internal static decimal RatePercent2Day
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(PercentageOfSubtotalAndTargetDateShippingProviderConfig)) ? asValue : 0;
            private set => CEFConfigDictionary.TrySet(value, typeof(PercentageOfSubtotalAndTargetDateShippingProviderConfig));
        }

        /// <summary>Gets the rate percent 1 day.</summary>
        /// <value>The rate percent 1 day.</value>
        [AppSettingsKey("Clarity.Shipping.PercOfSubTotal.RatePercent.1Day"),
         DefaultValue(0)]
        internal static decimal RatePercent1Day
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(PercentageOfSubtotalAndTargetDateShippingProviderConfig)) ? asValue : 0;
            private set => CEFConfigDictionary.TrySet(value, typeof(PercentageOfSubtotalAndTargetDateShippingProviderConfig));
        }

        /// <summary>Gets the default RatePercentStandard2Day.</summary>
        /// <value>The default RatePercentStandard2Day.</value>
        [AppSettingsKey("Clarity.Shipping.PercOfSubTotal.MinimumShippingRate"),
         DefaultValue(0)]
        internal static decimal MinimumShippingRate
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(PercentageOfSubtotalAndTargetDateShippingProviderConfig)) ? asValue : 0;
            private set => CEFConfigDictionary.TrySet(value, typeof(PercentageOfSubtotalAndTargetDateShippingProviderConfig));
        }

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<PercentageOfSubtotalAndTargetDateShippingProvider>() || isDefaultAndActivated;
    }
}
