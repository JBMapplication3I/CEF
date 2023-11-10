// <copyright file="DoMockingSetupForContextRunnerInventory.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner inventory class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerInventoryAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: InventoryLocations
            if (DoAll || DoInventory || DoInventoryLocationTable)
            {
                var index = 0;
                RawInventoryLocations = new()
                {
                    await CreateADummyInventoryLocationAsync(id: ++index, key: "UMCOM", name: "UMCOM", desc: "desc").ConfigureAwait(false),
                    await CreateADummyInventoryLocationAsync(id: ++index, key: "DALLAS", name: "Dallas Distribution Center", desc: "desc", contactID: 2).ConfigureAwait(false),
                    await CreateADummyInventoryLocationAsync(id: ++index, key: "DC", name: "DC Warehouse", desc: "desc", contactID: 3).ConfigureAwait(false),
                };
                await InitializeMockSetInventoryLocationsAsync(mockContext, RawInventoryLocations).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: InventoryLocationSections
            if (DoAll || DoInventory || DoInventoryLocationSectionTable)
            {
                var index = 0;
                RawInventoryLocationSections = new()
                {
                    await CreateADummyInventoryLocationSectionAsync(id: ++index, key: "Shelf 1-1B", name: "Shelf 1-1B", desc: "desc").ConfigureAwait(false),
                    await CreateADummyInventoryLocationSectionAsync(id: ++index, key: "Overhead 1", name: "Overhead 1", desc: "desc").ConfigureAwait(false),
                    await CreateADummyInventoryLocationSectionAsync(id: ++index, key: "Back Dock 1", name: "Back Dock 1", desc: "desc").ConfigureAwait(false),
                    await CreateADummyInventoryLocationSectionAsync(id: ++index, key: "Shelf 1-2B", name: "Shelf 1-2B", desc: "desc", inventoryLocationID: 2).ConfigureAwait(false),
                    await CreateADummyInventoryLocationSectionAsync(id: ++index, key: "Overhead 2", name: "Overhead 2", desc: "desc", inventoryLocationID: 2).ConfigureAwait(false),
                    await CreateADummyInventoryLocationSectionAsync(id: ++index, key: "Back Dock 2", name: "Back Dock 2", desc: "desc", inventoryLocationID: 2).ConfigureAwait(false),
                    await CreateADummyInventoryLocationSectionAsync(id: ++index, key: "Shelf 1-3B", name: "Shelf 1-3B", desc: "desc", inventoryLocationID: 3).ConfigureAwait(false),
                    await CreateADummyInventoryLocationSectionAsync(id: ++index, key: "Overhead 3", name: "Overhead 3", desc: "desc", inventoryLocationID: 3).ConfigureAwait(false),
                    await CreateADummyInventoryLocationSectionAsync(id: ++index, key: "Back Dock 3", name: "Back Dock 3", desc: "desc", inventoryLocationID: 3).ConfigureAwait(false),
                };
                await InitializeMockSetInventoryLocationSectionsAsync(mockContext, RawInventoryLocationSections).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: InventoryLocationRegions
            if (DoAll || DoInventory || DoInventoryLocationRegionTable)
            {
                var index = 0;
                RawInventoryLocationRegions = new()
                {
                    await CreateADummyInventoryLocationRegionAsync(id: ++index, key: "one").ConfigureAwait(false),
                    await CreateADummyInventoryLocationRegionAsync(id: ++index, key: "two").ConfigureAwait(false),
                    await CreateADummyInventoryLocationRegionAsync(id: ++index, key: "three").ConfigureAwait(false),
                };
                await InitializeMockSetInventoryLocationRegionsAsync(mockContext, RawInventoryLocationRegions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable InventoryLocationUsers
            if (DoAll || DoInventory || DoInventoryLocationUserTable)
            {
                var index = 0;
                RawInventoryLocationUsers = new()
                {
                    await CreateADummyInventoryLocationUserAsync(id: ++index, key: "one", masterID: 1, slaveID: 1).ConfigureAwait(false),
                    await CreateADummyInventoryLocationUserAsync(id: ++index, key: "two", masterID: 2, slaveID: 2).ConfigureAwait(false),
                    await CreateADummyInventoryLocationUserAsync(id: ++index, key: "three", masterID: 3, slaveID: 3).ConfigureAwait(false),
                };
                await InitializeMockSetInventoryLocationUsersAsync(mockContext, RawInventoryLocationUsers).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
