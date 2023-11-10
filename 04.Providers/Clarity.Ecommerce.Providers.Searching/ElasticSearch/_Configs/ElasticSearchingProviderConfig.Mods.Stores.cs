// <copyright file="ElasticSearchingProviderConfig.Mods.Stores.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the searching boosts all stores.</summary>
        /// <value>The searching boosts all stores.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllStores"),
            DefaultValue(110)]
        internal static double SearchingBoostsAllStores
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts any stores.</summary>
        /// <value>The searching boosts any stores.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyStores"),
            DefaultValue(110)]
        internal static double SearchingBoostsAnyStores
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts single store.</summary>
        /// <value>The searching boosts single store.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleStore"),
            DefaultValue(110)]
        internal static double SearchingBoostsSingleStore
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
