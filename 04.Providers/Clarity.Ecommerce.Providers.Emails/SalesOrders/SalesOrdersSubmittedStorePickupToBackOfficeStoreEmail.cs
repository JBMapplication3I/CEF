﻿// <copyright file="SalesOrdersSubmittedStorePickupToBackOfficeStoreEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales orders submitted store pickup to back office store email class</summary>
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

    /// <summary>The sales orders submitted store pickup to back office store email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class SalesOrdersSubmittedStorePickupToBackOfficeStoreEmail : EmailSettingsBase
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
         DefaultValue("A new in store pickup order has been submitted for {{CompanyName}}")]
        public override string? Subject
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("SalesOrders.BackOfficeStore.Submitted.StorePickup.html")]
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
        protected override string SettingsRoot => "Clarity.Notifications.SalesOrders.BackOfficeStore.Submitted.StorePickup";

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
            if (!Contract.CheckValidID(order!.StoreID))
            {
                // Do nothing
                return CEFAR.FailingCEFAR<int>("Unable to send email: No Store assigned to the order");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var receiverEmail = await context.Stores
                .AsNoTracking()
                .FilterByID(order.StoreID)
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
            await SharedSalesOrders.StandardOrderReplacementsAsync(order, replacementDictionary).ConfigureAwait(false);
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            var result = await Workflows.EmailQueues.FormatAndQueueEmailAsync(
                    email: receiverEmail,
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
