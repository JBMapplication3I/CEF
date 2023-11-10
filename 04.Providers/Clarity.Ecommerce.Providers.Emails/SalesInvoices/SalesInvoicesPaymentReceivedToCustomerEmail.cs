// <copyright file="SalesInvoicesPaymentReceivedToCustomerEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoices payment received to customer email class</summary>
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

    /// <summary>The sales invoices payment notification to customer email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class SalesInvoicesPaymentReceivedToCustomerEmail : EmailSettingsBase
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
         DefaultValue("SalesInvoices.Customer.PaymentReceived.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("Invoice Payment Recieved At {{CompanyName}}")]
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
        protected override string SettingsRoot => "Clarity.Notifications.SalesInvoices.Customer.Payment";

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("salesInvoice") == true && parameters["salesInvoice"] != null,
                "This email requires a parameter in the dictionary of { [\"salesInvoice\"] = <ISalesInvoiceModel> }");
            var invoice = parameters!["salesInvoice"] as ISalesInvoiceModel;
            var lastPayment = invoice!.SalesInvoicePayments!.Where(s => s.Active).OrderBy(x => x.CreatedDate).LastOrDefault();
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            await SharedSalesInvoice.StandardInvoiceReplacementsAsync(invoice, replacementDictionary).ConfigureAwait(false);
            var emailResult = await SharedSalesInvoice.GetInvoiceCustomerEmailToUseAsync(
                    invoice: invoice,
                    whichEmailTemplate: "payment",
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!emailResult.ActionSucceeded)
            {
                return emailResult.ChangeFailingCEFARType<int>();
            }
            to ??= emailResult.Result;
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{Last4Digits}}"] = lastPayment?.Slave?.Last4CardDigits;
            replacementDictionary["{{PaymentReceived}}"] = lastPayment?.Slave?.Amount.ToString();
            replacementDictionary["{{TotalPayment}}"] = invoice.SalesInvoicePayments!.Sum(x => x.Slave!.Amount).ToString();
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            return await FormatAndQueueEmailAsync(to, replacementDictionary, contextProfileName).ConfigureAwait(false);
        }
    }
}
