// <copyright file="CartCRUDWorkflow.extended.Compare.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the compare cart workflow class</summary>
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
    using Mapper;
    using Models;
    using Utilities;

    public partial class CartWorkflow
    {
        /// <inheritdoc/>
        public async Task<(ICartModel? cart, Guid? updatedSessionID)> CompareGetAsync(
            CompareCartBySessionLookupKey lookupKey,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await CompareGetEntityAsync(lookupKey, context).ConfigureAwait(false);
            if (entity?.SalesItems!.Any(x => x.Active) != true)
            {
                return (null, Guid.NewGuid());
            }
            await AssignUserIDToCartIfNullAsync(entity, lookupKey.UserID, context).ConfigureAwait(false);
            await AssignAccountIDToCartIfNullAsync(entity, lookupKey.AccountID, context).ConfigureAwait(false);
            var model = entity.StaticMap(contextProfileName);
            await AppendPriceDataToSalesItemsAsync(
                    model: model,
                    isStatic: true,
                    pricingFactoryContext: Contract.RequiresNotNull(pricingFactoryContext),
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return (model, entity.SessionID ?? lookupKey.SessionID);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<Guid?>> CompareAddItemAsync(
            CompareCartBySessionLookupKey lookupKey,
            int productID,
            SerializableAttributesDictionary? attributes,
            string? contextProfileName)
        {
            _ = Contract.RequiresValidID(productID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            _ = Contract.RequiresNotNull(
                await context.Products
                    .AsNoTracking()
                    .FilterByID(productID)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false),
                $"ERROR! Invalid Product ID '{productID}'");
            var query = context.CartItems
                .AsNoTracking()
                .FilterByActive(true)
                .FilterCartItemsByCartBrandID(lookupKey.BrandID)
                .FilterCartItemsByCartFranchiseID(lookupKey.FranchiseID)
                .FilterCartItemsByCartStoreID(lookupKey.StoreID)
                .FilterCartItemsByProductID(productID)
                .FilterCartItemsByCartTypeName(lookupKey.TypeKey, true);
            int? existingID = null;
            if (Contract.CheckValidID(lookupKey.UserID))
            {
                query = query.FilterCartItemsByCartUserID(lookupKey.UserID);
                if (Contract.CheckValidID(lookupKey.AccountID))
                {
                    query = query.FilterCartItemsByCartAccountID(lookupKey.AccountID);
                }
                existingID = await query
                    .Select(x => (int?)x.ID)
                    .FirstOrDefaultAsync();
                if (Contract.CheckValidID(existingID))
                {
                    return await GetCompareCartSessionByItemIDAsync(existingID!.Value, contextProfileName)
                        .AwaitAndWrapResultInPassingCEFARIfNotNullAsync()
                        .ConfigureAwait(false);
                }
            }
            if (Contract.CheckInvalidID(existingID))
            {
                existingID = await query
                    .FilterCartItemsByCartSessionID(lookupKey.SessionID)
                    .Select(x => (int?)x.ID)
                    .FirstOrDefaultAsync();
                if (Contract.CheckValidID(existingID))
                {
                    return ((Guid?)lookupKey.SessionID).WrapInPassingCEFARIfNotNull();
                }
            }
            // Does not already exist, resolve the compare cart itself and add to it
            var timestamp = DateExtensions.GenDateTime;
            var entity = await Workflows.CartItems.CreateEntityWithoutSavingAsync(
                    new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                    {
                        Active = true,
                        CreatedDate = timestamp,
                        ProductID = productID,
                        Quantity = 1m,
                        SerializableAttributes = attributes ?? new(),
                    },
                    timestamp,
                    contextProfileName)
                .ConfigureAwait(false);
            var cartID = await CompareCheckExistsAsync(lookupKey, contextProfileName).ConfigureAwait(false);
            if (Contract.CheckInvalidID(cartID))
            {
                var cartResponse = await GenerateCompareCartAsync(lookupKey, false, context, contextProfileName).ConfigureAwait(false);
                if (!cartResponse.ActionSucceeded)
                {
                    return CEFAR.FailingCEFAR<Guid?>("ERROR! Something about saving to the database failed");
                }
                var cart = cartResponse.Result!;
                cart.SalesItems!.Add(entity.Result!);
                context.Carts.Add((Cart)cart);
                return await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false)
                    ? cart.SessionID.WrapInPassingCEFARIfNotNull()
                    : CEFAR.FailingCEFAR<Guid?>("ERROR! Something about saving to the database failed");
            }
            entity.Result!.MasterID = cartID!.Value;
            context.CartItems.Add(entity.Result);
            return await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false)
                ? (await GetCompareCartSessionByIDAsync(cartID.Value, contextProfileName).ConfigureAwait(false)).WrapInPassingCEFARIfNotNull()
                : CEFAR.FailingCEFAR<Guid?>("ERROR! Something about saving to the database failed");
        }

        /// <inheritdoc/>
        public Task<CEFActionResponse> CompareRemoveAsync(int id, string? contextProfileName)
        {
            return Workflows.CartItems.DeactivateAsync(Contract.RequiresValidID(id), contextProfileName);
        }

        /// <inheritdoc/>
        public Task<CEFActionResponse> CompareRemoveAsync(
            CompareCartBySessionLookupKey lookupKey,
            int productID,
            string? contextProfileName)
        {
            return CompareRemoveInnerAsync(
                lookupKey,
                Contract.RequiresValidID(productID),
                contextProfileName);
        }

        /// <inheritdoc/>
        public Task<CEFActionResponse> CompareClearAsync(
            CompareCartBySessionLookupKey lookupKey,
            string? contextProfileName)
        {
            return CompareRemoveInnerAsync(lookupKey, null, contextProfileName);
        }

        /// <summary>Gets compare cart session by item identifier.</summary>
        /// <param name="cartItemID">        Identifier for the cart item.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The compare cart session by item identifier.</returns>
        private static async Task<Guid?> GetCompareCartSessionByItemIDAsync(int cartItemID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Carts
                .AsNoTracking()
                .Where(x => x.SalesItems!.Any(y => y.ID == cartItemID))
                .Select(x => x.SessionID)
                .SingleAsync()
                .ConfigureAwait(false);
        }

        /// <summary>Gets compare cart session by identifier.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The compare cart session by identifier.</returns>
        private static async Task<Guid?> GetCompareCartSessionByIDAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Carts
                .AsNoTracking()
                .FilterByID(id)
                .Select(x => x.SessionID)
                .SingleAsync()
                .ConfigureAwait(false);
        }

        /// <summary>Compare get entity.</summary>
        /// <param name="lookupKey">The lookup key.</param>
        /// <param name="context">  The context.</param>
        /// <returns>An ICart.</returns>
        private static async Task<ICart> CompareGetEntityAsync(
            CompareCartBySessionLookupKey lookupKey,
            IClarityEcommerceEntities context)
        {
            return await context.Carts
                .FilterByActive(true)
                .FilterCartsByLookupKey(lookupKey)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <summary>Remove item from compare cart.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        private async Task<CEFActionResponse> CompareRemoveInnerAsync(
            CompareCartBySessionLookupKey lookupKey,
            int? productID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.CartItems
                .AsNoTracking()
                .FilterByActive(true)
                .FilterCartItemsByCartBrandID(lookupKey.BrandID)
                .FilterCartItemsByCartFranchiseID(lookupKey.FranchiseID)
                .FilterCartItemsByCartStoreID(lookupKey.StoreID)
                .FilterCartItemsByCartTypeName(lookupKey.TypeKey)
                .FilterCartItemsByProductID(productID);
            if (Contract.CheckValidID(lookupKey.UserID))
            {
                var result2 = await CEFAR.AggregateAsync(
                    await query
                        .FilterCartItemsByCartUserID(lookupKey.UserID)
                        .FilterCartItemsByCartAccountID(lookupKey.AccountID)
                        .Select(x => x.ID)
                        .ToListAsync()
                        .ConfigureAwait(false),
                    x => Workflows.CartItems.DeactivateAsync(x, contextProfileName))
                    .ConfigureAwait(false);
                if (!result2.ActionSucceeded)
                {
                    return result2;
                }
            }
            var result1 = await CEFAR.AggregateAsync(
                await query
                    .FilterCartItemsByCartSessionID(lookupKey.SessionID)
                    .Select(x => x.ID)
                    .ToListAsync()
                    .ConfigureAwait(false),
                x => Workflows.CartItems.DeactivateAsync(x, contextProfileName)).ConfigureAwait(false);
            await RemoveCartsThatAreEmptyAsync(contextProfileName).ConfigureAwait(false);
            return result1;
        }

        /// <summary>Queries if a given compare check exists.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        private async Task<int?> CompareCheckExistsAsync(
            CompareCartBySessionLookupKey lookupKey,
            string? contextProfileName)
        {
            var result = await ResolveCompareCartsToLatestActiveWithItemsAsync(lookupKey, contextProfileName).ConfigureAwait(false);
            return result.ActionSucceeded ? result.Result : null;
        }

        /// <summary>Generates a compare cart.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="doAdd">             True to do add.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The compare cart.</returns>
        private async Task<CEFActionResponse<ICart>> GenerateCompareCartAsync(
            CompareCartBySessionLookupKey lookupKey,
            bool doAdd,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            var cart = await GenerateBlankCartAsync(
                    model: null,
                    typeName: lookupKey.TypeKey,
                    contextProfileName: contextProfileName,
                    sessionID: lookupKey.SessionID,
                    userID: lookupKey.UserID,
                    accountID: lookupKey.AccountID,
                    storeID: lookupKey.StoreID,
                    franchiseID: lookupKey.FranchiseID,
                    brandID: lookupKey.BrandID)
                .ConfigureAwait(false);
            return await AddCartAndSaveSafelyAsync(context, cart, doAdd).ConfigureAwait(false);
        }

        /// <summary>Resolve compare carts to latest active with items.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{int}.</returns>
        private async Task<CEFActionResponse<int>> ResolveCompareCartsToLatestActiveWithItemsAsync(
            CompareCartBySessionLookupKey lookupKey,
            string? contextProfileName)
        {
            if (lookupKey.SessionID == default && !Contract.CheckValidID(lookupKey.UserID))
            {
                return CEFAR.FailingCEFAR<int>(
                    "ERROR! Must supply at least a Session ID or User ID to locate data against");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var rootQuery = context.Carts.AsNoTracking().FilterByActive(true);
            var userCartIDs = Contract.CheckValidID(lookupKey.UserID)
                ? await rootQuery
                    .FilterCartsByLookupKey(lookupKey.ButIgnoreSessionID())
                    .Select(x => x.ID)
                    .ToListAsync()
                    .ConfigureAwait(false)
                : new();
            var sessionCartIDs = Contract.CheckValidID(lookupKey.SessionID)
                ? await rootQuery
                    .FilterCartsByLookupKey(lookupKey.ButIgnoreUserAndAccountID())
                    .Select(x => x.ID)
                    .ToListAsync()
                    .ConfigureAwait(false)
                : new();
            if (userCartIDs.Count == 0 && sessionCartIDs.Count == 0)
            {
                // There were no compare carts for this user by session Guid or user ID so generate a new one
                var cartResponse = await GenerateCompareCartAsync(
                        lookupKey: lookupKey,
                        doAdd: true,
                        context: context,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (!cartResponse.ActionSucceeded)
                {
                    return cartResponse.ChangeFailingCEFARType<int>();
                }
                return cartResponse.Result!.ID.WrapInPassingCEFAR(
                    "NOTE! No previous Compare Carts, generated a new one.");
            }
            if (userCartIDs.Count == 0)
            {
                return await ProcessCartIDsForLatestCompareCartAsync(
                        cartIDs: sessionCartIDs,
                        kind: "Session",
                        lookupKey: lookupKey,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            // ReSharper disable once InvertIf
            if (sessionCartIDs.Count == 0)
            {
                return await ProcessCartIDsForLatestCompareCartAsync(
                        cartIDs: userCartIDs,
                        kind: "User",
                        lookupKey: lookupKey,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            // We have both, combine the lists and do the full array
            var combinedIDs = userCartIDs.Union(sessionCartIDs).ToList();
            return await ProcessCartIDsForLatestCompareCartAsync(
                    cartIDs: combinedIDs,
                    kind: "User, Account and Session",
                    lookupKey: lookupKey,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Process the cart IDs for latest compare cart.</summary>
        /// <param name="cartIDs">           The cart IDs.</param>
        /// <param name="kind">              The kind.</param>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{int}.</returns>
        private async Task<CEFActionResponse<int>> ProcessCartIDsForLatestCompareCartAsync(
            IReadOnlyCollection<int> cartIDs,
            string kind,
            CompareCartBySessionLookupKey lookupKey,
            string? contextProfileName)
        {
            // See if there is only one
            if (cartIDs.Count == 1)
            {
                return cartIDs.First().WrapInPassingCEFAR(
                    $"NOTE! Only one active {kind}-based Compare Cart, returning it.");
            }
            // There is more than one compare cart, review the items to find the latest compare cart with valid values
            var lastSetDateTimes = new Dictionary<int, DateTime>();
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var cartID in cartIDs)
            {
                lastSetDateTimes[cartID] = await context.CartItems
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
                var cartResponse = await GenerateCompareCartAsync(
                        lookupKey: lookupKey,
                        true,
                        context: context,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (!cartResponse.ActionSucceeded)
                {
                    return cartResponse.ChangeFailingCEFARType<int>();
                }
                return cartResponse.Result!.ID.WrapInPassingCEFAR(
                    $"NOTE! No valid previous {kind}-based Compare Carts, generated a new one.");
            }
            // In order of oldest to newest, deactivate the older ones and then return the latest
            var idsInOrder = lastSetDateTimes.OrderBy(x => x.Value).Select(x => x.Key).ToArray();
            for (var i = 0; i < lastSetDateTimes.Count; i++)
            {
                var id = idsInOrder[i];
                if (Contract.CheckValidID(lookupKey.UserID))
                {
                    await AssignUserIDToCartIfNullAsync(
                            await context.Carts.FilterByID(id).SingleAsync().ConfigureAwait(false),
                            lookupKey.UserID,
                            context)
                        .ConfigureAwait(false);
                }
                if (Contract.CheckValidID(lookupKey.AccountID))
                {
                    await AssignAccountIDToCartIfNullAsync(
                            await context.Carts.FilterByID(id).SingleAsync().ConfigureAwait(false),
                            lookupKey.AccountID,
                            context)
                        .ConfigureAwait(false);
                }
                if (id == idsInOrder.Last())
                {
                    return id.WrapInPassingCEFAR(
                        $"NOTE! Deactivated older {kind}-based Compare Carts and returning latest.");
                }
                await DeactivateAsync(
                        entity: await context.Carts.FilterByID(id).SingleAsync().ConfigureAwait(false),
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            throw new InvalidOperationException(
                $"ERROR! Could not figure out what to do in finding latest Compare Cart by {kind}-based Cart IDs.");
        }
    }
}
