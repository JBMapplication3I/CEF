// <copyright file="ElasticSearchingProviderConfig.Mods.Types.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the searching boosts any type.</summary>
        /// <value>The searching boosts any type.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyType"),
            DefaultValue(110)]
        internal static double SearchingBoostsAnyType
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts single type.</summary>
        /// <value>The searching boosts single type.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleType"),
            DefaultValue(110)]
        internal static double SearchingBoostsSingleType
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
