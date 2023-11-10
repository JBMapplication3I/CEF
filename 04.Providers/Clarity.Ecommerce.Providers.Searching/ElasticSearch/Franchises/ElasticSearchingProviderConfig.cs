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
        /// <summary>Gets the zero-based index of the searching franchise.</summary>
        /// <value>The searching franchise index.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex"),
            DefaultValue(null),
            DependsOn(nameof(CEFConfigDictionary.FranchisesEnabled))]
        internal static string SearchingFranchiseIndex
        {
            get => CEFConfigDictionary.FranchisesEnabled
                ? CEFConfigDictionary.TryGet(out string asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : throw new ConfigurationErrorsException(
                        "Clarity.Searching.FranchiseIndex is required in appSettings.config when franchises are enabled")
                : null!;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching index old.</summary>
        /// <value>The searching index old.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex.Old"),
            DefaultValue(null),
            DependsOn(nameof(SearchingFranchiseIndex))]
        internal static string SearchingFranchiseIndexOld
        {
            get => CEFConfigDictionary.FranchisesEnabled
                ? $"{SearchingFranchiseIndex}-old"
                : null!;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Only get records of certain types (comma delimited), gets all types when blank.</summary>
        /// <value>The searching index filter by type keys.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex.FilterByTypeKeys"),
            DefaultValue((string[]?)null),
            SplitOn(new[] { ';', ',' })]
        internal static string[]? SearchingFranchiseIndexFilterByTypeKeys
        {
            get => CEFConfigDictionary.FranchisesEnabled
                ? CEFConfigDictionary.TryGet(out string[]? asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : null
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Allow filtering the value of certain attribute keys in the suggest queries.</summary>
        /// <value>A comma-delimited list of attribute keys to use for suggest filtering.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex.QueryByAttributeKeys"),
            DefaultValue(null)]
        internal static string? SearchingFranchiseIndexQueryByAttributeKeys
        {
            get => CEFConfigDictionary.FranchisesEnabled
                ? CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : null
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a comma-delimited list of attribute keys whose values should be on the suggest options.</summary>
        /// <value>A comma-delimited list of attribute keys whose values should be on the suggest options.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex.SuggestOptionAttributeKeys"),
            DefaultValue(null)]
        internal static string? SearchingFranchiseIndexSuggestOptionAttributeKeys
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with brands.</summary>
        /// <value>True to limit to those with brands, false if not.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex.RequiresABrand"),
            DefaultValue(false)]
        internal static bool SearchingFranchiseIndexRequiresABrand
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with categories.</summary>
        /// <value>True to limit to those with categories, false if not.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex.RequiresACategory"),
            DefaultValue(true)]
        internal static bool SearchingFranchiseIndexRequiresACategory
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /*
        /// <summary>Gets a value indicating whether the limit to those with franchises.</summary>
        /// <value>True to limit to those with franchises, false if not.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex.RequiresAFranchise"),
            DefaultValue(false)]
        internal static bool SearchingFranchiseIndexRequiresAFranchise
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
        */

        /// <summary>Gets a value indicating whether the limit to those with manufacturers.</summary>
        /// <value>True to limit to those with manufacturers, false if not.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex.RequiresAManufacturer"),
            DefaultValue(false)]
        internal static bool SearchingFranchiseIndexRequiresAManufacturer
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with products.</summary>
        /// <value>True to limit to those with products, false if not.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex.RequiresAProduct"),
            DefaultValue(false)]
        internal static bool SearchingFranchiseIndexRequiresAProduct
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with stores.</summary>
        /// <value>True to limit to those with stores, false if not.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex.RequiresAStore"),
            DefaultValue(false)]
        internal static bool SearchingFranchiseIndexRequiresAStore
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with vendors.</summary>
        /// <value>True to limit to those with vendors, false if not.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex.RequiresAVendor"),
            DefaultValue(false)]
        internal static bool SearchingFranchiseIndexRequiresAVendor
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Explanatory information in the search results reply, use for debugging only.</summary>
        /// <value>True if searching index results include search documents, false if not.</value>
        [AppSettingsKey("Clarity.Searching.FranchiseIndex.ResultsIncludeSearchDocuments"),
            DefaultValue(false)]
        internal static bool SearchingFranchiseIndexResultsIncludeSearchDocuments
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Enables searching for franchises.</summary>
        /// <value>True if searching for franchises, false if not.</value>
        [AppSettingsKey("Clarity.Searching.FranchisesEnabled"),
          DefaultValue(false),
          DependsOn(nameof(CEFConfigDictionary.FranchisesEnabled))]
        internal static bool SearchingFranchisesEnabled
        {
            get => CEFConfigDictionary.FranchisesEnabled
                ? CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : false
                : false;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
