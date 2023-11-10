// <copyright file="ElasticSearchingProviderConfig.Mods.Districts.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the searching boosts all districts.</summary>
        /// <value>The searching boosts all districts.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllDistricts"),
            DefaultValue(110)]
        internal static double SearchingBoostsAllDistricts
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts any districts.</summary>
        /// <value>The searching boosts any districts.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyDistricts"),
            DefaultValue(110)]
        internal static double SearchingBoostsAnyDistricts
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts single district.</summary>
        /// <value>The searching boosts single district.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleDistrict"),
            DefaultValue(110)]
        internal static double SearchingBoostsSingleDistrict
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
