// <copyright file="CartTypeCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart type workflow class</summary>
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
    using JSConfigs;
    using LinqKit;
    using Mapper;
    using Models;
    using MoreLinq;
    using Utilities;

    public partial class CartTypeWorkflow
    {
        /// <inheritdoc/>
        public Task<List<ICartTypeModel>> GetTypesForUserAsync(
            int userID,
            bool includeNotCreatedByUser,
            bool filterCartsByOrderRequest,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var (list1, _, _) = context.Carts
                .AsNoTracking()
                .AsExpandable()
                .FilterByActive(true)
                .FilterCartsByOrderRequest(filterCartsByOrderRequest)
                .FilterCartsByUserID(userID)
                .Select(x => x.Type!)
                .SelectListCartTypeAndMapToCartTypeModel(paging: null, sorts: null, groupings: null, contextProfileName);
            var (list2, _, _) = context.CartTypes
                .AsNoTracking()
                .AsExpandable()
                .FilterByActive(true)
                .FilterCartTypesByOrderRequests(filterCartsByOrderRequest)
                .FilterCartTypesByUserID(userID)
                .SelectListCartTypeAndMapToCartTypeModel(paging: null, sorts: null, groupings: null, contextProfileName);
            return Task.FromResult(
                list1
                    .Union(list2)
                    .DistinctBy(x => x!.ID)
                    .OrderBy(x => x!.SortOrder)
                    .ThenBy(x => x!.DisplayName ?? x.Name)
                    .ThenBy(x => x!.Name)
                    .Where(x => includeNotCreatedByUser || x!.CreatedByUserID == userID)
                    .ToList())!;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<ICartTypeModel>> GetForUserAsync(
            int userID,
            string typeName,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var existingID = await context.CartTypes
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByName(Contract.RequiresValidKey(typeName), true)
                .FilterCartTypesByUserID(Contract.RequiresValidID(userID))
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync();
            if (Contract.CheckInvalidID(existingID))
            {
                return CEFAR.FailingCEFAR<ICartTypeModel>("ERROR! Cart Type does not exist for user");
            }
            return (await GetAsync(existingID!.Value, context).ConfigureAwait(false))
                .WrapInPassingCEFARIfNotNull();
        }

        /// <inheritdoc/>
        public async Task<ICartTypeModel?> UpsertForUserAsync(
            int userID,
            ICartTypeModel model,
            string? contextProfileName,
            bool doSave = true)
        {
            model.CreatedByUserID = userID;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            if (!CEFConfigDictionary.AssignNameToAndShowDisplayName)
            {
                var existingID = await context.CartTypes
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByName(model.Name ?? model.DisplayName, true)
                    .FilterCartTypesByUserID(userID)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync();
                if (Contract.CheckValidID(existingID))
                {
                    return await GetAsync(existingID!.Value, context).ConfigureAwait(false);
                }
            }
            return await CreateForUserAsync(userID, model, context, doSave).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ICartTypeModel?> CreateForUserAsync(
            int userID,
            ICartTypeModel model,
            IClarityEcommerceEntities context,
            bool doSave = true)
        {
            Contract.RequiresInvalidID(Contract.RequiresNotNull(model).ID);
            model.CreatedByUserID = userID;
            if (CEFConfigDictionary.GenerateAndAssignGuidForCartTypeKeyAndName)
            {
                var guid = Guid.NewGuid().ToString();
                model.Name = guid;
                model.CustomKey = guid;
            }
            var entity = await CreateEntityWithoutSavingAsync(model, null, context).ConfigureAwait(false);
            entity.Result!.DisplayName = model.DisplayName;
            if (Contract.CheckAnyInvalidKey(entity.Result.CustomKey))
            {
                entity.Result.CustomKey = model.DisplayName;
            }
            if (Contract.CheckAnyInvalidKey(entity.Result.Name))
            {
                entity.Result.Name = model.DisplayName;
            }
            if (Contract.CheckAnyInvalidKey(entity.Result.DisplayName))
            {
                if (Contract.CheckAnyInvalidKey(model.DisplayName))
                {
                    entity.Result.DisplayName = model.Name;
                }
                else
                {
                    entity.Result.DisplayName = model.DisplayName;
                }
            }

            if (!doSave)
            {
                return MapFromConcreteFull(entity.Result, context.ContextProfileName);
            }
            context.Set<CartType>().Add(entity.Result!);
            var saveWorked = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            if (!saveWorked)
            {
                throw new System.IO.InvalidDataException(
                    $"Something about creating '{model.GetType().FullName}' and saving it failed");
            }
            // Pull the entity fresh from the database and return it
            return await GetAsync(entity.Result!.ID, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> DeleteForUserAsync(
            int userID,
            string cartTypeName,
            string? contextProfileName,
            bool doSave = true)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var existing = await context.CartTypes
                .FilterCartTypesByUserID(Contract.RequiresValidID(userID))
                .FilterByName(Contract.RequiresValidKey(cartTypeName), true)
                .FirstOrDefaultAsync();
            if (existing == null)
            {
                return CEFAR.PassingCEFAR();
            }
            return await DeleteAsync(existing, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ShareShoppingCartsWithSelectedUsersAsync(
            int brandID,
            int[]? userIDs,
            int? cartTypeID,
            int? currentAccountID,
            int? currentUserID,
            Dictionary<int, int>? productQuantities,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var users = userIDs.ToList();
            var currentCartType = await context.CartTypes
                .AsNoTracking()
                .FilterByID(cartTypeID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (!Contract.CheckNotNull(currentCartType))
            {
                return CEFAR.FailingCEFAR($"No cart type found for ID {cartTypeID}.");
            }
            if (users!.Count() == 1 && users!.First() == -1)
            {
                users = await context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterUsersByAccountID(currentAccountID)
                    .Where(accountUser => accountUser.ID != currentUserID)
                    .Select(user => user.ID)
                    .ToListAsync()
                    .ConfigureAwait(false);
                var associatedUsers = (await context.Users
                    .Where(x => x.JsonAttributes!.Contains("\"Key\":\"associatedAccounts\""))
                    .Select(y => new
                    {
                        y.ID,
                        y.JsonAttributes,
                    })
                    .ToListAsync()
                .ConfigureAwait(false))
                .Select(x => new UserModel
                {
                    ID = x.ID,
                    SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                })
                .Where(x => x!.SerializableAttributes
                    .SingleOrDefault(y => y.Key == "associatedAccounts")
                    .Value
                    .ToString()
                    .Contains(currentAccountID.ToString()))
                .Select(x => x.ID)
                .ToList();
                if (Contract.CheckNotEmpty(associatedUsers))
                {
                    users.AddRange(associatedUsers);
                    users = users.AsEnumerable().Distinct().ToList();
                }
            }
            // Pull shopping list.
            var shoppingListToCopy = (await context.Carts
                    .FilterByTypeID(cartTypeID)
                    .Select(x => new
                    {
                        x.ID,
                        x.StateID,
                        x.StatusID,
                        x.BrandID,
                        x.JsonAttributes,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new Cart
                {
                    ID = x.ID,
                    StateID = x.StateID,
                    StatusID = x.StatusID,
                    JsonAttributes = x.JsonAttributes,
                })
                .FirstOrDefault();
            if (Contract.CheckNull(shoppingListToCopy))
            {
                return CEFAR.FailingCEFAR($"Could not find cart with Type ID: {cartTypeID}");
            }
            var salesItems = await context.CartItems
                .FilterCartItemsByCartID(shoppingListToCopy.ID)
                .ToListAsync()
                .ConfigureAwait(false);
            // Copy shopping list.
            var timestamp = DateExtensions.GenDateTime;
            var allCarts = await context.Carts
                .Include(x => x.SalesItems)
                .Where(x => x.Type!.DisplayName == currentCartType.Name)
                .Where(y => users.Contains(y.UserID!.Value))
                .ToListAsync()
                .ConfigureAwait(false);
            foreach (var user in users)
            {
                var userShoppingList = allCarts.FirstOrDefault(x => x.UserID == user);
                var userShoppingListItems = userShoppingList?.SalesItems;
                if (Contract.CheckNull(userShoppingList, userShoppingListItems))
                {
                    var cartTypeGuid = Guid.NewGuid().ToString();
                    var cartCopy = new Cart
                    {
                        Active = true,
                        CustomKey = $"{shoppingListToCopy.ID}|{user}",
                        AccountID = currentAccountID,
                        UserID = user,
                        CreatedDate = timestamp,
                        StateID = shoppingListToCopy.StateID,
                        StatusID = shoppingListToCopy.StatusID,
                        BrandID = brandID,
                        Type = new CartType
                        {
                            Active = true,
                            CreatedDate = timestamp,
                            CreatedByUserID = user,
                            CustomKey = cartTypeGuid,
                            Name = cartTypeGuid,
                            DisplayName = currentCartType.Name,
                            BrandID = brandID,
                            JsonAttributes = currentCartType.JsonAttributes,
                        },
                        SalesItems = salesItems
                            .Select(saleItem => new CartItem
                            {
                                Active = true,
                                CreatedDate = timestamp,
                                CustomKey = $"{saleItem.ProductID}|{user}",
                                ProductID = saleItem.ProductID,
                                Sku = saleItem.Sku,
                                Name = saleItem.Name,
                                Description = saleItem.Description,
                                UnitCorePrice = saleItem.UnitCorePrice,
                                UnitSoldPrice = saleItem.UnitSoldPrice,
                                UnitOfMeasure = saleItem.UnitOfMeasure,
                                Quantity = productQuantities!.ContainsKey((int)saleItem.ProductID!)
                                    ? Convert.ToDecimal(productQuantities[(int)saleItem.ProductID])
                                    : saleItem.Quantity,
                                JsonAttributes = saleItem.JsonAttributes,
                            })
                            .ToList(),
                    };
                    context.Carts.Add(cartCopy);
                }
                else
                {
                    if (userShoppingList!.Type!.SerializableAttributes.TryGetValue("listOwnerUserID", out var owner)
                        && (Contract.CheckAnyInvalidKey(owner.Value) || owner.Value != currentUserID.ToString()))
                    {
                        return CEFAR.FailingCEFAR("Only the list owner can modify the shopping list.");
                    }
                    var cartToCopy = userShoppingList;
                    cartToCopy.UpdatedDate = timestamp;
                    foreach (var salesItem in userShoppingListItems!)
                    {
                        var updatedItem = salesItems.SingleOrDefault(x => x.ProductID == salesItem.ProductID);
                        if (Contract.CheckNull(updatedItem))
                        {
                            context.CartItems.Remove(salesItem);
                            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                        }
                        else
                        {
                            salesItem.Quantity = productQuantities!.ContainsKey((int)updatedItem.ProductID!)
                                    ? Convert.ToDecimal(productQuantities[(int)updatedItem.ProductID])
                                    : updatedItem.Quantity;
                            salesItem.UpdatedDate = timestamp;
                            salesItem.UnitCorePrice = updatedItem?.UnitCorePrice ?? 0m;
                            salesItem.UnitSoldPrice = updatedItem?.UnitSoldPrice ?? 0m;
                            salesItem.UnitOfMeasure = updatedItem?.UnitOfMeasure ?? string.Empty;
                            context.CartItems.Add(salesItem);
                            context.Entry(salesItem).State = EntityState.Modified;
                        }
                    }
                    await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                    var newItems = salesItems.Where(x => !userShoppingListItems.Any(y => y.ProductID == x.ProductID)).ToList();
                    var cartItem = new CartItem();
                    foreach (var newItem in newItems)
                    {
                        cartItem = new CartItem
                        {
                            Active = true,
                            CreatedDate = timestamp,
                            CustomKey = $"{newItem.ProductID}|{user}",
                            MasterID = userShoppingList.ID,
                            ProductID = newItem.ProductID,
                            Sku = newItem.Sku,
                            Name = newItem.Name,
                            Description = newItem.Description,
                            UnitCorePrice = newItem.UnitCorePrice,
                            UnitSoldPrice = newItem.UnitSoldPrice,
                            UnitOfMeasure = newItem.UnitOfMeasure,
                            Quantity = newItem.Quantity,
                        };
                        context.CartItems.Add(cartItem);
                    }
                    await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                }
            }
            foreach (var salesItem in salesItems)
            {
                var updatedItem = salesItems.SingleOrDefault(x => x.ProductID == salesItem.ProductID);
                if (Contract.CheckNull(updatedItem))
                {
                    context.CartItems.Remove(salesItem);
                    await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                }
                else
                {
                    salesItem.Quantity = productQuantities!.ContainsKey((int)updatedItem.ProductID!)
                            ? Convert.ToDecimal(productQuantities[(int)updatedItem.ProductID])
                            : updatedItem.Quantity;
                    salesItem.UpdatedDate = timestamp;
                    salesItem.UnitCorePrice = updatedItem?.UnitCorePrice ?? 0m;
                    salesItem.UnitSoldPrice = updatedItem?.UnitSoldPrice ?? 0m;
                    salesItem.UnitOfMeasure = updatedItem?.UnitOfMeasure ?? string.Empty;
                    context.CartItems.Add(salesItem);
                    context.Entry(salesItem).State = EntityState.Modified;
                }
            }
            try
            {
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                return CEFAR.PassingCEFAR();
            }
            catch (Exception)
            {
                return CEFAR.FailingCEFAR("Failed to share the shopping list with users on the account.");
            }
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeactivateAsync(
            CartType entity,
            string? contextProfileName)
        {
            if (entity == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot Deactivate a null record");
            }
            if (!entity.Active)
            {
                return CEFAR.PassingCEFAR();
            } // Already inactive
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var e = await context.CartTypes.FilterByID(entity.ID).SingleAsync();
            e.UpdatedDate = timestamp;
            e.Active = false;
            if (context.Carts == null)
            {
                return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                    .BoolToCEFAR("ERROR! Unable to save Deactivating the carts of this type before deactivating the type");
            }
            var carts = context.Carts
                .FilterByActive(true)
                .FilterCartsByUserID(e.CreatedByUserID)
                .FilterByTypeID(e.ID);
            foreach (var cart in carts)
            {
                cart.Active = false;
                cart.UpdatedDate = timestamp;
            }
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Unable to save Deactivating the record");
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            CartType? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            if (context.Carts != null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Carts.Count(x => x.TypeID == entity.ID);)
                {
                    await Workflows.Carts.DeleteAsync(
                            context.Carts.First(x => x.TypeID == entity.ID).ID,
                            context)
                        .ConfigureAwait(false);
                }
            }
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }
    }
}
