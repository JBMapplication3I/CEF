// <copyright file="SalesQuotesSubmittedNormalToCustomerEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quotes submitted normal to customer email class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Emails
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire.Annotations;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>The sales quotes submitted normal to customer email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class SalesQuotesSubmittedNormalToCustomerEmail : EmailSettingsBase
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
         DefaultValue("A new Quote has been submitted for {{CompanyName}}")]
        public override string? Subject
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType())
                ? asValue
                : "A new Quote has been submitted for {{CompanyName}}";
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("SalesQuotes.Customer.Submitted.Normal.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType())
                ? asValue
                : "SalesQuotes.Customer.Submitted.Normal.html";
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".From"),
         DefaultValue("noreply@claritymis.com")]
        public override string? From
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType())
                ? asValue
                : "noreply@claritymis.com";
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        protected override string SettingsRoot => "Clarity.Notifications.SalesQuotes.Customer.Submitted.Normal";

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
            if (quote!.BillingContact == null && Contract.CheckValidID(quote.BillingContactID))
            {
                quote.BillingContact = await Workflows.Contacts.GetAsync(
                        quote.BillingContactID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            if (quote.ShippingContact == null && Contract.CheckValidID(quote.ShippingContactID))
            {
                quote.ShippingContact = await Workflows.Contacts.GetAsync(
                        quote.ShippingContactID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckEmpty(quote.RateQuotes))
            {
                var search = RegistryLoaderWrapper.GetInstance<IRateQuoteSearchModel>(contextProfileName);
                search.Active = true;
                search.SalesQuoteID = quote.ID;
                search.Selected = true;
                quote.RateQuotes = (await Workflows.RateQuotes.SearchAsync(
                            search: search,
                            asListing: true,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false))
                    .results;
            }
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            await SharedSalesQuotes.StandardQuoteReplacementsAsync(quote, replacementDictionary).ConfigureAwait(false);
            var emailToUseResult = await SharedSalesQuotes.GetQuoteCustomerEmailToUseAsync(
                    quote: quote,
                    whichEmailTemplate: "submitted",
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!emailToUseResult.ActionSucceeded)
            {
                return emailToUseResult.ChangeFailingCEFARType<int>();
            }
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            var result = await Workflows.EmailQueues.FormatAndQueueEmailAsync(
                    email: emailToUseResult.Result,
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
