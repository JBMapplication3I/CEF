// <copyright file="ElasticSearchingProviderConfig.Mods.PricingRanges.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System.ComponentModel;
    using JSConfigs;

    /// <summary>An elastic searching provider configuration.</summary>
    internal static partial class ElasticSearchingProviderConfig
    {
        /// <summary>Gets a value indicating whether the searching pricing ranges is enabled.</summary>
        /// <value>True if searching pricing ranges enabled, false if not.</value>
        [AppSettingsKey("Clarity.Searching.PricingRanges.Enabled"),
            DefaultValue(true)]
        internal static bool SearchingPricingRangesEnabled
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching pricing ranges increment 1 amount.</summary>
        /// <value>The searching pricing ranges increment 1 amount.</value>
        [AppSettingsKey("Clarity.Searching.PricingRanges.Increment1"),
            DefaultValue(50)]
        internal static double SearchingPricingRangesIncrement1Amount
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 50;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching pricing ranges increment 1 stop.</summary>
        /// <value>The searching pricing ranges increment 1 stop.</value>
        [AppSettingsKey("Clarity.Searching.PricingRanges.Increment1Stop"),
            DefaultValue(100)]
        internal static double SearchingPricingRangesIncrement1Stop
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 100;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching pricing ranges increment 2 amount.</summary>
        /// <value>The searching pricing ranges increment 2 amount.</value>
        [AppSettingsKey("Clarity.Searching.PricingRanges.Increment2"),
            DefaultValue(100)]
        internal static double? SearchingPricingRangesIncrement2Amount
        {
            get => CEFConfigDictionary.TryGet(out double? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 100;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching pricing ranges increment 2 stop.</summary>
        /// <value>The searching pricing ranges increment 2 stop.</value>
        [AppSettingsKey("Clarity.Searching.PricingRanges.Increment2Stop"),
            DefaultValue(500)]
        internal static double? SearchingPricingRangesIncrement2Stop
        {
            get => CEFConfigDictionary.TryGet(out double? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching pricing ranges increment 3 amount.</summary>
        /// <value>The searching pricing ranges increment 3 amount.</value>
        [AppSettingsKey("Clarity.Searching.PricingRanges.Increment3"),
            DefaultValue(500)]
        internal static double? SearchingPricingRangesIncrement3Amount
        {
            get => CEFConfigDictionary.TryGet(out double? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching pricing ranges increment 3 stop.</summary>
        /// <value>The searching pricing ranges increment 3 stop.</value>
        [AppSettingsKey("Clarity.Searching.PricingRanges.Increment3Stop"),
            DefaultValue(1_000)]
        internal static double? SearchingPricingRangesIncrement3Stop
        {
            get => CEFConfigDictionary.TryGet(out double? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 1_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching pricing ranges increment 4 amount.</summary>
        /// <value>The searching pricing ranges increment 4 amount.</value>
        [AppSettingsKey("Clarity.Searching.PricingRanges.Increment4"),
            DefaultValue(1_000)]
        internal static double? SearchingPricingRangesIncrement4Amount
        {
            get => CEFConfigDictionary.TryGet(out double? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 1_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching pricing ranges increment 4 stop.</summary>
        /// <value>The searching pricing ranges increment 4 stop.</value>
        [AppSettingsKey("Clarity.Searching.PricingRanges.Increment4Stop"),
            DefaultValue(5_000)]
        internal static double? SearchingPricingRangesIncrement4Stop
        {
            get => CEFConfigDictionary.TryGet(out double? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 5_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts pricing ranges.</summary>
        /// <value>The searching boosts pricing ranges.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.PricingRanges"),
            DefaultValue(110)]
        internal static double SearchingBoostsPricingRanges
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
