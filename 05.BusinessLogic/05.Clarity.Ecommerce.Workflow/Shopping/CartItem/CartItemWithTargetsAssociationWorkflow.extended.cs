// <copyright file="CartItemWithTargetsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate cart item targets workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using JSConfigs;
    using Models;
    using Utilities;

    public partial class CartItemWithTargetsAssociationWorkflow
    {
        /// <summary>Resolve destination contact.</summary>
        /// <param name="newEntity">The new entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <param name="workflows">The workflows.</param>
        /// <returns>A Task.</returns>
        public static async Task<int> ResolveDestinationContactAsync(
            ISalesItemTargetBase? newEntity,
            ISalesItemTargetBaseModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context,
            IWorkflowsController workflows)
        {
            var contactID = Contract.CheckValidID(model.DestinationContactID)
                ? model.DestinationContactID
                : Contract.CheckValidID(model.DestinationContact?.ID)
                    ? model.DestinationContact!.ID
                    : new[] { -2, -3 }.Contains(model.DestinationContactID)
                        ? model.DestinationContactID
                        : 0;
            switch (contactID)
            {
                case -3:
                {
                    // Ship to Store
                    var resolvedID = await workflows.Contacts.ResolveWithAutoGenerateToIDOptionalAsync(
                            byID: null,
                            byKey: "ShipToStore",
                            model: new ContactModel { Active = true, CreatedDate = timestamp, CustomKey = "ShipToStore" },
                            context: context)
                        .ConfigureAwait(false);
                    var id = Contract.RequiresValidID(resolvedID, "ERROR! Unable to resolve destination contact");
                    if (newEntity != null)
                    {
                        newEntity.DestinationContactID = id;
                    }
                    return id;
                }
                case -2:
                {
                    // In Store Pickup
                    var resolvedID = await workflows.Contacts.ResolveWithAutoGenerateToIDOptionalAsync(
                            byID: null,
                            byKey: "InStorePickup",
                            model: new ContactModel { Active = true, CreatedDate = timestamp, CustomKey = "InStorePickup" },
                            context: context)
                        .ConfigureAwait(false);
                    var id = Contract.RequiresValidID(resolvedID, "ERROR! Unable to resolve destination contact");
                    if (newEntity != null)
                    {
                        newEntity.DestinationContactID = id;
                    }
                    return id;
                }
                case -1:
                {
                    throw new InvalidOperationException(
                        "ERROR: Cannot add an address via this method, the address should have already been saved and"
                        + " applied with the ID to this object");
                }
                case 0 when Contract.CheckAnyValidKey(model.DestinationContactKey, model.DestinationContact?.CustomKey):
                {
                    // We didn't get the ID, but we have a Key we can try to use
                    var id = await workflows.Contacts.ResolveToIDOptionalAsync(
                            byID: model.DestinationContactID,
                            byKey: model.DestinationContactKey,
                            model: model.DestinationContact,
                            context: context)
                        .ConfigureAwait(false)
                        ?? 0;
                    if (Contract.CheckInvalidID(id))
                    {
                        throw new ArgumentException("Unable to resolve destination contact");
                    }
                    var addedID = await TryNewContactAsync(model, context, workflows).ConfigureAwait(false);
                    id = Contract.RequiresValidID(addedID, "ERROR! Unable to resolve destination contact");
                    if (newEntity != null)
                    {
                        newEntity.DestinationContactID = id;
                    }
                    return id;
                }
                case 0 when model.DestinationContactID < -3 && model.DestinationContact != null:
                {
                    var addedID = await TryNewContactAsync(model, context, workflows).ConfigureAwait(false);
                    var id = Contract.RequiresValidID(addedID, "ERROR! Unable to resolve destination contact");
                    if (newEntity != null)
                    {
                        newEntity.DestinationContactID = id;
                    }
                    return id;
                }
                case 0:
                {
                    // No Value
                    throw new ArgumentException("Unable to resolve destination contact");
                }
                default:
                {
                    // Any other positive ID value
                    var resolvedID = await workflows.Contacts.ResolveToIDOptionalAsync(
                            byID: model.DestinationContactID,
                            byKey: model.DestinationContactKey,
                            model: model.DestinationContact,
                            context: context)
                        .ConfigureAwait(false);
                    var id = Contract.RequiresValidID(resolvedID, "ERROR! Unable to resolve destination contact");
                    if (newEntity != null)
                    {
                        newEntity.DestinationContactID = id;
                    }
                    return id;
                }
            }
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            ISalesItemTargetBaseModel model,
            ICartItemTarget entity,
            IClarityEcommerceEntities context)
        {
            return entity.Quantity == model.Quantity
                && entity.NothingToShip == model.NothingToShip
                && entity.DestinationContactID == model.DestinationContactID
                // && entity.TypeID == model.TypeID // This gets blanked by the UI often
                && entity.Type!.CustomKey == (model.TypeKey ?? model.Type?.CustomKey) // This gets set when TypeID is blanked
                && entity.SelectedRateQuoteID == model.SelectedRateQuoteID
                && entity.JsonAttributes == model.SerializableAttributes.SerializeAttributesDictionary();
            /*
            * TODO: Smarter checks for the ShipToStore and InStorePickup so they don't think it has changed when it
            * is pointing at the contact record for it
            */
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            ICartItemTarget newEntity,
            ISalesItemTargetBaseModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Type is already handled by this point
            // Lookup and assign the Contact if possible, fail if not
            await ResolveDestinationContactAsync(newEntity, model, timestamp, context, Workflows).ConfigureAwait(false);
            // Lookup and assign the PILS if possible, ignore if not
            await ResolveOriginProductInventoryLocationSectionAsync(newEntity, model, context).ConfigureAwait(false);
            // Lookup and assign the store if possible, ignore if not
            await ResolveOriginStoreAsync(newEntity, model, context).ConfigureAwait(false);
            // Lookup and assign the vendor if possible, ignore if not
            await ResolveOriginVendorAsync(newEntity, model, context).ConfigureAwait(false);
            // Lookup and assign the selected rate quote if possible, ignore if not
            await ResolveSelectedRateQuoteAsync(newEntity, model, context).ConfigureAwait(false);
            newEntity.Quantity = model.Quantity;
            // Generate a Hash if there isn't one
            if (newEntity.Hash.HasValue)
            {
                return;
            }
            // var clone = JsonConvert.DeserializeObject<DataModel.CartItemTarget>(
            //     JsonConvert.SerializeObject(newEntity, SerializableAttributesDictionaryExtensions.JsonSettings),
            //     SerializableAttributesDictionaryExtensions.JsonSettings);
            var clone = (ICartItemTarget)newEntity.Clone();
            clone.Type = Contract.CheckValidID(clone.TypeID)
                ? await context.SalesItemTargetTypes.AsNoTracking().FilterByID(clone.TypeID).SingleAsync()
                : clone.Type;
            clone.DestinationContact = Contract.CheckValidID(clone.DestinationContactID)
                ? await context.Contacts
                    .Include(x => x.Address)
                    .Include(x => x.Address!.Region)
                    .Include(x => x.Address!.Region!.Country)
                    .Include(x => x.Address!.Country)
                    .AsNoTracking()
                    .FilterByID(clone.DestinationContactID)
                    .SingleAsync()
                : clone.DestinationContact;
            clone.SelectedRateQuote = Contract.CheckValidID(clone.SelectedRateQuoteID)
                ? await context.RateQuotes.AsNoTracking().FilterByID(clone.SelectedRateQuoteID).SingleAsync()
                : clone.SelectedRateQuote;
            if (CEFConfigDictionary.InventoryAdvancedEnabled)
            {
                clone.OriginProductInventoryLocationSection =
                    Contract.CheckValidID(clone.OriginProductInventoryLocationSectionID)
                        ? await context.ProductInventoryLocationSections.AsNoTracking()
                            .FilterByID(clone.OriginProductInventoryLocationSectionID)
                            .SingleAsync()
                        : clone.OriginProductInventoryLocationSection;
            }
            if (CEFConfigDictionary.StoresEnabled)
            {
                clone.OriginStoreProduct = Contract.CheckValidID(clone.OriginStoreProductID)
                    ? await context.StoreProducts.AsNoTracking().FilterByID(clone.OriginStoreProductID).SingleAsync()
                    : clone.OriginStoreProduct;
            }
            if (CEFConfigDictionary.VendorsEnabled)
            {
                clone.OriginVendorProduct = Contract.CheckValidID(clone.OriginVendorProductID)
                    ? await context.VendorProducts.AsNoTracking().FilterByID(clone.OriginVendorProductID).SingleAsync()
                    : clone.OriginVendorProduct;
            }
            newEntity.Hash = Digest.Crc64(clone.ToHashableString());
        }

        /// <summary>Try new address.</summary>
        /// <param name="model">    The model.</param>
        /// <param name="context">  The context.</param>
        /// <param name="workflows">The workflows.</param>
        /// <returns>A Task{IContactModel}.</returns>
        private static async Task<int?> TryNewContactAsync(
            ISalesItemTargetBaseModel model,
            IClarityEcommerceEntities context,
            IWorkflowsController workflows)
        {
            // New address for guest checkout, we have to just save it to the contacts table as we don't have
            // an address book to work with
            var newContact = await workflows.Contacts.CreateAsync(model.DestinationContact!, context).ConfigureAwait(false);
            var id = newContact.Result;
            _ = Contract.RequiresValidID(id, "ERROR! Guest Checkout was unable to save the destination contact");
            return newContact.Result;
        }

        private async Task ResolveSelectedRateQuoteAsync(
            ISalesItemTargetBase newEntity,
            ISalesItemTargetBaseModel model,
            IClarityEcommerceEntities context)
        {
            if (Contract.CheckValidIDOrAnyValidKey(
                model.SelectedRateQuoteID ?? model.SelectedRateQuote?.ID,
                model.SelectedRateQuoteKey,
                model.SelectedRateQuoteName,
                model.SelectedRateQuote?.CustomKey,
                model.SelectedRateQuote?.Name))
            {
                var resolvedID = await Workflows.RateQuotes.ResolveToIDOptionalAsync(
                        byID: model.SelectedRateQuoteID,
                        byKey: model.SelectedRateQuoteKey,
                        byName: model.SelectedRateQuoteName,
                        model: model.SelectedRateQuote,
                        context: context)
                    .ConfigureAwait(false);
                if (Contract.CheckInvalidID(resolvedID))
                {
                    newEntity.SelectedRateQuoteID = null;
                    return;
                }
                newEntity.SelectedRateQuoteID = resolvedID;
                return;
            }
            newEntity.SelectedRateQuoteID = null;
        }

        private Task ResolveOriginStoreAsync(
            ISalesItemTargetBase newEntity,
            ISalesItemTargetBaseModel model,
            IClarityEcommerceEntities context)
        {
            ////if (!CEFConfigDictionary.StoresEnabled)
            ////{
            ////    // Don't assign a value, they are disabled
            ////    newEntity.OriginStoreProductID = null;
            ////    return;
            ////}
            ////if (Contract.CheckValidIDOrAnyValidKey(
            ////    model.OriginStoreProductID ?? model.OriginStoreProduct?.ID,
            ////    model.OriginStoreProductKey,
            ////    model.OriginStoreProduct?.CustomKey))
            ////{
            ////    var resolvedID = await Workflows.StoreProducts.ResolveToIDOptionalAsync(
            ////            byID: model.OriginStoreProductID,
            ////            byKey: model.OriginStoreProductKey,
            ////            model: model.OriginStoreProduct,
            ////            context: context)
            ////        .ConfigureAwait(false);
            ////    if (Contract.CheckInvalidID(resolvedID))
            ////    {
            ////        newEntity.OriginStoreProductID = null;
            ////        return;
            ////    }
            ////    newEntity.OriginStoreProductID = resolvedID;
            ////    return;
            ////}
            ////newEntity.OriginStoreProductID = null;
            return Task.CompletedTask;
        }

        private Task ResolveOriginVendorAsync(
            ISalesItemTargetBase newEntity,
            ISalesItemTargetBaseModel model,
            IClarityEcommerceEntities context)
        {
            ////if (!CEFConfigDictionary.VendorsEnabled)
            ////{
            ////    // Don't assign a value, they are disabled
            ////    newEntity.OriginVendorProductID = null;
            ////    return;
            ////}
            ////if (Contract.CheckValidIDOrAnyValidKey(
            ////    model.OriginVendorProductID ?? model.OriginVendorProduct?.ID,
            ////    model.OriginVendorProductKey,
            ////    model.OriginVendorProduct?.CustomKey))
            ////{
            ////    var resolvedID = await Workflows.VendorProducts.ResolveToIDOptionalAsync(
            ////            byID: model.OriginVendorProductID,
            ////            byKey: model.OriginVendorProductKey,
            ////            model: model.OriginVendorProduct,
            ////            context: context)
            ////        .ConfigureAwait(false);
            ////    if (Contract.CheckInvalidID(resolvedID))
            ////    {
            ////        newEntity.OriginVendorProductID = null;
            ////        return;
            ////    }
            ////    newEntity.OriginVendorProductID = resolvedID;
            ////    return;
            ////}
            ////newEntity.OriginVendorProductID = null;
            return Task.CompletedTask;
        }

        private Task ResolveOriginProductInventoryLocationSectionAsync(
            ISalesItemTargetBase newEntity,
            ISalesItemTargetBaseModel model,
            IClarityEcommerceEntities context)
        {
            ////if (!CEFConfigDictionary.InventoryAdvancedEnabled)
            ////{
            ////    // Don't assign a value, they are disabled
            ////    newEntity.OriginProductInventoryLocationSectionID = null;
            ////    return;
            ////}
            ////if (Contract.CheckValidIDOrAnyValidKey(
            ////    model.OriginProductInventoryLocationSectionID ?? model.OriginProductInventoryLocationSection?.ID,
            ////    model.OriginProductInventoryLocationSectionKey,
            ////    model.OriginProductInventoryLocationSection?.CustomKey))
            ////{
            ////    var resolvedID = await Workflows.ProductInventoryLocationSections.ResolveToIDOptionalAsync(
            ////            byID: model.OriginProductInventoryLocationSectionID,
            ////            byKey: model.OriginProductInventoryLocationSectionKey,
            ////            model: model.OriginProductInventoryLocationSection,
            ////            context: context)
            ////        .ConfigureAwait(false);
            ////    if (Contract.CheckInvalidID(resolvedID))
            ////    {
            ////        newEntity.OriginProductInventoryLocationSectionID = null;
            ////        return;
            ////    }
            ////    newEntity.OriginProductInventoryLocationSectionID = resolvedID;
            ////    return;
            ////}
            ////newEntity.OriginProductInventoryLocationSectionID = null;
            return Task.CompletedTask;
        }
    }
}
