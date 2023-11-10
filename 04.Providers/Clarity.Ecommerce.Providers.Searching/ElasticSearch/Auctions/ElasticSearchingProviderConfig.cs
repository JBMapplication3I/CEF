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
        /// <summary>Gets the zero-based index of the searching manufacturer.</summary>
        /// <value>The searching manufacturer index.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex"),
            DefaultValue(null),
            DependsOn(nameof(CEFConfigDictionary.AuctionsEnabled))]
        internal static string SearchingAuctionIndex
        {
            get => CEFConfigDictionary.AuctionsEnabled
                ? CEFConfigDictionary.TryGet(out string asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : throw new ConfigurationErrorsException(
                        "Clarity.Searching.AuctionIndex is required in appSettings.config when manufacturers are enabled")
                : null!;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching index old.</summary>
        /// <value>The searching index old.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex.Old"),
            DefaultValue(null),
            DependsOn(nameof(SearchingAuctionIndex))]
        internal static string SearchingAuctionIndexOld
        {
            get => CEFConfigDictionary.AuctionsEnabled
                ? $"{SearchingAuctionIndex}-old"
                : null!;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Only get records of certain types (comma delimited), gets all types when blank.</summary>
        /// <value>The searching index filter by type keys.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex.FilterByTypeKeys"),
            DefaultValue((string[]?)null),
            SplitOn(new[] { ';', ',' })]
        internal static string[]? SearchingAuctionIndexFilterByTypeKeys
        {
            get => CEFConfigDictionary.AuctionsEnabled
                ? CEFConfigDictionary.TryGet(out string[]? asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : null
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Allow filtering the value of certain attribute keys in the suggest queries.</summary>
        /// <value>A comma-delimited list of attribute keys to use for suggest filtering.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex.QueryByAttributeKeys"),
            DefaultValue(null)]
        internal static string? SearchingAuctionIndexQueryByAttributeKeys
        {
            get => CEFConfigDictionary.AuctionsEnabled
                ? CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : null
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a comma-delimited list of attribute keys whose values should be on the suggest options.</summary>
        /// <value>A comma-delimited list of attribute keys whose values should be on the suggest options.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex.SuggestOptionAttributeKeys"),
            DefaultValue(null)]
        internal static string? SearchingAuctionIndexSuggestOptionAttributeKeys
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with brands.</summary>
        /// <value>True to limit to those with brands, false if not.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex.RequiresABrand"),
            DefaultValue(false)]
        internal static bool SearchingAuctionIndexRequiresABrand
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with categories.</summary>
        /// <value>True to limit to those with categories, false if not.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex.RequiresACategory"),
            DefaultValue(true)]
        internal static bool SearchingAuctionIndexRequiresACategory
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with franchises.</summary>
        /// <value>True to limit to those with franchises, false if not.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex.RequiresAFranchise"),
            DefaultValue(false)]
        internal static bool SearchingAuctionIndexRequiresAFranchise
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with manufacturers.</summary>
        /// <value>True to limit to those with manufacturers, false if not.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex.RequiresAManufacturer"),
            DefaultValue(false)]
        internal static bool SearchingAuctionIndexRequiresAManufacturer
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with products.</summary>
        /// <value>True to limit to those with products, false if not.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex.RequiresAProduct"),
            DefaultValue(false)]
        internal static bool SearchingAuctionIndexRequiresAProduct
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with stores.</summary>
        /// <value>True to limit to those with stores, false if not.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex.RequiresAStore"),
            DefaultValue(false)]
        internal static bool SearchingAuctionIndexRequiresAStore
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with vendors.</summary>
        /// <value>True to limit to those with vendors, false if not.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex.RequiresAVendor"),
            DefaultValue(false)]
        internal static bool SearchingAuctionIndexRequiresAVendor
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Explanatory information in the search results reply, use for debugging only.</summary>
        /// <value>True if searching index results include search documents, false if not.</value>
        [AppSettingsKey("Clarity.Searching.AuctionIndex.ResultsIncludeSearchDocuments"),
            DefaultValue(false)]
        internal static bool SearchingAuctionIndexResultsIncludeSearchDocuments
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Enables searching for auctions.</summary>
        /// <value>True if searching for auctions, false if not.</value>
        [AppSettingsKey("Clarity.Searching.AuctionsEnabled"),
           DefaultValue(false),
           DependsOn(nameof(CEFConfigDictionary.AuctionsEnabled))]
        internal static bool SearchingAuctionsEnabled
        {
            get => CEFConfigDictionary.AuctionsEnabled
                ? CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : false
                : false;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
