// <copyright file="ElasticSearchingProviderConfig.Mods.Brands.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the searching boosts all brands.</summary>
        /// <value>The searching boosts all brands.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllBrands"),
            DefaultValue(110)]
        internal static double SearchingBoostsAllBrands
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts any brands.</summary>
        /// <value>The searching boosts any brands.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyBrands"),
            DefaultValue(110)]
        internal static double SearchingBoostsAnyBrands
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts single brand.</summary>
        /// <value>The searching boosts single brand.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleBrand"),
            DefaultValue(110)]
        internal static double SearchingBoostsSingleBrand
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
