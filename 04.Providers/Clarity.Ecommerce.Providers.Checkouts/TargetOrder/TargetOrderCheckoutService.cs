// <copyright file="TargetOrderCheckoutService.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target order checkout service class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Checkouts;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>Analyze the current cart and build target carts for it based on the internal rules.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_List_CartModel}"/>
    [PublicAPI,
        UsedInStorefront,
        RequiredRole("Supervisor"),
        Route("/Providers/Checkout/AnalyzeCurrentCartToTargetCarts", "POST",
            Summary = "Analyze the current cart and build target carts for it based on the internal rules.")]
    public class AnalyzeCurrentCartToTargetCarts : CheckoutModel, IReturn<CEFActionResponse<List<CartModel>>>
    {
        /// <summary>Gets or sets a value indicating whether the reset analysis.</summary>
        /// <value>True if reset analysis, false if not.</value>
        [DefaultValue(false),
            ApiMember(Name = nameof(ResetAnalysis), DataType = "int?", ParameterType = "body", IsRequired = false,
                Description = "Reset the analysis results so any previous target setups are removed.")]
        public bool ResetAnalysis { get; set; }
    }

    /// <summary>Analyze the specific cart and build target carts for it based on the internal rules.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_List_CartModel}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        RequiredRole("Supervisor"),
        Route("/Providers/Checkout/AnalyzeSpecificCartToTargetCarts", "POST",
            Summary = "Analyze the specific cart and build target carts for it based on the internal rules.")]
    public class AnalyzeSpecificCartToTargetCarts : CheckoutModel, IReturn<CEFActionResponse<List<CartModel>>>
    {
        /// <summary>Gets or sets a value indicating whether the reset analysis.</summary>
        /// <value>True if reset analysis, false if not.</value>
        [DefaultValue(false),
            ApiMember(Name = nameof(ResetAnalysis), DataType = "int?", ParameterType = "body", IsRequired = false,
                Description = "Reset the analysis results so any previous target setups are removed.")]
        public bool ResetAnalysis { get; set; }
    }

    /// <summary>Clear the current target carts and start over.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        UsedInStorefront,
        RequiredRole("Supervisor"),
        Route("/Providers/Checkout/ClearCurrentCartToTargetCartsAnalysis", "DELETE",
            Summary = "Clear the current target carts and start over.")]
    public class ClearCurrentCartToTargetCartsAnalysis : CheckoutModel, IReturn<CEFActionResponse>
    {
    }

    /// <summary>Checkout the current cart of the given session type.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI,
        UsedInStorefront,
        RequiredRole("Supervisor"),
        Route("/Providers/Checkout/ProcessCurrentCartToTargetOrders", "POST",
            Summary = "Checkout the current cart of the given session type.")]
    public class ProcessCurrentCartToTargetOrders : CheckoutModel, IReturn<CheckoutResult>
    {
    }

    /// <summary>Checkout the specific cart of the given cart identifier.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI,
        UsedInAdmin,
        Authenticate,
        RequiredRole("Supervisor"),
        Route("/Providers/Checkout/ProcessSpecificCartToTargetOrders", "POST",
            Summary = "Checkout the specific cart of the given cart identifier.")]
    public class ProcessSpecificCartToTargetOrders : CheckoutModel, IReturn<CheckoutResult>
    {
    }

    /// <summary>A target order checkout service.</summary>
    /// <seealso cref="CheckoutService"/>
    public partial class CheckoutService
    {
        /// <summary>Gets or sets target order checkout provider.</summary>
        /// <value>The target order checkout provider.</value>
        private static ICheckoutProviderBase? TargetOrderCheckoutProvider { get; set; }

        /// <summary>Reacts to a POST attempt for the Cart Checkout endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
        public async Task<object> Post(AnalyzeCurrentCartToTargetCarts request)
        {
            // Verify the account is not on hold
            var result1 = await DoAccountOnHoldCheckAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (result1 is not null)
            {
                return CEFAR.FailingCEFAR<List<CartModel>>(result1.ErrorMessage);
            }
            // Required Setup
            await TargetsSetupForSelfAsync(request).ConfigureAwait(false);
            var taxesProvider = await GetTaxProviderAsync().ConfigureAwait(false);
            var pricingFactoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(request.WithCartInfo!.CartTypeName!).ConfigureAwait(false);
            lookupKey.AltAccountID = await LocalAdminAccountIDAsync(CurrentAccountID).ConfigureAwait(false);
            // Reset if requested
            if (request.ResetAnalysis)
            {
                await TargetOrderCheckoutProvider!.ClearAnalysisAsync(
                        checkout: request,
                        pricingFactoryContext: pricingFactoryContext,
                        lookupKey: lookupKey,
                        taxesProvider: taxesProvider,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false);
            }
            // Run the checkout procedures
            var results = new CEFActionResponse<List<ICartModel?>?>[5];
            for (var i = 0; i < 3; i++)
            {
                results[i] = (await TargetOrderCheckoutProvider!.AnalyzeAsync(
                        checkout: request,
                        pricingFactoryContext: pricingFactoryContext,
                        lookupKey: lookupKey,
                        taxesProvider: taxesProvider,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false))!;
                if (i == 0)
                {
                    // continue;
                }
                // TODO: Check for material difference from previous run
            }
            return results.Last(x => x is not null)!.ChangeCEFARListType<ICartModel?, CartModel>();
        }

        /// <summary>Reacts to a POST attempt for the Cart Checkout endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
        public async Task<object> Post(ProcessCurrentCartToTargetOrders request)
        {
            // Verify the account is not on hold
            var result1 = await DoAccountOnHoldCheckAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (result1 is not null)
            {
                return result1;
            }
            int? selectedAccountID = await LocalAdminAccountIDOrThrow401Async(CurrentAccountIDOrThrow401).ConfigureAwait(false);
            int userID = await SelectedUserOrCurrentUserOrThrow401Async(CurrentUserIDOrThrow401).ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var userInfo = await context.Users
                .AsNoTracking()
                .FilterByID(userID)
                .Select(u => new { u.AccountID, AccountKey = u.Account!.CustomKey, u.UserName })
                .SingleAsync()
                .ConfigureAwait(false);
            request.WithUserInfo!.UserID = userID;
            request.WithUserInfo.UserName = userInfo.UserName;
            // Required Setup
            await TargetsSetupForSelfAsync(request).ConfigureAwait(false);
            // Run the checkout procedures
            var lookupKey = await GenSessionLookupKeyAsync(request.WithCartInfo!.CartTypeName!).ConfigureAwait(false);
            lookupKey.AltAccountID = await LocalAdminAccountIDAsync(CurrentAccountID);
            var result = await Contract.RequiresNotNull(TargetOrderCheckoutProvider).CheckoutAsync(
                    checkout: request,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    lookupKey: lookupKey,
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    gateway: RegistryLoaderWrapper.GetPaymentProvider(ServiceContextProfileName),
                    selectedAccountID: selectedAccountID,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            return await ValidateAndCompleteCheckoutAsync(
                    failCondition: !result.Succeeded || result.OrderIDs?.Any(x => Contract.CheckValidID(x)) != true,
                    result: result)
                .ConfigureAwait(false);
        }

        /// <summary>Reacts to a DELETE attempt for the clear analysis endpoint.</summary>
        /// <param name="request">The request to delete.</param>
        /// <returns>A Task{object}</returns>
        public async Task<object> Delete(ClearCurrentCartToTargetCartsAnalysis request)
        {
            await TargetsSetupForSelfAsync(request).ConfigureAwait(false);
            SessionCartBySessionAndTypeLookupKey lookupKey = await GenSessionLookupKeyAsync(request.WithCartInfo!.CartTypeName!).ConfigureAwait(false);
            lookupKey.AltAccountID = await LocalAdminAccountIDOrThrow401Async(CurrentAccountID).ConfigureAwait(false);
            return await Contract.RequiresNotNull(TargetOrderCheckoutProvider).ClearAnalysisAsync(
                    checkout: request,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    lookupKey: lookupKey,
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Reacts to a POST attempt for the Cart Checkout endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
        public async Task<object> Post(AnalyzeSpecificCartToTargetCarts request)
        {
            // Required Setup
            await TargetsSetupForOtherAsync(request).ConfigureAwait(false);
            var lookupKey = await GenCartByIDLookupKeyAsync(
                    cartID: Contract.RequiresValidID(request.WithCartInfo?.CartID),
                    userID: Contract.RequiresValidID(request.WithUserInfo!.UserID),
                    accountID: await Workflows.Accounts.GetIDByUserIDAsync(request.WithUserInfo.UserID!.Value, ServiceContextProfileName).ConfigureAwait(false))
                .ConfigureAwait(false);
            // Reset if requested
            if (request.ResetAnalysis)
            {
                await Contract.RequiresNotNull(TargetOrderCheckoutProvider).ClearAnalysisAsync(
                        checkout: request,
                        pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                        lookupKey: lookupKey,
                        taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false);
            }
            // Run the checkout procedures
            var result = await Contract.RequiresNotNull(TargetOrderCheckoutProvider).AnalyzeAsync(
                    checkout: request,
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            return result!.ChangeCEFARListType<ICartModel?, CartModel>();
        }

        /// <summary>Reacts to a POST attempt for the Cart Checkout endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
        public async Task<object> Post(ProcessSpecificCartToTargetOrders request)
        {
            // Required Setup
            await TargetsSetupForOtherAsync(request).ConfigureAwait(false);
            var lookupKey = await GenCartByIDLookupKeyAsync(
                    cartID: Contract.RequiresValidID(request.WithCartInfo?.CartID),
                    userID: Contract.RequiresValidID(request.WithUserInfo!.UserID),
                    accountID: await Workflows.Accounts.GetIDByUserIDAsync(request.WithUserInfo.UserID!.Value, ServiceContextProfileName).ConfigureAwait(false))
                .ConfigureAwait(false);
            // Run the checkout procedures
            var result = await Contract.RequiresNotNull(TargetOrderCheckoutProvider).CheckoutAsync(
                    checkout: request,
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    gateway: RegistryLoaderWrapper.GetPaymentProvider(ServiceContextProfileName),
                    selectedAccountID: await LocalAdminAccountIDAsync(CurrentAccountID).ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            return await ValidateAndCompleteCheckoutAsync(
                    failCondition: !result.Succeeded || result.OrderIDs?.Any(x => Contract.CheckValidID(x)) != true,
                    result: result)
                .ConfigureAwait(false);
        }

        /// <summary>Ensures that target order checkout provider exists and is initialized.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task EnsureTargetOrderCheckoutProviderAsync(string? contextProfileName)
        {
            if (OrderTypeID == 0)
            {
                await SetupIDsAsync(contextProfileName).ConfigureAwait(false);
            }
            TargetOrderCheckoutProvider ??= RegistryLoaderWrapper.GetInstance<ICheckoutProviderBase>(contextProfileName);
            if (TargetOrderCheckoutProvider.IsInitialized)
            {
                return;
            }
            await TargetOrderCheckoutProvider.InitAsync(
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
        private async Task TargetsSetupForSelfAsync(ICheckoutModel request)
        {
            // Load the checkout providers
            await EnsureTargetOrderCheckoutProviderAsync(ServiceContextProfileName).ConfigureAwait(false);
#if PAYPAL
            await EnsurePayPalCheckoutProviderAsync(ServiceContextProfileName).ConfigureAwait(false);
#endif
            var typeName = request.WithCartInfo?.CartTypeName ?? "Cart";
            // Resolve the correct session ID
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
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
            await EnsureTargetOrderCheckoutProviderAsync(ServiceContextProfileName).ConfigureAwait(false);
            // Referring Store
            await ProcessForSelectedStoreIDAsync(request).ConfigureAwait(false);
        }
    }
}
