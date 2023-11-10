// <copyright file="AddressBookWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address book workflow class</summary>
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
    using Models;
    using Utilities;

    /// <summary>The address book workflow.</summary>
    public class AddressBookWorkflow : IAddressBookWorkflow
    {
        /// <summary>The workflows.</summary>
        private readonly IWorkflowsController workflows
            = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();

        /// <inheritdoc/>
        public Task<List<IAccountContactModel>> GetAddressBookAsync(int accountID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(context.AccountContacts
                .AsNoTracking()
                .FilterAccountContactsByActive()
                .FilterAccountContactsByEndDate(DateExtensions.GenDateTime)
                .FilterAccountContactsByAccountID(accountID)
                .SelectLiteAccountContactAndMapToAccountContactModel(contextProfileName)
                .Where(x => x.Slave!.TypeID == 4
                        || (x.Slave.TypeID == 1
                            && x.Slave.SerializableAttributes.TryGetValue("userId", out var userId)
                            && userId.Value == accountID.ToString()))
                .ToList());
        }

        /// <inheritdoc/>
        public Task<IAccountContactModel?> GetAddressBookPrimaryShippingAsync(int accountID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.AccountContacts
                    .AsNoTracking()
                    .FilterAccountContactsByActive()
                    .FilterAccountContactsByAccountID(accountID)
                    .Where(x => x.IsPrimary)
                    .SelectSingleFullAccountContactAndMapToAccountContactModel(contextProfileName));
        }

        /// <inheritdoc/>
        public Task<IAccountContactModel?> GetAddressBookPrimaryBillingAsync(int accountID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.AccountContacts
                    .AsNoTracking()
                    .FilterAccountContactsByActive()
                    .FilterAccountContactsByAccountID(accountID)
                    .Where(x => x.IsBilling)
                    .SelectSingleFullAccountContactAndMapToAccountContactModel(contextProfileName));
        }

        /// <inheritdoc/>
        public async Task<IAccountContactModel?> CreateAddressInBookAsync(IAccountContactModel model, int? currentUserID, string? contextProfileName)
        {
            if (model.Contact?.Address is null)
            {
                return null;
            }
            var timestamp = DateExtensions.GenDateTime;
            // Create address in Geography.Address
            model.CreatedDate = model.Contact.CreatedDate = model.Contact.Address.CreatedDate = timestamp;
            model.Active = model.Contact.Active = model.Contact.Address.Active = true;
            model.SerializableAttributes ??= new SerializableAttributesDictionary();
            model.SerializableAttributes.TryAdd("userID", new SerializableAttributeObject { Key = "userID", Value = currentUserID.ToString() });
            // ReSharper disable once PossibleInvalidOperationException
            var contactCreateResponse = await workflows.Contacts.CreateAsync(model.Contact, contextProfileName).ConfigureAwait(false);
            // Link address to account but only if you have data to use
            if (model.MasterID <= 0 || !contactCreateResponse.ActionSucceeded)
            {
                return model;
            }
            if (model.IsBilling && model.IsPrimary)
            {
                model.IsPrimary = false; // Billing wins
            }
            if (model.IsBilling)
            {
                // ALl others must not be billing
                var book = await GetAddressBookAsync(model.MasterID, contextProfileName).ConfigureAwait(false);
                foreach (var entry in book.Where(x => x.IsBilling))
                {
                    entry.IsBilling = false;
                    await UpdateAddressInBookAsync(entry, contextProfileName).ConfigureAwait(false);
                }
            }
            if (model.IsPrimary)
            {
                // ALl others must not be primary
                var book = await GetAddressBookAsync(model.MasterID, contextProfileName).ConfigureAwait(false);
                foreach (var entry in book.Where(x => x.IsPrimary))
                {
                    entry.IsPrimary = false;
                    await UpdateAddressInBookAsync(entry, contextProfileName).ConfigureAwait(false);
                }
            }
            var newContact = (AccountContact)model.CreateAccountContactEntity(timestamp, contextProfileName);
            newContact.Active = true;
            newContact.SlaveID = contactCreateResponse.Result;
            newContact.MasterID = model.MasterID;
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                context.AccountContacts.Add(newContact);
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            return GetAccountAddress(newContact.ID, contextProfileName);
        }

        /// <inheritdoc/>
        public async Task<IAccountContactModel?> UpdateAddressInBookAsync(IAccountContactModel model, string? contextProfileName)
        {
            // Update Contact
            await workflows.Contacts.UpdateAsync(model.Contact!, contextProfileName).ConfigureAwait(false);
            // Update AccountAddress
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var accountContact = context.AccountContacts.SingleOrDefault(ad => ad.ID == model.ID);
            if (accountContact == null)
            {
                throw new ArgumentException("Must provide an ID which matches an Account Address");
            }
            accountContact.Name = model.Name;
            accountContact.IsBilling = model.IsBilling;
            accountContact.IsPrimary = model.IsPrimary;
            accountContact.UpdatedDate = DateExtensions.GenDateTime;
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            return GetAccountAddress(accountContact.ID, contextProfileName);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> DeactivateAddressInBookAsync(int id, string? contextProfileName)
        {
            Contract.RequiresValidID(id);
            // Locate Entity
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = context.AccountContacts.SingleOrDefault(ad => ad.ID == id);
            if (entity == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Record not found");
            }
            // Validate Entity against action
            if (!entity.Active)
            {
                // No need to update, return positive completion
                return CEFAR.PassingCEFAR();
            }
            // Perform Deactivate on the secondary object
            if (entity.Slave is not null)
            {
                entity.Slave.Active = false;
                entity.Slave.UpdatedDate = DateExtensions.GenDateTime;
                if (entity.Slave.Address is not null)
                {
                    entity.Slave.Address.Active = false;
                    entity.Slave.Address.UpdatedDate = DateExtensions.GenDateTime;
                }
            }
            // Perform Deactivate on the primary object
            return await DeactivateAsync(entity, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> DeactivateAddressInBookAsync(string key, string? contextProfileName)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            Contract.Requires<ArgumentOutOfRangeException>(!string.IsNullOrWhiteSpace(key));
            // Locate Entity
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.AccountContacts.FilterByCustomKey(key).SingleOrDefaultAsync();
            if (entity == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Record not found");
            }
            // Validate Entity against action
            if (!entity.Active)
            {
                // No need to update, return positive completion
                return CEFAR.PassingCEFAR();
            }
            // Perform Deactivate on the secondary object
            if (entity.Slave is not null)
            {
                entity.Slave.Active = false;
                entity.Slave.UpdatedDate = DateExtensions.GenDateTime;
                if (entity.Slave.Address is not null)
                {
                    entity.Slave.Address.Active = false;
                    entity.Slave.Address.UpdatedDate = DateExtensions.GenDateTime;
                }
            }
            // Perform Deactivate on the primary object
            return await DeactivateAsync(entity, contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Deactivates the record.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private static async Task<CEFActionResponse> DeactivateAsync(IBase? entity, string? contextProfileName)
        {
            if (entity is null)
            {
                return CEFAR.FailingCEFAR("ERROR! Record not found");
            }
            if (!entity.Active)
            {
                return CEFAR.PassingCEFAR();
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            entity.UpdatedDate = DateExtensions.GenDateTime;
            entity.Active = false;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false)).BoolToCEFAR("ERROR! Couldn't save changes");
        }

        /// <summary>Gets account address.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The account address.</returns>
        private static IAccountContactModel? GetAccountAddress(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.AccountContacts
                .FilterByActive(true)
                .FilterByID(id)
                .SelectSingleFullAccountContactAndMapToAccountContactModel(contextProfileName);
        }
    }
}
