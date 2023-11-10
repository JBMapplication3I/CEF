// <copyright file="DoMSFCRBrands.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner brands class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerBrandsAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Brands
            if (DoAll || DoBrands || DoBrandTable)
            {
                var index = 0;
                RawBrands = new()
                {
                    await CreateADummyBrandAsync(id: ++index, key: "brand-" + index, name: "Brand One", desc: "description").ConfigureAwait(false),
                    await CreateADummyBrandAsync(id: ++index, key: "brand-" + index, name: "Brand Two", desc: "description").ConfigureAwait(false),
                    await CreateADummyBrandAsync(id: ++index, key: "brand-" + index, name: "Brand Three", desc: "description").ConfigureAwait(false),
                    await CreateADummyBrandAsync(id: ++index, key: "brand-" + index, name: "Brand Four", desc: "description").ConfigureAwait(false),
                    await CreateADummyBrandAsync(id: ++index, key: "brand-" + index, name: "Brand Five", desc: "description").ConfigureAwait(false),
                };
                await InitializeMockSetBrandsAsync(mockContext, RawBrands).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Accounts
            if (DoAll || DoBrands || DoBrandAccountTable)
            {
                var index = 0;
                RawBrandAccounts = new()
                {
                    await CreateADummyBrandAccountAsync(id: ++index, key: "brand-account-" + index).ConfigureAwait(false),
                    await CreateADummyBrandAccountAsync(id: ++index, key: "brand-account-" + index, slaveID: 2).ConfigureAwait(false),
                    await CreateADummyBrandAccountAsync(id: ++index, key: "brand-account-" + index, slaveID: 3).ConfigureAwait(false),
                    await CreateADummyBrandAccountAsync(id: ++index, key: "brand-account-" + index, slaveID: 4).ConfigureAwait(false),
                    await CreateADummyBrandAccountAsync(id: ++index, key: "brand-account-" + index, slaveID: 5).ConfigureAwait(false),
                };
                await InitializeMockSetBrandAccountsAsync(mockContext, RawBrandAccounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Categories
            if (DoAll || DoBrands || DoBrandCategoryTable)
            {
                var index = 0;
                RawBrandCategories = new()
                {
                    await CreateADummyBrandCategoryAsync(id: ++index, key: "brand-category-" + index, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetBrandCategoriesAsync(mockContext, RawBrandCategories).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Currencies
            if (DoAll || DoBrands || DoBrandCurrencyTable)
            {
                var index = 0;
                RawBrandCurrencies = new()
                {
                    await CreateADummyBrandCurrencyAsync(id: ++index, key: "brand-currency-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetBrandCurrenciesAsync(mockContext, RawBrandCurrencies).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Franchises
            if (DoAll || DoBrands || DoBrandFranchiseTable)
            {
                var index = 0;
                RawBrandFranchises = new()
                {
                    await CreateADummyBrandFranchiseAsync(id: ++index, key: "brand-franchise-" + index, slaveID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetBrandFranchisesAsync(mockContext, RawBrandFranchises).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Images
            if (DoAll || DoBrands || DoBrandImageTable)
            {
                var index = 0;
                RawBrandImages = new()
                {
                    await CreateADummyBrandImageAsync(id: ++index, key: "Image-" + index, name: "An Image " + index, desc: "desc", displayName: "An Image " + index, isPrimary: true, originalFileFormat: "jpg", originalFileName: "image.jpg", thumbnailFileFormat: "jpg", thumbnailFileName: "image.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetBrandImagesAsync(mockContext, RawBrandImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Image Types
            if (DoAll || DoBrands || DoBrandImageTypeTable)
            {
                var index = 0;
                RawBrandImageTypes = new()
                {
                    await CreateADummyBrandImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetBrandImageTypesAsync(mockContext, RawBrandImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Inventory Locations
            if (DoAll || DoBrands || DoBrandInventoryLocationTable)
            {
                var index = 0;
                RawBrandInventoryLocations = new()
                {
                    await CreateADummyBrandInventoryLocationAsync(id: ++index, key: "Brand-InventoryLocation-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetBrandInventoryLocationsAsync(mockContext, RawBrandInventoryLocations).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Inventory Location Types
            if (DoAll || DoBrands || DoBrandInventoryLocationTypeTable)
            {
                var index = 0;
                RawBrandInventoryLocationTypes = new()
                {
                    await CreateADummyBrandInventoryLocationTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetBrandInventoryLocationTypesAsync(mockContext, RawBrandInventoryLocationTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Languages
            if (DoAll || DoBrands || DoBrandLanguageTable)
            {
                var index = 0;
                RawBrandLanguages = new()
                {
                    await CreateADummyBrandLanguageAsync(id: ++index, key: "brand-language-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetBrandLanguagesAsync(mockContext, RawBrandLanguages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Manufacturers
            if (DoAll || DoBrands || DoBrandManufacturerTable)
            {
                var index = 0;
                RawBrandManufacturers = new()
                {
                    await CreateADummyBrandManufacturerAsync(id: ++index, key: "Brand-Manufacturer-" + index, masterID: 1, slaveID: 1, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyBrandManufacturerAsync(id: ++index, key: "Brand-Manufacturer-" + index, masterID: 2, slaveID: 1, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyBrandManufacturerAsync(id: ++index, key: "Brand-Manufacturer-" + index, masterID: 1, slaveID: 2, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyBrandManufacturerAsync(id: ++index, key: "Brand-Manufacturer-" + index, masterID: 2, slaveID: 2, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetBrandManufacturersAsync(mockContext, RawBrandManufacturers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Products
            if (DoAll || DoBrands || DoBrandProductTable)
            {
                var index = 0;
                RawBrandProducts = new()
                {
                    await CreateADummyBrandProductAsync(id: ++index, key: "brand-product-" + index, slaveID: 1152, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyBrandProductAsync(id: ++index, key: "brand-product-" + index, slaveID: 1151, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetBrandProductsAsync(mockContext, RawBrandProducts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand SiteDomains
            if (DoAll || DoBrands || DoBrandSiteDomainTable)
            {
                var index = 0;
                RawBrandSiteDomains = new()
                {
                    await CreateADummyBrandSiteDomainAsync(id: ++index, key: "brand-sitedomain-" + index).ConfigureAwait(false),
                    await CreateADummyBrandSiteDomainAsync(id: ++index, key: "brand-sitedomain-" + index, slaveID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetBrandSiteDomainsAsync(mockContext, RawBrandSiteDomains).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Stores
            if (DoAll || DoBrands || DoBrandStoreTable)
            {
                var index = 0;
                RawBrandStores = new()
                {
                    await CreateADummyBrandStoreAsync(id: ++index, key: "brand-store-" + index, isVisibleIn: true, masterID: 1, slaveID: 1).ConfigureAwait(false),
                    await CreateADummyBrandStoreAsync(id: ++index, key: "brand-store-" + index, isVisibleIn: true, masterID: 1, slaveID: 2).ConfigureAwait(false),
                    await CreateADummyBrandStoreAsync(id: ++index, key: "brand-store-" + index, isVisibleIn: true, masterID: 1, slaveID: 3).ConfigureAwait(false),
                    await CreateADummyBrandStoreAsync(id: ++index, key: "brand-store-" + index, isVisibleIn: true, masterID: 1, slaveID: 4).ConfigureAwait(false),
                    await CreateADummyBrandStoreAsync(id: ++index, key: "brand-store-" + index, isVisibleIn: true, masterID: 1, slaveID: 5).ConfigureAwait(false),
                };
                await InitializeMockSetBrandStoresAsync(mockContext, RawBrandStores).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Users
            if (DoAll || DoBrands || DoBrandUserTable)
            {
                var index = 0;
                RawBrandUsers = new()
                {
                    await CreateADummyBrandUserAsync(id: ++index, key: "store-user-" + index).ConfigureAwait(false),
                    await CreateADummyBrandUserAsync(id: ++index, key: "store-user-" + index, slaveID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetBrandUsersAsync(mockContext, RawBrandUsers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Vendors
            if (DoAll || DoBrands || DoBrandVendorTable)
            {
                var index = 0;
                RawBrandVendors = new()
                {
                    await CreateADummyBrandVendorAsync(id: ++index, key: "Brand-Vendor-" + index, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetBrandVendorsAsync(mockContext, RawBrandVendors).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
