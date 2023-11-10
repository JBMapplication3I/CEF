// <copyright file="DoMockingSetupForContextRunnerAuctions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner auctions class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerAuctionsAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Auction Types
            if (DoAll || DoAuctions || DoAuctionTypeTable)
            {
                var index = 0;
                RawAuctionTypes = new()
                {
                    await CreateADummyAuctionTypeAsync(id: ++index, key: "In Person - Called", name: "In Person - Called", desc: "desc", sortOrder: 0, displayName: "In Person - Called").ConfigureAwait(false),
                    await CreateADummyAuctionTypeAsync(id: ++index, key: "In Person - Silent", name: "In Person - Silent", desc: "desc", sortOrder: 1, displayName: "In Person - Silent").ConfigureAwait(false),
                    await CreateADummyAuctionTypeAsync(id: ++index, key: "Online", name: "Online", desc: "desc", sortOrder: 2, displayName: "Online").ConfigureAwait(false),
                };
                await InitializeMockSetAuctionTypesAsync(mockContext, RawAuctionTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Auction Statuses
            if (DoAll || DoAuctions || DoAuctionStatusTable)
            {
                var index = 0;
                RawAuctionStatuses = new()
                {
                    await CreateADummyAuctionStatusAsync(id: ++index, key: "Pending", name: "Pending", desc: "desc", sortOrder: 0, displayName: "Pending").ConfigureAwait(false),
                    await CreateADummyAuctionStatusAsync(id: ++index, key: "Open", name: "Open", desc: "desc", sortOrder: 1, displayName: "Open").ConfigureAwait(false),
                    await CreateADummyAuctionStatusAsync(id: ++index, key: "Closed", name: "Closed", desc: "desc", sortOrder: 2, displayName: "Closed").ConfigureAwait(false),
                };
                await InitializeMockSetAuctionStatusesAsync(mockContext, RawAuctionStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Lot Types
            if (DoAll || DoAuctions || DoLotTypeTable)
            {
                var index = 0;
                RawLotTypes = new()
                {
                    await CreateADummyLotTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetLotTypesAsync(mockContext, RawLotTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Lot Groups
            if (DoAll || DoAuctions || DoLotGroupTable)
            {
                var index = 0;
                RawLotGroups = new()
                {
                    await CreateADummyLotGroupAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetLotGroupsAsync(mockContext, RawLotGroups).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Lot Statuses
            if (DoAll || DoAuctions || DoLotStatusTable)
            {
                var index = 0;
                RawLotStatuses = new()
                {
                    await CreateADummyLotStatusAsync(id: ++index, key: "Pending", name: "Pending", desc: "desc", sortOrder: 0, displayName: "Pending").ConfigureAwait(false),
                    await CreateADummyLotStatusAsync(id: ++index, key: "Open", name: "Open", desc: "desc", sortOrder: 1, displayName: "Open").ConfigureAwait(false),
                    await CreateADummyLotStatusAsync(id: ++index, key: "Closed", name: "Closed", desc: "desc", sortOrder: 2, displayName: "Closed").ConfigureAwait(false),
                };
                await InitializeMockSetLotStatusesAsync(mockContext, RawLotStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Bid Statuses
            if (DoAll || DoAuctions || DoBidStatusTable)
            {
                var index = 0;
                RawBidStatuses = new()
                {
                    await CreateADummyBidStatusAsync(id: ++index, key: "Losing", name: "Losing", desc: "desc", sortOrder: 0, displayName: "Losing").ConfigureAwait(false),
                    await CreateADummyBidStatusAsync(id: ++index, key: "Winning", name: "Winning", desc: "desc", sortOrder: 1, displayName: "Winning").ConfigureAwait(false),
                    await CreateADummyBidStatusAsync(id: ++index, key: "Lost", name: "Lost", desc: "desc", sortOrder: 2, displayName: "Lost").ConfigureAwait(false),
                    await CreateADummyBidStatusAsync(id: ++index, key: "Won", name: "Won", desc: "desc", sortOrder: 3, displayName: "Won").ConfigureAwait(false),
                };
                await InitializeMockSetBidStatusesAsync(mockContext, RawBidStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Auctions
            if (DoAll || DoAuctions || DoAuctionTable)
            {
                RawAuctions = new()
                {
                    await CreateADummyAuctionAsync(id: 1, key: "KEY-12345", name: "Estate Sale 1", desc: "An auction", typeID: 1,
                        jsonAttributes: new SerializableAttributesDictionary
                        {
                            ["Color"] = new() { Key = "Color", Value = "Purple" },
                        }.SerializeAttributesDictionary()).ConfigureAwait(false),
                    await CreateADummyAuctionAsync(id: 2, key: "KEY-22345", name: "Estate Sale 2", desc: "An auction", typeID: 1,
                        jsonAttributes: new SerializableAttributesDictionary
                        {
                            ["Color"] = new() { Key = "Color", Value = "Orange" },
                        }.SerializeAttributesDictionary()).ConfigureAwait(false),
                };
                await InitializeMockSetAuctionsAsync(mockContext, RawAuctions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Lots
            if (DoAll || DoAuctions || DoLotTable)
            {
                RawLots = new()
                {
                    await CreateADummyLotAsync(id: 1, key: "KEY-12345", name: "Item 1", desc: "A lot", statusID: 1, auctionID: 1, productID: 1151,
                            jsonAttributes: new SerializableAttributesDictionary
                            {
                                ["Color"] = new() { Key = "Color", Value = "Red" },
                            }.SerializeAttributesDictionary())
                        .ConfigureAwait(false),
                    await CreateADummyLotAsync(id: 2, key: "KEY-12346", name: "Item 2", desc: "A lot", statusID: 1, auctionID: 1, productID: 1152,
                            jsonAttributes: new SerializableAttributesDictionary
                            {
                                ["Color"] = new() { Key = "Color", Value = "Green" },
                            }.SerializeAttributesDictionary())
                        .ConfigureAwait(false),
                };
                await InitializeMockSetLotsAsync(mockContext, RawLots).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Bids
            if (DoAll || DoAuctions || DoBidTable)
            {
                RawBids = new()
                {
                    await CreateADummyBidAsync(id: 1, key: "KEY-12345", statusID: 1, lotID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetBidsAsync(mockContext, RawBids).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Auction Categories
            if (DoAll || DoAuctions || DoAuctionCategoryTable)
            {
                RawAuctionCategories = new()
                {
                    await CreateADummyAuctionCategoryAsync(id: 1, key: "key-1", masterID: 1, slaveID: 1).ConfigureAwait(false),
                    await CreateADummyAuctionCategoryAsync(id: 2, key: "key-2", masterID: 2, slaveID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAuctionCategoriesAsync(mockContext, RawAuctionCategories).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Lot Categories
            if (DoAll || DoAuctions || DoLotCategoryTable)
            {
                RawLotCategories = new()
                {
                    await CreateADummyLotCategoryAsync(id: 1, key: "key-1", masterID: 1, slaveID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetLotCategoriesAsync(mockContext, RawLotCategories).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Brand Auctions
            if (DoAll || DoAuctions || DoBrandAuctionTable)
            {
                RawBrandAuctions = new()
                {
                    await CreateADummyBrandAuctionAsync(id: 1, key: "KEY-12345", masterID: 1, slaveID: 1, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyBrandAuctionAsync(id: 2, key: "KEY-12346", masterID: 1, slaveID: 2, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetBrandAuctionsAsync(mockContext, RawBrandAuctions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Franchise Auctions
            if (DoAll || DoAuctions || DoFranchiseAuctionTable)
            {
                RawFranchiseAuctions = new()
                {
                    await CreateADummyFranchiseAuctionAsync(id: 1, key: "KEY-12345", masterID: 1, slaveID: 1, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyFranchiseAuctionAsync(id: 2, key: "KEY-12346", masterID: 1, slaveID: 2, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetFranchiseAuctionsAsync(mockContext, RawFranchiseAuctions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Store Auctions
            if (DoAll || DoAuctions || DoStoreAuctionTable)
            {
                RawStoreAuctions = new()
                {
                    await CreateADummyStoreAuctionAsync(id: 1, key: "KEY-12345", masterID: 1, slaveID: 1, isVisibleIn: true).ConfigureAwait(false),
                    await CreateADummyStoreAuctionAsync(id: 2, key: "KEY-12346", masterID: 1, slaveID: 2, isVisibleIn: true).ConfigureAwait(false),
                };
                await InitializeMockSetStoreAuctionsAsync(mockContext, RawStoreAuctions).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
