// <copyright file="TargetQuoteSubmitService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target quote submit service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.SalesQuoteHandlers.Checkouts;
    using JetBrains.Annotations;
    using Models;
    using SalesQuoteHandlers.Checkouts.Services;
    using Service;
    using ServiceStack;
    using TargetQuote;
    using Utilities;

    /// <summary>The analyze current quote cart to target quote carts.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, UsedInStorefront,
     Route("/Providers/SubmitQuote/AnalyzeCurrentQuoteCartToTargetQuoteCarts", "POST",
        Summary = "Submit the current quote cart of the given session type")]
    public class AnalyzeCurrentQuoteCartToTargetQuoteCarts : CheckoutModel, IReturn<CEFActionResponse<List<CartModel>>>
    {
    }

    /// <summary>An analyze specific quote cart to target quote carts.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, Authenticate, UsedInAdmin,
     Route("/Providers/SubmitQuote/AnalyzeSpecificQuoteCartToTargetQuoteCarts", "POST",
        Summary = "Submit the current quote cart of the given session type")]
    public class AnalyzeSpecificQuoteCartToTargetQuotes : CheckoutModel, IReturn<CEFActionResponse<List<CartModel>>>
    {
    }

    /// <summary>Clear the current target carts and start over.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, Authenticate, UsedInStorefront,
     Route("/Providers/SubmitQuote/ClearCurrentCartToTargetCartsAnalysis", "DELETE",
         Summary = "Clear the current target carts and start over.")]
    public class ClearCurrentQuoteCartToTargetCartsAnalysis : CheckoutModel, IReturn<CEFActionResponse>
    {
    }

    /// <summary>The process current quote cart to target quotes.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, UsedInStorefront,
     Route("/Providers/SubmitQuote/ProcessCurrentCartToTargetOrders", "POST",
        Summary = "Submit the current quote cart of the given session type.")]
    public class ProcessCurrentQuoteCartToTargetQuotes : CheckoutModel, IReturn<CheckoutResult>
    {
    }

    /// <summary>The process specific quote cart to target quotes.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, Authenticate, UsedInAdmin,
     Route("/Providers/SubmitQuote/ProcessSpecificQuoteCartToTargetQuotes", "POST",
        Summary = "Submit the current quote cart of the given cart identifier.")]
    public class ProcessSpecificQuoteCartToTargetQuotes : CheckoutModel, IReturn<CheckoutResult>
    {
    }

    /// <summary>A target quote submit service.</summary>
    /// <seealso cref="SalesQuoteCheckoutServiceBase"/>
    public partial class SubmitQuoteService : SalesQuoteCheckoutServiceBase
    {
        /// <summary>Gets or sets target quote submit provider.</summary>
        /// <value>The target quote submit provider.</value>
        private static ISalesQuoteSubmitProviderBase? TargetQuoteSubmitProvider { get; set; }

        /// <summary>Reacts to a POST attempt for the AnalyzeCurrentQuoteCartToTargetQuoteCarts endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
#pragma warning disable IDE1006 // Naming Styles
        public async Task<object> Post(AnalyzeCurrentQuoteCartToTargetQuoteCarts request)
#pragma warning restore IDE1006 // Naming Styles
        {
            // Verify the account is not on hold
            var result1 = await DoAccountOnHoldCheckAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (result1 != null)
            {
                return CEFAR.FailingCEFAR<List<CartModel>>(result1.ErrorMessage);
            }
            // Required Setup
            await TargetsSetupForSelfAsync(request).ConfigureAwait(false);
            var taxesProvider = await GetTaxProviderAsync().ConfigureAwait(false);
            var pricingFactoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(request.WithCartInfo!.CartTypeName!).ConfigureAwait(false);
            // Run the checkout procedures
            var results = new CEFActionResponse<List<ICartModel?>?>[5];
            for (var i = 0; i < 1; i++)
            {
                results[i] = await Contract.RequiresNotNull(TargetQuoteSubmitProvider).AnalyzeAsync(
                        checkout: request,
                        pricingFactoryContext: pricingFactoryContext,
                        lookupKey: lookupKey,
                        taxesProvider: taxesProvider,
                        contextProfileName: null)
                    .ConfigureAwait(false);
                if (i == 0)
                {
                    // continue;
                }
                // TODO: Check for material difference from previous run
            }
            return results.Last(x => x != null)!.ChangeCEFARListType<ICartModel?, CartModel>();
        }

        /// <summary>Reacts to a POST attempt for the ProcessCurrentQuoteCartToTargetQuotes endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
#pragma warning disable IDE1006 // Naming Styles
        public async Task<object> Post(ProcessCurrentQuoteCartToTargetQuotes request)
#pragma warning restore IDE1006 // Naming Styles
        {
            // Verify the account is not on hold
            var result1 = await DoAccountOnHoldCheckAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (result1 != null)
            {
                return result1;
            }
            // Required Setup
            await TargetsSetupForSelfAsync(request).ConfigureAwait(false);
            // Run the checkout procedures
            var result = await Contract.RequiresNotNull(TargetQuoteSubmitProvider).SubmitAsync(
                    checkout: request,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    lookupKey: await GenSessionLookupKeyAsync(request.WithCartInfo!.CartTypeName!).ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            return await ValidateAndCompleteSubmitAsync(
                    failCondition: !result.Succeeded || result.QuoteIDs?.Any(x => Contract.CheckValidID(x)) != true,
                    result: result)
                .ConfigureAwait(false);
        }

        /// <summary>Reacts to a DELETE attempt for the clear analysis endpoint.</summary>
        /// <param name="request">The request to delete.</param>
        /// <returns>A Task{object}</returns>
#pragma warning disable IDE1006 // Naming Styles
        public async Task<object> Delete(ClearCurrentQuoteCartToTargetCartsAnalysis request)
#pragma warning restore IDE1006 // Naming Styles
        {
            await TargetsSetupForSelfAsync(request).ConfigureAwait(false);
            return await Contract.RequiresNotNull(TargetQuoteSubmitProvider).ClearAnalysisAsync(
                    checkout: request,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    lookupKey: await GenSessionLookupKeyAsync(request.WithCartInfo!.CartTypeName!).ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        /// <summary>Reacts to a POST attempt for the Cart Checkout endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
#pragma warning disable IDE1006 // Naming Styles
        public async Task<object> Post(AnalyzeSpecificQuoteCartToTargetQuotes request)
#pragma warning restore IDE1006 // Naming Styles
        {
            // Required Setup
            await TargetsSetupForOtherAsync(request).ConfigureAwait(false);
            // Run the checkout procedures
            var result = await Contract.RequiresNotNull(TargetQuoteSubmitProvider).AnalyzeAsync(
                    checkout: request,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    lookupKey: await GenCartByIDLookupKeyAsync(
                        cartID: Contract.RequiresValidID(request.WithCartInfo?.CartID),
                        userID: Contract.RequiresValidID(request.WithUserInfo!.UserID),
                        accountID: await Workflows.Accounts.GetIDByUserIDAsync(request.WithUserInfo.UserID!.Value, null).ConfigureAwait(false)),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            return result!.ChangeCEFARListType<ICartModel?, CartModel>();
        }

        /// <summary>Reacts to a POST attempt for the Cart Checkout endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
#pragma warning disable IDE1006 // Naming Styles
        public async Task<object> Post(ProcessSpecificQuoteCartToTargetQuotes request)
#pragma warning restore IDE1006 // Naming Styles
        {
            // Required Setup
            await TargetsSetupForOtherAsync(request).ConfigureAwait(false);
            // Run the checkout procedures
            var result = await Contract.RequiresNotNull(TargetQuoteSubmitProvider).SubmitAsync(
                    checkout: request,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    lookupKey: await GenCartByIDLookupKeyAsync(
                        cartID: Contract.RequiresValidID(request.WithCartInfo?.CartID),
                        userID: Contract.RequiresValidID(request.WithUserInfo!.UserID),
                        accountID: await Workflows.Accounts.GetIDByUserIDAsync(request.WithUserInfo.UserID!.Value, null).ConfigureAwait(false)),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            return await ValidateAndCompleteSubmitAsync(
                    failCondition: !result.Succeeded || result.QuoteIDs?.Any(x => Contract.CheckValidID(x)) != true,
                    result: result)
                .ConfigureAwait(false);
        }

        /// <summary>Ensures that target quote submit provider exists and is initialized.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task EnsureTargetQuoteSubmitProviderAsync(string? contextProfileName)
        {
            if (QuoteTypeID == 0)
            {
                await SetupIDsAsync(contextProfileName).ConfigureAwait(false);
            }
            TargetQuoteSubmitProvider ??= new TargetQuoteSubmitProvider();
            if (TargetQuoteSubmitProvider.IsInitialized)
            {
                return;
            }
            await TargetQuoteSubmitProvider.InitAsync(
                    quoteStatusPendingID: QuoteStatusSubmittedID,
                    quoteStatusOnHoldID: QuoteStatusOnHoldID,
                    quoteTypeID: QuoteTypeID,
                    quoteStateID: QuoteStateID,
                    shippingTypeID: ShippingTypeID,
                    customerNoteTypeID: CustomerNoteTypeID,
                    defaultCurrencyID: DefaultCurrencyID,
                    ////preferredPaymentMethodAttr: PreferredPaymentMethodAttr,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Sets up for self.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task.</returns>
        private async Task TargetsSetupForSelfAsync(ICheckoutModel request)
        {
            // Load the checkout providers
            await EnsureTargetQuoteSubmitProviderAsync(null).ConfigureAwait(false);
            // Resolve the correct session ID
            var typeName = request.WithCartInfo?.CartTypeName ?? "Quote Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: null)
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
            // Referring Store
            await ProcessForSelectedStoreIDAsync(request).ConfigureAwait(false);
        }

        /// <summary>Sets up for other.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task.</returns>
        private async Task TargetsSetupForOtherAsync(ICheckoutModel request)
        {
            // Load the checkout providers
            await EnsureTargetQuoteSubmitProviderAsync(null).ConfigureAwait(false);
            // Referring Store
            await ProcessForSelectedStoreIDAsync(request).ConfigureAwait(false);
        }
    }
}
