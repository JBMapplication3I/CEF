// <copyright file="ElasticSearchingProviderConfig.Mods.Franchises.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the searching boosts all franchises.</summary>
        /// <value>The searching boosts all franchises.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllFranchises"),
            DefaultValue(110)]
        internal static double SearchingBoostsAllFranchises
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }

        /// <summary>Gets the searching boosts any franchises.</summary>
        /// <value>The searching boosts any franchises.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyFranchises"),
            DefaultValue(110)]
        internal static double SearchingBoostsAnyFranchises
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }

        /// <summary>Gets the searching boosts single franchise.</summary>
        /// <value>The searching boosts single franchise.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleFranchise"),
            DefaultValue(110)]
        internal static double SearchingBoostsSingleFranchise
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }
    }
}
