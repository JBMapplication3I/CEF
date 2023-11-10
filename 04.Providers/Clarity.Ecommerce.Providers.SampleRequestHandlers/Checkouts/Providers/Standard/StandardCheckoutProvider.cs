// <copyright file="StandardCheckoutProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard checkout provider class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Checkouts.Standard
{
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.SampleRequestHandlers.Checkouts;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A sample request checkout workflow.</summary>
    /// <seealso cref="ISampleRequestCheckoutProviderBase"/>
    public class StandardSampleRequestCheckoutProvider : SampleRequestCheckoutProviderBase
    {
        /// <inheritdoc/>
#pragma warning disable CA1065
        public override bool HasValidConfiguration => throw new System.NotImplementedException();
#pragma warning restore CA1065

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            string cartType,
            ITaxesProviderBase? taxesProvider,
            bool isTaxable,
            int? currentUserID,
            int? currentAccountID,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(checkout);
            var paymentProvider = RegistryLoaderWrapper.GetPaymentProvider(contextProfileName);
            Contract.RequiresNotNull(
                paymentProvider,
                "Could not load a Payment Provider. Are your provider settings in the web config correct?");
            await paymentProvider!.InitConfigurationAsync(contextProfileName).ConfigureAwait(false);
            IUserModel? user = null;
            // No username means Guest Checkout
            if (Contract.CheckValidKey(checkout.WithUserInfo?.ExternalUserID)
                || Contract.CheckValidID(checkout.WithUserInfo?.UserID))
            {
                // Get User, Create if doesn't exist
                user = Contract.CheckValidID(checkout.WithUserInfo?.UserID)
                    ? await Workflows.Users.GetAsync(checkout.WithUserInfo!.UserID!.Value, contextProfileName).ConfigureAwait(false)
                    : await Workflows.Users.GetAsync(checkout.WithUserInfo!.ExternalUserID!, contextProfileName).ConfigureAwait(false);
                if (user == null)
                {
                    user = RegistryLoaderWrapper.GetInstance<IUserModel>(contextProfileName);
                    user.UserName = checkout.WithUserInfo.ExternalUserID;
                    user.StatusID = await Workflows.UserStatuses.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: "Registered",
                            byName: "Registered",
                            byDisplayName: "Registered",
                            model: null,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    user.TypeID = await Workflows.UserTypes.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: "Customer",
                            byName: "Customer",
                            byDisplayName: "Customer",
                            model: null,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    user.Contact = (ContactModel?)checkout.Billing;
                    user.BillingAddress = (AddressModel?)checkout.Billing?.Address;
                    var createResponse = await Workflows.Users.CreateAsync(user, contextProfileName).ConfigureAwait(false);
                    user = await Workflows.Users.GetAsync(createResponse.Result, contextProfileName).ConfigureAwait(false);
                }
            }
            var cart = Contract.CheckValidID(checkout.WithCartInfo?.CartID)
                ? await Workflows.Carts.AdminGetAsync(
                        lookupKey: new CartByIDLookupKey(
                            cartID: checkout.WithCartInfo!.CartID!.Value,
                            userID: currentUserID,
                            accountID: currentAccountID),
                        pricingFactoryContext: pricingFactoryContext,
                        taxesProvider: taxesProvider,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false)
                : (checkout.WithCartInfo?.CartSessionID).HasValue
                    ? (await Workflows.Carts.SessionGetAsync(
                            lookupKey: new SessionCartBySessionAndTypeLookupKey(
                                sessionID: checkout.WithCartInfo!.CartSessionID!.Value,
                                typeKey: cartType,
                                userID: currentUserID,
                                accountID: currentAccountID),
                            pricingFactoryContext: pricingFactoryContext,
                            taxesProvider: taxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false))
                    .cartResponse.Result
                    : null;
            if (cart == null)
            {
                return new CheckoutResult { Succeeded = false, ErrorMessage = "ERROR! Could not look up your cart." };
            }
            if (cart.SalesItems!.Count == 0)
            {
                return new CheckoutResult { Succeeded = false, ErrorMessage = "ERROR! Your cart was empty." };
            }
            // Need to make sure all the products in the cart are type "FREE-SAMPLE"
            if (CEFConfigDictionary.SampleRequestEnforcesFreeSampleType
                && cart.SalesItems.Any(s => s.ProductTypeKey != "FREE-SAMPLE"))
            {
                return new CheckoutResult
                {
                    Succeeded = false,
                    ErrorMessage = "ERROR! At least one product in your cart is not a free sample.",
                };
            }
            var result = new CheckoutResult();
            var order = await RegistryLoaderWrapper.GetSampleRequestActionsProvider(contextProfileName: null)!
                .CreateViaCheckoutProcessAsync(checkout, cart, user!, contextProfileName)
                .ConfigureAwait(false);
            result.OrderID = order.ID;
            result.Succeeded = true;
            return result;
        }
    }
}
