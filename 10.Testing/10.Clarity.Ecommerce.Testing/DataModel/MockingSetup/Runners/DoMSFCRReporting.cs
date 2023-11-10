// <copyright file="DoMockingSetupForContextRunnerReportingAsync.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner reporting class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerReportingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Reports
            if (DoAll || DoReporting || DoReportTable)
            {
                RawReports = new()
                {
                    await CreateADummyReportAsync(id: 1, key: "key", name: "name", desc: "desc", jsonAttributes: "{}", resultsData: "").ConfigureAwait(false),
                };
                await InitializeMockSetReportsAsync(mockContext, RawReports).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Report Types
            if (DoAll || DoReporting || DoReportTypeTable)
            {
                RawReportTypes = new()
                {
                    await CreateADummyReportTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetReportTypesAsync(mockContext, RawReportTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
