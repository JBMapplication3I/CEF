// <copyright file="ElasticSearchingProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider configuration class</summary>
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
        [AppSettingsKey("Clarity.Searching.CategoryIndex"),
            DefaultValue(null),
            DependsOn(nameof(CEFConfigDictionary.CategoriesEnabled))]
        internal static string SearchingCategoryIndex
        {
            get => CEFConfigDictionary.CategoriesEnabled
                ? CEFConfigDictionary.TryGet(out string asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : throw new ConfigurationErrorsException(
                        "Clarity.Searching.CategoryIndex is required in appSettings.config when categories are enabled")
                : null!;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching index old.</summary>
        /// <value>The searching index old.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.Old"),
            DefaultValue(null),
            DependsOn(nameof(SearchingCategoryIndex))]
        internal static string SearchingCategoryIndexOld
        {
            get => CEFConfigDictionary.CategoriesEnabled
                ? $"{SearchingCategoryIndex}-old"
                : null!;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Only get records of certain types (comma delimited), gets all types when blank.</summary>
        /// <value>The searching index filter by type keys.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.FilterByTypeKeys"),
            DefaultValue((string[]?)null),
            SplitOn(new[] { ';', ',' })]
        internal static string[]? SearchingCategoryIndexFilterByTypeKeys
        {
            get => CEFConfigDictionary.CategoriesEnabled
                ? CEFConfigDictionary.TryGet(out string[]? asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : null
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Enable indexing of roles to enable preventing records from displaying in catalog based on
        /// user roles.</summary>
        /// <value>True if searching index include roles, false if not.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.FiltersIncludeRoles"),
            DefaultValue(false)]
        internal static bool SearchingCategoryIndexFiltersIncludeRoles
        {
            get => CEFConfigDictionary.CategoriesEnabled
                && CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig))
                && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Allow filtering the value of certain attribute keys in the suggest queries.</summary>
        /// <value>A comma-delimited list of attribute keys to use for suggest filtering.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.QueryByAttributeKeys"),
            DefaultValue(null)]
        internal static string? SearchingCategoryIndexQueryByAttributeKeys
        {
            get => CEFConfigDictionary.CategoriesEnabled
                ? CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : null
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a comma-delimited list of attribute keys whose values should be on the suggest options.</summary>
        /// <value>A comma-delimited list of attribute keys whose values should be on the suggest options.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.SuggestOptionAttributeKeys"),
            DefaultValue(null)]
        internal static string? SearchingCategoryIndexSuggestOptionAttributeKeys
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with brands.</summary>
        /// <value>True to limit to those with brands, false if not.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.RequiresABrand"),
            DefaultValue(false)]
        internal static bool SearchingCategoryIndexRequiresABrand
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /*
        /// <summary>Gets a value indicating whether the limit to those with categories.</summary>
        /// <value>True to limit to those with categories, false if not.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.RequiresACategory"),
            DefaultValue(true)]
        internal static bool SearchingCategoryIndexRequiresACategory
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
        */

        /// <summary>Gets a value indicating whether the limit to those with franchises.</summary>
        /// <value>True to limit to those with franchises, false if not.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.RequiresAFranchise"),
            DefaultValue(false)]
        internal static bool SearchingCategoryIndexRequiresAFranchise
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /*
        /// <summary>Gets a value indicating whether the limit to those with manufacturers.</summary>
        /// <value>True to limit to those with manufacturers, false if not.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.RequiresAManufacturer"),
            DefaultValue(false)]
        internal static bool SearchingCategoryIndexRequiresAManufacturer
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
        */

        /// <summary>Gets a value indicating whether the limit to those with products.</summary>
        /// <value>True to limit to those with products, false if not.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.RequiresAProduct"),
            DefaultValue(false)]
        internal static bool SearchingCategoryIndexRequiresAProduct
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with stores.</summary>
        /// <value>True to limit to those with stores, false if not.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.RequiresAStore"),
            DefaultValue(false)]
        internal static bool SearchingCategoryIndexRequiresAStore
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /*
        /// <summary>Gets a value indicating whether the limit to those with vendors.</summary>
        /// <value>True to limit to those with vendors, false if not.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.RequiresAVendor"),
            DefaultValue(false)]
        internal static bool SearchingCategoryIndexRequiresAVendor
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
        */

        /// <summary>Explanatory information in the search results reply, use for debugging only.</summary>
        /// <value>True if searching index results include search documents, false if not.</value>
        [AppSettingsKey("Clarity.Searching.CategoryIndex.ResultsIncludeSearchDocuments"),
            DefaultValue(false)]
        internal static bool SearchingCategoryIndexResultsIncludeSearchDocuments
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Enables searching for categories.</summary>
        /// <value>True if searching for categories, false if not.</value>
        [AppSettingsKey("Clarity.Searching.CategoriesEnabled"),
            DefaultValue(false),
            DependsOn(nameof(CEFConfigDictionary.CategoriesEnabled))]
        internal static bool SearchingCategoriesEnabled
        {
            get => CEFConfigDictionary.CategoriesEnabled
                ? CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : false
                : false;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
