// <autogenerated>
// <copyright file="DiscountManufacturerWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for DiscountManufacturer entities.</summary>
    /// <seealso cref="WorkflowBase{IDiscountManufacturerModel, IDiscountManufacturerSearchModel, IDiscountManufacturer, DiscountManufacturer}"/>
    /// <seealso cref="IDiscountManufacturerWorkflow"/>
    public partial class DiscountManufacturerWorkflow
        : WorkflowBase<IDiscountManufacturerModel, IDiscountManufacturerSearchModel, IDiscountManufacturer, DiscountManufacturer>
            , IDiscountManufacturerWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<DiscountManufacturer?, string?, IDiscountManufacturerModel?> MapFromConcreteFull
            => ModelMapperForDiscountManufacturer.MapDiscountManufacturerModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<DiscountManufacturer>, string?, IEnumerable<IDiscountManufacturerModel>> SelectLiteAndMapToModel
            => ModelMapperForDiscountManufacturer.SelectLiteDiscountManufacturerAndMapToDiscountManufacturerModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<DiscountManufacturer>, string?, IEnumerable<IDiscountManufacturerModel>> SelectListAndMapToModel
            => ModelMapperForDiscountManufacturer.SelectListDiscountManufacturerAndMapToDiscountManufacturerModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<DiscountManufacturer>, string?, IDiscountManufacturerModel?> SelectFirstFullAndMapToModel
            => ModelMapperForDiscountManufacturer.SelectFirstFullDiscountManufacturerAndMapToDiscountManufacturerModel;

        /// <inheritdoc/>
        protected override Func<IDiscountManufacturer, IDiscountManufacturerModel, DateTime, DateTime?, IDiscountManufacturer> UpdateEntityFromModel
            => ModelMapperForDiscountManufacturer.UpdateDiscountManufacturerFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<DiscountManufacturer>> FilterQueryByModelExtensionAsync(
            IQueryable<DiscountManufacturer> query,
            IDiscountManufacturerSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterDiscountManufacturersBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IDiscountManufacturer entity,
            IDiscountManufacturerModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IDiscountManufacturer entity,
            IDiscountManufacturerModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            // None to process
        }

        #region Relate Workflows
        /// <summary>Relate Required Slave.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required Slave.</param>
        /// <param name="model">             The model that has a Required Slave.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateRequiredSlaveAsync(
            IDiscountManufacturer entity,
            IDiscountManufacturerModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredSlaveAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required Slave.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required Slave.</param>
        /// <param name="model">    The model that has a Required Slave.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateRequiredSlaveAsync(
            IDiscountManufacturer entity,
            IDiscountManufacturerModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolved = await Workflows.Manufacturers.ResolveAsync(
                    byID: model.SlaveID, // By Other ID
                    byKey: model.SlaveKey, // By Flattened Other Key
                    byName: model.SlaveName, // Slave
                    model: model.Slave, // Manual name if not UserProductType and not Discount or Discount and not Master
                    context: context,
                    isInner: false)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.SlaveID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.Slave == null;
            if (!resolved.ActionSucceeded && model.Slave != null)
            {
                resolved.Result = model.Slave;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable SlaveID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.SlaveID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.SlaveID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.Slave!.UpdateManufacturerFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.SlaveID = resolved.Result!.ID;
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
                throw new InvalidOperationException("Cannot assign an inactive SlaveID to the DiscountManufacturer entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.SlaveID = resolved.Result!.ID;
                return;
            }
            // [Required] Scenario 6 is only allowed when allowed to Auto-Generate via Relate, skipped
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Slave to the DiscountManufacturer entity");
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultRelateWorkflowsAsync(
            IDiscountManufacturer entity,
            IDiscountManufacturerModel model,
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
            IDiscountManufacturer entity,
            IDiscountManufacturerModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // NOTE: Can't use Task.WhenAll as the context isn't thread safe and will throw an exception is more than
            // one thing is running at the same time on the same context.
            await RelateRequiredSlaveAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
