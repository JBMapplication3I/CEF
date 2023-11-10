// <autogenerated>
// <copyright file="SalesReturnReasonWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for SalesReturnReason entities.</summary>
    /// <seealso cref="TypableWorkflowBase{ISalesReturnReasonModel, ISalesReturnReasonSearchModel, ISalesReturnReason, SalesReturnReason}"/>
    /// <seealso cref="ISalesReturnReasonWorkflow"/>
    public partial class SalesReturnReasonWorkflow
        : TypableWorkflowBase<ISalesReturnReasonModel, ISalesReturnReasonSearchModel, ISalesReturnReason, SalesReturnReason>
            , ISalesReturnReasonWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<SalesReturnReason?, string?, ISalesReturnReasonModel?> MapFromConcreteFull
            => ModelMapperForSalesReturnReason.MapSalesReturnReasonModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<SalesReturnReason>, string?, IEnumerable<ISalesReturnReasonModel>> SelectLiteAndMapToModel
            => ModelMapperForSalesReturnReason.SelectLiteSalesReturnReasonAndMapToSalesReturnReasonModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<SalesReturnReason>, string?, IEnumerable<ISalesReturnReasonModel>> SelectListAndMapToModel
            => ModelMapperForSalesReturnReason.SelectListSalesReturnReasonAndMapToSalesReturnReasonModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<SalesReturnReason>, string?, ISalesReturnReasonModel?> SelectFirstFullAndMapToModel
            => ModelMapperForSalesReturnReason.SelectFirstFullSalesReturnReasonAndMapToSalesReturnReasonModel;

        /// <inheritdoc/>
        protected override Func<ISalesReturnReason, ISalesReturnReasonModel, DateTime, DateTime?, ISalesReturnReason> UpdateEntityFromModel
            => ModelMapperForSalesReturnReason.UpdateSalesReturnReasonFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<SalesReturnReason>> FilterQueryByModelExtensionAsync(
            IQueryable<SalesReturnReason> query,
            ISalesReturnReasonSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterSalesReturnReasonsBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            ISalesReturnReason entity,
            ISalesReturnReasonModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            ISalesReturnReason entity,
            ISalesReturnReasonModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            // None to process
        }

        #region Relate Workflows
        /// <summary>Relate Optional RestockingFeeAmountCurrency.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Optional RestockingFeeAmountCurrency.</param>
        /// <param name="model">             The model that has a Optional RestockingFeeAmountCurrency.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateOptionalRestockingFeeAmountCurrencyAsync(
            ISalesReturnReason entity,
            ISalesReturnReasonModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateOptionalRestockingFeeAmountCurrencyAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Optional RestockingFeeAmountCurrency.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Optional RestockingFeeAmountCurrency.</param>
        /// <param name="model">    The model that has a Optional RestockingFeeAmountCurrency.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateOptionalRestockingFeeAmountCurrencyAsync(
            ISalesReturnReason entity,
            ISalesReturnReasonModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.Currencies.ResolveWithAutoGenerateOptionalAsync(
                    byID: model.RestockingFeeAmountCurrencyID, // By Other ID
                    byKey: model.RestockingFeeAmountCurrencyKey, // By Flattened Other Key
                    byName: model.RestockingFeeAmountCurrencyName, // By Flattened Other Name
                    model: model.RestockingFeeAmountCurrency, // The other property as a model
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.RestockingFeeAmountCurrencyID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.RestockingFeeAmountCurrency == null;
            if (!resolved.ActionSucceeded && model.RestockingFeeAmountCurrency != null)
            {
                resolved.Result = model.RestockingFeeAmountCurrency;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.RestockingFeeAmountCurrencyID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.RestockingFeeAmountCurrencyID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.RestockingFeeAmountCurrency!.UpdateCurrencyFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.RestockingFeeAmountCurrencyID = resolved.Result!.ID;
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
                    entity.RestockingFeeAmountCurrencyID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.RestockingFeeAmountCurrencyID = null;
                entity.RestockingFeeAmountCurrency = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.RestockingFeeAmountCurrencyID = null;
                entity.RestockingFeeAmountCurrency = (Currency)resolved.Result!.CreateCurrencyEntity(timestamp, context.ContextProfileName);
                return;
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, assign the new model
                entity.RestockingFeeAmountCurrency = (Currency)resolved.Result!.CreateCurrencyEntity(timestamp, context.ContextProfileName);
                return;
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.RestockingFeeAmountCurrencyID = null;
                entity.RestockingFeeAmountCurrency = null;
                return;
            }
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the model)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // TODO: Determine 'Equals' object between the objects so we only update if different
            //     // [Optional] Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.RestockingFeeAmountCurrency.UpdateCurrencyFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given RestockingFeeAmountCurrency to the SalesReturnReason entity");
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultRelateWorkflowsAsync(
            ISalesReturnReason entity,
            ISalesReturnReasonModel model,
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
            ISalesReturnReason entity,
            ISalesReturnReasonModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // NOTE: Can't use Task.WhenAll as the context isn't thread safe and will throw an exception is more than
            // one thing is running at the same time on the same context.
            await RelateOptionalRestockingFeeAmountCurrencyAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
