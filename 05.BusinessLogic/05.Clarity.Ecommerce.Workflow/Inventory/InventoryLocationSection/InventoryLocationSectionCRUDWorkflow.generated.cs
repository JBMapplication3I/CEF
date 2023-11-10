// <autogenerated>
// <copyright file="InventoryLocationSectionWorkflow.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Workflow generated to provide base setups</summary>
// <remarks>This file was auto-generated by Workflows.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
#nullable enable
// ReSharper disable ConvertToUsingDeclaration, InvertIf, ReturnValueOfPureMethodIsNotUsed, UnusedMember.Local
#pragma warning disable CS0618,CS1711,CS1572,CS1580,CS1581,CS1584
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
    using Interfaces.Workflow;
    using Mapper;
    using Utilities;

    /// <summary>A workflow for InventoryLocationSection entities.</summary>
    /// <seealso cref="NameableWorkflowBase{IInventoryLocationSectionModel, IInventoryLocationSectionSearchModel, IInventoryLocationSection, InventoryLocationSection}"/>
    /// <seealso cref="IInventoryLocationSectionWorkflow"/>
    public partial class InventoryLocationSectionWorkflow
        : NameableWorkflowBase<IInventoryLocationSectionModel, IInventoryLocationSectionSearchModel, IInventoryLocationSection, InventoryLocationSection>
            , IInventoryLocationSectionWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<InventoryLocationSection?, string?, IInventoryLocationSectionModel?> MapFromConcreteFull
            => ModelMapperForInventoryLocationSection.MapInventoryLocationSectionModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<InventoryLocationSection>, string?, IEnumerable<IInventoryLocationSectionModel>> SelectLiteAndMapToModel
            => ModelMapperForInventoryLocationSection.SelectLiteInventoryLocationSectionAndMapToInventoryLocationSectionModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<InventoryLocationSection>, string?, IEnumerable<IInventoryLocationSectionModel>> SelectListAndMapToModel
            => ModelMapperForInventoryLocationSection.SelectListInventoryLocationSectionAndMapToInventoryLocationSectionModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<InventoryLocationSection>, string?, IInventoryLocationSectionModel?> SelectFirstFullAndMapToModel
            => ModelMapperForInventoryLocationSection.SelectFirstFullInventoryLocationSectionAndMapToInventoryLocationSectionModel;

        /// <inheritdoc/>
        protected override Func<IInventoryLocationSection, IInventoryLocationSectionModel, DateTime, DateTime?, IInventoryLocationSection> UpdateEntityFromModel
            => ModelMapperForInventoryLocationSection.UpdateInventoryLocationSectionFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<InventoryLocationSection>> FilterQueryByModelExtensionAsync(
            IQueryable<InventoryLocationSection> query,
            IInventoryLocationSectionSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterInventoryLocationSectionsBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IInventoryLocationSection entity,
            IInventoryLocationSectionModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IInventoryLocationSection entity,
            IInventoryLocationSectionModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            if (model.ProductInventoryLocationSections != null) { await Workflows.InventoryLocationSectionWithProductInventoryLocationSectionsAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
            // Skipped: Not supposed to map this property in via this manner: Shipments
        }

        #region Relate Workflows
        /// <summary>Relate Required InventoryLocation.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required InventoryLocation.</param>
        /// <param name="model">             The model that has a Required InventoryLocation.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateRequiredInventoryLocationAsync(
            IInventoryLocationSection entity,
            IInventoryLocationSectionModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredInventoryLocationAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required InventoryLocation.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required InventoryLocation.</param>
        /// <param name="model">    The model that has a Required InventoryLocation.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateRequiredInventoryLocationAsync(
            IInventoryLocationSection entity,
            IInventoryLocationSectionModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.InventoryLocations.ResolveWithAutoGenerateOptionalAsync(
                    byID: model.InventoryLocationID, // By Other ID
                    byKey: model.InventoryLocationKey, // By Flattened Other Key
                    byName: model.InventoryLocationName, // By Flattened Other Name
                    model: model.InventoryLocation, // The other property as a model
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.InventoryLocationID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.InventoryLocation == null;
            if (!resolved.ActionSucceeded && model.InventoryLocation != null)
            {
                resolved.Result = model.InventoryLocation;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable InventoryLocationID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.InventoryLocationID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.InventoryLocationID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.InventoryLocation!.UpdateInventoryLocationFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.InventoryLocationID = resolved.Result!.ID;
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
            if (!modelObjectIDIsNull && !modelObjectIsActive)
            {
                // [Required] Scenario 4: We have an ID on the model, but the object is inactive
                throw new InvalidOperationException("Cannot assign an inactive InventoryLocationID to the InventoryLocationSection entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.InventoryLocationID = resolved.Result!.ID;
                return;
            }
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.InventoryLocationID = 0;
                entity.InventoryLocation = (InventoryLocation)resolved.Result!.CreateInventoryLocationEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given InventoryLocation to the InventoryLocationSection entity");
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultRelateWorkflowsAsync(
            IInventoryLocationSection entity,
            IInventoryLocationSectionModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        // ReSharper disable AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
#pragma warning disable 1998
        protected override async Task RunDefaultRelateWorkflowsAsync(
#pragma warning restore 1998
            IInventoryLocationSection entity,
            IInventoryLocationSectionModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // NOTE: Can't use Task.WhenAll as the context isn't thread safe and will throw an exception is more than
            // one thing is running at the same time on the same context.
            await RelateRequiredInventoryLocationAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
