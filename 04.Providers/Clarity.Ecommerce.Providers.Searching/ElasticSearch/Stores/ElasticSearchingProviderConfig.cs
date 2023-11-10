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
        [AppSettingsKey("Clarity.Searching.StoreIndex"),
            DefaultValue(null),
            DependsOn(nameof(CEFConfigDictionary.StoresEnabled))]
        internal static string SearchingStoreIndex
        {
            get => CEFConfigDictionary.StoresEnabled
                ? CEFConfigDictionary.TryGet(out string asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : throw new ConfigurationErrorsException(
                        "Clarity.Searching.StoreIndex is required in appSettings.config when stores are enabled")
                : null!;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching index old.</summary>
        /// <value>The searching index old.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.Old"),
            DefaultValue(null),
            DependsOn(nameof(SearchingStoreIndex))]
        internal static string SearchingStoreIndexOld
        {
            get => CEFConfigDictionary.StoresEnabled
                ? $"{SearchingStoreIndex}-old"
                : null!;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Only get records of certain types (comma delimited), gets all types when blank.</summary>
        /// <value>The searching index filter by type keys.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.FilterByTypeKeys"),
            DefaultValue((string[]?)null),
            SplitOn(new[] { ';', ',' })]
        internal static string[]? SearchingStoreIndexFilterByTypeKeys
        {
            get => CEFConfigDictionary.StoresEnabled
                ? CEFConfigDictionary.TryGet(out string[]? asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : null
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /*
        /// <summary>Enable indexing of roles to enable preventing records from displaying in catalog based on
        /// user roles.</summary>
        /// <value>True if searching index include roles, false if not.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.FiltersIncludeRoles"),
            DefaultValue(false)]
        internal static bool SearchingStoreIndexFiltersIncludeRoles
        {
            get => CEFConfigDictionary.StoresEnabled
                && CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig))
                && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
        */

        /// <summary>Allow filtering the value of certain attribute keys in the suggest queries.</summary>
        /// <value>A comma-delimited list of attribute keys to use for suggest filtering.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.QueryByAttributeKeys"),
            DefaultValue(null)]
        internal static string? SearchingStoreIndexQueryByAttributeKeys
        {
            get => CEFConfigDictionary.StoresEnabled
                ? CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : null
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a comma-delimited list of attribute keys whose values should be on the suggest options.</summary>
        /// <value>A comma-delimited list of attribute keys whose values should be on the suggest options.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.SuggestOptionAttributeKeys"),
            DefaultValue(null)]
        internal static string? SearchingStoreIndexSuggestOptionAttributeKeys
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether to include info about their with badges.</summary>
        /// <value>True to include info about their badges, false if not.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.PrivateStoresEnabled"),
            DefaultValue(false)]
        internal static bool SearchingStoreIndexPrivateStoresEnabled
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether to include info about their with badges.</summary>
        /// <value>True to include info about their badges, false if not.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.FiltersIncludeBadges"),
            DefaultValue(false)]
        internal static bool SearchingStoreIndexFiltersIncludeBadges
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with brands.</summary>
        /// <value>True to limit to those with brands, false if not.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.RequiresABrand"),
            DefaultValue(false)]
        internal static bool SearchingStoreIndexRequiresABrand
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with categories.</summary>
        /// <value>True to limit to those with categories, false if not.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.RequiresACategory"),
            DefaultValue(true)]
        internal static bool SearchingStoreIndexRequiresACategory
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with franchises.</summary>
        /// <value>True to limit to those with franchises, false if not.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.RequiresAFranchise"),
            DefaultValue(false)]
        internal static bool SearchingStoreIndexRequiresAFranchise
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with manufacturers.</summary>
        /// <value>True to limit to those with manufacturers, false if not.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.RequiresAManufacturer"),
            DefaultValue(false)]
        internal static bool SearchingStoreIndexRequiresAManufacturer
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with products.</summary>
        /// <value>True to limit to those with products, false if not.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.RequiresAProduct"),
            DefaultValue(false)]
        internal static bool SearchingStoreIndexRequiresAProduct
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /*
        /// <summary>Gets a value indicating whether the limit to those with stores.</summary>
        /// <value>True to limit to those with stores, false if not.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.RequiresAStore"),
            DefaultValue(false)]
        internal static bool SearchingStoreIndexRequiresAStore
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
        */

        /// <summary>Gets a value indicating whether the limit to those with vendors.</summary>
        /// <value>True to limit to those with vendors, false if not.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.RequiresAVendor"),
            DefaultValue(false)]
        internal static bool SearchingStoreIndexRequiresAVendor
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Explanatory information in the search results reply, use for debugging only.</summary>
        /// <value>True if searching index results include search documents, false if not.</value>
        [AppSettingsKey("Clarity.Searching.storeIndex.ResultsIncludeSearchDocuments"),
            DefaultValue(false)]
        internal static bool SearchingStoreIndexResultsIncludeSearchDocuments
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts all badges.</summary>
        /// <value>The searching boosts all badges.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.Boosts.AllBadges"),
            DefaultValue(110)]
        internal static double SearchingStoreIndexBoostsAllBadges
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }

        /// <summary>Gets the searching boosts any badges.</summary>
        /// <value>The searching boosts any badges.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.Boosts.AnyBadges"),
            DefaultValue(110)]
        internal static double SearchingStoreIndexBoostsAnyBadges
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts single badge.</summary>
        /// <value>The searching boosts single badge.</value>
        [AppSettingsKey("Clarity.Searching.StoreIndex.Boosts.SingleBadge"),
            DefaultValue(110)]
        internal static double SearchingStoreIndexBoostsSingleBadge
        {
            get => CEFConfigDictionary.TryGet(out double asValue) ? asValue : 110;
            private set => CEFConfigDictionary.TrySet(value);
        }

        /// <summary>Enables searching for stores.</summary>
        /// <value>True if searching for stores, false if not.</value>
        [AppSettingsKey("Clarity.Searching.StoresEnabled"),
          DefaultValue(false),
          DependsOn(nameof(CEFConfigDictionary.StoresEnabled))]
        internal static bool SearchingStoresEnabled
        {
            get => CEFConfigDictionary.StoresEnabled
                ? CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : false
                : false;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
