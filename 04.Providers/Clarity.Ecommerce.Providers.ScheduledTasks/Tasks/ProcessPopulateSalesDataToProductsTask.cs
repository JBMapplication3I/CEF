// <copyright file="ProcessPopulateSalesDataToProductsTask.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the process populate sales data to products class</summary>
// ReSharper disable ExpressionIsAlwaysNull
namespace Clarity.Ecommerce.Tasks.PopulateSalesDataToProducts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;
    using Utilities;

    /// <summary>A Process Populate Sales Data to Products.</summary>
    public class ProcessPopulateSalesDataToProductsTask : TaskBase
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
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, "Process Populate Sales data to Products Scheduled Task: Starting", contextProfileName).ConfigureAwait(false);
            // Start a batch
            var result = DoProcessing(cancellationToken, contextProfileName);
            // The result is whether the task queue accepted all the emails to batch out
            if (!result)
            {
                var ex = new JobFailedException("Process Populate Sales data to Products Scheduled Task: Unable to fill task queue with product sales");
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex.Message, ex, null).ConfigureAwait(false);
                throw ex;
            }
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, "Process Populate Sales data to Products Scheduled Task: Finished", contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string? contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "0 3 * * *");
            // Nightly at 3am
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp, out string description)
        {
            description = "Processes sales data and puts it into the products for catalog sorting.";
            return new()
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 3 * * *", null), // Nightly at 3am
            };
        }

        /// <summary>Executes the processing operation.</summary>
        /// <param name="cancellationToken"> The cancellation token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private static bool DoProcessing(IJobCancellationToken? cancellationToken, string? contextProfileName)
        {
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var sales = context.SalesOrderItems
                    .Where(x => x.Active
                             && x.ProductID.HasValue
                             && x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m) > 0m)
                    .Select(x => new
                    {
                        x.ProductID,
                        Quantity = x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m),
                        SellingPrice = x.UnitSoldPrice ?? x.UnitCorePrice,
                    })
                    .GroupBy(x => x.ProductID, x => new { x.Quantity, x.SellingPrice })
                    .ToList();
                foreach (var sale in sales.ToList())
                {
                    cancellationToken?.ThrowIfCancellationRequested();
                    if (!sale.Any())
                    {
                        continue;
                    }
                    var product = context.Products.Single(x => x.ID == sale.Key);
                    product.TotalPurchasedQuantity = sale.Sum(x => x.Quantity);
                    product.TotalPurchasedAmount = sale.Sum(x => x.Quantity * x.SellingPrice);
                }
                context.SaveUnitOfWork();
            }
            return true;
        }
    }
}
