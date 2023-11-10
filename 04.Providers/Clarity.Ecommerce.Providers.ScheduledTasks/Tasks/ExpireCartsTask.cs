// <copyright file="ExpireCartsTask.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the expire carts task class</summary>
// ReSharper disable ExpressionIsAlwaysNull
namespace Clarity.Ecommerce.Tasks.ExpireCarts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;

    /// <summary>An expire carts task.</summary>
    /// <seealso cref="TaskBase"/>
    public class ExpireCartsTask : TaskBase
    {
        /// <summary>Gets or sets the expired threshold.</summary>
        /// <value>The expired threshold.</value>
        private string ExpiredThreshold { get; set; } = null!;

        /// <inheritdoc/>
        public override async Task ProcessAsync(IJobCancellationToken? cancellationToken)
        {
            string? contextProfileName = null;
            if (GetActiveTaskJobsCount(contextProfileName) > 1)
            {
                /* TODO@JTG: Issue Cancel Job command */
                return;
            }
            cancellationToken?.ThrowIfCancellationRequested();
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey} Scheduled Task: Starting", contextProfileName).ConfigureAwait(false);
            // Start a batch
            var result = await Workflows.Carts.ExpireCartsAsync(int.Parse(ExpiredThreshold), contextProfileName).ConfigureAwait(false);
            // The result is whether the task queue accepted all the emails to batch out
            if (!result.ActionSucceeded)
            {
                var ex = new JobFailedException($"Process {ConfigurationKey} Scheduled Task: Failed");
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex.Message, ex, contextProfileName).ConfigureAwait(false);
                throw ex;
            }
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey} Scheduled Task: Finished", contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task LoadSettingsAsync(string? contextProfileName)
        {
            await LoadSettingsAsync(contextProfileName, "0 0 * * *").ConfigureAwait(false);
            ExpiredThreshold = (await GetSettingValueOrCreateDefaultAsync(
                    settings: Configuration.ScheduledJobConfigurationSettings!,
                    name: $"Process {ConfigurationKey}: Expired Threshold (days)",
                    defaultValue: "30",
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false))!;
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp,
            out string description)
        {
            description = "Expires carts that have reached a specified age in days.";
            return new()
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 0 * * *", null), // Daily at midnight
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Expired Threshold (days)", "30", null), // After 30 days since last updated
            };
        }
    }
}
