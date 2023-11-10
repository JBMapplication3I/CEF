// <copyright file="RemoveDiscontinuedItemsFromCartsTask.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the remove discontinued items from carts task class</summary>
// ReSharper disable ExpressionIsAlwaysNull
namespace Clarity.Ecommerce.Tasks
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
    using Utilities;

    /// <summary>A remove discontinued items from carts task.</summary>
    /// <seealso cref="TaskBase"/>
    public class RemoveDiscontinuedItemsFromCartsTask : TaskBase
    {
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
            var result = await ProcessCartAuditAsync(cancellationToken, contextProfileName).ConfigureAwait(false);
            // The result is whether the task queue accepted all the emails to batch out
            if (!result.ActionSucceeded)
            {
                var ex = new JobFailedException("Cart Audit Workflow Removes Discontinued Products: Unable to queue notifications");
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex.Message, ex, contextProfileName).ConfigureAwait(false);
                throw ex;
            }
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey} Scheduled Task: Finished", contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string? contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "0 * * * *");
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp,
            out string description)
        {
            description = "When a product changes to discontinued, remove the product from cart and notify the customer.";
            return new()
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 * * * *", null), // Every hour
            };
        }

        /// <summary>Process the discontinued product removal from cart.</summary>
        /// <param name="cancellationToken"> The cancellation token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private async Task<CEFActionResponse> ProcessCartAuditAsync(
            IJobCancellationToken? cancellationToken,
            string? contextProfileName)
        {
            ProductAndEmailAddressHash = new();
            var cartSearchModel = RegistryLoaderWrapper.GetInstance<ICartSearchModel>(contextProfileName);
            ////cartSearchModel.TypeName = "Notify Me When Removed From Cart";
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
                    // Checks if product attribute isDiscontinued is not true for product
                    if (!item.ProductIsDiscontinued || !item.ProductID.HasValue)
                    {
                        continue;
                    }
                    if (item.ProductIsDiscontinued)
                    {
                        await Workflows.Carts.StaticRemoveAsync(
                                lookupKey: StaticCartLookupKey.FromCart(thisCart),
                                productID: item.ProductID.Value,
                                forceUniqueLineItemKey: item.ForceUniqueLineItemKey,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
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
                if (!context.Products.AsNoTracking().Any(x => x.ID == kvp.Key && x.IsDiscontinued))
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
                    var result = await new ProductRemovedFromCartNotificationToCustomerEmail().QueueAsync(
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
                                "Scheduler." + ConfigurationKey,
                                result.Messages.FirstOrDefault(),
                                new(result.Messages.DefaultIfEmpty(string.Empty).Aggregate((c, n) => c + "\r\n" + n)),
                                contextProfileName)
                            .ConfigureAwait(false);
                        continue;
                    }
                    await Logger.LogInformationAsync(
                            "Scheduler." + ConfigurationKey,
                            $"Queued Email to {email.Aggregate((c, n) => c + ", " + n)} regarding product ID {kvp.Key}",
                            contextProfileName)
                        .ConfigureAwait(false);
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }
    }
}
