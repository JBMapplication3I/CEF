// <copyright file="SalesQuotesSubmittedNormalToBackOfficeStoreEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quotes submitted normal to back office store email class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire.Annotations;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>The sales quotes submitted normal to back office store email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class SalesQuotesSubmittedNormalToBackOfficeStoreEmail : EmailSettingsBase
    {
        /// <inheritdoc/>
        [AppSettingsKey(".Enabled"),
         DefaultValue(true)]
        public override bool Enabled
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, GetType()) || asValue;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("A new ship to store quote has been submitted for {{CompanyName}}")]
        public override string? Subject
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("SalesQuotes.BackOfficeStore.Submitted.Normal.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".From"),
         DefaultValue("noreply@claritymis.com")]
        public override string? From
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".To"),
         DefaultValue("clarity-local@claritymis.com")]
        public override string? To
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        protected override string SettingsRoot => "Clarity.Notifications.SalesQuotes.BackOfficeStore.Submitted.Normal";

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("salesQuote") == true && parameters["salesQuote"] != null,
                "This email requires a parameter in the dictionary of { [\"salesQuote\"] = <ISalesQuoteModel> }");
            var quote = parameters!["salesQuote"] as ISalesQuoteModel;
            if (!Contract.CheckValidID(quote!.StoreID))
            {
                // Do nothing
                return CEFAR.FailingCEFAR<int>("Unable to send email: No Store assigned to the quote");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var receiverEmail = await context.Stores
                .AsNoTracking()
                .FilterByID(quote.StoreID)
                .Where(x => x.Contact != null)
                .Select(x => x.Contact!.Email1)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(receiverEmail))
            {
                // Don't know where to send it
                return CEFAR.FailingCEFAR<int>("Unable to send email: No primary contact email for the store");
            }
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            await SharedSalesQuotes.StandardQuoteReplacementsAsync(quote, replacementDictionary).ConfigureAwait(false);
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            var result = await Workflows.EmailQueues.FormatAndQueueEmailAsync(
                    email: receiverEmail,
                    replacementDictionary: replacementDictionary,
                    emailSettings: this,
                    attachmentPath: quote.StoredFiles?.Select(x => x.Name).ToList(),
                    attachmentType: Enums.FileEntityType.StoredFileSalesQuote,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return await Workflows.EmailQueues.GenerateResultAsync(result).ConfigureAwait(false);
        }
    }
}
