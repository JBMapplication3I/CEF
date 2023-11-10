// <copyright file="ShoppingCartItemsSubmittedToSpecificEmailEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shopping cart items submitted to specific email class</summary>
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

    /// <summary>The shopping cart items submitted to specific email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class ShoppingCartItemsSubmittedToSpecificEmailEmail : EmailSettingsBase
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
         DefaultValue("Shopping cart items submitted for {{CompanyName}}")]
        public override string? Subject
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("Carts.ToEmail.Submitted.html")]
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
        protected override string SettingsRoot => "Clarity.Notifications.Carts.ToEmail.Submitted";

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("cart") == true && parameters["cart"] != null,
                "This email requires a parameter in the dictionary of { [\"cart\"] = <ICartModel> }");
            var cart = parameters!["cart"] as ICartModel;
            if (cart!.BillingContact == null && Contract.CheckValidID(cart.BillingContactID))
            {
                cart.BillingContact = await Workflows.Contacts.GetAsync(
                        cart.BillingContactID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            if (cart.ShippingContact == null && Contract.CheckValidID(cart.ShippingContactID))
            {
                cart.ShippingContact = await Workflows.Contacts.GetAsync(
                        cart.ShippingContactID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            await SharedSalesOrders.StandardSalesCollectionReplacementsAsync(
                    cart,
                    cart.SalesItems!,
                    replacementDictionary)
                .ConfigureAwait(false);
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            var result = await Workflows.EmailQueues.FormatAndQueueEmailAsync(
                    email: to,
                    replacementDictionary: replacementDictionary,
                    emailSettings: this,
                    attachmentPath: null,
                    attachmentType: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return await Workflows.EmailQueues.GenerateResultAsync(result).ConfigureAwait(false);
        }
    }
}
