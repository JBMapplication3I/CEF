// <copyright file="ElasticSearchingProviderConfig.Kibana.cs" company="clarity-ventures.com">
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

        /// <summary>Gets URI of the searching kibana.</summary>
        /// <value>The searching kibana URI.</value>
        [AppSettingsKey("Clarity.Searching.Kibana.Uri"),
            DefaultValue("localhost"),
            Unused]
        internal static string SearchingKibanaUri
        {
            get => CEFConfigDictionary.TryGet(out string asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : "localhost";
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
