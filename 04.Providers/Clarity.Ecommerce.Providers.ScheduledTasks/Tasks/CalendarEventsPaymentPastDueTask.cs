// <copyright file="CalendarEventsPaymentPastDueTask.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar events payment past due task class</summary>
namespace Clarity.Ecommerce.Tasks.Surveys.ForCalendarEvents
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;
    using Providers.Emails;

    /// <summary>A payment past due task.</summary>
    /// <seealso cref="TaskBase"/>
    public class CalendarEventsPaymentPastDueTask : TaskBase
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
            await FinalPaymentPastDueAsync(contextProfileName: null).ConfigureAwait(false);
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
            description = "Performs checks to send emails for unpaid invoices";
            return new()
            {
                // Every day at midnight
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 0 * * *", null),
            };
        }

        /// <summary>Final payment past due.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task FinalPaymentPastDueAsync(string? contextProfileName)
        {
            var invoiceQueriesProvider = RegistryLoaderWrapper.GetSalesInvoiceQueriesProvider(contextProfileName)!;
            // Get event keys where event start date is: StartDate - 90 days < Today < Start Date
            foreach (var eventKey in await Workflows.CalendarEvents.GetEventKeysInLastStatementStateAsync(
                    days: 90,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false))
            {
                // Get invoices from user's who bought the event and the invoice is not marked as paid
                var invoices = await invoiceQueriesProvider.GetRecordsPerEventAndNotStatusAsync(
                        eventKey: eventKey,
                        statusKey: "Paid",
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                foreach (var invoice in invoices)
                {
                    // Send email to user per invoice
                    await new SalesInvoicesPaymentPastDueNotificationToCustomerEmail().QueueAsync(
                            contextProfileName,
                            invoice.User!.Email,
                            new() { ["invoice"] = invoice })
                        .ConfigureAwait(false);
                }
            }
        }
    }
}
