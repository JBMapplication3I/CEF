// <autogenerated>
// <copyright file="WalletWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for Wallet entities.</summary>
    /// <seealso cref="NameableWorkflowBase{IWalletModel, IWalletSearchModel, IWallet, Wallet}"/>
    /// <seealso cref="IWalletWorkflow"/>
    public partial class WalletWorkflow
        : NameableWorkflowBase<IWalletModel, IWalletSearchModel, IWallet, Wallet>
            , IWalletWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<Wallet?, string?, IWalletModel?> MapFromConcreteFull
            => ModelMapperForWallet.MapWalletModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<Wallet>, string?, IEnumerable<IWalletModel>> SelectLiteAndMapToModel
            => ModelMapperForWallet.SelectLiteWalletAndMapToWalletModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<Wallet>, string?, IEnumerable<IWalletModel>> SelectListAndMapToModel
            => ModelMapperForWallet.SelectListWalletAndMapToWalletModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<Wallet>, string?, IWalletModel?> SelectFirstFullAndMapToModel
            => ModelMapperForWallet.SelectFirstFullWalletAndMapToWalletModel;

        /// <inheritdoc/>
        protected override Func<IWallet, IWalletModel, DateTime, DateTime?, IWallet> UpdateEntityFromModel
            => ModelMapperForWallet.UpdateWalletFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<Wallet>> FilterQueryByModelExtensionAsync(
            IQueryable<Wallet> query,
            IWalletSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterWalletsBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IWallet entity,
            IWalletModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IWallet entity,
            IWalletModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            // None to process
        }

        #region Relate Workflows
        /// <summary>Relate Required User.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required User.</param>
        /// <param name="model">             The model that has a Required User.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateRequiredUserAsync(
            IWallet entity,
            IWalletModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredUserAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required User.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required User.</param>
        /// <param name="model">    The model that has a Required User.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateRequiredUserAsync(
            IWallet entity,
            IWalletModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolved = await Workflows.Users.ResolveAsync(
                    byID: model.UserID, // By Other ID
                    byKey: model.UserKey, // By Flattened Other Key
                    model: model.User, // Manual name if not UserProductType and not Discount or Discount and not Master
                    context: context,
                    isInner: false)
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
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable UserID");
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
            if (!modelObjectIDIsNull && !modelObjectIsActive)
            {
                // [Required] Scenario 4: We have an ID on the model, but the object is inactive
                throw new InvalidOperationException("Cannot assign an inactive UserID to the Wallet entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.UserID = resolved.Result!.ID;
                return;
            }
            // [Required] Scenario 6 is only allowed when allowed to Auto-Generate via Relate, skipped
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given User to the Wallet entity");
        }

        /// <summary>Relate Optional Currency.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Optional Currency.</param>
        /// <param name="model">             The model that has a Optional Currency.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateOptionalCurrencyAsync(
            IWallet entity,
            IWalletModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateOptionalCurrencyAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Optional Currency.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Optional Currency.</param>
        /// <param name="model">    The model that has a Optional Currency.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateOptionalCurrencyAsync(
            IWallet entity,
            IWalletModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolved = await Workflows.Currencies.ResolveAsync(
                    byID: model.CurrencyID, // By Other ID
                    byKey: model.CurrencyKey, // By Flattened Other Key
                    byName: model.CurrencyName, // By Flattened Other Name
                    model: model.Currency, // Manual name if not UserProductType and not Discount or Discount and not Master
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.CurrencyID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.Currency == null;
            if (!resolved.ActionSucceeded && model.Currency != null)
            {
                resolved.Result = model.Currency;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.CurrencyID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.CurrencyID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.Currency!.UpdateCurrencyFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.CurrencyID = resolved.Result!.ID;
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
                    entity.CurrencyID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.CurrencyID = null;
                entity.Currency = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'Currency' in this manner.");
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'Currency' in this manner.");
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.CurrencyID = null;
                entity.Currency = null;
                return;
            }
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the model)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // TODO: Determine 'Equals' object between the objects so we only update if different
            //     // [Optional] Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.Currency.UpdateCurrencyFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Currency to the Wallet entity");
        }

        /// <summary>Relate Optional AccountContact.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Optional AccountContact.</param>
        /// <param name="model">             The model that has a Optional AccountContact.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateOptionalAccountContactAsync(
            IWallet entity,
            IWalletModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateOptionalAccountContactAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Optional AccountContact.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Optional AccountContact.</param>
        /// <param name="model">    The model that has a Optional AccountContact.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateOptionalAccountContactAsync(
            IWallet entity,
            IWalletModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolved = await Workflows.AccountContacts.ResolveAsync(
                    byID: model.AccountContactID, // By Other ID
                    byKey: model.AccountContactKey, // By Flattened Other Key
                    byName: model.AccountContactName, // By Flattened Other Name
                    model: model.AccountContact, // Manual name if not UserProductType and not Discount or Discount and not Master
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.AccountContactID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.AccountContact == null;
            if (!resolved.ActionSucceeded && model.AccountContact != null)
            {
                resolved.Result = model.AccountContact;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.AccountContactID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.AccountContactID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.AccountContact!.UpdateAccountContactFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.AccountContactID = resolved.Result!.ID;
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
                    entity.AccountContactID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.AccountContactID = null;
                entity.AccountContact = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'AccountContact' in this manner.");
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'AccountContact' in this manner.");
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.AccountContactID = null;
                entity.AccountContact = null;
                return;
            }
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the model)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // TODO: Determine 'Equals' object between the objects so we only update if different
            //     // [Optional] Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.AccountContact.UpdateAccountContactFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given AccountContact to the Wallet entity");
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultRelateWorkflowsAsync(
            IWallet entity,
            IWalletModel model,
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
            IWallet entity,
            IWalletModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // NOTE: Can't use Task.WhenAll as the context isn't thread safe and will throw an exception is more than
            // one thing is running at the same time on the same context.
            await RelateRequiredUserAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalCurrencyAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalAccountContactAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
