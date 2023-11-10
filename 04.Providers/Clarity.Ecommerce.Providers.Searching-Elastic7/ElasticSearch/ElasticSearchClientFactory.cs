// <copyright file="ElasticSearchClientFactory.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the client factory class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Domain
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Interfaces.Providers.Searching;
    using Nest;
    using Utilities;

    /// <summary>A search configuration.</summary>
    public static class ElasticSearchClientFactory
    {
        private static readonly ConnectionSettings ConnectionSettings;

        /// <summary>Initializes static members of the <see cref="ElasticSearchClientFactory" /> class.</summary>
        static ElasticSearchClientFactory()
        {
            ConnectionSettings = new ConnectionSettings(CreateUri())
                .DefaultIndex(ElasticSearchingProviderConfig.SearchingProductIndex)
                .PrettyJson()
                .DefaultMappingFor<IndexableProductModel>(i => i
                    .IndexName(ElasticSearchingProviderConfig.SearchingProductIndex)
                    .IdProperty(x => x.ID))
                .IncludeServerStackTraceOnError()
#if STORES
                .DefaultMappingFor<IndexableStoreModel>(i => i
                    .IndexName(ElasticSearchingProviderConfig.SearchingStoreIndex)
                    .IdProperty(x => x.ID))
#endif
                ;
            if (Contract.CheckAllValidKeys(
                    ElasticSearchingProviderConfig.SearchingElasticSearchUsername,
                    ElasticSearchingProviderConfig.SearchingElasticSearchPassword))
            {
                ConnectionSettings.BasicAuthentication(
                    ElasticSearchingProviderConfig.SearchingElasticSearchUsername,
                    ElasticSearchingProviderConfig.SearchingElasticSearchPassword);
            }
            if (ElasticSearchingProviderConfig.SearchingElasticSearchDisableDirectStreaming)
            {
                ConnectionSettings.DisableDirectStreaming();
            }
        }

        /// <summary>Creates an URI.</summary>
        /// <returns>The new URI.</returns>
        public static Uri CreateUri()
        {
            var host = ElasticSearchingProviderConfig.SearchingElasticSearchUri;
            if (Process.GetProcessesByName("fiddler").Any())
            {
                host = "ipv4.fiddler";
            }
            if (!host.StartsWith("http"))
            {
                host = $"http://{host}";
            }
            return new($"{host}:{ElasticSearchingProviderConfig.SearchingElasticSearchPort}");
        }

        /// <summary>Gets the client.</summary>
        /// <returns>The client.</returns>
        public static ElasticClient GetClient() => new(ConnectionSettings);

        /// <summary>Creates index name.</summary>
        /// <returns>The new index name.</returns>
        public static string CreateProductIndexName()
            => $"{ElasticSearchingProviderConfig.SearchingProductIndex}.{DateTime.UtcNow:yyyy-MM-dd.HH-mm-ss}";

#if STORES
        /// <summary>Creates store index name.</summary>
        /// <returns>The new store index name.</returns>
        public static string CreateStoreIndexName()
            => $"{ElasticSearchingProviderConfig.SearchingStoreIndex}.{DateTime.UtcNow:yyyy-MM-dd.HH-mm-ss}";
#endif
    }
}
