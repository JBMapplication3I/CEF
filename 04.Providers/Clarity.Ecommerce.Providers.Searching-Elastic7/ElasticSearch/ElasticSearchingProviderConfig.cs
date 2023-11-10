// <copyright file="ElasticSearchingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System.ComponentModel;
    using Interfaces.Providers;
    using JetBrains.Annotations;
    using JSConfigs;

    /// <summary>An elastic searching provider configuration.</summary>
    [PublicAPI, GeneratesAppSettings]
    internal static class ElasticSearchingProviderConfig
    {
        /// <summary>Initializes static members of the <see cref="ElasticSearchingProviderConfig" /> class.</summary>
        static ElasticSearchingProviderConfig()
        {
            CEFConfigDictionary.Load(typeof(ElasticSearchingProviderConfig));
        }

        #region Connections
        /// <summary>For ElasticSearch: leave Uri blank to automatically switch between 'localhost' and 'ipv4.fiddler'.
        /// Use 'elasticsearch.corp.claritymis.com' to reference Dev-B if you can't run locally.</summary>
        /// <value>The searching elastic search URI.</value>
        [AppSettingsKey("Clarity.Searching.ElasticSearch.Uri"),
         DefaultValue("localhost")]
        internal static string SearchingElasticSearchUri
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : "localhost";
            ////get => "2019-train-1.hq.clarityinternal.com";
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching elastic search port.</summary>
        /// <value>The searching elastic search port.</value>
        [AppSettingsKey("Clarity.Searching.ElasticSearch.Port"),
         DefaultValue(9200)]
        internal static int SearchingElasticSearchPort
        {
            get => CEFConfigDictionary.TryGet(out int asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 9200;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching elastic search username.</summary>
        /// <value>The searching elastic search username.</value>
        [AppSettingsKey("Clarity.Searching.ElasticSearch.Username"),
         DefaultValue(null)]
        internal static string? SearchingElasticSearchUsername
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching elastic search password.</summary>
        /// <value>The searching elastic search password.</value>
        [AppSettingsKey("Clarity.Searching.ElasticSearch.Password"),
         DefaultValue(null)]
        internal static string? SearchingElasticSearchPassword
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the searching elastic search disable direct streaming.</summary>
        /// <value>True if searching elastic search disable direct streaming, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ElasticSearch.DisableDirectStreaming"),
         DefaultValue(false)]
        internal static bool SearchingElasticSearchDisableDirectStreaming
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets URI of the searching kibana.</summary>
        /// <value>The searching kibana URI.</value>
        [AppSettingsKey("Clarity.Searching.Kibana.Uri"),
         DefaultValue("localhost"),
         Unused]
        internal static string SearchingKibanaUri
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : "localhost";
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching kibana port.</summary>
        /// <value>The searching kibana port.</value>
        [AppSettingsKey("Clarity.Searching.Kibana.Port"),
         DefaultValue(5601),
         Unused]
        internal static int SearchingKibanaPort
        {
            get => CEFConfigDictionary.TryGet(out int asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 5601;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
        #endregion

        #region Products
        /// <summary>Gets the zero-based index of the searching product.</summary>
        /// <value>The searching product index.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex"),
         DefaultValue("An Exception if missing")]
        internal static string SearchingProductIndex
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : throw new System.Configuration.ConfigurationErrorsException(
                    "Clarity.Searching.ProductIndex is required in appSettings.config");
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching product index old.</summary>
        /// <value>The searching product index old.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.Old"),
         DefaultValue(null),
         DependsOn(nameof(SearchingProductIndex))]
        internal static string SearchingProductIndexOld
        {
            get => $"{SearchingProductIndex}-old";
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the searching product index requires a store.</summary>
        /// <value>True if searching product index requires a store, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.RequiresAStore"),
         DefaultValue(false)]
        internal static bool SearchingProductIndexRequiresAStore
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the searching product index requires a brand.</summary>
        /// <value>True if searching product index requires a brand, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.RequiresABrand"),
         DefaultValue(false)]
        internal static bool SearchingProductIndexRequiresABrand
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Only products that have valid/visible categories when true.</summary>
        /// <value>True if searching product index requires a category, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.RequiresACategory"),
         DefaultValue(true)]
        internal static bool SearchingProductIndexRequiresACategory
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Only get products of certain types (comma delimited), gets all types when blank.</summary>
        /// <value>The searching product index filter by type keys.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.FilterByTypeKeys"),
         DefaultValue((string[]?)null),
         SplitOn(new[] { ';', ',' })]
        internal static string[]? SearchingProductIndexFilterByTypeKeys
        {
            get => CEFConfigDictionary.TryGet(out string[]? asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Enable indexing of product roles to enable preventing products from displaying in catalog based on
        /// user roles.</summary>
        /// <value>True if searching product index include roles, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.FiltersIncludeRoles"),
         DefaultValue(false)]
        internal static bool SearchingProductIndexFiltersIncludeRoles
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Some clients need the associated products in the catalog for workflow manipulations, but this is
        /// more expensive to include, default off (true).</summary>
        /// <value>True if searching product index results include associated products, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.ResultsIncludeAssociatedProducts"),
         DefaultValue(false)]
        internal static bool SearchingProductIndexResultsIncludeAssociatedProducts
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Explanatory information in the search results reply, use for debugging only.</summary>
        /// <value>True if searching product index results include search documents, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.ResultsIncludeSearchDocuments"),
         DefaultValue(false)]
        internal static bool SearchingProductIndexResultsIncludeSearchDocuments
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        #region Price Ranges
        /// <summary>Gets a value indicating whether the searching price ranges is enabled.</summary>
        /// <value>True if searching price ranges enabled, false if not.</value>
        [AppSettingsKey("Clarity.Searching.PriceRanges.Enabled"),
            DefaultValue(true)]
        internal static bool SearchingPriceRangesEnabled
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching price ranges increment 1 amount.</summary>
        /// <value>The searching price ranges increment 1 amount.</value>
        [AppSettingsKey("Clarity.Searching.PriceRanges.Increment1"),
         DefaultValue(50)]
        internal static decimal SearchingPriceRangesIncrement1Amount
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 50;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching price ranges increment 1 stop.</summary>
        /// <value>The searching price ranges increment 1 stop.</value>
        [AppSettingsKey("Clarity.Searching.PriceRanges.Increment1Stop"),
         DefaultValue(100)]
        internal static decimal SearchingPriceRangesIncrement1Stop
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 100;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching price ranges increment 2 amount.</summary>
        /// <value>The searching price ranges increment 2 amount.</value>
        [AppSettingsKey("Clarity.Searching.PriceRanges.Increment2"),
         DefaultValue(100)]
        internal static decimal? SearchingPriceRangesIncrement2Amount
        {
            get => CEFConfigDictionary.TryGet(out decimal? asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 100;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching price ranges increment 2 stop.</summary>
        /// <value>The searching price ranges increment 2 stop.</value>
        [AppSettingsKey("Clarity.Searching.PriceRanges.Increment2Stop"),
         DefaultValue(500)]
        internal static decimal? SearchingPriceRangesIncrement2Stop
        {
            get => CEFConfigDictionary.TryGet(out decimal? asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching price ranges increment 3 amount.</summary>
        /// <value>The searching price ranges increment 3 amount.</value>
        [AppSettingsKey("Clarity.Searching.PriceRanges.Increment3"),
            DefaultValue(500)]
        internal static decimal? SearchingPriceRangesIncrement3Amount
        {
            get => CEFConfigDictionary.TryGet(out decimal? asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching price ranges increment 3 stop.</summary>
        /// <value>The searching price ranges increment 3 stop.</value>
        [AppSettingsKey("Clarity.Searching.PriceRanges.Increment3Stop"),
         DefaultValue(1_000)]
        internal static decimal? SearchingPriceRangesIncrement3Stop
        {
            get => CEFConfigDictionary.TryGet(out decimal? asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 1_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching price ranges increment 4 amount.</summary>
        /// <value>The searching price ranges increment 4 amount.</value>
        [AppSettingsKey("Clarity.Searching.PriceRanges.Increment4"),
            DefaultValue(1_000)]
        internal static decimal? SearchingPriceRangesIncrement4Amount
        {
            get => CEFConfigDictionary.TryGet(out decimal? asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 1_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching price ranges increment 4 stop.</summary>
        /// <value>The searching price ranges increment 4 stop.</value>
        [AppSettingsKey("Clarity.Searching.PriceRanges.Increment4Stop"),
         DefaultValue(5_000)]
        internal static decimal? SearchingPriceRangesIncrement4Stop
        {
            get => CEFConfigDictionary.TryGet(out decimal? asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 5_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
        #endregion

        #region Boosts
        /// <summary>Gets the searching boosts name match keyword.</summary>
        /// <value>The searching boosts name match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.Name.Match.Keyword"),
         DefaultValue(10_000)]
        internal static decimal SearchingBoostsNameMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 10_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts name prefix keyword.</summary>
        /// <value>The searching boosts name prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.Name.Prefix.Keyword"),
         DefaultValue(10_000)]
        internal static decimal SearchingBoostsNamePrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 10_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts custom key match keyword.</summary>
        /// <value>The searching boosts custom key match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CustomKey.Match.Keyword"),
         DefaultValue(15_000)]
        internal static decimal SearchingBoostsCustomKeyMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 15_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts custom key prefix keyword.</summary>
        /// <value>The searching boosts custom key prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CustomKey.Prefix.Keyword"),
         DefaultValue(15_000)]
        internal static decimal SearchingBoostsCustomKeyPrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 15_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts brand name match keyword.</summary>
        /// <value>The searching boosts brand name match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.BrandName.Match.Keyword"),
         DefaultValue(500)]
        internal static decimal SearchingBoostsBrandNameMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts brand name prefix keyword.</summary>
        /// <value>The searching boosts brand name prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.BrandName.Prefix.Keyword"),
         DefaultValue(50_000)]
        internal static decimal SearchingBoostsBrandNamePrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 50_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts seo keywords match keyword.</summary>
        /// <value>The searching boosts seo keywords match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SeoKeywords.Match.Keyword"),
         DefaultValue(500)]
        internal static decimal SearchingBoostsSeoKeywordsMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts seo keywords prefix keyword.</summary>
        /// <value>The searching boosts seo keywords prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SeoKeywords.Prefix.Keyword"),
         DefaultValue(500)]
        internal static decimal SearchingBoostsSeoKeywordsPrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name primary match keyword.</summary>
        /// <value>The searching boosts category name primary match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNamePrimary.Match.Keyword"),
         DefaultValue(1_000)]
        internal static decimal SearchingBoostsCategoryNamePrimaryMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 1_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name primary prefix keyword.</summary>
        /// <value>The searching boosts category name primary prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNamePrimary.Prefix.Keyword"),
         DefaultValue(500)]
        internal static decimal SearchingBoostsCategoryNamePrimaryPrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name secondary match keyword.</summary>
        /// <value>The searching boosts category name secondary match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameSecondary.Match.Keyword"),
         DefaultValue(750)]
        internal static decimal SearchingBoostsCategoryNameSecondaryMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 750;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name secondary prefix keyword.</summary>
        /// <value>The searching boosts category name secondary prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameSecondary.Prefix.Keyword"),
         DefaultValue(375)]
        internal static decimal SearchingBoostsCategoryNameSecondaryPrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 375;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name tertiary match keyword.</summary>
        /// <value>The searching boosts category name tertiary match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameTertiary.Match.Keyword"),
         DefaultValue(500)]
        internal static decimal SearchingBoostsCategoryNameTertiaryMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name tertiary prefix keyword.</summary>
        /// <value>The searching boosts category name tertiary prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameTertiary.Prefix.Keyword"),
         DefaultValue(250)]
        internal static decimal SearchingBoostsCategoryNameTertiaryPrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 250;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts total purchased quantity function score factor.</summary>
        /// <value>The searching boosts total purchased quantity function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.TotalPurchasedQuantity.FunctionScore.Factor"),
         DefaultValue(0.01d)]
        internal static decimal SearchingBoostsTotalPurchasedQuantityFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 0.01m;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts manufacturer part keyword number function score factor.</summary>
        /// <value>The searching boosts manufacturer part keyword number function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.ManufacturerPartNumber.Keyword.FunctionScore.Factor"),
         DefaultValue(125)]
        internal static decimal SearchingBoostsManufacturerPartKeywordNumberFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts custom key keyword function score factor.</summary>
        /// <value>The searching boosts custom key keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CustomKey.Keyword.FunctionScore.Factor"),
         DefaultValue(500)]
        internal static decimal SearchingBoostsCustomKeyKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts custom key function score factor.</summary>
        /// <value>The searching boosts custom key function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CustomKey.FunctionScore.Factor"),
         DefaultValue(500)]
        internal static decimal SearchingBoostsCustomKeyFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts name keyword function score factor.</summary>
        /// <value>The searching boosts name keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.Name.Keyword.FunctionScore.Factor"),
         DefaultValue(300)]
        internal static decimal SearchingBoostsNameKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 300;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts name function score factor.</summary>
        /// <value>The searching boosts name function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.Name.FunctionScore.Factor"),
         DefaultValue(300)]
        internal static decimal SearchingBoostsNameFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 300;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts brand name keyword function score factor.</summary>
        /// <value>The searching boosts brand name keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.BrandName.Keyword.FunctionScore.Factor"),
         DefaultValue(125)]
        internal static decimal SearchingBoostsBrandNameKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts brand name function score factor.</summary>
        /// <value>The searching boosts brand name function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.BrandName.FunctionScore.Factor"),
         DefaultValue(125)]
        internal static decimal SearchingBoostsBrandNameFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts seo keywords keyword function score factor.</summary>
        /// <value>The searching boosts seo keywords keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SeoKeywords.Keyword.FunctionScore.Factor"),
         DefaultValue(125)]
        internal static decimal SearchingBoostsSeoKeywordsKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts seo keywords function score factor.</summary>
        /// <value>The searching boosts seo keywords function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SeoKeywords.FunctionScore.Factor"),
         DefaultValue(125)]
        internal static decimal SearchingBoostsSeoKeywordsFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name primary keyword function score factor.</summary>
        /// <value>The searching boosts category name primary keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNamePrimary.Keyword.FunctionScore.Factor"),
         DefaultValue(300)]
        internal static decimal SearchingBoostsCategoryNamePrimaryKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 300;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name primary function score factor.</summary>
        /// <value>The searching boosts category name primary function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNamePrimary.FunctionScore.Factor"),
         DefaultValue(250)]
        internal static decimal SearchingBoostsCategoryNamePrimaryFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 250;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name secondary keyword function score factor.</summary>
        /// <value>The searching boosts category name secondary keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameSecondary.Keyword.FunctionScore.Factor"),
         DefaultValue(250)]
        internal static decimal SearchingBoostsCategoryNameSecondaryKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 250;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name secondary function score factor.</summary>
        /// <value>The searching boosts category name secondary function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameSecondary.FunctionScore.Factor"),
         DefaultValue(125)]
        internal static decimal SearchingBoostsCategoryNameSecondaryFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name tertiary keyword function score factor.</summary>
        /// <value>The searching boosts category name tertiary keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameTertiary.Keyword.FunctionScore.Factor"),
         DefaultValue(62.5d)]
        internal static decimal SearchingBoostsCategoryNameTertiaryKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 62.5m;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts category name tertiary function score factor.</summary>
        /// <value>The searching boosts category name tertiary function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.CategoryNameTertiary.FunctionScore.Factor"),
         DefaultValue(62.5d)]
        internal static decimal SearchingBoostsCategoryNameTertiaryFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 62.5m;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts description function score factor.</summary>
        /// <value>The searching boosts description function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.Description.FunctionScore.Factor"),
         DefaultValue(25)]
        internal static decimal SearchingBoostsDescriptionFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 25;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts short description function score factor.</summary>
        /// <value>The searching boosts short description function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.ShortDescription.FunctionScore.Factor"),
         DefaultValue(50)]
        internal static decimal SearchingBoostsShortDescriptionFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 50;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts function score maximum boost.</summary>
        /// <value>The searching boosts function score maximum boost.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.FunctionScore.MaxBoost"),
         DefaultValue(10)]
        internal static decimal SearchingBoostsFunctionScoreMaxBoost
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 10;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the category the searching boosts single belongs to.</summary>
        /// <value>The searching boosts single category.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleCategory"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsSingleCategory
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the category the searching boosts any belongs to.</summary>
        /// <value>The searching boosts any category.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyCategory"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsAnyCategory
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the categories the searching boosts all belongs to.</summary>
        /// <value>The searching boosts all categories.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllCategories"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsAllCategories
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts single role.</summary>
        /// <value>The searching boosts single role.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleRole"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsSingleRole
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts any roles.</summary>
        /// <value>The searching boosts any roles.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyRoles"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsAnyRoles
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts all roles.</summary>
        /// <value>The searching boosts all roles.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllRoles"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsAllRoles
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts single brand.</summary>
        /// <value>The searching boosts single brand.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleBrand"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsSingleBrand
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts any brands.</summary>
        /// <value>The searching boosts any brands.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyBrands"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsAnyBrands
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts all brands.</summary>
        /// <value>The searching boosts all brands.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllBrands"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsAllBrands
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts single store.</summary>
        /// <value>The searching boosts single store.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SingleStore"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsSingleStore
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts any stores.</summary>
        /// <value>The searching boosts any stores.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyStores"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsAnyStores
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts all stores.</summary>
        /// <value>The searching boosts all stores.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllStores"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsAllStores
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts price ranges.</summary>
        /// <value>The searching boosts price ranges.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.PriceRanges"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsPriceRanges
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts any attributes.</summary>
        /// <value>The searching boosts any attributes.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyAttributes"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsAnyAttributes
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts all attributes.</summary>
        /// <value>The searching boosts all attributes.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllAttributes"),
         DefaultValue(110)]
        internal static decimal SearchingBoostsAllAttributes
        {
            get => CEFConfigDictionary.TryGet(out decimal asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
        #endregion
        #endregion

        #region Stores
#if STORES
        /// <summary>Gets the zero-based index of the searching store.</summary>
        /// <value>The searching store index.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex"),
         DefaultValue("An Exception if missing")]
        internal static string SearchingStoreIndex
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : throw new System.Configuration.ConfigurationErrorsException(
                    "Clarity.Searching.StoreIndex is required in appSettings.config");
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching store index old.</summary>
        /// <value>The searching store index old.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.Old"),
         DefaultValue(null),
         DependsOn(nameof(SearchingStoreIndex))]
        internal static string SearchingStoreIndexOld
        {
            get => $"{SearchingStoreIndex}-old";
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Only get stores of certain types (comma delimited), gets all types when blank.</summary>
        /// <value>The searching store index filter by type keys.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.FilterByTypeKeys"),
         DefaultValue((string[]?)null),
         SplitOn(new[] { ';', ',' })]
        internal static string[]? SearchingStoreIndexFilterByTypeKeys
        {
            get => CEFConfigDictionary.TryGet(out string[]? asValue, typeof(ElasticSearchingProviderConfig)) ? asValue : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
#endif
        #endregion

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<ElasticSearchingProvider>() || isDefaultAndActivated;
    }
}
