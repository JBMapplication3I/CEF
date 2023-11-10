// <copyright file="OverduePassportTask.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the overdue passport task class</summary>
namespace Clarity.Ecommerce.Tasks.Surveys.ForCalendarEvents
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;

    /// <summary>An overdue passport task.</summary>
    /// <seealso cref="BaseTask"/>
    public class OverduePassportTask : BaseTask
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
            await PassportOverdueAsync(contextProfileName: null).ConfigureAwait(false);
            await Logger.LogInformationAsync(ConfigurationKey, $"Process {ConfigurationKey}: Finished", contextProfileName: null).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "0 0 * * *");
            // Every day at midnight
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp, out string description)
        {
            description = "Performs check for passport information before a trip";
            return new List<IScheduledJobConfigurationSettingModel>
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 0 * * *"), // Every 15 minutes
            };
        }

        /// <summary>Passport overdue.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task PassportOverdueAsync(string contextProfileName)
        {
            // Get event keys where event start date is: StartDate - 90 days < Today < Start Date.
            foreach (var eventKey in await Workflows.CalendarEvents.GetEventKeysInLastStatementStateAsync(90, contextProfileName).ConfigureAwait(false))
            {
                var search = RegistryLoaderWrapper.GetInstance<IUserEventAttendanceSearchModel>();
                search.CalendarEventKey = eventKey;
                var attendances = await Workflows.UserEventAttendances.GetEventAttendeesByEventIDAsync(search, contextProfileName).ConfigureAwait(false);
                foreach (var att in attendances.Result)
                {
                    var user = await Workflows.Users.GetAsync(att.SlaveID, contextProfileName).ConfigureAwait(false);
                    if (user.SerializableAttributes.ContainsKey("OverduePassportEmailSent")
                        || user.SerializableAttributes.ContainsKey("PassportDateOfExpiration")
                           && !string.IsNullOrEmpty(user.SerializableAttributes["PassportDateOfExpiration"].Value))
                    {
                        continue;
                    }
#pragma warning disable 618
                    await Workflows.EmailQueues.QueuePassportOverdueAsync(att.Slave, att.CalendarEvent.StartDate.AddDays(-90), null).ConfigureAwait(false);
#pragma warning restore 618
                    user.SerializableAttributes["OverduePassportEmailSent"] = new SerializableAttributeObject
                    {
                        Key = "OverduePassportEmailSent",
                        Value = "true",
                    };
                    await Workflows.Users.UpdateAsync(user, contextProfileName).ConfigureAwait(false);
                }
            }
        }
    }
}
