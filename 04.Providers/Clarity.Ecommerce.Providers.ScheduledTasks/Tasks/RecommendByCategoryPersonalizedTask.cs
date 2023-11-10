// <copyright file="RecommendByCategoryPersonalizedTask.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the recommend by category personalized task class</summary>
// ReSharper disable ExpressionIsAlwaysNull
namespace Clarity.Ecommerce.Tasks.Personalization.RecommendByCategory
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.DataModel;
    using Interfaces.Models;

    /// <summary>A recommend by category personalized.</summary>
    /// <seealso cref="TaskBase"/>
    public class RecommendByCategoryPersonalizedTask : TaskBase
    {
        private static string CompanyName { get; } = ConfigurationManager.AppSettings["Clarity.Emails.Defaults.CompanyName"]!;

        private static string From { get; } = ConfigurationManager.AppSettings["Clarity.Personalization.RecommendByCategory.From"]
            ?? ConfigurationManager.AppSettings["SystemValues.Emails.Defaults.From"]!;

        private static string CC { get; } = ConfigurationManager.AppSettings["Clarity.Personalization.RecommendByCategory.CC"]
            ?? ConfigurationManager.AppSettings["SystemValues.Emails.Defaults.CC"]!;

        private static string BCC { get; } = ConfigurationManager.AppSettings["Clarity.Personalization.RecommendByCategory.BCC"]
            ?? ConfigurationManager.AppSettings["SystemValues.Emails.Defaults.BCC"]!;

        private static string Subject { get; } = ConfigurationManager.AppSettings["Clarity.Personalization.RecommendByCategory.Subject"]
            ?? ConfigurationManager.AppSettings["SystemValues.Emails.Defaults.Subject"]!;

        private static string TemplatesRoot { get; } = ConfigurationManager.AppSettings["SystemValues.Emails.Defaults.TemplatesRoot"]!;

        private static string BodyTemplatePath { get; } = ConfigurationManager.AppSettings["Clarity.Personalization.RecommendByCategory.BodyTemplatePath"]!;

        private static string RepeatTemplatePath { get; } = ConfigurationManager.AppSettings["Clarity.Personalization.RecommendByCategory.RepeatTemplatePath"]!;

        private static string SiteRootUrl { get; } = ConfigurationManager.AppSettings["SystemValues.SiteRootUrl"]!;

        private static string ProductDetailUrlFragment { get; } = ConfigurationManager.AppSettings["SystemValues.ProductDetailUrlFragment"]!;

        private static string ProductImagesFolder { get; } = ConfigurationManager.AppSettings["Clarity.Uploads.Images.Product"]!; // Should be nested under their website root storage folder

        /// <inheritdoc/>
        public override async Task ProcessAsync(IJobCancellationToken? cancellationToken)
        {
            if (GetActiveTaskJobsCount(null) > 1)
            {
                /* TODO@JTG: Issue Cancel Job command */
                return;
            }
            cancellationToken?.ThrowIfCancellationRequested();
            string? contextProfileName = null;
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Starting", contextProfileName).ConfigureAwait(false);
            // Start a batch
            try
            {
                string mainTemplate, repeaterTemplate;
                using (var webClient = new WebClient())
                {
                    try
                    {
                        mainTemplate = webClient.DownloadString(SiteRootUrl + TemplatesRoot + BodyTemplatePath);
                        repeaterTemplate = webClient.DownloadString(SiteRootUrl + TemplatesRoot + RepeatTemplatePath);
                    }
                    catch
                    {
                        await Logger.LogErrorAsync(
                                "Scheduler." + ConfigurationKey,
                                $"Process {ConfigurationKey}: Unable to queue emails, bad template calls\r\n{SiteRootUrl}{TemplatesRoot}{BodyTemplatePath}\r\n{SiteRootUrl}{TemplatesRoot}{RepeatTemplatePath}",
                                contextProfileName)
                            .ConfigureAwait(false);
                        throw;
                    }
                }
                using var context = RegistryLoaderWrapper.GetContext(null);
                var productViewCounts = await context.PageViews
                    .Where(x => x.Active && x.ProductID.HasValue && x.Product!.Active && !x.Product.IsDiscontinued)
                    .GroupBy(x => x.ProductID!.Value)
                    .Select(x => new { x.Key, Count = x.Count() })
                    .ToDictionaryAsync(x => x.Key, x => x.Count)
                    .ConfigureAwait(false);
                if (!productViewCounts.Any())
                {
                    await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Quitting because there are no product view counts", contextProfileName).ConfigureAwait(false);
                    return;
                }
                var categoryProductsThatHaveBeenViewed = await context.PageViews
                    .Where(x => x.Active && x.ProductID.HasValue && x.Product!.Active && !x.Product.IsDiscontinued)
                    .Select(x => new { ProductID = x.ProductID!.Value, CategoryIDs = x.Product!.Categories!.Where(y => y.Active && y.Slave!.Active).Select(y => y.SlaveID) })
                    .SelectMany(x => x.CategoryIDs.Select(y => new { CategoryID = y, x.ProductID }))
                    .GroupBy(x => x.CategoryID).ToDictionaryAsync(x => x.Key, v => v.Select(k => k.ProductID).Distinct()).ConfigureAwait(false);
                if (!categoryProductsThatHaveBeenViewed.Any())
                {
                    await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Quitting because there are no category product view counts", contextProfileName).ConfigureAwait(false);
                    return;
                }
                var productsViewedByEachUser = await context.PageViews
                    .Where(x => x.Active
                        && x.ProductID.HasValue
                        && x.Product!.Active
                        && x.Product.IsDiscontinued
                        && x.UserID.HasValue
                        && x.User!.Active
                        && x.User.Email != null
                        && x.User.Email != string.Empty)
                    .GroupBy(x => x.UserID!.Value)
                    .ToDictionaryAsync(x => x.Key, x => x.Select(y => y.ProductID!.Value).Distinct())
                    .ConfigureAwait(false);
                if (!productsViewedByEachUser.Any())
                {
                    await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Quitting because there are no user product view counts", contextProfileName).ConfigureAwait(false);
                    return;
                }
                foreach (var userID in productsViewedByEachUser.Keys)
                {
                    var thisUsersCategories = new List<int>();
                    foreach (var productID in productsViewedByEachUser[userID])
                    {
                        thisUsersCategories.AddRange(categoryProductsThatHaveBeenViewed.Keys
                            .Where(categoryID => categoryProductsThatHaveBeenViewed[categoryID].Contains(productID)));
                    }
                    if (!thisUsersCategories.Any())
                    {
                        await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Skipping userID {userID} because there were no matching categories", contextProfileName).ConfigureAwait(false);
                        continue;
                    }
                    // Fill nine spots with the top viewed products from these categories
                    // by going through each category and taking the top 1 viewed from each,
                    // then the second from each, etc.
                    var productIDsToSendToUser = new List<int>();
                    var loopCounter = 0;
                    // Don't loop forever if there aren't enough products
                    while (productIDsToSendToUser.Count < 9 && loopCounter < 9)
                    {
                        ++loopCounter;
                        foreach (var categoryID in thisUsersCategories)
                        {
                            var categoryProducts = categoryProductsThatHaveBeenViewed[categoryID];
                            var toAdd = categoryProducts.OrderByDescending(x => productViewCounts[x]).Skip(loopCounter).Take(1).ToList();
                            if (toAdd.Any())
                            {
                                productIDsToSendToUser.AddRange(toAdd);
                            }
                        }
                    }
                    if (!thisUsersCategories.Any())
                    {
                        await Logger.LogInformationAsync(
                                name: "Scheduler." + ConfigurationKey,
                                message: $"Process {ConfigurationKey}: Skipping userID {userID} because there were no matching products for the categories we should send them",
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        continue;
                    }
                    var products = context.Products
                        .FilterByIDs(productIDsToSendToUser.ToArray())
                        .OrderBy(x => x.Name)
                        .Select(x => new
                        {
                            x.ID,
                            x.CustomKey,
                            x.Name,
                            x.SeoUrl,
                            PrimaryImageFileName = x.Images!
                                .Where(y => y.Active)
                                .OrderByDescending(y => y.IsPrimary)
                                .ThenByDescending(y => y.OriginalWidth)
                                .ThenByDescending(y => y.OriginalHeight)
                                .Select(y => y.OriginalFileName)
                                .Take(1)
                                .FirstOrDefault(),
                        })
                        .Take(9);
                    var user = context.Users.Single(x => x.ID == userID);
                    var messageBody = PopulateStaticReplacements(PopulateUserReplacements(
                        mainTemplate.Replace(
                            "{{ProductsBodyRepeat}}",
                            (await products.ToListAsync().ConfigureAwait(false)).Select(x => PopulateProductReplacements(repeaterTemplate, x)).Aggregate((c, n) => c + "\r\n" + n)),
                        user));
                    var emailModel = RegistryLoaderWrapper.GetInstance<IEmailQueueModel>(contextProfileName);
                    emailModel.Active = true;
                    emailModel.CreatedDate = DateExtensions.GenDateTime;
                    emailModel.AddressFrom = From;
                    emailModel.AddressesTo = await context.Users.Where(x => x.ID == userID).Select(x => x.Email).SingleAsync();
                    emailModel.AddressesCc = CC;
                    emailModel.AddressesBcc = BCC;
                    emailModel.Subject = Subject.Replace("{{CompanyName}}", CompanyName);
                    emailModel.Body = messageBody;
                    emailModel.IsHtml = true;
                    emailModel.Attempts = 0;
                    emailModel.StatusKey = "Pending";
                    emailModel.TypeKey = "General";
                    await Workflows.EmailQueues.AddEmailToQueueAsync(emailModel, null).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                var ex2 = new JobFailedException($"Process {ConfigurationKey}: Unable to queue emails", ex);
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex2.Message, ex2, null).ConfigureAwait(false);
                throw ex2;
            }
            await Logger.LogInformationAsync(
                    name: "Scheduler." + ConfigurationKey,
                    message: $"Process {ConfigurationKey}: Finished",
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string? contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "0 4 * * *");
            // Nightly at 4am
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp,
            out string description)
        {
            description = "Runs a pass to send emails to customers based on personalized data.";
            return new()
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 4 * * *", null), // Nightly at 4am
            };
        }

        private static string PopulateStaticReplacements(string template)
        {
            var retVal = template;
            retVal = retVal.Replace("{{SiteRootUrl}}", SiteRootUrl);
            retVal = retVal.Replace("{{ProductDetailUrlFragment}}", ProductDetailUrlFragment);
            retVal = retVal.Replace("{{ProductImagesPath}}", ProductImagesFolder.Replace("\\", "/").TrimStart('/').TrimEnd('/'));
            return retVal;
        }

        private static string PopulateUserReplacements(string template, IHaveAContactBase user)
        {
            var retVal = template;
            retVal = retVal.Replace("{{FirstName}}", user.Contact!.FirstName ?? user.Contact.FullName);
            return retVal;
        }

        private static string PopulateProductReplacements(string template, dynamic product)
        {
            var retVal = template;
            retVal = retVal.Replace("{{SeoUrl}}", product.SeoUrl);
            retVal = retVal.Replace("{{CustomKey}}", product.CustomKey);
            retVal = retVal.Replace("{{ProductName}}", product.Name);
            retVal = retVal.Replace("{{PrimaryImageFileName}}", product.PrimaryImageFileName ?? "placeholder.jpg");
            return retVal;
        }
    }
}
