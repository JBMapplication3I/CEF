// <copyright file="ElasticSearchingProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider tests class.</summary>
#nullable enable
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Testing
{
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
        public Task LiveIndexRunnerForManufacturers()
        {
            CEFConfigDictionary.Load();
            BaseModelMapper.Initialize();
            return new ElasticSearchingProvider().IndexAsync(
                contextProfileName: null,
                index: ElasticSearchingProviderConfig.SearchingManufacturerIndex,
                default);
        }

        [Fact]
        public async Task Verify_IndexingThenSuggestionsThenQueryingThenPurging_ForManufacturers_CompletesSuccessfully()
        {
            // Arrange
            TestOutputHelper.WriteLine("Loading...");
            CEFConfigDictionary.Load();
            BaseModelMapper.Initialize();
            const string contextProfileName = "ElasticSearchingProviderTests|Verify_IndexingThenSuggestionsThenQueryingThenPurging_ForManufacturers_CompletesSuccessfully";
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
                    // Main Entity
                    DoManufacturerTable = true,
                    // Direct Links
                    DoBrandManufacturerTable = true,
                    DoFranchiseManufacturerTable = true,
                    DoStoreManufacturerTable = true,
                    DoManufacturerProductTable = true,
                    DoManufacturerTypeTable = true,
                    DoVendorManufacturerTable = true,
                    DoReviewTable = true,
                    // Links of Direct Links
                    DoAuctionTable = true,
                    DoBadgeTable = true,
                    DoBrandTable = true,
                    DoCategoryTable = true,
                    DoContactTable = true,
                    DoAddressTable = true,
                    DoCountryTable = true,
                    DoDistrictTable = true,
                    DoFranchiseTable = true,
                    DoProductTable = true,
                    DoRegionTable = true,
                    DoStoreTable = true,
                    DoVendorTable = true,
                };
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var provider = new ElasticSearchingProvider();
                TestOutputHelper.WriteLine("...Loaded");
                // Act/Assert
                await Verify_Indexing_For_Manufacturers_CompletesSuccessfully(provider, contextProfileName).ConfigureAwait(false);
                await Verify_Suggestions_For_Manufacturers_CompletesSuccessfully(provider, contextProfileName).ConfigureAwait(false);
                await Verify_Querying_For_Manufacturers_CompletesSuccessfully(provider, contextProfileName).ConfigureAwait(false);
                await Verify_Purging_For_Manufacturers_CompletesSuccessfully(provider, contextProfileName).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
            TestOutputHelper.WriteLine("...Exiting");
        }

        private async Task Verify_Indexing_For_Manufacturers_CompletesSuccessfully(
            ISearchingProviderBase provider,
            string contextProfileName)
        {
            // Arrange
            TestOutputHelper.WriteLine("Indexing");
            // Act
            await provider.IndexAsync(
                    contextProfileName,
                    ElasticSearchingProviderConfig.SearchingManufacturerIndex,
                    default)
                .ConfigureAwait(false);
            // Assert
            TestOutputHelper.WriteLine("...Indexed");
        }

        private async Task Verify_Suggestions_For_Manufacturers_CompletesSuccessfully(
            ISearchingProviderBase provider,
            string contextProfileName)
        {
            // Arrange
            TestOutputHelper.WriteLine("Asking for suggestions");
            ManufacturerCatalogSearchForm form = new()
            {
                // PageSize = 4,
                // Page = 1,
                // PageFormat = "grid",
                // Sort = SearchSort.Relevance,
                Query = "Manufacturer",
                // Category = "Home", // Single-Category
                // CategoriesAny = new [] { "Home"/*, "Violins"*/ }, // Multiple-Category (Any single match)
                // CategoriesAll = new [] { "Home", "Lighting" }, // Multiple-Category (All must match)
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
            var result = await provider.SuggestionsAsync<ManufacturerCatalogSearchForm, ManufacturerSuggestResult>(
                    form,
                    ElasticSearchingProviderConfig.SearchingManufacturerIndex,
                    contextProfileName)
                .ConfigureAwait(false);
            // Assert
            Assert.NotNull(result);
            TestOutputHelper.WriteLine(JsonConvert.SerializeObject(result));
            Assert.True(result.Any(), "No results found");
            TestOutputHelper.WriteLine("...Suggestions given");
        }

        private async Task Verify_Querying_For_Manufacturers_CompletesSuccessfully(
            ISearchingProviderBase provider,
            string contextProfileName)
        {
            // Arrange
            ManufacturerCatalogSearchForm form = new()
            {
                PageSize = 1,
                PageSetSize = 5,
                Page = 1,
                PageFormat = "grid",
                Sort = Enums.SearchSort.Relevance,
                // Query = "3M",
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
            TestOutputHelper.WriteLine("Asking for results");
            // Act
            var result = await provider.QueryAsync<ManufacturerSearchViewModel, ManufacturerCatalogSearchForm, ManufacturerIndexableModel>(
                    form,
                    ElasticSearchingProviderConfig.SearchingManufacturerIndex,
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
            TestOutputHelper.WriteLine($"{result.CategoriesTree?.Children?.LongCount() ?? 0:N0} Children");
            TestOutputHelper.WriteLine($"{result.FranchiseIDs!.LongCount():N0} Franchise IDs");
            TestOutputHelper.WriteLine($"{result.ManufacturerIDs!.LongCount():N0} Manufacturer IDs");
            TestOutputHelper.WriteLine($"{result.PricingRanges!.LongCount():N0} Price Ranges");
            TestOutputHelper.WriteLine($"{result.ProductIDs!.LongCount():N0} Product IDs");
            TestOutputHelper.WriteLine($"{result.RatingRanges!.LongCount():N0} Rating Ranges");
            TestOutputHelper.WriteLine($"{result.StoreIDs!.LongCount():N0} Store IDs");
            TestOutputHelper.WriteLine($"{result.VendorIDs!.LongCount():N0} Vendor IDs");
            Assert.True(result.ElapsedMilliseconds < 1_000, "Took too long");
            Assert.True(result.Total > 0, "No results found");
            Assert.True(result.TotalPages > 1, "Results should have been paged");
            Assert.NotEmpty(result.Documents!); // , "Results themselves should have come back with the Query");
            Assert.True(result.Attributes!.LongCount() > 0, "Facets (Aggregation Points) of type Attributes should have come back with the Query");
            Assert.True(result.BrandIDs!.Count == 2, "Facets (Aggregation Points) of type Brand IDs should have come back with the Query");
            Assert.True(result.CategoriesTree?.Children == null, "Facets (Aggregation Points) of type Categories should have come back with the Query");
            Assert.True(result.FranchiseIDs!.Count == 2, "Facets (Aggregation Points) of type Franchise IDs should have come back with the Query");
            Assert.True(result.ManufacturerIDs!.Count == 0, "Facets (Aggregation Points) of type Manufacturer IDs should have come back with the Query");
            Assert.True(result.PricingRanges!.Count == 0, "Facets (Aggregation Points) of type Pricing Ranges should have come back with the Query");
            Assert.True(result.ProductIDs!.Count == 1, "Facets (Aggregation Points) of type Product IDs should have come back with the Query");
            Assert.True(result.RatingRanges!.Count == 5, "Facets (Aggregation Points) of type Rating Ranges should have come back with the Query");
            Assert.True(result.StoreIDs!.Count == 2, "Facets (Aggregation Points) of type Store IDs should have come back with the Query");
            Assert.True(result.VendorIDs!.Count == 1, "Facets (Aggregation Points) of type Vendor IDs should have come back with the Query");
            TestOutputHelper.WriteLine("...Results given");
        }

        private async Task Verify_Purging_For_Manufacturers_CompletesSuccessfully(
            ISearchingProviderBase provider,
            string contextProfileName)
        {
            // Arrange
            TestOutputHelper.WriteLine("Purging...");
            // Act
            await provider.PurgeAsync(
                    contextProfileName,
                    ElasticSearchingProviderConfig.SearchingManufacturerIndex)
                .ConfigureAwait(false);
            // Assert
            TestOutputHelper.WriteLine("...Purged");
        }
    }
}
