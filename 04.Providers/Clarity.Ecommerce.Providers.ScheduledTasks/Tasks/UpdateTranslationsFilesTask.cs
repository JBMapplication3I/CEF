// <copyright file="UpdateTranslationsFilesTask.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the update translations files task class</summary>
// ReSharper disable ExpressionIsAlwaysNull

namespace Clarity.Ecommerce.Tasks.UpdateTranslationsFiles
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;
    using Newtonsoft.Json;

    /// <summary>An index elastic search products.</summary>
    /// <seealso cref="TaskBase"/>
    public class UpdateTranslationsFilesTask : TaskBase
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
                await GenerateFilesAsync(contextProfileName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var ex2 = new JobFailedException($"Process {ConfigurationKey}: Unable to update translations files", ex);
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex2.Message, ex2, null).ConfigureAwait(false);
                throw ex2;
            }
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Finished", contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string? contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "0/15 * * * *"); // Every 15 minutes
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp,
            out string description)
        {
            description = "Updates static JS files that contain translations data.";
            return new()
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0/15 * * * *", null), // Every 15 minutes
            };
        }

        private async Task GenerateFilesAsync(string? contextProfileName)
        {
            var paths = new Dictionary<string, string>
            {
                ["ui.admin"] = (Globals.CEFRootPath + @"07.Portals\Admin\AngJS\lib\cef\js\i18n").Replace(@"07.Portals\07.Portals\", @"07.Portals\"),
                ["ui.storefront"] = (Globals.CEFRootPath + @"07.Portals\Storefront\AngJS\lib\cef\js\i18n").Replace(@"07.Portals\07.Portals\", @"07.Portals\"),
                ["ui.brandAdmin"] = (Globals.CEFRootPath + @"07.Portals\BrandAdmin\Service\i18n").Replace(@"07.Portals\07.Portals\", @"07.Portals\"),
                ["ui.storeAdmin"] = (Globals.CEFRootPath + @"07.Portals\StoreAdmin\Service\i18n").Replace(@"07.Portals\07.Portals\", @"07.Portals\"),
                ["ui.vendorAdmin"] = (Globals.CEFRootPath + @"07.Portals\VendorAdmin\Service\i18n").Replace(@"07.Portals\07.Portals\", @"07.Portals\"),
            };
            List<string> languages;
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                languages = await context.Languages
                    .AsNoTracking()
                    .FilterByActive(true)
                    .Select(x => x.Locale!)
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            foreach (var kvp in paths)
            {
                var pathDirectories = kvp.Value.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                var pathBuilder = string.Empty;
                foreach (var directory in pathDirectories)
                {
                    pathBuilder += (pathBuilder != string.Empty ? @"\" : string.Empty) + directory;
                    if (Directory.Exists(pathBuilder))
                    {
                        continue;
                    }
                    Directory.CreateDirectory(pathBuilder);
                }
                await languages.ForEachAsync(
                    4,
                    async language =>
                    {
                        var request = RegistryLoaderWrapper.GetInstance<IUiTranslationSearchModel>(contextProfileName);
                        request.KeyStartsWith = kvp.Key;
                        request.Locale = language;
                        var fullPath = kvp.Value + @"\" + kvp.Key + "." + language + ".json";
                        if (File.Exists(fullPath))
                        {
                            var lastData = await Workflows.UiTranslations.GetLastModifiedForResultSetAsync(
                                    request,
                                    contextProfileName)
                                .ConfigureAwait(false);
                            var lastWrite = File.GetLastWriteTime(fullPath);
                            if (lastWrite >= lastData)
                            {
                                // We don't need to write this file again as it hasn't changed
                                return;
                            }
                        }
                        var result = await Workflows.UiTranslations.SearchAndReturnDictionaryAsync(
                                request,
                                contextProfileName)
                            .ConfigureAwait(false);
                        var serialized = !result.Keys.Any()
                            ? "{}"
                            : JsonConvert.SerializeObject(
                                result[language],
                                SerializableAttributesDictionaryExtensions.JsonSettings);
                        var bytes = Encoding.UTF8.GetBytes(serialized);
                        using var file = !File.Exists(fullPath)
                            ? File.Create(fullPath, bytes.Length)
                            : new(fullPath, FileMode.Truncate);
#if NET5_0_OR_GREATER
                        await file.WriteAsync(bytes.AsMemory(0, bytes.Length)).ConfigureAwait(false);
#else
                        await file.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
#endif
                        await file.FlushAsync().ConfigureAwait(false);
                    })
                    .ConfigureAwait(false);
            }
        }
    }
}
