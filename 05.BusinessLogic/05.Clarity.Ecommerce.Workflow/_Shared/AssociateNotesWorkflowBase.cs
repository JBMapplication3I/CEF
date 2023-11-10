// <copyright file="AssociateNotesWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate notes workflow base class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    /// <summary>An associate notes workflow base.</summary>
    /// <typeparam name="TIMasterModel"> Type of the ti master model.</typeparam>
    /// <typeparam name="TIMasterEntity">Type of the ti master entity.</typeparam>
    /// <typeparam name="TISlaveModel">  Type of the ti slave model.</typeparam>
    /// <typeparam name="TISlaveEntity"> Type of the ti slave entity.</typeparam>
    /// <typeparam name="TSlaveEntity">  Type of the slave entity.</typeparam>
    /// <seealso cref="AssociateObjectsWorkflowBase{TIMasterModel, TIMasterEntity, TISlaveModel, TISlaveEntity, TSlaveEntity}"/>
    public abstract class AssociateNotesWorkflowBase<TIMasterModel, TIMasterEntity, TISlaveModel, TISlaveEntity, TSlaveEntity>
        : AssociateObjectsWorkflowBase<TIMasterModel, TIMasterEntity, TISlaveModel, TISlaveEntity, TSlaveEntity>
        where TIMasterModel : IBaseModel, IHaveNotesBaseModel
        where TIMasterEntity : IBase, IHaveNotesBase
        where TISlaveModel : IBaseModel, INoteModel
        where TISlaveEntity : IBase, INote
        where TSlaveEntity : class, TISlaveEntity, new()
    {
        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            TISlaveModel model,
            TISlaveEntity entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                entity.Note1 == model.Note1
                && entity.TypeID == model.TypeID
                && entity.CreatedByUserID == model.CreatedByUserID
                && entity.UpdatedByUserID == model.UpdatedByUserID
                && entity.AccountID == model.AccountID
                && entity.UserID == model.UserID
                && entity.VendorID == model.VendorID
                && entity.ManufacturerID == model.ManufacturerID
                && entity.StoreID == model.StoreID
                && entity.BrandID == model.BrandID
                && entity.SalesGroupID == model.SalesGroupID
                && entity.PurchaseOrderID == model.PurchaseOrderID
                && entity.SalesOrderID == model.SalesOrderID
                && entity.SalesReturnID == model.SalesReturnID
                && entity.SalesQuoteID == model.SalesQuoteID
                && entity.SampleRequestID == model.SampleRequestID
                && entity.CartID == model.CartID
                && entity.PurchaseOrderItemID == model.PurchaseOrderItemID
                && entity.SalesOrderItemID == model.SalesOrderItemID
                && entity.SalesReturnItemID == model.SalesReturnItemID
                && entity.SalesInvoiceItemID == model.SalesInvoiceItemID
                && entity.SalesQuoteItemID == model.SalesQuoteItemID
                && entity.SampleRequestItemID == model.SampleRequestItemID
                && entity.CartItemID == model.CartItemID);
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            TISlaveEntity newEntity,
            TISlaveModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(newEntity);
            model.ID = 0; // Clear the ID so we can create as new
            var newEntityAlt = (await Workflows.Notes.CreateEntityWithoutSavingAsync(
                        Contract.RequiresNotNull(model),
                        timestamp,
                        context.ContextProfileName)
                    .ConfigureAwait(false))
                .Result;
            // Base Properties
            newEntity.Active = model.Active;
            newEntity.CustomKey = model.CustomKey;
            newEntity.CreatedDate = model.CreatedDate;
            newEntity.UpdatedDate = model.UpdatedDate;
            newEntity.Hash = model.Hash;
            newEntity.JsonAttributes = newEntityAlt!.SerializableAttributes.SerializeAttributesDictionary();
            // Note Properties
            newEntity.Note1 = model.Note1;
            // IHaveATypeBase Properties
            newEntity.TypeID = await Workflows.NoteTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: model.TypeID,
                    byKey: model.TypeKey,
                    byName: model.TypeName,
                    byDisplayName: model.TypeDisplayName,
                    model: model.Type,
                    contextProfileName: context.ContextProfileName)
                .ConfigureAwait(false);
            // Related Objects
            newEntity.CreatedByUserID = newEntityAlt.CreatedByUserID;
            newEntity.UpdatedByUserID = newEntityAlt.UpdatedByUserID;
            newEntity.AccountID = newEntityAlt.AccountID;
            newEntity.UserID = newEntityAlt.UserID;
            newEntity.VendorID = newEntityAlt.VendorID;
            newEntity.ManufacturerID = newEntityAlt.ManufacturerID;
            newEntity.SalesGroupID = newEntityAlt.SalesGroupID;
            newEntity.PurchaseOrderID = newEntityAlt.PurchaseOrderID;
            newEntity.SalesOrderID = newEntityAlt.SalesOrderID;
            newEntity.SalesReturnID = newEntityAlt.SalesReturnID;
            newEntity.SalesQuoteID = newEntityAlt.SalesQuoteID;
            newEntity.SampleRequestID = newEntityAlt.SampleRequestID;
            newEntity.CartID = newEntityAlt.CartID;
            newEntity.PurchaseOrderItemID = newEntityAlt.PurchaseOrderItemID;
            newEntity.SalesOrderItemID = newEntityAlt.SalesOrderItemID;
            newEntity.SalesReturnItemID = newEntityAlt.SalesReturnItemID;
            newEntity.SalesInvoiceItemID = newEntityAlt.SalesInvoiceItemID;
            newEntity.SalesQuoteItemID = newEntityAlt.SalesQuoteItemID;
            newEntity.SampleRequestItemID = newEntityAlt.SampleRequestItemID;
            newEntity.CartItemID = newEntityAlt.CartItemID;
        }
    }
}
