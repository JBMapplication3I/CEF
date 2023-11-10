// <autogenerated>
// <copyright file="CounterWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for Counter entities.</summary>
    /// <seealso cref="WorkflowBase{ICounterModel, ICounterSearchModel, ICounter, Counter}"/>
    /// <seealso cref="ICounterWorkflow"/>
    public partial class CounterWorkflow
        : WorkflowBase<ICounterModel, ICounterSearchModel, ICounter, Counter>
            , ICounterWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<Counter?, string?, ICounterModel?> MapFromConcreteFull
            => ModelMapperForCounter.MapCounterModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<Counter>, string?, IEnumerable<ICounterModel>> SelectLiteAndMapToModel
            => ModelMapperForCounter.SelectLiteCounterAndMapToCounterModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<Counter>, string?, IEnumerable<ICounterModel>> SelectListAndMapToModel
            => ModelMapperForCounter.SelectListCounterAndMapToCounterModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<Counter>, string?, ICounterModel?> SelectFirstFullAndMapToModel
            => ModelMapperForCounter.SelectFirstFullCounterAndMapToCounterModel;

        /// <inheritdoc/>
        protected override Func<ICounter, ICounterModel, DateTime, DateTime?, ICounter> UpdateEntityFromModel
            => ModelMapperForCounter.UpdateCounterFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<Counter>> FilterQueryByModelExtensionAsync(
            IQueryable<Counter> query,
            ICounterSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterCountersBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            ICounter entity,
            ICounterModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            ICounter entity,
            ICounterModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            if (model.CounterLogs != null) { await Workflows.CounterWithCounterLogsAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
        }

        #region Relate Workflows
        /// <summary>Relate Required Type.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required Type.</param>
        /// <param name="model">             The model that has a Required Type.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateRequiredTypeAsync(
            ICounter entity,
            ICounterModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredTypeAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required Type.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required Type.</param>
        /// <param name="model">    The model that has a Required Type.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateRequiredTypeAsync(
            ICounter entity,
            ICounterModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.CounterTypes.ResolveWithAutoGenerateOptionalAsync(
                    byID: model.TypeID, // By Other ID
                    byKey: model.TypeKey, // By Flattened Other Key
                    byName: model.TypeName, // By Flattened Other Name
                    byDisplayName: null, // Skip DisplayName as it's not normally part of the interface
                    model: model.Type, // The other property as a model
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.TypeID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.Type == null;
            if (!resolved.ActionSucceeded && model.Type != null)
            {
                resolved.Result = model.Type;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable TypeID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.TypeID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.TypeID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.Type!.UpdateCounterTypeFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.TypeID = resolved.Result!.ID;
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
                throw new InvalidOperationException("Cannot assign an inactive TypeID to the Counter entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.TypeID = resolved.Result!.ID;
                return;
            }
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.TypeID = 0;
                entity.Type = (CounterType)resolved.Result!.CreateCounterTypeEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Type to the Counter entity");
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultRelateWorkflowsAsync(
            ICounter entity,
            ICounterModel model,
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
            ICounter entity,
            ICounterModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // NOTE: Can't use Task.WhenAll as the context isn't thread safe and will throw an exception is more than
            // one thing is running at the same time on the same context.
            await RelateRequiredTypeAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
