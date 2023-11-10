// <copyright file="DoMockingSetupForContextRunnerScouting.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner scouting class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerScoutingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Scouts
            if (DoAll || DoSampling || DoScoutTable)
            {
                RawScouts = new()
                {
                    await CreateADummyScoutAsync(id: 1, key: "SCOUT-1").ConfigureAwait(false),
                };
                await InitializeMockSetScoutsAsync(mockContext, RawScouts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Scout Categories
            if (DoAll || DoSampling || DoScoutCategoryTable)
            {
                RawScoutCategories = new()
                {
                    await CreateADummyScoutCategoryAsync(id: 1, key: "SCOUT-CATEGORY-1").ConfigureAwait(false),
                };
                await InitializeMockSetScoutCategoriesAsync(mockContext, RawScoutCategories).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Scout Category Types
            if (DoAll || DoSampling || DoScoutCategoryTypeTable)
            {
                RawScoutCategoryTypes = new()
                {
                    await CreateADummyScoutCategoryTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetScoutCategoryTypesAsync(mockContext, RawScoutCategoryTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
