// <copyright file="PayPalCheckoutService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayPal checkout service class</summary>
#if PAYPAL && NET5_0_OR_GREATER // PayPal package used doesn't have net5+ versions
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using System;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using PayPalInt;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>An initiate PayPal for current cart.</summary>
    /// <seealso cref="CheckoutModel"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin,
     Route("/Providers/Checkout/InitiatePayPalForCurrentCart", "POST",
        Summary = "Checkout the current shopping cart")]
    public class InitiatePayPalForCurrentCart : CheckoutModel, IReturn<CheckoutResult>
    {
    }

    /// <summary>A PayPal checkout return.</summary>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin,
     Route("/Providers/Checkout/CompletePayPalForCurrentCart", "POST",
        Summary = "Checkout the current shopping cart")]
    public class CompletePayPalForCurrentCart : IReturn<CheckoutResult>
    {
        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        [ApiMember(Name = nameof(Token), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Paypal Token")]
        public string Token { get; set; } = null!;

        /// <summary>Gets or sets the identifier of the payer.</summary>
        /// <value>The identifier of the payer.</value>
        [ApiMember(Name = nameof(PayerID), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Paypal PayerID")]
        public string PayerID { get; set; } = null!;
    }

    /// <summary>A PayPal checkout service.</summary>
    /// <seealso cref="CheckoutService"/>
    public partial class CheckoutService
    {
        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
#pragma warning disable IDE1006 // Naming Styles
        public async Task<object> Post(InitiatePayPalForCurrentCart request)
#pragma warning restore IDE1006 // Naming Styles
        {
            // Verify the account is not on hold
            var result1 = await DoAccountOnHoldCheckAsync().ConfigureAwait(false);
            if (result1 != null)
            {
                return result1;
            }
            // Required Setup
#if PAYPAL
            await EnsurePayPalCheckoutProviderAsync(null).ConfigureAwait(false);
#endif
            // Base Settings
            var typeName = request.WithCartInfo?.CartTypeName ?? "Cart";
            request.WithCartInfo ??= new();
            request.WithCartInfo.CartSessionID = await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false);
            request.WithUserInfo ??= new();
            request.WithUserInfo.ExternalUserID ??= CurrentUserName;
            // Run the checkout procedures
            var result = await PayPalCheckoutProvider.CheckoutAsync(
                    checkout: request,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    lookupKey: await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    gateway: null,
                    contextProfileName: null)
                .ConfigureAwait(false);
            // Storing the request for use in the complete call using the SessionBag
            SessionBag["CurrentCartModel"] = request;
            // Ensuring SessionBags SessionCartGuid gets reset will occur in the ReturnAction
            return (CheckoutResult)result;
        }

        /// <summary>Post this message.</summary>
        /// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
        /// <param name="request">The request.</param>
        /// <returns>A CheckoutResult.</returns>
#pragma warning disable IDE1006 // Naming Styles
        public async Task<object> Post(CompletePayPalForCurrentCart request)
#pragma warning restore IDE1006 // Naming Styles
        {
            // Verify the account is not on hold
            var result1 = await DoAccountOnHoldCheckAsync().ConfigureAwait(false);
            if (result1 != null)
            {
                return result1;
            }
            // Verify the session contains the cart checkout which was stored with the PayPalCheckout call
            if (SessionBag["CurrentCartModel"] is not CheckoutModel)
            {
                throw new InvalidOperationException(
                    "There is no current checkout model in session - InitiatePayPalForCurrentCart must be called first");
            }
            var checkoutModel = (CheckoutModel)SessionBag["CurrentCartModel"];
            checkoutModel.PayByPayPal!.PayPalToken = Contract.RequiresValidKey(request.Token);
            checkoutModel.PayByPayPal!.PayerID = Contract.RequiresValidKey(request.PayerID);
            var typeName = checkoutModel.WithCartInfo!.CartTypeName ?? "Cart";
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (cartResponse, updatedSessionID) = await Workflows.Carts.SessionGetAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (updatedSessionID != null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            // Required Setup
#if PAYPAL
            await EnsurePayPalCheckoutProviderAsync(null).ConfigureAwait(false);
#endif
            // Run the checkout procedures
            var pricingFactoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            // ReSharper disable once StyleCop.SA1008
            var result = await ((PayPalCheckoutProvider)PayPalCheckoutProvider).CheckoutReturnAsync(
                    checkoutModel,
                    cartResponse.Result!,
                    pricingFactoryContext,
                    async () => await SingleOrderCheckoutProvider.CheckoutAsync(
                            checkout: checkoutModel,
                            pricingFactoryContext: pricingFactoryContext,
                            lookupKey: lookupKey,
                            taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                            gateway: null,
                            contextProfileName: null)
                        .ConfigureAwait(false),
                    null)
                .ConfigureAwait(false);
            return await ValidateAndCompleteCheckoutAsync(
                    failCondition: !result.Succeeded || !Contract.CheckValidID(result.OrderID),
                    result: result)
                .ConfigureAwait(false);
        }
    }
}
#endif
