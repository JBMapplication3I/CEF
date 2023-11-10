// <copyright file="QueueSalesInvoicePaymentRemindersTask.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the queue sales invoice payment reminders task class</summary>
namespace Clarity.Ecommerce.Tasks.Surveys.ForCalendarEvents
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;
    using JSConfigs;
    using Providers.Emails;
    using Utilities;

    /// <summary>A payment past due task.</summary>
    /// <seealso cref="TaskBase"/>
    public class QueueSalesInvoicePaymentRemindersTask : TaskBase
    {
        /// <inheritdoc/>
        public override async Task ProcessAsync(IJobCancellationToken? cancellationToken)
        {
            if (GetActiveTaskJobsCount(null) > 1)
            {
                return;
            }
            cancellationToken?.ThrowIfCancellationRequested();
            await Logger.LogInformationAsync(
                    name: ConfigurationKey,
                    message: $"Process {ConfigurationKey}: Starting",
                    contextProfileName: null)
                .ConfigureAwait(false);
            await QueueRemindersAsync(contextProfileName: null).ConfigureAwait(false);
            await Logger.LogInformationAsync(
                    name: ConfigurationKey,
                    message: $"Process {ConfigurationKey}: Finished",
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string? contextProfileName)
        {
            // Every day at midnight
            return LoadSettingsAsync(contextProfileName, "0 0 * * *");
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp,
            out string description)
        {
            description = "Performs checks to send emails for unpaid invoices due in the future or passed due";
            return new()
            {
                // Every day at midnight
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 0 * * *", null),
            };
        }

        /// <summary>Queue reminders.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task QueueRemindersAsync(string? contextProfileName)
        {
            // Default value: new[] { -30 -14, -7, -3, -1, 1, 3, 7, 14, 30 }
            var reminderDayOffsets = CEFConfigDictionary.SalesInvoicesEmailsPaymentRemindersOccurAt;
            var today = DateTime.Today;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var ids = await context.SalesInvoices
                .AsNoTracking()
                .FilterByActive(true)
                .FilterSalesInvoicesByHasBalanceDue(true)
                .FilterSalesInvoicesByHasDueDate(true)
                .Select(x => new
                {
                    x.ID,
                    DueDate = x.DueDate!.Value,
                })
                .ToListAsync()
                .ConfigureAwait(false);
            await ids.ForEachAsync(
                    8,
                    async data =>
                    {
                        try
                        {
                            // Positive means it's in the future, negative means it's passed due
                            var daysDistanceFromDueDate = (today - data.DueDate).Days;
                            var isPastDue = daysDistanceFromDueDate < 0;
                            var isMultipleOfPassedDue = isPastDue
                                && Math.Abs(daysDistanceFromDueDate) % reminderDayOffsets.Last() == 0;
                            if (!reminderDayOffsets.Contains(daysDistanceFromDueDate)
                                && !isMultipleOfPassedDue)
                            {
                                // Not a date which should send an email, exit
                                return;
                            }
                            // Get the invoice
                            var invoice = Contract.RequiresNotNull(
                                await Workflows.SalesInvoices.GetAsync(
                                        data.ID,
                                        contextProfileName)
                                    .ConfigureAwait(false));
                            if (!Contract.CheckValidKey(invoice.User?.Email))
                            {
                                // No place to send the email to
                                return;
                            }
                            if (isPastDue)
                            {
                                await new SalesInvoicesPaymentPastDueNotificationToCustomerEmail().QueueAsync(
                                        contextProfileName,
                                        invoice.User!.Email,
                                        new()
                                        {
                                            ["invoice"] = invoice,
                                            ["daysDistanceFromDueDate"] = daysDistanceFromDueDate,
                                        })
                                    .ConfigureAwait(false);
                                return;
                            }
                            await new SalesInvoicesPaymentDueSoonNotificationToCustomerEmail().QueueAsync(
                                    contextProfileName,
                                    invoice.User!.Email,
                                    new()
                                    {
                                        ["invoice"] = invoice,
                                        ["daysDistanceFromDueDate"] = daysDistanceFromDueDate,
                                    })
                                .ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            await Logger.LogErrorAsync(
                                    name: $"{ConfigurationKey}.{nameof(QueueRemindersAsync)}.{ex.GetType()}",
                                    message: ex.Message,
                                    ex: ex,
                                    contextProfileName: contextProfileName)
                                .ConfigureAwait(false);
                        }
                    })
                .ConfigureAwait(false);
        }
    }
}
