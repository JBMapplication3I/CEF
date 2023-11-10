// <copyright file="Service.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the service class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Checkouts.Services
{
    using System;
    using System.Threading.Tasks;
    using Endpoints;
    using Models;
    using Service;

    /// <summary>A sample request checkout service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    public class SampleRequestCheckoutService : ClarityEcommerceServiceBase
    {
        /// <summary>Post handler for the SampleRequestCheckout endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{CheckoutResult}.</returns>
#pragma warning disable IDE1006 // Naming Styles
        public async Task<object> Post(SampleRequestCheckout request)
#pragma warning restore IDE1006 // Naming Styles
        {
            // Verify the account is not on hold
            var checkoutResult1 = await DoAccountOnHoldCheckAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (checkoutResult1 != null)
            {
                return checkoutResult1;
            }
            var typeName = request.WithCartInfo?.CartTypeName ?? "Samples Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: new(
                        typeKey: typeName,
                        sessionID: await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false),
                        userID: CurrentUserID,
                        accountID: await LocalAdminAccountIDAsync(CurrentAccountID).ConfigureAwait(false)),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (updatedSessionID != null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            request.WithCartInfo ??= new();
            request.WithCartInfo.CartSessionID = await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false);
            request.WithUserInfo ??= new();
            request.WithUserInfo.ExternalUserID ??= CurrentUserName;
            var checkoutResult = await RegistryLoaderWrapper.GetSampleRequestCheckoutProvider(null)!.CheckoutAsync(
                    checkout: request,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    cartType: typeName,
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    isTaxable: (await CurrentAccountAsync().ConfigureAwait(false))?.IsTaxable ?? true,
                    currentUserID: CurrentUserID,
                    currentAccountID: await LocalAdminAccountIDAsync(CurrentAccountID).ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (checkoutResult.OrderID == null || !checkoutResult.Succeeded)
            {
                // There was an error and the error message as put into ErrorMessage
                checkoutResult.Succeeded = false;
                return (CheckoutResult)checkoutResult;
            }
            await ClearSessionSampleCartGuidAsync().ConfigureAwait(false);
            _ = await Workflows.SampleRequests.GetAsync(checkoutResult.OrderID.Value, contextProfileName: null).ConfigureAwait(false);
            try
            {
                ////Workflows.EmailQueues.SendSampleRequestConfirmedNotification(samples);
            }
            catch (Exception)
            {
                checkoutResult.WarningMessage ??= string.Empty;
                checkoutResult.WarningMessage += "There was an error sending the customer samples confirmation.\r\n";
            }
            try
            {
                ////Workflows.EmailQueues.SendSampleRequestInternalNotification(samples, checkoutResult.PaymentTransactionID);
            }
            catch (Exception)
            {
                checkoutResult.WarningMessage ??= string.Empty;
                checkoutResult.WarningMessage += "There was an error sending the back-office samples confirmation.\r\n";
            }
            return (CheckoutResult)checkoutResult;
        }
    }
}
