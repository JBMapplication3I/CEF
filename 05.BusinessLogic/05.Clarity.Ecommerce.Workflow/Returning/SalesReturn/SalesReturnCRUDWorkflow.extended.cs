// <copyright file="SalesReturnCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class SalesReturnWorkflow
    {
        /// <summary>Identifier for the private note type.</summary>
        private static int? privateNoteTypeID;

        /// <inheritdoc/>
        public override Task<ISalesReturnModel?> GetAsync(int id, string? contextProfileName)
        {
            return GetAsync(id, null, contextProfileName);
        }

        /// <inheritdoc/>
        public override Task<ISalesReturnModel?> GetAsync(string key, string? contextProfileName)
        {
            return GetAsync(null, key, contextProfileName);
        }

        /// <inheritdoc/>
        public override Task<IEnumerable<ISalesReturnModel>> SearchForConnectAsync(
            ISalesReturnSearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.SalesReturns
                    .AsNoTracking()
                    .FilterByActive(search.Active)
                    .FilterSalesReturnBySearchModel(search)
                    .OrderByDescending(so => so.CreatedDate)
                    .SelectLiteSalesReturnAndMapToSalesReturnModel(contextProfileName));
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> UpdateAsync(
            ISalesReturnModel salesReturn,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(salesReturn);
            Contract.RequiresValidIDOrKey(salesReturn.ID, salesReturn.CustomKey);
            var entity = Contract.CheckValidID(salesReturn.ID)
                ? await context.SalesReturns.FilterByID(salesReturn.ID).SingleOrDefaultAsync()
                : null;
            if (entity == null && Contract.CheckValidKey(salesReturn.CustomKey))
            {
                entity = await context.SalesReturns
                    .FilterByCustomKey(salesReturn.CustomKey, true)
                    .OrderByDescending(x => x.Active)
                    .FirstOrDefaultAsync();
            }
            if (entity == null)
            {
                throw new ArgumentException("Must supply an ID or CustomKey that matches an existing record");
            }
            if (entity.CustomKey != salesReturn.CustomKey)
            {
                // This will throw if it finds another entity with this model's key
                await DuplicateCheckAsync(salesReturn, context).ConfigureAwait(false);
            }
            var previousRefundAmount = entity.RefundAmount;
            var previousCustomKey = entity.CustomKey;
            var timestamp = DateExtensions.GenDateTime;
            entity.Active = salesReturn.Active;
            entity.CustomKey = salesReturn.CustomKey;
            entity.UpdatedDate = timestamp;
            salesReturn.UpdatedDate = timestamp;
            entity.RefundAmount = salesReturn.RefundAmount;
            // For keeping track of modification
            var noteDescription = string.Empty;
            if (salesReturn.CustomKey != previousCustomKey)
            {
                noteDescription += $" Custom Key from {previousRefundAmount:c2} to {salesReturn.RefundAmount:c2}.";
            }
            if (salesReturn.RefundAmount != previousRefundAmount)
            {
                noteDescription += $" Refund Amount from {previousRefundAmount:c2} to {salesReturn.RefundAmount:c2}.";
            }
            if (!string.IsNullOrEmpty(noteDescription))
            {
                var tmpNotesList = salesReturn.Notes?.Cast<NoteModel>().ToList() ?? new List<NoteModel>();
                privateNoteTypeID ??= await Workflows.NoteTypes.ResolveToIDAsync(
                        byID: null,
                        byKey: "Private Note",
                        byName: "Private Note",
                        byDisplayName: "Private Note",
                        model: null,
                        contextProfileName: context.ContextProfileName)
                    .ConfigureAwait(false);
                tmpNotesList.Add(new()
                {
                    Active = true,
                    CreatedDate = timestamp,
                    TypeID = privateNoteTypeID.Value,
                    CreatedByUserID = salesReturn.UserID,
                    SalesReturnID = salesReturn.ID,
                    Note1 = $"User ID: {salesReturn.UserID} changed {noteDescription.Trim()}",
                });
                salesReturn.Notes = tmpNotesList.Cast<INoteModel>().ToList();
            }
            await AssignAdditionalPropertiesAsync(entity, salesReturn, timestamp, context).ConfigureAwait(false);
            // Deactivate previous RMAs
            ////foreach (var entitySalesItem in entity.SalesItems)
            ////{
            ////    entitySalesItem.Active = false;
            ////    entitySalesItem.UpdatedDate = timestamp;
            ////}
            // Create new RMAs
            ////var salesOrder = SalesOrderWorkflow.Get(salesReturn.SalesOrderIDs.First());
            ////var salesOrder = SalesOrderWorkflow.Get(salesReturn.AssociatedSalesOrders.First().SalesOrderID);
            ////foreach (var salesReturnSalesItem in salesReturn.SalesItems)//.Where(x => x.ID == null))
            ////{
            ////    var salesOrderItemModel = salesOrder.SalesItems.SingleOrDefault(x => x.ProductID == salesReturnSalesItem.ProductID);
            ////    AddSalesReturnItems(timestamp, entity, salesReturnSalesItem, salesOrderItemModel, true);
            ////}
            if (!await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
            {
                throw new InvalidDataException("Something about updating this object and saving it failed");
            }
            return entity.ID.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            ISalesReturn entity,
            ISalesReturnModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await base.AssignAdditionalPropertiesAsync(entity, model, timestamp, context).ConfigureAwait(false);
            // SalesReturn Properties
            entity.BalanceDue = model.BalanceDue;
            entity.PurchaseOrderNumber = model.PurchaseOrderNumber;
            entity.TrackingNumber = model.TrackingNumber;
            // Related Objects
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            // Associated Objects
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context.ContextProfileName).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            if (model.Notes != null)
            {
                if (Contract.CheckValidID(entity.ID))
                {
                    foreach (var note in model.Notes)
                    {
                        note.SalesReturnID = entity.ID;
                    }
                }
                await Workflows.SalesReturnWithNotesAssociation.AssociateObjectsAsync((SalesReturn)entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            }
#pragma warning disable SA1501,format // Statement should not be on a single line
            if (model.SalesItems != null) { await AssociateSalesItemsAsync(model, entity, timestamp, context.ContextProfileName).ConfigureAwait(false); }
            if (model.SalesReturnPayments != null) { await Workflows.SalesReturnWithSalesReturnPaymentsAssociation.AssociateObjectsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false); }
            if (model.Contacts != null) { await Workflows.SalesReturnWithContactsAssociation.AssociateObjectsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false); }
#pragma warning restore SA1501,format // Statement should not be on a single line
        }

        /// <summary>Converts this SalesReturnWorkflow to the sales return item.</summary>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="model">    The model.</param>
        /// <returns>The given data converted to the sales return item.</returns>
        private static SalesReturnItem ConvertToSalesReturnItem(
            DateTime timestamp,
            ISalesItemBaseModel<IAppliedSalesReturnItemDiscountModel> model)
        {
            return new()
            {
                // Base Properties
                Active = true,
                CreatedDate = timestamp,
                CustomKey = model.CustomKey,
                JsonAttributes = model.SerializableAttributes.SerializeAttributesDictionary(),
                // NameableBase Properties
                Name = model.ProductName,
                Description = model.ProductDescription,
                // SalesItemBase Properties
                Quantity = model.Quantity,
                QuantityBackOrdered = model.QuantityBackOrdered ?? 0m,
                QuantityPreSold = model.QuantityPreSold ?? 0m,
                UnitCorePrice = model.UnitCorePrice,
                UnitSoldPrice = model.UnitSoldPrice,
                Sku = model.Sku,
                ForceUniqueLineItemKey = model.ForceUniqueLineItemKey,
                UnitOfMeasure = model.UnitOfMeasure,
                // Related Objects
                UserID = model.UserID,
                ProductID = model.ProductID,
                // Associated Objects
                Discounts = model.Discounts
                    ?.Where(x => x.Active)
                    .Select(x => new AppliedSalesReturnItemDiscount
                    {
                        Active = true,
                        CreatedDate = timestamp,
                        CustomKey = x.CustomKey,
                        SlaveID = x.ID,
                    })
                    .ToList(),
            };
        }

        /// <summary>Gets a sales return by its identifier.</summary>
        /// <param name="id">                The identifier to get.</param>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An ISalesReturnModel.</returns>
        // ReSharper disable once CyclomaticComplexity, FunctionComplexityOverflow
        private async Task<ISalesReturnModel?> GetAsync(int? id, string? key, string? contextProfileName)
        {
            Contract.CheckValidIDOrKey(id, key);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var salesReturn = Contract.CheckValidID(id)
                ? context.SalesReturns
                    .AsNoTracking()
                    .FilterByID(id)
                    .SelectSingleFullSalesReturnAndMapToSalesReturnModel(contextProfileName)
                : context.SalesReturns
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(key, true)
                    .SelectSingleFullSalesReturnAndMapToSalesReturnModel(contextProfileName);
            // ReSharper disable once InvertIf
            if (salesReturn?.SalesItems != null)
            {
                foreach (var salesItem in salesReturn.SalesItems)
                {
                    if (!Contract.CheckValidID(salesItem.ProductID) || context.Products == null)
                    {
                        continue;
                    }
                    salesItem.ProductDownloadsNew = await context.Products
                        .Where(x => x.ID == salesItem.ProductID && x.StoredFiles!.Any())
                        .SelectMany(x => x.StoredFiles!)
                        .Where(pf => pf.Slave != null && pf.Slave.FileName != null)
                        .Select(pf => pf.Slave!.FileName!)
                        .ToListAsync()
                        .ConfigureAwait(false);
                }
            }
            if (salesReturn?.Contacts == null)
            {
                return salesReturn;
            }
            foreach (var salesReturnContactModel in salesReturn.Contacts)
            {
                salesReturnContactModel.Contact = (await Workflows.Contacts.ResolveAsync(
                            byID: salesReturnContactModel.ContactID,
                            byKey: salesReturnContactModel.ContactKey,
                            model: salesReturnContactModel.Contact,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false))
                    .Result;
            }
            return salesReturn;
        }

        /// <summary>Associate sales items.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task AssociateSalesItemsAsync(
            ISalesReturnModel model,
            ISalesReturn entity,
            DateTime timestamp,
            string? contextProfileName)
        {
            // TODO@JTG: Use the new associate process so we don't wipe and re-add
            // Remove current Items
            foreach (var item in Contract.RequiresNotEmpty(entity.SalesItems))
            {
                item.UpdatedDate = timestamp;
                item.Active = false;
            }
            if (model.SalesItems == null)
            {
                return;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var salesItem in model.SalesItems.Where(x => Contract.CheckValidIDOrAnyValidKey(x.ProductID, x.ProductKey, x.Sku)))
            {
                var product = Contract.CheckValidID(salesItem.ProductID)
                    ? await context.Products
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByID(salesItem.ProductID)
                        .Select(x => (int?)x.ID)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false)
                    : Contract.CheckValidKey(salesItem.ProductKey ?? salesItem.Sku)
                        ? await context.Products
                            .AsNoTracking()
                            .FilterByActive(true)
                            .FilterByCustomKey(salesItem.ProductKey ?? salesItem.Sku)
                            .Select(x => (int?)x.ID)
                            .SingleOrDefaultAsync()
                            .ConfigureAwait(false)
                        : null;
                if (product == null)
                {
                    // Create a new invisible product to show legacy data. Product Managers can update the content later
                    var newID = await Workflows.Products.CreateLegacyProductWithKeyAsync(
                            Contract.RequiresValidKey(salesItem.ProductKey ?? salesItem.Sku),
                            Contract.RequiresValidKey(salesItem.ProductName ?? salesItem.Sku),
                            contextProfileName)
                        .ConfigureAwait(false);
                    product = await context.Products
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByID(newID)
                        .Select(x => (int?)x.ID)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);
                    if (product == null)
                    {
                        continue;
                    }
                }
                entity.SalesItems!.Add(ConvertToSalesReturnItem(timestamp, salesItem));
            }
        }
    }
}
