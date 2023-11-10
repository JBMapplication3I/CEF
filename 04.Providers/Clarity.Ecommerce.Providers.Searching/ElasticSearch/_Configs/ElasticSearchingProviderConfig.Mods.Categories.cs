// <copyright file="ElasticSearchingProviderConfig.Mods.Categories.cs" company="clarity-ventures.com">
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
        /// <summary>Gets the categories the searching boosts all belongs to.</summary>
        /// <value>The searching boosts all categories.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllCategories"),
            DefaultValue(110)]
        internal static double SearchingBoostsAllCategories
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the category the searching boosts any belongs to.</summary>
        /// <value>The searching boosts any category.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyCategory"),
            DefaultValue(110)]
        internal static double SearchingBoostsAnyCategory
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the category the searching boosts single belongs to.</summary>
        /// <value>The searching boosts single category.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleCategory"),
            DefaultValue(110)]
        internal static double SearchingBoostsSingleCategory
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name primary function score factor.</summary>
        /// <value>The searching boosts category name primary function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNamePrimary.FunctionScore.Factor"),
            DefaultValue(250)]
        internal static double SearchingBoostsCategoryNamePrimaryFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 250;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name primary keyword function score factor.</summary>
        /// <value>The searching boosts category name primary keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNamePrimary.Keyword.FunctionScore.Factor"),
            DefaultValue(300)]
        internal static double SearchingBoostsCategoryNamePrimaryKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 300;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name primary match keyword.</summary>
        /// <value>The searching boosts category name primary match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNamePrimary.Match.Keyword"),
            DefaultValue(1_000)]
        internal static double SearchingBoostsCategoryNamePrimaryMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 1_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name primary prefix keyword.</summary>
        /// <value>The searching boosts category name primary prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNamePrimary.Prefix.Keyword"),
            DefaultValue(500)]
        internal static double SearchingBoostsCategoryNamePrimaryPrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name primary term keyword.</summary>
        /// <value>The searching boosts category name primary term keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNamePrimary.Term.Keyword"),
            DefaultValue(500)]
        internal static double SearchingBoostsCategoryNamePrimaryTermKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name secondary function score factor.</summary>
        /// <value>The searching boosts category name secondary function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameSecondary.FunctionScore.Factor"),
            DefaultValue(125)]
        internal static double SearchingBoostsCategoryNameSecondaryFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name secondary keyword function score factor.</summary>
        /// <value>The searching boosts category name secondary keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameSecondary.Keyword.FunctionScore.Factor"),
            DefaultValue(250)]
        internal static double SearchingBoostsCategoryNameSecondaryKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 250;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name secondary match keyword.</summary>
        /// <value>The searching boosts category name secondary match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameSecondary.Match.Keyword"),
            DefaultValue(750)]
        internal static double SearchingBoostsCategoryNameSecondaryMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 750;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name secondary prefix keyword.</summary>
        /// <value>The searching boosts category name secondary prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameSecondary.Prefix.Keyword"),
            DefaultValue(375)]
        internal static double SearchingBoostsCategoryNameSecondaryPrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 375;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name secondary term keyword.</summary>
        /// <value>The searching boosts category name secondary term keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameSecondary.Term.Keyword"),
            DefaultValue(375)]
        internal static double SearchingBoostsCategoryNameSecondaryTermKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 375;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name tertiary function score factor.</summary>
        /// <value>The searching boosts category name tertiary function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameTertiary.FunctionScore.Factor"),
            DefaultValue(62.5d)]
        internal static double SearchingBoostsCategoryNameTertiaryFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 62.5d;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name tertiary keyword function score factor.</summary>
        /// <value>The searching boosts category name tertiary keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameTertiary.Keyword.FunctionScore.Factor"),
            DefaultValue(62.5d)]
        internal static double SearchingBoostsCategoryNameTertiaryKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 62.5d;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name tertiary match keyword.</summary>
        /// <value>The searching boosts category name tertiary match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameTertiary.Match.Keyword"),
            DefaultValue(500)]
        internal static double SearchingBoostsCategoryNameTertiaryMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name tertiary prefix keyword.</summary>
        /// <value>The searching boosts category name tertiary prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameTertiary.Prefix.Keyword"),
            DefaultValue(250)]
        internal static double SearchingBoostsCategoryNameTertiaryPrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 250;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name tertiary term keyword.</summary>
        /// <value>The searching boosts category name tertiary term keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameTertiary.Term.Keyword"),
            DefaultValue(250)]
        internal static double SearchingBoostsCategoryNameTertiaryTermKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 250;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
