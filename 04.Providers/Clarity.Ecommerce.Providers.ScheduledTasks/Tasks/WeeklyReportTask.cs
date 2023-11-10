// <copyright file="WeeklyReportTask.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the weekly report task class</summary>
namespace Clarity.Ecommerce.Tasks.Surveys.ForCalendarEvents
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;

    /// <summary>A weekly report task.</summary>
    /// <seealso cref="BaseTask"/>
    public class WeeklyReportTask : BaseTask
    {
        /// <inheritdoc/>
        public override async Task ProcessAsync(IJobCancellationToken cancellationToken)
        {
            if (GetActiveTaskJobsCount(null) > 1)
            {
                return;
            }
            cancellationToken.ThrowIfCancellationRequested();
            await Logger.LogInformationAsync(ConfigurationKey, $"Process {ConfigurationKey}: Starting", contextProfileName: null).ConfigureAwait(false);
            await WeeklyReportAsync(contextProfileName: null).ConfigureAwait(false);
            await Logger.LogInformationAsync(ConfigurationKey, $"Process {ConfigurationKey}: Finished", contextProfileName: null).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "0 0 * * FRI");
            // Every Friday
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp, out string description)
        {
            description = "Send weekly report to Group Leaders";
            return new List<IScheduledJobConfigurationSettingModel>
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 0 * * FRI"), // Every Friday
            };
        }

        /// <summary>Weekly report.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task WeeklyReportAsync(string contextProfileName)
        {
            var search = RegistryLoaderWrapper.GetInstance<ICalendarEventSearchModel>();
            search.CurrentEventsOnly = true;
            var events = (await Workflows.CalendarEvents.SearchAsync(search, false, contextProfileName).ConfigureAwait(false)).results;
            foreach (var evt in events)
            {
                // ReSharper disable once PossibleInvalidOperationException
                var groupLeadersIds = await Workflows.UserEventAttendances.GetGroupLeadersIDsAsync(evt.ID.Value, contextProfileName).ConfigureAwait(false);
                foreach (var groupLeaderID in groupLeadersIds)
                {
                    var groupLeader = await Workflows.Users.GetAsync(groupLeaderID, contextProfileName).ConfigureAwait(false);
                    try
                    {
                        await Workflows.EmailQueues.QueueWeeklyEventReportGroupLeaderAsync(evt, groupLeader, null).ConfigureAwait(false);
                    }
                    catch
                    {
                        // Do Nothing
                    }
                }
            }
        }
    }
}
