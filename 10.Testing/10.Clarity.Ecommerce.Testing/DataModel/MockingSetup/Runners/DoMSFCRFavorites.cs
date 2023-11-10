// <copyright file="DoMockingSetupForContextRunnerFavoritesAsync.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner favorites class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerFavoritesAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Favorite Categories
            if (DoAll || DoFavorites || DoFavoriteCategoryTable)
            {
                RawFavoriteCategories = new()
                {
                    await CreateADummyFavoriteCategoryAsync(id: 1, key: "FAVORITE-CATEGORY-1").ConfigureAwait(false),
                };
                await InitializeMockSetFavoriteCategoriesAsync(mockContext, RawFavoriteCategories).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Favorite Manufacturers
            if (DoAll || DoFavorites || DoFavoriteManufacturerTable)
            {
                RawFavoriteManufacturers = new()
                {
                    await CreateADummyFavoriteManufacturerAsync(id: 1, key: "FAVORITE-MANUFACTURER-1").ConfigureAwait(false),
                };
                await InitializeMockSetFavoriteManufacturersAsync(mockContext, RawFavoriteManufacturers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Favorite Ship Carriers
            if (DoAll || DoFavorites || DoFavoriteShipCarrierTable)
            {
                RawFavoriteShipCarriers = new()
                {
                    await CreateADummyFavoriteShipCarrierAsync(id: 1, key: "FAVORITE-SHIP-CARRIER-1").ConfigureAwait(false),
                };
                await InitializeMockSetFavoriteShipCarriersAsync(mockContext, RawFavoriteShipCarriers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Favorite Stores
            if (DoAll || DoFavorites || DoFavoriteStoreTable)
            {
                RawFavoriteStores = new()
                {
                    await CreateADummyFavoriteStoreAsync(id: 1, key: "FAVORITE-STORE-1").ConfigureAwait(false),
                };
                await InitializeMockSetFavoriteStoresAsync(mockContext, RawFavoriteStores).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Favorite Vendors
            if (DoAll || DoFavorites || DoFavoriteVendorTable)
            {
                RawFavoriteVendors = new()
                {
                    await CreateADummyFavoriteVendorAsync(id: 1, key: "FAVORITE-VENDOR-1").ConfigureAwait(false),
                };
                await InitializeMockSetFavoriteVendorsAsync(mockContext, RawFavoriteVendors).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
