// <copyright file="ElasticSearchingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider configuration class</summary>
#pragma warning disable CS0162
// ReSharper disable HeuristicUnreachableCode, UnreachableCode
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System.ComponentModel;
    using System.Configuration;
    using JSConfigs;

    /// <summary>An elastic searching provider configuration.</summary>
    internal static partial class ElasticSearchingProviderConfig
    {
        /// <summary>Gets the zero-based index of the searching.</summary>
        /// <value>The searching index.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex"),
            DefaultValue(null)/*,
            DependsOn(nameof(CEFConfigDictionary.ProductsEnabled)*/]
        internal static string SearchingProductIndex
        {
            get => true
                ? CEFConfigDictionary.TryGet(out string asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : throw new ConfigurationErrorsException(
                        "Clarity.Searching.ProductIndex is required in appSettings.config")
                : null!;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching index old.</summary>
        /// <value>The searching index old.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.Old"),
            DefaultValue(null),
            DependsOn(nameof(SearchingProductIndex))]
        internal static string SearchingProductIndexOld
        {
            get => true
                ? $"{SearchingProductIndex}-old"
                : null!;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Only get records of certain types (comma delimited), gets all types when blank.</summary>
        /// <value>The searching index filter by type keys.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.FilterByTypeKeys"),
            DefaultValue((string[]?)null),
            SplitOn(new[] { ';', ',' })]
        internal static string[]? SearchingProductIndexFilterByTypeKeys
        {
            get => true
                ? CEFConfigDictionary.TryGet(out string[]? asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : null
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Enable indexing of roles to enable preventing records from displaying in catalog based on
        /// user roles.</summary>
        /// <value>True if searching index include roles, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.FiltersIncludeRoles"),
            DefaultValue(false)]
        internal static bool SearchingProductIndexFiltersIncludeRoles
        {
            // ReSharper disable once RedundantLogicalConditionalExpressionOperand
            get => true
                && CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig))
                && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        [AppSettingsKey("Clarity.Searching.ProductIndex.FiltersIncludeRoles.InvertRoles"),
            DependsOn(nameof(SearchingProductIndexFiltersIncludeRoles)),
            DefaultValue(false)]
        internal static bool InvertRoles
        {
            // ReSharper disable once RedundantLogicalConditionalExpressionOperand
            get => SearchingProductIndexFiltersIncludeRoles
                && CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig))
                && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Allow filtering the value of certain attribute keys in the suggest queries.</summary>
        /// <value>A comma-delimited list of attribute keys to use for suggest filtering.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.QueryByAttributeKeys"),
            DefaultValue(null)]
        internal static string? SearchingProductIndexQueryByAttributeKeys
        {
            get => true
                ? CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : null
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a comma-delimited list of attribute keys whose values should be on the suggest options.</summary>
        /// <value>A comma-delimited list of attribute keys whose values should be on the suggest options.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.SuggestOptionAttributeKeys"),
            DefaultValue(null)]
        internal static string? SearchingProductIndexSuggestOptionAttributeKeys
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with brands.</summary>
        /// <value>True to limit to those with brands, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.RequiresABrand"),
            DefaultValue(false)]
        internal static bool SearchingProductIndexRequiresABrand
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with categories.</summary>
        /// <value>True to limit to those with categories, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.RequiresACategory"),
            DefaultValue(true)]
        internal static bool SearchingProductIndexRequiresACategory
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with franchises.</summary>
        /// <value>True to limit to those with franchises, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.RequiresAFranchise"),
            DefaultValue(false)]
        internal static bool SearchingProductIndexRequiresAFranchise
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with manufacturers.</summary>
        /// <value>True to limit to those with manufacturers, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.RequiresAManufacturer"),
            DefaultValue(false)]
        internal static bool SearchingProductIndexRequiresAManufacturer
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /*
        /// <summary>Gets a value indicating whether the limit to those with products.</summary>
        /// <value>True to limit to those with products, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.RequiresAProduct"),
            DefaultValue(false)]
        internal static bool SearchingProductIndexRequiresAProduct
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
        */

        /// <summary>Gets a value indicating whether the limit to those with stores.</summary>
        /// <value>True to limit to those with stores, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.RequiresAStore"),
            DefaultValue(false)]
        internal static bool SearchingProductIndexRequiresAStore
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with vendors.</summary>
        /// <value>True to limit to those with vendors, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.RequiresAVendor"),
            DefaultValue(false)]
        internal static bool SearchingProductIndexRequiresAVendor
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Explanatory information in the search results reply, use for debugging only.</summary>
        /// <value>True if searching index results include search documents, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.ResultsIncludeSearchDocuments"),
            DefaultValue(false)]
        internal static bool SearchingProductIndexResultsIncludeSearchDocuments
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

        /// <summary>Gets the searching boosts brand name function score factor.</summary>
        /// <value>The searching boosts brand name function score factor.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.Boosts.BrandName.FunctionScore.Factor"),
            DefaultValue(125)]
        internal static double SearchingProductIndexBoostsBrandNameFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts brand name keyword function score factor.</summary>
        /// <value>The searching boosts brand name keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.Boosts.BrandName.Keyword.FunctionScore.Factor"),
            DefaultValue(125)]
        internal static double SearchingProductIndexBoostsBrandNameKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts brand name match keyword.</summary>
        /// <value>The searching boosts brand name match keyword.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.Boosts.BrandName.Match.Keyword"),
            DefaultValue(50_000)]
        internal static double SearchingProductIndexBoostsBrandNameMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 50_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts brand name prefix keyword.</summary>
        /// <value>The searching boosts brand name prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.Boosts.BrandName.Prefix.Keyword"),
            DefaultValue(50_000)]
        internal static double SearchingProductIndexBoostsBrandNamePrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 50_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts brand name term keyword.</summary>
        /// <value>The searching boosts brand name term keyword.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.Boosts.BrandName.Term.Keyword"),
            DefaultValue(50_000)]
        internal static double SearchingProductIndexBoostsBrandNameTermKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 50_000;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts manufacturer part keyword number function score factor.</summary>
        /// <value>The searching boosts manufacturer part keyword number function score factor.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.Boosts.ManufacturerPartNumber.Keyword.FunctionScore.Factor"),
            DefaultValue(125)]
        internal static double SearchingProductIndexBoostsManufacturerPartKeywordNumberFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts short description function score factor.</summary>
        /// <value>The searching boosts short description function score factor.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.Boosts.ShortDescription.FunctionScore.Factor"),
            DefaultValue(50)]
        internal static double SearchingProductIndexBoostsShortDescriptionFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 50;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts total purchased quantity function score factor.</summary>
        /// <value>The searching boosts total purchased quantity function score factor.</value>
        [AppSettingsKey("Clarity.Searching.ProductIndex.Boosts.TotalPurchasedQuantity.FunctionScore.Factor"),
            DefaultValue(0.01d)]
        internal static double SearchingProductIndexBoostsTotalPurchasedQuantityFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 0.01d;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
