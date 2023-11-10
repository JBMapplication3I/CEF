// <copyright file="LastPaymentReminderTask.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the last payment reminder task class</summary>
namespace Clarity.Ecommerce.Tasks.Surveys.ForCalendarEvents
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;
    using JetBrains.Annotations;

    /// <summary>A last payment reminder task.</summary>
    /// <seealso cref="BaseTask"/>
    [PublicAPI]
    public class LastPaymentReminderTask : BaseTask
    {
        private const string LastReminderSent = "LastReminderSent";

        /// <inheritdoc/>
        public override async Task ProcessAsync(IJobCancellationToken cancellationToken)
        {
            if (GetActiveTaskJobsCount(null) > 1)
            {
                return;
            }
            cancellationToken.ThrowIfCancellationRequested();
            await Logger.LogInformationAsync(ConfigurationKey, $"Process {ConfigurationKey}: Starting", contextProfileName: null).ConfigureAwait(false);
            await LastPaymentReminderAsync(contextProfileName: null).ConfigureAwait(false);
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
            description = "Performs checks to send emails for the last payment";
            return new List<IScheduledJobConfigurationSettingModel>
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 0 * * *"), // Every day at midnight
            };
        }

        private async Task LastPaymentReminderAsync(string contextProfileName)
        {
            // Get event keys where event start date is: StartDate - 90 days < Today < Start Date
            var search = RegistryLoaderWrapper.GetInstance<ICalendarEventSearchModel>();
            search.StrictDaysUntilDeparture = 100;
            var events = (await Workflows.CalendarEvents.SearchAsync(search, false, contextProfileName).ConfigureAwait(false)).results;
            foreach (var @event in events)
            {
                // ReSharper disable once PossibleInvalidOperationException
                var model = await Workflows.CalendarEvents.GetAsync(@event.ID.Value, contextProfileName).ConfigureAwait(false);
                if (model.SerializableAttributes.ContainsKey(LastReminderSent)
                    || model.UserEventAttendances == null
                    || model.UserEventAttendances.Count == 0)
                {
                    continue;
                }
                foreach (var uea in model.UserEventAttendances)
                {
                    try
                    {
                        var user = await Workflows.Users.GetAsync(uea.SlaveID, contextProfileName).ConfigureAwait(false);
                        await Workflows.EmailQueues.QueueLastPaymentReminderAsync(user, null).ConfigureAwait(false);
                    }
                    catch
                    {
                        // Do Nothing
                    }
                }
                model.SerializableAttributes[LastReminderSent] = new SerializableAttributeObject
                {
                    Key = LastReminderSent,
                    Value = true.ToString(),
                };
                await Workflows.CalendarEvents.UpdateAsync(model, contextProfileName).ConfigureAwait(false);
            }
        }
    }
}
