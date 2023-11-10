// <copyright file="ElasticSearchingProviderConfig.Mods.Roles.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the searching boosts all roles.</summary>
        /// <value>The searching boosts all roles.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllRoles"),
            DefaultValue(110)]
        internal static double SearchingBoostsAllRoles
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts any roles.</summary>
        /// <value>The searching boosts any roles.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyRoles"),
            DefaultValue(110)]
        internal static double SearchingBoostsAnyRoles
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts single role.</summary>
        /// <value>The searching boosts single role.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleRole"),
            DefaultValue(110)]
        internal static double SearchingBoostsSingleRole
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
