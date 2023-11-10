// <copyright file="DoMockingSetupForContextRunnerHangfireAsync.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner hangfire class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerHangfireAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Scheduled Job Configurations
            if (DoAll || DoHangfire || DoScheduledJobConfigurationTable)
            {
                RawScheduledJobConfigurations = new()
                {
                    await CreateADummyScheduledJobConfigurationAsync(id: 1, key: "SCHEDULED-JOB-CONFIGURATION-1", name: "Scheduled Job Configuration 1", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetScheduledJobConfigurationsAsync(mockContext, RawScheduledJobConfigurations).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Scheduled Job Configuration Settings
            if (DoAll || DoHangfire || DoScheduledJobConfigurationSettingTable)
            {
                RawScheduledJobConfigurationSettings = new()
                {
                    await CreateADummyScheduledJobConfigurationSettingAsync(id: 1, key: "SCHEDULED-JOB-CONFIGURATION-SETTING-1").ConfigureAwait(false),
                };
                await InitializeMockSetScheduledJobConfigurationSettingsAsync(mockContext, RawScheduledJobConfigurationSettings).ConfigureAwait(false);
            }
            #endregion
        }

        // Dirty Checking
        public bool HangfireAggregatedCounterDirty { private get; set; }
        public bool HangfireCounterDirty { private get; set; }
        public bool HangfireHashDirty { private get; set; }
        public bool HangfireJobDirty { private get; set; }
        public bool HangfireJobParameterDirty { private get; set; }
        public bool HangfireJobQueueDirty { private get; set; }
        public bool HangfireListDirty { private get; set; }
        public bool HangfireSchemaDirty { private get; set; }
        public bool HangfireServerDirty { private get; set; }
        public bool HangfireSetDirty { private get; set; }
        public bool HangfireStateDirty { private get; set; }
    }
}
