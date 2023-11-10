// <autogenerated>
// <copyright file="AccountProductWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for AccountProduct entities.</summary>
    /// <seealso cref="WorkflowBase{IAccountProductModel, IAccountProductSearchModel, IAccountProduct, AccountProduct}"/>
    /// <seealso cref="IAccountProductWorkflow"/>
    public partial class AccountProductWorkflow
        : WorkflowBase<IAccountProductModel, IAccountProductSearchModel, IAccountProduct, AccountProduct>
            , IAccountProductWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<AccountProduct?, string?, IAccountProductModel?> MapFromConcreteFull
            => ModelMapperForAccountProduct.MapAccountProductModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<AccountProduct>, string?, IEnumerable<IAccountProductModel>> SelectLiteAndMapToModel
            => ModelMapperForAccountProduct.SelectLiteAccountProductAndMapToAccountProductModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<AccountProduct>, string?, IEnumerable<IAccountProductModel>> SelectListAndMapToModel
            => ModelMapperForAccountProduct.SelectListAccountProductAndMapToAccountProductModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<AccountProduct>, string?, IAccountProductModel?> SelectFirstFullAndMapToModel
            => ModelMapperForAccountProduct.SelectFirstFullAccountProductAndMapToAccountProductModel;

        /// <inheritdoc/>
        protected override Func<IAccountProduct, IAccountProductModel, DateTime, DateTime?, IAccountProduct> UpdateEntityFromModel
            => ModelMapperForAccountProduct.UpdateAccountProductFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<AccountProduct>> FilterQueryByModelExtensionAsync(
            IQueryable<AccountProduct> query,
            IAccountProductSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterAccountProductsBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IAccountProduct entity,
            IAccountProductModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IAccountProduct entity,
            IAccountProductModel model,
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
            IAccountProduct entity,
            IAccountProductModel model,
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
            IAccountProduct entity,
            IAccountProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolved = await Workflows.Products.ResolveAsync(
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
                    entity.Slave!.UpdateProductFromModel(resolved.Result!, timestamp, timestamp);
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
                throw new InvalidOperationException("Cannot assign an inactive SlaveID to the AccountProduct entity");
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
                "Couldn't figure out how to relate the given Slave to the AccountProduct entity");
        }

        /// <summary>Relate Required Type.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required Type.</param>
        /// <param name="model">             The model that has a Required Type.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateRequiredTypeAsync(
            IAccountProduct entity,
            IAccountProductModel model,
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
            IAccountProduct entity,
            IAccountProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.AccountProductTypes.ResolveWithAutoGenerateOptionalAsync(
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
                    entity.Type!.UpdateAccountProductTypeFromModel(resolved.Result!, timestamp, timestamp);
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
                throw new InvalidOperationException("Cannot assign an inactive TypeID to the AccountProduct entity");
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
                entity.Type = (AccountProductType)resolved.Result!.CreateAccountProductTypeEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Type to the AccountProduct entity");
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultRelateWorkflowsAsync(
            IAccountProduct entity,
            IAccountProductModel model,
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
            IAccountProduct entity,
            IAccountProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // NOTE: Can't use Task.WhenAll as the context isn't thread safe and will throw an exception is more than
            // one thing is running at the same time on the same context.
            await RelateRequiredSlaveAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateRequiredTypeAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
