// <copyright file="SalesInvoiceVoidedNotificationToCustomerEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice voided notification to customer email class</summary>
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

    /// <summary>The sales invoice voided notification to customer email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class SalesInvoiceVoidedNotificationToCustomerEmail : EmailSettingsBase
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
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("SalesInvoices.Customer.Voided.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("Invoice Voided At {{CompanyName}}")]
        public override string? Subject
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
        protected override string SettingsRoot => "Clarity.Notifications.SalesInvoices.Customer.Voided";

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("invoice") == true && parameters["invoice"] != null,
                "This email requires a parameter in the dictionary of { [\"invoice\"] = <ISalesInvoiceModel> }");
            var invoice = parameters!["invoice"] as ISalesInvoiceModel;
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            await SharedSalesInvoice.StandardInvoiceReplacementsAsync(invoice!, replacementDictionary).ConfigureAwait(false);
            var emailToUseResult = await SharedSalesInvoice.GetInvoiceCustomerEmailToUseAsync(
                    invoice!,
                    "voided",
                    contextProfileName)
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
                    attachmentPath: invoice!.StoredFiles?.Select(x => x.Name).ToList(),
                    attachmentType: Enums.FileEntityType.StoredFileSalesInvoice,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return await Workflows.EmailQueues.GenerateResultAsync(result).ConfigureAwait(false);
        }
    }
}
