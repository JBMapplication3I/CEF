// <copyright file="DoMockingSetupForContextRunnerAdvertising.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner advertising class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerAdvertisingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Ads
            if (DoAll || DoAdvertising || DoAdTable)
            {
                var index = 0;
                RawAds = new()
                {
                    await CreateADummyAdAsync(id: ++index, key: "Key-" + index, name: "Ad " + index, desc: "desc", endDate: new DateTime(), expirationDate: new DateTime(), startDate: new DateTime(), weight: 0).ConfigureAwait(false),
                };
                await InitializeMockSetAdsAsync(mockContext, RawAds).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Ad Accounts
            if (DoAll || DoAdvertising || DoAdAccountTable)
            {
                var index = 0;
                RawAdAccounts = new()
                {
                    await CreateADummyAdAccountAsync(id: ++index, key: "Key-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetAdAccountsAsync(mockContext, RawAdAccounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Ad Brands
            if (DoAll || DoAdvertising || DoAdBrandTable)
            {
                var index = 0;
                RawAdBrands = new()
                {
                    await CreateADummyAdBrandAsync(id: ++index, key: "Key-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetAdBrandsAsync(mockContext, RawAdBrands).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Ad Franchises
            if (DoAll || DoAdvertising || DoAdFranchiseTable)
            {
                var index = 0;
                RawAdFranchises = new()
                {
                    await CreateADummyAdFranchiseAsync(id: ++index, key: "Key-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetAdFranchisesAsync(mockContext, RawAdFranchises).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Ad Images
            if (DoAll || DoAdvertising || DoAdImageTable)
            {
                var index = 0;
                RawAdImages = new()
                {
                    await CreateADummyAdImageAsync(id: ++index, key: "Image-" + index, name: "An Image " + index, desc: "desc", displayName: "An Image " + index, isPrimary: true, originalFileFormat: "jpg", originalFileName: "image.jpg", thumbnailFileFormat: "jpg", thumbnailFileName: "image.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetAdImagesAsync(mockContext, RawAdImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Ad Image Types
            if (DoAll || DoAdvertising || DoAdImageTypeTable)
            {
                var index = 0;
                RawAdImageTypes = new()
                {
                    await CreateADummyAdImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetAdImageTypesAsync(mockContext, RawAdImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Ad Statuses
            if (DoAll || DoAdvertising || DoAdStatusTable)
            {
                var index = 0;
                RawAdStatuses = new()
                {
                    await CreateADummyAdStatusAsync(id: ++index, key: "NORMAL", name: "Normal", desc: "desc", displayName: "Normal").ConfigureAwait(false),
                };
                await InitializeMockSetAdStatusesAsync(mockContext, RawAdStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Ad Stores
            if (DoAll || DoAdvertising || DoAdStoreTable)
            {
                var index = 0;
                RawAdStores = new()
                {
                    await CreateADummyAdStoreAsync(id: ++index, key: "Key-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetAdStoresAsync(mockContext, RawAdStores).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Ad Types
            if (DoAll || DoAdvertising || DoAdTypeTable)
            {
                var index = 0;
                RawAdTypes = new()
                {
                    await CreateADummyAdTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetAdTypesAsync(mockContext, RawAdTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Ad Zones
            if (DoAll || DoAdvertising || DoAdZoneTable)
            {
                var index = 0;
                RawAdZones = new()
                {
                    await CreateADummyAdZoneAsync(id: ++index, key: "Key-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetAdZonesAsync(mockContext, RawAdZones).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Ad Zone Accesses
            if (DoAll || DoAdvertising || DoAdZoneAccessTable)
            {
                RawAdZoneAccesses = new()
                {
                    await CreateADummyAdZoneAccessAsync(id: 1, key: "AdZoneAccess", name: "Ad Zone Access", desc: "desc", clickLimit: 0, endDate: new DateTime(), impressionLimit: 0, startDate: new DateTime(), uniqueAdLimit: 0).ConfigureAwait(false),
                    await CreateADummyAdZoneAccessAsync(id: 2, key: "AdZoneAccess-UniqueLevelLimits", name: "Ad Zone Access with Unique Level Limits", desc: "desc", clickLimit: 100, impressionLimit: 1000, uniqueAdLimit: 1, startDate: CreatedDate, endDate: CreatedDate.AddYears(1)).ConfigureAwait(false),
                };
                await InitializeMockSetAdZoneAccessesAsync(mockContext, RawAdZoneAccesses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Zones
            if (DoAll || DoAdvertising || DoZoneTable)
            {
                var index = 0;
                RawZones = new()
                {
                    await CreateADummyZoneAsync(id: ++index, key: "Key-" + index, name: "Zone " + index, desc: "desc", height: 0, width: 0).ConfigureAwait(false),
                };
                await InitializeMockSetZonesAsync(mockContext, RawZones).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Zone Statuses
            if (DoAll || DoAdvertising || DoZoneStatusTable)
            {
                var index = 0;
                RawZoneStatuses = new()
                {
                    await CreateADummyZoneStatusAsync(id: ++index, key: "NORMAL", name: "Normal", desc: "desc", displayName: "Normal").ConfigureAwait(false),
                };
                await InitializeMockSetZoneStatusesAsync(mockContext, RawZoneStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Zone Types
            if (DoAll || DoAdvertising || DoZoneTypeTable)
            {
                var index = 0;
                RawZoneTypes = new()
                {
                    await CreateADummyZoneTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetZoneTypesAsync(mockContext, RawZoneTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
