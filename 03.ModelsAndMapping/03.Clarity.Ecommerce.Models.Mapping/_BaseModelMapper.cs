// <copyright file="_BaseModelMapper.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base model mapper class</summary>
// ReSharper disable CyclomaticComplexity, FunctionComplexityOverflow, MissingBlankLines
// ReSharper disable StyleCop.SA1202
namespace Clarity.Ecommerce.Mapper
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Utilities;

    /// <content>A base model mapper.</content>
    public static partial class BaseModelMapper
    {
        /// <summary>An IAddressModel extension method that assign pre-properties to address.</summary>
        /// <param name="model">             The model to act on.</param>
        /// <param name="addressWorkflow">   The address workflow.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{IAddressModel}.</returns>
        public static Task<IAddressModel?> AssignPrePropertiesToAddressAsync(
            this IAddressModel? model,
            IAddressWorkflow addressWorkflow,
            string? contextProfileName)
        {
            return (model == null
                ? Task.FromResult<IAddressModel?>(null)!
                : addressWorkflow.ResolveAddressAsync(model, contextProfileName))!;
        }

        /// <summary>An IContactModel extension method that assign pre-properties to contact and address.</summary>
        /// <param name="model">             The model to act on.</param>
        /// <param name="addressWorkflow">   The address workflow.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        public static async Task AssignPrePropertiesToContactAndAddressAsync(
            this IContactModel? model,
            IAddressWorkflow addressWorkflow,
            string? contextProfileName)
        {
            if (model == null)
            {
                return;
            }
            if (!Contract.CheckValidIDOrAnyValidKey(
                    model.Type?.ID ?? model.TypeID,
                    model.TypeKey,
                    model.TypeName,
                    model.Type?.CustomKey,
                    model.Type?.Name))
            {
                model.TypeKey = "General";
            }
            if (model.Address == null)
            {
                return;
            }
            model.Address = await addressWorkflow.ResolveAddressAsync(model.Address, contextProfileName).ConfigureAwait(false);
        }

        /// <summary>The IAddress extension method that assign post properties to address.</summary>
        /// <param name="entity">   The entity to act on.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        public static void AssignPostPropertiesToAddress(
            this IAddress? entity,
            IAddressModel? model,
            DateTime timestamp)
        {
            if (entity == null || model == null)
            {
                return;
            }
            // Check for any actual address data, return unchanged if that is not the case
            if (Contract.CheckValidIDOrAnyValidKey(
                model.ID,
                model.Company,
                model.Street1,
                model.Street2,
                model.Street3,
                model.CountryKey,
                model.CountryCode,
                model.CountryCustom,
                model.CountryName,
                model.RegionKey,
                model.RegionCode,
                model.RegionCustom,
                model.RegionName,
                model.City,
                model.PostalCode))
            {
                return;
            }
            entity.UpdateAddressFromModel(model, timestamp, timestamp);
            entity.CountryID = model.CountryID;
            entity.CountryCustom = model.CountryCustom;
            entity.RegionID = model.RegionID;
            entity.RegionCustom = model.RegionCustom;
        }

        /// <summary>An IContact extension method that assign post properties to contact and address.</summary>
        /// <param name="entity">            The entity to act on.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">   The update timestamp.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        public static void AssignPostPropertiesToContactAndAddress(
            this IContact? entity,
            IContactModel? model,
            DateTime timestamp,
            DateTime? updateTimestamp,
            string? contextProfileName)
        {
            if (entity == null || model == null)
            {
                return;
            }
            // Note: We're mirroring several properties on the AccountContact and the Address being created for ease of lookup
            // Do the basic assignments from Contact Model to Contact entity
            entity.UpdateContactFromModel(model, timestamp, updateTimestamp ?? timestamp);
            if (!Contract.CheckValidID(entity.TypeID)
                && Contract.CheckValidID(model.TypeID))
            {
                entity.TypeID = model.TypeID;
            }
            if (model.Address == null)
            {
                // No Address on the Contact, just worry about Contact's properties that need to merge down
                entity.CustomKey = model.CustomKey;
                entity.Active = entity.Active = model.Active;
                entity.CreatedDate = entity.CreatedDate = timestamp;
                entity.UpdatedDate = entity.UpdatedDate = updateTimestamp;
                return;
            }
            // Have a Contact with Address, allowed to assign from it, add an instance to the new entity
            (entity.Address ??= RegistryLoaderWrapper.GetInstance<Address>(contextProfileName))
                .UpdateAddressFromModel(model.Address, timestamp, updateTimestamp ?? timestamp);
            // Merge down Properties
            entity.CustomKey = model.CustomKey ?? model.Address.CustomKey;
            entity.Address.CustomKey = model.Address.CustomKey ?? model.CustomKey;
            entity.Active = entity.Active = entity.Address.Active = true;
            entity.CreatedDate = entity.CreatedDate = entity.Address.CreatedDate = timestamp;
            entity.UpdatedDate = entity.UpdatedDate = entity.Address.UpdatedDate = updateTimestamp;
            entity.Address.Company = model.Address.Company;
            // Related Objects, these should be resolved using AddressWorkflow.Resolve before this associate workflow is called
            entity.Address.CountryID = model.Address.CountryID;
            entity.Address.CountryCustom = model.Address.CountryCustom;
            entity.Address.RegionID = model.Address.RegionID;
            entity.Address.RegionCustom = model.Address.RegionCustom;
        }

        /// <summary>An IAmAContactRelationshipTable{TMaster} extension method that assign post properties to contact and
        /// address on relationship table.</summary>
        /// <typeparam name="TMaster"> Type of the master.</typeparam>
        /// <typeparam name="TContact">Type of the contact.</typeparam>
        /// <param name="newEntity">         The newEntity to act on.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        public static void AssignPostPropertiesToContactAndAddressOnRelationshipTable<TMaster, TContact>(
                this IAmAContactRelationshipTable<TMaster, TContact> newEntity,
                IAmARelationshipTableBaseModel<IContactModel> model,
                DateTime timestamp,
                string? contextProfileName)
            where TMaster : IBase
            where TContact : IAmAContactRelationshipTable<TMaster, TContact>
        {
            // Note: We're mirroring several properties on the AccountContact and the Address being created for ease of lookup
            // Try To set CustomKey(s) from closest association to farthest
            if (model.Slave == null)
            {
                // No Contact, don't add one to the new entity and just assign values from this base
                newEntity.CustomKey = model.CustomKey;
                newEntity.Active = model.Active;
                newEntity.CreatedDate = timestamp;
                newEntity.UpdatedDate = null;
                return;
            }
            // Have a contact, allowed to assign from it, add an instance to the new entity
            // Do the basic assignments from Contact Model to Contact entity
            (newEntity.Slave ??= RegistryLoaderWrapper.GetInstance<Contact>(contextProfileName))
                .UpdateContactFromModel(model.Slave, timestamp, timestamp);
            newEntity.Slave.TypeID = model.Slave.TypeID;
            if (model.Slave.Address == null)
            {
                // No Address on the contact, just worry about Contact's properties that need to merge down
                newEntity.CustomKey = model.CustomKey ?? model.Slave.CustomKey;
                newEntity.Active = newEntity.Slave.Active = model.Active;
                newEntity.CreatedDate = newEntity.Slave.CreatedDate = timestamp;
                newEntity.UpdatedDate = newEntity.Slave.UpdatedDate = null;
                return;
            }
            // Have a contact with address, allowed to assign from it, add an instance to the new entity
            (newEntity.Slave.Address ?? (newEntity.Slave.Address = RegistryLoaderWrapper.GetInstance<Address>(contextProfileName)))
                .UpdateAddressFromModel(model.Slave.Address, timestamp, timestamp);
            // Merge down Properties
            newEntity.CustomKey = model.CustomKey ?? model.Slave.CustomKey ?? model.Slave.Address.CustomKey;
            newEntity.Slave.CustomKey = model.Slave.CustomKey ?? model.CustomKey ?? model.Slave.Address.CustomKey;
            newEntity.Slave.Address.CustomKey = model.Slave.Address.CustomKey ?? model.Slave.CustomKey ?? model.CustomKey;
            newEntity.Active = newEntity.Slave.Active = newEntity.Slave.Address.Active = true;
            newEntity.CreatedDate = newEntity.Slave.CreatedDate = newEntity.Slave.Address.CreatedDate = timestamp;
            newEntity.UpdatedDate = newEntity.Slave.UpdatedDate = newEntity.Slave.Address.UpdatedDate = null;
            newEntity.Slave.Address.Company = model.Slave.Address.Company;
            // Related Objects, these should be resolved using AddressWorkflow.Resolve before this associate workflow is called
            newEntity.Slave.Address.CountryID = model.Slave.Address.CountryID;
            newEntity.Slave.Address.CountryCustom = model.Slave.Address.CountryCustom;
            newEntity.Slave.Address.RegionID = model.Slave.Address.RegionID;
            newEntity.Slave.Address.RegionCustom = model.Slave.Address.RegionCustom;
        }

        #region Base Properties: ID,CustomKey,Active,CreatedDate,UpdatedDate,Hash
        /// <summary>A TModel extension method that creates entity base.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new entity base.</returns>
        internal static TEntity CreateEntityFromModelBase<TModel, TEntity>(
                this TModel model,
                DateTime timestamp,
                string? contextProfileName)
            where TModel : IBaseModel
            where TEntity : IBase
        {
            var entity = Contract.RequiresNotNull(RegistryLoaderWrapper.GetInstance<TEntity>(contextProfileName))
                .MapBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            entity.CreatedDate = timestamp; // Override the CreatedDate
            return entity;
        }

        /// <summary>A TEntity extension method that maps base model properties to entity.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="model"> The model.</param>
        /// <returns>A TEntity.</returns>
        internal static TEntity MapBaseModelPropertiesToEntity<TModel, TEntity>(this TEntity entity, TModel model)
            where TModel : IBaseModel
            where TEntity : IBase
        {
            Contract.RequiresNotNull(entity);
            entity.Active = Contract.RequiresNotNull(model).Active;
            entity.CustomKey = model.CustomKey;
            entity.CreatedDate = model.CreatedDate;
            entity.UpdatedDate = model.UpdatedDate;
            entity.Hash = model.Hash;
            entity.JsonAttributes = model.SerializableAttributes.SerializeAttributesDictionary();
            return entity;
        }

        /// <summary>A TModel extension method that map base entity properties to model.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="mode">              The mode.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TModel.</returns>
        internal static TModel MapBaseEntityPropertiesToModel<TModel>(
                this TModel model,
                dynamic entity,
                MappingMode mode,
#pragma warning disable IDE0060 // Remove unused parameter
                string? contextProfileName)
