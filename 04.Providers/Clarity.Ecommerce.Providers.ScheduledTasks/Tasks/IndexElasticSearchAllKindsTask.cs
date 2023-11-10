// <copyright file="IndexElasticSearchAllKindsTask.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the index elastic search products, stores and/or categories task class</summary>
// ReSharper disable ExpressionIsAlwaysNull
namespace Clarity.Ecommerce.Tasks.IndexElasticSearch.Products
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;

    /// <summary>An index elastic search products, stores and/or categories.</summary>
    /// <seealso cref="TaskBase"/>
    public class IndexElasticSearchAllKindsTask : TaskBase
    {
        /// <inheritdoc/>
        public override async Task ProcessAsync(IJobCancellationToken? cancellationToken)
        {
            string? contextProfileName = null;
            if (GetActiveTaskJobsCount(contextProfileName) > 1)
            {
                return;
            }
            cancellationToken?.ThrowIfCancellationRequested();
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Starting", contextProfileName).ConfigureAwait(false);
            // Start a batch
            try
            {
                var provider = RegistryLoaderWrapper.GetSearchingProvider(contextProfileName);
                await provider!.IndexAsync(
                        contextProfileName: contextProfileName,
                        index: "all",
                        ct: cancellationToken?.ShutdownToken ?? CancellationToken.None)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var ex2 = new JobFailedException($"Process {ConfigurationKey}: Unable to index products, stores and/or categories", ex);
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex2.Message, ex2, contextProfileName).ConfigureAwait(false);
                throw ex2;
            }
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Finished", contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string? contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "*/15 * * * *"); // Every 15 minutes
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp,
            out string description)
        {
            description = "Runs the indexer for products, stores and/or categories in ElasticSearch.";
            return new()
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "*/15 * * * *", null), // Every 15 minutes
            };
        }
    }
}
