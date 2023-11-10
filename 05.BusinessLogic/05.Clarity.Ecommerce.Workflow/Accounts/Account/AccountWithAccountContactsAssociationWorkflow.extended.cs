// <copyright file="AccountWithAccountContactsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate account contacts workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;

    public partial class AccountWithAccountContactsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IAccountContactModel model,
            IAccountContact entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                entity.Name == model.Name
                && entity.IsBilling == model.IsBilling
                && entity.IsPrimary == model.IsPrimary
                && entity.TransmittedToERP == model.TransmittedToERP
                // Compare ID, but nothing deeper
                && entity.MasterID == model.MasterID
                // Compare Contact Properties
                && entity.SlaveID == model.ContactID
                && model.Contact != null
                && entity.Slave!.CustomKey == model.Contact.CustomKey
                && entity.Slave.Email1 == model.Contact.Email1
                && entity.Slave.Fax1 == model.Contact.Fax1
                && entity.Slave.FirstName == model.Contact.FirstName
                && entity.Slave.MiddleName == model.Contact.MiddleName
                && entity.Slave.LastName == model.Contact.LastName
                && entity.Slave.FullName == model.Contact.FullName
                && entity.Slave.Phone1 == model.Contact.Phone1
                && entity.Slave.Phone2 == model.Contact.Phone2
                && entity.Slave.Phone3 == model.Contact.Phone3
                && entity.Slave.Website1 == model.Contact.Website1
                && entity.Slave.TypeID == model.Contact.TypeID
                // Compare Contact.Address Properties
                && entity.Slave.AddressID == model.Contact.AddressID
                && model.Contact.Address != null
                && entity.Slave.Address!.CustomKey == model.Contact.Address.CustomKey
                && entity.Slave.Address.RegionID == model.Contact.Address.RegionID
                && entity.Slave.Address.CountryID == model.Contact.Address.CountryID
                && entity.Slave.Address.City == model.Contact.Address.City
                && entity.Slave.Address.Street1 == model.Contact.Address.Street1
                && entity.Slave.Address.Street2 == model.Contact.Address.Street2
                && entity.Slave.Address.Street3 == model.Contact.Address.Street3
                && entity.Slave.Address.PostalCode == model.Contact.Address.PostalCode
                && entity.Slave.Address.Latitude == model.Contact.Address.Latitude
                && entity.Slave.Address.Longitude == model.Contact.Address.Longitude
                && entity.Slave.Address.CountryCustom == model.Contact.Address.CountryCustom
                && entity.Slave.Address.RegionCustom == model.Contact.Address.RegionCustom);
        }

        /// <inheritdoc/>
        protected override Task DeactivateObjectAdditionalPropertiesAsync(IAccountContact entity, DateTime timestamp)
        {
            if (entity.Slave == null)
            {
                return Task.CompletedTask;
            }
            entity.Slave.UpdatedDate = timestamp;
            entity.Slave.Active = false;
            if (entity.Slave.Address == null)
            {
                return Task.CompletedTask;
            }
            entity.Slave.Address.UpdatedDate = timestamp;
            entity.Slave.Address.Active = false;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IAccountContact newEntity,
            IAccountContactModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Note: We're mirroring several properties on the AccountContact and the Address being created for ease of lookup
            // Try To set CustomKey(s) from closest association to farthest
            if (model.Contact == null)
            {
                // No Contact, don't add one to the new entity and just assign values from this base
                newEntity.CustomKey = model.CustomKey;
                newEntity.Active = model.Active;
                newEntity.CreatedDate = timestamp;
                newEntity.UpdatedDate = null;
                newEntity.Name = model.Name;
                return;
            }
            // Have a contact, allowed to assign from it, add an instance to the new entity
            // Do the basic assignments from Contact Model to Contact entity
            (newEntity.Slave ??= new())
                .UpdateContactFromModel(model.Contact, timestamp, timestamp);
            if (newEntity.Slave is null)
            {
                // Wrap in null check for tests
                return;
            }
            newEntity.Slave.TypeID = await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: model.Contact.TypeID,
                    byKey: model.Contact.TypeKey,
                    byName: model.Contact.TypeName,
                    model: model.Contact.Type,
                    context: context)
                .ConfigureAwait(false);
            if (model.Contact.Address == null)
            {
                // No Address on the contact, just worry about Contact's properties that need to merge down
                newEntity.CustomKey = model.CustomKey ?? model.Contact.CustomKey;
                newEntity.Active = newEntity.Slave.Active = model.Active;
                newEntity.CreatedDate = newEntity.Slave.CreatedDate = timestamp;
                newEntity.UpdatedDate = newEntity.Slave.UpdatedDate = null;
                newEntity.Name = model.Name;
                return;
            }
            // Have a contact with address, allowed to assign from it, add an instance to the new entity
            (newEntity.Slave.Address ?? (newEntity.Slave.Address = new()))
                .UpdateAddressFromModel(model.Contact.Address, timestamp, timestamp);
            // Merge down Properties
            newEntity.CustomKey = model.CustomKey ?? model.Contact.CustomKey ?? model.Contact.Address.CustomKey;
            newEntity.Slave.CustomKey = model.Contact.CustomKey ?? model.CustomKey ?? model.Contact.Address.CustomKey;
            newEntity.Slave.Address.CustomKey = model.Contact.Address.CustomKey ?? model.Contact.CustomKey ?? model.CustomKey;
            newEntity.Active = newEntity.Slave.Active = newEntity.Slave.Address.Active = true;
            newEntity.CreatedDate = newEntity.Slave.CreatedDate = newEntity.Slave.Address.CreatedDate = timestamp;
            newEntity.UpdatedDate = newEntity.Slave.UpdatedDate = newEntity.Slave.Address.UpdatedDate = null;
            newEntity.Name = model.Name;
            // Related Objects, these should be resolved using AddressWorkflow.Resolve before this associate workflow is called
            newEntity.Slave.Address.CountryID = model.Contact.Address.CountryID;
            newEntity.Slave.Address.CountryCustom = model.Contact.Address.CountryCustom;
            newEntity.Slave.Address.RegionID = model.Contact.Address.RegionID;
            newEntity.Slave.Address.RegionCustom = model.Contact.Address.RegionCustom;
        }
    }
}
