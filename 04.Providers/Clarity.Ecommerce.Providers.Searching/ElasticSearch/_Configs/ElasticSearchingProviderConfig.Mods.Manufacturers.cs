// <copyright file="ElasticSearchingProviderConfig.Mods.Manufacturers.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the searching boosts all manufacturers.</summary>
        /// <value>The searching boosts all manufacturers.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllManufacturers"),
            DefaultValue(110)]
        internal static double SearchingBoostsAllManufacturers
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }

        /// <summary>Gets the searching boosts any manufacturers.</summary>
        /// <value>The searching boosts any manufacturers.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyManufacturers"),
            DefaultValue(110)]
        internal static double SearchingBoostsAnyManufacturers
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }

        /// <summary>Gets the searching boosts single manufacturer.</summary>
        /// <value>The searching boosts single manufacturer.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleManufacturer"),
            DefaultValue(110)]
        internal static double SearchingBoostsSingleManufacturer
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }
    }
}
