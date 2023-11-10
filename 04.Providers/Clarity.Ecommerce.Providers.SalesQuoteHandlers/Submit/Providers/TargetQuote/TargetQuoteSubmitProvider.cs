// <copyright file="TargetQuoteSubmitProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target quote submit provider class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Checkouts.TargetQuote
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A target quote submit provider.</summary>
    /// <seealso cref="SalesQuoteSubmitProviderBase"/>
    public partial class TargetQuoteSubmitProvider : SalesQuoteSubmitProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => TargetQuoteSubmitProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<ICheckoutResult> SubmitAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
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
            return await SubmitInnerAsync(
                    checkout: checkout,
                    analyzerResult: analyzerResult,
                    user: user,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<ICheckoutResult> SubmitAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
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
            return await SubmitInnerAsync(
                    checkout: checkout,
                    analyzerResult: analyzerResult,
                    user: user,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        private static async Task<(string originalCurrencyKey, string sellingCurrencyKey)> ProcessAndUpdateTotalsForCartAsync(
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
                item.OriginalCurrencyID = originalCurrencyID;
                item.SellingCurrencyID = sellingCurrencyID;
                item.UnitSoldPrice ??= item.UnitCorePrice;
                item.UnitCorePriceInSellingCurrency = null;
                item.UnitSoldPriceInSellingCurrency = null;
                item.ExtendedPriceInSellingCurrency = null;
            }
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
                analyzerResult.Result![1]!.Totals.Shipping = originalCart.Totals.Shipping;
            }
            return (originalCurrencyKey, sellingCurrencyKey);
        }

        private async Task<ICheckoutResult> SubmitInnerAsync(
            ICheckoutModel checkout,
            CEFActionResponse<List<ICartModel?>?> analyzerResult,
            IUserModel? user,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            var result = RegistryLoaderWrapper.GetInstance<ICheckoutResult>(contextProfileName);
            if (!analyzerResult.ActionSucceeded)
            {
                result.Succeeded = false;
                result.ErrorMessages = analyzerResult.Messages;
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
            var (originalCurrencyKey, sellingCurrencyKey) = await ProcessAndUpdateTotalsForCartAsync(
                    checkout,
                    analyzerResult,
                    user,
                    contextProfileName,
                    originalCart)
                .ConfigureAwait(false);
            var targetCarts = analyzerResult.Result!.Skip(1);
            var salesGroupResult = await BuildSalesGroupFromTargetCartsAsync(
                    checkout: checkout,
                    originalCart: originalCart,
                    targetCarts: targetCarts,
                    user: user,
                    pricingFactoryContext: pricingFactoryContext,
                    originalCurrencyKey: originalCurrencyKey,
                    sellingCurrencyKey: sellingCurrencyKey,
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
            var quoteIDs = salesGroupResult.Result!.SalesQuoteRequestSubs!.Select(x => x.ID).ToList();
            quoteIDs.Insert(0, salesGroupResult.Result!.SalesQuoteRequestMasters!.Single().ID);
            // ReSharper restore PossibleInvalidOperationException
            result.QuoteIDs = quoteIDs.ToArray();
            result.Succeeded = true;
            await HandleEmailsAsync(
                    splits: salesGroupResult.Result!.SalesQuoteRequestSubs!,
                    result: result,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await HandleAddressBookAsync(
                    checkout: checkout,
                    user: user,
                    nothingToShip: originalCart.SalesItems!.All(x => x.ProductNothingToShip),
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return result;
        }
    }
}
