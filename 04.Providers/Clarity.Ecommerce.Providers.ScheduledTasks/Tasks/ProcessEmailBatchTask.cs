// <copyright file="ProcessEmailBatchTask.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the process email batch task class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Tasks.EmailBatches
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Files;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>The process email batch.</summary>
    /// <seealso cref="TaskBase"/>
    public class ProcessEmailBatchTask : TaskBase
    {
        /// <summary>Gets or sets the size of the batch.</summary>
        /// <value>The size of the batch.</value>
        private int BatchSize { get; set; }

        /// <summary>Gets or sets the maximum attempts.</summary>
        /// <value>The maximum attempts.</value>
        private int MaxAttempts { get; set; }

        /// <inheritdoc/>
        public override async Task ProcessAsync(IJobCancellationToken? cancellationToken)
        {
            if (GetActiveTaskJobsCount(null) > 1)
            {
                return;
            }
            cancellationToken?.ThrowIfCancellationRequested();
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, "Process an Email Batch Scheduled Task: Starting", contextProfileName: null).ConfigureAwait(false);
            // Start a batch
            var result = await ProcessAnEmailBatchAsync(BatchSize, null, cancellationToken).ConfigureAwait(false);
            // The result is whether the task queue accepted all the emails to batch out
            if (!result)
            {
                var ex = new JobFailedException("Process an Email Batch Scheduled Task: Unable to fill task queue with a batch of emails");
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex.Message, ex, null).ConfigureAwait(false);
                throw ex;
            }
            await Logger.LogInformationAsync("Scheduler." + ConfigurationKey, "Process an Email Batch Scheduled Task: Finished", contextProfileName: null).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task LoadSettingsAsync(string? contextProfileName)
        {
            await LoadSettingsAsync(contextProfileName, "*/1 * * * *").ConfigureAwait(false); // Every minute
            var sizeSetting = await GetSettingValueOrCreateDefaultAsync(
                    settings: Configuration.ScheduledJobConfigurationSettings!,
                    name: $"Process {ConfigurationKey}: Email Batch Size",
                    defaultValue: "100",
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (sizeSetting == null)
            {
                Logger.LogWarning(
                    name: "Scheduler." + ConfigurationKey,
                    message: $"Process {ConfigurationKey}: Email Batch Size was not set up for this task, please set this setting. The Default value of '100' has been applied",
                    contextProfileName: contextProfileName);
            }
            BatchSize = int.Parse(sizeSetting ?? "100");
            if (BatchSize is <= 0 or > 10000)
            {
                var ex = new ArgumentOutOfRangeException(nameof(BatchSize), "Batch Size must be greater than 0 and less than or equal to 10000");
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex.Message, ex, null).ConfigureAwait(false);
                throw ex;
            }
            MaxAttempts = int.Parse(
                (await GetSettingValueOrCreateDefaultAsync(
                    settings: Configuration.ScheduledJobConfigurationSettings!,
                    name: $"Process {ConfigurationKey}: Email Max Retry Attempts",
                    defaultValue: "10",
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false))!);
            if (MaxAttempts is <= 0 or > 100)
            {
                var ex = new ArgumentOutOfRangeException(nameof(MaxAttempts), "Max Retry Attempts must be greater than 0 and less than or equal to 100", "10");
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex.Message, ex, null).ConfigureAwait(false);
                throw ex;
            }
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp,
            out string description)
        {
            description = "Processes emails in batches.";
            return new()
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "*/1 * * * *", null), // Every Minute
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Email Batch Size", "100", null),
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Email Max Retry Attempts", "10", null),
            };
        }

        /// <summary>SMTP client send completed.</summary>
        /// <param name="_">      Source of the event.</param>
        /// <param name="e">      Asynchronous completed event information.</param>
        /// <param name="emailID">Identifier for the email.</param>
        // ReSharper disable once UnusedParameter.Local
        private static void SmtpClientSendCompleted(object _, System.ComponentModel.AsyncCompletedEventArgs e, int emailID)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            var email = context.EmailQueues.Single(x => x.ID == emailID);
            var pendingStatusID = context.EmailStatuses.Where(x => x.Active && x.CustomKey == "Pending").Select(x => (int?)x.ID).SingleOrDefault() ?? 1;
            var sentStatusID = context.EmailStatuses.Where(x => x.Active && x.CustomKey == "Sent").Select(x => (int?)x.ID).SingleOrDefault() ?? 4;
            var cancelledStatusID = context.EmailStatuses.Where(x => x.Active && x.CustomKey == "Cancelled").Select(x => (int?)x.ID).SingleOrDefault() ?? 5;
            if (e.Cancelled)
            {
                email.StatusID = cancelledStatusID;
                email.HasError = true;
            }
            else if (e.Error != null)
            {
                email.StatusID = pendingStatusID;
                email.HasError = true;
            }
            else
            {
                // Success!
                email.StatusID = sentStatusID;
                email.HasError = false;
            }
            var savedSuccessfully = false;
            var start = DateTime.Now;
            var timeoutAt = start.AddSeconds(60);
            while (!savedSuccessfully && DateTime.Now < timeoutAt)
            {
                try
                {
                    context.SaveUnitOfWork();
                    savedSuccessfully = true;
                }
                catch
                {
                    System.Threading.Thread.SpinWait(100);
                }
            }
        }

        /// <summary>Process an email batch.</summary>
        /// <param name="batchSize">         The size of the batch.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="cancellationToken"> The cancellation token.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        private async Task<bool> ProcessAnEmailBatchAsync(int batchSize, string? contextProfileName, IJobCancellationToken? cancellationToken)
        {
            var pendingStatusID = await Workflows.EmailStatuses.ResolveWithAutoGenerateToIDAsync(null, "Pending", "Pending", "Pending", null, contextProfileName).ConfigureAwait(false);
            var retryingID = await Workflows.EmailStatuses.ResolveWithAutoGenerateToIDAsync(null, "Retrying", "Retrying", "Retrying", null, contextProfileName).ConfigureAwait(false);
            var retriesFailedStatusID = await Workflows.EmailStatuses.ResolveWithAutoGenerateToIDAsync(null, "Retries Failed", "Retries Failed", "Retries Failed", null, contextProfileName).ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var emailsToProcessFromQueue = await context.EmailQueues
                .Where(x => x.Active && x.StatusID == pendingStatusID)
                .OrderBy(x => x.CreatedDate)
                .Take(batchSize)
                .ToListAsync()
                .ConfigureAwait(false);
            if (emailsToProcessFromQueue.Count == 0)
            {
                return true;
            }
            var timestamp = DateExtensions.GenDateTime;
            var filesProvider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName);
            foreach (var email in emailsToProcessFromQueue)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                await ProcessEmailAsync(
                    cancellationToken,
                    email,
                    retriesFailedStatusID,
                    timestamp,
                    context,
                    retryingID,
                    pendingStatusID,
                    filesProvider!,
                    contextProfileName).ConfigureAwait(false);
            }
            return true;
        }

        /// <summary>Process the email.</summary>
        /// <param name="cancellationToken">    The cancellation token.</param>
        /// <param name="email">                The email.</param>
        /// <param name="retriesFailedStatusID">Identifier for the retries failed status.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="context">              The context.</param>
        /// <param name="retryingID">           Identifier for the retrying.</param>
        /// <param name="pendingStatusID">      Identifier for the pending status.</param>
        /// <param name="filesProvider">        The files provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task ProcessEmailAsync(
            IJobCancellationToken? cancellationToken,
            IEmailQueue email,
            int retriesFailedStatusID,
            DateTime timestamp,
            IClarityEcommerceEntities context,
            int retryingID,
            int pendingStatusID,
            IFilesProviderBase filesProvider,
            string? contextProfileName)
        {
            if (email.Attempts > MaxAttempts)
            {
                // We tried too many times
                email.HasError = true;
                email.StatusID = retriesFailedStatusID;
                email.UpdatedDate = timestamp;
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                return;
            }
            // Try sending it
            email.Attempts++;
            email.HasError = false;
            email.StatusID = retryingID;
            var root = await filesProvider.GetFileSaveRootPathAsync(fileEntityType: null).ConfigureAwait(false);
            if (root.EndsWith("/"))
            {
                root = root[1..];
            }
            try
            {
                var message = new MailMessage(email.AddressFrom!, email.AddressesTo!, email.Subject, email.Body) { IsBodyHtml = email.IsHtml };
                if (email.EmailQueueAttachments?.Count > 0)
                {
                    foreach (var attachment in email.EmailQueueAttachments)
                    {
                        message.Attachments.Add(new(attachment.Name!));
                    }
                }
                if (!string.IsNullOrWhiteSpace(email.AddressesCc))
                {
                    message.CC.Add(email.AddressesCc!);
                }
                if (!string.IsNullOrWhiteSpace(email.AddressesBcc))
                {
                    message.Bcc.Add(email.AddressesBcc!);
                }
                if (email.SerializableAttributes?.ContainsKey("AttachmentPath") == true)
                {
                    foreach (var filePath in email.SerializableAttributes["AttachmentPath"].Value.Split(';'))
                    {
                        var fullPath = Path.Combine(root, filePath.Replace("/", @"\"));
                        var attachment = new Attachment(fullPath);
                        var disposition = attachment.ContentDisposition!;
                        disposition.CreationDate = File.GetCreationTime(fullPath);
                        disposition.ModificationDate = File.GetLastWriteTime(fullPath);
                        disposition.ReadDate = File.GetLastAccessTime(fullPath);
                        disposition.FileName = Path.GetFileName(fullPath);
                        disposition.Size = new FileInfo(fullPath).Length;
                        disposition.DispositionType = DispositionTypeNames.Attachment;
                        message.Attachments.Add(attachment);
                    }
                }
                // Puts it into the thread Task Queue so we're even less blocking, and so we can pass in the email.ID manually
                var emailID = email.ID;
                System.ComponentModel.AsyncCompletedEventArgs? E = null;
                object? S = null;
                await Task.Run(() =>
                {
                    cancellationToken?.ThrowIfCancellationRequested();
                    var client = new SmtpClient();
                    client.SendCompleted += (s, e) =>
                    {
                        SmtpClientSendCompleted(s, e, emailID);
                        E = e;
                        S = s;
                        client.Dispose();
                        message.Dispose();
                    };
                    client.SendMailAsync(message);
                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex.Message, ex, contextProfileName).ConfigureAwait(false);
                email.StatusID = pendingStatusID;
                email.HasError = true;
            }
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
        }
    }

    /// <summary>A run email batch manually.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate,
     UsedInAdmin,
     Route("/Tasks/Messaging/EmailQueue/RunBatchManually", "POST",
        Summary = "Use to trigger an email batch manually, separately from the Scheduler")]
    public class RunEmailBatchManually : IReturn<CEFActionResponse>
    {
    }

    /// <summary>The process email batch service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [PublicAPI]
    public class ProcessEmailBatchService : ClarityEcommerceServiceBase
    {
        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>The result that yields an object.</returns>
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public async Task<object> Post(RunEmailBatchManually request)
        {
            try
            {
                await new ProcessEmailBatchTask().ProcessAsync(null).ConfigureAwait(false);
                return CEFAR.PassingCEFAR();
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(nameof(RunEmailBatchManually), ex.Message, ex, contextProfileName: null).ConfigureAwait(false);
                return CEFAR.FailingCEFAR("ERROR! There was an exception processing emails.");
            }
        }
    }

    /// <inheritdoc/>
    [PublicAPI]
    public class ProcessEmailBatchFeature : IPlugin
    {
        /// <summary>Registers this object.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
