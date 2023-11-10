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
        /// <summary>Gets the zero-based index of the searching vendor.</summary>
        /// <value>The searching vendor index.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex"),
            DefaultValue(null),
            DependsOn(nameof(CEFConfigDictionary.VendorsEnabled))]
        internal static string SearchingVendorIndex
        {
            get => CEFConfigDictionary.VendorsEnabled
                ? CEFConfigDictionary.TryGet(out string asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : throw new ConfigurationErrorsException(
                        "Clarity.Searching.VendorIndex is required in appSettings.config when vendors are enabled")
                : null!;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching index old.</summary>
        /// <value>The searching index old.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex.Old"),
            DefaultValue(null),
            DependsOn(nameof(SearchingVendorIndex))]
        internal static string SearchingVendorIndexOld
        {
            get => CEFConfigDictionary.VendorsEnabled
                ? $"{SearchingVendorIndex}-old"
                : null!;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Only get records of certain types (comma delimited), gets all types when blank.</summary>
        /// <value>The searching index filter by type keys.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex.FilterByTypeKeys"),
            DefaultValue((string[]?)null),
            SplitOn(new[] { ';', ',' })]
        internal static string[]? SearchingVendorIndexFilterByTypeKeys
        {
            get => CEFConfigDictionary.VendorsEnabled
                ? CEFConfigDictionary.TryGet(out string[]? asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : null
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Allow filtering the value of certain attribute keys in the suggest queries.</summary>
        /// <value>A comma-delimited list of attribute keys to use for suggest filtering.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex.QueryByAttributeKeys"),
            DefaultValue(null)]
        internal static string? SearchingVendorIndexQueryByAttributeKeys
        {
            get => CEFConfigDictionary.VendorsEnabled
                ? CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : null
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a comma-delimited list of attribute keys whose values should be on the suggest options.</summary>
        /// <value>A comma-delimited list of attribute keys whose values should be on the suggest options.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex.SuggestOptionAttributeKeys"),
            DefaultValue(null)]
        internal static string? SearchingVendorIndexSuggestOptionAttributeKeys
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with brands.</summary>
        /// <value>True to limit to those with brands, false if not.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex.RequiresABrand"),
            DefaultValue(false)]
        internal static bool SearchingVendorIndexRequiresABrand
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /*
        /// <summary>Gets a value indicating whether the limit to those with categories.</summary>
        /// <value>True to limit to those with categories, false if not.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex.RequiresACategory"),
            DefaultValue(true)]
        internal static bool SearchingVendorIndexRequiresACategory
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
        */

        /// <summary>Gets a value indicating whether the limit to those with franchises.</summary>
        /// <value>True to limit to those with franchises, false if not.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex.RequiresAFranchise"),
            DefaultValue(false)]
        internal static bool SearchingVendorIndexRequiresAFranchise
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with manufacturers.</summary>
        /// <value>True to limit to those with manufacturers, false if not.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex.RequiresAManufacturer"),
            DefaultValue(false)]
        internal static bool SearchingVendorIndexRequiresAManufacturer
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with products.</summary>
        /// <value>True to limit to those with products, false if not.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex.RequiresAProduct"),
            DefaultValue(false)]
        internal static bool SearchingVendorIndexRequiresAProduct
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets a value indicating whether the limit to those with stores.</summary>
        /// <value>True to limit to those with stores, false if not.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex.RequiresAStore"),
            DefaultValue(false)]
        internal static bool SearchingVendorIndexRequiresAStore
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /*
        /// <summary>Gets a value indicating whether the limit to those with vendors.</summary>
        /// <value>True to limit to those with vendors, false if not.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex.RequiresAVendor"),
            DefaultValue(false)]
        internal static bool SearchingVendorIndexRequiresAVendor
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
        */

        /// <summary>Explanatory information in the search results reply, use for debugging only.</summary>
        /// <value>True if searching index results include search documents, false if not.</value>
        [AppSettingsKey("Clarity.Searching.VendorIndex.ResultsIncludeSearchDocuments"),
            DefaultValue(false)]
        internal static bool SearchingVendorIndexResultsIncludeSearchDocuments
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Enables searching for vendors.</summary>
        /// <value>True if searching for vendors, false if not.</value>
        [AppSettingsKey("Clarity.Searching.VendorsEnabled"),
          DefaultValue(false),
          DependsOn(nameof(CEFConfigDictionary.VendorsEnabled))]
        internal static bool SearchingVendorsEnabled
        {
            get => CEFConfigDictionary.VendorsEnabled
                ? CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : false
                : false;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
