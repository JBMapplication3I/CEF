// <copyright file="ElasticSearchClientFactory.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the client factory class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Linq;
    using Interfaces.Models;
    using Interfaces.Providers.Searching;
    using JSConfigs;
    using Nest;
    using Utilities;

    /// <summary>A search configuration.</summary>
    public static class ElasticSearchClientFactory
    {
        /// <summary>The connection settings.</summary>
        private static ConnectionSettings? connectionSettings;

        /// <summary>Gets the client.</summary>
        /// <returns>The client.</returns>
        public static ElasticClient GetClient() => new(GenerateConnectionSettings());

        /// <summary>Generates a connection settings.</summary>
        /// <returns>The connection settings.</returns>
        private static ConnectionSettings GenerateConnectionSettings()
        {
            if (connectionSettings is not null)
            {
                return connectionSettings;
            }
            connectionSettings = new ConnectionSettings(CreateUri())
                .PrettyJson()
                .IncludeServerStackTraceOnError()
                .EnableDebugMode(apiCallDetails =>
                {
                    // Write the details to the console
                    try
                    {
                        System.Diagnostics.Debug.WriteLine(
                            Newtonsoft.Json.JsonConvert.SerializeObject(
                                apiCallDetails,
                                SerializableAttributesDictionaryExtensions.JsonSettings));
                    }
                    catch
                    {
                        // Do Nothing
                    }
                })
                .DefaultMappingFor<ProductIndexableModel>(
                    i => i.IndexName(ElasticSearchingProviderConfig.SearchingProductIndex).IdProperty(x => x.ID))
                .DefaultIndex(ElasticSearchingProviderConfig.SearchingProductIndex);
            if (CEFConfigDictionary.AuctionsEnabled)
            {
                connectionSettings
                    .DefaultMappingFor<AuctionIndexableModel>(
                        i => i.IndexName(ElasticSearchingProviderConfig.SearchingAuctionIndex ?? throw new("Invalid Auction index value")).IdProperty(x => x.ID))
                    .DefaultMappingFor<LotIndexableModel>(
                        i => i.IndexName(ElasticSearchingProviderConfig.SearchingLotIndex ?? throw new("Invalid Lot index value")).IdProperty(x => x.ID));
            }
            if (CEFConfigDictionary.CategoriesEnabled)
            {
                connectionSettings
                    .DefaultMappingFor<CategoryIndexableModel>(
                        i => i.IndexName(ElasticSearchingProviderConfig.SearchingCategoryIndex ?? throw new("Invalid Category index value")).IdProperty(x => x.ID));
            }
            if (CEFConfigDictionary.ManufacturersEnabled)
            {
                connectionSettings
                    .DefaultMappingFor<ManufacturerIndexableModel>(
                        i => i.IndexName(ElasticSearchingProviderConfig.SearchingManufacturerIndex ?? throw new("Invalid Manufacturer index value")).IdProperty(x => x.ID));
            }
            if (CEFConfigDictionary.StoresEnabled)
            {
                connectionSettings
                    .DefaultMappingFor<StoreIndexableModel>(
                        i => i.IndexName(ElasticSearchingProviderConfig.SearchingStoreIndex ?? throw new("Invalid Store index value")).IdProperty(x => x.ID));
            }
            if (CEFConfigDictionary.VendorsEnabled)
            {
                connectionSettings
                    .DefaultMappingFor<VendorIndexableModel>(
                        i => i.IndexName(ElasticSearchingProviderConfig.SearchingVendorIndex ?? throw new("Invalid Vendor index value")).IdProperty(x => x.ID));
            }
            if (CEFConfigDictionary.FranchisesEnabled)
            {
                connectionSettings
                    .DefaultMappingFor<FranchiseIndexableModel>(
                        i => i.IndexName(ElasticSearchingProviderConfig.SearchingFranchiseIndex ?? throw new("Invalid Franchise index value")).IdProperty(x => x.ID));
            }
            if (Contract.CheckAllValidKeys(
                    ElasticSearchingProviderConfig.SearchingElasticSearchUsername,
                    ElasticSearchingProviderConfig.SearchingElasticSearchPassword))
            {
                connectionSettings.BasicAuthentication(
                    ElasticSearchingProviderConfig.SearchingElasticSearchUsername,
                    ElasticSearchingProviderConfig.SearchingElasticSearchPassword);
            }
            if (ElasticSearchingProviderConfig.SearchingElasticSearchDisableDirectStreaming)
            {
                connectionSettings.DisableDirectStreaming();
            }
            return connectionSettings;
        }

        /// <summary>Creates an URI.</summary>
        /// <returns>The new URI.</returns>
        private static Uri CreateUri()
        {
            var host = ElasticSearchingProviderConfig.SearchingElasticSearchUri;
            if (System.Diagnostics.Process.GetProcessesByName("fiddler").Any())
            {
                host = "ipv4.fiddler";
            }
            if (!host.StartsWith("http"))
            {
                host = $"http://{host}";
            }
            return new($"{host}:{ElasticSearchingProviderConfig.SearchingElasticSearchPort}");
        }
    }
}
