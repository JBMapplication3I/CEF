// <copyright file="SingleQuoteSubmitService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the single quote submit service class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.SalesQuoteHandlers.Checkouts;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using SalesQuoteHandlers.Checkouts.Services;
    using Service;
    using ServiceStack;
    using SingleQuote;
    using Utilities;

    /// <summary>The process current cart to single quote.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, UsedInStorefront,
     Route("/Providers/SubmitQuote/ProcessCurrentQuoteCartToSingleQuote", "POST",
        Summary = "Submit the current quote cart of the given session type")]
    public class ProcessCurrentQuoteCartToSingleQuote : CheckoutModel, IReturn<CheckoutResult>
    {
    }

    /// <summary>The process specific cart to single quote.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, UsedInAdmin,
     Route("/Providers/SubmitQuote/ProcessSpecificQuoteCartToSingleQuote", "POST",
        Summary = "Submit Quote the specific quote cart")]
    public class ProcessSpecificQuoteCartToSingleQuote : CheckoutModel, IReturn<CheckoutResult>
    {
    }

    /// <summary>A single quote submit service.</summary>
    /// <seealso cref="SalesQuoteCheckoutServiceBase"/>
    public partial class SubmitQuoteService
    {
        /// <summary>Gets or sets the single quote submit provider.</summary>
        /// <value>The single quote submit provider.</value>
        private static ISalesQuoteSubmitProviderBase? SingleQuoteSubmitProvider { get; set; }

        /// <summary>Reacts to a POST attempt for the ProcessCurrentQuoteCartToSingleQuote endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
        public async Task<object> Post(ProcessCurrentQuoteCartToSingleQuote request)
        {
            if (CEFConfigDictionary.AffiliatesEnabled)
            {
                request.AffiliateAccountKey = Request.GetItemOrCookie(SelectedAffiliateAccountKeyCookieName).UrlDecode();
            }
            // Verify the account is not on hold
            var result1 = await DoAccountOnHoldCheckAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (result1 != null)
            {
                return result1;
            }
            // Required Setup
            await SingleSetupForSelfAsync(request).ConfigureAwait(false);
            // Checkout
            var result = await Contract.RequiresNotNull(SingleQuoteSubmitProvider).SubmitAsync(
                    checkout: request,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    lookupKey: await GenSessionLookupKeyAsync(request.WithCartInfo?.CartTypeName!).ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            return await ValidateAndCompleteSubmitAsync(
                    failCondition: !result.Succeeded || !Contract.CheckValidID(result.QuoteID),
                    result: result)
                .ConfigureAwait(false);
        }

        /// <summary>Reacts to a POST attempt for the ProcessSpecificCartToSingleQuote endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
        public async Task<object> Post(ProcessSpecificQuoteCartToSingleQuote request)
        {
            // Required Setup
            await SingleSetupForOtherAsync(request).ConfigureAwait(false);
            // Run the submit procedures
            var result = await Contract.RequiresNotNull(SingleQuoteSubmitProvider).SubmitAsync(
                    checkout: request,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    lookupKey: await GenCartByIDLookupKeyAsync(
                            cartID: Contract.RequiresValidID(request.WithCartInfo?.CartID),
                            userID: Contract.RequiresValidID(request.WithUserInfo?.UserID),
                            accountID: await Workflows.Accounts.GetIDByUserIDAsync(request.WithUserInfo!.UserID!.Value!, ServiceContextProfileName).ConfigureAwait(false))
                        .ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            return await ValidateAndCompleteSubmitAsync(
                    failCondition: !result.Succeeded || !Contract.CheckValidID(result.QuoteID),
                    result: result)
                .ConfigureAwait(false);
        }

        /// <summary>Ensures that single quote submit provider exists and is initialized.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task EnsureSingleQuoteSubmitProviderAsync(string? contextProfileName)
        {
            if (QuoteTypeID == 0)
            {
                await SetupIDsAsync(contextProfileName).ConfigureAwait(false);
            }
            SingleQuoteSubmitProvider ??= new SingleQuoteSubmitProvider();
            if (SingleQuoteSubmitProvider.IsInitialized)
            {
                return;
            }
            await SingleQuoteSubmitProvider.InitAsync(
                    quoteStatusPendingID: QuoteStatusSubmittedID,
                    quoteStatusOnHoldID: QuoteStatusOnHoldID,
                    quoteTypeID: QuoteTypeID,
                    quoteStateID: QuoteStateID,
                    shippingTypeID: ShippingTypeID,
                    customerNoteTypeID: CustomerNoteTypeID,
                    defaultCurrencyID: DefaultCurrencyID,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Sets up for other.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task.</returns>
        private async Task SingleSetupForOtherAsync(ICheckoutModel request)
        {
            // Load the submit providers
            await EnsureSingleQuoteSubmitProviderAsync(ServiceContextProfileName).ConfigureAwait(false);
            // Referring Store
            await ProcessForSelectedStoreIDAsync(request).ConfigureAwait(false);
        }

        /// <summary>Sets up for self.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task.</returns>
        private async Task SingleSetupForSelfAsync(ICheckoutModel request)
        {
            await EnsureSingleQuoteSubmitProviderAsync(ServiceContextProfileName).ConfigureAwait(false);
            var typeName = request.WithCartInfo?.CartTypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID != null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            // Base Settings
            request.WithCartInfo ??= new CheckoutWithCartInfo();
            request.WithCartInfo.CartSessionID = await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false);
            request.WithUserInfo ??= new CheckoutWithUserInfo();
            request.WithUserInfo.ExternalUserID ??= CurrentUserName;
        }
    }
}
