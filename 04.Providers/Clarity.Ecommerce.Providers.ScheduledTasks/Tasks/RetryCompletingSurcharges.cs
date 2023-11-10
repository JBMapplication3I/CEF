// <copyright file="RetryCompletingSurcharges.cs" company="clarity-ventures.com">
// Copyright (c) clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the update translations files task class</summary>
#nullable enable
namespace Clarity.Ecommerce.Tasks.UpdateTranslationsFiles
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;
    using Interfaces.Providers.Surcharges;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>An index elastic search products.</summary>
    /// <seealso cref="TaskBase"/>
    public class RetryCompletingSurcharges : TaskBase
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
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var surchargeProvider = RegistryLoaderWrapper.GetSurchargeProvider(contextProfileName);
            if (surchargeProvider is null)
            {
                throw new InvalidOperationException("This task requires a valid surcharge provider");
            }
            var distinctProviderKeys = (await context.SalesInvoices
                .AsNoTracking()
                .Where(i => i.JsonAttributes != null && i.JsonAttributes.Contains("\"MissingCompletion:"))
                .Select(i => new { i.ID, i.JsonAttributes })
                .ToListAsync())
                .SelectMany(i => i.JsonAttributes
                    .DeserializeAttributesDictionary()
                    .Where(a => a.Key.StartsWith("MissingCompletion:"))
                    .Select(a => (key: a.Key.Split(new[] { ':' }, 2).ElementAtOrDefault(1), val: a.Value.Value))
                    .Where(a => Contract.CheckValidKey(a.key))
                    .Select(k => new
                    {
                        i.ID,
                        ProviderKey = k.key,
                        Descriptor = JsonConvert.DeserializeObject<SurchargeDescriptor>(
                            k.val,
                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects, }),
                    }))
                .ToLookup(i => i.ProviderKey);
            foreach (var keySet in distinctProviderKeys)
            {
                var descriptor = keySet.First().Descriptor; // Representative descriptor. It's assumed they should all be the same.
                var invoiceIDs = keySet.Select(i => i.ID).ToHashSet();
                try
                {
                    await surchargeProvider.MarkCompleteAsync(descriptor, mayThrow: true, contextProfileName);
                }
                catch (Exception ex)
                {
                    await Logger.LogErrorAsync("Re-failed to mark surcharge complete. Retrying again later.", keySet.Key + ": " + ex.Message + "\n" + ex.StackTrace, contextProfileName).ConfigureAwait(false);
                    continue;
                }
                var invoices = await context.SalesInvoices.Where(i => invoiceIDs.Contains(i.ID)).ToListAsync();
                foreach (var invoice in invoices)
                {
                    var attributes = invoice.SerializableAttributes;
                    _ = attributes.TryRemove($"MissingCompletion:{keySet.Key}", out var _);
                    invoice.JsonAttributes = attributes.SerializeAttributesDictionary();
                }
                await context.SaveChangesAsync();
            }
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Finished", contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(DateTime timestamp, out string description)
        {
            description = "Complete failed surcharge completions";
            var ts = DateTime.Now;
            return new()
            {
                // Every 12 hours
                CreateCronScheduleSetting(ts, "0 0/12 * * *", contextProfileName: null),
            };
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string? contextProfileName)
        {
            // Every twelve hours
            return LoadSettingsAsync(contextProfileName, "0 0/12 * * * *");
        }
    }
}
