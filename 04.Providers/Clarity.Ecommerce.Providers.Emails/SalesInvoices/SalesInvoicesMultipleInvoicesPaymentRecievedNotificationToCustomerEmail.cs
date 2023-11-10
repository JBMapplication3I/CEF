// <copyright file="SalesInvoicesMultipleInvoicesPaymentRecievedNotificationToCustomerEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoices multiple invoices payment recieved notification to customer email class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire.Annotations;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>The sales invoices multiple invoices payment recieved notification to customer email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class SalesInvoicesMultipleInvoicesPaymentRecievedNotificationToCustomerEmail : EmailSettingsBase
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
         DefaultValue("SalesInvoices.Customer.Payment.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("Multiple Invoice Payments Recieved At {{CompanyName}}")]
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
                parameters?.ContainsKey("amounts") == true && parameters["amounts"] != null,
                "This email requires a parameter in the dictionary of { [\"amounts\"] = <Dictionary<int, decimal>> }");
            var amounts = parameters!["amounts"] as Dictionary<int, decimal>;
            // Do
            var aggregate = await CEFAR.AggregateAsync(
                    await Task.WhenAll(amounts!.Select(x => Workflows.SalesInvoices.GetAsync(x.Key, contextProfileName))).ConfigureAwait(false),
                    invoice => new SalesInvoicesPaymentReceivedToCustomerEmail().QueueAsync(
                        contextProfileName,
                        to,
                        new() { ["salesInvoice"] = invoice! },
                        customReplacements))
                .ConfigureAwait(false);
            return aggregate.ActionSucceeded
                ? CEFAR.PassingCEFAR(aggregate.Result!.First())
                : aggregate.ChangeFailingCEFARType<int>();
        }
    }
}
