// <copyright file="SalesOrdersSubmittedNormalToBackOfficeEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales orders submitted normal to back office email class</summary>
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

    /// <summary>The sales orders submitted normal to back office email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class SalesOrdersSubmittedNormalToBackOfficeEmail : EmailSettingsBase
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
         DefaultValue("A new order has been submitted for {{CompanyName}}")]
        public override string? Subject
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType())
                ? asValue
                : "A new order has been submitted for {{CompanyName}}";
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("SalesOrders.BackOffice.Submitted.Normal.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType())
                ? asValue
                : "SalesOrders.BackOffice.Submitted.Normal.html";
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
        [AppSettingsKey(".To"),
         DefaultValue("clarity-local@claritymis.com")]
        public override string? To
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType())
                ? asValue
                : "clarity-local@claritymis.com";
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        protected override string SettingsRoot => "Clarity.Notifications.SalesOrders.BackOffice.Submitted.Normal";

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("salesOrder") == true && parameters["salesOrder"] != null,
                "This email requires a parameter in the dictionary of { [\"salesOrder\"] = <ISalesOrderModel> }");
            var order = parameters!["salesOrder"] as ISalesOrderModel;
            if (order!.BillingContact == null)
            {
                if (Contract.CheckValidID(order.BillingContactID))
                {
                    order.BillingContact = await Workflows.Contacts.GetAsync(
                        order.BillingContactID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
                }
                else if (Contract.CheckValidID(order.SalesGroupAsMasterID))
                {
                    var salesGroup = await Workflows.SalesGroups.GetAsync(
                        order.SalesGroupAsMasterID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
                    if (salesGroup is not null && Contract.CheckValidID(salesGroup.BillingContactID))
                    {
                        order.BillingContact = await Workflows.Contacts.GetAsync(
                            salesGroup.BillingContactID!.Value,
                            contextProfileName)
                        .ConfigureAwait(false);
                    }
                }
                else if (Contract.CheckValidID(order.SalesGroupAsSubID))
                {
                    var salesGroup = await Workflows.SalesGroups.GetAsync(
                        order.SalesGroupAsSubID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
                    if (salesGroup is not null && Contract.CheckValidID(salesGroup.BillingContactID))
                    {
                        order.BillingContact = await Workflows.Contacts.GetAsync(
                            salesGroup.BillingContactID!.Value,
                            contextProfileName)
                        .ConfigureAwait(false);
                    }
                }
            }
            if (order.ShippingContact == null && Contract.CheckValidID(order.ShippingContactID))
            {
                order.ShippingContact = await Workflows.Contacts.GetAsync(
                        order.ShippingContactID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckEmpty(order.RateQuotes))
            {
                var search = RegistryLoaderWrapper.GetInstance<IRateQuoteSearchModel>(contextProfileName);
                search.Active = true;
                search.SalesOrderID = order.ID;
                search.Selected = true;
                order.RateQuotes = (await Workflows.RateQuotes.SearchAsync(
                            search,
                            true,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .results;
            }
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            await SharedSalesOrders.StandardOrderReplacementsAsync(order, replacementDictionary).ConfigureAwait(false);
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            var result = await Workflows.EmailQueues.FormatAndQueueEmailAsync(
                    email: to,
                    replacementDictionary: replacementDictionary,
                    emailSettings: this,
                    attachmentPath: order.StoredFiles?.Select(x => x.Name).ToList(),
                    attachmentType: Enums.FileEntityType.StoredFileSalesOrder,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return await Workflows.EmailQueues.GenerateResultAsync(result).ConfigureAwait(false);
        }
    }
}
