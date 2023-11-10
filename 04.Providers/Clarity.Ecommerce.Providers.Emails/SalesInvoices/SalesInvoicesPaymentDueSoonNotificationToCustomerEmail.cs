// <copyright file="SalesInvoicesPaymentDueSoonNotificationToCustomerEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoices payment due soon notification to customer email class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Hangfire.Annotations;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>The sales invoices payment due soon notification to customer email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class SalesInvoicesPaymentDueSoonNotificationToCustomerEmail : EmailSettingsBase
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
            DefaultValue("SalesInvoices.Customer.PaymentDueSoon.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
            DefaultValue("Invoice Payment Due Soon at {{CompanyName}}")]
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
        protected override string SettingsRoot => "Clarity.Notifications.SalesInvoices.Customer.PaymentDueSoon";

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
            var daysDistanceFromDueDate = parameters["daysDistanceFromDueDate"] as int?;
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{DaysDistanceFromDueDate}}"] = daysDistanceFromDueDate.ToString();
            await SharedSalesInvoice.StandardInvoiceReplacementsAsync(invoice!, replacementDictionary).ConfigureAwait(false);
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            return await FormatAndQueueEmailAsync(to, replacementDictionary, contextProfileName).ConfigureAwait(false);
        }
    }
}
