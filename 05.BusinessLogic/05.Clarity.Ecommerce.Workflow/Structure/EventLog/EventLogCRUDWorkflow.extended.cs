// <copyright file="EventLogCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the event log workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Mapper;
    using Models;

    public partial class EventLogWorkflow
    {
        /// <inheritdoc/>
        protected override bool OverrideDuplicateCheck => true;

        /// <inheritdoc/>
        public Task<IEventLogModel?> GetLastAsync(IEventLogSearchModel search, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.EventLogs
                    .FilterByActive(search.Active)
                    .FilterByName(search.Name)
                    .FilterByCustomKey(search.CustomKey)
                    .OrderByDescending(x => x.CreatedDate)
                    .SelectFirstFullEventLogAndMapToEventLogModel(contextProfileName));
        }

        /// <inheritdoc/>
        public Task AddEventAsync(
            string message,
            string name,
            string? customKey,
            int? dataID,
            string? contextProfileName)
        {
            return CreateAsync(
                new EventLogModel
                {
                    Active = true,
                    CustomKey = customKey,
                    Name = name,
                    Description = message,
                    DataID = dataID,
                },
                contextProfileName);
        }
    }
}
