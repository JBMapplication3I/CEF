// <copyright file="SingleOrderCheckoutService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the single order checkout service class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Checkouts;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Service;
    using ServiceStack;
    using SingleOrder;
    using Utilities;

    /// <summary>The process current cart to single order.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, UsedInStorefront,
     Route("/Providers/Checkout/ProcessCurrentCartToSingleOrder", "POST",
        Summary = "Checkout the current shopping cart")]
    public class ProcessCurrentCartToSingleOrder : CheckoutModel, IReturn<CheckoutResult>
    {
    }

    /// <summary>The process specific cart to single order.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, UsedInAdmin,
     Route("/Providers/Checkout/ProcessSpecificCartToSingleOrder", "POST",
        Summary = "Checkout the specific shopping cart")]
    public class ProcessSpecificCartToSingleOrder : CheckoutModel, IReturn<CheckoutResult>
    {
    }

    /// <summary>A single order checkout service.</summary>
    /// <seealso cref="CheckoutService"/>
    public partial class CheckoutService
    {
        /// <summary>Gets or sets the single order checkout provider.</summary>
        /// <value>The single order checkout provider.</value>
        private static ICheckoutProviderBase SingleOrderCheckoutProvider { get; set; } = null!;

        /// <summary>Reacts to a POST attempt for the Cart Checkout endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
        public async Task<object> Post(ProcessCurrentCartToSingleOrder request)
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
            ICheckoutResult result;
            if (request.PaymentStyle == Enums.PaymentMethodsStrings.PayPal)
            {
                result = await PayPalCheckoutProvider.CheckoutAsync(
                        checkout: request,
                        pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                        lookupKey: await GenSessionLookupKeyAsync(request.WithCartInfo!.CartTypeName!).ConfigureAwait(false),
                        taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                        gateway: null,
                        selectedAccountID: null,
                        contextProfileName: null)
                    .ConfigureAwait(false);
            }
            else
            {
                result = await SingleOrderCheckoutProvider.CheckoutAsync(
                        checkout: request,
                        pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                        lookupKey: await GenSessionLookupKeyAsync(request.WithCartInfo!.CartTypeName!).ConfigureAwait(false),
                        taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                        gateway: RegistryLoaderWrapper.GetPaymentProvider(contextProfileName: null),
                        selectedAccountID: null,
                        contextProfileName: null)
                    .ConfigureAwait(false);
            }
            return await ValidateAndCompleteCheckoutAsync(
                    failCondition: !result.Succeeded || !Contract.CheckValidID(result.OrderID),
                    result: result)
                .ConfigureAwait(false);
        }

        /// <summary>Reacts to a POST attempt for the Cart Checkout endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
        public async Task<object> Post(ProcessSpecificCartToSingleOrder request)
        {
            // Required Setup
            await SingleSetupForOtherAsync(request).ConfigureAwait(false);
            // Run the checkout procedures
            ICheckoutResult result;
            var lookupKey = await GenCartByIDLookupKeyAsync(
                    cartID: Contract.RequiresValidID(request.WithCartInfo?.CartID),
                    userID: Contract.RequiresValidID(request.WithUserInfo!.UserID),
                    accountID: await Workflows.Accounts.GetIDByUserIDAsync(request.WithUserInfo!.UserID!.Value, null).ConfigureAwait(false))
                .ConfigureAwait(false);
            if (request.PaymentStyle == Enums.PaymentMethodsStrings.PayPal)
            {
                result = await PayPalCheckoutProvider.CheckoutAsync(
                        checkout: request,
                        lookupKey: lookupKey,
                        pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                        taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                        gateway: null,
                        selectedAccountID: null,
                        contextProfileName: null)
                    .ConfigureAwait(false);
            }
            else
            {
                result = await SingleOrderCheckoutProvider.CheckoutAsync(
                        checkout: request,
                        lookupKey: lookupKey,
                        pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                        taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                        gateway: RegistryLoaderWrapper.GetPaymentProvider(contextProfileName: null),
                        selectedAccountID: null,
                        contextProfileName: null)
                    .ConfigureAwait(false);
            }
            return await ValidateAndCompleteCheckoutAsync(
                    failCondition: !result.Succeeded || !Contract.CheckValidID(result.OrderID),
                    result: result)
                .ConfigureAwait(false);
        }

        /// <summary>Ensures that single order checkout provider exists and is initialized.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task EnsureSingleOrderCheckoutProviderAsync(string? contextProfileName)
        {
            if (OrderTypeID == 0)
            {
                await SetupIDsAsync(contextProfileName).ConfigureAwait(false);
            }
            SingleOrderCheckoutProvider ??= RegistryLoaderWrapper.GetInstance<ICheckoutProviderBase>(contextProfileName);
            if (SingleOrderCheckoutProvider.IsInitialized)
            {
                return;
            }
            await SingleOrderCheckoutProvider.InitAsync(
                    orderStatusPendingID: OrderStatusPendingID,
                    orderStatusPaidID: OrderStatusPaidID,
                    orderStatusOnHoldID: OrderStatusOnHoldID,
                    orderTypeID: OrderTypeID,
                    orderStateID: OrderStateID,
                    billingTypeID: BillingTypeID,
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
        private async Task SingleSetupForSelfAsync(ICheckoutModel request)
        {
            await EnsureSingleOrderCheckoutProviderAsync(null).ConfigureAwait(false);
#if PAYPAL
            if (request.PaymentStyle == Enums.PaymentMethodsStrings.PayPal)
            {
                await EnsurePayPalCheckoutProviderAsync(null).ConfigureAwait(false);
            }
#endif
            var typeName = request.WithCartInfo?.CartTypeName ?? "Cart";
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
        }

        /// <summary>Sets up for other.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task.</returns>
        private async Task SingleSetupForOtherAsync(ICheckoutModel request)
        {
            // Load the checkout providers
            await EnsureSingleOrderCheckoutProviderAsync(null).ConfigureAwait(false);
            // Referring Store
            await ProcessForSelectedStoreIDAsync(request).ConfigureAwait(false);
        }
    }
}
