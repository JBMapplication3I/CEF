// <copyright file="SalesQuoteCheckoutServiceBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote checkout service base class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Checkouts.Services
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A sales quote checkout service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [JetBrains.Annotations.PublicAPI]
    public abstract class SalesQuoteCheckoutServiceBase : ClarityEcommerceServiceBase
    {
        /// <summary>Gets or sets the identifier of the shipping type.</summary>
        /// <value>The identifier of the shipping type.</value>
        protected static int ShippingTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the quote status pending.</summary>
        /// <value>The identifier of the quote status pending.</value>
        protected static int QuoteStatusSubmittedID { get; set; }

        /// <summary>Gets or sets the identifier of the quote status on hold.</summary>
        /// <value>The identifier of the quote status on hold.</value>
        protected static int QuoteStatusOnHoldID { get; set; }

        /// <summary>Gets or sets the identifier of the quote state.</summary>
        /// <value>The identifier of the quote state.</value>
        protected static int QuoteStateID { get; set; }

        /// <summary>Gets or sets the identifier of the quote type.</summary>
        /// <value>The identifier of the quote type.</value>
        protected static int QuoteTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the customer note type.</summary>
        /// <value>The identifier of the customer note type.</value>
        protected static int CustomerNoteTypeID { get; set; }

        /// <summary>Gets or sets the default currency identifier.</summary>
        /// <value>The default currency identifier.</value>
        protected static int DefaultCurrencyID { get; set; }

        /// <summary>Sets up the IDs.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected static async Task SetupIDsAsync(string? contextProfileName)
        {
            var workflows = RegistryLoaderWrapper.GetInstance<IWorkflowsController>(contextProfileName);
            ShippingTypeID = await workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                    null,
                    "Shipping",
                    "Shipping",
                    "Shipping",
                    null,
                    contextProfileName)
                .ConfigureAwait(false);
            QuoteStatusSubmittedID = await workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                    null,
                    "Submitted",
                    "Submitted",
                    "Submitted",
                    null,
                    contextProfileName)
                .ConfigureAwait(false);
            QuoteStatusOnHoldID = await workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                    null,
                    "On Hold",
                    "On Hold",
                    "On Hold",
                    null,
                    contextProfileName)
                .ConfigureAwait(false);
            QuoteTypeID = await workflows.SalesOrderTypes.ResolveWithAutoGenerateToIDAsync(
                    null,
                    "Web",
                    "Web",
                    "Web",
                    null,
                    contextProfileName)
                .ConfigureAwait(false);
            QuoteStateID = await workflows.SalesOrderStates.ResolveWithAutoGenerateToIDAsync(
                    null,
                    "WORK",
                    "Work",
                    "Work",
                    null,
                    contextProfileName)
                .ConfigureAwait(false);
            CustomerNoteTypeID = await workflows.NoteTypes.ResolveWithAutoGenerateToIDAsync(
                    null,
                    "Customer",
                    "Customer",
                    "Customer",
                    new NoteTypeModel
                    {
                        Active = true,
                        CustomKey = "Customer",
                        Name = "Customer",
                        DisplayName = "Customer",
                        IsCustomer = true,
                        IsPublic = true,
                    },
                    contextProfileName)
                .ConfigureAwait(false);
            DefaultCurrencyID = await workflows.Currencies.ResolveWithAutoGenerateToIDAsync(
                    null,
                    "USD",
                    "US Dollar",
                    null,
                    contextProfileName)
                .ConfigureAwait(false);
            ////PreferredPaymentMethodAttr = await workflows.GeneralAttributes.ResolveWithAutoGenerateAsync(
            ////        null,
            ////        "Preferred Payment Method",
            ////        "Preferred Payment Method",
            ////        "Preferred Payment Method",
            ////        new GeneralAttributeModel
            ////        {
            ////            Active = true,
            ////            CustomKey = "Preferred Payment Method",
            ////            Name = "Preferred Payment Method",
            ////            DisplayName = "Preferred Payment Method",
            ////            TypeName = "General"
            ////        },
            ////        true,
            ////        contextProfileName)
            ////    .ConfigureAwait(false);
        }

        /// <summary>Checkout with provider.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="cartTypeName">      Name of the cart type.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="Task{CheckoutResult}"/>.</returns>
        protected virtual async Task<CheckoutResult> CheckoutWithProviderAsync(
            ICheckoutModel request,
            string cartTypeName,
            string? contextProfileName)
        {
            var provider = RegistryLoaderWrapper.GetSalesQuoteCheckoutProvider(
                    contextProfileName: contextProfileName)
                ?? throw new InvalidOperationException("Failed to locate a quote checkout provider");
            return (CheckoutResult)await provider.SubmitAsync(
                    checkout: request,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    lookupKey: new SessionCartBySessionAndTypeLookupKey(
                        sessionID: await GetSessionQuoteCartGuidAsync().ConfigureAwait(false),
                        typeKey: cartTypeName,
                        userID: CurrentUserID,
                        accountID: await LocalAdminAccountIDAsync(CurrentAccountID).ConfigureAwait(false)),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Process for selected store identifier described by request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ProcessForSelectedStoreIDAsync(ICheckoutModel request)
        {
            var storeIDString = Request.GetItemOrCookie("cefSelectedStoreId");
            if (int.TryParse(storeIDString, out var storeID))
            {
                request.ReferringStoreID = storeID;
            }
            return Task.CompletedTask;
        }

        /// <summary>Validates and complete checkout.</summary>
        /// <param name="failCondition">              True if it failed, false if it succeeded.</param>
        /// <param name="result">                     The result.</param>
        /// <param name="checkoutByInventoryLocation">True to checkout by inventory location.</param>
        /// <returns>A CheckoutResult.</returns>
        protected async Task<ICheckoutResult> ValidateAndCompleteSubmitAsync(
            bool failCondition,
            ICheckoutResult result,
            bool checkoutByInventoryLocation = false)
        {
            if (failCondition)
            {
                // There was an error and the error message as put into ErrorMessage
                result.Succeeded = false;
                return result;
            }
            if (checkoutByInventoryLocation)
            {
                return result;
            }
            // Ensuring Session's SessionCartGuid gets reset
            await ClearSessionQuoteCartGuidAsync().ConfigureAwait(false);
            return result;
        }
    }
}
