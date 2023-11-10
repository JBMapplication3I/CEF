// <copyright file="ScheduledJobConfigurationCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the scheduled job configuration workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Mapper;

    public partial class ScheduledJobConfigurationWorkflow
    {
        /// <inheritdoc/>
        public override Task<IScheduledJobConfigurationModel?> GetAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.ScheduledJobConfigurations
                    .AsNoTracking()
                    .Include(x => x.ScheduledJobConfigurationSettings)
                    .Include(x => x.ScheduledJobConfigurationSettings!.Select(y => y.Slave))
                    .Include(x => x.ScheduledJobConfigurationSettings!.Select(y => y.Slave).Select(z => z!.Type))
                    .FilterByID(id)
                    .SelectSingleFullScheduledJobConfigurationAndMapToScheduledJobConfigurationModel(contextProfileName));
        }

        /// <inheritdoc/>
        public override Task<IScheduledJobConfigurationModel?> GetAsync(string key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.ScheduledJobConfigurations
                    .AsNoTracking()
                    .Include(x => x.ScheduledJobConfigurationSettings)
                    .Include(x => x.ScheduledJobConfigurationSettings!.Select(y => y.Slave))
                    .Include(x => x.ScheduledJobConfigurationSettings!.Select(y => y.Slave).Select(z => z!.Type))
                    .FilterByActive(true)
                    .FilterByCustomKey(key, true)
                    .SelectSingleFullScheduledJobConfigurationAndMapToScheduledJobConfigurationModel(contextProfileName));
        }
    }
}
