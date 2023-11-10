// <autogenerated>
// <copyright file="SalesOrderWithNotesAssociationWorkflow.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Association Workflow classes for each table</summary>
// <remarks>This file was auto-generated by AssociationWorkflows.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
#nullable enable
// ReSharper disable ConvertIfStatementToNullCoalescingExpression, InvalidXmlDocComment
#pragma warning disable CS0618,CS1998
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Mapper;
    using Utilities;

    /// <summary>An Sales Order Notes association workflow.</summary>
    /// <seealso cref="AssociateObjectsWorkflowBase{ISalesOrderModel, ISalesOrder, INoteModel, INote, Note}"/>
    /// <seealso cref="ISalesOrderWithNotesAssociationWorkflow"/>
    public partial class SalesOrderWithNotesAssociationWorkflow
        // ReSharper disable once RedundantExtendsListEntry
        : AssociateNotesWorkflowBase<ISalesOrderModel, ISalesOrder, INoteModel, INote, Note>
        , ISalesOrderWithNotesAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override ICollection<Note>? GetObjectsCollection(ISalesOrder entity) { return entity.Notes; }

        /// <inheritdoc/>
        protected override List<INoteModel>? GetModelObjectsList(ISalesOrderModel model) { return model.Notes; }

        /// <inheritdoc/>
        protected override async Task AddObjectToObjectsListAsync(ISalesOrder entity, INote newEntity)
        {
            if (newEntity == null) { return; }
            entity.Notes!.Add((Note)newEntity);
        }

        /// <inheritdoc/>
        protected override void InitializeObjectListIfNull(ISalesOrder entity)
        {
            if (entity.Notes != null) { return; }
            entity.Notes = new HashSet<Note>();
        }

        /// <inheritdoc/>
        protected override async Task DeactivateObjectAsync(INote entity, DateTime timestamp)
        {
            // Hook-in to deactivate more custom property assignments
            await DeactivateObjectAdditionalPropertiesAsync(entity, timestamp).ConfigureAwait(false);
            // Deactivate this entity
            entity.Active = false;
            entity.UpdatedDate = timestamp;
        }

        private static int UpsertThisEntityTypeAndGetIDInner(
            string? name,
            string? key,
            ITypableBaseModel? model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            var entity = string.IsNullOrWhiteSpace(key) ? null : context.NoteTypes.FilterByActive(true).FilterByCustomKey(key, true).SingleOrDefault();
            if (entity == null) { entity = string.IsNullOrWhiteSpace(name) ? null : context.NoteTypes.FilterByActive(true).FilterByName(name, true).SingleOrDefault(); }
            if (entity == null && !string.IsNullOrWhiteSpace(model?.CustomKey)) { entity = context.NoteTypes.FilterByActive(true).FilterByName(model!.CustomKey, true).SingleOrDefault(); }
            if (entity == null && !string.IsNullOrWhiteSpace(model?.Name)) { entity = context.NoteTypes.FilterByActive(true).FilterByName(model!.Name, true).SingleOrDefault(); }
            if (entity != null) { return entity.ID; } // Return existing
            // Using a separate context here so we don't save a partially modified Store
            var newEntity = RegistryLoaderWrapper.GetInstance<INoteType>(context.ContextProfileName);
            newEntity.Active = true;
            newEntity.CreatedDate = timestamp;
            if (model != null)
            {
                newEntity.CustomKey = model.CustomKey;
                newEntity.Name = model.Name;
                newEntity.DisplayName = model.DisplayName;
                newEntity.SortOrder = model.SortOrder;
            }
            else
            {
                newEntity.CustomKey = key;
                newEntity.Name = name;
            }
            context.NoteTypes.Add((NoteType)newEntity);
            if (!context.SaveUnitOfWork(true))
            {
                throw new InvalidOperationException("Saving the Type failed.");
            }
            return newEntity.ID;
        }

        private static int UpsertThisEntityTypeAndGetID(
            string? name,
            string? key,
            ITypableBaseModel? model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(name, key, model);
            return UpsertThisEntityTypeAndGetIDInner(name, key, model, timestamp, context);
        }

        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabase(INoteModel model)
        {
            return model.Active /* == true */
                // == Note Properties =====
                    // Check the note content (required)
                    && !string.IsNullOrWhiteSpace(model.Note1)
                    // Check the Type is valid by TypeID/TypeKey on this model or ID/CustomKey on the Type model (required)
                    && (Contract.CheckValidIDOrKey(model.TypeID, model.TypeKey)
                        || model.Type != null && Contract.CheckValidIDOrKey(model.Type.ID, model.Type.CustomKey))
                    // No need to check the master (any of them) because we're assigning the master via this collection
                    // No need to check for Created/Updated By User (optional)
                // == Hook-in to make additional checks
                && ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAsync(INoteModel model, INote entity, IClarityEcommerceEntities context)
        {
            return model.CustomKey == entity.CustomKey
                && model.Hash == entity.Hash
                // == Hook-in to make additional checks
                && await MatchObjectModelWithObjectEntityAdditionalChecksAsync(model, entity, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<INote> ModelToNewObjectAsync(
            INoteModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ModelToNewObjectAsync(model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<INote> ModelToNewObjectAsync(
            INoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Validate
            Contract.RequiresNotNull(model);
            var thisEntityTypeID = Contract.CheckValidID(model.TypeID)
                ? model.TypeID
                : model.Type != null && Contract.CheckValidID(model.Type.ID)
                    ? model.Type.ID
                    : UpsertThisEntityTypeAndGetID(
                        model.TypeName,
                        model.TypeKey,
                        model.Type,
                        timestamp,
                        context);
            if (thisEntityTypeID <= 0) { throw new ArgumentException("Note requires a valid Type."); }
            // Create a new entity and populate it with data
            var newEntity = model.CreateNoteEntity(timestamp, context.ContextProfileName);
            newEntity.UpdatedDate = null; // Clear the Updated Date
            newEntity.TypeID = thisEntityTypeID;
            // Hook-in to add more custom property assignments
            await ModelToNewObjectAdditionalPropertiesAsync(newEntity, model, timestamp, context).ConfigureAwait(false);
            // Return the new entity, ready for adding to the DB
            return newEntity;
        }
    }
}
