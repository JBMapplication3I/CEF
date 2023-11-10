// <copyright file="DoMockingSetupForContextRunnerCountersAsync.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner counters class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerCountersAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Counters
            if (DoAll || DoCounters || DoCounterTable)
            {
                RawCounters = new()
                {
                    await CreateADummyCounterAsync(id: 1, key: "COUNTER-1", value: 1).ConfigureAwait(false),
                };
                await InitializeMockSetCountersAsync(mockContext, RawCounters).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Counter Logs
            if (DoAll || DoCounters || DoCounterLogTable)
            {
                RawCounterLogs = new()
                {
                    await CreateADummyCounterLogAsync(id: 1, key: "COUNTER-LOG-1", value: 1.5m).ConfigureAwait(false),
                };
                await InitializeMockSetCounterLogsAsync(mockContext, RawCounterLogs).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Counter Log Types
            if (DoAll || DoCounters || DoCounterLogTypeTable)
            {
                RawCounterLogTypes = new()
                {
                    await CreateADummyCounterLogTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetCounterLogTypesAsync(mockContext, RawCounterLogTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Counters
            if (DoAll || DoCounters || DoCounterTypeTable)
            {
                RawCounterTypes = new()
                {
                    await CreateADummyCounterTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetCounterTypesAsync(mockContext, RawCounterTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
