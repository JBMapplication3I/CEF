// <copyright file="ElasticSearchingProviderConfig.Mods.Vendors.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the searching boosts all vendors.</summary>
        /// <value>The searching boosts all vendors.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllVendors"),
            DefaultValue(110)]
        internal static double SearchingBoostsAllVendors
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }

        /// <summary>Gets the searching boosts any vendors.</summary>
        /// <value>The searching boosts any vendors.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyVendors"),
            DefaultValue(110)]
        internal static double SearchingBoostsAnyVendors
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts single vendor.</summary>
        /// <value>The searching boosts single vendor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleVendor"),
            DefaultValue(110)]
        internal static double SearchingBoostsSingleVendor
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }
    }
}
