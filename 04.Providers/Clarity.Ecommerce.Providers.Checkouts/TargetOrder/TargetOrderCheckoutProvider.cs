// <copyright file="TargetOrderCheckoutProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target order checkout provider class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Checkouts.TargetOrder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A target order checkout provider.</summary>
    /// <seealso cref="CheckoutProviderBase"/>
    public partial class TargetOrderCheckoutProvider : CheckoutProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => TargetOrderCheckoutProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
            int? selectedAccountID,
            string? contextProfileName)
        {
            var resolveUserResult = await TryResolveUserAsync(
                    Contract.RequiresNotNull(checkout),
                    contextProfileName)
                .ConfigureAwait(false);
            if (!resolveUserResult.ActionSucceeded
                && resolveUserResult.Messages.Any(x => x.StartsWith("ERROR!")))
            {
                return new CheckoutResult
                {
                    Succeeded = false,
                    ErrorMessages = resolveUserResult.Messages,
                };
            }
            var user = resolveUserResult.ActionSucceeded ? resolveUserResult.Result : null;
            EnforceUserInPricingFactoryContextIfSet(pricingFactoryContext, user);
            var analyzerResult = await AnalyzeAsync(
                    checkout: checkout,
                    pricingFactoryContext: pricingFactoryContext,
                    lookupKey: lookupKey,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return await CheckoutInnerAsync(
                    checkout: checkout,
                    analyzerResult: analyzerResult,
                    user: user,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    gateway: gateway,
                    selectedAccountID: selectedAccountID,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
            int? selectedAccountID,
            string? contextProfileName)
        {
            var resolveUserResult = await TryResolveUserAsync(
                    Contract.RequiresNotNull(checkout),
                    contextProfileName)
                .ConfigureAwait(false);
            if (!resolveUserResult.ActionSucceeded && resolveUserResult.Messages.Any(x => x.StartsWith("ERROR!")))
            {
                return new CheckoutResult
                {
                    Succeeded = false,
                    ErrorMessages = resolveUserResult.Messages,
                };
            }
            var user = resolveUserResult.ActionSucceeded ? resolveUserResult.Result : null;
            EnforceUserInPricingFactoryContextIfSet(pricingFactoryContext, user);
            var analyzerResult = await AnalyzeAsync(
                    checkout: checkout,
                    lookupKey: lookupKey,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return await CheckoutInnerAsync(
                    checkout: checkout,
                    analyzerResult: analyzerResult,
                    user: user,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    gateway: gateway,
                    selectedAccountID: selectedAccountID,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Process the and update totals for cart.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="analyzerResult">    The analyzer result.</param>
        /// <param name="user">              The user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="originalCart">      The original cart.</param>
        /// <returns>A Task{(string originalCurrencyKey,string sellingCurrencyKey,CheckoutResult shouldFail)}.</returns>
        protected static async Task<(string originalCurrencyKey, string sellingCurrencyKey, CheckoutResult shouldFail)> ProcessAndUpdateTotalsForCartAsync(
            ICheckoutModel checkout,
            CEFActionResponse<List<ICartModel?>?> analyzerResult,
            IUserModel? user,
            string? contextProfileName,
            ICartModel originalCart)
        {
            var originalCurrencyKey = Contract.RequiresValidKey(CEFConfigDictionary.DefaultCurrency);
            var originalCurrencyID = Contract.RequiresValidID(
                await Workflows.Currencies.CheckExistsAsync(
                    originalCurrencyKey,
                    contextProfileName)
                .ConfigureAwait(false));
            // Gets the currency key from one of three places in order of priority: checkout/wallet -> user -> cookie
            var sellingCurrencyKey = Contract.RequiresValidKey(
                checkout.CurrencyKey ?? user?.CurrencyKey ?? CEFConfigDictionary.DefaultCurrency);
            var sellingCurrencyID = sellingCurrencyKey == originalCurrencyKey
                ? originalCurrencyID
                : Contract.RequiresValidID(
                    await Workflows.Currencies.CheckExistsAsync(
                        sellingCurrencyKey,
                        contextProfileName)
                    .ConfigureAwait(false));
            checkout.CurrencyKey = sellingCurrencyKey;
            foreach (var item in originalCart.SalesItems!)
            {
                var subResult = await VerifySubscriptionDoesntExist(item, user?.ID, contextProfileName).ConfigureAwait(false);
                if (!subResult.ActionSucceeded)
                {
                    return (
                        originalCurrencyKey,
                        sellingCurrencyKey,
                        new()
                        {
                            Succeeded = false,
                            ErrorMessages = subResult.Messages,
                        });
                }
                item.OriginalCurrencyID = originalCurrencyID;
                item.SellingCurrencyID = sellingCurrencyID;
                item.UnitSoldPrice = GetModifiedValue(
                    item.UnitSoldPrice ?? item.UnitCorePrice,
                    item.UnitSoldPriceModifier,
                    item.UnitSoldPriceModifierMode);
                if (originalCurrencyID == sellingCurrencyID)
                {
                    item.UnitCorePriceInSellingCurrency = null;
                    item.UnitSoldPriceInSellingCurrency = null;
                    item.ExtendedPriceInSellingCurrency = null;
                    continue;
                }
                item.UnitCorePriceInSellingCurrency = await Workflows.Currencies.ConvertAsync(
                        originalCurrencyKey,
                        sellingCurrencyKey,
                        item.UnitCorePrice,
                        contextProfileName)
                    .ConfigureAwait(false);
                item.UnitSoldPriceInSellingCurrency = await Workflows.Currencies.ConvertAsync(
                        originalCurrencyKey,
                        sellingCurrencyKey,
                        item.UnitSoldPrice.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
                item.ExtendedPriceInSellingCurrency = await Workflows.Currencies.ConvertAsync(
                        originalCurrencyKey,
                        sellingCurrencyKey,
                        item.ExtendedPrice,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            originalCart.Totals!.Discounts = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(originalCart.Totals.Discounts, originalCart.SubtotalDiscountsModifier, originalCart.SubtotalDiscountsModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            originalCart.Totals.Fees = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(originalCart.Totals.Fees, originalCart.SubtotalFeesModifier, originalCart.SubtotalFeesModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            originalCart.Totals.Handling = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(originalCart.Totals.Handling, originalCart.SubtotalHandlingModifier, originalCart.SubtotalHandlingModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            originalCart.Totals.Shipping = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(originalCart.Totals.Shipping, originalCart.SubtotalShippingModifier, originalCart.SubtotalShippingModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            originalCart.Totals.SubTotal = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    originalCart.Totals.SubTotal,
                    contextProfileName)
                .ConfigureAwait(false);
            originalCart.Totals.Tax = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(originalCart.Totals.Tax, originalCart.SubtotalTaxesModifier, originalCart.SubtotalTaxesModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            var newMasterTotals = RegistryLoaderWrapper.GetInstance<ICartTotals>(contextProfileName);
            newMasterTotals.SubTotal = originalCart.Totals.SubTotal;
            newMasterTotals.Handling = originalCart.Totals.Handling;
            newMasterTotals.Fees = originalCart.Totals.Fees;
            newMasterTotals.Tax = originalCart.Totals.Tax;
            newMasterTotals.Discounts = originalCart.Totals.Discounts;
            foreach (var x in analyzerResult.Result!.Skip(1).Where(x => x != null))
            {
                // Get the data if it's not already loaded
                if (Contract.CheckAnyValidID(x!.RateQuotes?.Select(y => y.ID).ToArray() ?? Array.Empty<int>()))
                {
                    continue;
                }
                x.RateQuotes = (await Workflows.RateQuotes.SearchAsync(
                            search: new RateQuoteSearchModel
                            {
                                Active = true,
                                CartID = x.ID,
                                Selected = true,
                            },
                            asListing: true,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false))
                    .results;
            }
            newMasterTotals.Shipping = 0m;
            foreach (var cart in analyzerResult.Result!.Skip(1).Where(x => x != null))
            {
                var rateQuotes = cart!.RateQuotes
                    ?.Where(x => x.Active && x.Selected)
                    .OrderByDescending(x => x.UpdatedDate ?? x.CreatedDate)
                    .ToArray();
                if (Contract.CheckEmpty(rateQuotes))
                {
                    continue;
                }
                newMasterTotals.Shipping += rateQuotes!.First().Rate ?? 0m;
            }
            var doSingleShippingAssignment = false;
            if (newMasterTotals.Shipping <= 0m && originalCart.Totals.Shipping > 0m)
            {
                // Single destination shipping assignment
                newMasterTotals.Shipping = originalCart.Totals.Shipping;
                doSingleShippingAssignment = true;
            }
            originalCart.Totals = newMasterTotals;
            await Workflows.Carts.SetCartTotalsAsync(
                    lookupKey: CartByIDLookupKey.FromCart(originalCart),
                    totals: originalCart.Totals,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (doSingleShippingAssignment)
            {
                // Single destination shipping rate assignment
                analyzerResult.Result![1]!.Totals!.Shipping = originalCart.Totals.Shipping;
            }
            return (originalCurrencyKey, sellingCurrencyKey, new() { Succeeded = true });
        }

        /// <summary>Checkout inner.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="analyzerResult">       The analyzer result.</param>
        /// <param name="user">                 The user.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">        The tax provider.</param>
        /// <param name="gateway">              The gateway.</param>
        /// <param name="selectedAccountID">     The current account identifier.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{ICheckoutResult}.</returns>
        protected virtual async Task<ICheckoutResult> CheckoutInnerAsync(
            ICheckoutModel checkout,
            CEFActionResponse<List<ICartModel?>?> analyzerResult,
            IUserModel? user,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
            int? selectedAccountID,
            string? contextProfileName)
        {
            var result = RegistryLoaderWrapper.GetInstance<ICheckoutResult>(contextProfileName);
            if (!analyzerResult.ActionSucceeded)
            {
                result.Succeeded = false;
                result.ErrorMessages = analyzerResult.Messages;
                return result;
            }
            var useWalletResult = await TryToUseWalletIfSetAsync(
                    checkout,
                    pricingFactoryContext.UserID,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!useWalletResult.ActionSucceeded)
            {
                result.Succeeded = false;
                result.ErrorMessages = useWalletResult.Messages;
                return result;
            }
            // Update the totals to include all the separate shipping rate quotes and then send the original cart for
            // update with any new info.
            var originalCart = analyzerResult.Result![0]!;
            var updatedOriginalCart = (await TryResolveCartAsync(
                        checkout: checkout,
                        pricingFactoryContext: pricingFactoryContext,
                        lookupKey: new(
                            sessionID: originalCart.SessionID!.Value,
                            typeKey: originalCart.TypeName!,
                            userID: user?.ID,
                            accountID: user?.AccountID),
                        taxesProvider: taxesProvider,
                        validate: true,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false))
                .Result!;
            originalCart = updatedOriginalCart;
            var (originalCurrencyKey, sellingCurrencyKey, shouldFail) = await ProcessAndUpdateTotalsForCartAsync(
                    checkout: checkout,
                    analyzerResult: analyzerResult,
                    user: user,
                    contextProfileName: contextProfileName,
                    originalCart: originalCart)
                .ConfigureAwait(false);
            if (!shouldFail.Succeeded)
            {
                return shouldFail;
            }
            /* Since we narrowed what we are updating above, we shouldn't need to do a full pull again.
             * Cart Item Discounts were getting lost
            updatedOriginalCart = (await TryResolveCartAsync(
                        checkout: checkout,
                        pricingFactoryContext: pricingFactoryContext,
                        cartType: originalCart.TypeName,
                        taxesProvider: taxesProvider,
                        userID: user?.ID,
                        validate: true,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false))
                .Result;
            originalCart = updatedOriginalCart;
            originalCart.Totals = newMasterTotals;
            */
            // Start trying to process payment
            if (checkout.PaymentStyle == Enums.PaymentMethodsStrings.Payoneer
                && !await ValidatePayoneerReadyForOrderAsync(
                        storeID: analyzerResult.Result.Skip(1).FirstOrDefault(x => Contract.CheckValidID(x!.StoreID))?.StoreID,
                        buyer: user!,
                        paymentStyle: checkout.PaymentStyle,
                        overridePayoneerAccountID: checkout.PayByPayoneer?.PayoneerAccountID,
                        overridePayoneerCustomerID: checkout.PayByPayoneer?.PayoneerCustomerID,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false))
            {
                throw new InvalidOperationException(
                    "Cannot validate that the order to be created is ready for use in Payoneer");
            }
            CEFActionResponse<IProcessPaymentResponse>? processPaymentResult = null;
            if (checkout.PaymentStyle != Enums.PaymentMethodsStrings.Invoice)
            {
                processPaymentResult = await ProcessPaymentAsync(
                        checkout: checkout,
                        cart: originalCart,
                        gateway: gateway,
                        originalCurrencyKey: originalCurrencyKey,
                        sellingCurrencyKey: sellingCurrencyKey,
                        paymentAlreadyConverted: false, // TODO: Based on multi-currency value status
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (!processPaymentResult.ActionSucceeded)
                {
                    result.Succeeded = false;
                    result.ErrorMessages = processPaymentResult.Messages;
                    return result;
                }
            }
            var targetCarts = analyzerResult.Result!.Skip(1);
            var salesGroupResult = await BuildSalesGroupFromTargetCartsAsync(
                    checkout: checkout,
                    originalCart: originalCart,
                    targetCarts: targetCarts,
                    user: user,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    processPaymentResponse: processPaymentResult ?? new(),
                    originalCurrencyKey: originalCurrencyKey,
                    sellingCurrencyKey: sellingCurrencyKey,
                    selectedAccountID: selectedAccountID,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!salesGroupResult.ActionSucceeded)
            {
                result.Succeeded = false;
                result.ErrorMessages = salesGroupResult.Messages;
                return result;
            }
            // Remove the carts as they are no longer needed, including the target grouping type keys
            var typeIDs = analyzerResult.Result!.Skip(1).Where(x => x != null).Select(x => x!.TypeID);
            var cartIDs = analyzerResult.Result!.Where(x => Contract.CheckValidID(x?.ID)).Select(x => x!.ID);
            foreach (var cartID in cartIDs)
            {
                try
                {
                    await Workflows.Carts.DeleteAsync(cartID, contextProfileName).ConfigureAwait(false);
                }
                catch
                {
                    // Do Nothing
                }
            }
            foreach (var typeID in typeIDs)
            {
                try
                {
                    await Workflows.CartTypes.DeleteAsync(typeID, contextProfileName).ConfigureAwait(false);
                }
                catch
                {
                    // Do Nothing
                }
            }
            // ReSharper disable PossibleInvalidOperationException
            var orderIDs = salesGroupResult.Result!.SubSalesOrders!.Select(x => x.ID).ToList();
            orderIDs.Insert(0, salesGroupResult.Result!.SalesOrderMasters!.Single().ID);
            // ReSharper restore PossibleInvalidOperationException
            result.OrderIDs = orderIDs.ToArray();
            result.Succeeded = true;
            await HandleEmailsAsync(
                    checkout: checkout,
                    splits: salesGroupResult.Result!.SubSalesOrders!,
                    result: result,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await HandleAddressBookAsync(
                    checkout: checkout,
                    user: user,
                    nothingToShip: originalCart.SalesItems!.All(x => x.ProductNothingToShip),
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await HandleWalletAsync(
                    user: user,
                    checkout: checkout,
                    gateway: gateway,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            foreach (var order in salesGroupResult.Result.SubSalesOrders!)
            {
                await ProcessMembershipsForOrderAsync(
                        salesOrder: order,
                        pricingFactoryContext: pricingFactoryContext,
                        invoiceID: null,
                        timestamp: salesGroupResult.Result.CreatedDate,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // ReSharper disable once UnusedVariable
                var subscriptionsResult = await ProcessSubscriptionsForOrderAsync(
                        salesOrder: order,
                        pricingFactoryContext: pricingFactoryContext,
                        salesGroupID: salesGroupResult.Result.ID,
                        invoiceID: null,
                        timestamp: salesGroupResult.Result.CreatedDate,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                ////if (subscriptionsResult.Messages.Count > 0)
                ////{
                ////    result.WarningMessage += subscriptionsResult.Messages.Aggregate((c, n) => $"{c}\r\n{n}");
                ////    // checkout.WarningMessages.AddRange(subscriptionsResult.Messages);
                ////}
            }
            return result;
        }
    }
}
