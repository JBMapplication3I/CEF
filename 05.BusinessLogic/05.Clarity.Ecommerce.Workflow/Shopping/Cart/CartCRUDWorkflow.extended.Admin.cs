// <copyright file="CartCRUDWorkflow.extended.Admin.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the admin cart workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
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
        public async Task<ICartModel?> AdminGetAsync(
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            Contract.RequiresAllValid(
                lookupKey.CartID,
                lookupKey.UserID,
                lookupKey.AccountID,
                pricingFactoryContext,
                taxesProvider);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await GetEntityAsync(lookupKey.CartID, context).ConfigureAwait(false);
            return (await SessionCartResolveInnerAdminAsync(
                        lookupKey: new SessionCartBySessionAndTypeLookupKey(
                            sessionID: entity.SessionID!.Value,
                            typeKey: entity.Type!.CustomKey!,
                            userID: entity.UserID,
                            accountID: entity.AccountID,
                            brandID: entity.BrandID,
                            franchiseID: entity.FranchiseID,
                            storeID: entity.StoreID),
                        cart: entity,
                        taxesProvider: taxesProvider,
                        pricingFactoryContext: pricingFactoryContext,
                        context: context,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false))
                .cartResponse.Result;
        }

        /// <inheritdoc/>
        public async Task<ICartModel?> AdminGetAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            Contract.RequiresAllValid(lookupKey.UserID, lookupKey.AccountID, lookupKey.TypeKey, pricingFactoryContext, taxesProvider);
            await RemoveCartsThatAreEmptyAsync(contextProfileName).ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.Carts
                .FilterByActive(true)
                .FilterCartsByLookupKey(lookupKey)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (entity is null)
            {
                return null;
            }
            return (await SessionCartResolveInnerAdminAsync(
                        lookupKey: lookupKey,
                        cart: entity,
                        taxesProvider: taxesProvider,
                        pricingFactoryContext: pricingFactoryContext,
                        context: context,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false))
                .cartResponse.Result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> AdminCreateAsync(
            ICartModel model,
            string? contextProfileName)
        {
            var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await GenerateBlankCartAsync(
                    Contract.RequiresNotNull(model),
                    model.TypeName!,
                    contextProfileName)
                .ConfigureAwait(false);
            if (entity is null)
            {
                return CEFAR.FailingCEFAR<int>("ERROR! Failed to generate a blank cart");
            }
            var timestamp = DateExtensions.GenDateTime;
            async Task AssignFlatValuesAsync()
            {
                // ReSharper disable AccessToModifiedClosure
                // ReSharper disable once AccessToDisposedClosure
                var typeKey = await context!.CartTypes
                    .AsNoTracking()
                    .FilterByID(entity!.TypeID)
                    .Select(x => x.CustomKey ?? x.Name)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
                var applySession = typeKey?.StartsWith("Target-Grouping-") != true;
                entity.DueDate = model.DueDate;
                entity.RequestedShipDate = model.RequestedShipDate;
                entity.UpdatedDate = timestamp;
                entity.SessionID = model.SessionID ?? (applySession ? Guid.NewGuid() : null);
                entity.CustomKey = entity.SessionID?.ToString();
                entity.StoreID = model.StoreID;
                entity.AccountID = model.AccountID;
                entity.UserID = model.UserID;
                entity.SubtotalDiscountsModifier = model.SubtotalDiscountsModifier;
                entity.SubtotalDiscountsModifierMode = model.SubtotalDiscountsModifierMode;
                entity.SubtotalTaxesModifier = model.SubtotalTaxesModifier;
                entity.SubtotalTaxesModifierMode = model.SubtotalTaxesModifierMode;
                entity.SubtotalHandlingModifier = model.SubtotalHandlingModifier;
                entity.SubtotalHandlingModifierMode = model.SubtotalHandlingModifierMode;
                entity.SubtotalFeesModifier = model.SubtotalFeesModifier;
                entity.SubtotalFeesModifierMode = model.SubtotalFeesModifierMode;
                entity.SubtotalShippingModifier = model.SubtotalShippingModifier;
                entity.SubtotalShippingModifierMode = model.SubtotalShippingModifierMode;
                entity.JsonAttributes = model.SerializableAttributes.SerializeAttributesDictionary();
                entity.ShippingSameAsBilling = model.ShippingSameAsBilling;
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (model.Totals is null)
                {
                    return;
                }
                entity.SubtotalItems = model.Totals.SubTotal;
                entity.SubtotalShipping = model.Totals.Shipping;
                entity.SubtotalHandling = model.Totals.Handling;
                entity.SubtotalFees = model.Totals.Fees;
                entity.SubtotalDiscounts = model.Totals.Discounts;
                entity.SubtotalTaxes = model.Totals.Tax;
                entity.Total = model.Totals.Total;
                // ReSharper restore AccessToModifiedClosure
            }
            await AssignFlatValuesAsync().ConfigureAwait(false);
            if (Contract.CheckValidID(entity.ID))
            {
                // It's been saved previously, so don't try to add, just save the context now to cement any changes
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
            else
            {
                try
                {
                    var (entityToUse, wasSaved, dupeFound) = await AdminAddCartAndSaveSafelyAsync(
                            entity,
                            contextProfileName)
                        .ConfigureAwait(false);
                    if (wasSaved)
                    {
                        // Do nothing, entity should have an ID now as that function saved the context after adding the entity
                    }
                    else if (dupeFound)
                    {
                        // Turns out this entity was a duplicate, replace the entity with the one loaded from that context
                        // Reset the context we're using since it was in an invalid state
                        context.Dispose();
                        context = null;
                        context = RegistryLoaderWrapper.GetContext(contextProfileName);
                        // Since the context this entity was loaded from is gone, we need to load from the one we have here instead
                        entity = context.Carts.Find(entityToUse.ID);
                    }
                }
                catch
                {
                    // TODO: Log the exception
                    return CEFAR.FailingCEFAR<int>("ERROR! Unable to save new Cart");
                }
            }
            // Now that we know we have a saved cart record, associate data to it
            await CleanBillingShippingAndContactsAsync(model, context).ConfigureAwait(false);
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            var retVal = await context.SaveUnitOfWorkAsync().ConfigureAwait(false)
                ? entity.ID.WrapInPassingCEFAR()
                : CEFAR.FailingCEFAR<int>("ERROR! Unable to save new Cart");
            var resave = false;
            foreach (var salesItem in entity.SalesItems!)
            {
                if (salesItem.ID > 0)
                {
                    continue;
                }
                salesItem.MasterID = entity.ID;
                context.Entry(salesItem).State = EntityState.Added;
                resave = true;
            }
            if (resave)
            {
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            context.Dispose();
            context = null;
            return retVal;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> AdminUpdateAsync(
            ICartModel model,
            string? contextProfileName)
        {
            Contract.RequiresValidID<ArgumentNullException>(model?.ID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.Carts
                .FilterByID(model!.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (entity is null)
            {
                return CEFAR.FailingCEFAR($"ERROR! Unable to locate cart with ID '{model.ID}'");
            }
            var timestamp = DateExtensions.GenDateTime;
            // Base Properties
            entity.Active = model.Active;
            entity.BrandID = model.BrandID;
            entity.CreatedDate = model.CreatedDate;
            entity.UpdatedDate = timestamp;
            entity.CustomKey = model.CustomKey;
            entity.Hash = model.Hash;
            entity.TypeID = model.TypeID;
            // SalesCollection Properties
            entity.DueDate = model.DueDate;
            if (model.Totals is not null)
            {
                entity.SubtotalItems = model.Totals.SubTotal;
                entity.SubtotalShipping = model.Totals.Shipping;
                entity.SubtotalHandling = model.Totals.Handling;
                entity.SubtotalFees = model.Totals.Fees;
                entity.SubtotalDiscounts = model.Totals.Discounts;
                entity.SubtotalTaxes = model.Totals.Tax;
                entity.Total = model.Totals.Total;
            }
            entity.ShippingSameAsBilling = model.ShippingSameAsBilling;
            // Cart Properties
            entity.SessionID = model.SessionID;
            entity.RequestedShipDate = model.RequestedShipDate;
            entity.SubtotalDiscountsModifier = model.SubtotalDiscountsModifier;
            entity.SubtotalDiscountsModifierMode = model.SubtotalDiscountsModifierMode;
            entity.SubtotalTaxesModifier = model.SubtotalTaxesModifier;
            entity.SubtotalTaxesModifierMode = model.SubtotalTaxesModifierMode;
            entity.SubtotalHandlingModifier = model.SubtotalHandlingModifier;
            entity.SubtotalHandlingModifierMode = model.SubtotalHandlingModifierMode;
            entity.SubtotalFeesModifier = model.SubtotalFeesModifier;
            entity.SubtotalFeesModifierMode = model.SubtotalFeesModifierMode;
            entity.SubtotalShippingModifier = model.SubtotalShippingModifier;
            entity.SubtotalShippingModifierMode = model.SubtotalShippingModifierMode;
            // Relate/Associate Objects
            await CleanBillingShippingAndContactsAsync(model, context).ConfigureAwait(false);
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            // Save
            if (!await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR("ERROR! Unable to save cart changes");
            }
            await RemoveCartsThatAreEmptyAsync(contextProfileName).ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> AdminClearAsync(
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            var changed = false;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var cartItem in context.CartItems.Where(x => x.MasterID == lookupKey.CartID))
            {
                cartItem.Active = false;
                cartItem.UpdatedDate = timestamp;
                changed = true;
            }
            if (changed)
            {
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                await RemoveCartsThatAreEmptyAsync(contextProfileName).ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Session cart resolve inner.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="context">              The context.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse{ICartModel}.</returns>
        private async Task<(CEFActionResponse<ICartModel?> cartResponse, Guid? updatedSessionID)> SessionCartResolveInnerAdminAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ICart cart,
            ITaxesProviderBase? taxesProvider,
            IPricingFactoryContextModel pricingFactoryContext,
            IClarityEcommerceEntities context,
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
            var bestCartID = response.Result.cartID;
            if (cart is null || cart.ID != bestCartID)
            {
                cart = await SessionGetEntityAsync(bestCartID, context).ConfigureAwait(false);
            }
            if (cart is null)
            {
                response.Messages.Add("ERROR! Couldn't load Cart");
                return (response.ChangeFailingCEFARType<ICartModel?>(), null);
            }
            await AssignUserIDToCartIfNullAsync(cart, lookupKey.UserID, context).ConfigureAwait(false);
            await AssignAccountIDToCartIfNullAsync(cart, lookupKey.AccountID, context).ConfigureAwait(false);
            var model = cart.SessionMap(contextProfileName);
            if (model!.SalesItems!.Any(x => x.TotalQuantity <= 0))
            {
                foreach (var salesItem in model.SalesItems!.Where(x => x.TotalQuantity <= 0))
                {
                    await Workflows.CartItems.DeleteAsync(salesItem.ID, contextProfileName).ConfigureAwait(false);
                }
                model.SalesItems = model.SalesItems!
                    .Where(x => x.TotalQuantity > 0)
                    .ToList();
            }
            if (model.SalesItems!.Count == 0)
            {
                // Cart shouldn't exist
                response.Messages.Add("ERROR! This cart doesn't have any sales items and will be removed.");
                // ReSharper disable once InvertIf
                if (Contract.CheckValidID(response.Result.cartID))
                {
                    await DeleteAsync(response.Result.cartID, context).ConfigureAwait(false);
                    response.Result = (0, null);
                }
                return (response.ChangeFailingCEFARType<ICartModel?>(), null);
            }
            await AppendPriceDataToSalesItemsAsync(model, false, pricingFactoryContext, contextProfileName).ConfigureAwait(false);
            model.Totals.SubTotal = model.SalesItems.Sum(si => si.ExtendedPrice);
            await AppendShippingAndHandlingDataAsync(
                    model: model,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await AppendStoreDataAsync(
                    model: model,
                    context: context,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await AppendTaxesDataAsync(
                    model: model,
                    userID: lookupKey.UserID,
                    currentAccountID: lookupKey.AltAccountID,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await AppendDiscountDataAsync(
                    model: model,
                    taxesProvider: taxesProvider,
                    pricingFactoryContext: pricingFactoryContext,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return (model.WrapInPassingCEFAR(response.Messages.ToArray()), null);
        }

        /// <summary>Appends a shipping and handling data.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task AppendShippingAndHandlingDataAsync(ICartModel model, string? contextProfileName)
        {
            model.Totals.Handling = CEFConfigDictionary.ShippingHandlingFeesEnabled
                ? await CalculateCartHandlingChargesAsync(model, contextProfileName).ConfigureAwait(false)
                + (model.Totals.SubTotal > 0
                    || await CalculateCartWeightAsync(model, contextProfileName).ConfigureAwait(false) > 0m
                        ? CEFConfigDictionary.ChargesHandlingForNon0CostOrWeightOrders ?? 0m
                        : 0m)
                : 0m;
            var sameAsBilling = model.ShippingSameAsBilling ?? false;
            model.Totals.Shipping = sameAsBilling && model.BillingContact is null && !Contract.CheckValidID(model.BillingContactID)
                // Shipping contact is same as billing contact, and billing contact is null, don't show shipping
                ? 0m
                : !sameAsBilling && model.ShippingContact is null && !Contract.CheckValidID(model.ShippingContactID)
                    // Shipping contact is null, don't show shipping
                    ? 0m
                    // Shipping contact is valid, use shipment or selected rate quote data
                    : model.Shipment is not null
                        // Use Shipment data
                        ? model.Shipment.PublishedRate ?? model.Totals.Shipping
                        // Try Rate Quotes Data
                        : model.RateQuotes!.Any(y => y.Active && y.Selected)
                            ? model.RateQuotes!.Where(y => y.Active).OrderByDescending(y => y.CreatedDate).First(y => y.Selected).Rate
                            ?? model.Totals.Shipping
                            : model.Totals.Shipping;
        }

        /// <summary>Appends a store data.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task AppendStoreDataAsync(
            ICartModel model,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            if (CEFConfigDictionary.StoresEnabled
                && model.Totals.Tax == 0
                && model.SalesItems?.Any() == true
                && model.SalesItems[0].SerializableAttributes.ContainsKey("SelectedStoreID")
                && int.TryParse(model.SalesItems[0].SerializableAttributes["SelectedStoreID"].Value, out var storeID))
            {
                model.Store = context.Stores
                    .AsNoTracking()
                    .FilterByID(storeID)
                    .SelectSingleLiteStoreAndMapToStoreModel(contextProfileName);
            }
        }

        /// <summary>Appends the taxes data.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="taxesProvider">     The taxes provider.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task AppendTaxesDataAsync(
            ICartModel model,
            int? userID,
            int? currentAccountID,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            if (!CEFConfigDictionary.TaxesEnabled || taxesProvider is null)
            {
                return;
            }
            var taxes = await taxesProvider.CalculateCartAsync(
                    model,
                    userID,
                    currentAccountID,
                    contextProfileName)
                .ConfigureAwait(false);
            // TODO: Handle taxes.ErrorMessages.Any()
            model.Totals.Tax = taxes.TotalTaxes;
        }

        /// <summary>Appends a discount data to the cart model.</summary>
        /// <param name="model">                The model.</param>
        /// <param name="taxesProvider">        The taxes provider.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task AppendDiscountDataAsync(
            ICartModel model,
            ITaxesProviderBase? taxesProvider,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            if (!CEFConfigDictionary.DiscountsEnabled)
            {
                return;
            }
            await Workflows.DiscountManager.VerifyCurrentDiscountsAsync(
                    model,
                    pricingFactoryContext,
                    taxesProvider,
                    contextProfileName)
                .ConfigureAwait(false);
            model.Totals.Discounts = model.Discounts!.Where(y => y.Active).Sum(y => y.DiscountTotal)
                + model.SalesItems!
                    .Where(y => y.Discounts?.Any(z => z.Active) == true)
                    .Sum(y => y.Discounts!.Where(z => z.Active).Sum(z => z.DiscountTotal));
            if (model.Discounts?.Any() != true)
            {
                return;
            }
            // Don't send all the codes back to the UI
            foreach (var discount in model.Discounts.Where(x => x.Discount is not null))
            {
                discount.Discount!.Codes = null;
            }
        }
    }
}
