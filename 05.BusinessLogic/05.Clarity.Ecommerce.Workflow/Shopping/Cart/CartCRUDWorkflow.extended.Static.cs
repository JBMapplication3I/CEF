// <copyright file="CartCRUDWorkflow.extended.Static.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the static cart workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
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
        public virtual async Task<ICartModel?> StaticGetAsync(
            StaticCartLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = StaticGetEntity(lookupKey, context);
            if (entity is null)
            {
                entity = await context.Carts
                    .AsNoTracking()
                    .FilterCartsByUserID(lookupKey.UserID)
                    .FilterByTypeKey<Cart, CartType>(lookupKey.TypeKey)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            var model = entity.StaticMap(contextProfileName);
            await AppendPriceDataToSalesItemsAsync(model, true, pricingFactoryContext, contextProfileName).ConfigureAwait(false);
            return model;
        }

        /// <inheritdoc/>
        public virtual async Task<ICartModel?> StaticGetAsync(
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = StaticGetEntity(lookupKey, context);
            var model = entity.StaticMap(contextProfileName);
            await AppendPriceDataToSalesItemsAsync(model, true, pricingFactoryContext, contextProfileName).ConfigureAwait(false);
            return model;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> StaticAddLotAsync(
            StaticCartLookupKey lookupKey,
            int lotID,
            decimal? quantity,
            string? contextProfileName)
        {
            Contract.RequiresAllValid(lotID, lookupKey, lookupKey.UserID, lookupKey.TypeKey);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var lot = await context.Lots
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(lotID)
                .Select(x => new { x.ID, x.ProductID, })
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            Contract.RequiresValidID(lot?.ID, $"ERROR! Invalid Lot ID '{lotID}'");
            if (Contract.CheckValidID(await context.CartItems
                .FilterByActive(true)
                .FilterCartItemsByProductID(lot!.ProductID)
                .FilterCartItemsByCartUserID(lookupKey.UserID)
                .FilterCartItemsByCartTypeName(lookupKey.TypeKey, true)
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false)))
            {
                return CEFAR.PassingCEFAR();
            }
            var timestamp = DateExtensions.GenDateTime;
            var entityResponse = await Workflows.CartItems.CreateEntityWithoutSavingAsync(
                    model: new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                    {
                        Active = true,
                        CreatedDate = timestamp,
                        ProductID = lot.ProductID,
                        Quantity = lookupKey.TypeKey == "Notify Me When In Stock" ? quantity ?? 1m : 1m,
                    },
                    timestamp: timestamp,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var cartID = StaticCheckExists(lookupKey, contextProfileName);
            if (Contract.CheckValidID(cartID))
            {
                entityResponse.Result!.MasterID = cartID!.Value;
                context.CartItems.Add(entityResponse.Result!);
            }
            else
            {
                var cart = await GenerateStaticCartAsync(
                        lookupKey: lookupKey,
                        doAdd: false,
                        context: context,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                cart.SalesItems!.Add(entityResponse.Result!);
                context.Carts.Add((Cart)cart);
            }
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Something about saving to the database failed");
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> StaticAddItemAsync(
            StaticCartLookupKey lookupKey,
            int productID,
            decimal? quantity,
            SerializableAttributesDictionary attributes,
            string? contextProfileName)
        {
            Contract.RequiresAllValid(productID, lookupKey.UserID, lookupKey.AccountID, lookupKey.TypeKey);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            Contract.RequiresValidID(
                await context.Products
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(productID)
                    .Select(x => x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false),
                $"ERROR! Invalid Product ID '{productID}'");
            if (Contract.CheckValidID(await context.CartItems
                .FilterByActive(true)
                .FilterCartItemsByProductID(productID)
                .FilterCartItemsByCartLookupKey(lookupKey)
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false)))
            {
                return CEFAR.PassingCEFAR();
            }
            var timestamp = DateExtensions.GenDateTime;
            var createResponse = await Workflows.CartItems.CreateEntityWithoutSavingAsync(
                    new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                    {
                        Active = true,
                        CreatedDate = timestamp,
                        ProductID = productID,
                        Quantity = lookupKey.TypeKey == "Notify Me When In Stock" ? quantity ?? 1m : 1m,
                        SerializableAttributes = attributes,
                    },
                    timestamp,
                    contextProfileName)
                .ConfigureAwait(false);
            var cartID = StaticCheckExists(lookupKey, contextProfileName);
            if (Contract.CheckInvalidID(cartID))
            {
                var cart = await GenerateStaticCartAsync(lookupKey, false, context, contextProfileName).ConfigureAwait(false);
                cart.SalesItems!.Add(createResponse.Result!);
                context.Carts.Add((Cart)cart);
            }
            else
            {
                createResponse.Result!.MasterID = cartID!.Value;
                context.CartItems.Add(createResponse.Result);
            }
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Something about saving to the database failed");
        }

        /// <inheritdoc/>
        public virtual Task<CEFActionResponse> StaticRemoveAsync(
            CartByIDLookupKey lookupKey,
            string? contextProfileName)
        {
            return Workflows.CartItems.DeactivateAsync(lookupKey.CartID, contextProfileName);
        }

        /// <inheritdoc/>
        public virtual Task<CEFActionResponse> StaticRemoveAsync(
            StaticCartLookupKey lookupKey,
            int productID,
            string? forceUniqueLineItemKey,
            string? contextProfileName)
        {
            Contract.RequiresAllValid(lookupKey.TypeKey, lookupKey.UserID, productID);
            return StaticRemoveInnerAsync(
                lookupKey,
                Contract.RequiresValidID(productID),
                forceUniqueLineItemKey,
                contextProfileName);
        }

        /// <inheritdoc/>
        public virtual Task<CEFActionResponse> StaticClearAsync(
            StaticCartLookupKey lookupKey,
            string? contextProfileName)
        {
            Contract.RequiresAllValid(lookupKey.TypeKey, lookupKey.UserID);
            return StaticRemoveInnerAsync(
                lookupKey: lookupKey,
                productID: null,
                forceUniqueLineItemKey: null,
                contextProfileName: contextProfileName);
        }

        /// <summary>Verify account access.</summary>
        /// <param name="accountToken">The account token.</param>
        /// <param name="context">     The context.</param>
        /// <returns>A Task{bool}.</returns>
        protected virtual Task<bool> VerifyAccountAccessAsync(
            string? accountToken,
            IClarityEcommerceEntities context)
        {
            // TODO
            // var entity = await context.Accounts
            //     .AsNoTracking()
            //     .FilterByActive()
            //     .FilterAccountsByUserUserName(userName)
            //     .FilterAccountsByToken(accountToken)
            //     .SingleOrDefaultAsync();
            // return entity.Children.Count() > 0;
            return context.Accounts.AnyAsync(x => x.Active && x.Token == accountToken);
        }

        /// <summary>Removes an item from the Static cart (or all if no product ID is passed).</summary>
        /// <param name="lookupKey">             The lookup key.</param>
        /// <param name="productID">             Identifier for the product.</param>
        /// <param name="forceUniqueLineItemKey">The force unique line item key.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        protected virtual async Task<CEFActionResponse> StaticRemoveInnerAsync(
            StaticCartLookupKey lookupKey,
            int? productID,
            string? forceUniqueLineItemKey,
            string? contextProfileName)
        {
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var ids = await context.CartItems
                    .FilterByActive(true)
                    .FilterCartItemsByCartLookupKey(lookupKey)
                    .FilterCartItemsByProductID(productID)
                    .FilterCartItemsByForceUniqueLineItemKey(
                        forceUniqueLineItemKey,
                        !Contract.CheckValidKey(forceUniqueLineItemKey))
                    .Select(x => x.ID)
                    .ToListAsync()
                    .ConfigureAwait(false);
                foreach (var id in ids)
                {
                    await Workflows.CartItems.DeactivateAsync(id, contextProfileName).ConfigureAwait(false);
                }
            }
            await RemoveCartsThatAreEmptyAsync(contextProfileName).ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Gets a static Cart by its user id and type.</summary>
        /// <param name="lookupKey">The lookup key.</param>
        /// <param name="context">  The context.</param>
        /// <returns>An ICart.</returns>
        protected virtual ICart? StaticGetEntity(
            StaticCartLookupKey lookupKey,
            IClarityEcommerceEntities context)
        {
            return context.Carts
                .FilterByActive(true)
                .FilterCartsByLookupKey(lookupKey)
                .SingleOrDefault();
        }

        /// <summary>Static get entity.</summary>
        /// <param name="lookupKey">The lookup key.</param>
        /// <param name="context">  The context.</param>
        /// <returns>An ICart.</returns>
        protected virtual ICart? StaticGetEntity(
            CartByIDLookupKey lookupKey,
            IClarityEcommerceEntities context)
        {
            return context.Carts
                .FilterByActive(true)
                .FilterByID(Contract.RequiresValidID(lookupKey.CartID))
                .SingleOrDefault();
        }

        /// <summary>Queries if a given static check exists.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        protected virtual int? StaticCheckExists(
            StaticCartLookupKey lookupKey,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.Carts
                .FilterByActive(true)
                .FilterCartsByLookupKey(lookupKey)
                .Select(x => (int?)x.ID)
                .SingleOrDefault();
        }

        /// <summary>Generates a static cart of the specified type.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="doAdd">             True to do add.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The static cart.</returns>
        protected virtual async Task<ICart> GenerateStaticCartAsync(
            StaticCartLookupKey lookupKey,
            bool doAdd,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            Contract.RequiresValidID(lookupKey.UserID);
            var cart = await GenerateBlankCartAsync(
                    null,
                    Contract.RequiresValidKey(lookupKey.TypeKey),
                    contextProfileName)
                .ConfigureAwait(false);
            cart.SessionID = null;
            cart.UserID = lookupKey.UserID;
            cart.AccountID = lookupKey.AccountID;
            cart.BrandID = lookupKey.BrandID;
            await AddCartAndSaveSafelyAsync(context, cart, doAdd).ConfigureAwait(false);
            return cart;
        }
    }
}
