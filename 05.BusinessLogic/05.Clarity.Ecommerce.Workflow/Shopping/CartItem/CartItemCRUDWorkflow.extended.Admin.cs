// <copyright file="CartItemCRUDWorkflow.extended.Admin.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart item workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;
    using JSConfigs;
    using Models;
    using Utilities;

    public partial class CartItemWorkflow
    {
        /// <inheritdoc/>
        public Task<CEFActionResponse<List<int?>>> AdminUpdateMultipleAsync(
            CartByIDLookupKey lookupKey,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> models,
            string? contextProfileName)
        {
            return Contract.CheckEmpty(models)
                ? Task.FromResult(CEFAR.FailingCEFAR<List<int?>>("ERROR! This list of models to update was null or empty"))
                : CEFAR.AggregateAsync(models, x => AdminUpdateAsync(lookupKey, x!, contextProfileName));
        }

        /// <summary>Update a list of cart items properties.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="model">             A model of the core cart item attributes.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{int?}.</returns>
        private async Task<CEFActionResponse<int?>> AdminUpdateAsync(
            CartByIDLookupKey lookupKey,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.CartItems.FilterByID(model.ID).SingleOrDefaultAsync().ConfigureAwait(false);
            if (entity == null)
            {
                return CEFAR.FailingCEFAR<int?>("ERROR! Could not locate cart item");
            }
            var product = await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(model.ProductID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            // TODO@JTG: Custom Override so not tied to a product
            if (product == null)
            {
                return CEFAR.FailingCEFAR<int?>("ERROR! Could not locate product");
            }
            var timestamp = DateExtensions.GenDateTime;
            var x = await GetModifiedQuantityForProductByMinMaxAllowForAccountIDOrUserIDAsync(
                    cartItem: entity,
                    product: product,
                    newQuantity: model.Quantity,
                    newQuantityBackOrder: model.QuantityBackOrdered ?? 0m,
                    newQuantityPreSold: model.QuantityPreSold ?? 0m,
                    currentQuantity: null,
                    currentQuantityBackOrder: null,
                    currentQuantityPreSold: null,
                    accountID: lookupKey.AccountID,
                    userID: lookupKey.UserID,
                    matrix: await Workflows.Stores.GetStoreInventoryLocationsMatrixAsync(contextProfileName).ConfigureAwait(false),
                    isUnlimitedCache: new Dictionary<int, bool>(),
                    allowPreSaleCache: new Dictionary<int, bool>(),
                    allowBackOrderCache: new Dictionary<int, bool>(),
                    flatStockCache: new Dictionary<int, decimal>(),
                    isForQuote: false, // TODO: Tie to cart type
                    contextProfileName: contextProfileName,
                    context: context)
                .ConfigureAwait(false);
            // Base Properties
            entity.Active = model.Active;
            entity.CustomKey = model.CustomKey;
            entity.CreatedDate = model.CreatedDate;
            entity.UpdatedDate = timestamp;
            entity.Hash = model.Hash;
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, contextProfileName).ConfigureAwait(false);
            if (!Contract.CheckValidKey(entity.JsonAttributes))
            {
                entity.JsonAttributes = "{}";
            }
            // NameableBase Properties
            entity.Name = product.Name;
            entity.Description = product.ShortDescription ?? product.Description;
            // SalesItemBase Properties
            entity.Sku = product.CustomKey;
            entity.ForceUniqueLineItemKey = model.ForceUniqueLineItemKey;
            entity.UnitOfMeasure = model.UnitOfMeasure;
            entity.Quantity = x.NewQuantity;
            entity.QuantityBackOrdered = x.NewQuantityBackOrdered;
            entity.QuantityPreSold = x.NewQuantityPreSold;
            entity.UnitCorePrice = model.UnitCorePrice;
            entity.UnitSoldPriceModifier = model.UnitSoldPriceModifier ?? 0m;
            entity.UnitSoldPriceModifierMode = model.UnitSoldPriceModifierMode ?? (int)Enums.TotalsModifierModes.Add;
            if (model.UnitSoldPrice == entity.UnitSoldPrice
                && entity.UnitSoldPriceModifierMode == (int)Enums.TotalsModifierModes.Add
                && entity.UnitSoldPriceModifier == 0m)
            {
                // It's the default value, use null instead
                entity.UnitSoldPrice = null;
            }
            else
            {
                entity.UnitSoldPrice = model.UnitSoldPrice;
            }
            // TODO: If the in selling values are empty but we have the target currency, run the calcs and store them here
            entity.UnitCorePriceInSellingCurrency = model.UnitCorePriceInSellingCurrency;
            entity.UnitSoldPriceInSellingCurrency = model.UnitSoldPriceInSellingCurrency;
            // Related Objects
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            // Associated Objects
            if (Contract.CheckNotEmpty(model.Targets))
            {
                await Workflows.CartItemWithTargetsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            if (Contract.CheckNotEmpty(model.Notes))
            {
                if (Contract.CheckValidID(entity.ID))
                {
                    foreach (var note in model.Notes!)
                    {
                        note.CartItemID = entity.ID;
                    }
                }
                await Workflows.CartItemWithNotesAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            if (!await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR<int?>("ERROR! Something about updating this record and saving it failed");
            }
            return ((int?)entity.ID).WrapInPassingCEFAR();
        }
    }
}
