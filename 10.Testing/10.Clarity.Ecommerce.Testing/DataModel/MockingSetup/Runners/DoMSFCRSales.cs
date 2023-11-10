// <copyright file="DoMockingSetupForContextRunnerSales.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner sales class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerSalesAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Sales Groups
            if (DoAll || DoSales || DoSalesGroupTable)
            {
                RawSalesGroups = new()
                {
                    await CreateADummySalesGroupAsync(id: 00001, key: "WEB-18-1").ConfigureAwait(false),
                };
                await InitializeMockSetSalesGroupsAsync(mockContext, RawSalesGroups).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Item Target Types
            if (DoAll || DoSales || DoSalesItemTargetTypeTable)
            {
                var index = 0;
                RawSalesItemTargetTypes = new()
                {
                    await CreateADummySalesItemTargetTypeAsync(id: ++index, jsonAttributes: "{}", sortOrder: 0, key: "ShipToHome", name: "Ship to Home", displayName: "Ship to Home").ConfigureAwait(false),
                    await CreateADummySalesItemTargetTypeAsync(id: ++index, jsonAttributes: "{}", sortOrder: 1, key: "ShipToStore", name: "Ship to Store", displayName: "Ship to Store").ConfigureAwait(false),
                    await CreateADummySalesItemTargetTypeAsync(id: ++index, jsonAttributes: "{}", sortOrder: 2, key: "PickupInStore", name: "Pickup In Store", displayName: "Pickup In Store").ConfigureAwait(false),
                    await CreateADummySalesItemTargetTypeAsync(id: ++index, jsonAttributes: "{}", sortOrder: 3, key: "ShipToWarehouse", name: "Ship to Warehouse", displayName: "Ship to Warehouse").ConfigureAwait(false),
                };
                await InitializeMockSetSalesItemTargetTypesAsync(mockContext, RawSalesItemTargetTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
