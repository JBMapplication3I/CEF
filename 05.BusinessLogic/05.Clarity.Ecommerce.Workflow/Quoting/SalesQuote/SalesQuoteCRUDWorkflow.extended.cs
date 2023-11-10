// <copyright file="SalesQuoteCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class SalesQuoteWorkflow
    {
        /// <inheritdoc/>
        public override Task<ISalesQuoteModel?> GetAsync(int id, string? contextProfileName)
        {
            return GetAsync(id, null, contextProfileName);
        }

        /// <inheritdoc/>
        public override Task<ISalesQuoteModel?> GetAsync(string key, string? contextProfileName)
        {
            return GetAsync(null, key, contextProfileName);
        }

        /// <inheritdoc/>
        public override Task<IEnumerable<ISalesQuoteModel>> SearchForConnectAsync(
            ISalesQuoteSearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.SalesQuotes
                    .AsNoTracking()
                    .FilterByActive(search.Active)
                    .FilterSalesQuotesBySearchModel(search)
                    .FilterSalesQuotesByHasSalesGroupAsRequest(search.HasSalesGroupAsMaster)
                    .FilterSalesQuotesByHasSalesGroupAsResponse(search.HasSalesGroupAsResponse)
                    .FilterSalesQuotesByCategoryIDs(search.CategoryIDs)
                    .OrderByDescending(so => so.CreatedDate)
                    .SelectLiteSalesQuoteAndMapToSalesQuoteModel(contextProfileName));
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            ISalesQuote entity,
            ISalesQuoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await base.AssignAdditionalPropertiesAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RunLimitedAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<SalesQuote>> FilterQueryByModelCustomAsync(
            IQueryable<SalesQuote> query,
            ISalesQuoteSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelCustomAsync(query, search, context).ConfigureAwait(false))
                .FilterSalesQuotesByHasSalesGroupAsMaster(search.HasSalesGroupAsMaster)
                .FilterSalesQuotesByHasSalesGroupAsSub(search.HasSalesGroupAsSub)
                .FilterSalesQuotesByHasSalesGroupAsRequest(search.HasSalesGroupAsRequest)
                .FilterSalesQuotesByHasSalesGroupAsResponse(search.HasSalesGroupAsResponse);
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            SalesQuote? entity,
            IClarityEcommerceEntities context)
        {
            if (entity is null)
            {
                return CEFAR.PassingCEFAR();
            }
            // ReSharper disable once InvertIf
            if (context.Notes != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Notes.Count(x => x.SalesQuoteID == entity.ID);)
                {
                    context.Notes.Remove(context.Notes.First(x => x.SalesQuoteID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }

        /// <summary>Gets a sales quote by its identifier or key.</summary>
        /// <param name="id">                The identifier to get.</param>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An ISalesQuoteModel.</returns>
        private static Task<ISalesQuoteModel?> GetAsync(int? id, string? key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.SalesQuotes
                    .AsNoTracking()
                    .FilterByActive(Contract.CheckValidID(id) ? null : true)
                    .FilterByID(id)
                    .FilterByCustomKey(key, true)
                    .SelectSingleFullSalesQuoteAndMapToSalesQuoteModel(contextProfileName));
        }

        private async Task RunLimitedRelateWorkflowsAsync(
            ISalesQuote entity,
            ISalesQuoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await RelateOptionalSalesGroupAsRequestMasterAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalSalesGroupAsRequestSubAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalSalesGroupAsResponseMasterAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalSalesGroupAsResponseSubAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        private async Task RelateOptionalSalesGroupAsRequestMasterAsync(
            ISalesQuote entity,
            ISalesQuoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolved = await Workflows.SalesGroups.ResolveAsync(
                    model.SalesGroupAsRequestMasterID, // By Other ID
                    model.SalesGroupAsRequestMasterKey, // By Flattened Other Key
                    model.SalesGroupAsRequestMaster,
                    context,
                    isInner: true)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.SalesGroupAsRequestMasterID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.SalesGroupAsRequestMaster == null;
            if (resolved.Result == null && model.SalesGroupAsRequestMaster != null)
            {
                resolved.Result = model.SalesGroupAsRequestMaster;
            }
            var modelObjectIsNull = resolved.Result == null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.SalesGroupAsRequestMasterID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.SalesGroupAsRequestMaster!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.SalesGroupAsRequestMaster!.UpdateSalesGroupFromModel(model.SalesGroupAsRequestMaster!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.SalesGroupAsRequestMasterID = resolved.Result!.ID;
                if (!modelObjectIsNull)
                {
                    if (!entityObjectIsNull)
                    {
                        // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
                    }
                    else
                    {
                        // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
                    }
                }
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved.Result!.Active;
            if (!modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // [Optional] Scenario 4: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                    entity.SalesGroupAsRequestMasterID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.SalesGroupAsRequestMasterID = null;
                entity.SalesGroupAsRequestMaster = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'SalesGroup' in this manner.");
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'SalesGroup' in this manner.");
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.SalesGroupAsRequestMasterID = null;
                entity.SalesGroupAsRequestMaster = null;
                return;
            }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given SalesGroup to the SalesQuote entity");
        }

        private async Task RelateOptionalSalesGroupAsRequestSubAsync(
            ISalesQuote entity,
            ISalesQuoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolved = await Workflows.SalesGroups.ResolveAsync(
                    model.SalesGroupAsRequestSubID, // By Other ID
                    model.SalesGroupAsRequestSubKey, // By Flattened Other Key
                    model.SalesGroupAsRequestSub,
                    context,
                    isInner: true)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.SalesGroupAsRequestSubID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.SalesGroupAsRequestSub == null;
            if (resolved.Result == null && model.SalesGroupAsRequestSub != null)
            {
                resolved.Result = model.SalesGroupAsRequestSub;
            }
            var modelObjectIsNull = resolved.Result == null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.SalesGroupAsRequestSubID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.SalesGroupAsRequestSub!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.SalesGroupAsRequestSub!.UpdateSalesGroupFromModel(model.SalesGroupAsRequestSub!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.SalesGroupAsRequestSubID = resolved.Result!.ID;
                if (!modelObjectIsNull)
                {
                    if (!entityObjectIsNull)
                    {
                        // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
                    }
                    else
                    {
                        // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
                    }
                }
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved.Result!.Active;
            if (!modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // [Optional] Scenario 4: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                    entity.SalesGroupAsRequestSubID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.SalesGroupAsRequestSubID = null;
                entity.SalesGroupAsRequestSub = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'SalesGroup' in this manner.");
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'SalesGroup' in this manner.");
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.SalesGroupAsRequestSubID = null;
                entity.SalesGroupAsRequestSub = null;
                return;
            }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given SalesGroup to the SalesQuote entity");
        }

        private async Task RelateOptionalSalesGroupAsResponseMasterAsync(
            ISalesQuote entity,
            ISalesQuoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolved = await Workflows.SalesGroups.ResolveAsync(
                    model.SalesGroupAsResponseMasterID, // By Other ID
                    model.SalesGroupAsResponseMasterKey, // By Flattened Other Key
                    model.SalesGroupAsResponseMaster,
                    context,
                    isInner: true)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.SalesGroupAsResponseMasterID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.SalesGroupAsResponseMaster == null;
            if (resolved.Result == null && model.SalesGroupAsResponseMaster != null)
            {
                resolved.Result = model.SalesGroupAsResponseMaster;
            }
            var modelObjectIsNull = resolved.Result == null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.SalesGroupAsResponseMasterID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.SalesGroupAsResponseMaster!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.SalesGroupAsResponseMaster!.UpdateSalesGroupFromModel(model.SalesGroupAsResponseMaster!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.SalesGroupAsResponseMasterID = resolved.Result!.ID;
                if (!modelObjectIsNull)
                {
                    if (!entityObjectIsNull)
                    {
                        // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
                    }
                    else
                    {
                        // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
                    }
                }
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved.Result!.Active;
            if (!modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // [Optional] Scenario 4: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                    entity.SalesGroupAsResponseMasterID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.SalesGroupAsResponseMasterID = null;
                entity.SalesGroupAsResponseMaster = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'SalesGroup' in this manner.");
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'SalesGroup' in this manner.");
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.SalesGroupAsResponseMasterID = null;
                entity.SalesGroupAsResponseMaster = null;
                return;
            }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given SalesGroup to the SalesQuote entity");
        }

        private async Task RelateOptionalSalesGroupAsResponseSubAsync(
            ISalesQuote entity,
            ISalesQuoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolved = await Workflows.SalesGroups.ResolveAsync(
                    model.SalesGroupAsResponseSubID, // By Other ID
                    model.SalesGroupAsResponseSubKey, // By Flattened Other Key
                    model.SalesGroupAsResponseSub,
                    context,
                    isInner: true)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.SalesGroupAsResponseSubID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.SalesGroupAsResponseSub == null;
            if (resolved.Result == null && model.SalesGroupAsResponseSub != null)
            {
                resolved.Result = model.SalesGroupAsResponseSub;
            }
            var modelObjectIsNull = resolved.Result == null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.SalesGroupAsResponseSubID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.SalesGroupAsResponseSub!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.SalesGroupAsResponseSub!.UpdateSalesGroupFromModel(model.SalesGroupAsResponseSub!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.SalesGroupAsResponseSubID = resolved.Result!.ID;
                if (!modelObjectIsNull)
                {
                    if (!entityObjectIsNull)
                    {
                        // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
                    }
                    else
                    {
                        // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
                    }
                }
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved.Result!.Active;
            if (!modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // [Optional] Scenario 4: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                    entity.SalesGroupAsResponseSubID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.SalesGroupAsResponseSubID = null;
                entity.SalesGroupAsResponseSub = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'SalesGroup' in this manner.");
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'SalesGroup' in this manner.");
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.SalesGroupAsResponseSubID = null;
                entity.SalesGroupAsResponseSub = null;
                return;
            }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given SalesGroup to the SalesQuote entity");
        }

        /// <summary>Executes the limited associate workflows operation.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task RunLimitedAssociateWorkflowsAsync(
            ISalesQuote entity,
            ISalesQuoteModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Skipped: Not supposed to make it or fully deprecated: Attributes
            // Skipped: Not supposed to make it or fully deprecated: Children
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, contextProfileName).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            if (model.Notes != null)
            {
                if (Contract.CheckValidID(entity.ID))
                {
                    foreach (var note in model.Notes)
                    {
                        note.SalesQuoteID = entity.ID;
                    }
                }
                await Workflows.SalesQuoteWithNotesAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
#pragma warning disable SA1501,format // Statement should not be on a single line
            if (model.SalesItems != null) { await Workflows.SalesQuoteWithSalesItemsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            // if (model.Discounts != null) { await Workflows.SalesQuoteWithDiscountsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.StoredFiles != null) { await Workflows.SalesQuoteWithStoredFilesAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.Contacts != null) { await Workflows.SalesQuoteWithContactsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.AssociatedSalesOrders != null) { await Workflows.SalesQuoteWithAssociatedSalesOrdersAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.SalesQuoteCategories != null) { await Workflows.SalesQuoteWithSalesQuoteCategoriesAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
#pragma warning restore SA1501,format // Statement should not be on a single line
        }
    }
}
