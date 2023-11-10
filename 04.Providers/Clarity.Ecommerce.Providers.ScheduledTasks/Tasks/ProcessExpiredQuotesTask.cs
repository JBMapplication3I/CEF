// <copyright file="ProcessExpiredQuotesTask.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the process expired quotes task class</summary>
// ReSharper disable ExpressionIsAlwaysNull
namespace Clarity.Ecommerce.Tasks.ExpiredQuotes
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;

    /// <summary>The process expired quotes.</summary>
    /// <seealso cref="TaskBase"/>
    public class ProcessExpiredQuotesTask : TaskBase
    {
        /// <inheritdoc/>
        public override async Task ProcessAsync(IJobCancellationToken? cancellationToken)
        {
            if (GetActiveTaskJobsCount(null) > 1)
            {
                return;
            }
            string? contextProfileName = null;
            cancellationToken?.ThrowIfCancellationRequested();
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, "Process Expired Quotes Scheduled Task: Starting", contextProfileName).ConfigureAwait(false);
            var result = await RegistryLoaderWrapper.GetSalesQuoteActionsProvider(null)!
                .SetRecordsToExpiredAsync(contextProfileName).ConfigureAwait(false);
            if (!result)
            {
                var ex = new JobFailedException("Process Expired Quotes Scheduled Task: Unable to queue notifications");
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex.Message, ex, null).ConfigureAwait(false);
                throw ex;
            }
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, "Process Expired Quotes Scheduled Task: Finished", contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string? contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "0 0 * * *");
            // Every day at midnight
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp, out string description)
        {
            description = "Changes quotes to an expired status";
            return new()
            {
                CreateSetting(timestamp, "Process Expired Quotes: Cron Schedule", "0 0 * * *", null), // Every day at midnight
            };
        }
    }
}
