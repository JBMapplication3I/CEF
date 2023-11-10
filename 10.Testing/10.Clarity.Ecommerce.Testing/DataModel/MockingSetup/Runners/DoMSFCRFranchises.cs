// <copyright file="DoMSFCRFranchises.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner franchises class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerFranchisesAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Franchises
            if (DoAll || DoFranchises || DoFranchiseTable)
            {
                var index = 0;
                RawFranchises = new()
                {
                    await CreateADummyFranchiseAsync(id: ++index, key: "franchise-" + index, name: "Franchise One", desc: "description",
                            jsonAttributes: new SerializableAttributesDictionary
                            {
                                ["Color"] = new() { Key = "Color", Value = "Red" },
                            }.SerializeAttributesDictionary()).ConfigureAwait(false),
                    await CreateADummyFranchiseAsync(id: ++index, key: "franchise-" + index, name: "Franchise Two", desc: "description").ConfigureAwait(false),
                    await CreateADummyFranchiseAsync(id: ++index, key: "franchise-" + index, name: "Franchise Three", desc: "description").ConfigureAwait(false),
                    await CreateADummyFranchiseAsync(id: ++index, key: "franchise-" + index, name: "Franchise Four", desc: "description").ConfigureAwait(false),
                    await CreateADummyFranchiseAsync(id: ++index, key: "franchise-" + index, name: "Franchise Five", desc: "description").ConfigureAwait(false),
                };
                await InitializeMockSetFranchisesAsync(mockContext, RawFranchises).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Accounts
            if (DoAll || DoFranchises || DoFranchiseAccountTable)
            {
                var index = 0;
                RawFranchiseAccounts = new()
                {
                    await CreateADummyFranchiseAccountAsync(id: ++index, key: "franchise-account-" + index).ConfigureAwait(false),
                    await CreateADummyFranchiseAccountAsync(id: ++index, key: "franchise-account-" + index, slaveID: 2).ConfigureAwait(false),
                    await CreateADummyFranchiseAccountAsync(id: ++index, key: "franchise-account-" + index, slaveID: 3).ConfigureAwait(false),
                    await CreateADummyFranchiseAccountAsync(id: ++index, key: "franchise-account-" + index, slaveID: 4).ConfigureAwait(false),
                    await CreateADummyFranchiseAccountAsync(id: ++index, key: "franchise-account-" + index, slaveID: 5).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseAccountsAsync(mockContext, RawFranchiseAccounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Categories
            if (DoAll || DoFranchises || DoFranchiseCategoryTable)
            {
                var index = 0;
                RawFranchiseCategories = new()
                {
                    await CreateADummyFranchiseCategoryAsync(id: ++index, key: "franchise-category-" + index, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseCategoriesAsync(mockContext, RawFranchiseCategories).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Countries
            if (DoAll || DoFranchises || DoFranchiseCountryTable)
            {
                var index = 0;
                RawFranchiseCountries = new()
                {
                    await CreateADummyFranchiseCountryAsync(id: ++index, key: "franchise-country-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseCountriesAsync(mockContext, RawFranchiseCountries).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Currencies
            if (DoAll || DoFranchises || DoFranchiseCurrencyTable)
            {
                var index = 0;
                RawFranchiseCurrencies = new()
                {
                    await CreateADummyFranchiseCurrencyAsync(id: ++index, key: "franchise-currency-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseCurrenciesAsync(mockContext, RawFranchiseCurrencies).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Districts
            if (DoAll || DoFranchises || DoFranchiseDistrictTable)
            {
                var index = 0;
                RawFranchiseDistricts = new()
                {
                    await CreateADummyFranchiseDistrictAsync(id: ++index, key: "franchise-district-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseDistrictsAsync(mockContext, RawFranchiseDistricts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Images
            if (DoAll || DoFranchises || DoFranchiseImageTable)
            {
                var index = 0;
                RawFranchiseImages = new()
                {
                    await CreateADummyFranchiseImageAsync(id: ++index, key: "Image-" + index, name: "An Image " + index, desc: "desc", displayName: "An Image " + index, isPrimary: true, originalFileFormat: "jpg", originalFileName: "image.jpg", thumbnailFileFormat: "jpg", thumbnailFileName: "image.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseImagesAsync(mockContext, RawFranchiseImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Image Types
            if (DoAll || DoFranchises || DoFranchiseImageTypeTable)
            {
                var index = 0;
                RawFranchiseImageTypes = new()
                {
                    await CreateADummyFranchiseImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseImageTypesAsync(mockContext, RawFranchiseImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Inventory Locations
            if (DoAll || DoFranchises || DoFranchiseInventoryLocationTable)
            {
                var index = 0;
                RawFranchiseInventoryLocations = new()
                {
                    await CreateADummyFranchiseInventoryLocationAsync(id: ++index, key: "Franchise-InventoryLocation-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseInventoryLocationsAsync(mockContext, RawFranchiseInventoryLocations).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Inventory Location Types
            if (DoAll || DoFranchises || DoFranchiseInventoryLocationTypeTable)
            {
                var index = 0;
                RawFranchiseInventoryLocationTypes = new()
                {
                    await CreateADummyFranchiseInventoryLocationTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseInventoryLocationTypesAsync(mockContext, RawFranchiseInventoryLocationTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Languages
            if (DoAll || DoFranchises || DoFranchiseLanguageTable)
            {
                var index = 0;
                RawFranchiseLanguages = new()
                {
                    await CreateADummyFranchiseLanguageAsync(id: ++index, key: "franchise-language-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseLanguagesAsync(mockContext, RawFranchiseLanguages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Manufacturers
            if (DoAll || DoFranchises || DoFranchiseManufacturerTable)
            {
                var index = 0;
                RawFranchiseManufacturers = new()
                {
                    await CreateADummyFranchiseManufacturerAsync(id: ++index, key: "Franchise-Manufacturer-" + index, masterID: 1, slaveID: 1, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyFranchiseManufacturerAsync(id: ++index, key: "Franchise-Manufacturer-" + index, masterID: 2, slaveID: 1, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyFranchiseManufacturerAsync(id: ++index, key: "Franchise-Manufacturer-" + index, masterID: 1, slaveID: 2, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyFranchiseManufacturerAsync(id: ++index, key: "Franchise-Manufacturer-" + index, masterID: 2, slaveID: 2, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseManufacturersAsync(mockContext, RawFranchiseManufacturers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Products
            if (DoAll || DoFranchises || DoFranchiseProductTable)
            {
                var index = 0;
                RawFranchiseProducts = new()
                {
                    await CreateADummyFranchiseProductAsync(id: ++index, key: "franchise-product-" + index, slaveID: 1152, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyFranchiseProductAsync(id: ++index, key: "franchise-product-" + index, slaveID: 1151, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseProductsAsync(mockContext, RawFranchiseProducts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Regions
            if (DoAll || DoFranchises || DoFranchiseRegionTable)
            {
                var index = 0;
                RawFranchiseRegions = new()
                {
                    await CreateADummyFranchiseRegionAsync(id: ++index, key: "franchise-region-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseRegionsAsync(mockContext, RawFranchiseRegions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise SiteDomains
            if (DoAll || DoFranchises || DoFranchiseSiteDomainTable)
            {
                var index = 0;
                RawFranchiseSiteDomains = new()
                {
                    await CreateADummyFranchiseSiteDomainAsync(id: ++index, key: "franchise-sitedomain-" + index).ConfigureAwait(false),
                    await CreateADummyFranchiseSiteDomainAsync(id: ++index, key: "franchise-sitedomain-" + index, slaveID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseSiteDomainsAsync(mockContext, RawFranchiseSiteDomains).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Stores
            if (DoAll || DoFranchises || DoFranchiseStoreTable)
            {
                var index = 0;
                RawFranchiseStores = new()
                {
                    await CreateADummyFranchiseStoreAsync(id: ++index, key: "franchise-store-" + index, slaveID: 1, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyFranchiseStoreAsync(id: ++index, key: "franchise-store-" + index, slaveID: 2, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyFranchiseStoreAsync(id: ++index, key: "franchise-store-" + index, slaveID: 3, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyFranchiseStoreAsync(id: ++index, key: "franchise-store-" + index, slaveID: 4, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyFranchiseStoreAsync(id: ++index, key: "franchise-store-" + index, slaveID: 5, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseStoresAsync(mockContext, RawFranchiseStores).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Types
            if (DoAll || DoFranchises || DoFranchiseTypeTable)
            {
                var index = 0;
                RawFranchiseTypes = new()
                {
                    await CreateADummyFranchiseTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseTypesAsync(mockContext, RawFranchiseTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Users
            if (DoAll || DoFranchises || DoFranchiseUserTable)
            {
                var index = 0;
                RawFranchiseUsers = new()
                {
                    await CreateADummyFranchiseUserAsync(id: ++index, key: "store-user-" + index).ConfigureAwait(false),
                    await CreateADummyFranchiseUserAsync(id: ++index, key: "store-user-" + index, slaveID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseUsersAsync(mockContext, RawFranchiseUsers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Vendors
            if (DoAll || DoFranchises || DoFranchiseVendorTable)
            {
                var index = 0;
                RawFranchiseVendors = new()
                {
                    await CreateADummyFranchiseVendorAsync(id: ++index, key: "Franchise-Vendor-" + index, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseVendorsAsync(mockContext, RawFranchiseVendors).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
