// <copyright file="SurveyTask.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the survey task class</summary>
namespace Clarity.Ecommerce.Tasks.Surveys.ForCalendarEvents
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;

    /// <summary>A survey task.</summary>
    /// <seealso cref="BaseTask"/>
    public class SurveyTask : BaseTask
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
            await CreateSurveysAsync(contextProfileName: null).ConfigureAwait(false);
            await Logger.LogInformationAsync(ConfigurationKey, $"Process {ConfigurationKey}: Finished", contextProfileName: null).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "*/15 * * * *");
            // Every 15 minutes
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp, out string description)
        {
            description = "Performs checks for end of events in order to create and send a survey";
            return new List<IScheduledJobConfigurationSettingModel>
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "*/15 * * * *"), // Every 15 minutes
            };
        }

        /// <summary>Creates the surveys.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new surveys.</returns>
        private async Task CreateSurveysAsync(string contextProfileName)
        {
            var surveyMonkeyProvider = RegistryLoaderWrapper.GetSurveyProvider(contextProfileName);
            var search = RegistryLoaderWrapper.GetInstance<ICalendarEventSearchModel>();
            search.EndDate = DateExtensions.GenDateTime.AddDays(-1);
            search.StatusKey = "NORMAL";
            var events = await Workflows.CalendarEvents.GetDoneEventsWithNoSurveyAsync(search, contextProfileName).ConfigureAwait(false);
            foreach (var @event in events)
            {
                try
                {
                    await surveyMonkeyProvider.CreateEventSurveyAsync(@event, contextProfileName).ConfigureAwait(false);
                    @event.StatusKey = "SURVEYSENT";
                    @event.StatusID = 0;
                    @event.StatusName = null;
                    @event.Status = null;
                    await Workflows.CalendarEvents.UpdateAsync(@event, contextProfileName).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    await Logger.LogErrorAsync("Create survey", ex.Message, contextProfileName).ConfigureAwait(false);
                }
            }
        }
    }
}
