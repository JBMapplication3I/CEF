// <copyright file="ElasticSearchingProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Testing
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Providers.Searching;
    using JSConfigs;
    using Mapper;
#if NET5_0_OR_GREATER
    using Lamar;
#else
    using StructureMap;
    using StructureMap.Pipeline;
#endif
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Providers.Searching.ElasticSearch")]
    public partial class ElasticSearchingProviderTests : XUnitLogHelper
    {
        public ElasticSearchingProviderTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task Verify_GetAllSuggestionsFromProvider_Works_Async()
        {
            // Arrange
            TestOutputHelper.WriteLine("Loading...");
            CEFConfigDictionary.Load();
            BaseModelMapper.Initialize();
            const string contextProfileName = "ElasticSearchingProviderTests|Verify_GetAllSuggestionsFromProvider_Works_Async";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAddressTable = true,
                    DoAttributeTypeTable = true,
                    DoAuctionCategoryTable = true,
                    DoAuctionTable = true,
                    DoAuctionTypeTable = true,
                    DoBadgeTable = true,
                    DoBrandAuctionTable = true,
                    DoBrandCategoryTable = true,
                    DoBrandFranchiseTable = true,
                    DoBrandManufacturerTable = true,
                    DoBrandProductTable = true,
                    DoBrandStoreTable = true,
                    DoBrandTable = true,
                    DoBrandVendorTable = true,
                    DoCategoryTable = true,
                    DoCategoryTypeTable = true,
                    DoContactTable = true,
                    DoCountryTable = true,
                    DoDistrictTable = true,
                    DoEventStatusTable = true,
                    DoFranchiseAuctionTable = true,
                    DoFranchiseCategoryTable = true,
                    DoFranchiseCountryTable = true,
                    DoFranchiseDistrictTable = true,
                    DoFranchiseManufacturerTable = true,
                    DoFranchiseProductTable = true,
                    DoFranchiseRegionTable = true,
                    DoFranchiseStoreTable = true,
                    DoFranchiseTable = true,
                    DoFranchiseTypeTable = true,
                    DoFranchiseVendorTable = true,
                    DoGeneralAttributeTable = true,
                    DoLotCategoryTable = true,
                    DoLotTable = true,
                    DoLotTypeTable = true,
                    DoManufacturerProductTable = true,
                    DoManufacturerTable = true,
                    DoManufacturerTypeTable = true,
                    DoProductAssociationTable = true,
                    DoProductAssociationTypeTable = true,
                    DoProductCategoryTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRegionTable = true,
                    DoSettingTable = true,
                    DoSettingTypeTable = true,
                    DoStoreAuctionTable = true,
                    DoStoreBadgeTable = true,
                    DoStoreCategoryTable = true,
                    DoStoreContactTable = true,
                    DoStoreCountryTable = true,
                    DoStoreDistrictTable = true,
                    DoStoreManufacturerTable = true,
                    DoStoreProductTable = true,
                    DoStoreRegionTable = true,
                    DoStoreTable = true,
                    DoStoreTypeTable = true,
                    DoStoreVendorTable = true,
                    DoVendorManufacturerTable = true,
                    DoVendorProductTable = true,
                    DoVendorTable = true,
                    DoVendorTypeTable = true,
                };
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var provider = new ElasticSearchingProvider();
                await Task.WhenAll(
                        Verify_Indexing_For_Auctions_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Categories_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Franchises_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Lots_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Manufacturers_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Products_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Stores_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Vendors_CompletesSuccessfully(provider, contextProfileName))
                    .ConfigureAwait(false);
                ProductCatalogSearchForm form = new()
                {
                    Query = "a",
                    Page = 1,
                    PageSize = 100,
                    Role = "Anonymous",
                };
                // Act
                var results = await provider.GetAllSuggestionsFromProviderAsync(form, contextProfileName).ConfigureAwait(false);
                Assert.NotEmpty(results);
                // Teardown
                await Task.WhenAll(
                        Verify_Purging_For_Auctions_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Categories_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Franchises_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Lots_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Manufacturers_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Products_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Stores_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Vendors_CompletesSuccessfully(provider, contextProfileName))
                    .ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetAllSearchResultsFromProvider_Works_Async()
        {
            // Arrange
            TestOutputHelper.WriteLine("Loading...");
            CEFConfigDictionary.Load();
            BaseModelMapper.Initialize();
            const string contextProfileName = "ElasticSearchingProviderTests|Verify_GetAllSearchResultsFromProvider_Works_Async";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAddressTable = true,
                    DoAttributeTypeTable = true,
                    DoAuctionCategoryTable = true,
                    DoAuctionTable = true,
                    DoAuctionTypeTable = true,
                    DoBadgeTable = true,
                    DoBrandAuctionTable = true,
                    DoBrandCategoryTable = true,
                    DoBrandFranchiseTable = true,
                    DoBrandManufacturerTable = true,
                    DoBrandProductTable = true,
                    DoBrandStoreTable = true,
                    DoBrandTable = true,
                    DoBrandVendorTable = true,
                    DoCategoryTable = true,
                    DoCategoryTypeTable = true,
                    DoContactTable = true,
                    DoCountryTable = true,
                    DoDistrictTable = true,
                    DoEventTypeTable = true,
                    DoEventStatusTable = true,
                    DoEventTable = true,
                    DoFranchiseAuctionTable = true,
                    DoFranchiseCategoryTable = true,
                    DoFranchiseCountryTable = true,
                    DoFranchiseDistrictTable = true,
                    DoFranchiseManufacturerTable = true,
                    DoFranchiseProductTable = true,
                    DoFranchiseRegionTable = true,
                    DoFranchiseStoreTable = true,
                    DoFranchiseTable = true,
                    DoFranchiseTypeTable = true,
                    DoFranchiseVendorTable = true,
                    DoGeneralAttributeTable = true,
                    DoLotCategoryTable = true,
                    DoLotTable = true,
                    DoLotTypeTable = true,
                    DoManufacturerProductTable = true,
                    DoManufacturerTable = true,
                    DoManufacturerTypeTable = true,
                    DoProductAssociationTable = true,
                    DoProductAssociationTypeTable = true,
                    DoProductCategoryTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRegionTable = true,
                    DoSettingTable = true,
                    DoSettingTypeTable = true,
                    DoStoreAuctionTable = true,
                    DoStoreBadgeTable = true,
                    DoStoreCategoryTable = true,
                    DoStoreContactTable = true,
                    DoStoreCountryTable = true,
                    DoStoreDistrictTable = true,
                    DoStoreManufacturerTable = true,
                    DoStoreProductTable = true,
                    DoStoreRegionTable = true,
                    DoStoreTable = true,
                    DoStoreTypeTable = true,
                    DoStoreVendorTable = true,
                    DoVendorManufacturerTable = true,
                    DoVendorProductTable = true,
                    DoVendorTable = true,
                    DoVendorTypeTable = true,
                };
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var provider = new ElasticSearchingProvider();
                await Task.WhenAll(
                        Verify_Indexing_For_Auctions_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Categories_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Franchises_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Lots_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Manufacturers_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Products_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Stores_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Indexing_For_Vendors_CompletesSuccessfully(provider, contextProfileName))
                    .ConfigureAwait(false);
                ProductCatalogSearchForm form = new()
                {
                    Page = 1,
                    PageSize = 100,
                    Role = "Anonymous",
                };
                // Act
                var resultDateTime = await provider.GetAllSearchResultsAsViewModelLastModifiedAsync(
                        form,
                        contextProfileName)
                    .ConfigureAwait(false);
                Assert.Equal(new DateTime(2023, 1, 1), resultDateTime);
                var results = await provider.GetAllSearchResultsAsViewModelsAsync(
                        form,
                        contextProfileName)
                    .ConfigureAwait(false);
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                // Teardown
                await Task.WhenAll(
                        Verify_Purging_For_Auctions_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Categories_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Franchises_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Lots_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Manufacturers_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Products_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Stores_CompletesSuccessfully(provider, contextProfileName),
                        Verify_Purging_For_Vendors_CompletesSuccessfully(provider, contextProfileName))
                    .ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [DebuggerStepThrough]
        private async Task DoSetupAsync(
            IContainer childContainer,
            MockingSetup mockingSetup,
            string contextProfileName)
        {
            await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
            RegistryLoader.RootContainer.Configure(
                x => x.For<ILogger>().UseInstance(
                    new ObjectInstance(new Logger
                    {
                        ExtraLogger = s =>
                        {
                            try
                            {
                                TestOutputHelper.WriteLine(s);
                            }
                            catch
                            {
                                // Do nothing
                            }
                        },
                    })));
            childContainer.Configure(x =>
            {
                x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
            });
            RegistryLoader.OverrideContainer(childContainer, contextProfileName);
        }
    }
}
