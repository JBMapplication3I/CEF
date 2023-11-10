// <copyright file="ElasticSearchingProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider tests class.</summary>
#nullable enable
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Testing
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Ecommerce.Testing;
    using Elasticsearch.Net;
    using ElasticSearch;
    using Interfaces.Providers.Searching;
    using JSConfigs;
    using Mapper;
    using Newtonsoft.Json;
    using Xunit;

    public partial class ElasticSearchingProviderTests
    {
        [Fact(Skip = "Use this to do a live run if you need it")]
        public Task LiveIndexRunnerForProducts()
        {
            CEFConfigDictionary.Load();
            BaseModelMapper.Initialize();
            return new ElasticSearchingProvider().IndexAsync(
                contextProfileName: null,
                index: ElasticSearchingProviderConfig.SearchingProductIndex,
                default);
        }

        [Fact]
        public async Task Verify_IndexingThenSuggestionsThenQueryingThenPurging_ForProducts_CompletesSuccessfully()
        {
            // Arrange
            TestOutputHelper.WriteLine("Loading...");
            CEFConfigDictionary.Load();
            BaseModelMapper.Initialize();
            const string contextProfileName = "ElasticSearchingProviderTests|Verify_IndexingThenSuggestionsThenQueryingThenPurging_ForProducts_CompletesSuccessfully";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    // Core
                    SaveChangesResult = 1,
                    DoAttributeTypeTable = true,
                    DoGeneralAttributeTable = true,
                    DoSettingTable = true,
                    DoSettingTypeTable = true,
                    DoEventStatusTable = true,
                    DoEventTypeTable = true,
                    DoEventTable = true,
                    // Main Entity
                    DoProductTable = true,
                    // Direct Links
                    DoBrandProductTable = true,
                    DoFranchiseProductTable = true,
                    DoManufacturerProductTable = true,
                    DoProductAssociationTable = true,
                    DoProductCategoryTable = true,
                    DoProductTypeTable = true,
                    DoStoreProductTable = true,
                    DoVendorProductTable = true,
                    DoReviewTable = true,
                    // Links of Direct Links
                    DoBrandTable = true,
                    DoCategoryTable = true,
                    DoFranchiseTable = true,
                    DoManufacturerTable = true,
                    DoProductAssociationTypeTable = true,
                    DoStoreTable = true,
                    DoVendorTable = true,
                };
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var provider = new ElasticSearchingProvider();
                TestOutputHelper.WriteLine("...Loaded");
                // Act/Assert
                await Verify_Indexing_For_Products_CompletesSuccessfully(provider, contextProfileName).ConfigureAwait(false);
                await Verify_Suggestions_For_Products_CompletesSuccessfully(provider, contextProfileName).ConfigureAwait(false);
                await Verify_Querying_For_Products_CompletesSuccessfully(provider, contextProfileName).ConfigureAwait(false);
                await Verify_Purging_For_Products_CompletesSuccessfully(provider, contextProfileName).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
            TestOutputHelper.WriteLine("...Exiting");
        }

        private async Task Verify_Indexing_For_Products_CompletesSuccessfully(
            ISearchingProviderBase provider,
            string contextProfileName)
        {
            // Arrange
            TestOutputHelper.WriteLine("Indexing");
            // Act
            await provider.IndexAsync(
                    contextProfileName,
                    ElasticSearchingProviderConfig.SearchingProductIndex,
                    default)
                .ConfigureAwait(false);
            // Assert
            TestOutputHelper.WriteLine("...Indexed");
        }

        private async Task Verify_Suggestions_For_Products_CompletesSuccessfully(
            ISearchingProviderBase provider,
            string contextProfileName)
        {
            // Arrange
            TestOutputHelper.WriteLine("Asking for suggestions");
            ProductCatalogSearchForm form = new()
            {
                // PageSize = 4,
                // Page = 1,
                // PageFormat = "grid",
                // Sort = SearchSort.Relevance,
                Query = "200ml",
                // Category = "Home", // Single-Category
                // CategoriesAny = new [] { "Home"/*, "Violins"*/ }, // Multiple-Category (Any single match)
                // CategoriesAll = new [] { "Home", "Lighting" }, // Multiple-Category (All must match)
                // PriceRanges = new [] { "< $50", "$1000 +" },
                // AttributesAny = new Dictionary<string, string[]>
                // {
                //     ["FDA"] = new[] { "True" },
                //     ["Minimum Operating Temp (°F )"] = new[] { "-452", "-418", "-400", "-328", "-320", "-319", "-200" }
                // },
                // AttributesAll = new Dictionary<string, string[]>
                // {
                //     ["FDA"] = new[] { "True" },
                //     ["Minimum Operating Temp (°F )"] = new[] { "-452", "-418", "-400", "-328", "-320", "-319", "-200" }
                // },
            };
            TestOutputHelper.WriteLine(JsonConvert.SerializeObject(form));
            // Act
            var result = await provider.SuggestionsAsync<ProductCatalogSearchForm, ProductSuggestResult>(
                    form,
                    ElasticSearchingProviderConfig.SearchingProductIndex,
                    contextProfileName)
                .ConfigureAwait(false);
            // Assert
            Assert.NotNull(result);
            TestOutputHelper.WriteLine(JsonConvert.SerializeObject(result));
            Assert.True(result.Any(), "No results found");
            TestOutputHelper.WriteLine("...Suggestions given");
        }

        private async Task Verify_Querying_For_Products_CompletesSuccessfully(
            ISearchingProviderBase provider,
            string contextProfileName)
        {
            // Arrange
            ProductCatalogSearchForm form = new()
            {
                PageSize = 1,
                PageSetSize = 5,
                Page = 1,
                PageFormat = "grid",
                Sort = Enums.SearchSort.Relevance,
                RolesAny = Array.Empty<string>(),
                // Query = "3M",
                // Category = "Agriculture|CAT-21", // Single-Category
                // CategoriesAny = new [] { "Home"/*, "Violins"*/ }, // Multiple-Category (Any single match)
                // CategoriesAll = new [] { "Home", "Lighting" }, // Multiple-Category (All must match)
                // PriceRanges = new [] { "< $50", "$1000 +" },
                // AttributesAny = new Dictionary<string, string[]>
                // {
                //     ["FDA"] = new[] { "True" },
                //     ["Minimum Operating Temp (°F )"] = new[] { "-452", "-418", "-400", "-328", "-320", "-319", "-200" }
                // },
                // AttributesAll = new Dictionary<string, string[]>
                // {
                //     ["FDA"] = new[] { "True" },
                //     ["Minimum Operating Temp (°F )"] = new[] { "-452", "-418", "-400", "-328", "-320", "-319", "-200" }
                // },
            };
            TestOutputHelper.WriteLine("Asking for results");
            // Act
            var result = await provider.QueryAsync<ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel>(
                    form,
                    ElasticSearchingProviderConfig.SearchingProductIndex,
                    contextProfileName)
                .ConfigureAwait(false);
            // Assert
            Assert.NotNull(result);
            TestOutputHelper.WriteLine(JsonConvert.SerializeObject(result));
            Assert.NotNull(result.Form);
            var rootCauses = string.Empty;
            if (result.ServerError != null)
            {
                var serverError = (ServerError)result.ServerError;
                rootCauses = serverError.Error.RootCause
                    .Select(x => x.Reason)
                    .Aggregate(string.Empty, (c, n) => c + "\r\n" + n);
            }
            Assert.True(result.IsValid, result.DebugInformation + "\r\n\r\n" + result.ServerError + "\r\n\r\n" + rootCauses);
            Assert.Null(result.ServerError);
            Assert.Null(result.DebugInformation);
            TestOutputHelper.WriteLine($"{result.ElapsedMilliseconds:N0}ms elapsed");
            TestOutputHelper.WriteLine($"{result.Total:N0} Results");
            TestOutputHelper.WriteLine($"{result.TotalPages:N0} Pages");
            TestOutputHelper.WriteLine($"{result.Documents!.LongCount():N0} Documents");
            TestOutputHelper.WriteLine($"{result.ResultIDs.LongCount():N0} Result IDs");
            TestOutputHelper.WriteLine($"{result.Attributes!.LongCount():N0} Attributes");
            TestOutputHelper.WriteLine($"{result.BrandIDs!.LongCount():N0} Brand IDs");
            TestOutputHelper.WriteLine($"{result.CategoriesTree!.Children!.LongCount():N0} Category Tree Children");
            TestOutputHelper.WriteLine($"{result.FranchiseIDs!.LongCount():N0} Franchise IDs");
            TestOutputHelper.WriteLine($"{result.ManufacturerIDs!.LongCount():N0} Manufacturer IDs");
            TestOutputHelper.WriteLine($"{result.PricingRanges!.LongCount():N0} Price Ranges");
            TestOutputHelper.WriteLine($"{result.RatingRanges!.LongCount():N0} Rating Ranges");
            TestOutputHelper.WriteLine($"{result.StoreIDs!.LongCount():N0} Store IDs");
            TestOutputHelper.WriteLine($"{result.VendorIDs!.LongCount():N0} Vendor IDs");
            Assert.True(result.ElapsedMilliseconds < 1_000, "Took too long");
            Assert.True(result.Total > 0, "No results found");
            Assert.True(result.TotalPages > 1, "Results should have been paged");
            Assert.True(result.Documents!.LongCount() > 0, "Results themselves should have come back with the Query");
            Assert.True(result.HitsMetaDataHitScores!.Count == 1, "Hits with MetaData Scors should have come back with the Query");
            Assert.Equal(1d, result.HitsMetaDataHitScores["1152"]); // Because we didn't tell it to filter or query by anything, gets default score
            Assert.True(result.Attributes!.Any(), "Facets (Aggregation Points) of type Attributes should have come back with the Query");
            Assert.True(result.BrandIDs!.Count == 1, "Facets (Aggregation Points) of type Brand IDs should have come back with the Query");
            Assert.True(result.CategoriesTree!.Children!.Any(), "Facets (Aggregation Points) of type Categories should have come back with the Query");
            Assert.True(result.FranchiseIDs!.Count == 1, "Facets (Aggregation Points) of type Franchise IDs should have come back with the Query");
            Assert.True(result.ManufacturerIDs!.Count == 1, "Facets (Aggregation Points) of type Manufacturer IDs should have come back with the Query");
            Assert.True(result.PricingRanges!.Count == 12, "Facets (Aggregation Points) of type Pricing Ranges should have come back with the Query");
            Assert.True(result.PricingRanges.ElementAt(0).DocCount == 2, "Facets (Aggregation Points) of type Pricing Ranges should have come back with the Query");
            Assert.True(result.RatingRanges!.Count == 5, "Facets (Aggregation Points) of type Rating Ranges should have come back with the Query");
            Assert.True(result.StoreIDs!.Count == 1, "Facets (Aggregation Points) of type Store IDs should have come back with the Query");
            Assert.True(result.VendorIDs!.Count == 4, "Facets (Aggregation Points) of type Vendor IDs should have come back with the Query");
            TestOutputHelper.WriteLine("...Results given");
            TestOutputHelper.WriteLine("Asking for Filtered Results A");
            // Arrange
            form.Query = "Wine";
            // Act
            var result2 = await provider.QueryAsync<ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel>(
                    form,
                    ElasticSearchingProviderConfig.SearchingProductIndex,
                    contextProfileName)
                .ConfigureAwait(false);
            // Assert
            Assert.NotNull(result2);
            TestOutputHelper.WriteLine(JsonConvert.SerializeObject(result2));
            Assert.NotNull(result2.Form);
            rootCauses = string.Empty;
            if (result2.ServerError != null)
            {
                var serverError = (ServerError)result2.ServerError;
                rootCauses = serverError.Error.RootCause
                    .Select(x => x.Reason)
                    .Aggregate(string.Empty, (c, n) => c + "\r\n" + n);
            }
            Assert.True(result2.IsValid, result2.DebugInformation + "\r\n\r\n" + result2.ServerError + "\r\n\r\n" + rootCauses);
            Assert.Null(result2.ServerError);
            Assert.Null(result2.DebugInformation);
            TestOutputHelper.WriteLine($"{result2.ElapsedMilliseconds:N0}ms elapsed");
            TestOutputHelper.WriteLine($"{result2.Total:N0} Results");
            TestOutputHelper.WriteLine($"{result2.TotalPages:N0} Pages");
            TestOutputHelper.WriteLine($"{result2.Documents!.LongCount():N0} Documents");
            TestOutputHelper.WriteLine($"{result2.ResultIDs.LongCount():N0} Result IDs");
            TestOutputHelper.WriteLine($"{result2.Attributes!.LongCount():N0} Attributes");
            TestOutputHelper.WriteLine($"{result2.BrandIDs!.LongCount():N0} Brand IDs");
            TestOutputHelper.WriteLine($"{result2.CategoriesTree!.Children!.LongCount():N0} Category Tree Children");
            TestOutputHelper.WriteLine($"{result2.FranchiseIDs!.LongCount():N0} Franchise IDs");
            TestOutputHelper.WriteLine($"{result2.ManufacturerIDs!.LongCount():N0} Manufacturer IDs");
            TestOutputHelper.WriteLine($"{result2.PricingRanges!.LongCount():N0} Price Ranges");
            TestOutputHelper.WriteLine($"{result2.RatingRanges!.LongCount():N0} Rating Ranges");
            TestOutputHelper.WriteLine($"{result2.StoreIDs!.LongCount():N0} Store IDs");
            TestOutputHelper.WriteLine($"{result2.VendorIDs!.LongCount():N0} Vendor IDs");
            Assert.True(result2.ElapsedMilliseconds < 1_000, "Took too long");
            Assert.True(result2.Total > 0, "No results found");
            Assert.True(result2.TotalPages > 1, "Results should have been paged");
            Assert.True(result2.Documents!.LongCount() > 0, "Results themselves should have come back with the Query");
            Assert.True(result2.HitsMetaDataHitScores!.Count == 1, "Hits with MetaData Scors should have come back with the Query");
            Assert.Equal(591.6716d, result2.HitsMetaDataHitScores["1152"]); // Because we gave it something to match against, it can score it
            Assert.True(result2.Attributes!.Any(), "Facets (Aggregation Points) of type Attributes should have come back with the Query");
            Assert.True(result2.BrandIDs!.Count == 1, "Facets (Aggregation Points) of type Brand IDs should have come back with the Query");
            Assert.True(result2.CategoriesTree!.Children!.Any(), "Facets (Aggregation Points) of type Categories should have come back with the Query");
            Assert.True(result2.FranchiseIDs!.Count == 1, "Facets (Aggregation Points) of type Franchise IDs should have come back with the Query");
            Assert.True(result2.ManufacturerIDs!.Count == 1, "Facets (Aggregation Points) of type Manufacturer IDs should have come back with the Query");
            Assert.True(result2.PricingRanges!.Count == 12, "Facets (Aggregation Points) of type Pricing Ranges should have come back with the Query");
            Assert.True(result2.PricingRanges.ElementAt(0).DocCount == 2, "Facets (Aggregation Points) of type Pricing Ranges should have come back with the Query");
            Assert.True(result2.RatingRanges!.Count == 5, "Facets (Aggregation Points) of type Rating Ranges should have come back with the Query");
            Assert.True(result2.StoreIDs!.Count == 1, "Facets (Aggregation Points) of type Store IDs should have come back with the Query");
            Assert.True(result2.VendorIDs!.Count == 4, "Facets (Aggregation Points) of type Vendor IDs should have come back with the Query");
            TestOutputHelper.WriteLine("...Filtered Results A given");
            // Arrange
            TestOutputHelper.WriteLine("Asking for Filtered Results B");
            form.Query = null;
            form.RatingRanges = new[] { "3.01 - 4" };
            // Act
            var result3 = await provider.QueryAsync<ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel>(
                    form,
                    ElasticSearchingProviderConfig.SearchingProductIndex,
                    contextProfileName)
                .ConfigureAwait(false);
            // Assert
            Assert.NotNull(result3);
            TestOutputHelper.WriteLine(JsonConvert.SerializeObject(result3));
            Assert.NotNull(result3.Form);
            rootCauses = string.Empty;
            if (result3.ServerError != null)
            {
                var serverError = (ServerError)result3.ServerError;
                rootCauses = serverError.Error.RootCause
                    .Select(x => x.Reason)
                    .Aggregate(string.Empty, (c, n) => c + "\r\n" + n);
            }
            Assert.True(result3.IsValid, result3.DebugInformation + "\r\n\r\n" + result3.ServerError + "\r\n\r\n" + rootCauses);
            Assert.Null(result3.ServerError);
            Assert.Null(result3.DebugInformation);
            TestOutputHelper.WriteLine($"{result3.ElapsedMilliseconds:N0}ms elapsed");
            TestOutputHelper.WriteLine($"{result3.Total:N0} Results");
            TestOutputHelper.WriteLine($"{result3.TotalPages:N0} Pages");
            TestOutputHelper.WriteLine($"{result3.Documents!.LongCount():N0} Documents");
            TestOutputHelper.WriteLine($"{result3.ResultIDs.LongCount():N0} Result IDs");
            TestOutputHelper.WriteLine($"{result3.Attributes!.LongCount():N0} Attributes");
            TestOutputHelper.WriteLine($"{result3.BrandIDs!.LongCount():N0} Brand IDs");
            TestOutputHelper.WriteLine($"{result3.CategoriesTree!.Children!.LongCount():N0} Category Tree Children");
            TestOutputHelper.WriteLine($"{result3.FranchiseIDs!.LongCount():N0} Franchise IDs");
            TestOutputHelper.WriteLine($"{result3.ManufacturerIDs!.LongCount():N0} Manufacturer IDs");
            TestOutputHelper.WriteLine($"{result3.PricingRanges!.LongCount():N0} Price Ranges");
            TestOutputHelper.WriteLine($"{result3.RatingRanges!.LongCount():N0} Rating Ranges");
            TestOutputHelper.WriteLine($"{result3.StoreIDs!.LongCount():N0} Store IDs");
            TestOutputHelper.WriteLine($"{result3.VendorIDs!.LongCount():N0} Vendor IDs");
            Assert.True(result3.ElapsedMilliseconds < 1_000, "Took too long");
            Assert.True(result3.Total > 0, "No results found");
            Assert.True(result3.TotalPages == 1, "Results should not have been paged");
            Assert.True(result3.Documents!.LongCount() > 0, "Results themselves should have come back with the Query");
            Assert.True(result3.HitsMetaDataHitScores!.Count == 1, "Hits with MetaData Scors should have come back with the Query");
            Assert.Equal(1d, result3.HitsMetaDataHitScores["1152"]); // Because we gave it something to match against, it can score it
            Assert.False(result3.Attributes!.Any(), "Facets (Aggregation Points) of type Attributes should have come back with the Query");
            Assert.True(result3.BrandIDs!.Count == 1, "Facets (Aggregation Points) of type Brand IDs should have come back with the Query");
            Assert.True(result3.CategoriesTree!.Children!.Any(), "Facets (Aggregation Points) of type Categories should have come back with the Query");
            Assert.True(result3.FranchiseIDs!.Count == 1, "Facets (Aggregation Points) of type Franchise IDs should have come back with the Query");
            Assert.True(result3.ManufacturerIDs!.Count == 1, "Facets (Aggregation Points) of type Manufacturer IDs should have come back with the Query");
            Assert.True(result3.PricingRanges!.Count == 12, "Facets (Aggregation Points) of type Pricing Ranges should have come back with the Query");
            Assert.True(result3.RatingRanges!.Count == 5, "Facets (Aggregation Points) of type Rating Ranges should have come back with the Query");
            Assert.True(result3.RatingRanges.ElementAt(3).DocCount == 1, "Facets (Aggregation Points) of type Pricing Ranges should have come back with the Query");
            Assert.True(result3.StoreIDs!.Count == 1, "Facets (Aggregation Points) of type Store IDs should have come back with the Query");
            Assert.True(result3.VendorIDs!.Count == 4, "Facets (Aggregation Points) of type Vendor IDs should have come back with the Query");
            TestOutputHelper.WriteLine("...Filtered Results B given");
            // Arrange
            TestOutputHelper.WriteLine("Asking for Filtered Results C");
            form.RatingRanges = null;
            form.PricingRanges = new[] { "< $50" };
            // Act
            var result4 = await provider.QueryAsync<ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel>(
                    form,
                    ElasticSearchingProviderConfig.SearchingProductIndex,
                    contextProfileName)
                .ConfigureAwait(false);
            // Assert
            Assert.NotNull(result4);
            TestOutputHelper.WriteLine(JsonConvert.SerializeObject(result4));
            Assert.NotNull(result4.Form);
            rootCauses = string.Empty;
            if (result4.ServerError != null)
            {
                var serverError = (ServerError)result4.ServerError;
                rootCauses = serverError.Error.RootCause
                    .Select(x => x.Reason)
                    .Aggregate(string.Empty, (c, n) => c + "\r\n" + n);
            }
            Assert.True(result4.IsValid, result4.DebugInformation + "\r\n\r\n" + result4.ServerError + "\r\n\r\n" + rootCauses);
            Assert.Null(result4.ServerError);
            Assert.Null(result4.DebugInformation);
            TestOutputHelper.WriteLine($"{result4.ElapsedMilliseconds:N0}ms elapsed");
            TestOutputHelper.WriteLine($"{result4.Total:N0} Results");
            TestOutputHelper.WriteLine($"{result4.TotalPages:N0} Pages");
            TestOutputHelper.WriteLine($"{result4.Documents!.LongCount():N0} Documents");
            TestOutputHelper.WriteLine($"{result4.ResultIDs.LongCount():N0} Result IDs");
            TestOutputHelper.WriteLine($"{result4.Attributes!.LongCount():N0} Attributes");
            TestOutputHelper.WriteLine($"{result4.BrandIDs!.LongCount():N0} Brand IDs");
            TestOutputHelper.WriteLine($"{result4.CategoriesTree!.Children!.LongCount():N0} Category Tree Children");
            TestOutputHelper.WriteLine($"{result4.FranchiseIDs!.LongCount():N0} Franchise IDs");
            TestOutputHelper.WriteLine($"{result4.ManufacturerIDs!.LongCount():N0} Manufacturer IDs");
            TestOutputHelper.WriteLine($"{result4.PricingRanges!.LongCount():N0} Price Ranges");
            TestOutputHelper.WriteLine($"{result4.RatingRanges!.LongCount():N0} Rating Ranges");
            TestOutputHelper.WriteLine($"{result4.StoreIDs!.LongCount():N0} Store IDs");
            TestOutputHelper.WriteLine($"{result4.VendorIDs!.LongCount():N0} Vendor IDs");
            Assert.True(result4.ElapsedMilliseconds < 1_000, "Took too long");
            Assert.True(result4.Total > 0, "No results found");
            Assert.True(result4.TotalPages > 1, "Results should have been paged");
            Assert.True(result4.Documents!.LongCount() > 0, "Results themselves should have come back with the Query");
            Assert.True(result4.HitsMetaDataHitScores!.Count == 1, "Hits with MetaData Scors should have come back with the Query");
            Assert.Equal(1d, result4.HitsMetaDataHitScores["1152"]); // Because we gave it something to match against, it can score it
            Assert.True(result4.Attributes!.Any(), "Facets (Aggregation Points) of type Attributes should have come back with the Query");
            Assert.True(result4.BrandIDs!.Count == 1, "Facets (Aggregation Points) of type Brand IDs should have come back with the Query");
            Assert.True(result4.CategoriesTree!.Children!.Any(), "Facets (Aggregation Points) of type Categories should have come back with the Query");
            Assert.True(result4.FranchiseIDs!.Count == 1, "Facets (Aggregation Points) of type Franchise IDs should have come back with the Query");
            Assert.True(result4.ManufacturerIDs!.Count == 1, "Facets (Aggregation Points) of type Manufacturer IDs should have come back with the Query");
            Assert.True(result4.PricingRanges!.Count == 12, "Facets (Aggregation Points) of type Pricing Ranges should have come back with the Query");
            Assert.True(result4.RatingRanges!.Count == 5, "Facets (Aggregation Points) of type Rating Ranges should have come back with the Query");
            Assert.True(result4.StoreIDs!.Count == 1, "Facets (Aggregation Points) of type Store IDs should have come back with the Query");
            Assert.True(result4.VendorIDs!.Count == 4, "Facets (Aggregation Points) of type Vendor IDs should have come back with the Query");
            TestOutputHelper.WriteLine("...Filtered Results C given");
            // Arrange
            TestOutputHelper.WriteLine("Asking for Filtered Results D");
            form.PricingRanges = null;
            form.AttributesAll = new() { ["Color"] = new[] { "Blue", }, };
            // Act
            var result5 = await provider.QueryAsync<ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel>(
                    form,
                    ElasticSearchingProviderConfig.SearchingProductIndex,
                    contextProfileName)
                .ConfigureAwait(false);
            // Assert
            Assert.NotNull(result5);
            TestOutputHelper.WriteLine(JsonConvert.SerializeObject(result5));
            Assert.NotNull(result5.Form);
            rootCauses = string.Empty;
            if (result5.ServerError != null)
            {
                var serverError = (ServerError)result5.ServerError;
                rootCauses = serverError.Error.RootCause
                    .Select(x => x.Reason)
                    .Aggregate(string.Empty, (c, n) => c + "\r\n" + n);
            }
            Assert.True(result5.IsValid, result5.DebugInformation + "\r\n\r\n" + result5.ServerError + "\r\n\r\n" + rootCauses);
            Assert.Null(result5.ServerError);
            Assert.Null(result5.DebugInformation);
            TestOutputHelper.WriteLine($"{result5.ElapsedMilliseconds:N0}ms elapsed");
            TestOutputHelper.WriteLine($"{result5.Total:N0} Results");
            TestOutputHelper.WriteLine($"{result5.TotalPages:N0} Pages");
            TestOutputHelper.WriteLine($"{result5.Documents!.LongCount():N0} Documents");
            TestOutputHelper.WriteLine($"{result5.ResultIDs.LongCount():N0} Result IDs");
            TestOutputHelper.WriteLine($"{result5.Attributes!.LongCount():N0} Attributes");
            TestOutputHelper.WriteLine($"{result5.BrandIDs!.LongCount():N0} Brand IDs");
            TestOutputHelper.WriteLine($"{result5.CategoriesTree!.Children!.LongCount():N0} Category Tree Children");
            TestOutputHelper.WriteLine($"{result5.FranchiseIDs!.LongCount():N0} Franchise IDs");
            TestOutputHelper.WriteLine($"{result5.ManufacturerIDs!.LongCount():N0} Manufacturer IDs");
            TestOutputHelper.WriteLine($"{result5.PricingRanges!.LongCount():N0} Price Ranges");
            TestOutputHelper.WriteLine($"{result5.RatingRanges!.LongCount():N0} Rating Ranges");
            TestOutputHelper.WriteLine($"{result5.StoreIDs!.LongCount():N0} Store IDs");
            TestOutputHelper.WriteLine($"{result5.VendorIDs!.LongCount():N0} Vendor IDs");
            Assert.True(result5.ElapsedMilliseconds < 1_000, "Took too long");
            Assert.True(result5.Total > 0, "No results found");
            Assert.True(result5.TotalPages == 1, "Results should not have been paged");
            Assert.True(result5.Documents!.LongCount() > 0, "Results themselves should have come back with the Query");
            Assert.True(result5.HitsMetaDataHitScores!.Count == 1, "Hits with MetaData Scors should have come back with the Query");
            Assert.Equal(1d, result5.HitsMetaDataHitScores["1151"]); // Because we gave it something to match against, it can score it
            Assert.True(result5.Attributes!.Any(), "Facets (Aggregation Points) of type Attributes should have come back with the Query");
            Assert.True(result5.BrandIDs!.Count == 1, "Facets (Aggregation Points) of type Brand IDs should have come back with the Query");
            Assert.True(result5.CategoriesTree!.Children!.Any(), "Facets (Aggregation Points) of type Categories should have come back with the Query");
            Assert.True(result5.FranchiseIDs!.Count == 1, "Facets (Aggregation Points) of type Franchise IDs should have come back with the Query");
            Assert.True(result5.ManufacturerIDs!.Count == 1, "Facets (Aggregation Points) of type Manufacturer IDs should have come back with the Query");
            Assert.True(result5.PricingRanges!.Count == 12, "Facets (Aggregation Points) of type Pricing Ranges should have come back with the Query");
            Assert.True(result5.RatingRanges!.Count == 5, "Facets (Aggregation Points) of type Rating Ranges should have come back with the Query");
            Assert.True(result5.StoreIDs!.Count == 1, "Facets (Aggregation Points) of type Store IDs should have come back with the Query");
            Assert.True(result5.VendorIDs!.Count == 4, "Facets (Aggregation Points) of type Vendor IDs should have come back with the Query");
            TestOutputHelper.WriteLine("...Filtered Results D given");
            // Arrange
            TestOutputHelper.WriteLine("Asking for Filtered Results E");
            form.PricingRanges = null;
            form.AttributesAny = new() { ["Color"] = new[] { "Blue", }, };
            // Act
            var result6 = await provider.QueryAsync<ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel>(
                    form,
                    ElasticSearchingProviderConfig.SearchingProductIndex,
                    contextProfileName)
                .ConfigureAwait(false);
            // Assert
            Assert.NotNull(result6);
            TestOutputHelper.WriteLine(JsonConvert.SerializeObject(result6));
            Assert.NotNull(result6.Form);
            rootCauses = string.Empty;
            if (result6.ServerError != null)
            {
                var serverError = (ServerError)result6.ServerError;
                rootCauses = serverError.Error.RootCause
                    .Select(x => x.Reason)
                    .Aggregate(string.Empty, (c, n) => c + "\r\n" + n);
            }
            Assert.True(result6.IsValid, result6.DebugInformation + "\r\n\r\n" + result6.ServerError + "\r\n\r\n" + rootCauses);
            Assert.Null(result6.ServerError);
            Assert.Null(result6.DebugInformation);
            TestOutputHelper.WriteLine($"{result6.ElapsedMilliseconds:N0}ms elapsed");
            TestOutputHelper.WriteLine($"{result6.Total:N0} Results");
            TestOutputHelper.WriteLine($"{result6.TotalPages:N0} Pages");
            TestOutputHelper.WriteLine($"{result6.Documents!.LongCount():N0} Documents");
            TestOutputHelper.WriteLine($"{result6.ResultIDs.LongCount():N0} Result IDs");
            TestOutputHelper.WriteLine($"{result6.Attributes!.LongCount():N0} Attributes");
            TestOutputHelper.WriteLine($"{result6.BrandIDs!.LongCount():N0} Brand IDs");
            TestOutputHelper.WriteLine($"{result6.CategoriesTree!.Children!.LongCount():N0} Category Tree Children");
            TestOutputHelper.WriteLine($"{result6.FranchiseIDs!.LongCount():N0} Franchise IDs");
            TestOutputHelper.WriteLine($"{result6.ManufacturerIDs!.LongCount():N0} Manufacturer IDs");
            TestOutputHelper.WriteLine($"{result6.PricingRanges!.LongCount():N0} Price Ranges");
            TestOutputHelper.WriteLine($"{result6.RatingRanges!.LongCount():N0} Rating Ranges");
            TestOutputHelper.WriteLine($"{result6.StoreIDs!.LongCount():N0} Store IDs");
            TestOutputHelper.WriteLine($"{result6.VendorIDs!.LongCount():N0} Vendor IDs");
            Assert.True(result6.ElapsedMilliseconds < 1_000, "Took too long");
            Assert.True(result6.Total > 0, "No results found");
            Assert.True(result6.TotalPages == 1, "Results should not have been paged");
            Assert.True(result6.Documents!.LongCount() > 0, "Results themselves should have come back with the Query");
            Assert.True(result6.HitsMetaDataHitScores!.Count == 1, "Hits with MetaData Scors should have come back with the Query");
            Assert.Equal(1d, result6.HitsMetaDataHitScores["1151"]); // Because we gave it something to match against, it can score it
            Assert.True(result6.Attributes!.Any(), "Facets (Aggregation Points) of type Attributes should have come back with the Query");
            Assert.True(result6.BrandIDs!.Count == 1, "Facets (Aggregation Points) of type Brand IDs should have come back with the Query");
            Assert.True(result6.CategoriesTree!.Children!.Any(), "Facets (Aggregation Points) of type Categories should have come back with the Query");
            Assert.True(result6.FranchiseIDs!.Count == 1, "Facets (Aggregation Points) of type Franchise IDs should have come back with the Query");
            Assert.True(result6.ManufacturerIDs!.Count == 1, "Facets (Aggregation Points) of type Manufacturer IDs should have come back with the Query");
            Assert.True(result6.PricingRanges!.Count == 12, "Facets (Aggregation Points) of type Pricing Ranges should have come back with the Query");
            Assert.True(result6.RatingRanges!.Count == 5, "Facets (Aggregation Points) of type Rating Ranges should have come back with the Query");
            Assert.True(result6.StoreIDs!.Count == 1, "Facets (Aggregation Points) of type Store IDs should have come back with the Query");
            Assert.True(result6.VendorIDs!.Count == 4, "Facets (Aggregation Points) of type Vendor IDs should have come back with the Query");
            TestOutputHelper.WriteLine("...Filtered Results E given");
            // Arrange
            TestOutputHelper.WriteLine("Asking for Filtered Results F");
            form.AttributesAny = null;
            form.CategoriesAny = new[] { "Wine|WINE", };
            // Act
            var result7 = await provider.QueryAsync<ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel>(
                    form,
                    ElasticSearchingProviderConfig.SearchingProductIndex,
                    contextProfileName)
                .ConfigureAwait(false);
            // Assert
            Assert.NotNull(result7);
            TestOutputHelper.WriteLine(JsonConvert.SerializeObject(result7));
            Assert.NotNull(result7.Form);
            rootCauses = string.Empty;
            if (result7.ServerError != null)
            {
                var serverError = (ServerError)result7.ServerError;
                rootCauses = serverError.Error.RootCause
                    .Select(x => x.Reason)
                    .Aggregate(string.Empty, (c, n) => c + "\r\n" + n);
            }
            Assert.True(result7.IsValid, result7.DebugInformation + "\r\n\r\n" + result7.ServerError + "\r\n\r\n" + rootCauses);
            Assert.Null(result7.ServerError);
            Assert.Null(result7.DebugInformation);
            TestOutputHelper.WriteLine($"{result7.ElapsedMilliseconds:N0}ms elapsed");
            TestOutputHelper.WriteLine($"{result7.Total:N0} Results");
            TestOutputHelper.WriteLine($"{result7.TotalPages:N0} Pages");
            TestOutputHelper.WriteLine($"{result7.Documents!.LongCount():N0} Documents");
            TestOutputHelper.WriteLine($"{result7.ResultIDs.LongCount():N0} Result IDs");
            TestOutputHelper.WriteLine($"{result7.Attributes!.LongCount():N0} Attributes");
            TestOutputHelper.WriteLine($"{result7.BrandIDs!.LongCount():N0} Brand IDs");
            TestOutputHelper.WriteLine($"{result7.CategoriesTree!.Children!.LongCount():N0} Category Tree Children");
            TestOutputHelper.WriteLine($"{result7.FranchiseIDs!.LongCount():N0} Franchise IDs");
            TestOutputHelper.WriteLine($"{result7.ManufacturerIDs!.LongCount():N0} Manufacturer IDs");
            TestOutputHelper.WriteLine($"{result7.PricingRanges!.LongCount():N0} Price Ranges");
            TestOutputHelper.WriteLine($"{result7.RatingRanges!.LongCount():N0} Rating Ranges");
            TestOutputHelper.WriteLine($"{result7.StoreIDs!.LongCount():N0} Store IDs");
            TestOutputHelper.WriteLine($"{result7.VendorIDs!.LongCount():N0} Vendor IDs");
            Assert.True(result7.ElapsedMilliseconds < 1_000, "Took too long");
            Assert.True(result7.Total > 0, "No results found");
            Assert.True(result7.TotalPages == 1, "Results should not have been paged");
            Assert.True(result7.Documents!.LongCount() > 0, "Results themselves should have come back with the Query");
            Assert.True(result7.HitsMetaDataHitScores!.Count == 1, "Hits with MetaData Scors should have come back with the Query");
            Assert.Equal(1d, result7.HitsMetaDataHitScores["1151"]); // Because we gave it something to match against, it can score it
            Assert.True(result7.Attributes!.Any(), "Facets (Aggregation Points) of type Attributes should have come back with the Query");
            Assert.True(result7.BrandIDs!.Count == 1, "Facets (Aggregation Points) of type Brand IDs should have come back with the Query");
            Assert.True(result7.CategoriesTree!.Children!.Any(), "Facets (Aggregation Points) of type Categories should have come back with the Query");
            Assert.True(result7.FranchiseIDs!.Count == 1, "Facets (Aggregation Points) of type Franchise IDs should have come back with the Query");
            Assert.True(result7.ManufacturerIDs!.Count == 1, "Facets (Aggregation Points) of type Manufacturer IDs should have come back with the Query");
            Assert.True(result7.PricingRanges!.Count == 12, "Facets (Aggregation Points) of type Pricing Ranges should have come back with the Query");
            Assert.True(result7.RatingRanges!.Count == 5, "Facets (Aggregation Points) of type Rating Ranges should have come back with the Query");
            Assert.True(result7.StoreIDs!.Count == 1, "Facets (Aggregation Points) of type Store IDs should have come back with the Query");
            Assert.True(result7.VendorIDs!.Count == 4, "Facets (Aggregation Points) of type Vendor IDs should have come back with the Query");
            TestOutputHelper.WriteLine("...Filtered Results F given");
            // Arrange
            TestOutputHelper.WriteLine("Asking for Filtered Results G");
            form.CategoriesAny = null;
            form.CategoriesAll = new[] { "Wine|WINE", };
            // Act
            var result8 = await provider.QueryAsync<ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel>(
                    form,
                    ElasticSearchingProviderConfig.SearchingProductIndex,
                    contextProfileName)
                .ConfigureAwait(false);
            // Assert
            Assert.NotNull(result8);
            TestOutputHelper.WriteLine(JsonConvert.SerializeObject(result8));
            Assert.NotNull(result8.Form);
            rootCauses = string.Empty;
            if (result8.ServerError != null)
            {
                var serverError = (ServerError)result8.ServerError;
                rootCauses = serverError.Error.RootCause
                    .Select(x => x.Reason)
                    .Aggregate(string.Empty, (c, n) => c + "\r\n" + n);
            }
            Assert.True(result8.IsValid, result8.DebugInformation + "\r\n\r\n" + result8.ServerError + "\r\n\r\n" + rootCauses);
            Assert.Null(result8.ServerError);
            Assert.Null(result8.DebugInformation);
            TestOutputHelper.WriteLine($"{result8.ElapsedMilliseconds:N0}ms elapsed");
            TestOutputHelper.WriteLine($"{result8.Total:N0} Results");
            TestOutputHelper.WriteLine($"{result8.TotalPages:N0} Pages");
            TestOutputHelper.WriteLine($"{result8.Documents!.LongCount():N0} Documents");
            TestOutputHelper.WriteLine($"{result8.ResultIDs.LongCount():N0} Result IDs");
            TestOutputHelper.WriteLine($"{result8.Attributes!.LongCount():N0} Attributes");
            TestOutputHelper.WriteLine($"{result8.BrandIDs!.LongCount():N0} Brand IDs");
            TestOutputHelper.WriteLine($"{result8.CategoriesTree!.Children!.LongCount():N0} Category Tree Children");
            TestOutputHelper.WriteLine($"{result8.FranchiseIDs!.LongCount():N0} Franchise IDs");
            TestOutputHelper.WriteLine($"{result8.ManufacturerIDs!.LongCount():N0} Manufacturer IDs");
            TestOutputHelper.WriteLine($"{result8.PricingRanges!.LongCount():N0} Price Ranges");
            TestOutputHelper.WriteLine($"{result8.RatingRanges!.LongCount():N0} Rating Ranges");
            TestOutputHelper.WriteLine($"{result8.StoreIDs!.LongCount():N0} Store IDs");
            TestOutputHelper.WriteLine($"{result8.VendorIDs!.LongCount():N0} Vendor IDs");
            Assert.True(result8.ElapsedMilliseconds < 1_000, "Took too long");
            Assert.True(result8.Total > 0, "No results found");
            Assert.True(result8.TotalPages == 1, "Results should not have been paged");
            Assert.True(result8.Documents!.LongCount() > 0, "Results themselves should have come back with the Query");
            Assert.True(result8.HitsMetaDataHitScores!.Count == 1, "Hits with MetaData Scors should have come back with the Query");
            Assert.Equal(1d, result8.HitsMetaDataHitScores["1151"]); // Because we gave it something to match against, it can score it
            Assert.True(result8.Attributes!.Any(), "Facets (Aggregation Points) of type Attributes should have come back with the Query");
            Assert.True(result8.BrandIDs!.Count == 1, "Facets (Aggregation Points) of type Brand IDs should have come back with the Query");
            Assert.True(result8.CategoriesTree!.Children!.Any(), "Facets (Aggregation Points) of type Categories should have come back with the Query");
            Assert.True(result8.FranchiseIDs!.Count == 1, "Facets (Aggregation Points) of type Franchise IDs should have come back with the Query");
            Assert.True(result8.ManufacturerIDs!.Count == 1, "Facets (Aggregation Points) of type Manufacturer IDs should have come back with the Query");
            Assert.True(result8.PricingRanges!.Count == 12, "Facets (Aggregation Points) of type Pricing Ranges should have come back with the Query");
            Assert.True(result8.RatingRanges!.Count == 5, "Facets (Aggregation Points) of type Rating Ranges should have come back with the Query");
            Assert.True(result8.StoreIDs!.Count == 1, "Facets (Aggregation Points) of type Store IDs should have come back with the Query");
            Assert.True(result8.VendorIDs!.Count == 4, "Facets (Aggregation Points) of type Vendor IDs should have come back with the Query");
            TestOutputHelper.WriteLine("...Filtered Results G given");
        }

        private async Task Verify_Purging_For_Products_CompletesSuccessfully(
            ISearchingProviderBase provider,
            string contextProfileName)
        {
            // Arrange
            TestOutputHelper.WriteLine("Purging...");
            // Act
            await provider.PurgeAsync(
                    contextProfileName,
                    ElasticSearchingProviderConfig.SearchingProductIndex)
                .ConfigureAwait(false);
            // Assert
            TestOutputHelper.WriteLine("...Purged");
        }
    }
}
