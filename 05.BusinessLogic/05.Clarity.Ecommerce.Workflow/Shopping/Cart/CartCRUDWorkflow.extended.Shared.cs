// <copyright file="CartCRUDWorkflow.extended.Shared.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart workflow class (functions shared across multiple kinds)</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    public partial class CartWorkflow
    {
        /// <inheritdoc/>
        public override Task<ICartModel?> GetAsync(int id, string? contextProfileName)
        {
            throw new InvalidOperationException("Getting a cart requires additional information.");
        }

        /// <inheritdoc/>
        public override Task<ICartModel?> GetAsync(string key, string? contextProfileName)
        {
            throw new InvalidOperationException("Getting a cart requires additional information.");
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> CreateAsync(
            ICartModel model,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(model);
            Contract.RequiresInvalidID(model.ID);
            if (!OverrideDuplicateCheck)
            {
                await DuplicateCheckAsync(model, contextProfileName).ConfigureAwait(false);
            }
            var timestamp = DateExtensions.GenDateTime;
            var entity = RegistryLoaderWrapper.GetInstance<Cart>(contextProfileName);
            entity.Active = true;
            entity.CreatedDate = timestamp;
            entity.SessionID = model.SessionID ?? Guid.NewGuid();
            entity.SubtotalFees = model.Totals?.Fees ?? 0;
            entity.UserID = model.UserID;
            entity.AccountID = model.AccountID;
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var tempSalesItems = entity.SalesItems;
                context.Carts.Add(entity);
                // NOTE: This section is for unit testing only so the table contents all get generated correctly in the
                // mock context, it is not called in normal site run-time as contextProfileName would be null
                if (Contract.CheckValidKey(contextProfileName))
                {
                    foreach (var item in tempSalesItems!)
                    {
                        item.MasterID = entity.ID;
                        var tempTargets = item.Targets;
                        context.CartItems.Add(item);
                        Contract.RequiresValidID(item.ID);
                        foreach (var target in tempTargets!)
                        {
                            target.MasterID = item.ID;
                            context.CartItemTargets.Add(target);
                            Contract.RequiresValidID(target.ID);
                        }
                    }
                    Contract.RequiresAllValidIDs(
                        await context.CartItems.Select(x => (int?)x.ID).ToArrayAsync().ConfigureAwait(false));
                    Contract.RequiresAllValidIDs(
                        await context.Carts
                            .Where(x => x.ID == entity.ID)
                            .SelectMany(x => x.SalesItems!
                                .Where(y => y.Active)
                                .Select(y => (int?)y.ID))
                            .ToArrayAsync()
                            .ConfigureAwait(false));
                }
                var saveWorked = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                if (!saveWorked)
                {
                    throw new InvalidDataException(
                        $"Something about creating '{model.GetType().FullName}' and saving it failed");
                }
            }
            return entity.ID.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> UpdateAsync(
            ICartModel model,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresValidID(model?.ID);
            var type = Contract.CheckValidKey(model!.Type?.Name)
                // ReSharper disable once PossibleNullReferenceException
                ? model.Type!.Name
                : Contract.CheckValidKey(model.TypeName)
                    ? model.TypeName
                    : "Cart";
            Cart? entity = null;
            if (Contract.CheckValidID(model.ID))
            {
                entity = await context.Carts
                    .FilterByActive(true)
                    .FilterByID(model.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            if (entity is null && Contract.CheckInvalidID(model.ID) && Contract.CheckValidKey(model.CustomKey))
            {
                entity = await context.Carts
                    .FilterByActive(true)
                    .FilterByCustomKey(model.CustomKey, true)
                    .FilterCartsByBrandID(model.BrandID)
                    .FilterCartsByFranchiseID(model.FranchiseID)
                    .FilterCartsByStoreID(model.StoreID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            if (entity is null && Contract.CheckInvalidID(model.ID))
            {
                entity = await context.Carts
                    .FilterByActive(true)
                    .FilterByTypeName<Cart, CartType>(type, true)
                    .FilterCartsBySessionID(model.SessionID)
                    .FilterCartsByBrandID(model.BrandID)
                    .FilterCartsByFranchiseID(model.FranchiseID)
                    .FilterCartsByStoreID(model.StoreID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            if (entity is null)
            {
                throw new ArgumentException("Must supply an ID or CustomKey, or Session ID with Type Key that matches an existing record");
            }
            if (entity.CustomKey != model.CustomKey)
            {
                // This will throw if it finds another entity with this model's key
                await DuplicateCheckAsync(model, context).ConfigureAwait(false);
            }
            var timestamp = DateExtensions.GenDateTime;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (model.Totals is not null)
            {
                entity.SubtotalFees = model.Totals!.Fees;
                entity.SubtotalTaxes = model.Totals.Tax;
            }
            entity.RequestedShipDate = model.DueDate;
            entity.UpdatedDate = timestamp;
            await CleanBillingShippingAndContactsAsync(model, context).ConfigureAwait(false);
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            // NOTE: This section is for unit testing only so the table contents all get generated correctly in the
            // mock context, it is not called in normal site run-time as contextProfileName would be null
            if (Contract.CheckValidKey(context.ContextProfileName))
            {
                await CorrectionsForTesting(context, entity).ConfigureAwait(false);
            }
            var saveWorked = await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            if (!saveWorked)
            {
                throw new InvalidDataException("Something about updating this object and saving it failed");
            }
            await RemoveCartsThatAreEmptyAsync(context).ConfigureAwait(false);
            return entity.ID.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<ICart>> AddCartAndSaveSafelyAsync(
            IClarityEcommerceEntities context,
            ICart cart,
            bool doAdd)
        {
            while (true)
            {
                if (doAdd)
                {
                    context.Carts.Add((Cart)cart);
                }
                try
                {
                    return await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false)
                        ? cart.WrapInPassingCEFAR()!
                        : CEFAR.FailingCEFAR<ICart>();
                }
                catch
                {
                    // Try to get the entity again, it probably tried to make a duplicate cart
                    // because of threading issues, so call the other one that was made
                    var cartTypeName = await context.CartTypes
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByID(cart.TypeID)
                        .FilterByName(cart.Type?.Name)
                        .Select(x => x.Name)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);
                    var altCart = await context.Carts
                        .FilterByActive(true)
                        .FilterByID(cart.ID)
                        .FilterCartsBySessionID(cart.SessionID)
                        .FilterCartsByUserID(cart.UserID)
                        .FilterCartsByBrandID(cart.BrandID)
                        .FilterCartsByFranchiseID(cart.FranchiseID)
                        .FilterCartsByStoreID(cart.StoreID)
                        .FilterByTypeName<Cart, CartType>(cart.Type?.Name ?? cartTypeName, true)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);
                    if (altCart is null)
                    {
                        throw;
                    }
                    return altCart.WrapInFailingCEFAR<ICart>(
                        "WARNING! We got the cart but it may not contain what was sent to be added.")!;
                }
            }
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<SerializableAttributesDictionary>> GetAttributesAsync(
            CartByIDLookupKey lookupKey,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await context.Carts
                    .AsNoTracking()
                    .FilterByID(Contract.RequiresValidID(lookupKey.CartID))
                    .Select(x => x.JsonAttributes)
                    .SingleAsync()
                    .ConfigureAwait(false))
                .DeserializeAttributesDictionary()
                .WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UpdateAttributesAsync(
            CartByIDLookupKey lookupKey,
            SerializableAttributesDictionary attrs,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts
                .FilterByID(Contract.RequiresValidID(lookupKey.CartID))
                .SingleAsync()
                .ConfigureAwait(false);
            cart.JsonAttributes = attrs.SerializeAttributesDictionary();
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<IContactModel>> GetShippingContactAsync(
            int id,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var contactID = await context.Carts
                .AsNoTracking()
                .FilterByID(id)
                .Select(x => x.ShippingContactID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(contactID))
            {
                return CEFAR.FailingCEFAR<IContactModel>("WARNING! No Cart Shipping Contact");
            }
            return context.Contacts
                .AsNoTracking()
                .FilterByID(contactID)
                .SelectSingleLiteContactAndMapToContactModel(context.ContextProfileName)
                .WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SetShippingContactAsync(
            int id,
            IContactModel? newContact,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            ICart cart = await context.Carts
                .FilterByID(id)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (cart == null)
            {
                return CEFAR.FailingCEFAR("WARNING! No Cart");
            }
            cart.UpdatedDate = DateExtensions.GenDateTime;
            if (newContact == null)
            {
                cart.ShippingContactID = null;
                cart.ShippingContact = null;
                return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
            }
            if (Contract.CheckValidID(newContact.ID)
                && !await context.AccountContacts
                        .FilterByActive(true)
                        .FilterAccountContactsByContactID(newContact.ID)
                        .AnyAsync()
                        .ConfigureAwait(false))
            {
                // Most likely guest checkout and we don't want to change the ID in targets checkout on repeat
                // analyze calls, so just update it and continue using it
                var shippingContact = await Workflows.Contacts.UpdateAsync(newContact, contextProfileName).ConfigureAwait(false);
                cart.ShippingContactID = shippingContact.Result;
                return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
            }
            else
            {
                // It's either totally new or is in the address book so we want to copy it to not mess up the
                // address book entry
                newContact.ID = newContact.Address!.ID = 0;
                var shippingContact = await Workflows.Contacts.CreateEntityWithoutSavingAsync(
                        model: newContact,
                        timestamp: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                shippingContact.Result!.TypeID = await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Shipping",
                        byName: "Shipping",
                        byDisplayName: "Shipping",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                cart.ShippingContact = shippingContact.Result;
                return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false)).BoolToCEFAR();
            }
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SetBillingContactAsync(
            int id,
            IContactModel? newContact,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            ICart cart = await context.Carts
                .Include(x => x.BillingContact)
                .FilterByID(id)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (cart == null)
            {
                return CEFAR.FailingCEFAR("WARNING! No Cart");
            }
            cart.UpdatedDate = DateExtensions.GenDateTime;
            if (newContact == null)
            {
                cart.BillingContactID = null;
                cart.BillingContact = null;
                return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
            }
            if (Contract.CheckValidID(newContact.ID)
                && !await context.AccountContacts
                        .FilterByActive(true)
                        .FilterAccountContactsByContactID(newContact.ID)
                        .AnyAsync()
                        .ConfigureAwait(false))
            {
                // Most likely guest checkout and we don't want to change the ID in targets checkout on repeat
                // analyze calls, so just update it and continue using it
                var billingContact = await Workflows.Contacts.UpdateAsync(newContact, contextProfileName).ConfigureAwait(false);
                cart.BillingContactID = billingContact.Result;
                return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
            }
            else
            {
                // It's either totally new or is in the address book so we want to copy it to not mess up the
                // address book entry
                newContact.ID = newContact.Address!.ID = 0;
                var billingContact = await Workflows.Contacts.CreateEntityWithoutSavingAsync(
                        model: newContact,
                        timestamp: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                billingContact.Result!.TypeID = await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Billing",
                        byName: "Billing",
                        byDisplayName: "Billing",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                cart.BillingContact = billingContact.Result;
                return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false)).BoolToCEFAR();
            }
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> IsShippingRequiredAsync(
            CartByIDLookupKey lookupKey,
            List<IShippingProviderBase> shippingProviders,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cartType = await context.Carts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(lookupKey.CartID)
                .OrderByDescending(x => x.SalesItems!.Count(y => y.Active))
                .Select(x => x.Type!.Name)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (!Contract.CheckValidKey(cartType))
            {
                return CEFAR.FailingCEFAR();
            }
            var response = new CEFActionResponse<(int cartID, Guid? sessionID)>();
            var r = await CustomSessionMapGetAsync(
                    response: response,
                    id: lookupKey.CartID,
                    cartType: cartType!,
                    userID: null,
                    currentAccountID: lookupKey.AltAccountID,
                    pricingFactoryContext: null,
                    taxesProvider: null,
                    contextProfileName: contextProfileName,
                    skipItems: true,
                    skipDiscounts: true,
                    skipTotals: true)
                .ConfigureAwait(false);
            return await IsShippingRequiredAsync(r.Result!, shippingProviders, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> IsShippingRequiredAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            List<IShippingProviderBase> shippingProviders,
            string? contextProfileName)
        {
            Contract.RequiresAllValid(lookupKey.SessionID, lookupKey.TypeKey);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cartID = await context.Carts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterCartsByLookupKey(lookupKey)
                .Select(x => x.ID)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            var response = new CEFActionResponse<(int cartID, Guid? sessionID)>();
            var r = await CustomSessionMapGetAsync(
                    response: response,
                    id: cartID,
                    cartType: lookupKey.TypeKey,
                    userID: null,
                    currentAccountID: lookupKey.AltAccountID,
                    pricingFactoryContext: null,
                    taxesProvider: null,
                    contextProfileName: contextProfileName,
                    skipItems: true,
                    skipDiscounts: true,
                    skipTotals: true)
                .ConfigureAwait(false);
            return await IsShippingRequiredAsync(r.Result!, shippingProviders, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> RemoveCartsThatAreEmptyAsync(string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await RemoveCartsThatAreEmptyAsync(context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> RemoveCartsThatAreEmptyAsync(IClarityEcommerceEntities context)
        {
            if (Contract.CheckValidKey(context.ContextProfileName))
            {
                // Don't run this during tests with a mock database (contextProfileName will have a value)
                return CEFAR.PassingCEFAR();
            }
            // Remove Carts that are !Active
            await CEFAR.AggregateAsync(
                    await context.Carts
                        .AsNoTracking()
                        .FilterByActive(false)
                        .Select(x => x.ID)
                        .ToListAsync()
                        .ConfigureAwait(false),
                    x => DeleteAsync(x, context.ContextProfileName))
                .ConfigureAwait(false);
            // Remove Carts that are Active, but have no Active CartItems
            await CEFAR.AggregateAsync(
                    await context.Carts
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterCartsByHaveActiveItems(false)
                        .Select(x => x.ID)
                        .ToListAsync()
                        .ConfigureAwait(false),
                    x => DeleteAsync(x, context.ContextProfileName))
                .ConfigureAwait(false);
            // Remove CartItems that are !Active
            await CEFAR.AggregateAsync(
                    await context.CartItems
                        .AsNoTracking()
                        .FilterByActive(false)
                        .Select(x => x.ID)
                        .ToListAsync()
                        .ConfigureAwait(false),
                    x => Workflows.CartItems.DeleteAsync(x, context.ContextProfileName))
                .ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SetSameAsBillingAsync(int id, bool isSame, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts
                .FilterByID(Contract.RequiresValidID(id))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (cart == null)
            {
                return CEFAR.FailingCEFAR("WARNING! No Cart");
            }
            if (isSame && (cart.ShippingSameAsBilling ?? false))
            {
                // Already matches
                return CEFAR.PassingCEFAR();
            }
            if (!isSame && !(cart.ShippingSameAsBilling ?? false))
            {
                // Already matches
                return CEFAR.PassingCEFAR();
            }
            cart.UpdatedDate = DateExtensions.GenDateTime;
            cart.ShippingSameAsBilling = isSame;
            // ReSharper disable once InvertIf
            if (isSame && Contract.CheckValidID(cart.ShippingContactID))
            {
                cart.ShippingContactID = null;
                cart.ShippingContact = null;
            }
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ClearShippingContactAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts
                .FilterByID(Contract.RequiresValidID(id))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (cart == null)
            {
                return CEFAR.FailingCEFAR("WARNING! No Cart");
            }
            if (cart.ShippingContact == null)
            {
                return CEFAR.PassingCEFAR();
            }
            cart.ShippingContactID = null;
            cart.ShippingContact = null;
            cart.UpdatedDate = DateExtensions.GenDateTime;
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ClearBillingContactAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts
                .FilterByID(Contract.RequiresValidID(id))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (cart == null)
            {
                return CEFAR.FailingCEFAR("WARNING! No Cart");
            }
            if (cart.BillingContact == null)
            {
                return CEFAR.PassingCEFAR();
            }
            cart.BillingContactID = null;
            cart.BillingContact = null;
            cart.UpdatedDate = DateExtensions.GenDateTime;
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SetStoreAsync(
            CartByIDLookupKey lookupKey,
            int? storeID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts
                .FilterByID(Contract.RequiresValidID(lookupKey.CartID))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (cart == null)
            {
                return CEFAR.FailingCEFAR("Cart not found");
            }
            cart.StoreID = Contract.CheckValidID(storeID)
                && Contract.CheckValidID(await Workflows.Stores.CheckExistsAsync(storeID!.Value, contextProfileName).ConfigureAwait(false))
                ? storeID
                : null;
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SetCartTotalsAsync(
            CartByIDLookupKey lookupKey,
            ICartTotals? totals,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts
                .FilterByID(Contract.RequiresValidID(lookupKey.CartID))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (cart == null)
            {
                return CEFAR.FailingCEFAR("Cart not found");
            }
            if (totals is null)
            {
                cart.SubtotalFees = 0m;
                cart.SubtotalHandling = 0m;
                cart.SubtotalItems = 0m;
                cart.SubtotalShipping = 0m;
                cart.SubtotalTaxes = 0m;
                cart.SubtotalDiscounts = 0m;
                cart.Total = 0m;
            }
            else
            {
                cart.SubtotalFees = totals.Fees;
                cart.SubtotalHandling = totals.Handling;
                cart.SubtotalItems = totals.SubTotal;
                cart.SubtotalShipping = totals.Shipping;
                cart.SubtotalTaxes = totals.Tax;
                cart.SubtotalDiscounts = totals.Discounts;
                cart.Total = totals.Total;
            }
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SetRequestedShipDateAsync(
            CartByIDLookupKey lookupKey,
            DateTime? date,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts
                .FilterByID(Contract.RequiresValidID(lookupKey.CartID))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (cart == null)
            {
                return CEFAR.FailingCEFAR("Cart not found");
            }
            cart.RequestedShipDate = date;
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SetContactsAsync(
            CartByIDLookupKey lookupKey,
            List<ICartContactModel>? contacts,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts
                .FilterByID(Contract.RequiresValidID(lookupKey.CartID))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (cart == null)
            {
                return CEFAR.FailingCEFAR("Cart not found");
            }
            var model = RegistryLoaderWrapper.GetInstance<ICartModel>(contextProfileName);
            model.Contacts = contacts;
            await Workflows.CartWithContactsAssociation.AssociateObjectsAsync(cart, model, DateExtensions.GenDateTime, contextProfileName).ConfigureAwait(false);
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SetUserAndAccountAsync(
            CartByIDLookupKey lookupKey,
            int? userID,
            int? accountID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts
                .FilterByID(Contract.RequiresValidID(lookupKey.CartID))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (cart == null)
            {
                return CEFAR.FailingCEFAR("Cart not found");
            }
            var model = RegistryLoaderWrapper.GetInstance<ICartModel>(contextProfileName);
            model.UserID = userID;
            model.AccountID = accountID;
            var timestamp = DateExtensions.GenDateTime;
            await RelateOptionalUserAsync(cart, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateOptionalAccountAsync(cart, model, timestamp, contextProfileName).ConfigureAwait(false);
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ExpireCartsAsync(int expiredThreshold, string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                if (expiredThreshold < 1)
                {
                    return CEFAR.PassingCEFAR();
                }
                var expirationDate = DateTime.UtcNow.AddDays(-expiredThreshold);
                var expiredCartIDs = await context.Carts
                    .FilterByActive(true)
                    .FilterByExcludedTypeKeys<Cart, CartType>(new[]
                    {
                        "Favorites List",
                        "Wish List",
                        "Bookmark",
                        "Watch List",
                        "Notify Me When In Stock",
                    })
                    .Where(x => (x.UpdatedDate ?? x.CreatedDate) < expirationDate)
                    .Select(x => x.ID)
                    .ToListAsync()
                    .ConfigureAwait(false);
                if (expiredCartIDs.Count == 0)
                {
                    return CEFAR.PassingCEFAR();
                }
                return await CEFAR.AggregateAsync(
                        expiredCartIDs,
                        x => DeleteAsync(x, contextProfileName))
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        $"{nameof(CartWorkflow)}.{nameof(ExpireCartsAsync)}.{ex.GetType()}",
                        ex.Message,
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR(ex.Message);
            }
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Cart? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.RateQuotes is not null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.RateQuotes.Count(x => x.CartID == entity.ID);)
                {
                    context.RateQuotes.Remove(context.RateQuotes.First(x => x.CartID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse, InvertIf
            if (context.Notes is not null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Notes.Count(x => x.CartID == entity.ID);)
                {
                    context.Notes.Remove(context.Notes.First(x => x.CartID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }

        /// <summary>Admin add cart and save safely asynchronous.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{(ICart entityToUse,bool wasSaved,bool dupeFound)}.</returns>
        protected virtual async Task<(ICart entityToUse, bool wasSaved, bool dupeFound)> AdminAddCartAndSaveSafelyAsync(
            ICart cart,
            string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                context.Carts.Add((Cart)cart);
                var saved = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                if (!saved)
                {
                    throw new("ERROR! Something about creating the cart as an admin didn't work.");
                }
                return (cart, true, false);
            }
            catch (DbUpdateException ex)
            {
                const string MsgTest = "Cannot insert duplicate key row in object 'Shopping.Cart'";
                if (!ex.Message.StartsWith(MsgTest)
                    && ex.InnerException?.Message.StartsWith(MsgTest) != true
                    && ex.InnerException?.InnerException?.Message.StartsWith(MsgTest) != true
                    && ex.InnerException?.InnerException?.InnerException?.Message.StartsWith(MsgTest) != true)
                {
                    throw;
                }
                // Try to get the entity again, it probably tried to make a duplicate cart
                // because of threading issues, so call the other one that was made
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var cartTypeName = await context.CartTypes
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(cart.TypeID)
                    .FilterByName(cart.Type?.Name)
                    .Select(x => x.Name)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
                var altCart = await context.Carts
                    .FilterByActive(true)
                    .FilterByID(cart.ID)
                    .FilterCartsBySessionID(cart.SessionID)
                    .FilterCartsByUserID(cart.UserID)
                    .FilterCartsByBrandID(cart.BrandID)
                    .FilterCartsByFranchiseID(cart.FranchiseID)
                    .FilterCartsByStoreID(cart.StoreID)
                    .FilterByTypeName<Cart, CartType>(cart.Type?.Name ?? cartTypeName, true)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
                if (altCart is null)
                {
                    throw;
                }
                return (altCart, false, true);
            }
        }

        private static async Task CorrectionsForTesting(IClarityEcommerceEntities context, Cart entity)
        {
            var tempSalesItems = entity.SalesItems;
            foreach (var item in tempSalesItems!)
            {
                if (!Contract.CheckValidID(item.MasterID))
                {
                    item.MasterID = entity.ID;
                }
                var tempTargets = item.Targets;
                if (!Contract.CheckValidID(item.ID))
                {
                    context.CartItems.Add(item);
                }
                Contract.RequiresValidID(item.ID);
                foreach (var target in tempTargets!)
                {
                    if (!Contract.CheckValidID(target.MasterID))
                    {
                        target.MasterID = item.ID;
                    }
                    if (!Contract.CheckValidID(target.ID))
                    {
                        context.CartItemTargets.Add(target);
                    }
                    Contract.RequiresValidID(target.ID);
                }
            }
            Contract.RequiresAllValidIDs(
                await context.CartItems
                    .Select(x => (int?)x.ID)
                    .ToArrayAsync()
                    .ConfigureAwait(false));
            Contract.RequiresAllValidIDs(
                await context.Carts
                    .Where(x => x.ID == entity.ID)
                    .SelectMany(
                        x => x.SalesItems!
                            .Where(y => y.Active)
                            .Select(y => (int?)y.ID))
                    .ToArrayAsync()
                    .ConfigureAwait(false));
        }

        /// <summary>Assign user identifier to the cart if null.</summary>
        /// <param name="cart">   The cart.</param>
        /// <param name="userID"> Identifier for the user.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        private static Task AssignUserIDToCartIfNullAsync(
            IAmFilterableByNullableUser cart,
            int? userID,
            IClarityEcommerceEntities context)
        {
            if (cart.UserID.HasValue || !userID.HasValue)
            {
                return Task.CompletedTask;
            }
            cart.UserID = userID;
            return context.SaveUnitOfWorkAsync(true);
        }

        /// <summary>Assign user identifier to the cart if null.</summary>
        /// <param name="cart">     The cart.</param>
        /// <param name="accountID">Identifier for the account.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        private static Task AssignAccountIDToCartIfNullAsync(
            IAmFilterableByNullableAccount cart,
            int? accountID,
            IClarityEcommerceEntities context)
        {
            if (cart.AccountID.HasValue || !accountID.HasValue)
            {
                return Task.CompletedTask;
            }
            cart.AccountID = accountID;
            return context.SaveUnitOfWorkAsync(true);
        }

        /// <summary>Assign user identifier to cart if null.</summary>
        /// <param name="cartID">            Identifier for the cart.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task AssignUserIDToCartIfNullAsync(
            int cartID,
            int? userID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts.FilterByID(cartID).SingleAsync().ConfigureAwait(false);
            if (cart.UserID.HasValue || !userID.HasValue)
            {
                return;
            }
            cart.UserID = userID;
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
        }

        /// <summary>Assign account identifier to cart if null.</summary>
        /// <param name="cartID">            Identifier for the cart.</param>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task AssignAccountIDToCartIfNullAsync(
            int cartID,
            int? accountID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var cart = await context.Carts.FilterByID(cartID).SingleAsync().ConfigureAwait(false);
            if (cart.AccountID.HasValue || !accountID.HasValue)
            {
                return;
            }
            cart.AccountID = accountID;
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
        }

        /// <summary>Query if 'address' is valid for shipping estimator.</summary>
        /// <param name="address">The address.</param>
        /// <returns>True if valid for shipping estimator, false if not.</returns>
        private static bool IsValidForShippingEstimator(IAddressModel? address)
        {
            if (address == null)
            {
                return false;
            }
            if (!Contract.CheckValidKey(address.PostalCode))
            {
                return false;
            }
            if (!Contract.CheckAnyValidKey(address.Region?.Code, address.RegionCode))
            {
                return false;
            }
            if (!Contract.CheckAnyValidKey(address.Country?.Code, address.CountryCode))
            {
                return false;
            }
            return true;
        }

        /// <summary>Executes the limited relate workflows operation.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task RunLimitedRelateWorkflowsAsync(
            ICart entity,
            ICartModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <summary>Executes the limited relate workflows operation.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        private async Task RunLimitedRelateWorkflowsAsync(
            ICart entity,
            ICartModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // NOTE: Can't use Task.WhenAll as the context isn't thread safe and will throw an exception is more than
            // one thing is running at the same time on the same context.
            await RelateOptionalBillingContactAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalShippingContactAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateRequiredStatusAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateRequiredStateAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateRequiredTypeAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalUserAsync(entity, model, context).ConfigureAwait(false);
            await RelateOptionalAccountAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalStoreAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalBrandAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalShipmentAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <summary>Relate Optional User.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Optional User.</param>
        /// <param name="model">    The model that has a Optional User.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        private async Task RelateOptionalUserAsync(
            ICart entity,
            ICartModel model,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolvedID = await Workflows.Users.ResolveToIDOptionalAsync(
                    model.UserID, // By Other ID
                    model.UserKey, // By Flattened Other Key
                    model.User, // Manual name if not UserProductType and not Discount or Discount and not Master
                    context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.UserID);
            var modelIDIsNull = !Contract.CheckValidID(resolvedID);
            var entityObjectIsNull = entity.User == null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.UserID == resolvedID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && entity.User!.ID == resolvedID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                return;
            }
            if (modelIDIsNull)
            {
                return;
            }
            // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
            entity.UserID = resolvedID!.Value;
        }

        /// <summary>Is shipping required.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="shippingProviders"> The shipping providers.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        private async Task<CEFActionResponse> IsShippingRequiredAsync(
            ISalesCollectionBaseModel? cart,
            IEnumerable<IShippingProviderBase> shippingProviders,
            string? contextProfileName)
        {
            if (cart is null)
            {
                const string Message = "WARNING! No cart";
                ////Logger?.LogWarning("Get Shipping Rates for Cart", Message, contextProfileName);
                return CEFAR.FailingCEFAR(Message);
            }
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                // ReSharper disable once InvertIf
                if (!await context.CartItems
                        .FilterByActive(true)
                        .FilterCartItemsByCartID(cart.ID)
                        .FilterCartItemsByHasQuantity()
                        .AnyAsync()
                        .ConfigureAwait(false))
                {
                    const string Message = "WARNING! There are no items in this cart.";
                    ////Logger?.LogWarning("Get Shipping Rates for Cart", Message, contextProfileName);
                    return CEFAR.FailingCEFAR(Message);
                }
            }
            return shippingProviders.All(x => x.Name.Contains("FlatRate") || x.Name.Contains("ZoneRate"))
                ? CEFAR.PassingCEFAR()
                : await ValidatePackagingProviderAsync(
                        cart.ID,
                        cart.ShippingSameAsBilling ?? false,
                        cart.BillingContact,
                        cart.ShippingContact,
                        contextProfileName)
                    .ConfigureAwait(false);
        }

        /// <summary>Generates a blank cart.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="typeName">          Name of the type.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="sessionID">         Identifier for the session.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="brandID">           Identifier for the brand.</param>
        /// <param name="franchiseID">       Identifier for the franchise.</param>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <returns>The blank cart.</returns>
        private async Task<ICart> GenerateBlankCartAsync(
            ICartModel? model,
            string typeName,
            string? contextProfileName,
            Guid? sessionID = null,
            int? userID = null,
            int? accountID = null,
            int? brandID = null,
            int? franchiseID = null,
            int? storeID = null)
        {
            var typeID = 0;
            if (model != null)
            {
                typeID = await Workflows.CartTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: model.TypeID,
                        byKey: model.TypeKey,
                        byName: model.TypeName,
                        byDisplayName: model.TypeDisplayName,
                        model: model.Type,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(typeID))
            {
                typeID = await Workflows.CartTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: typeName,
                        byName: typeName,
                        byDisplayName: typeName,
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            _ = Contract.RequiresValidID(typeID, "ERROR! No Cart Type information to resolve");
            int? statusID = null;
            if (model != null)
            {
                statusID = await Workflows.CartStatuses.ResolveWithAutoGenerateToIDOptionalAsync(
                        byID: model.StatusID,
                        byKey: model.StatusKey,
                        byName: model.StatusName,
                        byDisplayName: model.StatusDisplayName,
                        model: model.Status,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(statusID))
            {
                statusID = await Workflows.CartStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "New",
                        byName: "New",
                        byDisplayName: "New",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            var stateID = await Workflows.CartStates.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "WORK",
                    byName: "Work",
                    byDisplayName: "Work",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var cart = RegistryLoaderWrapper.GetInstance<Cart>(contextProfileName);
            cart.Active = true;
            cart.CreatedDate = DateExtensions.GenDateTime;
            cart.JsonAttributes = "{}";
            cart.TypeID = typeID;
            cart.StatusID = statusID!.Value;
            cart.StateID = stateID;
            cart.SessionID = sessionID;
            cart.UserID = userID;
            cart.AccountID = accountID;
            cart.BrandID = brandID;
            cart.FranchiseID = franchiseID;
            cart.StoreID = storeID;
            return cart;
        }

        /// <summary>Appends a price data to sales items.</summary>
        /// <param name="model">                The model.</param>
        /// <param name="isStatic">             True if this cart is static (use quantity 1 for each item).</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task AppendPriceDataToSalesItemsAsync(
            ICartModel? model,
            bool isStatic,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName)
        {
            if (pricingFactoryContext == null || model == null)
            {
                return;
            }
            // Cache the values temporarily in case it's the same total sum context, so we don't run the value multiple times
            var tempCalcCache = new Dictionary<long, ICalculatedPrice>();
            foreach (var item in model.SalesItems!.Where(item => Contract.CheckValidID(item.ProductID)))
            {
                pricingFactoryContext.Quantity = isStatic ? 1m : item.TotalQuantity;
                // ReSharper disable once PossibleInvalidOperationException (Verified in the Where above)
                var hash = Digest.Crc64($"{item.ProductID!.Value}{item.SerializableAttributes.SerializeAttributesDictionary()}");
                if (tempCalcCache.ContainsKey(hash)
                    && tempCalcCache.TryGetValue(hash, out var calcPrice)
                    && calcPrice.IsValid)
                {
                    item.UnitCorePrice = calcPrice.BasePrice;
                    item.UnitSoldPrice = calcPrice.SalePrice;
                    continue;
                }
                var calculatedPrice = await Workflows.PricingFactory.CalculatePriceAsync(
                        productID: item.ProductID.Value,
                        salesItemAttributes: item.SerializableAttributes!,
                        pricingFactoryContext: pricingFactoryContext,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                tempCalcCache[hash] = calculatedPrice;
                if (!calculatedPrice.IsValid)
                {
                    continue;
                }
                if (CEFConfigDictionary.UseCustomPriceConversionForCartItems
                    && item.SerializableAttributes.TryGetValue("SoldPrice", out var rawPrice)
                    && decimal.TryParse(rawPrice!.Value, out decimal soldPrice))
                {
                    item.UnitSoldPrice = soldPrice;
                    item.UnitCorePrice = soldPrice;
                }
                else
                {
                    item.UnitCorePrice = calculatedPrice.BasePrice;
                    item.UnitSoldPrice = calculatedPrice.SalePrice;
                }
                item.SerializableAttributes ??= new();
                item.SerializableAttributes!["PriceKey"] = new()
                {
                    Key = "PriceKey",
                    Value = calculatedPrice.PriceKey!,
                };
                item.SerializableAttributes["PriceKeyAlt"] = new()
                {
                    Key = "PriceKeyAlt",
                    Value = calculatedPrice.PriceKeyAlt!,
                };
                item.SerializableAttributes["PriceKeyRelevantRules"] = new()
                {
                    Key = "PriceKeyRelevantRules",
                    Value = Newtonsoft.Json.JsonConvert.SerializeObject(calculatedPrice.RelevantRules),
                };
                item.SerializableAttributes["PriceKeyUsedRules"] = new()
                {
                    Key = "PriceKeyUsedRules",
                    Value = Newtonsoft.Json.JsonConvert.SerializeObject(calculatedPrice.UsedRules),
                };
            }
        }

        /// <summary>Clean billing shipping and contacts.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        private async Task CleanBillingShippingAndContactsAsync(
            ICartModel model,
            IClarityEcommerceEntities context)
        {
            if (model.BillingContact?.Address != null)
            {
                model.BillingContact.Address = await Workflows.Addresses.ResolveAddressAsync(
                        model.BillingContact.Address,
                        context)
                    .ConfigureAwait(false);
            }
            if (model.ShippingContact?.Address != null)
            {
                model.ShippingContact.Address = await Workflows.Addresses.ResolveAddressAsync(
                        model.ShippingContact.Address,
                        context)
                    .ConfigureAwait(false);
            }
            if (model.Contacts?.Any(x => x.Active && x.Contact?.Address != null) != true)
            {
                return;
            }
            foreach (var cartContact in model.Contacts.Where(x => x.Active && x.Contact?.Address != null))
            {
                cartContact.Contact!.Address = await Workflows.Addresses.ResolveAddressAsync(
                        cartContact.Contact.Address!,
                        context)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>Validates the packaging provider.</summary>
        /// <param name="cartID">               Identifier for the cart.</param>
        /// <param name="shippingSameAsBilling">True to shipping same as billing.</param>
        /// <param name="billing">              The billing.</param>
        /// <param name="shipping">             The shipping.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        private async Task<CEFActionResponse> ValidatePackagingProviderAsync(
            int cartID,
            bool shippingSameAsBilling,
            IContactModel? billing,
            IContactModel? shipping,
            string? contextProfileName)
        {
            var packagingProvider = RegistryLoaderWrapper.GetPackagingProvider(contextProfileName);
            if (packagingProvider == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Packaging provider is required to get shipping rate quotes");
            }
            var itemsResult = await packagingProvider.GetItemPackagesAsync(cartID, contextProfileName).ConfigureAwait(false);
            if (!itemsResult.ActionSucceeded)
            {
                CEFAR.FailingCEFAR("ERROR! Packaging provider must return successfully");
                return itemsResult.ChangeFailingCEFARType<List<IRateQuoteModel>>();
            }
            var items = itemsResult.Result;
            if (items!.Count == 0)
            {
                const string Message = "WARNING! There are no items in this cart with a valid Package assigned.";
                await Logger.LogWarningAsync("Get Shipping Rates for Cart", Message, contextProfileName).ConfigureAwait(false);
                return CEFAR.FailingCEFAR(Message);
            }
            if (shippingSameAsBilling)
            {
                // ReSharper disable once InvertIf
                if (!IsValidForShippingEstimator(billing?.Address))
                {
                    const string Message = "WARNING! There was no billing address to calculate rates against (shipping same as billing has been set).";
                    await Logger.LogWarningAsync("Get Shipping Rates for Cart", Message, contextProfileName).ConfigureAwait(false);
                    return CEFAR.FailingCEFAR(Message);
                }
            }
            else if (!IsValidForShippingEstimator(shipping?.Address))
            {
                const string Message = "WARNING! There was no shipping address to calculate rates against.";
                ////await Logger.LogWarningAsync("Get Shipping Rates for Cart", Message, contextProfileName).ConfigureAwait(false);
                return CEFAR.FailingCEFAR(Message);
            }
            return items
                .Any(
                    pkg => pkg.DimensionalWeight > 0.00m
                        || pkg.Weight > 0.00m
                        || (pkg.Depth ?? 0.00m) > 0.00m
                        && (pkg.Height ?? 0.00m) > 0.00m
                        && (pkg.Width ?? 0.00m) > 0.00m)
                .BoolToCEFAR("ERROR! There are no valid packages for the cart items");
        }
    }
}
