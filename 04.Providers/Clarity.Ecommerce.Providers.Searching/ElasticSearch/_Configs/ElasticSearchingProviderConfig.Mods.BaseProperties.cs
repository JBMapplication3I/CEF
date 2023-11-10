// <copyright file="ElasticSearchingProviderConfig.Mods.BaseProperties.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the searching boosts custom key function score factor.</summary>
        /// <value>The searching boosts custom key function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CustomKey.FunctionScore.Factor"),
            DefaultValue(500)]
        internal static double SearchingBoostsCustomKeyFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts custom key keyword function score factor.</summary>
        /// <value>The searching boosts custom key keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CustomKey.Keyword.FunctionScore.Factor"),
            DefaultValue(500)]
        internal static double SearchingBoostsCustomKeyKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts custom key match keyword.</summary>
        /// <value>The searching boosts custom key match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CustomKey.Match.Keyword"),
            DefaultValue(15_000)]
        internal static double SearchingBoostsCustomKeyMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 15_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts custom key prefix keyword.</summary>
        /// <value>The searching boosts custom key prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CustomKey.Prefix.Keyword"),
            DefaultValue(15_000)]
        internal static double SearchingBoostsCustomKeyPrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 15_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts custom key term keyword.</summary>
        /// <value>The searching boosts custom key term keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CustomKey.Term.Keyword"),
            DefaultValue(15_000)]
        internal static double SearchingBoostsCustomKeyTermKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 15_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts name function score factor.</summary>
        /// <value>The searching boosts name function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.Name.FunctionScore.Factor"),
            DefaultValue(300)]
        internal static double SearchingBoostsNameFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 300;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts name keyword function score factor.</summary>
        /// <value>The searching boosts name keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.Name.Keyword.FunctionScore.Factor"),
            DefaultValue(300)]
        internal static double SearchingBoostsNameKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 300;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts name match keyword.</summary>
        /// <value>The searching boosts name match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.Name.Match.Keyword"),
            DefaultValue(10_000)]
        internal static double SearchingBoostsNameMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 10_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts name prefix keyword.</summary>
        /// <value>The searching boosts name prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.Name.Prefix.Keyword"),
            DefaultValue(10_000)]
        internal static double SearchingBoostsNamePrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 10_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts name term keyword.</summary>
        /// <value>The searching boosts name term keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.Name.Term.Keyword"),
            DefaultValue(10_000)]
        internal static double SearchingBoostsNameTermKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 10_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts description function score factor.</summary>
        /// <value>The searching boosts description function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.Description.FunctionScore.Factor"),
            DefaultValue(25)]
        internal static double SearchingBoostsDescriptionFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 25;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts function score maximum boost.</summary>
        /// <value>The searching boosts function score maximum boost.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.FunctionScore.MaxBoost"),
            DefaultValue(10)]
        internal static double SearchingBoostsFunctionScoreMaxBoost
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 10;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts city term keyword.</summary>
        /// <value>The searching boosts city term keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.City.Term.Keyword"),
            DefaultValue(110)]
        internal static double SearchingBoostsCityTermKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
