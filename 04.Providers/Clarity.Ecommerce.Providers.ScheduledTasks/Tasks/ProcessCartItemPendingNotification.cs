// <copyright file="ProcessCartItemPendingNotification.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the process cart item pending notification class</summary>
// ReSharper disable ExpressionIsAlwaysNull
namespace Clarity.Ecommerce.Tasks.PendingCarts
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;
    using Models;
    using Providers.Emails;
    using Tasks;
    using Utilities;

    /// <summary>A pending carts notifications task.</summary>
    /// <seealso cref="TaskBase"/>
    public class PendingCartsNotificationsTask : TaskBase
    {
        /// <summary>Gets or sets the pending threshold.</summary>
        /// <value>The pending threshold.</value>
        private string PendingThreshold { get; set; } = null!;

        /// <summary>Gets or sets the product and email address hash.</summary>
        /// <value>The product and email address hash.</value>
        private Dictionary<int, List<string[]>> ProductAndEmailAddressHash { get; set; } = null!;

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
            var result = await ProcessCartAuditToNotifyCustomerForPendingOrderAsync(
                    int.Parse(PendingThreshold),
                    cancellationToken,
                    contextProfileName)
                .ConfigureAwait(false);
            // The result is whether the task queue accepted all the emails to batch out
            if (!result.ActionSucceeded)
            {
                var ex = new JobFailedException($"Process {ConfigurationKey} Scheduled Task: Unable to queue notifications");
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex.Message, ex, contextProfileName).ConfigureAwait(false);
                throw ex;
            }
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey} Scheduled Task: Finished", contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task LoadSettingsAsync(string? contextProfileName)
        {
            await LoadSettingsAsync(contextProfileName, "0 0 * * *").ConfigureAwait(false);
            PendingThreshold = (await GetSettingValueOrCreateDefaultAsync(
                    Configuration.ScheduledJobConfigurationSettings!,
                    $"Process {ConfigurationKey}: Pending Threshold (days)",
                    "15",
                    contextProfileName)
                .ConfigureAwait(false))!;
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp,
            out string description)
        {
            description = "When a product is in cart without order for more than {x} days, sends notification to customer about it.";
            return new()
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 0 * * *", null), // Daily at midnight
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Pending Threshold (days)", "15", null), // After 15 days since last updated
            };
        }

        /// <summary>Process cart audit to check cart items are in cart for defined time period without order and send
        /// notification to customer.</summary>
        /// <param name="pendingThreshold">  The pending threshold.</param>
        /// <param name="cancellationToken"> The cancellation token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private async Task<CEFActionResponse> ProcessCartAuditToNotifyCustomerForPendingOrderAsync(
            int pendingThreshold,
            IJobCancellationToken? cancellationToken,
            string? contextProfileName)
        {
            ProductAndEmailAddressHash = new();
            var cartSearchModel = RegistryLoaderWrapper.GetInstance<ICartSearchModel>(contextProfileName);
            ////cartSearchModel.TypeName = "Notify Me When Removed From Cart";
            var timestamp = DateExtensions.GenDateTime;
            var results = (await Workflows.Carts.SearchAsync(cartSearchModel, true, contextProfileName).ConfigureAwait(false)).results;
            foreach (var cart in results)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                var thisCart = await Workflows.Carts.StaticGetAsync(
                        lookupKey: CartByIDLookupKey.FromCart(cart),
                        pricingFactoryContext: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Cart must have a user on it with an email address
                if (string.IsNullOrWhiteSpace(thisCart!.UserContactEmail))
                {
                    continue;
                }
                foreach (var item in thisCart.SalesItems!)
                {
                    cancellationToken?.ThrowIfCancellationRequested();
                    // Checks if expired
                    if (!item.ProductID.HasValue
                        || ((item.UpdatedDate ?? item.CreatedDate) - timestamp).TotalDays < pendingThreshold)
                    {
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
                    ProductAndEmailAddressHash[productID].Add(new[]
                    {
                        thisCart.ID.ToString(),
                        thisCart.UserContactEmail!,
                    });
                }
            }
            foreach (var kvp in ProductAndEmailAddressHash)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var productInCart = context.CartItems
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterCartItemsByProductID(kvp.Key)
                    .FilterCartItemsByHasQuantity()
                    .Any(x => ((x.UpdatedDate ?? x.CreatedDate) - timestamp).TotalDays >= pendingThreshold);
                if (!productInCart)
                {
                    continue;
                }
                foreach (var email in kvp.Value)
                {
                    // Queues the object to be sent, can read the result for a failure to queue object
                    var product = await context.Products
                        .AsNoTracking()
                        .FilterByID(kvp.Key)
                        .Select(x => new { x.Name, x.SeoUrl })
                        .SingleAsync()
                        .ConfigureAwait(false);
                    var result = await new ProductInCartNotificationToCustomerEmail().QueueAsync(
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
                            name: "Scheduler." + ConfigurationKey,
                            message: $"Queued Email to {email.Aggregate((c, n) => c + ", " + n)} regarding product in cart {kvp.Key}",
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }
    }
}
