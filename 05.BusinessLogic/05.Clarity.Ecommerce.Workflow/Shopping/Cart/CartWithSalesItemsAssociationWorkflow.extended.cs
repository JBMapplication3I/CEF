// <copyright file="CartWithSalesItemsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate cart sales items workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Utilities;

    public partial class CartWithSalesItemsAssociationWorkflow
    {
        private const int DefaultModMode = (int)Enums.TotalsModifierModes.Add;

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> model,
            ICartItem entity,
            IClarityEcommerceEntities context)
        {
            if (!(entity.Quantity == model.Quantity
                  && entity.QuantityBackOrdered == model.QuantityBackOrdered
                  && entity.QuantityPreSold == model.QuantityPreSold
                  && entity.Sku == model.Sku
                  && entity.ForceUniqueLineItemKey == model.ForceUniqueLineItemKey
                  && entity.UnitCorePrice == model.UnitCorePrice
                  && entity.UnitCorePriceInSellingCurrency == model.UnitCorePriceInSellingCurrency
                  && entity.UnitSoldPrice == model.UnitSoldPrice
                  && entity.UnitSoldPriceInSellingCurrency == model.UnitSoldPriceInSellingCurrency
                  && entity.UnitSoldPriceModifier == model.UnitSoldPriceModifier
                  && entity.UnitSoldPriceModifierMode == model.UnitSoldPriceModifierMode
                  && entity.UnitOfMeasure == model.UnitOfMeasure
                  && entity.OriginalCurrencyID == model.OriginalCurrencyID
                  && entity.SellingCurrencyID == model.SellingCurrencyID
                  && entity.ProductID == model.ProductID
                  && entity.UserID == model.UserID
                  && entity.Active == model.Active
                  && entity.JsonAttributes == model.SerializableAttributes.SerializeAttributesDictionary()))
            {
                return false;
            }
            if (!(entity.UnitSoldPrice == model.UnitSoldPrice
                  || entity.UnitSoldPrice == null && model.UnitSoldPrice == model.UnitCorePrice))
            {
                return false;
            }
            if ((entity.UnitSoldPriceModifier ?? 0m) != (model.UnitSoldPriceModifier ?? 0m))
            {
                return false;
            }
            if ((entity.UnitSoldPriceModifierMode ?? DefaultModMode) != (model.UnitSoldPriceModifierMode ?? DefaultModMode))
            {
                return false;
            }
            // NOTE: Considering null model collection as equivalent to no active records in entity collection
            var noTargetsMatch = entity.Targets?.Any(x => x.Active) == false && model.Targets?.Any(x => x.Active) != true;
            if (!noTargetsMatch)
            {
                foreach (var target in model.Targets!.Where(x => x.Active && x.DestinationContact == null))
                {
                    target.DestinationContactID = await CartItemWithTargetsAssociationWorkflow.ResolveDestinationContactAsync(
                            newEntity: null,
                            model: target,
                            timestamp: DateExtensions.GenDateTime,
                            context: context,
                            workflows: Workflows)
                        .ConfigureAwait(false);
                    target.DestinationContact = await Workflows.Contacts.GetAsync(
                            target.DestinationContactID,
                            context)
                        .ConfigureAwait(false);
                }
                var entityTargets = entity.Targets!
                    .Where(x => x.Active)
                    .Select(x => new TargetGroupingKey(
                            typeKey: x.Type!.CustomKey,
                            storeID: CEFConfigDictionary.StoresEnabled ? x.OriginStoreProductID : null,
                            brandID: CEFConfigDictionary.BrandsEnabled ? x.BrandProductID : null,
                            vendorID: CEFConfigDictionary.VendorsEnabled ? x.OriginVendorProductID : null,
                            ilID: CEFConfigDictionary.InventoryAdvancedEnabled ? x.OriginProductInventoryLocationSectionID : null,
                            nothingToShip: x.NothingToShip,
                            customSplitKey: null, // TODO@JTG
                            hashedDestination: Digest.Crc64(x.DestinationContact!.ToHashableString()))
                        .ToString())
                    .OrderBy(x => x)
                    .DefaultIfEmpty(string.Empty)
                    .Aggregate((c, n) => $"{c}\r\n{n}");
                var modelTargets = model.Targets!
                    .Where(x => x.Active)
                    .Select(x => new TargetGroupingKey(
                            typeKey: x.TypeKey ?? x.Type!.CustomKey,
                            storeID: null,
                            brandID: CEFConfigDictionary.BrandsEnabled ? x.BrandProductID : null,
                            vendorID: null,
                            ilID: null,
                            nothingToShip: x.NothingToShip,
                            customSplitKey: null, // TODO@JTG
                            hashedDestination: Digest.Crc64(x.DestinationContact!.ToHashableString()))
                        .ToString())
                    .OrderBy(x => x)
                    .DefaultIfEmpty(string.Empty)
                    .Aggregate((c, n) => $"{c}\r\n{n}");
                if (entityTargets != modelTargets)
                {
                    return false;
                }
            }
            var noDiscountsMatch = !entity.Discounts!.Any(x => x.Active) && model.Discounts?.Any(x => x.Active) != true;
            if (!noDiscountsMatch)
            {
                var entityDiscounts = entity.Discounts!
                    .Where(x => x.Active)
                    .Select(x => x.DiscountTotal.ToString("c4"))
                    .OrderBy(x => x)
                    .DefaultIfEmpty(string.Empty)
                    .Aggregate((c, n) => $"{c}\r\n{n}");
                var modelDiscounts = model.Discounts!
                    .Where(x => x.Active)
                    .Select(x => x.DiscountTotal.ToString("c4"))
                    .OrderBy(x => x)
                    .DefaultIfEmpty(string.Empty)
                    .Aggregate((c, n) => $"{c}\r\n{n}");
                if (entityDiscounts != modelDiscounts)
                {
                    return false;
                }
            }
            var noNotesMatch = !entity.Notes!.Any(x => x.Active) && model.Notes?.Any(x => x.Active) != true;
            // ReSharper disable once InvertIf
            if (!noNotesMatch)
            {
                var entityNotes = entity.Notes!
                    .Where(x => x.Active)
                    .Select(x => x.Note1)
                    .OrderBy(x => x)
                    .DefaultIfEmpty(string.Empty)
                    .Aggregate((c, n) => $"{c}\r\n{n}");
                var modelNotes = model.Notes!
                    .Where(x => x.Active)
                    .Select(x => x.Note1)
                    .OrderBy(x => x)
                    .DefaultIfEmpty(string.Empty)
                    .Aggregate((c, n) => $"{c}\r\n{n}");
                if (entityNotes != modelNotes)
                {
                    return false;
                }
            }
            return true;
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            ICartItem newEntity,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Give these properties default values
            newEntity.UnitSoldPriceModifier ??= 0m;
            newEntity.UnitSoldPriceModifierMode ??= DefaultModMode;
            // Status is already handled by this point as are the non-foreign columns on this table itself
            await ResolveOriginalCurrencyAsync(newEntity, model, context.ContextProfileName).ConfigureAwait(false);
            await ResolveSellingCurrencyAsync(newEntity, model, context.ContextProfileName).ConfigureAwait(false);
            await ResolveProductAsync(newEntity, model, context.ContextProfileName).ConfigureAwait(false);
            await ResolveUserAsync(newEntity, model, context.ContextProfileName).ConfigureAwait(false);
            if (model.Notes != null)
            {
                if (Contract.CheckValidID(newEntity.ID))
                {
                    foreach (var note in model.Notes)
                    {
                        note.CartItemID = newEntity.ID;
                    }
                }
                await Workflows.CartItemWithNotesAssociation.AssociateObjectsAsync(newEntity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            }
            if (model.Targets != null)
            {
                // Blank the ID so they can associate to this new cart item like they were new.
                // Otherwise, they will act like they are updating, but they aren't in this entity's
                // list (they belong to the original) so they all get "removed"
                foreach (var target in model.Targets)
                {
                    target.ID = 0;
                }
                await Workflows.CartItemWithTargetsAssociation.AssociateObjectsAsync(newEntity, model, timestamp, context).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        protected override Task DeactivateObjectAdditionalPropertiesAsync(ICartItem entity, DateTime timestamp)
        {
            return Task.WhenAll(
                entity.Notes!.Where(x => x.Active).ForEachAsync(8, x => Task.FromResult(x.Active = false)),
                entity.Targets!.Where(x => x.Active).ForEachAsync(8, x => Task.FromResult(x.Active = false)),
                entity.Discounts!.Where(x => x.Active).ForEachAsync(8, x => Task.FromResult(x.Active = false)));
        }

        private async Task ResolveOriginalCurrencyAsync(
            ICartItem newEntity,
            ISalesItemBaseModel model,
            string? contextProfileName)
        {
            if (Contract.CheckValidIDOrAnyValidKey(
                model.OriginalCurrencyID ?? model.OriginalCurrency?.ID,
                model.OriginalCurrencyKey,
                model.OriginalCurrencyName,
                model.OriginalCurrency?.CustomKey,
                model.OriginalCurrency?.Name))
            {
                newEntity.OriginalCurrencyID = await Workflows.Currencies.ResolveWithAutoGenerateToIDAsync(
                        byID: model.OriginalCurrencyID,
                        byKey: model.OriginalCurrencyKey,
                        byName: model.OriginalCurrencyName,
                        model: model.OriginalCurrency,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
        }

        private async Task ResolveSellingCurrencyAsync(
            ICartItem newEntity,
            ISalesItemBaseModel model,
            string? contextProfileName)
        {
            if (Contract.CheckValidIDOrAnyValidKey(
                model.SellingCurrencyID ?? model.SellingCurrency?.ID,
                model.SellingCurrencyKey,
                model.SellingCurrencyName,
                model.SellingCurrency?.CustomKey,
                model.SellingCurrency?.Name))
            {
                newEntity.SellingCurrencyID = await Workflows.Currencies.ResolveWithAutoGenerateToIDAsync(
                        byID: model.SellingCurrencyID,
                        byKey: model.SellingCurrencyKey,
                        byName: model.SellingCurrencyName,
                        model: model.SellingCurrency,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
        }

        private async Task ResolveProductAsync(
            ICartItem newEntity,
            ISalesItemBaseModel model,
            string? contextProfileName)
        {
            if (Contract.CheckValidIDOrAnyValidKey(
                model.ProductID,
                model.ProductKey,
                model.ProductName))
            {
                newEntity.ProductID = await Workflows.Products.ResolveToIDOptionalAsync(
                        byID: model.ProductID,
                        byKey: model.ProductKey,
                        byName: model.ProductName,
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
        }

        private async Task ResolveUserAsync(
            ICartItem newEntity,
            ISalesItemBaseModel model,
            string? contextProfileName)
        {
            if (Contract.CheckValidIDOrAnyValidKey(
                model.UserID ?? model.User?.ID,
                model.UserKey,
                model.User?.CustomKey))
            {
                newEntity.UserID = await Workflows.Users.ResolveToIDOptionalAsync(
                        byID: model.UserID,
                        byKey: model.UserKey,
                        model: model.User,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
        }
    }
}
