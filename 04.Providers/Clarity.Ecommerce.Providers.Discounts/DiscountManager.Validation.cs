// <copyright file="DiscountManager.Validation.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount manager class</summary>
namespace Clarity.Ecommerce.Providers.Discounts
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

    /// <summary>Manager for discounts.</summary>
    public partial class DiscountManager
    {
        /// <summary>Values that represent whether to process automatically applied discounts.</summary>
        private enum ProcessAutoApplied
        {
            Skip,
            // ReSharper disable once UnusedMember.Local
            Include,
            Only,
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> VerifyCurrentDiscountsAsync(
            int cartID,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            if (!CEFConfigDictionary.DiscountsEnabled)
            {
                // Feature Disabled
                return CEFAR.PassingCEFAR();
            }
            // Load the cart
            var cartResponse = await LoadCartAndClearDiscountsIfEmptyAsync(
                    cartID: cartID,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!cartResponse.ActionSucceeded)
            {
                return cartResponse;
            }
            var cart = cartResponse.Result;
            var discountIDs = cart!.Discounts!
                .Where(x => x.Active)
                .Cast<IAppliedDiscountBaseModel>()
                .Union(cart.SalesItems!
                    .Where(x => x.Active)
                    .SelectMany(y => y.Discounts!)
                    .Where(y => y.Active))
                .Select(x => x.SlaveID)
                .Distinct()
                .ToList();
            var aggregated = await CEFAR.AggregateAsync(
                    discountIDs,
                    x => VerifyDiscountAgainstCartAsync(
                        discountID: x,
                        cart: cart,
                        pricingFactoryContext: pricingFactoryContext,
                        skipAutoApplied: ProcessAutoApplied.Skip,
                        contextProfileName: contextProfileName)!)
                .ConfigureAwait(false);
            var aggregatedAuto = await VerifyAutoAppliedDiscountsAgainstCartAsync(
                    discountIDs: discountIDs,
                    cart: cart,
                    pricingFactoryContext: pricingFactoryContext,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return CEFAR.Aggregate(aggregated, aggregatedAuto);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> VerifyCurrentDiscountsAsync(
            ICartModel cart,
            IPricingFactoryContextModel? pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            if (!CEFConfigDictionary.DiscountsEnabled)
            {
                // Feature Disabled
                return CEFAR.PassingCEFAR();
            }
            // Load the cart
            var cartResponse = await LoadCartAndClearDiscountsIfEmptyAsync(
                    cart,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!cartResponse.ActionSucceeded)
            {
                return cartResponse;
            }
            var discountIDs = cart.Discounts!
                .Where(x => x.Active)
                .Cast<IAppliedDiscountBaseModel>()
                .Union(cart.SalesItems!
                    .Where(x => x.Active)
                    .SelectMany(y => y.Discounts!)
                    .Where(y => y.Active))
                .Select(x => x.SlaveID)
                .Distinct()
                .ToList();
            var aggregated = await CEFAR.AggregateAsync(
                    discountIDs,
                    x => VerifyDiscountAgainstCartAsync(
                        x,
                        cart,
                        pricingFactoryContext,
                        ProcessAutoApplied.Skip,
                        contextProfileName)!)
                .ConfigureAwait(false);
            var aggregatedAuto = await VerifyAutoAppliedDiscountsAgainstCartAsync(
                    discountIDs,
                    cart,
                    pricingFactoryContext,
                    contextProfileName)
                .ConfigureAwait(false);
            return CEFAR.Aggregate(aggregated, aggregatedAuto);
        }

        /// <summary>Loads cart and clear discounts if empty.</summary>
        /// <param name="cartID">               Identifier for the cart.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">        The taxes provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The cart and clear discounts if empty.</returns>
        internal static async Task<CEFActionResponse<ICartModel>> LoadCartAndClearDiscountsIfEmptyAsync(
            int cartID,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            // Load the cart
            var (cartResponse, updatedSessionID) = await Workflows.Carts.SessionGetAsync(
                    lookupKey: new CartByIDLookupKey(
                        cartID: cartID,
                        userID: pricingFactoryContext.UserID,
                        accountID: pricingFactoryContext.AccountID),
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!cartResponse.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<ICartModel>("ERROR! Invalid Cart");
            }
            if (pricingFactoryContext.SessionID == cartResponse.Result!.SessionID
                && pricingFactoryContext.SessionID == updatedSessionID)
            {
                return await LoadCartAndClearDiscountsIfEmptyAsync(
                        cartResponse.Result,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            // We can assume that if loading the cart involves changing the session id, something is wrong. That
            // should have all happened before coming into discounts.
            await DeactivateAllDiscountsForCartAndSaveAsync(
                    cartResponse.Result,
                    contextProfileName)
                .ConfigureAwait(false);
            return CEFAR.FailingCEFAR<ICartModel>("ERROR! Your cart is empty");
        }

        /// <summary>Loads cart and clear discounts if empty.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The cart and clear discounts if empty.</returns>
        internal static async Task<CEFActionResponse<ICartModel>> LoadCartAndClearDiscountsIfEmptyAsync(
            ICartModel cart,
            string? contextProfileName)
        {
            cart.Discounts ??= new(); // Make sure it's initialized
            // Check if it's empty
            if (!Contract.CheckEmpty(cart.SalesItems?.Where(x => x.Active)))
            {
                return cart.WrapInPassingCEFARIfNotNull();
            }
            // Deactivate all discounts on cart because it's empty
            if (Contract.CheckNotEmpty(cart.Discounts))
            {
                await DeactivateAllDiscountsForCartAndSaveAsync(cart, contextProfileName).ConfigureAwait(false);
            }
            return CEFAR.FailingCEFAR<ICartModel>("ERROR! Your cart is empty.");
        }

        private static async Task<CEFActionResponse> VerifyAutoAppliedDiscountsAgainstCartAsync(
            IReadOnlyCollection<int> discountIDs,
            ICartModel cart,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName)
        {
            var aggregatedAppliedAuto = await CEFAR.AggregateAsync(
                    discountIDs,
                    id => VerifyDiscountAgainstCartAsync(
                        id,
                        cart,
                        pricingFactoryContext,
                        ProcessAutoApplied.Only,
                        contextProfileName)!)
                .ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var autoAppliedDiscountsToCheck = await context.Discounts
                .FilterByActive(true)
                .FilterDiscountsByIsAutoApplied(true)
                .FilterByExcludedIDs(discountIDs)
                .OrderByDescending(x => x.Priority ?? int.MinValue)
                .Select(x => x.ID)
                .ToListAsync();
            var aggregatedNotAppliedButAuto = await CEFAR.AggregateAsync(
                    autoAppliedDiscountsToCheck,
                    id => VerifyDiscountAgainstCartAsync(
                        id,
                        cart,
                        pricingFactoryContext,
                        ProcessAutoApplied.Only,
                        contextProfileName)!)
                .ConfigureAwait(false);
            return CEFAR.Aggregate(aggregatedAppliedAuto, aggregatedNotAppliedButAuto);
        }

        /// <summary>Verify current discount.</summary>
        /// <param name="discountID">           Identifier for the discount.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="skipAutoApplied">      The skip automatic applied.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        private static async Task<CEFActionResponse<ReasonsToBeInvalid>> VerifyDiscountAgainstCartAsync(
            int discountID,
            ICartModel cart,
            IPricingFactoryContextModel? pricingFactoryContext,
            ProcessAutoApplied skipAutoApplied,
            string? contextProfileName)
        {
            var reasonsToBeInvalid = new ReasonsToBeInvalid();
            await reasonsToBeInvalid.PopulateAsync(
                    Contract.RequiresValidID(discountID, $"Invalid Discount ID: '{discountID}'"),
                    cart,
                    pricingFactoryContext,
                    skipAutoApplied,
                    contextProfileName)
                .ConfigureAwait(false);
            if (reasonsToBeInvalid.WasSkipped)
            {
                // This will always return true and out a CEFAR instance
                reasonsToBeInvalid.TryConvertToCEFAR(skipAutoApplied, out var cefar);
                return cefar!;
            }
            if (reasonsToBeInvalid.CheckShouldDiscountBeDeactivated())
            {
                // The discount is bad by one of multiple means but already applied, we need to remove it
                await DeactivateDiscountForCartAndSaveAsync(
                        cart,
                        discountID,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            if (reasonsToBeInvalid.TryConvertToCEFAR(skipAutoApplied, out var retVal))
            {
                // It may or may not be passing, but if it hit here, it's something we should send back
                if (retVal!.ActionSucceeded)
                {
                    // Update the Discount Total, like when changing quantity of the item from 3 to 4, a $10 discount
                    // for the item should change from $30 to $40
                    await ApplyDiscountToCartAsync(
                            id: Contract.RequiresValidID(reasonsToBeInvalid.Discount!.ID),
                            typeID: reasonsToBeInvalid.Discount.DiscountTypeID,
                            value: reasonsToBeInvalid.Discount.Value,
                            valueType: reasonsToBeInvalid.Discount.ValueType,
                            buyXValue: reasonsToBeInvalid.Discount.BuyXValue ?? 0m,
                            getYValue: reasonsToBeInvalid.Discount.GetYValue ?? 0m,
                            productTypeIDs: reasonsToBeInvalid.Discount.ProductTypes?.Select(x => x.SlaveID).Cast<int?>().ToArray(),
                            categoryIDs: reasonsToBeInvalid.Discount.Categories?.Select(x => x.SlaveID).Cast<int?>().ToArray(),
                            currentApplicationLimit: reasonsToBeInvalid.UsageLimitRemainingApplications,
                            //// applicationsPreviouslyConsumedBy: reasonsToBeInvalid.ApplicationsPreviouslyConsumedBy,
                            validByBrandProducts: reasonsToBeInvalid.ValidByBrandProducts,
                            validByCategoryProducts: reasonsToBeInvalid.ValidByCategoryProducts,
                            validByManufacturerProducts: reasonsToBeInvalid.ValidByManufacturerProducts,
                            validByStoreProducts: reasonsToBeInvalid.ValidByStoreProducts,
                            validByFranchiseProducts: reasonsToBeInvalid.ValidByFranchiseProducts,
                            validByVendorProducts: reasonsToBeInvalid.ValidByVendorProducts,
                            validByProductTypes: reasonsToBeInvalid.ValidByProductTypes,
                            validByThresholdProducts: reasonsToBeInvalid.ValidByThresholdAmountProducts,
                            productIDs: reasonsToBeInvalid.Discount.Products!.Select(x => x.SlaveID).ToList(),
                            cart: cart,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                }
                return retVal;
            }
            // This discount has passed existing requirements
            if (skipAutoApplied != ProcessAutoApplied.Skip && !reasonsToBeInvalid.IsApplied)
            {
                await ApplyVerifiedAutoAppliedDiscountToCartAsync(
                        id: Contract.RequiresValidID(reasonsToBeInvalid.Discount!.ID),
                        typeID: reasonsToBeInvalid.Discount.DiscountTypeID,
                        value: reasonsToBeInvalid.Discount.Value,
                        valueType: reasonsToBeInvalid.Discount.ValueType,
                        buyXValue: reasonsToBeInvalid.Discount.BuyXValue ?? 0m,
                        getYValue: reasonsToBeInvalid.Discount.GetYValue ?? 0m,
                        productTypeIDs: reasonsToBeInvalid.Discount.ProductTypes?.Select(x => x.SlaveID).Cast<int?>().ToArray(),
                        categoryIDs: reasonsToBeInvalid.Discount.Categories?.Select(x => x.SlaveID).Cast<int?>().ToArray(),
                        currentApplicationLimit: reasonsToBeInvalid.UsageLimitRemainingApplications,
                        //// applicationsPreviouslyConsumedBy: reasonsToBeInvalid.ApplicationsPreviouslyConsumedBy,
                        validByBrandProducts: reasonsToBeInvalid.ValidByBrandProducts,
                        validByCategoryProducts: reasonsToBeInvalid.ValidByCategoryProducts,
                        validByManufacturerProducts: reasonsToBeInvalid.ValidByManufacturerProducts,
                        validByStoreProducts: reasonsToBeInvalid.ValidByStoreProducts,
                        validByFranchiseProducts: reasonsToBeInvalid.ValidByFranchiseProducts,
                        validByVendorProducts: reasonsToBeInvalid.ValidByVendorProducts,
                        validByProductTypes: reasonsToBeInvalid.ValidByProductTypes,
                        validByThresholdProducts: reasonsToBeInvalid.ValidByThresholdAmountProducts,
                        productIDs: reasonsToBeInvalid.Discount.Products!.Select(x => x.SlaveID).ToList(),
                        cart: cart,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            // It should continue to be on the cart
            return reasonsToBeInvalid.WrapInPassingCEFARIfNotNull();
        }

#if FALSE
        /// <summary>Validates the automatic applied cart discounts.</summary>
        /// <param name="cart">            The cart.</param>
        /// <param name="discounts">       The discounts.</param>
        /// <param name="userID">          Identifier for the user.</param>
        /// <param name="context">         The context.</param>
        /// <returns>A Task.</returns>
        private static async Task ValidateAutoAppliedCartDiscountsAsync(
            ICartModel cart,
            IEnumerable<Discount> discounts,
            int userID,
            IClarityEcommerceEntities context)
        {
            var cartDiscounts = cart.Discounts;
            foreach (var discount in discounts)
            {
                var validator = GetDiscountValidator(discount.DiscountTypeID);
                var response = await validator.ValidateDiscountAsync(
                        isAdd: false,
                        userID: userID,
                        cartID: cart.ID.Value,
                        subtotal: cart.Totals.Total,
                        discount: discount,
                        cartItems: cart.SalesItems,
                        rateQuotes: cart.RateQuotes,
                        context: context)
                    .ConfigureAwait(false);
                if (!response.ActionSucceeded)
                {
                    cartDiscounts.RemoveAll(x => x.DiscountID == discount.ID);
                    continue;
                }
                if (!cartDiscounts.All(x => !x.Active || x.SlaveID != discount.ID && x.MasterID != cart.ID))
                {
                    continue;
                }
                var entity = await context.AppliedCartDiscounts
                    .FilterByActive(true)
                    .FilterIAmARelationshipTableByMasterID<AppliedCartDiscount, Cart, Discount>(cart.ID.Value)
                    .FilterIAmARelationshipTableBySlaveID<AppliedCartDiscount, Cart, Discount>(discount.ID)
                    .SingleAsync()
                    .ConfigureAwait(false);
                cartDiscounts.Add(ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityFull(entity));
            }
            cart.Discounts = cartDiscounts;
        }

        /// <summary>Validates the automatic applied cart item discounts.</summary>
        /// <param name="cart">            The cart.</param>
        /// <param name="discountsToCheck">The discounts to check.</param>
        /// <param name="userID">          Identifier for the user.</param>
        /// <param name="context">         The context.</param>
        /// <returns>A Task.</returns>
        private static async Task ValidateAutoAppliedCartItemDiscountsAsync(
            ICartModel cart,
            IEnumerable<Discount> discountsToCheck,
            int userID,
            IClarityEcommerceEntities context)
        {
            foreach (var discountToCheck in discountsToCheck)
            {
                var validator = GetDiscountValidator(discountToCheck.DiscountTypeID);
                // NOTE: This will add the discount to the item if it's valid and not already on it, won't remove it
                // if it is on it and not valid
                var response = await validator.ValidateDiscountAsync(
                        isAdd: cart.Discounts.All(x => x.DiscountID != discountToCheck.ID),
                        userID: userID,
                        cartID: cart.ID.Value,
                        subtotal: cart.Totals.Total,
                        discount: discountToCheck,
                        cartItems: cart.SalesItems,
                        rateQuotes: cart.RateQuotes,
                        context: context)
                    .ConfigureAwait(false);
                if (!response.ActionSucceeded)
                {
                    // Remove discounts which shouldn't be on there anymore
                    foreach (var salesItem in cart.SalesItems.Where(x => x.Discounts.Any(y => y.DiscountID == discountToCheck.ID)))
                    {
                        salesItem.Discounts = salesItem.Discounts.Where(x => x.DiscountID != discountToCheck.ID).ToList();
                    }
                    continue;
                }
                // Ensure we re-read the list of discounts with the valid discounts on it
                foreach (var salesItem in cart.SalesItems)
                {
                    var entity = await context.AppliedCartItemDiscounts
                        .FilterByActive(true)
                        .FilterIAmARelationshipTableByMasterID<AppliedCartItemDiscount, CartItem, Discount>(Contract.RequiresValidID(salesItem.ID))
                        .FilterIAmARelationshipTableBySlaveID<AppliedCartItemDiscount, CartItem, Discount>(discountToCheck.ID)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);
                    if (entity == null)
                    {
                        continue;
                    }
                    var updatedDiscounts = salesItem.Discounts.Where(x => x.DiscountID != discountToCheck.ID).ToList();
                    updatedDiscounts.Add(
                        ModelMapperForAppliedCartItemDiscount.MapAppliedCartItemDiscountModelFromEntityFull(entity));
                    salesItem.Discounts = updatedDiscounts;
                }
            }
        }

        private static async Task ValidateAutoAppliedUserDiscountsAsync(
            ICartModel cart,
            IEnumerable<Discount> discountsToCheck,
            int userID,
            IClarityEcommerceEntities context)
        {
            foreach (var discountToCheck in discountsToCheck)
            {
                var validator = GetDiscountValidator(discountToCheck.DiscountTypeID);
                // NOTE: This will add the discount to the item if it's valid and not already on it, won't remove it
                // if it is on it and not valid
                var response = await validator.ValidateDiscountAsync(
                        isAdd: cart.Discounts.All(x => x.DiscountID != discountToCheck.ID),
                        userID: userID,
                        cartID: cart.ID.Value,
                        subtotal: cart.Totals.Total,
                        discount: discountToCheck,
                        cartItems: cart.SalesItems,
                        rateQuotes: cart.RateQuotes,
                        context: context)
                    .ConfigureAwait(false);
                if (!response.ActionSucceeded)
                {
                    // Remove discounts which shouldn't be on there anymore
                    foreach (var salesItem in cart.SalesItems.Where(x => x.Discounts.Any(y => y.DiscountID == discountToCheck.ID)))
                    {
                        salesItem.Discounts = salesItem.Discounts.Where(x => x.DiscountID != discountToCheck.ID).ToList();
                    }
                    continue;
                }
                // Ensure we re-read the list of discounts with the valid discounts on it
                foreach (var salesItem in cart.SalesItems)
                {
                    var entity = await context.AppliedCartItemDiscounts
                        .FilterByActive(true)
                        .FilterIAmARelationshipTableByMasterID<AppliedCartItemDiscount, CartItem, Discount>(Contract.RequiresValidID(salesItem.ID))
                        .FilterIAmARelationshipTableBySlaveID<AppliedCartItemDiscount, CartItem, Discount>(discountToCheck.ID)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);
                    if (entity == null)
                    {
                        continue;
                    }
                    var updatedDiscounts = salesItem.Discounts.Where(x => x.DiscountID != discountToCheck.ID).ToList();
                    updatedDiscounts.Add(
                        ModelMapperForAppliedCartItemDiscount.MapAppliedCartItemDiscountModelFromEntityFull(entity));
                    salesItem.Discounts = updatedDiscounts;
                }
            }
        }
#endif

        /// <summary>The reasons to be invalid.</summary>
        private class ReasonsToBeInvalid
        {
            #region Properties
            /// <summary>Gets or sets the discount.</summary>
            /// <value>The discount.</value>
            public IDiscountModel? Discount { get; internal set; }

            /// <summary>Gets a value indicating whether the was skipped.</summary>
            /// <value>True if was skipped, false if not.</value>
            public bool WasSkipped { get; private set; }

            /// <summary>Gets or sets a value indicating whether this discount is applied.</summary>
            /// <value>True if this discount is applied, false if not.</value>
            public bool IsApplied { get; private set; }

            /// <summary>Gets or sets the usage limit remaining applications.</summary>
            /// <value>The usage limit remaining applications.</value>
            public int? UsageLimitRemainingApplications { get; private set; }

            /// <summary>Gets the amount to applications previously consumed by.</summary>
            /// <value>Amount to applications previously consumed by.</value>
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public Dictionary<int, Dictionary<int, decimal>>? ApplicationsPreviouslyConsumedBy { get; private set; }

            /// <summary>Gets the valid by threshold amount products.</summary>
            /// <value>The valid by threshold amount products.</value>
            public Dictionary<int, bool>? ValidByThresholdAmountProducts { get; private set; }

            /// <summary>Gets the valid by brand products.</summary>
            /// <value>The valid by brand products.</value>
            public Dictionary<int, bool>? ValidByBrandProducts { get; private set; }

            /// <summary>Gets the valid by category products.</summary>
            /// <value>The valid by category products.</value>
            public Dictionary<int, bool>? ValidByCategoryProducts { get; private set; }

            /// <summary>Gets the valid by manufacturer products.</summary>
            /// <value>The valid by manufacturer products.</value>
            public Dictionary<int, bool>? ValidByManufacturerProducts { get; private set; }

            /// <summary>Gets the valid by store products.</summary>
            /// <value>The valid by store products.</value>
            public Dictionary<int, bool>? ValidByStoreProducts { get; private set; }

            /// <summary>Gets the valid by franchise products.</summary>
            /// <value>The valid by franchise products.</value>
            public Dictionary<int, bool>? ValidByFranchiseProducts { get; private set; }

            /// <summary>Gets the valid by vendor products.</summary>
            /// <value>The valid by vendor products.</value>
            public Dictionary<int, bool>? ValidByVendorProducts { get; private set; }

            /// <summary>Gets a list of types of the valid by products.</summary>
            /// <value>A list of types of the valid by products.</value>
            public Dictionary<int, bool>? ValidByProductTypes { get; private set; }

            /// <summary>Gets or sets a value indicating whether this discount is supposed to be automatically applied.</summary>
            /// <value>True if this discount is automatic applied, false if not.</value>
            private bool IsAutoApplied { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by deactivated.</summary>
            /// <value>True if invalid by deactivated, false if not.</value>
            private bool InvalidByDeactivated { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by is future.</summary>
            /// <value>True if invalid by is future, false if not.</value>
            private bool InvalidByIsFuture { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by is expired.</summary>
            /// <value>True if invalid by is expired, false if not.</value>
            private bool InvalidByIsExpired { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by exclusivity.</summary>
            /// <value>True if invalid by exclusivity, false if not.</value>
            private bool InvalidByExclusivity { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by usage limit.</summary>
            /// <value>True if invalid by usage limit, false if not.</value>
            private bool InvalidByUsageLimit { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by not authenticated.</summary>
            /// <value>True if invalid by not authenticated, false if not.</value>
            private bool InvalidByNotAuthed { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by brand.</summary>
            /// <value>True if invalid by brand, false if not.</value>
            private bool InvalidByBrand { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by franchise.</summary>
            /// <value>True if invalid by franchise, false if not.</value>
            private bool InvalidByFranchise { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by store.</summary>
            /// <value>True if invalid by store, false if not.</value>
            private bool InvalidByStore { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by user.</summary>
            /// <value>True if invalid by user, false if not.</value>
            private bool InvalidByUser { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by account.</summary>
            /// <value>True if invalid by account, false if not.</value>
            private bool InvalidByAccount { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by account type.</summary>
            /// <value>True if invalid by account type, false if not.</value>
            private bool InvalidByAccountType { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by user role.</summary>
            /// <value>True if invalid by user role, false if not.</value>
            private bool InvalidByUserRole { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by country.</summary>
            /// <value>True if invalid by country, false if not.</value>
            private bool InvalidByCountry { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by product.</summary>
            /// <value>True if invalid by product, false if not.</value>
            private bool InvalidByProduct { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by product type.</summary>
            /// <value>True if invalid by product type, false if not.</value>
            private bool InvalidByProductType { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by manufacturer.</summary>
            /// <value>True if invalid by manufacturer, false if not.</value>
            private bool InvalidByManufacturer { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by vendor.</summary>
            /// <value>True if invalid by vendor, false if not.</value>
            private bool InvalidByVendor { get; set; }

            /// <summary>Gets or sets a value indicating whether the discount invalid by category.</summary>
            /// <value>True if invalid by category, false if not.</value>
            private bool InvalidByCategory { get; set; }

            /// <summary>Gets or sets a value indicating whether the invalid by ship carrier method.</summary>
            /// <value>True if invalid by ship carrier method, false if not.</value>
            private bool InvalidByShipCarrierMethod { get; set; }

            /// <summary>Gets or sets a value indicating whether the invalid by no rate quote selected.</summary>
            /// <value>True if invalid by no rate quote selected, false if not.</value>
            private bool InvalidByNoRateQuoteSelected { get; set; }

            /// <summary>Gets or sets a value indicating whether the invalid by selected rate quote not matching methods.</summary>
            /// <value>True if invalid by selected rate quote not matching methods, false if not.</value>
            private bool InvalidBySelectedRateQuoteNotMatchingMethods { get; set; }

            /// <summary>Gets or sets a value indicating whether the invalid by selected rate quote already free.</summary>
            /// <value>True if invalid by selected rate quote already free, false if not.</value>
            private bool InvalidBySelectedRateQuoteAlreadyFree { get; set; }

            /// <summary>Gets or sets a value indicating whether the invalid by threshold amount.</summary>
            /// <value>True if invalid by threshold amount, false if not.</value>
            private bool InvalidByThresholdAmount { get; set; }

            /// <summary>Gets or sets a message describing the invalid by threshold amount.</summary>
            /// <value>A message describing the invalid by threshold amount.</value>
            private string? InvalidByThresholdAmountMessage { get; set; }

            /// <summary>Gets or sets the cart product IDs.</summary>
            /// <value>The cart product IDs.</value>
            private List<int>? CartProductIDs { get; set; }

            /// <summary>Gets or sets the cart product type IDs.</summary>
            /// <value>The cart product type IDs.</value>
            private List<int>? CartProductTypeIDs { get; set; }

            /// <summary>Gets or sets the brand IDs.</summary>
            /// <value>The brand IDs.</value>
            private List<int>? BrandIDs { get; set; }

            /// <summary>Gets or sets the franchise IDs.</summary>
            /// <value>The franchise IDs.</value>
            private List<int>? FranchiseIDs { get; set; }

            /// <summary>Gets or sets the manufacturer IDs.</summary>
            /// <value>The manufacturer IDs.</value>
            private List<int>? ManufacturerIDs { get; set; }

            /// <summary>Gets or sets the store IDs.</summary>
            /// <value>The store IDs.</value>
            private List<int>? StoreIDs { get; set; }

            /// <summary>Gets or sets the vendor IDs.</summary>
            /// <value>The vendor IDs.</value>
            private List<int>? VendorIDs { get; set; }

            /// <summary>Gets or sets the category IDs.</summary>
            /// <value>The category IDs.</value>
            private List<int>? CategoryIDs { get; set; }

            /// <summary>Gets or sets the ship carrier method IDs.</summary>
            /// <value>The ship carrier method IDs.</value>
            private List<int>? ShipCarrierMethodIDs { get; set; }
            #endregion

            /// <summary>Verify discount and populate the reasons it could be invalid.</summary>
            /// <param name="discountID">           Identifier for the discount.</param>
            /// <param name="cart">                 The cart.</param>
            /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
            /// <param name="skipAutoApplied">      The skip automatic applied.</param>
            /// <param name="contextProfileName">   Name of the context profile.</param>
            /// <returns>A Task.</returns>
            // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
            public async Task PopulateAsync(
                int discountID,
                ICartModel cart,
                IPricingFactoryContextModel? pricingFactoryContext,
                ProcessAutoApplied skipAutoApplied,
                string? contextProfileName)
            {
                ////Discount = await Workflows.Discounts.GetAsync(discountID, contextProfileName).ConfigureAwait(false);
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                Discount = (await context.Discounts
                        .AsNoTracking()
                        .FilterByID(Contract.RequiresValidID(discountID, $"Invalid Discount ID: '{discountID}'"))
                        .Select(x => new
                        {
                            // Base Properties
                            x.ID,
                            x.Active,
                            // Discount Properties
                            x.IsAutoApplied,
                            x.CanCombine,
                            x.StartDate,
                            x.EndDate,
                            x.DiscountTypeID,
                            x.UsageLimitPerAccount,
                            x.UsageLimitPerUser,
                            x.UsageLimitPerCart,
                            x.UsageLimitGlobally,
                            x.DiscountCompareOperator,
                            x.ThresholdAmount,
                            x.Value,
                            x.ValueType,
                            x.BuyXValue,
                            x.GetYValue,
                            // Associated Objects
                            Accounts = x.Accounts!.Where(y => y.Active).Select(y => y.SlaveID),
                            AccountTypes = x.AccountTypes!.Where(y => y.Active).Select(y => y.SlaveID),
                            Brands = x.Brands!.Where(y => y.Active).Select(y => y.SlaveID),
                            Categories = x.Categories!.Where(y => y.Active).Select(y => y.SlaveID),
                            Countries = x.Countries!.Where(y => y.Active).Select(y => y.SlaveID),
                            Franchises = x.Franchises!.Where(y => y.Active).Select(y => y.SlaveID),
                            Manufacturers = x.Manufacturers!.Where(y => y.Active).Select(y => y.SlaveID),
                            Products = x.Products!.Where(y => y.Active).Select(y => y.SlaveID),
                            ProductTypes = x.ProductTypes!.Where(y => y.Active).Select(y => y.SlaveID),
                            ShipCarrierMethods = x.ShipCarrierMethods!.Where(y => y.Active).Select(y => y.SlaveID),
                            Stores = x.Stores!.Where(y => y.Active).Select(y => y.SlaveID),
                            UserRoles = x.UserRoles!.Where(y => y.Active).Select(y => y.RoleName),
                            Users = x.Users!.Where(y => y.Active).Select(y => y.SlaveID),
                            Vendors = x.Vendors!.Where(y => y.Active).Select(y => y.SlaveID),
                        })
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .Select(x => new DiscountModel
                    {
                        // Base Properties
                        ID = x.ID,
                        Active = x.Active,
                        // Discount Properties
                        IsAutoApplied = x.IsAutoApplied,
                        CanCombine = x.CanCombine,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        DiscountTypeID = x.DiscountTypeID,
                        UsageLimitPerAccount = x.UsageLimitPerAccount,
                        UsageLimitPerUser = x.UsageLimitPerUser,
                        UsageLimitPerCart = x.UsageLimitPerCart,
                        UsageLimitGlobally = x.UsageLimitGlobally,
                        DiscountCompareOperator = x.DiscountCompareOperator == null
                            ? Enums.CompareOperator.Undefined
                            : (Enums.CompareOperator)x.DiscountCompareOperator.Value,
                        ThresholdAmount = x.ThresholdAmount,
                        Value = x.Value,
                        ValueType = x.ValueType,
                        BuyXValue = x.BuyXValue,
                        GetYValue = x.GetYValue,
                        // Associated Objects
                        Accounts = x.Accounts.Select(y => new DiscountAccountModel { SlaveID = y }).ToList(),
                        AccountTypes = x.AccountTypes.Select(y => new DiscountAccountTypeModel { SlaveID = y }).ToList(),
                        Brands = x.Brands.Select(y => new DiscountBrandModel { SlaveID = y }).ToList(),
                        Categories = x.Categories.Select(y => new DiscountCategoryModel { SlaveID = y }).ToList(),
                        Countries = x.Countries.Select(y => new DiscountCountryModel { SlaveID = y }).ToList(),
                        Franchises = x.Franchises.Select(y => new DiscountFranchiseModel { SlaveID = y }).ToList(),
                        Manufacturers = x.Manufacturers.Select(y => new DiscountManufacturerModel { SlaveID = y }).ToList(),
                        Products = x.Products.Select(y => new DiscountProductModel { SlaveID = y }).ToList(),
                        ProductTypes = x.ProductTypes.Select(y => new DiscountProductTypeModel { SlaveID = y }).ToList(),
                        ShipCarrierMethods = x.ShipCarrierMethods.Select(y => new DiscountShipCarrierMethodModel { SlaveID = y }).ToList(),
                        Stores = x.Stores.Select(y => new DiscountStoreModel { SlaveID = y }).ToList(),
                        UserRoles = x.UserRoles.Select(y => new DiscountUserRoleModel { RoleName = y }).ToList(),
                        Users = x.Users.Select(y => new DiscountUserModel { SlaveID = y }).ToList(),
                        Vendors = x.Vendors.Select(y => new DiscountVendorModel { SlaveID = y }).ToList(),
                    })
                    .Single();
                // Read the Discount definition
                Contract.RequiresValidID(Discount.ID);
                if (skipAutoApplied == ProcessAutoApplied.Skip && Discount.IsAutoApplied)
                {
                    WasSkipped = true;
                    IsAutoApplied = true;
                    return;
                }
                if (skipAutoApplied == ProcessAutoApplied.Only && !Discount.IsAutoApplied)
                {
                    WasSkipped = true;
                    IsAutoApplied = false;
                    return;
                }
                if (CEFConfigDictionary.LoginEnabled)
                {
                    Discount.Accounts ??= new();
                    Discount.AccountTypes ??= new();
                    Discount.UserRoles ??= new();
                    Discount.Users ??= new();
                    Discount.Countries ??= new();
                }
                if (CEFConfigDictionary.BrandsEnabled)
                {
                    Discount.Brands ??= new();
                }
                if (CEFConfigDictionary.FranchisesEnabled)
                {
                    Discount.Franchises ??= new();
                }
                if (CEFConfigDictionary.StoresEnabled)
                {
                    Discount.Stores ??= new();
                }
                if (CEFConfigDictionary.ManufacturersEnabled)
                {
                    Discount.Manufacturers ??= new();
                }
                if (CEFConfigDictionary.VendorsEnabled)
                {
                    Discount.Vendors ??= new();
                }
                if (CEFConfigDictionary.CategoriesEnabled)
                {
                    Discount.Categories ??= new();
                }
                if (CEFConfigDictionary.ShippingEstimatesEnabled)
                {
                    Discount.ShipCarrierMethods ??= new();
                }
                Discount.Products ??= new();
                Discount.ProductTypes ??= new();
                // Check to see if the Discount is already applied
                IsApplied = CheckIfApplied(Discount, cart);
                // Check if the Discount has been deactivated, isn't available yet or expired
                InvalidByDeactivated = !Discount.Active;
                IsAutoApplied = Discount.IsAutoApplied;
                InvalidByIsFuture = Discount.StartDate.HasValue && Discount.StartDate.Value > DateExtensions.GenDateTime;
                InvalidByIsExpired = Discount.EndDate.HasValue && Discount.EndDate.Value < DateExtensions.GenDateTime;
                if (CEFConfigDictionary.LoginEnabled)
                {
                    if (pricingFactoryContext is null)
                    {
                        InvalidByNotAuthed = true;
                    }
                    InvalidByNotAuthed = !Contract.CheckValidID(pricingFactoryContext?.UserID)
                        && (Discount.Accounts!.Any()
                            || Discount.AccountTypes!.Any()
                            || Discount.UserRoles!.Any()
                            || Discount.Users!.Any()
                            || Discount.Countries!.Any());
                    InvalidByUser
                        = InvalidByAccount
                        = InvalidByAccountType
                        = InvalidByUserRole
                        = InvalidByCountry
                        = InvalidByNotAuthed;
                    if (!InvalidByNotAuthed && Discount.Users!.Any())
                    {
                        InvalidByUser = Discount.Users!
                            .All(x => x.SlaveID != pricingFactoryContext!.UserID);
                    }
                    if (!InvalidByNotAuthed && Discount.Accounts!.Any())
                    {
                        InvalidByAccount = !Contract.CheckValidID(pricingFactoryContext!.AccountID)
                            || Discount.Accounts!
                                .All(x => x.SlaveID != pricingFactoryContext.AccountID);
                    }
                    if (!InvalidByNotAuthed && Discount.AccountTypes!.Any())
                    {
                        InvalidByAccountType = !Contract.CheckValidID(pricingFactoryContext!.AccountTypeID)
                            || Discount.AccountTypes!
                                .All(x => x.SlaveID != pricingFactoryContext.AccountTypeID);
                    }
                    if (!InvalidByNotAuthed && Discount.UserRoles!.Any())
                    {
                        InvalidByUserRole = !Contract.CheckNotEmpty(pricingFactoryContext!.UserRoles)
                            || Discount.UserRoles!
                                .All(x => !pricingFactoryContext.UserRoles!.Contains(x.RoleName!));
                    }
                    if (!InvalidByNotAuthed && Discount.Countries!.Any())
                    {
                        InvalidByCountry = !Contract.CheckValidID(pricingFactoryContext!.CountryID)
                            || Discount.Countries!
                                .All(x => x.SlaveID != pricingFactoryContext.CountryID);
                    }
                }
                // TODO: This may have issues if there's no product assigned to the line item
                CartProductIDs = cart.SalesItems!
                    .Select(y => y.ProductID)
                    .Where(x => x.HasValue)
                    .Cast<int>()
                    .ToList();
                if (Discount.Products.Any())
                {
                    InvalidByProduct = Discount.Products
                        .All(x => !CartProductIDs.Contains(x.SlaveID));
                }
                if (Discount.ProductTypes.Any())
                {
                    CartProductTypeIDs = cart.SalesItems!
                        .Select(y => y.ProductTypeID)
                        .Where(x => x.HasValue)
                        .Cast<int>()
                        .ToList();
                    InvalidByProductType = Discount.ProductTypes
                        .All(x => !CartProductTypeIDs.Contains(x.SlaveID));
                    if (!InvalidByProductType && CartProductTypeIDs.Any())
                    {
                        ValidByProductTypes = context.Products
                            .FilterByActive(true)
                            .FilterByIDs(
                                cart.SalesItems!.Where(x => Contract.CheckValidID(x.ProductID)).Select(x => x.ProductID!.Value))
                            .FilterByTypeIDs<Product, ProductType>(CartProductTypeIDs.Cast<int?>().ToArray())
                            .Select(x => x.ID) // ProductID
                            .Distinct()
                            .ToDictionary(toKey => toKey, _ => true);
                    }
                }
                if (CEFConfigDictionary.BrandsEnabled && Discount.Brands!.Any())
                {
                    BrandIDs = await Contract.RequiresNotNull(context.BrandProducts)
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterIAmARelationshipTableByMasterIDs<BrandProduct, Brand, Product>(Discount.Brands!.Select(x => x.SlaveID))
                        .FilterIAmARelationshipTableBySlaveIDs<BrandProduct, Brand, Product>(CartProductIDs)
                        .Select(x => x.MasterID)
                        .Distinct()
                        .ToListAsync()
                        .ConfigureAwait(false);
                    InvalidByBrand = !Contract.CheckValidID(pricingFactoryContext?.BrandID)
                        || Discount.Brands!
                            .All(x => x.SlaveID != pricingFactoryContext!.BrandID);
                    if (!InvalidByBrand && BrandIDs.Any())
                    {
                        ValidByBrandProducts = context.BrandProducts
                            .FilterByActive(true)
                            .FilterIAmARelationshipTableByMasterIDs<BrandProduct, Brand, Product>(BrandIDs)
                            .FilterIAmARelationshipTableBySlaveIDs<BrandProduct, Brand, Product>(
                                cart.SalesItems!.Where(x => Contract.CheckValidID(x.ProductID)).Select(x => x.ProductID!.Value))
                            .Select(x => x.SlaveID) // ProductID
                            .Distinct()
                            .ToDictionary(toKey => toKey, _ => true);
                    }
                }
                if (CEFConfigDictionary.CategoriesEnabled && Discount.Categories!.Any())
                {
                    CategoryIDs = await Contract.RequiresNotNull(context.ProductCategories)
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterIAmARelationshipTableBySlaveIDs<ProductCategory, Product, Category>(Discount.Categories!.Select(x => x.SlaveID))
                        .FilterIAmARelationshipTableByMasterIDs<ProductCategory, Product, Category>(CartProductIDs)
                        .Select(x => x.SlaveID)
                        .Distinct()
                        .ToListAsync()
                        .ConfigureAwait(false);
                    InvalidByCategory = Discount.Categories!
                        .All(x => !CategoryIDs.Contains(x.SlaveID));
                    if (!InvalidByCategory && CategoryIDs.Any())
                    {
                        ValidByCategoryProducts = context.ProductCategories
                            .FilterByActive(true)
                            .FilterIAmARelationshipTableBySlaveIDs<ProductCategory, Product, Category>(CategoryIDs)
                            .FilterIAmARelationshipTableByMasterIDs<ProductCategory, Product, Category>(
                                cart.SalesItems!.Where(x => Contract.CheckValidID(x.ProductID)).Select(x => x.ProductID!.Value))
                            .Select(x => x.MasterID) // ProductID
                            .Distinct()
                            .ToDictionary(toKey => toKey, _ => true);
                    }
                }
                if (CEFConfigDictionary.FranchisesEnabled && Discount.Franchises!.Any())
                {
                    FranchiseIDs = await Contract.RequiresNotNull(context.FranchiseProducts)
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterIAmARelationshipTableByMasterIDs<FranchiseProduct, Franchise, Product>(Discount.Franchises!.Select(x => x.SlaveID))
                        .FilterIAmARelationshipTableBySlaveIDs<FranchiseProduct, Franchise, Product>(CartProductIDs)
                        .Select(x => x.MasterID)
                        .Distinct()
                        .ToListAsync()
                        .ConfigureAwait(false);
                    InvalidByFranchise = !Contract.CheckValidID(pricingFactoryContext?.FranchiseID)
                        || Discount.Franchises!
                            .All(x => x.SlaveID != pricingFactoryContext!.FranchiseID);
                    if (!InvalidByFranchise && FranchiseIDs.Any())
                    {
                        ValidByFranchiseProducts = context.FranchiseProducts
                            .FilterByActive(true)
                            .FilterIAmARelationshipTableByMasterIDs<FranchiseProduct, Franchise, Product>(FranchiseIDs)
                            .FilterIAmARelationshipTableBySlaveIDs<FranchiseProduct, Franchise, Product>(
                                cart.SalesItems!.Where(x => Contract.CheckValidID(x.ProductID)).Select(x => x.ProductID!.Value))
                            .Select(x => x.SlaveID) // ProductID
                            .Distinct()
                            .ToDictionary(toKey => toKey, _ => true);
                    }
                }
                if (CEFConfigDictionary.ManufacturersEnabled && Discount.Manufacturers!.Any())
                {
                    ManufacturerIDs = await Contract.RequiresNotNull(context.ManufacturerProducts)
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterIAmARelationshipTableByMasterIDs<ManufacturerProduct, Manufacturer, Product>(Discount.Manufacturers!.Select(x => x.SlaveID))
                        .FilterIAmARelationshipTableBySlaveIDs<ManufacturerProduct, Manufacturer, Product>(CartProductIDs)
                        .Select(x => x.MasterID)
                        .Distinct()
                        .ToListAsync()
                        .ConfigureAwait(false);
                    InvalidByManufacturer = Discount.Manufacturers!
                        .All(x => !ManufacturerIDs.Contains(x.SlaveID));
                    if (!InvalidByManufacturer && ManufacturerIDs.Any())
                    {
                        ValidByManufacturerProducts = context.ManufacturerProducts
                            .FilterByActive(true)
                            .FilterIAmARelationshipTableByMasterIDs<ManufacturerProduct, Manufacturer, Product>(ManufacturerIDs)
                            .FilterIAmARelationshipTableBySlaveIDs<ManufacturerProduct, Manufacturer, Product>(
                                cart.SalesItems!.Where(x => Contract.CheckValidID(x.ProductID)).Select(x => x.ProductID!.Value))
                            .Select(x => x.SlaveID) // ProductID
                            .Distinct()
                            .ToDictionary(toKey => toKey, _ => true);
                    }
                }
                if (CEFConfigDictionary.StoresEnabled && Discount.Stores!.Any())
                {
                    StoreIDs = await Contract.RequiresNotNull(context.StoreProducts)
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterIAmARelationshipTableByMasterIDs<StoreProduct, Store, Product>(Discount.Stores!.Select(x => x.SlaveID))
                        .FilterIAmARelationshipTableBySlaveIDs<StoreProduct, Store, Product>(CartProductIDs)
                        .Select(x => x.MasterID)
                        .Distinct()
                        .ToListAsync()
                        .ConfigureAwait(false);
                    InvalidByStore = !Contract.CheckValidID(pricingFactoryContext?.StoreID)
                        || Discount.Stores!
                            .All(x => x.SlaveID != pricingFactoryContext!.StoreID);
                    if (!InvalidByStore && StoreIDs.Any())
                    {
                        ValidByStoreProducts = context.StoreProducts
                            .FilterByActive(true)
                            .FilterIAmARelationshipTableByMasterIDs<StoreProduct, Store, Product>(StoreIDs)
                            .FilterIAmARelationshipTableBySlaveIDs<StoreProduct, Store, Product>(
                                cart.SalesItems!.Where(x => Contract.CheckValidID(x.ProductID)).Select(x => x.ProductID!.Value))
                            .Select(x => x.SlaveID) // ProductID
                            .Distinct()
                            .ToDictionary(toKey => toKey, _ => true);
                    }
                }
                if (CEFConfigDictionary.VendorsEnabled && Discount.Vendors!.Any())
                {
                    VendorIDs = await Contract.RequiresNotNull(context.VendorProducts)
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterIAmARelationshipTableByMasterIDs<VendorProduct, Vendor, Product>(Discount.Vendors!.Select(x => x.SlaveID))
                        .FilterIAmARelationshipTableBySlaveIDs<VendorProduct, Vendor, Product>(CartProductIDs)
                        .Select(x => x.MasterID)
                        .Distinct()
                        .ToListAsync()
                        .ConfigureAwait(false);
                    InvalidByVendor = Discount.Vendors!
                        .All(x => !VendorIDs.Contains(x.SlaveID));
                    if (!InvalidByVendor && VendorIDs.Any())
                    {
                        ValidByVendorProducts = context.VendorProducts
                            .FilterByActive(true)
                            .FilterIAmARelationshipTableByMasterIDs<VendorProduct, Vendor, Product>(VendorIDs)
                            .FilterIAmARelationshipTableBySlaveIDs<VendorProduct, Vendor, Product>(
                                cart.SalesItems!.Where(x => Contract.CheckValidID(x.ProductID)).Select(x => x.ProductID!.Value))
                            .Select(x => x.SlaveID) // ProductID
                            .Distinct()
                            .ToDictionary(toKey => toKey, _ => true);
                    }
                }
                if (CEFConfigDictionary.ShippingEstimatesEnabled && Discount.ShipCarrierMethods!.Any())
                {
                    ShipCarrierMethodIDs = cart.RateQuotes
                        ?.Where(x => x.Active && x.Selected)
                        .Select(x => x.ShipCarrierMethodID)
                        .ToList()
                        ?? new List<int>();
                    InvalidByShipCarrierMethod = Discount.ShipCarrierMethods!
                        .All(x => !ShipCarrierMethodIDs.Contains(x.SlaveID));
                }
                // See if the Discount can combine into the current cart
                InvalidByExclusivity = VerifyCombination(Discount.ID, Discount.CanCombine, cart);
                // Check to see if the Discount hasn't been used too many times
                (InvalidByUsageLimit, UsageLimitRemainingApplications, ApplicationsPreviouslyConsumedBy) = await VerifyUsageLimitsAsync(
                        isAdd: true,
                        discountID: Discount.ID,
                        discountTypeID: Discount.DiscountTypeID,
                        discountUsageLimitPerAccount: Discount.UsageLimitPerAccount,
                        discountUsageLimitPerUser: Discount.UsageLimitPerUser,
                        discountUsageLimitPerCart: Discount.UsageLimitPerCart,
                        discountUsageLimitGlobally: Discount.UsageLimitGlobally,
                        cart: cart,
                        pricingFactoryContext: pricingFactoryContext,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Check specific requirements based on the kind of discount (especially Threshold Amount)
                var dict = ValidateThresholds(Discount, cart, contextProfileName);
                if (dict != null && dict.Any(x => x.Value))
                {
                    ValidByThresholdAmountProducts = dict;
                }
            }

            /// <summary>Determines if the discount should be deactivated.</summary>
            /// <returns>True if it should, false if it should not.</returns>
            // ReSharper disable once CyclomaticComplexity
            public bool CheckShouldDiscountBeDeactivated()
            {
                return IsApplied
                    && (InvalidByDeactivated
                        || IsAutoApplied
                        || InvalidByIsFuture
                        || InvalidByIsExpired
                        || InvalidByNotAuthed
                        || InvalidByBrand
                        || InvalidByFranchise
                        || InvalidByStore
                        || InvalidByUser
                        || InvalidByAccount
                        || InvalidByAccountType
                        || InvalidByUserRole
                        || InvalidByCountry
                        || InvalidByProductType
                        || InvalidByManufacturer
                        || InvalidByVendor
                        || InvalidByCategory
                        || InvalidByShipCarrierMethod
                        || InvalidByNoRateQuoteSelected
                        || InvalidBySelectedRateQuoteNotMatchingMethods
                        || InvalidBySelectedRateQuoteAlreadyFree
                        || InvalidByThresholdAmount);
            }

            /// <summary>Attempts to convert to CEFAR from the given data.</summary>
            /// <param name="skipAutoApplied">The skip automatic applied.</param>
            /// <param name="cefar">          The CEFAR.</param>
            /// <returns>True if it succeeds, false if it fails.</returns>
            // ReSharper disable once CyclomaticComplexity
            public bool TryConvertToCEFAR(ProcessAutoApplied skipAutoApplied, out CEFActionResponse<ReasonsToBeInvalid>? cefar)
            {
                if (WasSkipped)
                {
                    cefar = this.WrapInPassingCEFAR()!;
                    return true;
                }
                if (InvalidByDeactivated || InvalidByBrand)
                {
                    // We don't need to tell the customer it's been deactivated, or that it belongs to a different brand,
                    // just pretend it doesn't exist
                    cefar = this.WrapInFailingCEFAR("ERROR! No discount found for this code.")!;
                    return true;
                }
                if (InvalidByIsFuture)
                {
                    cefar = this.WrapInFailingCEFAR(
                        "ERROR! This discount code doesn't become available until",
                        $"{Discount!.StartDate}")!;
                    return true;
                }
                if (InvalidByIsExpired)
                {
                    cefar = this.WrapInFailingCEFAR(
                        "ERROR! This discount code expired on",
                        $"{Discount!.EndDate}")!;
                    return true;
                }
                if (InvalidByNotAuthed
                    || IsAutoApplied && skipAutoApplied == ProcessAutoApplied.Skip
                    || InvalidByStore
                    || InvalidByFranchise
                    || InvalidByBrand
                    || InvalidByUser
                    || InvalidByAccount
                    || InvalidByAccountType
                    || InvalidByUserRole
                    || InvalidByCountry
                    || InvalidByProduct
                    || InvalidByProductType
                    || InvalidByManufacturer
                    || InvalidByVendor
                    || InvalidByCategory
                    || InvalidByShipCarrierMethod
                    || InvalidByNoRateQuoteSelected
                    || InvalidBySelectedRateQuoteNotMatchingMethods
                    || InvalidBySelectedRateQuoteAlreadyFree
                    || InvalidByThresholdAmount
                    || InvalidByExclusivity
                    || InvalidByUsageLimit)
                {
                    var messages = new[]
                    {
                        "ERROR! You do not meet the requirements for this Discount.",
                        InvalidByNotAuthed ? "InvalidByNotAuthed" : null,
                        IsAutoApplied && skipAutoApplied == ProcessAutoApplied.Skip ? "IsAutoApplied" : null,
                        InvalidByStore ? "InvalidByStore" : null,
                        InvalidByFranchise ? "InvalidByFranchise" : null,
                        InvalidByBrand ? "InvalidByBrand" : null,
                        InvalidByUser ? "InvalidByUser" : null,
                        InvalidByAccount ? "InvalidByAccount" : null,
                        InvalidByAccountType ? "InvalidByAccountType" : null,
                        InvalidByUserRole ? "InvalidByUserRole" : null,
                        InvalidByCountry ? "InvalidByCountry" : null,
                        InvalidByProduct ? "InvalidByProduct" : null,
                        InvalidByProductType ? "InvalidByProductType " : null,
                        InvalidByManufacturer ? "InvalidByManufacturer" : null,
                        InvalidByVendor ? "InvalidByVendor" : null,
                        InvalidByCategory ? "InvalidByCategory" : null,
                        InvalidByShipCarrierMethod ? "InvalidByShipCarrierMethod" : null,
                        InvalidByNoRateQuoteSelected ? "InvalidByNoRateQuoteSelected" : null,
                        InvalidBySelectedRateQuoteNotMatchingMethods ? "InvalidBySelectedRateQuoteNotMatchingMethods" : null,
                        InvalidBySelectedRateQuoteAlreadyFree ? "InvalidBySelectedRateQuoteAlreadyFree" : null,
                        InvalidByThresholdAmount ? InvalidByThresholdAmountMessage : null,
                        InvalidByExclusivity ? "ERROR! The requested discount can not be combined with other discounts on your cart." : null,
                        InvalidByUsageLimit ? "ERROR! This discount has been used the maximum number of times." : null,
                    };
                    cefar = this.WrapInFailingCEFAR(messages.Where(x => x != null).ToArray()!)!;
                    return true;
                }
                if (IsApplied)
                {
                    // Already applied
                    cefar = this.WrapInPassingCEFAR()!;
                    return true;
                }
                // Say that we aren't done yet
                cefar = null;
                return false;
            }

            /// <summary>Check if applied.</summary>
            /// <param name="discount">          The discount.</param>
            /// <param name="cart">              The cart.</param>
            /// <returns>True if it succeeds, false if it fails.</returns>
            private static bool CheckIfApplied(IDiscountModel discount, ICartModel cart)
            {
                return discount.DiscountTypeID switch
                {
                    (int)Enums.DiscountType.Order => cart.Discounts!
                        .Any(x => x.Active && x.SlaveID == discount.ID),
                    (int)Enums.DiscountType.Shipping => cart.Discounts!
                        .Any(x => x.Active && x.SlaveID == discount.ID),
                    (int)Enums.DiscountType.BuyXGetY => cart.SalesItems!
                        .SelectMany(x => x.Discounts!)
                        .Any(x => x.Active && x.SlaveID == discount.ID),
                    (int)Enums.DiscountType.Product => cart.SalesItems!
                        .SelectMany(x => x.Discounts!)
                        .Any(x => x.Active && x.SlaveID == discount.ID),
                    _ => throw new ArgumentException("Unknown discount type"),
                };
            }

            /// <summary>Verify combination.</summary>
            /// <param name="discountID">Identifier for the discount.</param>
            /// <param name="canCombine">True if this discount can combine.</param>
            /// <param name="cart">      The cart.</param>
            /// <returns>True if it succeeds, false if it fails.</returns>
            private static bool VerifyCombination(int discountID, bool canCombine, ICartModel cart)
            {
                if (!canCombine)
                {
                    if (cart.Discounts!.Any(x => x.Active && x.SlaveID != discountID)
                        || cart.SalesItems!.Where(x => x.Active).SelectMany(x => x.Discounts!).Any(x => x.Active && x.SlaveID != discountID))
                    {
                        // Others are set, cannot add to a list that is already set up
                        return true;
                    }
                    // No other discounts, good to add
                    return false;
                }
                // This discount can combine, look for others applied that can't
                if (cart.Discounts!.Any(x => x.Active && !x.DiscountCanCombine)
                    || cart.SalesItems!.Where(x => x.Active).SelectMany(x => x.Discounts!).Any(x => x.Active && !x.DiscountCanCombine))
                {
                    return true;
                }
                // Other discount's are all combinable, good to add
                return false;
            }

            /// <summary>Verify usage limits.</summary>
            /// <param name="isAdd">                       True if is adding the discount (vs verifying existing).</param>
            /// <param name="discountID">                  Identifier for the discount.</param>
            /// <param name="discountTypeID">              Identifier for the discount type.</param>
            /// <param name="discountUsageLimitPerAccount">The discount usage limit per account.</param>
            /// <param name="discountUsageLimitPerUser">   The discount usage limit per user.</param>
            /// <param name="discountUsageLimitPerCart">   The discount usage limit per cart.</param>
            /// <param name="discountUsageLimitGlobally">  The discount usage limit globally.</param>
            /// <param name="cart">                        The cart.</param>
            /// <param name="pricingFactoryContext">       Context for the pricing factory.</param>
            /// <param name="contextProfileName">          Name of the context profile.</param>
            /// <returns>A Task{bool}.</returns>
            private static async Task<(bool overLimit, int? remainingApplications, Dictionary<int, Dictionary<int, decimal>>? applicationsPreviouslyConsumedBy)> VerifyUsageLimitsAsync(
                bool isAdd,
                int discountID,
                int discountTypeID,
                int? discountUsageLimitPerAccount,
                int? discountUsageLimitPerUser,
                int? discountUsageLimitPerCart,
                int? discountUsageLimitGlobally,
                IBaseModel cart,
                IPricingFactoryContextModel? pricingFactoryContext,
                string? contextProfileName)
            {
                if (discountUsageLimitPerAccount <= 0
                    && discountUsageLimitPerUser <= 0
                    && discountUsageLimitPerCart <= 0
                    && discountUsageLimitGlobally <= 0)
                {
                    // No limit to this discount
                    return (false, null, null);
                }
                Dictionary<int, Dictionary<int, decimal>>? applicationsPreviouslyConsumedBy = null;
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var pastUsagesFromOtherOrders = 0;
                if (discountUsageLimitGlobally > 0)
                {
                    pastUsagesFromOtherOrders += await CountUsagesOfThisDiscountInnerAsync<SalesOrder,
                                AppliedSalesOrderDiscount,
                                SalesOrderItem,
                                AppliedSalesOrderItemDiscount,
                                SalesOrderItemTarget>(
                            // Not this account
                            accountIDNot: pricingFactoryContext?.AccountID,
                            // Not this user
                            userIDNot: pricingFactoryContext?.UserID,
                            // ReSharper disable once PossibleInvalidOperationException (verified before coming in here)
                            discountID: discountID,
                            discountType: discountTypeID,
                            context: context)
                        .ConfigureAwait(false);
                }
                var pastUsagesFromYourOrders = 0;
                if (discountUsageLimitGlobally > 0 || discountUsageLimitPerAccount > 0 || discountUsageLimitPerUser > 0)
                {
                    pastUsagesFromYourOrders += await CountUsagesOfThisDiscountInnerAsync<SalesOrder,
                                AppliedSalesOrderDiscount,
                                SalesOrderItem,
                                AppliedSalesOrderItemDiscount,
                                SalesOrderItemTarget>(
                            accountIDOnly: discountUsageLimitGlobally > 0 || discountUsageLimitPerAccount > 0 ? pricingFactoryContext?.AccountID : null,
                            userIDOnly: discountUsageLimitPerUser > 0 ? pricingFactoryContext?.UserID : null,
                            // ReSharper disable once PossibleInvalidOperationException (verified before coming in here)
                            discountID: discountID,
                            discountType: discountTypeID,
                            context: context)
                        .ConfigureAwait(false);
                }
                /*
                var currentUsagesInThisCart = 0;
                if (isAdd)
                {
                    currentUsagesInThisCart += await CountUsagesOfThisDiscountInnerAsync<Cart,
                                AppliedCartDiscount,
                                CartItem,
                                AppliedCartItemDiscount,
                                CartItemTarget>(
                            cartIDOnly: cart.ID, // Count only this cart against it
                            // If it's more than 4 hours ago, ignore it, they should have checked out sooner
                            minimum: DateExtensions.GenDateTime.AddHours(-4),
                            // ReSharper disable once PossibleInvalidOperationException (verified before coming in here)
                            discountID: discountID,
                            discountType: discountTypeID,
                            context: context)
                        .ConfigureAwait(false);
                }
                */
                var currentUsagesInOtherCarts = 0;
                if (isAdd)
                {
                    applicationsPreviouslyConsumedBy = new();
                    currentUsagesInOtherCarts += await CountUsagesOfThisDiscountInnerAsync<Cart,
                                AppliedCartDiscount,
                                CartItem,
                                AppliedCartItemDiscount,
                                CartItemTarget>(
                            // Don't count this cart against it
                            cartIDNot: cart.ID,
                            // If it's more than 4 hours ago, ignore it, they should have checked out sooner
                            minimum: DateExtensions.GenDateTime.AddHours(-4),
                            // ReSharper disable once PossibleInvalidOperationException (verified before coming in here)
                            discountID: discountID,
                            discountType: discountTypeID,
                            applicationsPreviouslyConsumedBy: applicationsPreviouslyConsumedBy,
                            context: context)
                        .ConfigureAwait(false);
                }
                var thisCartLimit = discountUsageLimitPerCart > 0
                    ? Math.Max(0, discountUsageLimitPerCart.Value /*- currentUsagesInThisCart*/)
                    : int.MaxValue;
                var globalLimit = discountUsageLimitGlobally > 0
                    ? Math.Max(0, discountUsageLimitGlobally.Value - pastUsagesFromOtherOrders - pastUsagesFromYourOrders - currentUsagesInOtherCarts)
                    : int.MaxValue;
                var thisAccountLimit = discountUsageLimitPerAccount > 0
                    ? Math.Max(0, discountUsageLimitPerAccount.Value - pastUsagesFromYourOrders)
                    : int.MaxValue;
                var thisUserLimit = discountUsageLimitPerUser > 0
                    ? Math.Max(0, discountUsageLimitPerUser.Value - pastUsagesFromYourOrders)
                    : int.MaxValue;
                var lowestLimit = new[] { thisCartLimit, thisAccountLimit, thisUserLimit, globalLimit, }.Min();
                if (lowestLimit == int.MaxValue)
                {
                    // Not limited
                    return (false, null, null);
                }
                // ReSharper disable once UnusedVariable (Keep for debugging)
                var previousUsagesFromYourOrdersAndOtherCarts = pastUsagesFromOtherOrders + pastUsagesFromYourOrders + currentUsagesInOtherCarts;
                return (lowestLimit <= 0, lowestLimit, applicationsPreviouslyConsumedBy);
            }

            /// <summary>Count usages of this discount inner.</summary>
            /// <typeparam name="TCollection">        Type of the collection.</typeparam>
            /// <typeparam name="TCollectionDiscount">Type of the collection discount.</typeparam>
            /// <typeparam name="TItem">              Type of the item.</typeparam>
            /// <typeparam name="TItemDiscount">      Type of the item discount.</typeparam>
            /// <typeparam name="TItemTarget">        Type of the item target.</typeparam>
            /// <param name="discountID">                      Identifier for the discount.</param>
            /// <param name="discountType">                    Type of the discount.</param>
            /// <param name="context">                         The context.</param>
            /// <param name="applicationsPreviouslyConsumedBy">Amount to applications previously consumed by.</param>
            /// <param name="accountIDOnly">                   The account identifier only.</param>
            /// <param name="accountIDNot">                    The account identifier not.</param>
            /// <param name="userIDOnly">                      The user identifier only.</param>
            /// <param name="userIDNot">                       The user identifier not.</param>
            /// <param name="cartIDOnly">                      The cart identifier only.</param>
            /// <param name="cartIDNot">                       The cart identifier not.</param>
            /// <param name="minimum">                         The minimum.</param>
            /// <returns>The total number of usages of this discount inner.</returns>
            // ReSharper disable once CognitiveComplexity
            private static Task<int> CountUsagesOfThisDiscountInnerAsync<TCollection, TCollectionDiscount, TItem, TItemDiscount, TItemTarget>(
                    int discountID,
                    int discountType,
                    IDbContext context,
                    IDictionary<int, Dictionary<int, decimal>>? applicationsPreviouslyConsumedBy = null,
                    int? accountIDOnly = null,
                    int? accountIDNot = null,
                    int? userIDOnly = null,
                    int? userIDNot = null,
                    int? cartIDOnly = null,
                    int? cartIDNot = null,
                    DateTime? minimum = null)
                where TCollection : class, ISalesCollectionBase, IHaveAppliedDiscountsBase<TCollection, TCollectionDiscount>
                where TCollectionDiscount : class, IAppliedDiscountBase<TCollection, TCollectionDiscount>
                where TItem : class, ISalesItemBase<TItem, TItemDiscount, TItemTarget>
                where TItemDiscount : class, IAppliedDiscountBase<TItem, TItemDiscount>
                where TItemTarget : class, ISalesItemTargetBase
            {
                switch (discountType)
                {
                    case (int)Enums.DiscountType.Product:
                    case (int)Enums.DiscountType.BuyXGetY:
                    {
                        var query = context.Set<TItemDiscount>()
                            .FilterByActive(true)
                            .FilterIAmARelationshipTableBySlaveID<TItemDiscount, TItem, Discount>(discountID)
                            .FilterByModifiedSince(minimum);
                        if (Contract.CheckValidID(userIDOnly))
                        {
                            query = query.Where(x => x.Master!.Master!.UserID == userIDOnly!.Value);
                        }
                        if (Contract.CheckValidID(userIDNot))
                        {
                            query = query.Where(x => x.Master!.Master!.UserID != userIDNot!.Value);
                        }
                        if (Contract.CheckValidID(accountIDOnly))
                        {
                            query = query.Where(x => x.Master!.Master!.AccountID == accountIDOnly!.Value);
                        }
                        if (Contract.CheckValidID(accountIDNot))
                        {
                            query = query.Where(x => x.Master!.Master!.AccountID != accountIDNot!.Value);
                        }
                        if (Contract.CheckValidID(cartIDOnly))
                        {
                            query = query.Where(x => x.Master!.MasterID == cartIDOnly!.Value);
                        }
                        if (Contract.CheckValidID(cartIDNot))
                        {
                            query = query.Where(x => x.Master!.MasterID != cartIDNot!.Value);
                        }
                        // ReSharper disable once InvertIf (Keeping for consistency)
                        if (applicationsPreviouslyConsumedBy != null && Contract.CheckValidID(cartIDOnly))
                        {
                            if (!applicationsPreviouslyConsumedBy.ContainsKey(cartIDOnly!.Value))
                            {
                                applicationsPreviouslyConsumedBy[cartIDOnly.Value] = new();
                            }
                            applicationsPreviouslyConsumedBy[cartIDOnly.Value] = query
                                .ToDictionary(
                                    toKey => toKey.MasterID,
                                    toValue => toValue.DiscountTotal);
                        }
                        // Applications could use more than one each, but default to 1 if not set (schema was added later)
                        return query.Select(x => x.ApplicationsUsed ?? 1).DefaultIfEmpty(0).SumAsync();
                        // return query.CountAsync();
                    }
                    case (int)Enums.DiscountType.Order:
                    case (int)Enums.DiscountType.Shipping:
                    {
                        var query = context.Set<TCollectionDiscount>()
                            .FilterByActive(true)
                            .FilterIAmARelationshipTableBySlaveID<TCollectionDiscount, TCollection, Discount>(discountID)
                            .FilterByModifiedSince(minimum);
                        if (Contract.CheckValidID(userIDOnly))
                        {
                            query = query.Where(x => x.Master!.UserID == userIDOnly!.Value);
                        }
                        if (Contract.CheckValidID(userIDNot))
                        {
                            query = query.Where(x => x.Master!.UserID != userIDNot!.Value);
                        }
                        if (Contract.CheckValidID(accountIDOnly))
                        {
                            query = query.Where(x => x.Master!.AccountID == accountIDOnly!.Value);
                        }
                        if (Contract.CheckValidID(accountIDNot))
                        {
                            query = query.Where(x => x.Master!.AccountID != accountIDNot!.Value);
                        }
                        if (Contract.CheckValidID(cartIDOnly))
                        {
                            query = query.Where(x => x.MasterID == cartIDOnly!.Value);
                        }
                        if (Contract.CheckValidID(cartIDNot))
                        {
                            query = query.Where(x => x.MasterID != cartIDNot!.Value);
                        }
                        // Applications could use more than one each, but default to 1 if not set (schema was added later)
                        return query.Select(x => x.ApplicationsUsed ?? 1).DefaultIfEmpty(0).SumAsync();
                        // return query.CountAsync();
                    }
                    default: // NOTE: This should never happen
                    {
                        throw new ArgumentException("Unknown Discount Type");
                    }
                }
            }

            /// <summary>Validates the discount can be added.</summary>
            /// <param name="discount">          The discount.</param>
            /// <param name="cart">              The cart.</param>
            /// <param name="contextProfileName">Name of the context profile.</param>
            /// <returns>A Dictionary{int,bool}.</returns>
            private Dictionary<int, bool>? ValidateThresholds(
                IDiscountModel discount,
                ICartModel cart,
                string? contextProfileName)
            {
                // NOTE: We've already verified a lot of general requirements, this is just to check to specific things
                // per the type of discount
                switch (discount.DiscountTypeID)
                {
                    case (int)Enums.DiscountType.Order:
                    {
                        ValidateDiscountCodeCanBeAddedOrder(
                            discount.DiscountCompareOperator,
                            discount.ThresholdAmount,
                            discount.DiscountTypeID,
                            cart);
                        return null;
                    }
                    case (int)Enums.DiscountType.Shipping:
                    {
                        ValidateDiscountCodeCanBeAddedShipping(
                            discount.DiscountCompareOperator,
                            discount.ThresholdAmount,
                            discount.DiscountTypeID,
                            discount.ShipCarrierMethods,
                            cart,
                            contextProfileName);
                        return null;
                    }
                    case (int)Enums.DiscountType.Product:
                    {
                        return ValidateDiscountCodeCanBeAddedProduct(
                            discount.DiscountCompareOperator,
                            discount.ThresholdAmount,
                            discount.DiscountTypeID,
                            cart);
                    }
                    case (int)Enums.DiscountType.BuyXGetY:
                    {
                        ValidateDiscountCodeCanBeAddedBuyXGetY(
                            discount.DiscountCompareOperator,
                            discount.ThresholdAmount,
                            discount.DiscountTypeID,
                            discount.BuyXValue ?? 0m,
                            discount.GetYValue ?? 0m,
                            cart);
                        return null;
                    }
                    default:
                    {
                        throw new ArgumentException("Unknown discount type");
                    }
                }
            }

            /// <summary>Validates the discount code can be added (order).</summary>
            /// <param name="compareOperator">The compare operator.</param>
            /// <param name="thresholdAmount">The threshold amount.</param>
            /// <param name="discountTypeID"> Identifier for the discount type.</param>
            /// <param name="cart">           The cart.</param>
            private void ValidateDiscountCodeCanBeAddedOrder(
                Enums.CompareOperator compareOperator,
                decimal thresholdAmount,
                int discountTypeID,
                ISalesCollectionBaseModel cart)
            {
                ValidateAmountThreshold(
                    cart.Totals.SubTotal,
                    compareOperator,
                    thresholdAmount,
                    discountTypeID);
            }

            /// <summary>Validates the discount code can be added (shipping).</summary>
            /// <param name="compareOperator">   The compare operator.</param>
            /// <param name="thresholdAmount">   The threshold amount.</param>
            /// <param name="discountTypeID">    Identifier for the discount type.</param>
            /// <param name="shipCarrierMethods">The ship carrier methods.</param>
            /// <param name="cart">              The cart.</param>
            /// <param name="contextProfileName">Name of the context profile.</param>
            private void ValidateDiscountCodeCanBeAddedShipping(
                Enums.CompareOperator compareOperator,
                decimal thresholdAmount,
                int discountTypeID,
                IReadOnlyCollection<IDiscountShipCarrierMethodModel>? shipCarrierMethods,
                ISalesCollectionBaseModel cart,
                string? contextProfileName)
            {
                // Ensure the data is there
                if (Contract.CheckEmpty(cart.RateQuotes))
                {
                    // TODO: We need to read the rates selected on target carts too
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    // Go ahead and pull again to be sure
                    cart.RateQuotes = context.RateQuotes
                        .FilterByActive(true)
                        .FilterRateQuotesByCartID(cart.ID)
                        ////.FilterRateQuotesBySelected(true)
                        .SelectListRateQuoteAndMapToRateQuoteModel(null, null, null, contextProfileName)
                        .results
                        .ToList();
                }
                // Are any selected?
                if (Contract.CheckEmpty(cart.RateQuotes!.Where(x => x.Selected)))
                {
                    InvalidByNoRateQuoteSelected = true;
                    return;
                }
                // There should only be one selected per cart
                var selectedRateQuote = cart.RateQuotes!.Single(x => x.Selected);
                if (Contract.CheckNotEmpty(shipCarrierMethods)
                    && shipCarrierMethods!.All(x => x.SlaveID != selectedRateQuote.ShipCarrierMethodID))
                {
                    InvalidBySelectedRateQuoteNotMatchingMethods = true;
                    return;
                }
                // Does this shipping rate actually have a cost that we can discount?
                if (selectedRateQuote.Rate is null or <= 0m)
                {
                    InvalidBySelectedRateQuoteAlreadyFree = true;
                    return;
                }
                ValidateAmountThreshold(
                    selectedRateQuote.Rate.Value,
                    compareOperator,
                    thresholdAmount,
                    discountTypeID);
            }

            /// <summary>Validates the discount code can be added (product).</summary>
            /// <param name="compareOperator">The compare operator.</param>
            /// <param name="thresholdAmount">The threshold amount.</param>
            /// <param name="discountTypeID"> Identifier for the discount type.</param>
            /// <param name="cart">           The cart.</param>
            private Dictionary<int, bool> ValidateDiscountCodeCanBeAddedProduct(
                Enums.CompareOperator compareOperator,
                decimal thresholdAmount,
                int discountTypeID,
                ICartModel cart)
            {
                var dict = new Dictionary<int, bool>();
                foreach (var group in cart.SalesItems!.Where(x => x.Active).GroupBy(x => x.ProductID))
                {
                    if (!Contract.CheckValidID(group.Key))
                    {
                        continue;
                    }
                    var sum = group.Sum(x => (x.UnitSoldPrice ?? x.UnitCorePrice) * x.TotalQuantity);
                    dict[group.Key!.Value] = ValidateAmountThreshold(sum, compareOperator, thresholdAmount, discountTypeID);
                }
                // ReSharper disable once InvertIf (Keeping for consistency)
                if (dict.Any(x => x.Value))
                {
                    // At least one of them did pass, so clear the message and failure
                    InvalidByThresholdAmount = false;
                    InvalidByThresholdAmountMessage = null;
                }
                return dict;
            }

            /// <summary>Validates the discount code can be added (buy x get y).</summary>
            /// <param name="compareOperator">The compare operator.</param>
            /// <param name="thresholdAmount">The threshold amount.</param>
            /// <param name="discountTypeID"> Identifier for the discount type.</param>
            /// <param name="buyXValue">      The buy x coordinate value.</param>
            /// <param name="getYValue">      The get y coordinate value.</param>
            /// <param name="cart">           The cart.</param>
            private void ValidateDiscountCodeCanBeAddedBuyXGetY(
                Enums.CompareOperator compareOperator,
                decimal thresholdAmount,
                int discountTypeID,
                decimal buyXValue,
                decimal getYValue,
                ICartModel cart)
            {
                if (buyXValue == 0m || getYValue == 0m)
                {
                    InvalidByThresholdAmount = true;
                    InvalidByThresholdAmountMessage = "ERROR! Invalid Buy X Get Y values.";
                    return;
                }
                ValidateAmountThreshold(
                    cart.SalesItems!.Sum(x => (x.UnitSoldPrice ?? x.UnitCorePrice) * x.TotalQuantity),
                    compareOperator,
                    thresholdAmount,
                    discountTypeID);
            }

            /// <summary>Validates the amount threshold.</summary>
            /// <param name="compareAmount">  The compare amount.</param>
            /// <param name="compareOperator">The compare operator.</param>
            /// <param name="thresholdAmount">The threshold amount.</param>
            /// <param name="discountTypeID"> Identifier for the discount type.</param>
            /// <returns>True if it succeeds, false if it fails.</returns>
            private bool ValidateAmountThreshold(
                decimal compareAmount,
                Enums.CompareOperator compareOperator,
                decimal thresholdAmount,
                int discountTypeID)
            {
                switch (compareOperator)
                {
                    case Enums.CompareOperator.LessThan:
                    {
                        InvalidByThresholdAmount = !(compareAmount < thresholdAmount);
                        InvalidByThresholdAmountMessage = InvalidByThresholdAmount
                            ? $"ERROR! {(Enums.DiscountType)discountTypeID} amount should be less than"
                                + $" {thresholdAmount:c} (actual: {compareAmount:c})."
                            : null;
                        return !InvalidByThresholdAmount;
                    }
                    case Enums.CompareOperator.LessThanOrEqualTo:
                    {
                        InvalidByThresholdAmount = !(compareAmount <= thresholdAmount);
                        InvalidByThresholdAmountMessage = InvalidByThresholdAmount
                            ? $"ERROR! {(Enums.DiscountType)discountTypeID} amount should be less than or equal to"
                                + $" {thresholdAmount:c} (actual: {compareAmount:c})."
                            : null;
                        return !InvalidByThresholdAmount;
                    }
                    case Enums.CompareOperator.GreaterThan:
                    {
                        InvalidByThresholdAmount = !(compareAmount > thresholdAmount);
                        InvalidByThresholdAmountMessage = InvalidByThresholdAmount
                            ? $"ERROR! {(Enums.DiscountType)discountTypeID} amount should be greater than"
                                + $" {thresholdAmount:c} (actual: {compareAmount:c})."
                            : null;
                        return !InvalidByThresholdAmount;
                    }
                    case Enums.CompareOperator.GreaterThanOrEqualTo:
                    {
                        InvalidByThresholdAmount = !(compareAmount >= thresholdAmount);
                        InvalidByThresholdAmountMessage = InvalidByThresholdAmount
                            ? $"ERROR! {(Enums.DiscountType)discountTypeID} amount should be greater than or equal to"
                                + $" {thresholdAmount:c} (actual: {compareAmount:c})."
                            : null;
                        return !InvalidByThresholdAmount;
                    }
                    // ReSharper disable once RedundantCaseLabel
                    case Enums.CompareOperator.Undefined:
                    default:
                    {
                        return true;
                    }
                }
            }
        }
    }
}
