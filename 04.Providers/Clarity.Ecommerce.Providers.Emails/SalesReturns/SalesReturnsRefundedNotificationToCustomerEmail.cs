// <copyright file="SalesReturnsRefundedNotificationToCustomerEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales returns refunded notification to customer email class</summary>
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

    /// <summary>The sales returns refunded notification to customer email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class SalesReturnsRefundedNotificationToCustomerEmail : EmailSettingsBase
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
         DefaultValue("Your return request has been refunded at {{CompanyName}}")]
        public override string? Subject
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("SalesReturns.Customer.Refunded.html")]
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
        protected override string SettingsRoot => "Clarity.Notifications.SalesReturns.Customer.Refunded";

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("salesReturn") == true && parameters["salesReturn"] != null,
                "This email requires a parameter in the dictionary of { [\"salesReturn\"] = <ISalesReturnModel> }");
            var salesReturn = parameters!["salesOrder"] as ISalesReturnModel;
            if (salesReturn!.BillingContact == null && Contract.CheckValidID(salesReturn.BillingContactID))
            {
                salesReturn.BillingContact = await Workflows.Contacts.GetAsync(
                        salesReturn.BillingContactID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            if (salesReturn.ShippingContact == null && Contract.CheckValidID(salesReturn.ShippingContactID))
            {
                salesReturn.ShippingContact = await Workflows.Contacts.GetAsync(
                        salesReturn.ShippingContactID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            // Do
            return await SharedSalesReturns.FormatAndQueueSalesReturnNotificationAsync(
                    salesReturn: salesReturn,
                    emailSettings: this,
                    isInternal: false,
                    replacementDictionary: GetBaseReplacementsDictionary(),
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }
    }
}
