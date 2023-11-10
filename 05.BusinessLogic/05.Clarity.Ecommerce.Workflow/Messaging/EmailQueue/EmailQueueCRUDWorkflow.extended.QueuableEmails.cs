// <copyright file="EmailQueueCRUDWorkflow.extended.QueuableEmails.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email queue workflow class</summary>
// ReSharper disable CognitiveComplexity, CyclomaticComplexity, FunctionComplexityOverflow, StyleCop.SA1202
#nullable enable
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using JSConfigs;
    using Models;
    using Utilities;

    public partial class EmailQueueWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> FormatAndQueueEmailAsync(
            string? email,
            Dictionary<string, string?> replacementDictionary,
            IEmailSettings emailSettings,
            IReadOnlyCollection<string?>? attachmentPath,
            Enums.FileEntityType? attachmentType,
            string? contextProfileName)
        {
            var subjectWithReplacements = DoReplacements(replacementDictionary, emailSettings.Subject);
            string bodyWithReplacements;
            using (var webClient = WebClientFactory.Create())
            {
                try
                {
                    emailSettings.Load();
                    if (!emailSettings.Enabled)
                    {
                        return CEFAR.FailingCEFAR<int>("Email not enabled");
                    }
                    var rootURL = CEFConfigDictionary.EmailTemplateRouteHostUrl
                        ?? CEFConfigDictionary.SiteRouteHostUrlSSL
                        ?? CEFConfigDictionary.SiteRouteHostUrl;
                    if (Contract.CheckValidKey(CEFConfigDictionary.EmailTemplateRouteRelativePath))
                    {
                        rootURL += CEFConfigDictionary.EmailTemplateRouteRelativePath;
                    }
                    if (!rootURL.EndsWith("/"))
                    {
                        rootURL += "/";
                    }
                    ////Logger.LogInformation("Attempting to load template at", rootURL + emailSettings.TemplatePath, contextProfileName);
                    ////foreach (var item in replacementDictionary)
                    ////{
                    ////    Logger.LogInformation($"Dictionary Item: {item.Key}", $"Dictionary Value: {item.Value}", contextProfileName);
                    ////}
                    var urlToLoadFrom = rootURL + emailSettings.FullTemplatePath;
                    if (!Contract.CheckValidKey(urlToLoadFrom))
                    {
                        throw new System.Configuration.ConfigurationErrorsException(
                            "Cannot load an email template without a valid FullTemplatePath");
                    }
                    ////Logger.LogInformation("Attempting to load template at", urlToLoadFrom + emailSettings.TemplatePath, contextProfileName);
                    var body = Contract.CheckNullKey(contextProfileName)
                        ? await webClient.DownloadStringTaskAsync(urlToLoadFrom).ConfigureAwait(false)
                        : string.Empty; // Fake the process with an empty string for tests
                    ////Logger.LogInformation("Body", body, contextProfileName);
                    if (string.IsNullOrWhiteSpace(body))
                    {
                        body = string.Empty;
                    }
                    bodyWithReplacements = DoReplacements(replacementDictionary, body);
                    ////Logger.LogInformation("bodyWithReplacements", bodyWithReplacements, contextProfileName);
                    if (string.IsNullOrWhiteSpace(bodyWithReplacements))
                    {
                        bodyWithReplacements = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    return CEFAR.FailingCEFAR<int>(
                        $"Unable to send email: Unable to load template for {emailSettings.GetType().Name},"
                        + $" at: {emailSettings.FullTemplatePath} - with Exception: {ex.Message}");
                }
            }
            var toQueue = RegistryLoaderWrapper.GetInstance<IEmailQueueModel>(contextProfileName);
            toQueue.Active = true;
            toQueue.AddressFrom = emailSettings.From;
            toQueue.Subject = subjectWithReplacements;
            toQueue.Body = bodyWithReplacements;
            toQueue.IsHtml = true;
            toQueue.AddressesTo = email;
            toQueue.AddressesCc = string.Empty;
            toQueue.AddressesBcc = string.Empty;
            if (Contract.CheckValidKey(emailSettings.To))
            {
                if (!Contract.CheckValidKey(toQueue.AddressesTo))
                {
                    toQueue.AddressesTo = emailSettings.To;
                }
                else if (toQueue.AddressesTo!.IndexOf(emailSettings.To!, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    toQueue.AddressesTo += ", " + emailSettings.To;
                }
            }
            if (Contract.CheckValidKey(emailSettings.CC))
            {
                toQueue.AddressesCc ??= string.Empty;
                toQueue.AddressesCc += ", " + emailSettings.CC;
                toQueue.AddressesCc = toQueue.AddressesCc.Trim(' ', ',');
            }
            if (Contract.CheckValidKey(emailSettings.BCC))
            {
                toQueue.AddressesBcc ??= string.Empty;
                toQueue.AddressesBcc += ", " + emailSettings.BCC;
                toQueue.AddressesBcc = toQueue.AddressesBcc.Trim(' ', ',');
            }
            if (Contract.CheckValidKey(toQueue.AddressesTo))
            {
                toQueue.AddressesTo = toQueue.AddressesTo!.Trim(' ', ',');
            }
            toQueue.Attempts = 0;
            toQueue.HasError = false;
            toQueue.StatusKey = "Pending";
            toQueue.StatusName = "Pending";
            toQueue.TypeKey = "General";
            toQueue.TypeName = "General";
            // ReSharper disable once InvertIf
            if (attachmentPath?.Any() == true && attachmentType.HasValue)
            {
                var attachments = new StringBuilder();
                var last = attachmentPath.Last();
                var provider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName);
                if (provider is not null)
                {
                    foreach (var attachment in attachmentPath.Where(Contract.CheckValidKey))
                    {
                        var fileUrl = await provider.GetFileUrlAsync(attachment!, attachmentType.Value).ConfigureAwait(false);
                        attachments.Append(fileUrl);
                        if (attachment != last)
                        {
                            attachments.Append(';');
                        }
                    }
                    toQueue.SerializableAttributes = new()
                    {
                        ["AttachmentPath"] = new()
                        {
                            Key = "AttachmentPath",
                            Value = attachments.ToString(),
                        },
                    };
                }
            }
            return await AddEmailToQueueAsync(toQueue, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task<CEFActionResponse<int>> GenerateResultAsync(
            CEFActionResponse<int> result)
        {
            if (!result.ActionSucceeded)
            {
                result.Messages.Add("Unable to queue email");
            }
            return Task.FromResult(result);
        }
    }
}
