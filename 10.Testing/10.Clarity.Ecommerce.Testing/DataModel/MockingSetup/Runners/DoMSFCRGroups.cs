// <copyright file="DoMSFCRGroups.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner groups class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerGroupsAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Groups
            if (DoAll || DoGroups || DoGroupTable)
            {
                RawGroups = new()
                {
                    await CreateADummyGroupAsync(id: 1, key: "GROUP-1", name: "Group 1", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetGroupsAsync(mockContext, RawGroups).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Group Statuses
            if (DoAll || DoGroups || DoGroupStatusTable)
            {
                RawGroupStatuses = new()
                {
                    await CreateADummyGroupStatusAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetGroupStatusesAsync(mockContext, RawGroupStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Group Types
            if (DoAll || DoGroups || DoGroupTypeTable)
            {
                RawGroupTypes = new()
                {
                    await CreateADummyGroupTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetGroupTypesAsync(mockContext, RawGroupTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Group Users
            if (DoAll || DoGroups || DoGroupUserTable)
            {
                RawGroupUsers = new()
                {
                    await CreateADummyGroupUserAsync(id: 1, key: "GROUP-USER-1").ConfigureAwait(false),
                };
                await InitializeMockSetGroupUsersAsync(mockContext, RawGroupUsers).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
