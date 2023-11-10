// <copyright file="ProcessProductNowInStockNotificationsTask.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the process product now in stock notifications task class</summary>
// ReSharper disable ExpressionIsAlwaysNull
namespace Clarity.Ecommerce.Tasks.ProductNowInStockNotifications
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Providers.Emails;
    using Utilities;

    /// <summary>The process product now in stock notifications.</summary>
    /// <seealso cref="TaskBase"/>
    public class ProcessProductNowInStockNotificationsTask : TaskBase
    {
        /// <summary>Gets or sets the product and email address hash.</summary>
        /// <value>The product and email address hash.</value>
        private Dictionary<int, List<string[]>> ProductAndEmailAddressHash { get; set; } = null!;

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
            var result = await ProcessNotificationsAsync(cancellationToken, contextProfileName).ConfigureAwait(false);
            // The result is whether the task queue accepted all the emails to batch out
            if (!result)
            {
                var ex = new JobFailedException($"Process {ConfigurationKey}: Unable to queue notifications");
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex.Message, ex, null).ConfigureAwait(false);
                throw ex;
            }
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Finished", contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string? contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "0 * * * *");
            // Every hour
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp, out string description)
        {
            description = "When a product's inventory level changes to in stock, and the customer has requested to be notified, queue an email.";
            return new()
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 * * * *", null), // Every hour
            };
        }

        /// <summary>Process the notifications.</summary>
        /// <param name="cancellationToken"> The cancellation token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private async Task<bool> ProcessNotificationsAsync(IJobCancellationToken? cancellationToken, string? contextProfileName)
        {
            var provider = RegistryLoaderWrapper.GetInventoryProvider(contextProfileName);
            if (provider == null || provider.Name == "NoInventoryProvider")
            {
                return false;
            }
            ProductAndEmailAddressHash = new();
            var cartWorkflow = RegistryLoaderWrapper.GetInstance<ICartWorkflow>(contextProfileName);
            var cartSearchModel = RegistryLoaderWrapper.GetInstance<ICartSearchModel>(contextProfileName);
            cartSearchModel.TypeName = "Notify Me When In Stock";
            var results = (await cartWorkflow.SearchAsync(cartSearchModel, true, contextProfileName).ConfigureAwait(false)).results;
            foreach (var cart in results)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                var thisCart = await cartWorkflow.StaticGetAsync(
                        lookupKey: CartByIDLookupKey.FromCart(cart),
                        pricingFactoryContext: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(thisCart!.UserContactEmail))
                {
                    continue;
                } // Cart must have a user on it with an email address
                foreach (var item in thisCart.SalesItems!)
                {
                    cancellationToken?.ThrowIfCancellationRequested();
                    if (item.QuantityBackOrdered > 0 || !item.ProductID.HasValue)
                    {
                        // We'll track ones we sent using QuantityBackOrdered
                        continue;
                    }
                    var productID = item.ProductID.Value;
                    if (!ProductAndEmailAddressHash.ContainsKey(productID))
                    {
                        ProductAndEmailAddressHash[productID] = new();
                    }
                    if (ProductAndEmailAddressHash[productID].Any(x => x[1] == thisCart.UserContactEmail))
                    {
                        continue;
                    }
                    ProductAndEmailAddressHash[productID].Add(new[] { thisCart.ID.ToString(), thisCart.UserContactEmail! });
                }
            }
            foreach (var kvp in ProductAndEmailAddressHash)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                var inStock = await provider.CheckHasAnyAvailableInventoryAsync(
                        productID: kvp.Key,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (!inStock.ActionSucceeded || !inStock.Result)
                {
                    continue;
                }
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var product = await context.Products
                    .AsNoTracking()
                    .FilterByID(kvp.Key)
                    .Select(x => new { x.Name, x.SeoUrl })
                    .SingleAsync()
                    .ConfigureAwait(false);
                foreach (var email in kvp.Value)
                {
                    // Queues the object to be sent, can read the result for a failure to queue object
                    var result = await new ProductNowInStockNotificationToCustomerEmail().QueueAsync(
                            contextProfileName: contextProfileName,
                            to: email[1],
                            parameters: new()
                            {
                                ["productName"] = product.Name,
                                ["productSeoUrl"] = product.SeoUrl,
                            })
                        .ConfigureAwait(false);
                    if (!result.ActionSucceeded)
                    {
                        await Logger.LogErrorAsync(
                                name: "Scheduler." + ConfigurationKey,
                                message: result.Messages.FirstOrDefault(),
                                ex: new(result.Messages.DefaultIfEmpty(string.Empty).Aggregate((c, n) => c + "\r\n" + n)),
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        continue;
                    }
                    await Logger.LogInformationAsync(
                        "Scheduler." + ConfigurationKey,
                        $"Queued Email to {email.Aggregate((c, n) => c + ", " + n)} regarding product ID {kvp.Key}",
                        contextProfileName).ConfigureAwait(false);
                    var salesItems = await context.CartItems
                        .Where(x => x.Master!.Type!.Name == "Notify Me When In Stock"
                            && x.User != null
                            && email.Contains(x.User.Email)).ToListAsync().ConfigureAwait(false);
                    foreach (var salesItem in salesItems)
                    {
                        salesItem.QuantityBackOrdered = 1;
                    }
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
            return true;
        }
    }
}
