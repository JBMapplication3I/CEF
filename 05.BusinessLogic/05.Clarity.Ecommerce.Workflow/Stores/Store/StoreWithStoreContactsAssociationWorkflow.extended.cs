// <copyright file="StoreWithStoreContactsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate store contacts workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;

    public partial class StoreWithStoreContactsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IStoreContactModel model,
            IStoreContact entity,
            IClarityEcommerceEntities context)
        {
            return entity.Name == model.Name
                // Compare ID, but nothing deeper
                && entity.MasterID == model.StoreID
                // Compare Contact Properties
                && entity.ContactID == model.ContactID
                && entity.Contact!.CustomKey == model.Contact!.CustomKey
                && entity.Contact.Email1 == model.Contact.Email1
                && entity.Contact.Fax1 == model.Contact.Fax1
                && entity.Contact.FirstName == model.Contact.FirstName
                && entity.Contact.MiddleName == model.Contact.MiddleName
                && entity.Contact.LastName == model.Contact.LastName
                && entity.Contact.FullName == model.Contact.FullName
                && entity.Contact.Phone1 == model.Contact.Phone1
                && entity.Contact.Phone2 == model.Contact.Phone2
                && entity.Contact.Phone3 == model.Contact.Phone3
                && entity.Contact.Website1 == model.Contact.Website1
                && entity.Contact.TypeID == model.Contact.TypeID
                // Compare Contact.Address Properties
                && entity.Contact.AddressID == model.Contact.AddressID
                && entity.Contact.Address!.CustomKey == model.Contact.Address!.CustomKey
                && entity.Contact.Address.RegionID == model.Contact.Address.RegionID
                && entity.Contact.Address.CountryID == model.Contact.Address.CountryID
                && entity.Contact.Address.City == model.Contact.Address.City
                && entity.Contact.Address.Street1 == model.Contact.Address.Street1
                && entity.Contact.Address.Street2 == model.Contact.Address.Street2
                && entity.Contact.Address.Street3 == model.Contact.Address.Street3
                && entity.Contact.Address.PostalCode == model.Contact.Address.PostalCode
                && entity.Contact.Address.Latitude == model.Contact.Address.Latitude
                && entity.Contact.Address.Longitude == model.Contact.Address.Longitude
                && entity.Contact.Address.CountryCustom == model.Contact.Address.CountryCustom
                && entity.Contact.Address.RegionCustom == model.Contact.Address.RegionCustom;
        }

        /// <inheritdoc/>
        protected override Task DeactivateObjectAdditionalPropertiesAsync(
            IStoreContact entity,
            DateTime timestamp)
        {
            if (entity.Contact == null)
            {
                return Task.CompletedTask;
            }
            entity.Contact.UpdatedDate = timestamp;
            entity.Contact.Active = false;
            if (entity.Contact.Address == null)
            {
                return Task.CompletedTask;
            }
            entity.Contact.Address.UpdatedDate = timestamp;
            entity.Contact.Address.Active = false;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override Task ModelToNewObjectAdditionalPropertiesAsync(
            IStoreContact newEntity,
            IStoreContactModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.AssignPostPropertiesToContactAndAddressOnRelationshipTable(model, timestamp, context.ContextProfileName);
            return Task.CompletedTask;
        }
    }
}
