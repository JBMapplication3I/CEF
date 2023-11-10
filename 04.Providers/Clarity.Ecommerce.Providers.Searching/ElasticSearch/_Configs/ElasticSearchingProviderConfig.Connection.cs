// <copyright file="ElasticSearchingProviderConfig.Connection.cs" company="clarity-ventures.com">
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
        /// <summary>Gets a value indicating whether the searching elastic search disable direct streaming.</summary>
        /// <value>True if searching elastic search disable direct streaming, false if not.</value>
        [AppSettingsKey("Clarity.Searching.ElasticSearch.DisableDirectStreaming"),
            DefaultValue(false)]
        internal static bool SearchingElasticSearchDisableDirectStreaming
        {
            get => CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) && asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching elastic search password.</summary>
        /// <value>The searching elastic search password.</value>
        [AppSettingsKey("Clarity.Searching.ElasticSearch.Password"),
            DefaultValue(null)]
        internal static string? SearchingElasticSearchPassword
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : null;
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

        /// <summary>For ElasticSearch: leave Uri blank to automatically switch between 'localhost' and 'ipv4.fiddler'.
        /// Use 'elasticsearch.corp.claritymis.com' to reference Dev-B if you can't run locally.</summary>
        /// <value>The searching elastic search URI.</value>
        [AppSettingsKey("Clarity.Searching.ElasticSearch.Uri"),
            DefaultValue("localhost")]
        internal static string SearchingElasticSearchUri
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : "localhost";
            ////get => "2019-train-1.hq.clarityinternal.com";
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching elastic search username.</summary>
        /// <value>The searching elastic search username.</value>
        [AppSettingsKey("Clarity.Searching.ElasticSearch.Username"),
            DefaultValue(null)]
        internal static string? SearchingElasticSearchUsername
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
