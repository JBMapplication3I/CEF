// <copyright file="SendFinalStatementsTask.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the send final statements task class</summary>
// ReSharper disable ExpressionIsAlwaysNull
namespace Clarity.Ecommerce.Tasks.SendFinalStatementWithFinalPaymentReminderAndPdfInsert
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.Models;

    /// <summary>The process sends final statement with final payment reminder and PDF inserts.</summary>
    /// <seealso cref="BaseTask"/>
    public class SendFinalStatementsTask : BaseTask
    {
        /// <inheritdoc/>
        public override async Task ProcessAsync(IJobCancellationToken cancellationToken)
        {
            if (GetActiveTaskJobsCount(null) > 1)
            {
                return;
            }
            cancellationToken?.ThrowIfCancellationRequested();
            string contextProfileName = null;
            await Logger.LogInformationAsync(
                ConfigurationKey,
                "Process sends final statement with final payment reminder and PDF inserts Scheduled Task: Starting",
                contextProfileName).ConfigureAwait(false);
            // Start a process
            var result = ProcessFinalPaymentsSending(cancellationToken, contextProfileName);
            // The result is whether the task queue accepted all the emails to batch out
            if (!result)
            {
                var ex = new JobFailedException(
                    "Process sends final statement with final payment reminder and PDF inserts Scheduled Task: Unable to queue notifications");
                await Logger.LogErrorAsync(
                    ConfigurationKey, ex.Message, ex, null).ConfigureAwait(false);
                throw ex;
            }
            await Logger.LogInformationAsync(
                ConfigurationKey,
                "Process sends final statement with final payment reminder and PDF inserts Scheduled Task: Finished",
                contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "0 0 1/28 * *");
            // once in 4 weeks (technically, the 28th of every month, but close enough
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp, out string description)
        {
            description = "Once in a 4 weeks sends final statement with final payment reminder and PDF inserts, queue an email.";
            return new List<IScheduledJobConfigurationSettingModel>
            {
                CreateSetting(
                    timestamp,
                    "Process sends final statement with final payment reminder and PDF inserts: Cron Schedule",
                    "0 0 1/28 * *"), // once in 4 weeks (technically, the 28th of every month, but close enough
            };
        }

        /// <summary>Process Final Payments Sending.</summary>
        /// <param name="cancellationToken"> The cancellation token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        // ReSharper disable once UnusedParameter.Local
        private static bool ProcessFinalPaymentsSending(IJobCancellationToken cancellationToken, string contextProfileName)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            throw new NotImplementedException();
            ////// get email queue
            ////var emailQueueWorkflow = RegistryLoaderWrapper.GetInstance<IEmailQueueWorkflow>();
            ////var salesInvoicesWorkflow = RegistryLoaderWrapper.GetInstance<ISalesInvoiceWorkflow>();
            ////var saleInvoices = salesInvoicesWorkflow.GetSalesInvoicesForFinalPaymentReminder(contextProfileName);
            ////var emails = saleInvoices.Select(so => so.BillingContact.Email1).ToArray();
            ////return true;
        }
    }
}
