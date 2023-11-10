// <copyright file="ProcessDelayedImportsTask.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the process delayed imports task class</summary>
namespace Clarity.Ecommerce.Tasks.DelayedImports
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Models.Import;
    using Interfaces.Providers.Files;
    using Interfaces.Providers.Importer;
    using Models;
    using ServiceStack;
    using Utilities;

    /// <summary>The process delayed imports.</summary>
    /// <seealso cref="BaseTask"/>
    public class ProcessDelayedImportsTask : BaseTask
    {
        /// <summary>Gets or sets the maximum attempts.</summary>
        /// <value>The maximum attempts.</value>
        private int MaxAttempts { get; set; }

        /// <inheritdoc/>
        public override async Task ProcessAsync(IJobCancellationToken cancellationToken)
        {
            if (GetActiveTaskJobsCount(null) > 1)
            {
                return;
            }
            cancellationToken?.ThrowIfCancellationRequested();
            await Logger.LogInformationAsync(ConfigurationKey, $"Process {ConfigurationKey}: Starting", contextProfileName: null).ConfigureAwait(false);
            // Start a batch
            var result = await ProcessAnyDelayedImportsAsync(cancellationToken, contextProfileName: null).ConfigureAwait(false);
            // The result is whether the task queue accepted all the emails to batch out
            if (!result)
            {
                var ex = new JobFailedException($"Process {ConfigurationKey}: Unable to fill task queue with any imports");
                await Logger.LogErrorAsync(ConfigurationKey, ex.Message, ex, null).ConfigureAwait(false);
                throw ex;
            }
            await Logger.LogInformationAsync(ConfigurationKey, $"Process {ConfigurationKey}: Finished", contextProfileName: null).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task LoadSettingsAsync(string contextProfileName)
        {
            await LoadSettingsAsync(contextProfileName, "0 0 * * *").ConfigureAwait(false); // Every day at midnight
            MaxAttempts = int.Parse(await GetSettingValueOrCreateDefaultAsync(
                Configuration.ScheduledJobConfigurationSettings,
                $"Process {ConfigurationKey}: Import Max Retry Attempts",
                @"10",
                contextProfileName).ConfigureAwait(false));
            if (MaxAttempts <= 0 || MaxAttempts > 100)
            {
                var ex = new ArgumentOutOfRangeException(
                    nameof(MaxAttempts),
                    "Max Retry Attempts must be greater than 0 and less than or equal to 100",
                    "10");
                await Logger.LogErrorAsync(ConfigurationKey, ex.Message, ex, null).ConfigureAwait(false);
                throw ex;
            }
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(
            DateTime timestamp, out string description)
        {
            description = "Processes product imports which have been delayed.";
            return new List<IScheduledJobConfigurationSettingModel>
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 0 * * *"), // Every day at midnight
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Import Max Retry Attempts", "10"),
            };
        }

        /// <summary>Executes the import operation.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <returns>A CEFActionResponse.</returns>
        private static async Task<CEFActionResponse> RunImportAsync(string fileName)
        {
            Contract.RequiresValidKey(fileName, "File name needs to be specified");
            var provider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName: null);
            var rootPath = await provider.GetFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType.ImportProduct).ConfigureAwait(false);
            var filePath = Path.Combine(rootPath, fileName);
            var stream = File.OpenRead(filePath);
            var spreadsheetModel = RegistryLoaderWrapper.GetInstance<ISpreadsheetImportModel>();
            spreadsheetModel.SpreadsheetStream = stream;
            var extension = fileName.GetExtension();
            IImporterProviderBase importer;
            switch (extension.ToLower())
            {
                case ".xls":
                {
                    spreadsheetModel.ImportType = Enums.ImportType.XLS;
                    importer = RegistryLoaderWrapper.GetSpecificImportProvider("Excel", contextProfileName: null);
                    break;
                }
                case ".xlsx":
                {
                    spreadsheetModel.ImportType = Enums.ImportType.XLSX;
                    importer = RegistryLoaderWrapper.GetSpecificImportProvider("Excel", contextProfileName: null);
                    break;
                }
                case ".csv":
                {
                    spreadsheetModel.ImportType = Enums.ImportType.XLSX;
                    importer = RegistryLoaderWrapper.GetSpecificImportProvider("CSV", contextProfileName: null);
                    break;
                }
                default:
                {
                    throw new InvalidOperationException("Extension not supported");
                }
            }
            Contract.RequiresNotNull(importer, "Could load Excel Sheet importer provider");
            return await importer.IntegrateProductsAsync(spreadsheetModel, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>Process any delayed imports.</summary>
        /// <param name="cancellationToken"> The cancellation token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private async Task<bool> ProcessAnyDelayedImportsAsync(IJobCancellationToken cancellationToken, string contextProfileName)
        {
            var pendingStatusID = await Workflows.FutureImportStatuses.ResolveWithAutoGenerateToIDAsync(null, "Pending", "Pending", "Pending", null, contextProfileName).ConfigureAwait(false);
            var retryingID = await Workflows.FutureImportStatuses.ResolveWithAutoGenerateToIDAsync(null, "Retrying", "Retrying", "Retrying", null, contextProfileName).ConfigureAwait(false);
            var retriesFailedStatusID = await Workflows.FutureImportStatuses.ResolveWithAutoGenerateToIDAsync(null, "Retries Failed", "Retries Failed", "Retries Failed", null, contextProfileName).ConfigureAwait(false);
            var doneStatusID = await Workflows.FutureImportStatuses.ResolveWithAutoGenerateToIDAsync(null, "Done", "Done", "Done", null, contextProfileName).ConfigureAwait(false);
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var importsToProcessFromQueue = context.FutureImports
                .Where(x => x.Active && x.StatusID == pendingStatusID && x.RunImportAt <= timestamp)
                .OrderBy(x => x.CreatedDate)
                .ToList();
            if (!importsToProcessFromQueue.Any())
            {
                return true;
            }
            var filesProvider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName);
            foreach (var import in importsToProcessFromQueue)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                await ProcessImportAsync(
                    import,
                    retriesFailedStatusID,
                    timestamp,
                    context,
                    retryingID,
                    pendingStatusID,
                    doneStatusID,
                    filesProvider,
                    contextProfileName).ConfigureAwait(false);
            }
            return true;
        }

        /// <summary>Process the import.</summary>
        /// <param name="import">               The import.</param>
        /// <param name="retriesFailedStatusID">Identifier for the retries failed status.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="context">              The context.</param>
        /// <param name="retryingID">           Identifier for the retrying.</param>
        /// <param name="pendingStatusID">      Identifier for the pending status.</param>
        /// <param name="doneStatusID">         Identifier for the done status.</param>
        /// <param name="filesProvider">        The files provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task ProcessImportAsync(
            IFutureImport import,
            int retriesFailedStatusID,
            DateTime timestamp,
            IClarityEcommerceEntities context,
            int retryingID,
            int pendingStatusID,
            int doneStatusID,
            IFilesProviderBase filesProvider,
            string contextProfileName)
        {
            if (import.Attempts > MaxAttempts)
            {
                // We tried too many times
                import.HasError = true;
                import.StatusID = retriesFailedStatusID;
                import.UpdatedDate = timestamp;
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                return;
            }
            // Try sending it
            import.Attempts++;
            import.HasError = false;
            import.StatusID = retryingID;
            try
            {
                var root = await filesProvider.GetFileSaveRootPathAsync(Enums.FileEntityType.ImportProduct).ConfigureAwait(false);
                if (root.EndsWith("/"))
                {
                    root = root.Substring(1, root.Length - 1);
                }
                var result = await RunImportAsync(root + import.FileName).ConfigureAwait(false);
                if (result.ActionSucceeded)
                {
                    // Success!
                    import.StatusID = doneStatusID;
                    import.HasError = false;
                }
                else
                {
                    // Failed!
                    import.StatusID = pendingStatusID;
                    import.HasError = true;
                    Logger.LogError(
                        "ProcessDelayedImport.Error",
                        $"There was an error importing {import.FileName}",
                        result.Messages.Aggregate((c, n) => $"{c}\r\n{n}"),
                        contextProfileName);
                }
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(ConfigurationKey, ex.Message, ex, contextProfileName).ConfigureAwait(false);
                import.StatusID = pendingStatusID;
                import.HasError = true;
            }
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
        }
    }
}
