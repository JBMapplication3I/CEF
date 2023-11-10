// <copyright file="ElasticSearchingProviderConfig.Mods.Regions.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the searching boosts any region.</summary>
        /// <value>The searching boosts any region.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyRegion"),
            DefaultValue(110)]
        internal static double SearchingBoostsAnyRegion
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts single region.</summary>
        /// <value>The searching boosts single region.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleRegion"),
            DefaultValue(110)]
        internal static double SearchingBoostsSingleRegion
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
