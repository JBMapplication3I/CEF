// <copyright file="UpdateSeoSiteMapsTask.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the update seo site maps task class</summary>
// ReSharper disable ExpressionIsAlwaysNull
namespace Clarity.Ecommerce.Tasks.UpdateSeoSiteMaps
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;
    using JSConfigs;

    /// <summary>An update seo site maps.</summary>
    /// <seealso cref="TaskBase"/>
    public class UpdateSeoSiteMapsTask : TaskBase
    {
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
            // Start a batch
            var dropPath = System.IO.Path.Combine(
                CEFConfigDictionary.StoredFilesInternalLocalPath,
                CEFConfigDictionary.SEOSiteMapsRelativePath);
            var siteMap1 = await Workflows.Products.GenerateProductSiteMapContentAsync(contextProfileName).ConfigureAwait(false);
            var result1 = await Workflows.Products.SaveProductSiteMapAsync(siteMap1, dropPath).ConfigureAwait(false);
            if (!result1)
            {
                var ex = new JobFailedException($"Process {ConfigurationKey}: Unable to save product site map");
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex.Message, ex, null).ConfigureAwait(false);
                throw ex;
            }
            cancellationToken?.ThrowIfCancellationRequested();
            var siteMap2 = await Workflows.Categories.GenerateCategorySiteMapContentAsync(contextProfileName).ConfigureAwait(false);
            var result2 = await Workflows.Categories.SaveCategorySiteMapAsync(siteMap2, dropPath).ConfigureAwait(false);
            if (!result2)
            {
                var ex = new JobFailedException($"Process {ConfigurationKey}: Unable to save category site map");
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex.Message, ex, null).ConfigureAwait(false);
                throw ex;
            }
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Finished", contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string? contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "0 * * * * *");
            // Every hour
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp, out string description)
        {
            description = "Periodically update the Site Maps that are used for SEO indexing purposes.";
            return new()
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 * * * *", null), // Every hour
            };
        }
    }
}
