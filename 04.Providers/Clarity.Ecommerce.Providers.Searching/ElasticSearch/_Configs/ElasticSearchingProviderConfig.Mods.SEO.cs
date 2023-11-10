// <copyright file="ElasticSearchingProviderConfig.Mods.SEO.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the searching boosts seo keywords function score factor.</summary>
        /// <value>The searching boosts seo keywords function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SeoKeywords.FunctionScore.Factor"),
            DefaultValue(125)]
        internal static double SearchingBoostsSeoKeywordsFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts seo keywords keyword function score factor.</summary>
        /// <value>The searching boosts seo keywords keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SeoKeywords.Keyword.FunctionScore.Factor"),
            DefaultValue(125)]
        internal static double SearchingBoostsSeoKeywordsKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts seo keywords match keyword.</summary>
        /// <value>The searching boosts seo keywords match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SeoKeywords.Match.Keyword"),
            DefaultValue(500)]
        internal static double SearchingBoostsSeoKeywordsMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts seo keywords prefix keyword.</summary>
        /// <value>The searching boosts seo keywords prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SeoKeywords.Prefix.Keyword"),
            DefaultValue(500)]
        internal static double SearchingBoostsSeoKeywordsPrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts seo keywords term keyword.</summary>
        /// <value>The searching boosts seo keywords term keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SeoKeywords.Term.Keyword"),
            DefaultValue(500)]
        internal static double SearchingBoostsSeoKeywordsTermKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
