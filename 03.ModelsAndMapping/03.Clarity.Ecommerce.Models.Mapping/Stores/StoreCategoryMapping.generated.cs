// <autogenerated>
// <copyright file="Mapping.Stores.StoreCategory.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForStoreCategory
    {
        public sealed class AnonStoreCategory : StoreCategory
        {
            // public new Store? Master { get; set; }
        }

        public static readonly Func<StoreCategory?, string?, IStoreCategoryModel?> MapStoreCategoryModelFromEntityFull = CreateStoreCategoryModelFromEntityFull;

        public static readonly Func<StoreCategory?, string?, IStoreCategoryModel?> MapStoreCategoryModelFromEntityLite = CreateStoreCategoryModelFromEntityLite;

        public static readonly Func<StoreCategory?, string?, IStoreCategoryModel?> MapStoreCategoryModelFromEntityList = CreateStoreCategoryModelFromEntityList;

        public static Func<IStoreCategory, IStoreCategoryModel, string?, IStoreCategoryModel>? CreateStoreCategoryModelFromEntityHooksFull { get; set; }

        public static Func<IStoreCategory, IStoreCategoryModel, string?, IStoreCategoryModel>? CreateStoreCategoryModelFromEntityHooksLite { get; set; }

        public static Func<IStoreCategory, IStoreCategoryModel, string?, IStoreCategoryModel>? CreateStoreCategoryModelFromEntityHooksList { get; set; }

        public static Expression<Func<StoreCategory, AnonStoreCategory>>? PreBuiltStoreCategorySQLSelectorFull { get; set; }

        public static Expression<Func<StoreCategory, AnonStoreCategory>>? PreBuiltStoreCategorySQLSelectorLite { get; set; }

        public static Expression<Func<StoreCategory, AnonStoreCategory>>? PreBuiltStoreCategorySQLSelectorList { get; set; }

        /// <summary>An <see cref="IStoreCategoryModel"/> extension method that creates a(n) <see cref="StoreCategory"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="StoreCategory"/> entity.</returns>
        public static IStoreCategory CreateStoreCategoryEntity(
            this IStoreCategoryModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IStoreCategoryModel, StoreCategory>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateStoreCategoryFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IStoreCategoryModel"/> extension method that updates a(n) <see cref="StoreCategory"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="StoreCategory"/> entity.</returns>
        public static IStoreCategory UpdateStoreCategoryFromModel(
            this IStoreCategory entity,
            IStoreCategoryModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // StoreCategory Properties
            entity.IsVisibleIn = model.IsVisibleIn;
            // StoreCategory's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenStoreCategorySQLSelectorFull()
        {
            PreBuiltStoreCategorySQLSelectorFull = x => x == null ? null! : new AnonStoreCategory
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForCategory.PreBuiltCategorySQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                IsVisibleIn = x.IsVisibleIn,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenStoreCategorySQLSelectorLite()
        {
            PreBuiltStoreCategorySQLSelectorLite = x => x == null ? null! : new AnonStoreCategory
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForCategory.PreBuiltCategorySQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                IsVisibleIn = x.IsVisibleIn,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenStoreCategorySQLSelectorList()
        {
            PreBuiltStoreCategorySQLSelectorList = x => x == null ? null! : new AnonStoreCategory
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForCategory.PreBuiltCategorySQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                IsVisibleIn = x.IsVisibleIn,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
                Master = ModelMapperForStore.PreBuiltStoreSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties
            };
        }

        public static IEnumerable<IStoreCategoryModel> SelectFullStoreCategoryAndMapToStoreCategoryModel(
            this IQueryable<StoreCategory> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreCategorySQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltStoreCategorySQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateStoreCategoryModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IStoreCategoryModel> SelectLiteStoreCategoryAndMapToStoreCategoryModel(
            this IQueryable<StoreCategory> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreCategorySQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltStoreCategorySQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateStoreCategoryModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IStoreCategoryModel> SelectListStoreCategoryAndMapToStoreCategoryModel(
            this IQueryable<StoreCategory> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreCategorySQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltStoreCategorySQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateStoreCategoryModelFromEntityList(x, contextProfileName))!;
        }

        public static IStoreCategoryModel? SelectFirstFullStoreCategoryAndMapToStoreCategoryModel(
            this IQueryable<StoreCategory> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreCategorySQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreCategorySQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateStoreCategoryModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStoreCategoryModel? SelectFirstListStoreCategoryAndMapToStoreCategoryModel(
            this IQueryable<StoreCategory> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreCategorySQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreCategorySQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateStoreCategoryModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStoreCategoryModel? SelectSingleFullStoreCategoryAndMapToStoreCategoryModel(
            this IQueryable<StoreCategory> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreCategorySQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreCategorySQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateStoreCategoryModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStoreCategoryModel? SelectSingleLiteStoreCategoryAndMapToStoreCategoryModel(
            this IQueryable<StoreCategory> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreCategorySQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreCategorySQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateStoreCategoryModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStoreCategoryModel? SelectSingleListStoreCategoryAndMapToStoreCategoryModel(
            this IQueryable<StoreCategory> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreCategorySQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreCategorySQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateStoreCategoryModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IStoreCategoryModel> results, int totalPages, int totalCount) SelectFullStoreCategoryAndMapToStoreCategoryModel(
            this IQueryable<StoreCategory> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreCategorySQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltStoreCategorySQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateStoreCategoryModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStoreCategoryModel> results, int totalPages, int totalCount) SelectLiteStoreCategoryAndMapToStoreCategoryModel(
            this IQueryable<StoreCategory> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreCategorySQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltStoreCategorySQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateStoreCategoryModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStoreCategoryModel> results, int totalPages, int totalCount) SelectListStoreCategoryAndMapToStoreCategoryModel(
            this IQueryable<StoreCategory> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreCategorySQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltStoreCategorySQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateStoreCategoryModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IStoreCategoryModel? CreateStoreCategoryModelFromEntityFull(this IStoreCategory? entity, string? contextProfileName)
        {
            return CreateStoreCategoryModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IStoreCategoryModel? CreateStoreCategoryModelFromEntityLite(this IStoreCategory? entity, string? contextProfileName)
        {
            return CreateStoreCategoryModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IStoreCategoryModel? CreateStoreCategoryModelFromEntityList(this IStoreCategory? entity, string? contextProfileName)
        {
            return CreateStoreCategoryModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IStoreCategoryModel? CreateStoreCategoryModelFromEntity(
            this IStoreCategory? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IStoreCategoryModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // StoreCategory's Properties
                    // StoreCategory's Related Objects
                    // StoreCategory's Associated Objects
                    // Additional Mappings
                    if (CreateStoreCategoryModelFromEntityHooksFull != null) { model = CreateStoreCategoryModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // StoreCategory's Properties
                    // StoreCategory's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // StoreCategory's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateStoreCategoryModelFromEntityHooksLite != null) { model = CreateStoreCategoryModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // StoreCategory's Properties
                    model.IsVisibleIn = entity.IsVisibleIn;
                    // StoreCategory's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.MasterName = entity.Master?.Name;
                    model.SlaveID = entity.SlaveID;
                    model.Slave = ModelMapperForCategory.CreateCategoryModelFromEntityLite(entity.Slave, contextProfileName);
                    model.SlaveKey = entity.Slave?.CustomKey;
                    model.SlaveName = entity.Slave?.Name;
                    // StoreCategory's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateStoreCategoryModelFromEntityHooksList != null) { model = CreateStoreCategoryModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
