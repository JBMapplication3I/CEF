// <copyright file="SalesQuoteWithSalesItemsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate quote files workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class SalesQuoteWithSalesItemsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            ISalesItemBaseModel<IAppliedSalesQuoteItemDiscountModel> model,
            ISalesQuoteItem entity,
            IClarityEcommerceEntities context)
        {
            return await base.MatchObjectModelWithObjectEntityAdditionalChecksAsync(model, entity, context).ConfigureAwait(false)
                && model.OriginalCurrencyID == entity.OriginalCurrencyID
                && model.SellingCurrencyID == entity.SellingCurrencyID
                && model.ProductID == entity.ProductID
                && model.UserID == entity.UserID
                && model.Hash == entity.Hash
                && model.Quantity == entity.Quantity
                && model.QuantityBackOrdered == entity.QuantityBackOrdered
                && model.QuantityPreSold == entity.QuantityPreSold
                && model.UnitCorePrice == entity.UnitCorePrice
                && model.UnitSoldPrice == entity.UnitSoldPrice
                && model.UnitCorePriceInSellingCurrency == entity.UnitCorePriceInSellingCurrency
                && model.UnitSoldPriceInSellingCurrency == entity.UnitSoldPriceInSellingCurrency
                && model.Sku == entity.Sku
                && model.ForceUniqueLineItemKey == entity.ForceUniqueLineItemKey
                && model.UnitOfMeasure == entity.UnitOfMeasure
                && model.SerializableAttributes?.SerializeAttributesDictionary()
                    == entity.SerializableAttributes?.SerializeAttributesDictionary();
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            ISalesQuoteItem newEntity,
            ISalesItemBaseModel<IAppliedSalesQuoteItemDiscountModel> model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // TODO: Ensure all properties are set
            // Resolve related objects
            newEntity.CustomKey = model.CustomKey;
            newEntity.Active = true;
            newEntity.CreatedDate = timestamp;
            newEntity.UpdatedDate = null;
            newEntity.Hash = model.Hash;
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(
                    newEntity,
                    model,
                    context)
                .ConfigureAwait(false);
            newEntity.Name = model.Name ?? model.ProductName;
            newEntity.Description = model.Description ?? model.ProductDescription;
            newEntity.Quantity = model.Quantity;
            newEntity.QuantityBackOrdered = model.QuantityBackOrdered;
            newEntity.QuantityPreSold = model.QuantityPreSold;
            newEntity.Sku = model.Sku ?? model.ProductKey;
            newEntity.ForceUniqueLineItemKey = model.ForceUniqueLineItemKey;
            newEntity.UnitCorePrice = model.UnitCorePrice;
            newEntity.UnitCorePriceInSellingCurrency = model.UnitCorePriceInSellingCurrency;
            newEntity.UnitSoldPrice = model.UnitSoldPrice;
            newEntity.UnitSoldPriceInSellingCurrency = model.UnitSoldPriceInSellingCurrency;
            newEntity.UnitOfMeasure = model.UnitOfMeasure;
            // Related Objects
            newEntity.ProductID = await Workflows.Products.ResolveWithAutoGenerateToIDOptionalAsync(
                    model.ProductID,
                    model.ProductKey,
                    model.ProductName,
                    null,
                    context)
                .ConfigureAwait(false);
            newEntity.UserID = await Workflows.Users.ResolveToIDOptionalAsync(
                    model.UserID,
                    model.UserKey,
                    model.User,
                    context)
                .ConfigureAwait(false);
            if (!Contract.CheckValidIDOrAnyValidKey(
                    model.OriginalCurrencyID ?? model.OriginalCurrency?.ID,
                    model.OriginalCurrencyKey,
                    model.OriginalCurrencyName,
                    model.OriginalCurrency?.CustomKey,
                    model.OriginalCurrency?.Name))
            {
                model.OriginalCurrencyKey = "USD";
            }
            newEntity.OriginalCurrencyID = await Workflows.Currencies.ResolveWithAutoGenerateToIDAsync(
                    model.OriginalCurrencyID,
                    model.OriginalCurrencyKey,
                    model.OriginalCurrencyName,
                    model.OriginalCurrency,
                    context)
                .ConfigureAwait(false);
            if (!Contract.CheckValidIDOrAnyValidKey(
                    model.SellingCurrencyID ?? model.SellingCurrency?.ID,
                    model.SellingCurrencyKey,
                    model.SellingCurrencyName,
                    model.SellingCurrency?.CustomKey,
                    model.SellingCurrency?.Name))
            {
                model.SellingCurrencyKey = "USD";
            }
            newEntity.SellingCurrencyID = await Workflows.Currencies.ResolveWithAutoGenerateToIDAsync(
                    model.SellingCurrencyID,
                    model.SellingCurrencyKey,
                    model.SellingCurrencyName,
                    model.SellingCurrency,
                    context)
                .ConfigureAwait(false);
            await Workflows.SalesQuoteItemWithDiscountsAssociation.AssociateObjectsAsync(
                    newEntity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }
    }
}
