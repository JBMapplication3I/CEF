// <copyright file="SalesGroupCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales group workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using ServiceStack;
    using Utilities;

    public partial class SalesGroupWorkflow
    {
        /// <inheritdoc/>
        public async Task<ISalesGroupModel> SecureSalesGroupAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName)
        {
            var model = await GetAsync(id, contextProfileName).ConfigureAwait(false);
            if (Contract.CheckNotNull(model)
                && accountIDs.Exists(x => x == model!.AccountID)
                && model!.Active)
            {
                return model;
            }
            throw HttpError.Unauthorized("Unauthorized to view this SalesGroup");
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<SalesGroup>> FilterQueryByModelCustomAsync(
            IQueryable<SalesGroup> query,
            ISalesGroupSearchModel search,
            IClarityEcommerceEntities context)
        {
            if (search == null)
            {
                return query;
            }
            return Contract.RequiresNotNull(query)
                .FilterSalesGroupsByDates(search.MinDate, search.MaxDate);
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            ISalesGroup entity,
            ISalesGroupModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            UpdateEntityFromModel(entity, model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            await RunLimitedRelateWorkflowsAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(
                    entity,
                    model,
                    timestamp,
                    context.ContextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Executes limited relate workflows.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        protected async Task RunLimitedRelateWorkflowsAsync(
            ISalesGroup entity,
            ISalesGroupModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // NOTE: Can't use Task.WhenAll as the context isn't thread safe and will throw an exception is more than
            // one thing is running at the same time on the same context.
            await RelateOptionalAccountAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalBrandAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateBillingContactAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <summary>Executes limited associate workflows.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        private async Task RelateBillingContactAsync(
            ISalesGroup entity,
            ISalesGroupModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // May resolve but not allowed to auto-generate
            var resolved = await Workflows.Contacts.ResolveAsync(
                    byID: model.BillingContactID, // By Other ID
                    byKey: model.BillingContactKey, // By Flattened Other Key
                    model: model.BillingContact, // Manual name if not UserProductType and not Discount or Discount and not Master
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.BillingContactID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.BillingContact == null;
            if (resolved.Result == null && model.BillingContact != null)
            {
                resolved.Result = model.BillingContact;
            }
            var modelObjectIsNull = resolved.Result == null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.BillingContactID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.BillingContact!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.BillingContact!.UpdateContactFromModel(model.BillingContact!, timestamp, timestamp);
                    // Update the Address Properties as well if available
                    if (model.BillingContact?.Address != null)
                    {
                        if (entity.BillingContact!.Address == null)
                        {
                            var address = await Workflows.Addresses.ResolveAddressAsync(
                                    model.BillingContact.Address,
                                    context)
                                .ConfigureAwait(false);
                            entity.BillingContact.Address = (Address)address.CreateAddressEntity(timestamp, context.ContextProfileName);
                            entity.BillingContact.Address.RegionID = address.RegionID;
                            entity.BillingContact.Address.CountryID = address.CountryID;
                        }
                        else
                        {
                            var address = await Workflows.Addresses.ResolveAddressAsync(
                                    model.BillingContact.Address,
                                    context)
                                .ConfigureAwait(false);
                            entity.BillingContact.Address.UpdateAddressFromModel(address, timestamp, timestamp);
                            entity.BillingContact.Address.RegionID = address.RegionID;
                            entity.BillingContact.Address.CountryID = address.CountryID;
                        }
                    }
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.BillingContactID = resolved.Result!.ID;
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
                    entity.BillingContactID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.BillingContactID = null;
                entity.BillingContact = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'Contact' in this manner.");
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, however we're not allowed to create, so throw as we can't handle this situation
                throw new InvalidOperationException("Cannot create a new record of type 'Contact' in this manner.");
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.BillingContactID = null;
                entity.BillingContact = null;
                return;
            }
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the model)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // TODO: Determine 'Equals' object between the objects so we only update if different
            //     // [Optional] Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.BillingContact.UpdateContactFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given BillingContact to the SalesGroup entity");
        }
    }
}
