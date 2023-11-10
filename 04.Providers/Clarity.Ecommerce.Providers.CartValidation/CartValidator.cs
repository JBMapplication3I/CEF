// <copyright file="CartValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart validator class</summary>
// ReSharper disable BadParensLineBreaks, CognitiveComplexity, CyclomaticComplexity, InvertIf, MergeSequentialChecks, MultipleSpaces
namespace Clarity.Ecommerce.Providers.CartValidation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.CartValidation;
    using Interfaces.Providers.Taxes;
    using Interfaces.Workflow;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A cart validator.</summary>
    public partial class CartValidator : ICartValidator
    {
        /// <summary>Gets the workflows.</summary>
        /// <value>The workflows.</value>
        protected static IWorkflowsController Workflows { get; }
            = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();

        /// <summary>Gets or sets the configuration.</summary>
        /// <value>The configuration.</value>
        protected ICartValidatorConfig? Config { get; set; }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> ValidateReadyForCheckoutAsync(
            ICartModel cart,
            IAccountModel? currentAccount,
            ITaxesProviderBase? taxesProvider,
            IPricingFactoryContextModel pricingFactoryContext,
            int? currentUserID,
            int? currentAccountID,
            string? contextProfileName)
        {
            Config = RegistryLoader.NamedContainerInstance(contextProfileName).GetInstance<ICartValidatorConfig>();
            var response = CEFAR.FailingCEFAR();
            // Validations that will just kill the response
            if (!ValidateAccountIsNotOnHoldIfAccountIsAvailableToCheck(response, currentAccount?.IsOnHold))
            {
                return response;
            }
            if (!ValidateCartHasActiveItemsInIt(response, cart))
            {
                return response;
            }
            // Process Products with Minimum and Maximum Purchase Requirements and check to see if we changed anything
            // (including removing items from the cart)
            var discontinuedCache = new Dictionary<int, bool>();
            var isUnlimitedCache = new Dictionary<int, bool>();
            var allowPreSaleCache = new Dictionary<int, bool>();
            var allowBackOrderCache = new Dictionary<int, bool>();
            var flatStockCache = new Dictionary<int, decimal>();
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                async Task<CEFActionResponse> UpdateAndReloadCartAsync(ICartModel cartInner)
                {
                    await Workflows.CartItems.UpdateMultipleAsync(
                            lookupKey: SessionCartBySessionAndTypeLookupKey.FromCart(cartInner),
                            models: cartInner.SalesItems!,
                            pricingFactoryContext: pricingFactoryContext,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    cart = (await RefreshCartToValidateAsync(
                                cart: cartInner,
                                taxesProvider: taxesProvider,
                                pricingFactoryContext: pricingFactoryContext,
                                currentUserID: currentUserID,
                                currentAccountID: currentAccountID,
                                contextProfileName: contextProfileName,
                                // ReSharper disable once AccessToDisposedClosure
                                context: context)
                            .ConfigureAwait(false))
                        .Result!;
                    if (cart == null!)
                    {
                        response.Messages.Add("ERROR! After validation, the cart was empty.");
                        return response;
                    }
                    return CEFAR.PassingCEFAR();
                }
                var changedCart = false;
                foreach (var cartItem in cart.SalesItems!)
                {
                    changedCart |= await ProcessProductWithMinimumsAsync(
                            cart: cart,
                            currentAccountTypeName: currentAccount?.TypeName,
                            currentAccountID: currentAccount?.ID,
                            currentUserID: currentUserID,
                            pricingFactoryContext: pricingFactoryContext,
                            cartItem: cartItem,
                            discontinuedCache: discontinuedCache,
                            isUnlimitedCache: isUnlimitedCache,
                            allowPreSaleCache: allowPreSaleCache,
                            allowBackOrderCache: allowBackOrderCache,
                            flatStockCache: flatStockCache,
                            response: response,
                            contextProfileName: contextProfileName,
                            context: context)
                        .ConfigureAwait(false);
                }
                if (changedCart)
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
                if (await CheckForSKURestrictionsAsync(response, cart.SalesItems, cart.ShippingContact?.Address).ConfigureAwait(false))
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
                if (!CEFConfigDictionary.OverrideProductRoleRestrictions && await CheckForProductRoleRestrictionsAsync(response, cart.UserID, cart.SalesItems, contextProfileName).ConfigureAwait(false))
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
                if (await CheckForProductRoleRestrictionsAltAsync(response, cart.UserID, cart.SalesItems, contextProfileName).ConfigureAwait(false))
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
                if (await ProcessGlobalWithMinimumDollarFreeShippingAsync(cart, pricingFactoryContext, response, contextProfileName).ConfigureAwait(false))
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
                if (await ProcessProductsWithDocumentRequiredForPurchaseAsync(response, cart, currentUserID, contextProfileName).ConfigureAwait(false))
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
                if (await ProcessProductsWithMustPurchaseMultiplesOfAmountAsync(response, cart, contextProfileName).ConfigureAwait(false))
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
                if (await ProcessBrandsWithMinimumsAsync(response, cart, pricingFactoryContext, contextProfileName).ConfigureAwait(false))
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
                if (await ProcessCategoriesWithMinimumsAsync(response, cart, pricingFactoryContext, contextProfileName).ConfigureAwait(false))
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
                if (await ProcessFranchisesWithMinimumsAsync(response, cart, pricingFactoryContext, contextProfileName).ConfigureAwait(false))
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
                if (await ProcessManufacturersWithMinimumsAsync(response, cart, pricingFactoryContext, contextProfileName).ConfigureAwait(false))
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
                if (await ProcessStoresWithMinimumsAsync(response, cart, pricingFactoryContext, contextProfileName).ConfigureAwait(false))
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
                if (await ProcessVendorsWithMinimumsAsync(response, cart, pricingFactoryContext, contextProfileName).ConfigureAwait(false))
                {
                    var r = await UpdateAndReloadCartAsync(cart).ConfigureAwait(false);
                    if (!r.ActionSucceeded)
                    {
                        return r;
                    }
                }
            }
            // Did we change anything?
            response.ActionSucceeded = !response.Messages.Any(m => m!.StartsWith("WARNING!") || m.StartsWith("ERROR!"));
            return response;
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> ClearCachesAsync(string? pattern = null)
        {
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName: null).ConfigureAwait(false);
            if (Contract.CheckValidKey(pattern))
            {
                await client!.RemoveByPatternAsync(pattern!).ConfigureAwait(false);
                return CEFAR.PassingCEFAR();
            }
            await client!.RemoveByPatternAsync("HardSoftStops:*").ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Refresh cart to validate.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="currentUserID">        Identifier for the current user.</param>
        /// <param name="currentAccountID">     Identifier for the current account.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <param name="context">              The context.</param>
        /// <returns>A CEFActionResponse{ICartModel}.</returns>
        protected virtual async Task<CEFActionResponse<ICartModel>> RefreshCartToValidateAsync(
            ICartModel cart,
            ITaxesProviderBase? taxesProvider,
            IPricingFactoryContextModel pricingFactoryContext,
            int? currentUserID,
            int? currentAccountID,
            string? contextProfileName,
            IClarityEcommerceEntities context)
        {
            return (await Workflows.Carts.SessionGetAsync(
                        SessionCartBySessionAndTypeLookupKey.FromCart(cart),
                        pricingFactoryContext,
                        taxesProvider,
                        contextProfileName)
                    .ConfigureAwait(false))
                .cartResponse!;
        }

        /// <summary>Validates the cart has active items in it.</summary>
        /// <param name="response">The response.</param>
        /// <param name="cart">    The cart.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected virtual bool ValidateCartHasActiveItemsInIt(
            CEFActionResponse response,
            ICartModel cart)
        {
            if (cart?.SalesItems?.Any(x => x.Active) == true)
            {
                // SUCCESS! The cart has active items.
                return true;
            }
            response.Messages.Add("ERROR! There are no active items in this cart.");
            return false;
        }
    }
}
