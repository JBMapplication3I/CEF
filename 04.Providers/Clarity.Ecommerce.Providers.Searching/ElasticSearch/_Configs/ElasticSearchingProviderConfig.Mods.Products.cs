// <copyright file="ElasticSearchingProviderConfig.Mods.Products.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the searching boosts all products.</summary>
        /// <value>The searching boosts all products.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllProducts"),
            DefaultValue(110)]
        internal static double SearchingBoostsAllProducts
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }

        /// <summary>Gets the searching boosts any products.</summary>
        /// <value>The searching boosts any products.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyProducts"),
            DefaultValue(110)]
        internal static double SearchingBoostsAnyProducts
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }

        /// <summary>Gets the searching boosts single product.</summary>
        /// <value>The searching boosts single product.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleProduct"),
            DefaultValue(110)]
        internal static double SearchingBoostsSingleProduct
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }
    }
}
