// <autogenerated>
// <copyright file="SalesGroupWithSalesQuoteRequestSubsAssociationWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>An Sales Group Sales Quote Request Subs association workflow.</summary>
    /// <seealso cref="AssociateObjectsWorkflowBase{ISalesGroupModel, ISalesGroup, ISalesQuoteModel, ISalesQuote, SalesQuote}"/>
    /// <seealso cref="ISalesGroupWithSalesQuoteRequestSubsAssociationWorkflow"/>
    public partial class SalesGroupWithSalesQuoteRequestSubsAssociationWorkflow
        // ReSharper disable once RedundantExtendsListEntry
        : AssociateObjectsWorkflowBase<ISalesGroupModel, ISalesGroup, ISalesQuoteModel, ISalesQuote, SalesQuote>
        , ISalesGroupWithSalesQuoteRequestSubsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override ICollection<SalesQuote>? GetObjectsCollection(ISalesGroup entity) { return entity.SalesQuoteRequestSubs; }

        /// <inheritdoc/>
        protected override List<ISalesQuoteModel>? GetModelObjectsList(ISalesGroupModel model) { return model.SalesQuoteRequestSubs; }

        /// <inheritdoc/>
        protected override async Task AddObjectToObjectsListAsync(ISalesGroup entity, ISalesQuote newEntity)
        {
            if (newEntity == null) { return; }
            entity.SalesQuoteRequestSubs!.Add((SalesQuote)newEntity);
        }

        /// <inheritdoc/>
        protected override void InitializeObjectListIfNull(ISalesGroup entity)
        {
            if (entity.SalesQuoteRequestSubs != null) { return; }
            entity.SalesQuoteRequestSubs = new HashSet<SalesQuote>();
        }

        /// <inheritdoc/>
        protected override async Task DeactivateObjectAsync(ISalesQuote entity, DateTime timestamp)
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
            var entity = string.IsNullOrWhiteSpace(key) ? null : context.SalesQuoteTypes.FilterByActive(true).FilterByCustomKey(key, true).SingleOrDefault();
            if (entity == null) { entity = string.IsNullOrWhiteSpace(name) ? null : context.SalesQuoteTypes.FilterByActive(true).FilterByName(name, true).SingleOrDefault(); }
            if (entity == null && !string.IsNullOrWhiteSpace(model?.CustomKey)) { entity = context.SalesQuoteTypes.FilterByActive(true).FilterByName(model!.CustomKey, true).SingleOrDefault(); }
            if (entity == null && !string.IsNullOrWhiteSpace(model?.Name)) { entity = context.SalesQuoteTypes.FilterByActive(true).FilterByName(model!.Name, true).SingleOrDefault(); }
            if (entity != null) { return entity.ID; } // Return existing
            // Using a separate context here so we don't save a partially modified Store
            var newEntity = RegistryLoaderWrapper.GetInstance<ISalesQuoteType>(context.ContextProfileName);
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
            context.SalesQuoteTypes.Add((SalesQuoteType)newEntity);
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

        private static int UpsertThisEntityStatusAndGetIDInner(
            string? name,
            string? key,
            IStatusableBaseModel? model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            var entity = context.SalesQuoteStatuses.FilterByActive(true).FilterByCustomKey(key, true).SingleOrDefault();
            if (entity == null) { entity = context.SalesQuoteStatuses.FilterByActive(true).FilterByName(name, true).SingleOrDefault(); }
            if (entity == null && !string.IsNullOrWhiteSpace(model?.CustomKey)) { entity = context.SalesQuoteStatuses.FilterByActive(true).FilterByName(model!.CustomKey, true).SingleOrDefault(); }
            if (entity == null && !string.IsNullOrWhiteSpace(model?.Name)) { entity = context.SalesQuoteStatuses.FilterByActive(true).FilterByName(model!.Name, true).SingleOrDefault(); }
            if (entity != null) { return entity.ID; } // Return existing
            // Using a separate context here so we don't save a partially modified Store
            var newEntity = RegistryLoaderWrapper.GetInstance<ISalesQuoteStatus>(context.ContextProfileName);
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
            context.SalesQuoteStatuses.Add((SalesQuoteStatus)newEntity);
            if (!context.SaveUnitOfWork(true))
            {
                throw new InvalidOperationException("Saving the Type failed.");
            }
            return newEntity.ID;
        }

        private static int UpsertThisEntityStatusAndGetID(
            string? name,
            string? key,
            IStatusableBaseModel? model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(name, key, model);
            return UpsertThisEntityStatusAndGetIDInner(name, key, model, timestamp, context);
        }

        private static int UpsertThisEntityStateAndGetIDInner(
            string? name,
            string? key,
            IStateableBaseModel? model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            var entity = context.SalesQuoteStates.FilterByActive(true).FilterByCustomKey(key, true).SingleOrDefault();
            if (entity == null) { entity = context.SalesQuoteStates.FilterByActive(true).FilterByName(name, true).SingleOrDefault(); }
            if (entity == null && !string.IsNullOrWhiteSpace(model?.CustomKey)) { entity = context.SalesQuoteStates.FilterByActive(true).FilterByName(model!.CustomKey, true).SingleOrDefault(); }
            if (entity == null && !string.IsNullOrWhiteSpace(model?.Name)) { entity = context.SalesQuoteStates.FilterByActive(true).FilterByName(model!.Name, true).SingleOrDefault(); }
            if (entity != null) { return entity.ID; } // Return existing
            // Using a separate context here so we don't save a partially modified Store
            var newEntity = RegistryLoaderWrapper.GetInstance<ISalesQuoteState>(context.ContextProfileName);
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
            context.SalesQuoteStates.Add((SalesQuoteState)newEntity);
            if (!context.SaveUnitOfWork(true))
            {
                throw new InvalidOperationException("Saving the Type failed.");
            }
            return newEntity.ID;
        }

        private int UpsertThisEntityStateAndGetID(
            string? name,
            string? key,
            IStateableBaseModel? model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(name, key, model);
            return UpsertThisEntityStateAndGetIDInner(name, key, model, timestamp, context);
        }

        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabase(ISalesQuoteModel model)
        {
            return model.Active /* == true */
                // No additional default properties to check
                // == Hook-in to make additional checks
                && ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAsync(ISalesQuoteModel model, ISalesQuote entity, IClarityEcommerceEntities context)
        {
            return model.CustomKey == entity.CustomKey
                && model.Hash == entity.Hash
                // == Hook-in to make additional checks
                && await MatchObjectModelWithObjectEntityAdditionalChecksAsync(model, entity, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<ISalesQuote> ModelToNewObjectAsync(
            ISalesQuoteModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ModelToNewObjectAsync(model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<ISalesQuote> ModelToNewObjectAsync(
            ISalesQuoteModel model,
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
            if (thisEntityTypeID <= 0) { throw new ArgumentException("SalesQuote requires a valid Type."); }
            var thisEntityStatusID = Contract.CheckValidID(model.StatusID)
                ? model.StatusID
                : model.Status != null && Contract.CheckValidID(model.Status.ID)
                    ? model.Status.ID
                    : UpsertThisEntityStatusAndGetID(model.StatusName, model.StatusKey, model.Status, timestamp, context);
            if (thisEntityStatusID <= 0) { throw new ArgumentException("SalesQuote requires a valid Status."); }
            var thisEntityStateID = Contract.CheckValidID(model.StateID)
                ? model.StateID
                : model.State != null && Contract.CheckValidID(model.State.ID)
                    ? model.State.ID
                    : UpsertThisEntityStateAndGetID(model.StateName, model.StateKey, model.State, timestamp, context);
            if (thisEntityStateID <= 0) { throw new ArgumentException("SalesQuote requires a valid State."); }
            // Create a new entity and populate it with data
            var newEntity = model.CreateSalesQuoteEntity(timestamp, context.ContextProfileName);
            newEntity.UpdatedDate = null; // Clear the Updated Date
            newEntity.TypeID = thisEntityTypeID;
            newEntity.StatusID = thisEntityStatusID;
            newEntity.StateID = thisEntityStateID;
            // Hook-in to add more custom property assignments
            await ModelToNewObjectAdditionalPropertiesAsync(newEntity, model, timestamp, context).ConfigureAwait(false);
            // Return the new entity, ready for adding to the DB
            return newEntity;
        }
    }
}
