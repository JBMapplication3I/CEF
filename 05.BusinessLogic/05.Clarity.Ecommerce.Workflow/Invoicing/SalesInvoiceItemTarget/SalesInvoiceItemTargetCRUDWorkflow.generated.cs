// <autogenerated>
// <copyright file="SalesInvoiceItemTargetWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for SalesInvoiceItemTarget entities.</summary>
    /// <seealso cref="WorkflowBase{ISalesItemTargetBaseModel, ISalesItemTargetBaseSearchModel, ISalesInvoiceItemTarget, SalesInvoiceItemTarget}"/>
    /// <seealso cref="ISalesInvoiceItemTargetWorkflow"/>
    public partial class SalesInvoiceItemTargetWorkflow
        : WorkflowBase<ISalesItemTargetBaseModel, ISalesItemTargetBaseSearchModel, ISalesInvoiceItemTarget, SalesInvoiceItemTarget>
            , ISalesInvoiceItemTargetWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<SalesInvoiceItemTarget?, string?, ISalesItemTargetBaseModel?> MapFromConcreteFull
            => ModelMapperForSalesInvoiceItemTarget.MapSalesInvoiceItemTargetModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<SalesInvoiceItemTarget>, string?, IEnumerable<ISalesItemTargetBaseModel>> SelectLiteAndMapToModel
            => ModelMapperForSalesInvoiceItemTarget.SelectLiteSalesInvoiceItemTargetAndMapToSalesItemTargetBaseModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<SalesInvoiceItemTarget>, string?, IEnumerable<ISalesItemTargetBaseModel>> SelectListAndMapToModel
            => ModelMapperForSalesInvoiceItemTarget.SelectListSalesInvoiceItemTargetAndMapToSalesItemTargetBaseModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<SalesInvoiceItemTarget>, string?, ISalesItemTargetBaseModel?> SelectFirstFullAndMapToModel
            => ModelMapperForSalesInvoiceItemTarget.SelectFirstFullSalesInvoiceItemTargetAndMapToSalesItemTargetBaseModel;

        /// <inheritdoc/>
        protected override Func<ISalesInvoiceItemTarget, ISalesItemTargetBaseModel, DateTime, DateTime?, ISalesInvoiceItemTarget> UpdateEntityFromModel
            => ModelMapperForSalesInvoiceItemTarget.UpdateSalesInvoiceItemTargetFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<SalesInvoiceItemTarget>> FilterQueryByModelExtensionAsync(
            IQueryable<SalesInvoiceItemTarget> query,
            ISalesItemTargetBaseSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterSalesInvoiceItemTargetsBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            ISalesInvoiceItemTarget entity,
            ISalesItemTargetBaseModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            ISalesInvoiceItemTarget entity,
            ISalesItemTargetBaseModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            // None to process
        }

        #region Relate Workflows
        /// <summary>Relate Required Type.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required Type.</param>
        /// <param name="model">             The model that has a Required Type.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateRequiredTypeAsync(
            ISalesInvoiceItemTarget entity,
            ISalesItemTargetBaseModel model,
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
            ISalesInvoiceItemTarget entity,
            ISalesItemTargetBaseModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.SalesItemTargetTypes.ResolveWithAutoGenerateOptionalAsync(
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
                    entity.Type!.UpdateSalesItemTargetTypeFromModel(resolved.Result!, timestamp, timestamp);
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
                throw new InvalidOperationException("Cannot assign an inactive TypeID to the SalesInvoiceItemTarget entity");
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
                entity.Type = (SalesItemTargetType)resolved.Result!.CreateSalesItemTargetTypeEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Type to the SalesInvoiceItemTarget entity");
        }

        /// <summary>Relate Required DestinationContact.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required DestinationContact.</param>
        /// <param name="model">             The model that has a Required DestinationContact.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateRequiredDestinationContactAsync(
            ISalesInvoiceItemTarget entity,
            ISalesItemTargetBaseModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredDestinationContactAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required DestinationContact.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required DestinationContact.</param>
        /// <param name="model">    The model that has a Required DestinationContact.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateRequiredDestinationContactAsync(
            ISalesInvoiceItemTarget entity,
            ISalesItemTargetBaseModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.Contacts.ResolveWithAutoGenerateOptionalAsync(
                    byID: model.DestinationContactID, // By Other ID
                    byKey: model.DestinationContactKey, // By Flattened Other Key
                    model: model.DestinationContact, // The other property as a model
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.DestinationContactID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.DestinationContact == null;
            if (!resolved.ActionSucceeded && model.DestinationContact != null)
            {
                resolved.Result = model.DestinationContact;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable DestinationContactID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.DestinationContactID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.DestinationContactID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.DestinationContact!.UpdateContactFromModel(resolved.Result!, timestamp, timestamp);
                    // Update the Address Properties as well if available
                    if (model.DestinationContact?.Address != null)
                    {
                        if (entity.DestinationContact!.Address == null)
                        {
                            entity.DestinationContact.Address = (Address)model.DestinationContact.Address.CreateAddressEntity(timestamp, context.ContextProfileName);
                        }
                        else
                        {
                            entity.DestinationContact.Address.UpdateAddressFromModel(model.DestinationContact.Address, timestamp, timestamp);
                        }
                    }
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.DestinationContactID = resolved.Result!.ID;
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
                throw new InvalidOperationException("Cannot assign an inactive DestinationContactID to the SalesInvoiceItemTarget entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.DestinationContactID = resolved.Result!.ID;
                return;
            }
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.DestinationContactID = 0;
                entity.DestinationContact = (Contact)resolved.Result!.CreateContactEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given DestinationContact to the SalesInvoiceItemTarget entity");
        }

        /// <summary>Relate Optional BrandProduct.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Optional BrandProduct.</param>
        /// <param name="model">             The model that has a Optional BrandProduct.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateOptionalBrandProductAsync(
            ISalesInvoiceItemTarget entity,
            ISalesItemTargetBaseModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateOptionalBrandProductAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Optional BrandProduct.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Optional BrandProduct.</param>
        /// <param name="model">    The model that has a Optional BrandProduct.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateOptionalBrandProductAsync(
            ISalesInvoiceItemTarget entity,
            ISalesItemTargetBaseModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolved = await Workflows.BrandProducts.ResolveAsync(
                    byID: model.BrandProductID, // By Other ID
                    byKey: model.BrandProductKey, // By Flattened Other Key
                    model: model.BrandProduct, // Manual name if not UserProductType and not Discount or Discount and not Master
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.BrandProductID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.BrandProduct == null;
            if (!resolved.ActionSucceeded && model.BrandProduct != null)
            {
                resolved.Result = model.BrandProduct;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.BrandProductID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.BrandProductID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.BrandProduct!.UpdateBrandProductFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.BrandProductID = resolved.Result!.ID;
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
                    entity.BrandProductID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.BrandProductID = null;
                entity.BrandProduct = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'BrandProduct' in this manner.");
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'BrandProduct' in this manner.");
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.BrandProductID = null;
                entity.BrandProduct = null;
                return;
            }
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the model)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // TODO: Determine 'Equals' object between the objects so we only update if different
            //     // [Optional] Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.BrandProduct.UpdateBrandProductFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given BrandProduct to the SalesInvoiceItemTarget entity");
        }

        /// <summary>Relate Optional SelectedRateQuote.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Optional SelectedRateQuote.</param>
        /// <param name="model">             The model that has a Optional SelectedRateQuote.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateOptionalSelectedRateQuoteAsync(
            ISalesInvoiceItemTarget entity,
            ISalesItemTargetBaseModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateOptionalSelectedRateQuoteAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Optional SelectedRateQuote.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Optional SelectedRateQuote.</param>
        /// <param name="model">    The model that has a Optional SelectedRateQuote.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateOptionalSelectedRateQuoteAsync(
            ISalesInvoiceItemTarget entity,
            ISalesItemTargetBaseModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolved = await Workflows.RateQuotes.ResolveAsync(
                    byID: model.SelectedRateQuoteID, // By Other ID
                    byKey: model.SelectedRateQuoteKey, // By Flattened Other Key
                    byName: model.SelectedRateQuoteName, // By Flattened Other Name
                    model: model.SelectedRateQuote, // Manual name if not UserProductType and not Discount or Discount and not Master
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.SelectedRateQuoteID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.SelectedRateQuote == null;
            if (!resolved.ActionSucceeded && model.SelectedRateQuote != null)
            {
                resolved.Result = model.SelectedRateQuote;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.SelectedRateQuoteID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.SelectedRateQuoteID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.SelectedRateQuote!.UpdateRateQuoteFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.SelectedRateQuoteID = resolved.Result!.ID;
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
                    entity.SelectedRateQuoteID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.SelectedRateQuoteID = null;
                entity.SelectedRateQuote = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'RateQuote' in this manner.");
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'RateQuote' in this manner.");
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.SelectedRateQuoteID = null;
                entity.SelectedRateQuote = null;
                return;
            }
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the model)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // TODO: Determine 'Equals' object between the objects so we only update if different
            //     // [Optional] Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.SelectedRateQuote.UpdateRateQuoteFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given SelectedRateQuote to the SalesInvoiceItemTarget entity");
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultRelateWorkflowsAsync(
            ISalesInvoiceItemTarget entity,
            ISalesItemTargetBaseModel model,
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
            ISalesInvoiceItemTarget entity,
            ISalesItemTargetBaseModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // NOTE: Can't use Task.WhenAll as the context isn't thread safe and will throw an exception is more than
            // one thing is running at the same time on the same context.
            await RelateRequiredTypeAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateRequiredDestinationContactAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalBrandProductAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalSelectedRateQuoteAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
