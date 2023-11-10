// <copyright file="CartCRUDWorkflow.extended.Session.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart workflow class</summary>
// ReSharper disable RedundantSuppressNullableWarningExpression
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    public partial class CartWorkflow
    {
        /// <inheritdoc/>
        public async Task<(CEFActionResponse<ICartModel?> cartResponse, Guid? updatedSessionID)> SessionGetAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            var response = await ResolveSessionCartsToLatestActiveWithItemsAsync(
                    lookupKey: lookupKey,
                    cartID: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!response.ActionSucceeded)
            {
                return (response.ChangeFailingCEFARType<ICartModel?>(), null);
            }
            if (!Contract.CheckValidID(response.Result.cartID))
            {
                response.Messages.Add("ERROR! Invalid Cart ID");
                return (response.ChangeFailingCEFARType<ICartModel?>(), null);
            }
            await AssignUserIDToCartIfNullAsync(response.Result.cartID, lookupKey.UserID, contextProfileName).ConfigureAwait(false);
            await AssignAccountIDToCartIfNullAsync(response.Result.cartID, lookupKey.AccountID, contextProfileName).ConfigureAwait(false);
            return (await CustomSessionMapGetAsync(
                        response: response,
                        id: response.Result.cartID,
                        cartType: lookupKey.TypeKey,
                        userID: lookupKey.UserID,
                        currentAccountID: lookupKey.AltAccountID,
                        pricingFactoryContext: pricingFactoryContext,
                        taxesProvider: taxesProvider,
                        contextProfileName: contextProfileName,
                        skipDiscounts: lookupKey.TypeKey.StartsWith("Target-Grouping-"),
                        altAccountID: lookupKey.AltAccountID)
                    .ConfigureAwait(false),
                response.Result.sessionID);
        }

        /// <inheritdoc/>
        public async Task<(CEFActionResponse<ICartModel?> cartResponse, Guid? updatedSessionID)> SessionGetAsync(
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            var response = await ResolveSessionCartsToLatestActiveWithItemsAsync(
                    lookupKey.ToSessionLookupKey("Cart", pricingFactoryContext.SessionID),
                    cartID: lookupKey.CartID,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!response.ActionSucceeded)
            {
                return (response.ChangeFailingCEFARType<ICartModel?>(), null);
            }
            if (!Contract.CheckValidID(response.Result.cartID))
            {
                response.Messages.Add("ERROR! Invalid Cart ID");
                return (response.ChangeFailingCEFARType<ICartModel?>(), null);
            }
            await AssignUserIDToCartIfNullAsync(response.Result.cartID, lookupKey.UserID, contextProfileName).ConfigureAwait(false);
            await AssignAccountIDToCartIfNullAsync(response.Result.cartID, lookupKey.AccountID, contextProfileName).ConfigureAwait(false);
            return (await CustomSessionMapGetAsync(
                        response: response,
                        id: response.Result.cartID,
                        cartType: "Cart",
                        userID: lookupKey.UserID,
                        currentAccountID: lookupKey.AltAccountID,
                        pricingFactoryContext: pricingFactoryContext,
                        taxesProvider: taxesProvider,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false),
                response.Result.sessionID);
        }

        /// <inheritdoc/>
        public async Task<(int? cartID, Guid? updatedSessionID)> SessionGetAsIDAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName)
        {
            var resolveResponse = await ResolveSessionCartsToLatestActiveWithItemsAsync(
                    lookupKey: lookupKey,
                    cartID: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return resolveResponse.ActionSucceeded
                ? Contract.CheckValidID(resolveResponse.Result.cartID)
                    ? (resolveResponse.Result.cartID, resolveResponse.Result.sessionID)
                    : (null, null)
                : (null, null);
        }

        /// <inheritdoc/>
        public Task<(int? cartID, Guid? updatedSessionID)> CheckExistsByTypeNameAndSessionIDAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName)
        {
            return SessionGetAsIDAsync(
                lookupKey: lookupKey,
                pricingFactoryContext: pricingFactoryContext,
                contextProfileName: contextProfileName);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<List<IRateQuoteModel>>> GetRateQuotesAsync(
            CartByIDLookupKey lookupKey,
            IContactModel origin,
            bool expedited,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = (await context.Carts
                    .FilterByActive(true)
                    .FilterByID(Contract.RequiresValidID(lookupKey.CartID))
                    .Select(x => new
                    {
                        x.ID,
                        x.ShippingSameAsBilling,
                        x.BillingContactID,
                        x.ShippingContactID,
                        SalesItems = x.SalesItems!.Where(y => y.Active).Select(y => new { y.ID }),
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new Cart
                {
                    ID = x.ID,
                    ShippingSameAsBilling = x.ShippingSameAsBilling,
                    BillingContactID = x.BillingContactID,
                    ShippingContactID = x.ShippingContactID,
                    SalesItems = x.SalesItems.Select(y => new CartItem { ID = y.ID }).ToList(),
                })
                .SingleOrDefault();
            if (cart is null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>>();
            }
            return await GetRateQuotesAsync(cart, origin, expedited, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<List<IRateQuoteModel>>> GetRateQuotesAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            IContactModel origin,
            bool expedited,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Carts.FilterByActive(true);
            query = lookupKey.TypeKey.StartsWith("Target")
                ? query.FilterCartsByLookupKey(TargetCartLookupKey.FromSessionLookupKey(lookupKey), false)
                : query.FilterCartsByLookupKey(lookupKey);
            var cart = (await query
                    .Select(x => new
                    {
                        x.ID,
                        x.ShippingSameAsBilling,
                        x.BillingContactID,
                        x.ShippingContactID,
                        SalesItems = x.SalesItems!.Where(y => y.Active).Select(y => new { y.ID }),
                    })
                    .OrderByDescending(x => x.SalesItems.Count())
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new Cart
                {
                    ID = x.ID,
                    ShippingSameAsBilling = x.ShippingSameAsBilling,
                    BillingContactID = x.BillingContactID,
                    ShippingContactID = x.ShippingContactID,
                    SalesItems = x.SalesItems.Select(y => new CartItem { ID = y.ID }).ToList(),
                })
                .FirstOrDefault();
            if (cart is null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>>();
            }
            return await GetRateQuotesAsync(cart, origin, expedited, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ApplyRateQuoteToCartAsync(
            CartByIDLookupKey lookupKey,
            int? rateQuoteID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts
                .FilterByID(Contract.RequiresValidID(lookupKey.CartID))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            return await ApplyRateQuoteToCartInnerAsync(cart, rateQuoteID, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ApplyRateQuoteToCartAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            int? rateQuoteID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Carts.FilterByActive(true);
            query = lookupKey.TypeKey.StartsWith("Target")
                ? query.FilterCartsByLookupKey(TargetCartLookupKey.FromSessionLookupKey(lookupKey), false)
                : query.FilterCartsByLookupKey(lookupKey);
            var cart = await query
                .OrderByDescending(x => x.SalesItems!.Count(y => y.Active))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return await ApplyRateQuoteToCartInnerAsync(cart, rateQuoteID, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ClearRateQuoteFromCartAsync(
            CartByIDLookupKey lookupKey,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts
                .FilterByID(Contract.RequiresValidID(lookupKey.CartID))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            return await ClearRateQuoteFromCartInnerAsync(cart, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ClearRateQuoteFromCartAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Carts.FilterByActive(true);
            query = lookupKey.TypeKey.StartsWith("Target")
                ? query.FilterCartsByLookupKey(TargetCartLookupKey.FromSessionLookupKey(lookupKey), false)
                : query.FilterCartsByLookupKey(lookupKey);
            var cart = await query
                .OrderByDescending(x => x.SalesItems!.Count(y => y.Active))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return await ClearRateQuoteFromCartInnerAsync(cart, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task SetQuantityBackOrderedForItemAsync(
            int salesItemID,
            int quantity,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.CartItems.FilterByID(salesItemID).SingleAsync().ConfigureAwait(false);
            if (entity.QuantityBackOrdered == quantity)
            {
                return;
            }
            entity.QuantityBackOrdered = quantity;
            entity.UpdatedDate = DateExtensions.GenDateTime;
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> AddCartFeeAsync(
            ICartModel cart,
            string amountToFee,
            string? contextProfileName)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (cart == null)
            {
                return CEFAR.FailingCEFAR<int>("ERROR! No cart provided");
            }
            if (!decimal.TryParse(Contract.RequiresValidKey(amountToFee).Replace("$", string.Empty), out var amount))
            {
                return cart.ID.WrapInPassingCEFAR();
            }
            // ReSharper disable once ConstantNullCoalescingCondition
            cart.Totals ??= RegistryLoaderWrapper.GetInstance<ICartTotals>(contextProfileName);
            cart.Totals.Fees = amount;
            return await UpdateAsync(cart, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SessionClearAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var (cartID, _) = await CheckExistsByTypeNameAndSessionIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: pricingFactoryContext,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(cartID))
            {
                return CEFAR.FailingCEFAR("ERROR! Could not locate cart to clear it");
            }
            foreach (var cartItem in context.CartItems.Where(x => x.MasterID == cartID!.Value))
            {
                cartItem.Active = false;
            }
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            await RemoveCartsThatAreEmptyAsync(contextProfileName).ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Applies the rate quote to cart.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="rateQuoteID">       Identifier for the rate quote.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        private static async Task<CEFActionResponse> ApplyRateQuoteToCartInnerAsync(
            ICart cart,
            int? rateQuoteID,
            string? contextProfileName)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (cart == null)
            {
                return CEFAR.FailingCEFAR("WARNING! No Cart");
            }
            if (Contract.CheckEmpty(cart.SalesItems))
            {
                return CEFAR.FailingCEFAR("ERROR! There are no items in this Cart");
            }
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var rate = (decimal?)null;
            foreach (var rateQuote in context.RateQuotes.FilterByActive(true).Where(x => x.CartID == cart.ID))
            {
                if (rateQuote.Selected && rateQuote.ID != rateQuoteID)
                {
                    rateQuote.Selected = false;
                    rateQuote.UpdatedDate = timestamp;
                    continue;
                }
                // ReSharper disable once InvertIf
                if (!rateQuote.Selected && rateQuote.ID == rateQuoteID)
                {
                    rateQuote.Selected = true;
                    rateQuote.UpdatedDate = timestamp;
                    rate = rateQuote.Rate;
                }
                // Else Ignore, no change
            }
            cart.SubtotalShipping = rate ?? cart.SubtotalShipping;
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Was unable to save changes to the Cart");
        }

        /// <summary>Clears the rate quote from cart inner .</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        private static async Task<CEFActionResponse> ClearRateQuoteFromCartInnerAsync(
            ICart cart,
            string? contextProfileName)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (cart == null)
            {
                return CEFAR.FailingCEFAR("WARNING! No Cart");
            }
            if (Contract.CheckEmpty(cart.SalesItems))
            {
                return CEFAR.FailingCEFAR("ERROR! There are no items in this Cart");
            }
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var rateQuote in context.RateQuotes.FilterByActive(true).Where(x => x.CartID == cart.ID && x.Selected))
            {
                rateQuote.Selected = false;
                rateQuote.UpdatedDate = timestamp;
            }
            cart.SubtotalShipping = 0m;
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Was unable to save changes to the Cart");
        }

        /// <summary>Calculates the product handling charges.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The calculated product handling charges.</returns>
        private static async Task<decimal> CalculateProductHandlingChargesAsync(
            int? productID,
            string? contextProfileName)
        {
            if (!productID.HasValue)
            {
                return 0m;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var productHandlingCharge = await context.Products
                .AsNoTracking()
                .FilterByID(productID)
                .Select(x => x.HandlingCharge)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false)
                ?? 0m;
            var categoryHandlingCharges = await context.ProductCategories
                .AsNoTracking()
                .FilterProductCategoriesByProductID(productID)
                .Where(x => x.Slave != null)
                .Select(x => x.Slave!.HandlingCharge)
                .Where(x => x.HasValue)
                .DefaultIfEmpty(0m)
                .SumAsync()
                .ConfigureAwait(false);
            return productHandlingCharge + (categoryHandlingCharges ?? 0m);
        }

        /// <summary>Calculates the product weight.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The calculated product weight.</returns>
        private static async Task<decimal> CalculateProductWeightAsync(int? productID, string? contextProfileName)
        {
            if (Contract.CheckInvalidID(productID))
            {
                return 0m;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Products
                .AsNoTracking()
                .FilterByID(productID)
                .Select(x => x.Weight)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false)
                ?? 0m;
        }

        /// <summary>Calculates the cart handling charges.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The calculated cart handling charges.</returns>
        private static async Task<decimal> CalculateCartHandlingChargesAsync(
            ICartModel cart,
            string? contextProfileName)
        {
            if (Contract.CheckEmpty(cart.SalesItems))
            {
                return 0m;
            }
            var sum = 0m;
            var tempCache = new Dictionary<int, decimal>();
            foreach (var productID in cart.SalesItems!.Select(x => x.ProductID).Where(Contract.CheckValidID).Cast<int>())
            {
                if (tempCache.ContainsKey(productID) && tempCache.TryGetValue(productID, out var value))
                {
                    sum += value;
                    continue;
                }
                sum += tempCache[productID] = await CalculateProductHandlingChargesAsync(productID, contextProfileName).ConfigureAwait(false);
            }
            return sum;
        }

        /// <summary>Calculates the cart weight.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The calculated cart weight.</returns>
        private static async Task<decimal> CalculateCartWeightAsync(ICartModel cart, string? contextProfileName)
        {
            if (Contract.CheckEmpty(cart.SalesItems))
            {
                return 0m;
            }
            var sum = 0m;
            var tempCache = new Dictionary<int, decimal>();
            foreach (var productID in cart.SalesItems!.Select(x => x.ProductID).Where(Contract.CheckValidID).Cast<int>())
            {
                if (tempCache.ContainsKey(productID) && tempCache.TryGetValue(productID, out var value))
                {
                    sum += value;
                    continue;
                }
                sum += tempCache[productID] = await CalculateProductWeightAsync(productID, contextProfileName).ConfigureAwait(false);
            }
            return sum;
        }

        /// <summary>Session get entity.</summary>
        /// <param name="cartID"> Identifier for the cart.</param>
        /// <param name="context">The context.</param>
        /// <returns>An ICart.</returns>
        private static async Task<ICart> SessionGetEntityAsync(
            int? cartID,
            IClarityEcommerceEntities context)
        {
            return await context.Carts
                .FilterByActive(true)
                .FilterByID(Contract.RequiresValidID(cartID))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <summary>Custom session map get.</summary>
        /// <param name="response">             The response.</param>
        /// <param name="id">                   The identifier.</param>
        /// <param name="cartType">             Type of the cart.</param>
        /// <param name="userID">               Identifier for the user.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">        The taxes provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <param name="skipItems">            True to skip items.</param>
        /// <param name="skipDiscounts">        True to skip discounts.</param>
        /// <param name="skipTotals">           True to skip totals.</param>
        /// <param name="skipContacts">         True to skip contacts.</param>
        /// <returns>A Task{CEFActionResponse{ICartModel}}.</returns>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
        private async Task<CEFActionResponse<ICartModel?>> CustomSessionMapGetAsync(
            CEFActionResponse<(int cartID, Guid? sessionID)> response,
            int id,
            string cartType,
            int? userID,
            int? currentAccountID,
            IPricingFactoryContextModel? pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName,
            bool skipItems = false,
            bool skipDiscounts = false,
            bool skipTotals = false,
            bool skipContacts = false,
            int? altAccountID = null)
        {
            var oneDayBack = DateExtensions.GenDateTime.Date.AddDays(-1);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            ICartModel? model = (await context.Carts
                    .FilterByID(id)
                    .Select(x => new
                    {
                        // Base
                        x.ID,
                        x.CustomKey,
                        x.Active,
                        x.CreatedDate,
                        x.UpdatedDate,
                        x.JsonAttributes,
                        // Cart Properties
                        x.SessionID,
                        x.SubtotalDiscountsModifier,
                        x.SubtotalDiscountsModifierMode,
                        x.SubtotalTaxesModifier,
                        x.SubtotalTaxesModifierMode,
                        x.SubtotalHandlingModifier,
                        x.SubtotalHandlingModifierMode,
                        x.SubtotalFeesModifier,
                        x.SubtotalFeesModifierMode,
                        x.SubtotalShippingModifier,
                        x.SubtotalShippingModifierMode,
                        x.RequestedShipDate,
                        x.ShippingSameAsBilling,
                        // Related Objects
                        x.UserID,
                        x.AccountID,
                        x.BrandID,
                        x.FranchiseID,
                        x.StoreID,
                        x.TypeID,
                        x.StatusID,
                        x.StateID,
                        x.ShippingContactID,
                        x.BillingContactID,
                        User = x.User == null
                            ? null
                            : new
                            {
                                x.User.CustomKey,
                                x.User.UserName,
                                Contact = x.User.Contact == null
                                    ? null
                                    : new
                                    {
                                        x.User.Contact.Email1,
                                        x.User.Contact.FirstName,
                                        x.User.Contact.LastName,
                                    },
                            },
                        Account = x.Account == null
                            ? null
                            : new
                            {
                                x.Account.CustomKey,
                                x.Account.Name,
                            },
                        Brand = x.Brand == null
                            ? null
                            : new
                            {
                                x.Brand.CustomKey,
                                x.Brand.Name,
                            },
                        Franchise = x.Franchise == null
                            ? null
                            : new
                            {
                                x.Franchise.CustomKey,
                                x.Franchise.Name,
                            },
                        Store = x.Store == null
                            ? null
                            : new
                            {
                                x.Store.CustomKey,
                                x.Store.Name,
                            },
                        Type = new
                        {
                            x.Type!.CustomKey,
                            x.Type.Name,
                        },
                        Status = new
                        {
                            x.Status!.CustomKey,
                            x.Status.Name,
                        },
                        State = new
                        {
                            x.State!.CustomKey,
                            x.State.Name,
                        },
                        // Line Items
                        SalesItems = x.SalesItems!
                            .Where(y => !skipItems && y.Active)
                            .Select(y => new
                            {
                                // Base
                                y.ID,
                                y.CustomKey,
                                y.Active,
                                y.CreatedDate,
                                y.JsonAttributes,
                                // Overridden data
                                y.Name,
                                y.Description,
                                y.Sku,
                                // Cart Items
                                y.ForceUniqueLineItemKey,
                                // Related Items
                                y.MasterID,
                                y.UserID,
                                User = y.User == null
                                    ? null
                                    : new
                                    {
                                        UserKey = y.User.CustomKey,
                                        UserUserName = y.User.UserName,
                                    },
                                y.ProductID,
                                Product = y.Product == null
                                    ? null
                                    : new
                                    {
                                        y.Product.CustomKey,
                                        y.Product.Name,
                                        y.Product.Description,
                                        y.Product.ShortDescription,
                                        y.Product.SeoUrl,
                                        y.Product.UnitOfMeasure,
                                        y.Product.RequiresRoles,
                                        y.Product.RequiresRolesAlt,
                                        y.Product.TypeID,
                                        Type = new { y.Product.Type!.CustomKey },
                                        y.Product.NothingToShip,
                                        y.Product.DropShipOnly,
                                        y.Product.MaximumPurchaseQuantity,
                                        y.Product.MaximumPurchaseQuantityIfPastPurchased,
                                        y.Product.MinimumPurchaseQuantity,
                                        y.Product.MinimumPurchaseQuantityIfPastPurchased,
                                        y.Product.MaximumBackOrderPurchaseQuantity,
                                        y.Product.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                                        y.Product.MaximumBackOrderPurchaseQuantityGlobal,
                                        y.Product.MaximumPrePurchaseQuantity,
                                        y.Product.MaximumPrePurchaseQuantityIfPastPurchased,
                                        y.Product.MaximumPrePurchaseQuantityGlobal,
                                        y.Product.IsDiscontinued,
                                        y.Product.IsUnlimitedStock,
                                        y.Product.AllowBackOrder,
                                        y.Product.AllowPreSale,
                                        y.Product.IsEligibleForReturn,
                                        y.Product.RestockingFeePercent,
                                        y.Product.RestockingFeeAmount,
                                        y.Product.IsTaxable,
                                        y.Product.TaxCode,
                                        y.Product.JsonAttributes,
                                        PrimaryImage = y.Product.Images!
                                            .Where(z => z.Active)
                                            .OrderByDescending(z => z.IsPrimary)
                                            .ThenBy(z => z.OriginalWidth)
                                            .ThenBy(z => z.OriginalHeight)
                                            .Take(1)
                                            .Select(z => z.ThumbnailFileName ?? z.OriginalFileName)
                                            .FirstOrDefault(),
                                    },
                                y.UnitOfMeasure,
                                // SalesItem Properties
                                y.Quantity,
                                y.QuantityBackOrdered,
                                y.QuantityPreSold,
                                y.UnitSoldPrice,
                                y.UnitSoldPriceModifier,
                                y.UnitSoldPriceModifierMode,
                                Discounts = y.Discounts!
                                    .Where(z => !skipDiscounts && z.Active)
                                    .Select(z => new
                                    {
                                        // Base Properties
                                        z.ID,
                                        z.CustomKey,
                                        z.CreatedDate,
                                        z.Active,
                                        z.JsonAttributes,
                                        // Applied Discount Properties
                                        z.DiscountTotal,
                                        z.ApplicationsUsed,
                                        // Related Objects
                                        z.MasterID,
                                        z.SlaveID,
                                        DiscountCanCombine = z.Slave!.CanCombine,
                                        // Used by UI
                                        z.Slave.DiscountTypeID,
                                        DiscountValue = z.Slave.Value,
                                        DiscountValueType = z.Slave.ValueType,
                                        DiscountIsAutoApplied = z.Slave.IsAutoApplied,
                                        DiscountName = z.Slave.Name,
                                    }
                                    !),
                                Targets = y.Targets!
                                    .Where(z => z.Active)
                                    .Select(z => new
                                    {
                                        // Base Properties
                                        z.ID,
                                        z.CustomKey,
                                        z.Active,
                                        z.CreatedDate,
                                        z.UpdatedDate,
                                        z.JsonAttributes,
                                        // SalesItemTarget Properties
                                        z.Quantity,
                                        z.NothingToShip,
                                        // Related Objects
                                        z.MasterID,
                                        z.TypeID,
                                        Type = new
                                        {
                                            z.Type!.CustomKey,
                                            z.Type.Name,
                                        },
                                        z.DestinationContactID,
                                        z.OriginProductInventoryLocationSectionID,
                                        z.OriginStoreProductID,
                                        z.OriginVendorProductID,
                                        z.SelectedRateQuoteID,
                                    }
                                    !),
                            }
                            !),
                        ItemQuantity = skipItems ? 0m : x.SalesItems!
                            .Where(y => y.Active)
                            .Select(y => y.Quantity + (y.QuantityBackOrdered ?? 0m) + (y.QuantityPreSold ?? 0m))
                            .DefaultIfEmpty(0m)
                            .Sum(),
                        x.SubtotalTaxes,
                        x.SubtotalFees,
                        x.SubtotalHandling,
                        x.SubtotalShipping,
                        x.SubtotalDiscounts,
                        x.SubtotalItems,
                        // Addresses/Contacts
                        x.ShipmentID,
                        // Associated Objects
                        Discounts = x.Discounts!
                            .Where(y => !skipDiscounts && y.Active)
                            .Select(y => new
                            {
                                // Base Properties
                                y.ID,
                                y.CreatedDate,
                                y.Active,
                                // Applied Discount Properties
                                y.DiscountTotal,
                                y.ApplicationsUsed,
                                // Related Objects
                                y.MasterID,
                                y.SlaveID,
                                DiscountCanCombine = y.Slave!.CanCombine,
                                // Used by UI
                                y.Slave.DiscountTypeID,
                                DiscountValue = y.Slave.Value,
                                DiscountValueType = y.Slave.ValueType,
                                DiscountIsAutoApplied = y.Slave.IsAutoApplied,
                                DiscountName = y.Slave.Name,
                            }
                            !),
                        RateQuotes = x.RateQuotes!
                            .Where(y => y.Active && (x.CreatedDate > oneDayBack || x.UpdatedDate > oneDayBack))
                            .Select(y => new
                            {
                                // Base Properties
                                y.ID,
                                y.CreatedDate,
                                y.Active,
                                // RateQuote Properties
                                y.CartHash,
                                y.EstimatedDeliveryDate,
                                y.Rate,
                                y.RateTimestamp,
                                y.Selected,
                                y.TargetShippingDate,
                                // Related Objects
                                y.ShipCarrierMethodID,
                                ShipCarrierMethod = new
                                {
                                    y.ShipCarrierMethod!.CustomKey,
                                    y.ShipCarrierMethod.Name,
                                },
                            }
                            !),
                    }
                    !)
                    .ToListAsync()
                    .ConfigureAwait(false))
                // ReSharper disable once CyclomaticComplexity
                .Select(x => new CartModel
                {
                    // Base
                    ID = x.ID,
                    CustomKey = x.SessionID?.ToString(),
                    Active = x.Active,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                    // Related Objects
                    UserID = x.UserID,
                    AccountID = x.AccountID,
                    BrandID = x.BrandID,
                    FranchiseID = x.FranchiseID,
                    StoreID = x.StoreID,
                    TypeID = x.TypeID,
                    StatusID = x.StatusID,
                    StateID = x.StateID,
                    ShippingSameAsBilling = x.ShippingSameAsBilling ?? false,
                    UserKey = x.User?.CustomKey,
                    UserUserName = x.User?.UserName,
                    UserContactEmail = x.User?.Contact?.Email1,
                    UserContactFirstName = x.User?.Contact?.FirstName,
                    UserContactLastName = x.User?.Contact?.LastName,
                    AccountKey = x.Account?.CustomKey,
                    AccountName = x.Account?.Name,
                    BrandKey = x.Brand?.CustomKey,
                    BrandName = x.Brand?.Name,
                    FranchiseKey = x.Franchise?.CustomKey,
                    FranchiseName = x.Franchise?.Name,
                    StoreKey = x.Store?.CustomKey,
                    StoreName = x.Store?.Name,
                    TypeKey = x.Type.CustomKey,
                    TypeName = x.Type.Name,
                    StatusKey = x.Status.CustomKey,
                    StatusName = x.Status.Name,
                    StateKey = x.State.CustomKey,
                    StateName = x.State.Name,
                    // Line Items
                    SalesItems = skipItems ? null : x.SalesItems
                        // ReSharper disable once CyclomaticComplexity
                        .Select(y => new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                        {
                            // Base
                            ID = y.ID,
                            CustomKey = y.CustomKey,
                            Active = y.Active,
                            CreatedDate = y.CreatedDate,
                            SerializableAttributes = y.JsonAttributes.DeserializeAttributesDictionary(),
                            // Overridden data
                            Name = y.Name,
                            Description = y.Description,
                            Sku = y.Sku,
                            // Cart Items
                            ForceUniqueLineItemKey = y.ForceUniqueLineItemKey,
                            // Related Items
                            MasterID = y.MasterID,
                            UserID = y.UserID,
                            ProductID = y.ProductID,
                            UserKey = y.User?.UserKey,
                            UserUserName = y.User?.UserUserName,
                            ProductKey = y.Product?.CustomKey,
                            ProductName = y.Product?.Name,
                            ProductDescription = y.Product?.Description,
                            ProductShortDescription = y.Product?.ShortDescription,
                            ProductSeoUrl = y.Product?.SeoUrl,
                            ProductUnitOfMeasure = y.Product?.UnitOfMeasure,
                            UnitOfMeasure = y.UnitOfMeasure ?? y.Product?.UnitOfMeasure,
                            ProductRequiresRoles = y.Product?.RequiresRoles,
                            ProductRequiresRolesAlt = y.Product?.RequiresRolesAlt,
                            ProductTypeID = y.Product?.TypeID,
                            ProductTypeKey = y.Product?.Type.CustomKey,
                            ProductNothingToShip = y.Product?.NothingToShip ?? false,
                            ProductDropShipOnly = y.Product?.DropShipOnly ?? false,
                            ProductMaximumPurchaseQuantity = y.Product?.MaximumPurchaseQuantity,
                            ProductMaximumPurchaseQuantityIfPastPurchased = y.Product?.MaximumPurchaseQuantityIfPastPurchased,
                            ProductMinimumPurchaseQuantity = y.Product?.MinimumPurchaseQuantity,
                            ProductMinimumPurchaseQuantityIfPastPurchased = y.Product?.MinimumPurchaseQuantityIfPastPurchased,
                            ProductMaximumBackOrderPurchaseQuantity = y.Product?.MaximumBackOrderPurchaseQuantity,
                            ProductMaximumBackOrderPurchaseQuantityIfPastPurchased = y.Product?.MaximumBackOrderPurchaseQuantityIfPastPurchased,
                            ProductMaximumBackOrderPurchaseQuantityGlobal = y.Product?.MaximumBackOrderPurchaseQuantityGlobal,
                            ProductMaximumPrePurchaseQuantity = y.Product?.MaximumPrePurchaseQuantity,
                            ProductMaximumPrePurchaseQuantityIfPastPurchased = y.Product?.MaximumPrePurchaseQuantityIfPastPurchased,
                            ProductMaximumPrePurchaseQuantityGlobal = y.Product?.MaximumPrePurchaseQuantityGlobal,
                            ProductIsDiscontinued = y.Product?.IsDiscontinued ?? false,
                            ProductIsUnlimitedStock = y.Product?.IsUnlimitedStock ?? false,
                            ProductAllowBackOrder = y.Product?.AllowBackOrder ?? false,
                            ProductAllowPreSale = y.Product?.AllowPreSale ?? false,
                            ProductIsEligibleForReturn = y.Product?.IsEligibleForReturn ?? false,
                            ProductRestockingFeePercent = y.Product?.RestockingFeePercent,
                            ProductRestockingFeeAmount = y.Product?.RestockingFeeAmount,
                            ProductIsTaxable = y.Product?.IsTaxable ?? true,
                            ProductTaxCode = y.Product?.TaxCode,
                            ProductSerializableAttributes = y.Product?.JsonAttributes.DeserializeAttributesDictionary(),
                            ProductPrimaryImage = y.Product?.PrimaryImage,
                            Quantity = y.Quantity,
                            QuantityBackOrdered = y.QuantityBackOrdered ?? 0m,
                            QuantityPreSold = y.QuantityPreSold ?? 0m,
                            UnitSoldPrice = cartType == "Quote Cart" || y.Product?.Type.CustomKey == "Custom Quote Item" || CEFConfigDictionary.UseCustomPriceConversionForCartItems ? y.UnitSoldPrice : null,
                            UnitSoldPriceModifier = y.UnitSoldPriceModifier ?? 0m,
                            UnitSoldPriceModifierMode = y.UnitSoldPriceModifierMode ?? (int)Enums.TotalsModifierModes.Add,
                            Discounts = skipDiscounts ? null : y.Discounts
                                .Select(z => new AppliedCartItemDiscountModel
                                {
                                    // Base Properties
                                    ID = z.ID,
                                    CreatedDate = z.CreatedDate,
                                    Active = z.Active,
                                    // Applied Discount Properties
                                    DiscountTotal = z.DiscountTotal,
                                    ApplicationsUsed = z.ApplicationsUsed,
                                    // Related Objects
                                    MasterID = z.MasterID,
                                    SlaveID = z.SlaveID,
                                    DiscountCanCombine = z.DiscountCanCombine,
                                    // Used by UI
                                    DiscountTypeID = z.DiscountTypeID,
                                    DiscountValue = z.DiscountValue,
                                    DiscountValueType = z.DiscountValueType,
                                    DiscountIsAutoApplied = z.DiscountIsAutoApplied,
                                    SlaveName = z.DiscountName,
                                })
                                .ToList(),
                            Targets = y.Targets
                                .Select(z => new SalesItemTargetBaseModel
                                {
                                    // Base Properties
                                    ID = z.ID,
                                    CustomKey = z.CustomKey,
                                    Active = z.Active,
                                    CreatedDate = z.CreatedDate,
                                    UpdatedDate = z.UpdatedDate,
                                    SerializableAttributes = z.JsonAttributes.DeserializeAttributesDictionary(),
                                    // SalesItemTarget Properties
                                    Quantity = z.Quantity,
                                    NothingToShip = z.NothingToShip,
                                    // Related Objects
                                    MasterID = z.MasterID,
                                    TypeID = z.TypeID,
                                    TypeKey = z.Type.CustomKey,
                                    TypeName = z.Type.Name,
                                    DestinationContactID = z.DestinationContactID,
                                    SelectedRateQuoteID = z.SelectedRateQuoteID,
                                })
                                .ToList(),
                        })
                        .ToList(),
                    ItemQuantity = x.ItemQuantity,
                    SessionID = x.SessionID,
                    SubtotalDiscountsModifier = x.SubtotalDiscountsModifier,
                    SubtotalDiscountsModifierMode = x.SubtotalDiscountsModifierMode,
                    SubtotalTaxesModifier = x.SubtotalTaxesModifier,
                    SubtotalTaxesModifierMode = x.SubtotalTaxesModifierMode,
                    SubtotalHandlingModifier = x.SubtotalHandlingModifier,
                    SubtotalHandlingModifierMode = x.SubtotalHandlingModifierMode,
                    SubtotalFeesModifier = x.SubtotalFeesModifier,
                    SubtotalFeesModifierMode = x.SubtotalFeesModifierMode,
                    SubtotalShippingModifier = x.SubtotalShippingModifier,
                    SubtotalShippingModifierMode = x.SubtotalShippingModifierMode,
                    RequestedShipDate = x.RequestedShipDate,
                    // Addresses/Contacts
                    BillingContactID = x.BillingContactID,
                    ShippingContactID = x.ShippingContactID,
                    ShipmentID = x.ShipmentID,
                    // Associated Objects
                    Discounts = skipDiscounts ? null : x.Discounts
                        .Select(y => new AppliedCartDiscountModel
                        {
                            // Base Properties
                            ID = y.ID,
                            CreatedDate = y.CreatedDate,
                            Active = y.Active,
                            // Applied Discount Properties
                            DiscountTotal = y.DiscountTotal,
                            ApplicationsUsed = y.ApplicationsUsed,
                            // Related Objects
                            MasterID = y.MasterID,
                            SlaveID = y.SlaveID,
                            DiscountCanCombine = y.DiscountCanCombine,
                            // Used by UI
                            DiscountTypeID = y.DiscountTypeID,
                            DiscountValue = y.DiscountValue,
                            DiscountValueType = y.DiscountValueType,
                            DiscountIsAutoApplied = y.DiscountIsAutoApplied,
                            SlaveName = y.DiscountName,
                        })
                        .ToList(),
                    RateQuotes = x.RateQuotes
                        .Select(y => new RateQuoteModel
                        {
                            // Base Properties
                            ID = y.ID,
                            CreatedDate = y.CreatedDate,
                            Active = y.Active,
                            // RateQuote's Properties
                            CartHash = y.CartHash,
                            EstimatedDeliveryDate = y.EstimatedDeliveryDate,
                            Rate = y.Rate,
                            RateTimestamp = y.RateTimestamp,
                            Selected = y.Selected,
                            TargetShippingDate = y.TargetShippingDate,
                            // RateQuote's Related Objects (Not Mapped unless Forced, or a flattening property)
                            ShipCarrierMethodID = y.ShipCarrierMethodID,
                            ShipCarrierMethodKey = y.ShipCarrierMethod.CustomKey,
                            ShipCarrierMethodName = y.ShipCarrierMethod.Name,
                        })
                        .OrderBy(y => y.Rate ?? 0m)
                        .ToList(),
                    Totals = skipTotals ? new() : new CartTotals
                    {
                        SubTotal = x.SubtotalItems,
                        Tax = x.SubtotalTaxes,
                        Shipping = x.SubtotalShipping,
                        Handling = x.SubtotalHandling,
                        Fees = x.SubtotalFees,
                        Discounts = x.SubtotalDiscounts,
                    },
                })
                .SingleOrDefault();
            if (skipDiscounts && model != null)
            {
                // Blanks the discounts for target groups in case they got stored somehow
                model.Discounts = new();
                if (model.SalesItems != null)
                {
                    foreach (var salesItem in model.SalesItems)
                    {
                        salesItem.Discounts = new();
                    }
                }
            }
            if (model == null)
            {
                response.Messages.Add("ERROR! Couldn't load Cart");
                return response.ChangeFailingCEFARType<ICartModel?>();
            }
            if (!skipContacts)
            {
                // TODO: Country/Region memory caching so they only load once
                async Task<ICountryModel> GetCountryAsync(int countryID, IClarityEcommerceEntities context2)
                {
                    ICountryModel countryModel = (await context2.Countries
                            .AsNoTracking()
                            .FilterByID(countryID)
                            .Select(x => new
                            {
                                // Base Properties
                                x.ID,
                                x.CustomKey,
                                x.CreatedDate,
                                x.UpdatedDate,
                                x.Active,
                                x.Hash,
                                x.JsonAttributes,
                                // NameableBase Properties
                                x.Name,
                                x.Description,
                                // Country Properties
                                x.Code,
                                x.ISO3166Alpha2,
                                x.ISO3166Alpha3,
                                x.ISO3166Numeric,
                                x.PhoneRegEx,
                                x.PhonePrefix,
                            }
                            !)
                            .ToListAsync()
                            .ConfigureAwait(false))
                        .Select(x => new CountryModel
                        {
                            // Base Properties
                            ID = x.ID,
                            CustomKey = x.CustomKey,
                            CreatedDate = x.CreatedDate,
                            UpdatedDate = x.UpdatedDate,
                            Active = x.Active,
                            Hash = x.Hash,
                            SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                            // NameableBase Properties
                            Name = x.Name,
                            Description = x.Description,
                            // Country Properties
                            Code = x.Code,
                            ISO3166Alpha2 = x.ISO3166Alpha2,
                            ISO3166Alpha3 = x.ISO3166Alpha3,
                            ISO3166Numeric = x.ISO3166Numeric,
                            PhoneRegEx = x.PhoneRegEx,
                            PhonePrefix = x.PhonePrefix,
                        })
                        .Single();
                    return countryModel;
                }
                async Task<IRegionModel> GetRegionAsync(int regionID, IClarityEcommerceEntities context2)
                {
                    IRegionModel regionModel = (await context2.Regions
                            .AsNoTracking()
                            .FilterByID(regionID)
                            .Select(x => new
                            {
                                // Base Properties
                                x.ID,
                                x.CustomKey,
                                x.CreatedDate,
                                x.UpdatedDate,
                                x.Active,
                                x.Hash,
                                x.JsonAttributes,
                                // NameableBase Properties
                                x.Name,
                                x.Description,
                                // Region Properties
                                x.Code,
                                // Related Objects
                                x.CountryID,
                            }
                            !)
                            .ToListAsync()
                            .ConfigureAwait(false))
                        .Select(x => new RegionModel
                        {
                            // Base Properties
                            ID = x.ID,
                            CustomKey = x.CustomKey,
                            CreatedDate = x.CreatedDate,
                            UpdatedDate = x.UpdatedDate,
                            Active = x.Active,
                            Hash = x.Hash,
                            SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                            // NameableBase Properties
                            Name = x.Name,
                            Description = x.Description,
                            // Region Properties
                            Code = x.Code,
                            // Related Objects
                            CountryID = x.CountryID,
                        })
                        .Single();
                    // ReSharper disable once InvertIf
                    if (Contract.CheckValidID(regionModel.CountryID))
                    {
                        regionModel.Country = await GetCountryAsync(
                                regionModel.CountryID,
                                context2)
                            .ConfigureAwait(false);
                        regionModel.CountryKey = regionModel.Country.CustomKey;
                        regionModel.CountryName = regionModel.Country.Name;
                    }
                    return regionModel;
                }
                async Task<IAddressModel> GetAddressAsync(int addressID, IClarityEcommerceEntities context2)
                {
                    IAddressModel addressModel = (await context2.Addresses
                            .AsNoTracking()
                            .FilterByID(addressID)
                            .Select(x => new
                            {
                                // Base Properties
                                x.ID,
                                x.CustomKey,
                                x.CreatedDate,
                                x.UpdatedDate,
                                x.Active,
                                x.Hash,
                                x.JsonAttributes,
                                // Address Properties
                                x.Company,
                                x.Street1,
                                x.Street2,
                                x.Street3,
                                x.City,
                                x.RegionCustom,
                                x.CountryCustom,
                                x.PostalCode,
                                x.Latitude,
                                x.Longitude,
                                // Related Objects
                                x.CountryID,
                                x.RegionID,
                            }
                            !)
                            .ToListAsync()
                            .ConfigureAwait(false))
                        .Select(x => new AddressModel
                        {
                            // Base Properties
                            ID = x.ID,
                            CustomKey = x.CustomKey,
                            CreatedDate = x.CreatedDate,
                            UpdatedDate = x.UpdatedDate,
                            Active = x.Active,
                            Hash = x.Hash,
                            SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                            // Address Properties
                            Company = x.Company,
                            Street1 = x.Street1,
                            Street2 = x.Street2,
                            Street3 = x.Street3,
                            City = x.City,
                            RegionCustom = x.RegionCustom,
                            CountryCustom = x.CountryCustom,
                            PostalCode = x.PostalCode,
                            Latitude = x.Latitude,
                            Longitude = x.Longitude,
                            // Related Objects
                            CountryID = x.CountryID,
                            RegionID = x.RegionID,
                        })
                        .Single();
                    if (Contract.CheckValidID(addressModel.CountryID))
                    {
                        addressModel.Country = await GetCountryAsync(
                                addressModel.CountryID!.Value,
                                context2)
                            .ConfigureAwait(false);
                        addressModel.CountryKey = addressModel.Country.CustomKey;
                        addressModel.CountryName = addressModel.Country.Name;
                    }
                    // ReSharper disable once InvertIf
                    if (Contract.CheckValidID(addressModel.RegionID))
                    {
                        addressModel.Region = await GetRegionAsync(
                                addressModel.RegionID!.Value,
                                context2)
                            .ConfigureAwait(false);
                        addressModel.RegionKey = addressModel.Region.CustomKey;
                        addressModel.RegionName = addressModel.Region.Name;
                    }
                    return addressModel;
                }
                async Task<IContactModel> GetContactAsync(int contactID, IClarityEcommerceEntities context2)
                {
                    IContactModel contactModel = (await context2.Contacts
                            .AsNoTracking()
                            .FilterByID(contactID)
                            .Select(x => new
                            {
                                // Base Properties
                                x.ID,
                                x.CustomKey,
                                x.CreatedDate,
                                x.UpdatedDate,
                                x.Active,
                                x.Hash,
                                x.JsonAttributes,
                                // Contact Properties
                                x.FirstName,
                                x.MiddleName,
                                x.LastName,
                                x.FullName,
                                x.Phone1,
                                x.Phone2,
                                x.Phone3,
                                x.Fax1,
                                x.Email1,
                                x.Website1,
                                // Related Objects
                                x.TypeID,
                                x.AddressID,
                            }
                            !)
                            .ToListAsync()
                            .ConfigureAwait(false))
                        .Select(x => new ContactModel
                        {
                            // Base Properties
                            ID = x.ID,
                            CustomKey = x.CustomKey,
                            CreatedDate = x.CreatedDate,
                            UpdatedDate = x.UpdatedDate,
                            Active = x.Active,
                            Hash = x.Hash,
                            SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                            // Contact Properties
                            FirstName = x.FirstName,
                            MiddleName = x.MiddleName,
                            LastName = x.LastName,
                            FullName = x.FullName,
                            Phone1 = x.Phone1,
                            Phone2 = x.Phone2,
                            Phone3 = x.Phone3,
                            Fax1 = x.Fax1,
                            Email1 = x.Email1,
                            Website1 = x.Website1,
                            // Related Objects
                            TypeID = x.TypeID,
                            AddressID = x.AddressID,
                        })
                        .Single();
                    // ReSharper disable once InvertIf
                    if (Contract.CheckValidID(contactModel.AddressID))
                    {
                        contactModel.Address = await GetAddressAsync(
                                contactModel.AddressID!.Value,
                                context2)
                            .ConfigureAwait(false);
                        contactModel.AddressKey = contactModel.Address.CustomKey;
                    }
                    return contactModel;
                }
                if (Contract.CheckValidID(model.BillingContactID))
                {
                    model.BillingContact = await GetContactAsync(
                            model.BillingContactID!.Value,
                            // ReSharper disable once AccessToDisposedClosure
                            context)
                        .ConfigureAwait(false);
                    model.BillingContactKey = model.BillingContact.CustomKey;
                }
                if (Contract.CheckValidID(model.ShippingContactID))
                {
                    model.ShippingContact = await GetContactAsync(
                            model.ShippingContactID!.Value,
                            // ReSharper disable once AccessToDisposedClosure
                            context)
                        .ConfigureAwait(false);
                    model.ShippingContactKey = model.ShippingContact.CustomKey;
                }
            }
            if (!skipTotals)
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                model.Totals ??= RegistryLoaderWrapper.GetInstance<ICartTotals>(contextProfileName);
                model.Totals = new CartTotals
                {
                    SubTotal = skipItems ? 0m : model.SalesItems!.Sum(si => si.ExtendedPrice),
                    Tax = model.Totals.Tax,
                    Fees = model.Totals.Fees,
                    Handling = model.Totals.Handling,
                    Shipping = model.RateQuotes!.Any(y => y.Selected)
                        ? model.RateQuotes!
                            .OrderByDescending(y => y.CreatedDate)
                            .First(y => y.Selected)
                            .Rate
                        ?? model.Totals.Shipping
                        : model.Totals.Shipping,
                    Discounts = skipDiscounts
                        ? 0m
                        : model.Discounts!.Sum(y => y.DiscountTotal)
                            + (skipItems
                                ? 0m
                                : model.SalesItems!
                                    .Where(y => y.Discounts!.Any())
                                    .Sum(y => y.Discounts!.Sum(z => z.DiscountTotal))),
                };
            }
            if (!skipItems && model.SalesItems!.Any(x => x.TotalQuantity <= 0))
            {
                foreach (var salesItem in model.SalesItems!.Where(x => x.TotalQuantity <= 0))
                {
                    await Workflows.CartItems.DeleteAsync(salesItem.ID, contextProfileName).ConfigureAwait(false);
                }
                model.SalesItems = model.SalesItems!
                    .Where(x => x.TotalQuantity > 0)
                    .ToList();
            }
            if (!skipItems && model.SalesItems!.Count == 0)
            {
                // Cart shouldn't exist
                response.Messages.Add("ERROR! This cart doesn't have any sales items and will be removed.");
                // ReSharper disable once InvertIf
                if (Contract.CheckValidID(response.Result.cartID))
                {
                    await DeleteAsync(response.Result.cartID, context).ConfigureAwait(false);
                    response.Result = (0, null);
                }
                return response.ChangeFailingCEFARType<ICartModel?>();
            }
            if (!skipItems)
            {
                await AppendPriceDataToSalesItemsAsync(model, false, pricingFactoryContext, contextProfileName).ConfigureAwait(false);
            }
            if (!skipTotals)
            {
                // ReSharper disable once ConstantNullCoalescingCondition
                model.Totals ??= RegistryLoaderWrapper.GetInstance<ICartTotals>(contextProfileName);
                model.Totals.SubTotal = skipItems ? 0m : model.SalesItems!.Sum(x => x.ExtendedPrice);
                model.Totals.Handling = CEFConfigDictionary.ShippingHandlingFeesEnabled
                    ? await CalculateCartHandlingChargesAsync(model, contextProfileName).ConfigureAwait(false)
                    + (model.Totals.SubTotal > 0
                        || await CalculateCartWeightAsync(model, contextProfileName).ConfigureAwait(false) > 0m
                                ? CEFConfigDictionary.ChargesHandlingForNon0CostOrWeightOrders ?? 0m
                                : 0m)
                    : 0m;
                var sameAsBilling = model.ShippingSameAsBilling ?? false;
                model.Totals.Shipping = skipContacts ? 0m : sameAsBilling && model.BillingContact == null && !Contract.CheckValidID(model.BillingContactID)
                    // Shipping contact is same as billing contact, and billing contact is null, don't show shipping
                    ? 0m
                    : !sameAsBilling && model.ShippingContact == null && !Contract.CheckValidID(model.ShippingContactID)
                        // Shipping contact is null, don't show shipping
                        ? 0m
                        // Shipping contact is valid, use shipment or selected rate quote data
                        : model.Shipment != null
                            // Use Shipment data
                            ? model.Shipment.PublishedRate ?? model.Totals.Shipping
                            // Try Rate Quotes Data
                            : model.RateQuotes!.Any(y => y.Active && y.Selected)
                                ? model.RateQuotes!.Where(y => y.Active).OrderByDescending(y => y.CreatedDate).First(y => y.Selected).Rate
                                ?? model.Totals.Shipping
                                : model.Totals.Shipping;
            }
            // ReSharper disable once ConstantNullCoalescingCondition
            model.Totals ??= RegistryLoaderWrapper.GetInstance<ICartTotals>(contextProfileName);
            if (!skipItems
                && CEFConfigDictionary.StoresEnabled
                && model.Totals.Tax == 0
                && model.SalesItems?.Any() == true
                && model.SalesItems[0].SerializableAttributes.ContainsKey("SelectedStoreID")
                && int.TryParse(model.SalesItems[0].SerializableAttributes["SelectedStoreID"].Value, out var storeID))
            {
                model.Store = context.Stores
                    .AsNoTracking()
                    .FilterByID(storeID)
                    .SelectSingleLiteStoreAndMapToStoreModel(context.ContextProfileName);
            }
            if (!skipTotals && CEFConfigDictionary.TaxesEnabled && taxesProvider != null)
            {
                var taxes = await taxesProvider.CalculateCartAsync(
                        cart: model,
                        userID: userID,
                        contextProfileName: contextProfileName,
                        key: null,
                        vatId: null,
                        currentAccountId: currentAccountID)
                    .ConfigureAwait(false);
                // TODO: Handle taxes.ErrorMessages.Any()
                model.Totals.Tax = taxes.TotalTaxes;
            }
            if (!CEFConfigDictionary.DiscountsEnabled || skipDiscounts)
            {
                return model.WrapInPassingCEFAR(response.Messages.ToArray());
            }
            await Workflows.DiscountManager.VerifyCurrentDiscountsAsync(
                    cart: model,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!skipTotals)
            {
                model.Totals.Discounts = model.Discounts!.Where(y => y.Active).Sum(y => y.DiscountTotal)
                    + (skipItems ? 0m : model.SalesItems!
                        .Where(y => y.Discounts?.Any(z => z.Active) == true)
                        .Sum(y => y.Discounts!.Where(z => z.Active).Sum(z => z.DiscountTotal)));
            }
            if (model.Discounts?.Any() != true)
            {
                return model.WrapInPassingCEFAR(response.Messages.ToArray());
            }
            // Don't send all the codes back to the UI
            foreach (var discount in model.Discounts.Where(x => x?.Discount != null))
            {
                discount.Discount!.Codes = null;
            }
            return model.WrapInPassingCEFAR(response.Messages.ToArray());
        }

        /// <summary>Resolve session carts to latest active with items.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="cartID">               Identifier for the cart.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse{(int, Guid?)}.</returns>
        private async Task<CEFActionResponse<(int cartID, Guid? sessionID)>> ResolveSessionCartsToLatestActiveWithItemsAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            int? cartID,
            string? contextProfileName)
        {
            if (!Contract.CheckValidID(lookupKey.SessionID) && !Contract.CheckValidID(lookupKey.UserID) && !Contract.CheckValidID(lookupKey.AccountID))
            {
                return CEFAR.FailingCEFAR<(int cartID, Guid? sessionID)>(
                    "ERROR! Must supply at least a Session, User or Account ID to locate data against");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            if (Contract.CheckValidID(cartID) && !Contract.CheckValidKey(lookupKey.TypeKey))
            {
                lookupKey.TypeKey = (await context.Carts
                    .AsNoTracking()
                    .FilterByID(cartID!.Value)
                    .Select(x => x.Type!.Name)
                    .SingleAsync()
                    .ConfigureAwait(false))!;
            }
            var rootQuery = context.Carts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByTypeName<Cart, CartType>(lookupKey.TypeKey, true);
            var userCartIDs = Contract.CheckAllValidIDs(lookupKey.UserID, lookupKey.AccountID)
                ? await rootQuery
                    .FilterCartsByLookupKey(lookupKey.ButIgnoreSessionID())
                    .Select(x => x.ID)
                    .ToListAsync()
                    .ConfigureAwait(false)
                : new();
            var sessionCartIDs = lookupKey.SessionID != default
                ? await rootQuery
                    .FilterCartsByLookupKey(lookupKey.ButIgnoreUserAndAccountID())
                    .Select(x => x.ID)
                    .ToListAsync()
                    .ConfigureAwait(false)
                : new();
            if (userCartIDs.Count == 0 && sessionCartIDs.Count == 0)
            {
                // There were no session carts for this user by session Guid or user ID so generate a new one
                var blankCart = await GenerateBlankCartAsync(
                        model: null,
                        typeName: lookupKey.TypeKey,
                        contextProfileName: contextProfileName,
                        sessionID: lookupKey.SessionID == default ? Guid.NewGuid() : lookupKey.SessionID,
                        userID: lookupKey.UserID,
                        accountID: lookupKey.AccountID,
                        brandID: lookupKey.BrandID,
                        franchiseID: lookupKey.FranchiseID,
                        storeID: lookupKey.StoreID)
                    .ConfigureAwait(false);
                var response = await AddCartAndSaveSafelyAsync(context, blankCart, true).ConfigureAwait(false);
                if (!response.ActionSucceeded)
                {
                    return response.ChangeFailingCEFARType<(int cartID, Guid? sessionID)>();
                }
                return (response.Result!.ID, response.Result.SessionID).WrapInPassingCEFAR(
                    $"NOTE! No previous {lookupKey.TypeKey} Carts, generated a new one.");
            }
            if (userCartIDs.Count == 0)
            {
                return await ProcessCartIDsForLatestSessionCartAsync(
                        cartIDs: sessionCartIDs,
                        kind: "Session",
                        typeName: lookupKey.TypeKey,
                        sessionID: lookupKey.SessionID,
                        userID: lookupKey.UserID,
                        accountID: lookupKey.AccountID,
                        brandID: lookupKey.BrandID,
                        franchiseID: lookupKey.FranchiseID,
                        storeID: lookupKey.StoreID,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            // ReSharper disable once InvertIf
            if (sessionCartIDs.Count == 0)
            {
                return await ProcessCartIDsForLatestSessionCartAsync(
                        cartIDs: userCartIDs,
                        kind: "User",
                        typeName: lookupKey.TypeKey,
                        sessionID: lookupKey.SessionID,
                        userID: lookupKey.UserID,
                        accountID: lookupKey.AccountID,
                        brandID: lookupKey.BrandID,
                        franchiseID: lookupKey.FranchiseID,
                        storeID: lookupKey.StoreID,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            // We have both, combine the lists and do the full array
            var combinedIDs = userCartIDs.Union(sessionCartIDs).ToList();
            if (Contract.CheckValidID(cartID))
            {
                combinedIDs.Add(cartID!.Value);
            }
            return await ProcessCartIDsForLatestSessionCartAsync(
                    cartIDs: combinedIDs,
                    kind: "User and Session",
                    typeName: lookupKey.TypeKey,
                    sessionID: lookupKey.SessionID,
                    userID: lookupKey.UserID,
                    accountID: lookupKey.AccountID,
                    brandID: lookupKey.BrandID,
                    franchiseID: lookupKey.FranchiseID,
                    storeID: lookupKey.StoreID,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Process the cart IDs for latest session cart.</summary>
        /// <param name="cartIDs">           The cart IDs.</param>
        /// <param name="kind">              The kind.</param>
        /// <param name="typeName">          Name of the type.</param>
        /// <param name="sessionID">         Identifier for the session.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="brandID">           Identifier for the brand.</param>
        /// <param name="franchiseID">       Identifier for the franchise.</param>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{int}.</returns>
        private async Task<CEFActionResponse<(int cartID, Guid? sessionID)>> ProcessCartIDsForLatestSessionCartAsync(
            IReadOnlyCollection<int> cartIDs,
            string kind,
            string typeName,
            Guid? sessionID,
            int? userID,
            int? accountID,
            int? brandID,
            int? franchiseID,
            int? storeID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // See if there is only one
            if (cartIDs.Count == 1)
            {
                var id = cartIDs.First();
                return (id, await context.Carts.Where(x => x.ID == id).Select(x => x.SessionID).SingleAsync().ConfigureAwait(false)).WrapInPassingCEFAR(
                    $"NOTE! Only one active {kind}-based Cart of type '{typeName}', returning it.");
            }
            // There is more than one session cart, review the items to find the latest session cart with valid values
            var lastSetDateTimes = new Dictionary<int, DateTime>();
            foreach (var cartID in cartIDs)
            {
                lastSetDateTimes[cartID] = await Contract.RequiresNotNull(context.CartItems)
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterCartItemsByCartID(cartID)
                    .Select(x => x.CreatedDate)
                    .DefaultIfEmpty(DateTime.MinValue)
                    .MaxAsync()
                    .ConfigureAwait(false);
            }
            if (lastSetDateTimes.Count == 0)
            {
                var cart = await GenerateBlankCartAsync(
                        model: null,
                        typeName: typeName,
                        contextProfileName: contextProfileName,
                        sessionID: sessionID ?? Guid.NewGuid(),
                        userID: userID,
                        accountID: accountID,
                        brandID: brandID,
                        franchiseID: franchiseID,
                        storeID: storeID)
                    .ConfigureAwait(false);
                await AddCartAndSaveSafelyAsync(context, cart, true).ConfigureAwait(false);
                return (cart.ID, cart.SessionID).WrapInPassingCEFAR(
                    $"NOTE! No valid previous {kind}-based Carts of type '{typeName}', generated a new one.");
            }
            // In order of oldest to newest, deactivate the older ones and then return the latest
            var idsInOrder = lastSetDateTimes.OrderBy(x => x.Value).Select(x => x.Key).ToArray();
            var idsToDelete = new List<int>();
            for (var i = 0; i < lastSetDateTimes.Count; i++)
            {
                var id = idsInOrder[i];
                if (Contract.CheckValidID(userID))
                {
                    await AssignUserIDToCartIfNullAsync(
                            cart: await context.Carts.FilterByID(id).SingleAsync().ConfigureAwait(false),
                            userID: userID,
                            context: context)
                        .ConfigureAwait(false);
                }
                if (Contract.CheckValidID(accountID))
                {
                    await AssignAccountIDToCartIfNullAsync(
                            cart: await context.Carts.FilterByID(id).SingleAsync().ConfigureAwait(false),
                            accountID: accountID,
                            context: context)
                        .ConfigureAwait(false);
                }
                if (id == idsInOrder.Last())
                {
                    return (id, await context.Carts.Where(x => x.ID == id).Select(x => x.SessionID).SingleAsync().ConfigureAwait(false)).WrapInPassingCEFAR(
                        $"NOTE! Deactivated older {kind}-based Carts of type '{typeName}' and returning latest.");
                }
                idsToDelete.Add(id);
            }
            await BulkDeleteAsync(idsToDelete, contextProfileName).ConfigureAwait(false);
            throw new InvalidOperationException(
                $"ERROR! Could not figure out what to do in finding latest '{typeName}' Cart by {kind}-based Cart IDs.");
        }

        /// <summary>Gets rate quotes.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="origin">            The origin.</param>
        /// <param name="expedited">         True if expedited.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The rate quotes.</returns>
        private async Task<CEFActionResponse<List<IRateQuoteModel>>> GetRateQuotesAsync(
            ICart cart,
            IContactModel origin,
            bool expedited,
            string? contextProfileName)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (cart == null)
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>>("WARNING! No Cart");
            }
            var sameAsBilling = cart.ShippingSameAsBilling ?? false;
            if (sameAsBilling && !Contract.CheckValidID(cart.BillingContactID)
                || !sameAsBilling && !Contract.CheckValidID(cart.ShippingContactID))
            {
                return CEFAR.FailingCEFAR<List<IRateQuoteModel>>("WARNING! No shipping destination has been selected");
            }
            var shippingProviders = RegistryLoaderWrapper.GetShippingProviders(contextProfileName);
            var response = await IsShippingRequiredAsync(
                    lookupKey: new CartByIDLookupKey(
                            cartID: cart.ID,
                            userID: cart.UserID,
                            accountID: cart.AccountID,
                            brandID: cart.BrandID,
                            franchiseID: cart.FranchiseID,
                            storeID: cart.StoreID),
                    shippingProviders: shippingProviders,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!response.ActionSucceeded)
            {
                return response.ChangeFailingCEFARType<List<IRateQuoteModel>>();
            }
            var rates = new List<IRateQuoteModel>();
            // TODO@JTG: This contact call is having a significant performance hit. Replace with direct mapping that the shipping providers need for their API calls and Hashes
            var destination = await Workflows.Contacts.GetAsync(
                    sameAsBilling ? cart.BillingContactID!.Value : cart.ShippingContactID!.Value,
                    contextProfileName)
                .ConfigureAwait(false);
            var result = new CEFActionResponse<List<IRateQuoteModel>>();
            foreach (var shippingProvider in shippingProviders)
            {
                var providerRatesResponse = await shippingProvider.GetRateQuotesAsync(
                        cartID: cart.ID,
                        // Confirmed active and are the line items
                        salesItemIDs: cart.SalesItems!.Select(x => x.ID).ToList(),
                        origin: origin,
                        destination: destination!,
                        expedited: expedited,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (providerRatesResponse == null)
                {
                    result.Messages.Add($"WARNING! {shippingProvider.Name} failed to return anything.");
                    continue;
                }
                if (!providerRatesResponse.ActionSucceeded)
                {
                    result.Messages.Add($"WARNING! {shippingProvider.Name} failed to return valid results.");
                    result.Messages.AddRange(providerRatesResponse.Messages);
                    continue;
                }
                if (providerRatesResponse.Result!.Count == 0)
                {
                    result.Messages.Add($"WARNING! {shippingProvider.Name} did not have any results to provide.");
                    continue;
                }
                rates.AddRange(providerRatesResponse.Result);
            }
            if (rates.Count == 0)
            {
                result.Messages.Add("WARNING! No provider had any results to provide.");
                result.ActionSucceeded = false;
            }
            else
            {
                result.ActionSucceeded = true;
                result.Result = rates.OrderBy(r => r.Rate).ToList();
            }
            return result;
        }
    }
}
