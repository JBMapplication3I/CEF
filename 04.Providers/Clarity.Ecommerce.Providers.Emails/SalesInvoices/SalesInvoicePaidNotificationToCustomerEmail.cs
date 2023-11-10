// <copyright file="SalesInvoicePaidNotificationToCustomerEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice paid notification to customer email class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire.Annotations;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>The sales invoice partially paid notification to customer email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class SalesInvoicePartiallyPaidNotificationToCustomerEmail : EmailSettingsBase
    {
        /// <inheritdoc/>
        protected override string SettingsRoot => "Clarity.Notifications.SalesInvoices.Customer.PartiallyPaid";

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
                    "partially paid",
                    contextProfileName)
                .ConfigureAwait(false);
            if (!emailToUseResult.ActionSucceeded)
            {
                return emailToUseResult.ChangeFailingCEFARType<int>();
            }
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            var result = await Workflows.EmailQueues.FormatAndQueueEmailAsync(
                    emailToUseResult.Result,
                    replacementDictionary,
                    this,
                    invoice!.StoredFiles?.Select(x => x.Name).ToList(),
                    Enums.FileEntityType.StoredFileSalesInvoice,
                    contextProfileName)
                .ConfigureAwait(false);
            return await Workflows.EmailQueues.GenerateResultAsync(result).ConfigureAwait(false);
        }
    }
}
