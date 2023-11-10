// <autogenerated>
// <copyright file="Mapping.Stores.StoreUser.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Stores section of the Mapping class</summary>
// <remarks>This file was auto-generated by Mapping.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
// ReSharper disable CyclomaticComplexity, FunctionComplexityOverflow, InvokeAsExtensionMethod, MergeCastWithTypeCheck
// ReSharper disable MissingLinebreak, RedundantDelegateInvoke, RedundantUsingDirective
#pragma warning disable CS0618 // Ignore Obsolete warnings
#nullable enable
namespace Clarity.Ecommerce.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using LinqKit;
    using MoreLinq;
    using Utilities;

    public static partial class ModelMapperForStoreUser
    {
        public sealed class AnonStoreUser : StoreUser
        {
            public Contact? SlaveContact { get; set; }
            // public new Store? Master { get; set; }
        }

        public static readonly Func<StoreUser?, string?, IStoreUserModel?> MapStoreUserModelFromEntityFull = CreateStoreUserModelFromEntityFull;

        public static readonly Func<StoreUser?, string?, IStoreUserModel?> MapStoreUserModelFromEntityLite = CreateStoreUserModelFromEntityLite;

        public static readonly Func<StoreUser?, string?, IStoreUserModel?> MapStoreUserModelFromEntityList = CreateStoreUserModelFromEntityList;

        public static Func<IStoreUser, IStoreUserModel, string?, IStoreUserModel>? CreateStoreUserModelFromEntityHooksFull { get; set; }

        public static Func<IStoreUser, IStoreUserModel, string?, IStoreUserModel>? CreateStoreUserModelFromEntityHooksLite { get; set; }

        public static Func<IStoreUser, IStoreUserModel, string?, IStoreUserModel>? CreateStoreUserModelFromEntityHooksList { get; set; }

        public static Expression<Func<StoreUser, AnonStoreUser>>? PreBuiltStoreUserSQLSelectorFull { get; set; }

        public static Expression<Func<StoreUser, AnonStoreUser>>? PreBuiltStoreUserSQLSelectorLite { get; set; }

        public static Expression<Func<StoreUser, AnonStoreUser>>? PreBuiltStoreUserSQLSelectorList { get; set; }

        /// <summary>An <see cref="IStoreUserModel"/> extension method that creates a(n) <see cref="StoreUser"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="StoreUser"/> entity.</returns>
        public static IStoreUser CreateStoreUserEntity(
            this IStoreUserModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IStoreUserModel, StoreUser>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateStoreUserFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IStoreUserModel"/> extension method that updates a(n) <see cref="StoreUser"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="StoreUser"/> entity.</returns>
        public static IStoreUser UpdateStoreUserFromModel(
            this IStoreUser entity,
            IStoreUserModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // StoreUser's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenStoreUserSQLSelectorFull()
        {
            PreBuiltStoreUserSQLSelectorFull = x => x == null ? null! : new AnonStoreUser
            {
                MasterID = x.MasterID,
                Master = ModelMapperForStore.PreBuiltStoreSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenStoreUserSQLSelectorLite()
        {
            PreBuiltStoreUserSQLSelectorLite = x => x == null ? null! : new AnonStoreUser
            {
                MasterID = x.MasterID,
                Master = ModelMapperForStore.PreBuiltStoreSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenStoreUserSQLSelectorList()
        {
            PreBuiltStoreUserSQLSelectorList = x => x == null ? null! : new AnonStoreUser
            {
                MasterID = x.MasterID,
                Master = ModelMapperForStore.PreBuiltStoreSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties (List)
                SlaveID = x.SlaveID,
                Slave = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IStoreUserModel> SelectFullStoreUserAndMapToStoreUserModel(
            this IQueryable<StoreUser> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreUserSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltStoreUserSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateStoreUserModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IStoreUserModel> SelectLiteStoreUserAndMapToStoreUserModel(
            this IQueryable<StoreUser> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreUserSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltStoreUserSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateStoreUserModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IStoreUserModel> SelectListStoreUserAndMapToStoreUserModel(
            this IQueryable<StoreUser> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreUserSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltStoreUserSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateStoreUserModelFromEntityList(x, contextProfileName))!;
        }

        public static IStoreUserModel? SelectFirstFullStoreUserAndMapToStoreUserModel(
            this IQueryable<StoreUser> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreUserSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreUserSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateStoreUserModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStoreUserModel? SelectFirstListStoreUserAndMapToStoreUserModel(
            this IQueryable<StoreUser> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreUserSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreUserSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateStoreUserModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStoreUserModel? SelectSingleFullStoreUserAndMapToStoreUserModel(
            this IQueryable<StoreUser> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreUserSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreUserSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateStoreUserModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStoreUserModel? SelectSingleLiteStoreUserAndMapToStoreUserModel(
            this IQueryable<StoreUser> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreUserSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreUserSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateStoreUserModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStoreUserModel? SelectSingleListStoreUserAndMapToStoreUserModel(
            this IQueryable<StoreUser> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreUserSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreUserSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateStoreUserModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IStoreUserModel> results, int totalPages, int totalCount) SelectFullStoreUserAndMapToStoreUserModel(
            this IQueryable<StoreUser> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreUserSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltStoreUserSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateStoreUserModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStoreUserModel> results, int totalPages, int totalCount) SelectLiteStoreUserAndMapToStoreUserModel(
            this IQueryable<StoreUser> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreUserSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltStoreUserSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateStoreUserModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStoreUserModel> results, int totalPages, int totalCount) SelectListStoreUserAndMapToStoreUserModel(
            this IQueryable<StoreUser> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreUserSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltStoreUserSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateStoreUserModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IStoreUserModel? CreateStoreUserModelFromEntityFull(this IStoreUser? entity, string? contextProfileName)
        {
            return CreateStoreUserModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IStoreUserModel? CreateStoreUserModelFromEntityLite(this IStoreUser? entity, string? contextProfileName)
        {
            return CreateStoreUserModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IStoreUserModel? CreateStoreUserModelFromEntityList(this IStoreUser? entity, string? contextProfileName)
        {
            return CreateStoreUserModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IStoreUserModel? CreateStoreUserModelFromEntity(
            this IStoreUser? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IStoreUserModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // StoreUser's Properties
                    // StoreUser's Related Objects
                    // StoreUser's Associated Objects
                    // Additional Mappings
                    if (CreateStoreUserModelFromEntityHooksFull != null) { model = CreateStoreUserModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // StoreUser's Properties
                    // StoreUser's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.Slave = ModelMapperForUser.CreateUserModelFromEntityLite(entity.Slave, contextProfileName);
                    // StoreUser's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateStoreUserModelFromEntityHooksLite != null) { model = CreateStoreUserModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // StoreUser's Properties
                    // StoreUser's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.MasterName = entity.Master?.Name;
                    model.SlaveID = entity.SlaveID;
                    model.SlaveKey = entity.Slave?.CustomKey;
                    model.SlaveUserName = entity.Slave?.UserName;
                    // StoreUser's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateStoreUserModelFromEntityHooksList != null) { model = CreateStoreUserModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