#pragma warning restore IDE0060 // Remove unused parameter
            where TModel : IBaseModel
        {
            Contract.RequiresNotNull(model);
            Contract.RequiresNotNull(entity);
            switch (mode)
            {
                case MappingMode.Full:
                {
                    // Nothing specific here, just start with Lite
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite:
                {
                    // Add Updated Date
                    model.UpdatedDate = entity.UpdatedDate;
                    goto case MappingMode.List;
                }
                case MappingMode.List:
                default:
                {
                    // These are mapped at all times (they are important for lists or otherwise would still transmit data if not sent)
                    model.ID = entity.ID;
                    model.CustomKey = entity.CustomKey;
                    model.Active = entity.Active;
                    model.CreatedDate = entity.CreatedDate;
                    model.Hash = entity.Hash;
                    break;
                }
            }
            return model;
        }
        #endregion

        #region NameableBase Properties: Name,Description
        /// <summary>A TModel extension method that creates entity from model nameable base.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new entity nameable base.</returns>
        internal static TEntity CreateEntityFromModelNameableBase<TModel, TEntity>(
                this TModel model,
                DateTime timestamp,
                string? contextProfileName)
            where TModel : INameableBaseModel
            where TEntity : INameableBase
        {
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<TModel, TEntity>(timestamp, contextProfileName)
                .MapNameableBaseModelPropertiesToEntity(model);
        }

        /// <summary>A TEntity extension method that maps nameable base model properties to entity.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="model"> The model.</param>
        /// <returns>A TEntity.</returns>
        internal static TEntity MapNameableBaseModelPropertiesToEntity<TModel, TEntity>(this TEntity entity, TModel model)
            where TModel : INameableBaseModel
            where TEntity : INameableBase
        {
            entity = entity.MapBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            entity.Name = model.Name;
            entity.Description = model.Description;
            return entity;
        }

        /// <summary>A TModel extension method that maps nameable base entity properties to model.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="mode">              The mode.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TModel.</returns>
        internal static TModel MapNameableBaseEntityPropertiesToModel<TModel>(
                this TModel model,
                dynamic entity,
                MappingMode mode,
                string? contextProfileName)
            where TModel : INameableBaseModel
        {
            // Map the Inherited properties
            model = MapBaseEntityPropertiesToModel(model, Contract.RequiresNotNull(entity), mode, contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full:
                {
                    // Nothing specific here, just start with Lite
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite:
                {
                    // Add Description
                    model.Description = entity.Description;
                    goto case MappingMode.List;
                }
                case MappingMode.List:
                default:
                {
                    // These are mapped at all times (they are important for lists or otherwise would still transmit data if not sent)
                    model.Name = entity.Name;
                    break;
                }
            }
            return model;
        }
        #endregion

        #region DisplayableBase Properties: DisplayName, TranslationKey, SortOrder
        /// <summary>A TModel extension method that creates entity displayable base.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new entity displayable base.</returns>
        internal static TEntity CreateEntityFromModelDisplayableBase<TModel, TEntity>(
                this TModel model,
                DateTime timestamp,
                string? contextProfileName)
            where TModel : IDisplayableBaseModel
            where TEntity : IDisplayableBase
        {
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelNameableBase<TModel, TEntity>(timestamp, contextProfileName)
                .MapDisplayableBaseModelPropertiesToEntity(model);
        }

        /// <summary>A TModel extension method that maps displayable base entity properties to the model.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="mode">              The mode.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TModel.</returns>
        internal static TModel MapDisplayableBaseEntityPropertiesToModel<TModel>(
                this TModel model,
                dynamic entity,
                MappingMode mode,
                string? contextProfileName)
            where TModel : IDisplayableBaseModel
        {
            model = MapNameableBaseEntityPropertiesToModel(
                Contract.RequiresNotNull(model),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            model.DisplayName = entity.DisplayName;
            model.TranslationKey = entity.TranslationKey;
            model.SortOrder = entity.SortOrder;
            return model;
        }

        /// <summary>A TEntity extension method that map displayable base model properties to entity.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="model"> The model.</param>
        /// <returns>A TEntity.</returns>
        internal static TEntity MapDisplayableBaseModelPropertiesToEntity<TModel, TEntity>(this TEntity entity, TModel model)
            where TModel : IDisplayableBaseModel
            where TEntity : IDisplayableBase
        {
            entity = Contract.RequiresNotNull(entity)
                .MapNameableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            entity.DisplayName = model.DisplayName;
            entity.TranslationKey = model.TranslationKey;
            entity.SortOrder = model.SortOrder;
            return entity;
        }
        #endregion

        #region TypableBase Properties: None at this level
        /// <summary>A TModel extension method that creates entity typable base.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new entity typable base.</returns>
        internal static TEntity CreateEntityTypableBase<TModel, TEntity>(
                this TModel model,
                DateTime timestamp,
                string? contextProfileName)
            where TModel : ITypableBaseModel
            where TEntity : ITypableBase
        {
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelDisplayableBase<TModel, TEntity>(timestamp, contextProfileName)
                .MapDisplayableBaseModelPropertiesToEntity(model);
        }

        /// <summary>A TModel extension method that maps typable base entity properties to the model.</summary>
        /// <typeparam name="TModel">Type of the model.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="mode">              The mode.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TModel.</returns>
        internal static TModel MapTypableBaseEntityPropertiesToModel<TModel>(
                this TModel model,
                dynamic entity,
                MappingMode mode,
                string? contextProfileName)
            where TModel : ITypableBaseModel
        {
            return MapDisplayableBaseEntityPropertiesToModel(
                Contract.RequiresNotNull(model),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
        }

        /// <summary>A TEntity extension method that map typable base model properties to entity.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="model"> The model.</param>
        /// <returns>A TEntity.</returns>
        internal static TEntity MapTypableBaseModelPropertiesToEntity<TModel, TEntity>(this TEntity entity, TModel model)
            where TModel : ITypableBaseModel
            where TEntity : ITypableBase
        {
            return Contract.RequiresNotNull(entity)
                .MapDisplayableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
        }

        /// <summary>A TEntity extension method that creates type model from typable base entity.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="entity">            The entity.</param>
        /// <param name="mode">              The mode.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new type model from typable base entity.</returns>
        internal static ITypeModel? CreateTypeModelFromTypableBaseEntity<TEntity>(
                this TEntity? entity,
                MappingMode mode,
                string? contextProfileName)
            where TEntity : ITypableBase
        {
            if (entity == null)
            {
                return null;
            }
            return MapDisplayableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<ITypeModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
        }
        #endregion

        #region StatusableBase Properties: None at this Level
        /// <summary>A TModel extension method that creates entity statusable base.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new entity statusable base.</returns>
        internal static TEntity CreateEntityStatusableBase<TModel, TEntity>(
                this TModel model,
                DateTime timestamp,
                string? contextProfileName)
            where TModel : IStatusableBaseModel
            where TEntity : IStatusableBase
        {
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelDisplayableBase<TModel, TEntity>(timestamp, contextProfileName)
                .MapDisplayableBaseModelPropertiesToEntity(model);
        }

        /// <summary>A TModel extension method that maps statusable base entity properties to the model.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="mode">              The mode.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TModel.</returns>
        internal static TModel MapStatusableBaseEntityPropertiesToModel<TModel, TEntity>(
                this TModel model,
                TEntity entity,
                MappingMode mode,
                string? contextProfileName)
            where TModel : IStatusableBaseModel
            where TEntity : IStatusableBase
        {
            return Contract.RequiresNotNull(model)
                .MapDisplayableBaseEntityPropertiesToModel(
                    Contract.RequiresNotNull(entity),
                    mode,
                    contextProfileName);
        }

        /// <summary>A TEntity extension method that map statusable base model properties to entity.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="model"> The model.</param>
        /// <returns>A TEntity.</returns>
        internal static TEntity MapStatusableBaseModelPropertiesToEntity<TModel, TEntity>(this TEntity entity, TModel model)
            where TModel : IStatusableBaseModel
            where TEntity : IStatusableBase
        {
            return Contract.RequiresNotNull(entity)
                .MapDisplayableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
        }

        /// <summary>A TEntity extension method that creates type model from statusable base entity.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="entity">            The entity.</param>
        /// <param name="mode">              The mode.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new type model from typable base entity.</returns>
        internal static IStatusModel CreateStatusModelFromStatusableBaseEntity<TEntity>(
                this TEntity entity,
                MappingMode mode,
                string? contextProfileName)
            where TEntity : IStatusableBase
        {
            return MapDisplayableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IStatusModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
        }
        #endregion

        #region StateableBase Properties: None at this Level
        /// <summary>A TModel extension method that creates entity stateable base.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new entity stateable base.</returns>
        internal static TEntity CreateEntityStateableBase<TModel, TEntity>(
                this TModel model,
                DateTime timestamp,
                string? contextProfileName)
            where TModel : IStateableBaseModel
            where TEntity : IStateableBase
        {
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelDisplayableBase<TModel, TEntity>(timestamp, contextProfileName)
                .MapDisplayableBaseModelPropertiesToEntity(model);
        }

        /// <summary>A TModel extension method that maps stateable base entity properties to the model.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="model">             The model.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="mode">              The mode.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TModel.</returns>
        internal static TModel MapStateableBaseEntityPropertiesToModel<TModel, TEntity>(
                this TModel model,
                TEntity entity,
                MappingMode mode,
                string? contextProfileName)
            where TModel : IStateableBaseModel
            where TEntity : IStateableBase
        {
            return Contract.RequiresNotNull(model)
                .MapDisplayableBaseEntityPropertiesToModel(
                    Contract.RequiresNotNull(entity),
                    mode,
                    contextProfileName);
        }

        /// <summary>A TEntity extension method that map stateable base model properties to entity.</summary>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="model"> The model.</param>
        /// <returns>A TEntity.</returns>
        internal static TEntity MapStateableBaseModelPropertiesToEntity<TModel, TEntity>(this TEntity entity, TModel model)
            where TModel : IStateableBaseModel
            where TEntity : IStateableBase
        {
            return Contract.RequiresNotNull(entity)
                .MapDisplayableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
        }

        /// <summary>A TEntity extension method that creates type model from stateable base entity.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="entity">            The entity.</param>
        /// <param name="mode">              The mode.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new type model from typable base entity.</returns>
        // ReSharper disable once UnusedMember.Local
        internal static IStateModel CreateStateModelFromStateableBaseEntity<TEntity>(
                this TEntity entity,
                MappingMode mode,
                string? contextProfileName)
            where TEntity : IStateableBase
        {
            return MapDisplayableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IStateModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
        }
        #endregion

        // TODO: Remove this after updating all associate workflows properly
        #region IAmARelationshipTable Properties: MasterID, SlaveID
        /// <summary>A TEntity extension method that map i am a relationship table model properties to entity.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <typeparam name="TModel"> Type of the model.</typeparam>
        /// <param name="entity">The entity to act on.</param>
        /// <param name="model"> The model.</param>
        /// <returns>A TEntity.</returns>
        internal static TEntity MapIAmARelationshipTableBaseModelPropertiesToEntity<TEntity, TModel>(
                this TEntity entity,
                TModel model)
            where TEntity : IAmARelationshipTable
            where TModel : IAmARelationshipTableBaseModel
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // Map this Level's Properties
            entity.MasterID = model.MasterID;
            entity.SlaveID = model.SlaveID;
            return entity;
        }
        #endregion

        #region SalesCollectionBase Properties:
        /// <summary>A TIModel extension method that map sales collection base entity properties to model.</summary>
        /// <typeparam name="TIModel">            Type of the model's interface.</typeparam>
        /// <typeparam name="TIEntity">           Type of the entity's interface.</typeparam>
        /// <typeparam name="TEntity">            Type of the entity.</typeparam>
        /// <typeparam name="TStatus">            Type of the status.</typeparam>
        /// <typeparam name="TType">              Type of the type.</typeparam>
        /// <typeparam name="TITypeModel">        Type of the type model's interface.</typeparam>
        /// <typeparam name="TSalesItem">         Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">          Type of the discount.</typeparam>
        /// <typeparam name="TIDiscountModel">    Type of the discount model's interface.</typeparam>
        /// <typeparam name="TIItemDiscountModel">Type of the item discount model's interface.</typeparam>
        /// <typeparam name="TState">             Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">        Type of the stored file.</typeparam>
        /// <typeparam name="TIStoredFileModel">  Type of the stored file model's interface.</typeparam>
        /// <typeparam name="TContact">           Type of the contact.</typeparam>
        /// <typeparam name="TIContactModel">     Type of the contact model's interface.</typeparam>
        /// <typeparam name="TItemDiscount">      Type of the item discount.</typeparam>
        /// <typeparam name="TItemTarget">        Type of the item target.</typeparam>
        /// <typeparam name="TSalesEvent">        Type of the sales event.</typeparam>
        /// <typeparam name="TISalesEventModel">  Type of the sales event model's interface.</typeparam>
        /// <typeparam name="TSalesEventType">    Type of the sales event type.</typeparam>
        /// <param name="model">             The model to act on.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="mode">              The mode.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TIModel.</returns>
        internal static TIModel MapSalesCollectionBaseEntityPropertiesToModel<TIModel,
                TIEntity,
                TEntity,
                TStatus,
                TType,
                TITypeModel,
                TSalesItem,
                TDiscount,
                TIDiscountModel,
                TIItemDiscountModel,
                TState,
                TStoredFile,
                TIStoredFileModel,
                TContact,
                TIContactModel,
                TItemDiscount,
                TItemTarget,
                TSalesEvent,
                TISalesEventModel,
                // ReSharper disable once StyleCop.SA1110
                TSalesEventType>(this TIModel model, TIEntity entity, MappingMode mode, string? contextProfileName)
            where TIModel : ISalesCollectionBaseModel<TITypeModel, TIStoredFileModel, TIContactModel, TISalesEventModel, TIDiscountModel, TIItemDiscountModel>
            where TIEntity : ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TEntity : IHaveAppliedDiscountsBase<TEntity, TDiscount>, IHaveContactsBase<TEntity, TContact>, IHaveSalesEventsBase<TEntity, TSalesEvent, TSalesEventType>
            where TStatus : IStatusableBase
            where TType : ITypableBase
            where TITypeModel : ITypeModel
            where TSalesItem : ISalesItemBase<TSalesItem, TItemDiscount, TItemTarget>
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TIDiscountModel : IAppliedDiscountBaseModel
            where TIItemDiscountModel : IAppliedDiscountBaseModel
            where TState : IStateableBase
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TIStoredFileModel : IAmAStoredFileRelationshipTableModel
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TIContactModel : IAmAContactRelationshipTableModel
            where TItemDiscount : IAppliedDiscountBase<TSalesItem, TItemDiscount>
            where TItemTarget : ISalesItemTargetBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
            where TISalesEventModel : ISalesEventBaseModel
        {
            // Map the Inherited properties
            model = Contract.RequiresNotNull(model)
                .MapBaseEntityPropertiesToModel(Contract.RequiresNotNull(entity), mode, contextProfileName);
            // Map this level's Properties
            model.Totals = RegistryLoaderWrapper.GetInstance<ICartTotals>(contextProfileName); // Initializing here so we don't have to recheck against null
            switch (mode)
            {
                case MappingMode.Full:
                {
                    // Related Objects
#pragma warning disable CS8631
                    model.Status = entity.Status?.CreateStatusModelFromStatusableBaseEntity(mode, contextProfileName);
#pragma warning restore CS8631
                    model.Type = entity.Type switch
                    {
                        ICartType c => (TITypeModel?)c.CreateCartTypeModelFromEntityLite(contextProfileName),
                        _ => (TITypeModel?)entity.Type.CreateTypeModelFromTypableBaseEntity(mode, contextProfileName),
                    };
                    model.BillingContact = entity.BillingContact.CreateContactModelFromEntityFull(contextProfileName);
                    model.ShippingContact = entity.ShippingContact.CreateContactModelFromEntityFull(contextProfileName);
                    model.User = entity.User.CreateUserModelFromEntityLite(contextProfileName);
                    model.Account = entity.Account.CreateAccountModelFromEntityLite(contextProfileName);
                    model.Brand = entity.Brand.CreateBrandModelFromEntityLite(contextProfileName);
                    model.Franchise = entity.Franchise.CreateFranchiseModelFromEntityLite(contextProfileName);
                    model.Store = entity.Store.CreateStoreModelFromEntityLite(contextProfileName);
                    // model.Contacts = entity.Contacts?.Select(CreateContactModelFromEntityLite).ToList();
                    // Note: SalesItems and Discounts must be mapped at the upper level due to mapping constraints (different for each sales type)
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite:
                {
                    model.DueDate = entity.DueDate;
                    model.ShippingSameAsBilling = entity.ShippingSameAsBilling;
                    goto case MappingMode.List;
                }
                case MappingMode.List:
                default:
                {
                    // Totals Object
                    model.Totals.SubTotal = entity.SubtotalItems;
                    model.Totals.Shipping = entity.SubtotalShipping;
                    model.Totals.Tax = entity.SubtotalTaxes;
                    model.Totals.Fees = entity.SubtotalFees;
                    model.Totals.Handling = entity.SubtotalHandling;
                    model.Totals.Discounts = entity.SubtotalDiscounts;
                    // Related Objects Flattened
                    model.AccountID = entity.AccountID;
                    model.AccountKey = entity.Account?.CustomKey;
                    model.UserID = entity.UserID;
                    model.UserKey = entity.User?.UserName;
                    model.UserUserName = entity.User?.CustomKey;
                    model.UserContactEmail = entity.User?.Contact?.Email1;
                    model.UserContactFirstName = entity.User?.Contact?.FirstName;
                    model.UserContactLastName = entity.User?.Contact?.LastName;
                    model.BrandID = entity.BrandID;
                    model.BrandKey = entity.Brand?.CustomKey;
                    model.BrandName = entity.Brand?.Name;
                    model.FranchiseID = entity.FranchiseID;
                    model.FranchiseKey = entity.Franchise?.CustomKey;
                    model.FranchiseName = entity.Franchise?.Name;
                    model.StoreID = entity.StoreID;
                    model.StoreKey = entity.Store?.CustomKey;
                    model.StoreName = entity.Store?.Name;
                    model.BillingContactID = entity.BillingContactID;
                    model.ShippingContactID = entity.ShippingContactID;
                    model.StatusID = entity.StatusID;
                    model.StatusName = entity.Status?.Name;
                    model.StatusKey = entity.Status?.CustomKey;
                    model.TypeID = entity.TypeID;
                    model.TypeName = entity.Type?.Name;
                    model.TypeKey = entity.Type?.CustomKey;
                    // Convenience Properties
                    model.ItemQuantity = entity.SalesItems
                        ?.Where(x => x.Active)
                        .Select(x => x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m))
                        .DefaultIfEmpty(0m)
                        .Sum()
                        ?? -1m;
                    break;
                }
            }
            return model;
        }

        /// <summary>A TIEntity extension method that map sales collection base model properties to entity.</summary>
        /// <typeparam name="TIModel">        Type of the ti model.</typeparam>
        /// <typeparam name="TIEntity">       Type of the ti entity.</typeparam>
        /// <typeparam name="TEntity">        Type of the entity.</typeparam>
        /// <typeparam name="TStatus">        Type of the status.</typeparam>
        /// <typeparam name="TType">          Type of the type.</typeparam>
        /// <typeparam name="TSalesItem">     Type of the sales item.</typeparam>
        /// <typeparam name="TDiscount">      Type of the discount.</typeparam>
        /// <typeparam name="TState">         Type of the state.</typeparam>
        /// <typeparam name="TStoredFile">    Type of the stored file.</typeparam>
        /// <typeparam name="TContact">       Type of the contact.</typeparam>
        /// <typeparam name="TSalesEvent">    Type of the sales event.</typeparam>
        /// <typeparam name="TSalesEventType">Type of the sales event type.</typeparam>
        /// <param name="entity">The entity to act on.</param>
        /// <param name="model"> The model.</param>
        /// <returns>A TIEntity.</returns>
        internal static TIEntity MapSalesCollectionBaseModelPropertiesToEntity<TIModel,
                TIEntity,
                TEntity,
                TStatus,
                TType,
                TSalesItem,
                TDiscount,
                TState,
                TStoredFile,
                TContact,
                TSalesEvent,
                // ReSharper disable once StyleCop.SA1110
                TSalesEventType>(this TIEntity entity, TIModel model)
            where TIModel : ISalesCollectionBaseModel
            where TIEntity : ISalesCollectionBase<TEntity, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
            where TEntity : TIEntity
            where TStatus : IStatusableBase
            where TType : ITypableBase
            where TDiscount : IAppliedDiscountBase<TEntity, TDiscount>
            where TState : IStateableBase
            where TStoredFile : IAmAStoredFileRelationshipTable<TEntity>
            where TContact : IAmAContactRelationshipTable<TEntity, TContact>
            where TSalesItem : IBase
            where TSalesEvent : ISalesEventBase<TEntity, TSalesEventType>
            where TSalesEventType : ITypableBase
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // Map this Level's Properties
            entity.DueDate = model.DueDate;
            entity.SubtotalItems = model.Totals?.SubTotal ?? 0;
            entity.SubtotalShipping = model.Totals?.Shipping ?? 0;
            entity.SubtotalTaxes = model.Totals?.Tax ?? 0;
            entity.SubtotalFees = model.Totals?.Fees ?? 0;
            entity.SubtotalHandling = model.Totals?.Handling ?? 0;
            entity.SubtotalDiscounts = model.Totals?.Discounts ?? 0;
            entity.Total = model.Totals?.Total ?? 0;
            entity.ShippingSameAsBilling = model.ShippingSameAsBilling;
            // Return
            return entity;
        }
        #endregion

        #region SalesItemBase Properties
        /// <summary>A TModel extension method that map sales item base entity properties to model.</summary>
        /// <typeparam name="TModel">    Type of the model.</typeparam>
        /// <typeparam name="TSalesItem">Type of the sales item entity.</typeparam>
        /// <typeparam name="TDiscount"> Type of the sales item discount entity.</typeparam>
        /// <typeparam name="TTarget">   Type of the sales item target entity.</typeparam>
        /// <param name="model">             The model to act on.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="mode">              The mode.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TModel.</returns>
        internal static TModel MapSalesItemBaseEntityPropertiesToModel<TModel, TSalesItem, TDiscount, TTarget>(
                this TModel model,
                ISalesItemBase<TSalesItem, TDiscount, TTarget> entity,
                MappingMode mode,
                string? contextProfileName)
            where TModel : ISalesItemBaseModel
            where TSalesItem : IHaveAppliedDiscountsBase<TSalesItem, TDiscount>
            where TDiscount : IAppliedDiscountBase<TSalesItem, TDiscount>
            where TTarget : ISalesItemTargetBase
        {
            // Map the Inherited properties
            model = Contract.RequiresNotNull(model)
                .MapNameableBaseEntityPropertiesToModel(
                    Contract.RequiresNotNull(entity),
                    mode,
                    contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full:
                {
                    // Related Objects
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite:
                {
                    // Specifics from Product
                    model.ProductPrimaryImage = entity.Product?.Images
                        ?.Where(y => y.Active)
                        .OrderByDescending(y => y.IsPrimary)
                        .ThenBy(y => y.OriginalWidth)
                        .ThenBy(y => y.OriginalHeight)
                        .Take(1)
                        .Select(y => y.ThumbnailFileName ?? y.OriginalFileName)
                        .FirstOrDefault();
                    model.ProductDescription = entity.Product?.Description;
                    goto case MappingMode.List;
                }
                case MappingMode.List:
                default:
                {
                    // These are mapped at all times (they are important for lists or otherwise would still transmit data if not sent)
                    // SalesItem Properties
                    model.Quantity = entity.Quantity;
                    model.QuantityBackOrdered = entity.QuantityBackOrdered ?? 0m;
                    model.QuantityPreSold = entity.QuantityPreSold ?? 0m;
                    model.UnitCorePrice = entity.UnitCorePrice;
                    model.UnitSoldPrice = entity.UnitSoldPrice;
                    model.UnitCorePriceInSellingCurrency = entity.UnitCorePriceInSellingCurrency;
                    model.UnitSoldPriceInSellingCurrency = entity.UnitSoldPriceInSellingCurrency;
                    model.ItemType = Enums.ItemType.Item;
                    model.UnitOfMeasure = entity.UnitOfMeasure;
                    model.Sku = entity.Sku;
                    model.ForceUniqueLineItemKey = entity.ForceUniqueLineItemKey;
                    // Related Objects
                    model.ProductID = entity.ProductID;
                    if (entity.Product != null)
                    {
                        model.ProductKey = entity.Product.CustomKey;
                        model.ProductName = entity.Product.Name;
                        model.ProductDescription = entity.Product.Description;
                        model.ProductShortDescription = entity.Product.ShortDescription;
                        model.ProductSeoUrl = entity.Product.SeoUrl;
                        model.ProductUnitOfMeasure = entity.Product.UnitOfMeasure;
                        model.UnitOfMeasure = entity.UnitOfMeasure ?? entity.Product.UnitOfMeasure;
                        model.ProductRequiresRoles = entity.Product.RequiresRoles;
                        model.ProductRequiresRolesAlt = entity.Product.RequiresRolesAlt;
                        model.ProductTypeID = entity.Product.TypeID;
                        model.ProductTypeKey = entity.Product.Type?.CustomKey;
                        model.ProductNothingToShip = entity.Product.NothingToShip;
                        model.ProductDropShipOnly = entity.Product.DropShipOnly;
                        model.ProductMaximumPurchaseQuantity = entity.Product.MaximumPurchaseQuantity;
                        model.ProductMaximumPurchaseQuantityIfPastPurchased = entity.Product.MaximumPurchaseQuantityIfPastPurchased;
                        model.ProductMinimumPurchaseQuantity = entity.Product.MinimumPurchaseQuantity;
                        model.ProductMinimumPurchaseQuantityIfPastPurchased = entity.Product.MinimumPurchaseQuantityIfPastPurchased;
                        model.ProductMaximumBackOrderPurchaseQuantity = entity.Product.MaximumBackOrderPurchaseQuantity;
                        model.ProductMaximumBackOrderPurchaseQuantityIfPastPurchased = entity.Product.MaximumBackOrderPurchaseQuantityIfPastPurchased;
                        model.ProductMaximumBackOrderPurchaseQuantityGlobal = entity.Product.MaximumBackOrderPurchaseQuantityGlobal;
                        model.ProductMaximumPrePurchaseQuantity = entity.Product.MaximumPrePurchaseQuantity;
                        model.ProductMaximumPrePurchaseQuantityIfPastPurchased = entity.Product.MaximumPrePurchaseQuantityIfPastPurchased;
                        model.ProductMaximumPrePurchaseQuantityGlobal = entity.Product.MaximumPrePurchaseQuantityGlobal;
                        model.ProductIsDiscontinued = entity.Product.IsDiscontinued;
                        model.ProductIsUnlimitedStock = entity.Product.IsUnlimitedStock;
                        model.ProductAllowBackOrder = entity.Product.AllowBackOrder;
                        model.ProductAllowPreSale = entity.Product.AllowPreSale;
                        model.ProductIsEligibleForReturn = entity.Product.IsEligibleForReturn;
                        model.ProductRestockingFeePercent = entity.Product.RestockingFeePercent;
                        model.ProductRestockingFeeAmount = entity.Product.RestockingFeeAmount;
                        model.ProductIsTaxable = entity.Product.IsTaxable;
                        model.ProductTaxCode = entity.Product.TaxCode;
                        model.ProductSerializableAttributes = entity.Product.JsonAttributes.DeserializeAttributesDictionary();
                    }
                    model.MasterID = entity.MasterID;
                    model.UserID = entity.UserID;
                    if (entity.User != null)
                    {
                        model.UserKey = entity.User.CustomKey;
                        model.UserUserName = entity.User.UserName;
                    }
                    model.OriginalCurrencyID = entity.OriginalCurrencyID;
                    if (entity.OriginalCurrency != null)
                    {
                        model.OriginalCurrencyKey = entity.OriginalCurrency.CustomKey;
                        model.OriginalCurrencyName = entity.OriginalCurrency.Name;
                    }
                    model.SellingCurrencyID = entity.SellingCurrencyID;
                    if (entity.SellingCurrency != null)
                    {
                        model.SellingCurrencyKey = entity.SellingCurrency.CustomKey;
                        model.SellingCurrencyName = entity.SellingCurrency.Name;
                    }
                    // This is generated with the List mode in the T4
                    ////model.StatusID = entity.StatusID;
                    ////if (entity.Status != null)
                    ////{
                    ////    model.StatusKey = entity.Status.CustomKey;
                    ////    model.StatusName = entity.Status.Name;
                    ////    model.StatusDisplayName = entity.Status.DisplayName;
                    ////    model.StatusSortOrder = entity.Status.SortOrder;
                    ////}
                    break;
                }
            }
            return model;
        }
        #endregion

        /// <summary>An anon role user. This class cannot be inherited.</summary>
        /// <seealso cref="RoleUser"/>
        public sealed class AnonRoleUser : RoleUser
        {
        }
    }
}
