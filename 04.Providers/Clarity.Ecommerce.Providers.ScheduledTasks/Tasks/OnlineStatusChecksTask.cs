// <copyright file="OnlineStatusChecksTask.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the online status checks task class</summary>
// ReSharper disable ExpressionIsAlwaysNull
namespace Clarity.Ecommerce.Tasks.OnlineStatusChecks
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    /// <summary>An index elastic search products.</summary>
    /// <seealso cref="TaskBase"/>
    public class OnlineStatusChecksTask : TaskBase
    {
        /// <summary>Gets or sets the away threshold.</summary>
        /// <value>The away threshold.</value>
        private string AwayThreshold { get; set; } = null!;

        /// <summary>Gets or sets the offline threshold.</summary>
        /// <value>The offline threshold.</value>
        private string OfflineThreshold { get; set; } = null!;

        /// <inheritdoc/>
        public override async Task ProcessAsync(IJobCancellationToken? cancellationToken)
        {
            if (GetActiveTaskJobsCount(null) > 1)
            {
                return;
            }
            cancellationToken?.ThrowIfCancellationRequested();
            string? contextProfileName = null;
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Starting", contextProfileName).ConfigureAwait(false);
            try
            {
                await ProcessHeartBeatsAsync(contextProfileName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var ex2 = new JobFailedException($"Process {ConfigurationKey}: Unable to process heartbeats", ex);
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex2.Message, ex2, null).ConfigureAwait(false);
                throw ex2;
            }
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Finished", contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task LoadSettingsAsync(string? contextProfileName)
        {
            await LoadSettingsAsync(contextProfileName, "*/1 * * * *").ConfigureAwait(false);
            AwayThreshold = (await GetSettingValueOrCreateDefaultAsync(
                    Configuration.ScheduledJobConfigurationSettings!,
                    $"Process {ConfigurationKey}: Away Threshold (seconds)",
                    "300",
                    contextProfileName)
                .ConfigureAwait(false))!;
            OfflineThreshold = (await GetSettingValueOrCreateDefaultAsync(
                    Configuration.ScheduledJobConfigurationSettings!,
                    $"Process {ConfigurationKey}: Offline Threshold (seconds)",
                    "3900",
                    contextProfileName)
                .ConfigureAwait(false))!;
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp, out string description)
        {
            description = "Performs checks against the heartbeat data for chat to mark users as Away or Offline";
            return new()
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "*/1 * * * *", null), // Every minute
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Away Threshold (seconds)", "300", null), // After 5 minutes since heartbeat
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Offline Threshold (seconds)", "3900", null), // After 65 minutes since heartbeat (60+5)
            };
        }

        /// <summary>Process a status.</summary>
        /// <param name="context">           The context.</param>
        /// <param name="now">               The now Date/Time.</param>
        /// <param name="currentStatusID">   Identifier for the current status.</param>
        /// <param name="newStatusID">       Identifier for the new status.</param>
        /// <param name="newStatusThreshold">The new status threshold.</param>
        /// <returns>A Task.</returns>
        private static async Task ProcessAStatusAsync(
            IClarityEcommerceEntities context,
            DateTime now,
            int? currentStatusID,
            int newStatusID,
            int newStatusThreshold)
        {
            foreach (var userID in context.Users
                .AsNoTracking()
                .FilterByActive(true)
                .FilterUsersByUserOnlineStatusID(currentStatusID)
                .Select(x => x.ID)
                .Distinct())
            {
                var lastHeartBeatForUser = await context.ConversationUsers
                    .Where(x => x.SlaveID == userID)
                    .Select(x => x.LastHeartbeat)
                    .DefaultIfEmpty(DateTime.MinValue)
                    .MaxAsync()
                    .ConfigureAwait(false);
                if (lastHeartBeatForUser == null
                    || lastHeartBeatForUser == DateTime.MinValue
                    || now - lastHeartBeatForUser > new TimeSpan(0, 0, newStatusThreshold))
                {
                    (await context.Users.FilterByID(userID).SingleAsync().ConfigureAwait(false)).UserOnlineStatusID = newStatusID;
                }
            }
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
        }

        /// <summary>Enforce status present.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int.</returns>
        private Task<int> EnforceStatusPresentAsync(string name, string? contextProfileName)
        {
            return Workflows.UserOnlineStatuses.ResolveWithAutoGenerateToIDAsync(null, null, name, null, contextProfileName);
        }

        /// <summary>Process the heart beats.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task ProcessHeartBeatsAsync(string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            var onlineStatusID = await EnforceStatusPresentAsync("Online", contextProfileName).ConfigureAwait(false);
            var awayStatusID = await EnforceStatusPresentAsync("Away", contextProfileName).ConfigureAwait(false);
            var offlineStatusID = await EnforceStatusPresentAsync("Offline", contextProfileName).ConfigureAwait(false);
            var awayStatusThreshold = int.Parse(AwayThreshold);
            var offlineStatusThreshold = int.Parse(OfflineThreshold);
            var now = DateExtensions.GenDateTime;
            // Set Online users to Away if above the threshold
            // Set Away users to Offline if above the threshold
            // Set null value users to Offline
            await Task.WhenAll(
                ProcessAStatusAsync(context, now, onlineStatusID, awayStatusID, awayStatusThreshold),
                ProcessAStatusAsync(context, now, awayStatusID, offlineStatusID, offlineStatusThreshold),
                ProcessAStatusAsync(context, now, null, offlineStatusID, 0))
                .ConfigureAwait(false);
        }
    }
}
