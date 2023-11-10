// <copyright file="JBMTargetOrderCheckoutProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target order checkout provider class</summary>

namespace Clarity.Ecommerce.Providers.Checkouts.TargetOrder
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using Interfaces.Providers.Taxes;
    using Models;
    using Utilities;

    /// <summary>A target order checkout provider.</summary>
    /// <seealso cref="CheckoutProviderBase"/>
    public partial class JBMTargetOrderCheckoutProvider : TargetOrderCheckoutProvider
    {
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

        /// <summary>Checkout inner.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="analyzerResult">       The analyzer result.</param>
        /// <param name="user">                 The user.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">        The tax provider.</param>
        /// <param name="gateway">              The gateway.</param>
        /// <param name="selectedAccountID">    The selected account identifier.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{ICheckoutResult}.</returns>
        protected override async Task<ICheckoutResult> CheckoutInnerAsync(
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
                            accountID: selectedAccountID ?? user?.AccountID),
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
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // Update billing
            //var salesGroup = await context.SalesGroups
            //        .FilterByID(salesGroupResult.Result!.ID)
            //        .SingleOrDefaultAsync()
            //        .ConfigureAwait(false);
            //salesGroup.BillingContactID = originalCart.BillingContactID;
            //context.SalesGroups.Add(salesGroup);
            //await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            var salesGroup = salesGroupResult.Result;
            salesGroup!.BillingContact = originalCart.BillingContact;
            await Workflows.SalesGroups.UpdateAsync(salesGroup, contextProfileName).ConfigureAwait(false);
            salesGroupResult.Result = await Workflows.SalesGroups.GetAsync(salesGroup.ID, contextProfileName).ConfigureAwait(false);
            if (salesGroupResult.Result!.SalesOrderMasters.Any(m => string.IsNullOrWhiteSpace(m.CustomKey))
                || salesGroupResult.Result.SubSalesOrders.Any(s => string.IsNullOrWhiteSpace(s.CustomKey)))
            {
                salesGroupResult.Result = await CreateAndAddCustomKeysToTargetOrders(salesGroupResult.Result, contextProfileName);
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
            salesGroupResult.Result!.SalesOrderMasters!.First().ShippingContact = salesGroupResult.Result!.SubSalesOrders.First().ShippingContact;
            salesGroupResult.Result!.SalesOrderMasters!.First().ShippingContactID = salesGroupResult.Result!.SubSalesOrders.First().ShippingContactID;
            await HandleEmailsAsync(
                    checkout: checkout,
                    splits: salesGroupResult.Result!.SalesOrderMasters!,
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
            if (salesGroupResult.Result.SubSalesOrders.Any(x => x.SalesItems == null || x.SalesItems?.Count == 0))
            {
                foreach (var sub in salesGroupResult.Result.SubSalesOrders)
                {
                    sub.SalesItems = (await Workflows.SalesOrderItems
                            .SearchAsync(new SalesItemBaseSearchModel { MasterID = sub.ID }, true, contextProfileName)
                            .ConfigureAwait(false))
                        .results;
                }
            }
            var resp = await BuildFusionSalesOrderAndSendAsync(salesGroupResult.Result, contextProfileName, user!.ID, selectedAccountID);
            if (resp is not null)
            {
                result.ErrorMessages.Add(resp);
            }
            return result;
        }

        private async Task<ISalesGroupModel> CreateAndAddCustomKeysToTargetOrders(ISalesGroupModel salesGroup, string? contextProfileName)
        {
            foreach (var m in salesGroup.SalesOrderMasters!)
            {
                if (string.IsNullOrWhiteSpace(m.CustomKey))
                {
                    m.CustomKey = GenerateCustomKey(m.ID);
                    await Workflows.SalesOrders.UpdateAsync(m, contextProfileName);
                }
            }
            foreach (var s in salesGroup.SubSalesOrders!)
            {
                if (string.IsNullOrWhiteSpace(s.CustomKey))
                {
                    s.CustomKey = GenerateCustomKey(s.ID);
                    var shipping = await Workflows.Contacts.GetAsync((int)s.ShippingContactID!, contextProfileName);
                    s.ShippingContact ??= shipping;
                    await Workflows.SalesOrders.UpdateAsync(s, contextProfileName);
                }
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return salesGroup;
        }

        private string? GenerateCustomKey(int? ID)
        {
            if (ID is null)
            {
                return null;
            }
            return $"8{ID.ToString().PadLeft(6, '0')}";
        }
    }
}
