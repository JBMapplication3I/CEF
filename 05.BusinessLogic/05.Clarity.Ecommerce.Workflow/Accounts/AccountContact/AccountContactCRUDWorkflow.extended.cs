// <copyright file="AccountContactCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account contact workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class AccountContactWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse> MarkAccountContactAsNeitherBillingNorShippingAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.AccountContacts
                .FilterByActive(true)
                .FilterByID(Contract.RequiresValidID(id))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (entity == null)
            {
                return CEFAR.FailingCEFAR($"ERROR! Could not locate record with ID '{id}'");
            }
            var changed = false;
            if (entity.IsBilling)
            {
                entity.IsBilling = false;
                changed = true;
            }
            if (entity.IsPrimary)
            {
                entity.IsPrimary = false;
                changed = true;
            }
            if (changed)
            {
                entity.UpdatedDate = DateExtensions.GenDateTime;
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> MarkAccountContactAsPrimaryShippingAsync(int id, string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.AccountContacts
                .FilterByActive(true)
                .FilterByID(Contract.RequiresValidID(id))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (entity == null)
            {
                return CEFAR.FailingCEFAR($"ERROR! Could not locate record with ID '{id}'");
            }
            var changed = false;
            if (entity.IsBilling)
            {
                entity.IsBilling = false;
                changed = true;
            }
            if (!entity.IsPrimary)
            {
                entity.IsPrimary = true;
                changed = true;
            }
            if (changed)
            {
                entity.UpdatedDate = timestamp;
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            // Now that we have set this record, update all others in the account to not have this value
            changed = false;
            var ids = await context.AccountContacts
                .FilterByActive(true)
                .FilterByExcludedID(entity.ID)
                .FilterIAmARelationshipTableByMasterID<AccountContact, Account, Contact>(entity.MasterID)
                .Where(x => x.IsPrimary)
                .Select(x => x.ID)
                .ToListAsync()
                .ConfigureAwait(false);
            if (Contract.CheckNotEmpty(ids))
            {
                foreach (var e in context.AccountContacts.FilterByIDs(ids))
                {
                    e.IsPrimary = false;
                    e.UpdatedDate = timestamp;
                    changed = true;
                }
            }
            if (changed)
            {
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> MarkAccountContactAsDefaultBillingAsync(int id, string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.AccountContacts
                .FilterByActive(true)
                .FilterByID(Contract.RequiresValidID(id))
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (entity == null)
            {
                return CEFAR.FailingCEFAR($"ERROR! Could not locate record with ID '{id}'");
            }
            var changed = false;
            if (!entity.IsBilling)
            {
                entity.IsBilling = true;
                changed = true;
            }
            if (entity.IsPrimary)
            {
                entity.IsPrimary = false;
                changed = true;
            }
            if (changed)
            {
                entity.UpdatedDate = timestamp;
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            // Now that we have set this record, update all others in the account to not have this value
            changed = false;
            var ids = await context.AccountContacts
                .FilterByActive(true)
                .FilterByExcludedID(entity.ID)
                .FilterIAmARelationshipTableByMasterID<AccountContact, Account, Contact>(entity.MasterID)
                .Where(x => x.IsBilling)
                .Select(x => x.ID)
                .ToListAsync()
                .ConfigureAwait(false);
            if (Contract.CheckNotEmpty(ids))
            {
                foreach (var e in context.AccountContacts.FilterByIDs(ids))
                {
                    e.IsBilling = false;
                    e.UpdatedDate = timestamp;
                    changed = true;
                }
            }
            if (changed)
            {
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IAccountContact entity,
            IAccountContactModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateAccountContactFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            await model.Contact.AssignPrePropertiesToContactAndAddressAsync(Workflows.Addresses, context.ContextProfileName).ConfigureAwait(false);
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            entity.Slave.AssignPostPropertiesToContactAndAddress(model.Contact, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null, context.ContextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        private async Task RunLimitedRelateWorkflowsAsync(
            IAccountContact entity,
            IAccountContactModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            await AlternateRelateRequiredMasterAsync(entity, model, contextProfileName).ConfigureAwait(false);
            await RelateRequiredSlaveAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
        }

        private async Task AlternateRelateRequiredMasterAsync(
            IAccountContact entity,
            IAccountContactModel model,
            string? contextProfileName)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object ID (we aren't attempting to update it's values at all, only interested in the ID)
            // May resolve but not allowed to auto-generate
            var resolvedID = await Workflows.Accounts.ResolveToIDAsync(
                    byID: model.MasterID, // By Other ID
                    byKey: model.MasterKey, // By Flattened Other Key
                    byName: model.MasterName, // By Flattened Other Name
                    model: null, // Skip if IAmARelationshipTable and propertyName is "Master" or CalendarEventProduct or UserEventAttendance
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            // No fall-backs for this entity
            if (Contract.CheckInvalidID(resolvedID))
            {
                // Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable MasterID");
            }
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.MasterID);
            var entityObjectIsNull = entity.Master == null;
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && entity.MasterID == resolvedID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && entity.MasterID == resolvedID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // Scenario 2: They match IDs, just return
                return;
            }
            // Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
            entity.MasterID = resolvedID;
        }
    }
}
