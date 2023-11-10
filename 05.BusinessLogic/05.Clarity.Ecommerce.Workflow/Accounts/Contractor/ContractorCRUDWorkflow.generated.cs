// <autogenerated>
// <copyright file="ContractorWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for Contractor entities.</summary>
    /// <seealso cref="WorkflowBase{IContractorModel, IContractorSearchModel, IContractor, Contractor}"/>
    /// <seealso cref="IContractorWorkflow"/>
    public partial class ContractorWorkflow
        : WorkflowBase<IContractorModel, IContractorSearchModel, IContractor, Contractor>
            , IContractorWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<Contractor?, string?, IContractorModel?> MapFromConcreteFull
            => ModelMapperForContractor.MapContractorModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<Contractor>, string?, IEnumerable<IContractorModel>> SelectLiteAndMapToModel
            => ModelMapperForContractor.SelectLiteContractorAndMapToContractorModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<Contractor>, string?, IEnumerable<IContractorModel>> SelectListAndMapToModel
            => ModelMapperForContractor.SelectListContractorAndMapToContractorModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<Contractor>, string?, IContractorModel?> SelectFirstFullAndMapToModel
            => ModelMapperForContractor.SelectFirstFullContractorAndMapToContractorModel;

        /// <inheritdoc/>
        protected override Func<IContractor, IContractorModel, DateTime, DateTime?, IContractor> UpdateEntityFromModel
            => ModelMapperForContractor.UpdateContractorFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<Contractor>> FilterQueryByModelExtensionAsync(
            IQueryable<Contractor> query,
            IContractorSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterContractorsBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IContractor entity,
            IContractorModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IContractor entity,
            IContractorModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            if (model.ServiceAreas != null) { await Workflows.ContractorWithServiceAreasAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
        }

        #region Relate Workflows
        /// <summary>Relate Optional Account.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Optional Account.</param>
        /// <param name="model">             The model that has a Optional Account.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateOptionalAccountAsync(
            IContractor entity,
            IContractorModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateOptionalAccountAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Optional Account.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Optional Account.</param>
        /// <param name="model">    The model that has a Optional Account.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateOptionalAccountAsync(
            IContractor entity,
            IContractorModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.Accounts.ResolveWithAutoGenerateOptionalAsync(
                    byID: model.AccountID, // By Other ID
                    byKey: model.AccountKey, // By Flattened Other Key
                    byName: model.AccountName, // By Flattened Other Name
                    model: model.Account, // The other property as a model
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.AccountID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.Account == null;
            if (!resolved.ActionSucceeded && model.Account != null)
            {
                resolved.Result = model.Account;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.AccountID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.AccountID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.Account!.UpdateAccountFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.AccountID = resolved.Result!.ID;
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
                    entity.AccountID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.AccountID = null;
                entity.Account = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.AccountID = null;
                entity.Account = (Account)resolved.Result!.CreateAccountEntity(timestamp, context.ContextProfileName);
                return;
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, assign the new model
                entity.Account = (Account)resolved.Result!.CreateAccountEntity(timestamp, context.ContextProfileName);
                return;
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.AccountID = null;
                entity.Account = null;
                return;
            }
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the model)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // TODO: Determine 'Equals' object between the objects so we only update if different
            //     // [Optional] Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.Account.UpdateAccountFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Account to the Contractor entity");
        }

        /// <summary>Relate Optional User.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Optional User.</param>
        /// <param name="model">             The model that has a Optional User.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateOptionalUserAsync(
            IContractor entity,
            IContractorModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateOptionalUserAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Optional User.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Optional User.</param>
        /// <param name="model">    The model that has a Optional User.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateOptionalUserAsync(
            IContractor entity,
            IContractorModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.Users.ResolveWithAutoGenerateOptionalAsync(
                    byID: model.UserID, // By Other ID
                    byKey: model.UserKey, // By Flattened Other Key
                    model: model.User, // The other property as a model
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.UserID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.User == null;
            if (!resolved.ActionSucceeded && model.User != null)
            {
                resolved.Result = model.User;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.UserID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.UserID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.User!.UpdateUserFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.UserID = resolved.Result!.ID;
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
                    entity.UserID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.UserID = null;
                entity.User = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.UserID = null;
                entity.User = (User)resolved.Result!.CreateUserEntity(timestamp, context.ContextProfileName);
                return;
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, assign the new model
                entity.User = (User)resolved.Result!.CreateUserEntity(timestamp, context.ContextProfileName);
                return;
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.UserID = null;
                entity.User = null;
                return;
            }
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the model)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // TODO: Determine 'Equals' object between the objects so we only update if different
            //     // [Optional] Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.User.UpdateUserFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given User to the Contractor entity");
        }

        /// <summary>Relate Optional Store.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Optional Store.</param>
        /// <param name="model">             The model that has a Optional Store.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateOptionalStoreAsync(
            IContractor entity,
            IContractorModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateOptionalStoreAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Optional Store.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Optional Store.</param>
        /// <param name="model">    The model that has a Optional Store.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateOptionalStoreAsync(
            IContractor entity,
            IContractorModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.Stores.ResolveWithAutoGenerateOptionalAsync(
                    byID: model.StoreID, // By Other ID
                    byKey: model.StoreKey, // By Flattened Other Key
                    byName: model.StoreName, // By Flattened Other Name
                    model: model.Store, // The other property as a model
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.StoreID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.Store == null;
            if (!resolved.ActionSucceeded && model.Store != null)
            {
                resolved.Result = model.Store;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.StoreID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.StoreID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.Store!.UpdateStoreFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.StoreID = resolved.Result!.ID;
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
                    entity.StoreID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.StoreID = null;
                entity.Store = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.StoreID = null;
                entity.Store = (Store)resolved.Result!.CreateStoreEntity(timestamp, context.ContextProfileName);
                return;
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, assign the new model
                entity.Store = (Store)resolved.Result!.CreateStoreEntity(timestamp, context.ContextProfileName);
                return;
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.StoreID = null;
                entity.Store = null;
                return;
            }
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the model)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // TODO: Determine 'Equals' object between the objects so we only update if different
            //     // [Optional] Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.Store.UpdateStoreFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Store to the Contractor entity");
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultRelateWorkflowsAsync(
            IContractor entity,
            IContractorModel model,
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
            IContractor entity,
            IContractorModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // NOTE: Can't use Task.WhenAll as the context isn't thread safe and will throw an exception is more than
            // one thing is running at the same time on the same context.
            await RelateOptionalAccountAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalUserAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalStoreAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
