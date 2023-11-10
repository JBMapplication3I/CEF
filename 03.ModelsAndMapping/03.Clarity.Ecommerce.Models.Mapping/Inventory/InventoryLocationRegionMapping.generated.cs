// <autogenerated>
// <copyright file="Mapping.Inventory.InventoryLocationRegion.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Inventory section of the Mapping class</summary>
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

    public static partial class ModelMapperForInventoryLocationRegion
    {
        public sealed class AnonInventoryLocationRegion : InventoryLocationRegion
        {
            // public new InventoryLocation? Master { get; set; }
        }

        public static readonly Func<InventoryLocationRegion?, string?, IInventoryLocationRegionModel?> MapInventoryLocationRegionModelFromEntityFull = CreateInventoryLocationRegionModelFromEntityFull;

        public static readonly Func<InventoryLocationRegion?, string?, IInventoryLocationRegionModel?> MapInventoryLocationRegionModelFromEntityLite = CreateInventoryLocationRegionModelFromEntityLite;

        public static readonly Func<InventoryLocationRegion?, string?, IInventoryLocationRegionModel?> MapInventoryLocationRegionModelFromEntityList = CreateInventoryLocationRegionModelFromEntityList;

        public static Func<IInventoryLocationRegion, IInventoryLocationRegionModel, string?, IInventoryLocationRegionModel>? CreateInventoryLocationRegionModelFromEntityHooksFull { get; set; }

        public static Func<IInventoryLocationRegion, IInventoryLocationRegionModel, string?, IInventoryLocationRegionModel>? CreateInventoryLocationRegionModelFromEntityHooksLite { get; set; }

        public static Func<IInventoryLocationRegion, IInventoryLocationRegionModel, string?, IInventoryLocationRegionModel>? CreateInventoryLocationRegionModelFromEntityHooksList { get; set; }

        public static Expression<Func<InventoryLocationRegion, AnonInventoryLocationRegion>>? PreBuiltInventoryLocationRegionSQLSelectorFull { get; set; }

        public static Expression<Func<InventoryLocationRegion, AnonInventoryLocationRegion>>? PreBuiltInventoryLocationRegionSQLSelectorLite { get; set; }

        public static Expression<Func<InventoryLocationRegion, AnonInventoryLocationRegion>>? PreBuiltInventoryLocationRegionSQLSelectorList { get; set; }

        /// <summary>An <see cref="IInventoryLocationRegionModel"/> extension method that creates a(n) <see cref="InventoryLocationRegion"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="InventoryLocationRegion"/> entity.</returns>
        public static IInventoryLocationRegion CreateInventoryLocationRegionEntity(
            this IInventoryLocationRegionModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IInventoryLocationRegionModel, InventoryLocationRegion>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateInventoryLocationRegionFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IInventoryLocationRegionModel"/> extension method that updates a(n) <see cref="InventoryLocationRegion"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="InventoryLocationRegion"/> entity.</returns>
        public static IInventoryLocationRegion UpdateInventoryLocationRegionFromModel(
            this IInventoryLocationRegion entity,
            IInventoryLocationRegionModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // InventoryLocationRegion's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenInventoryLocationRegionSQLSelectorFull()
        {
            PreBuiltInventoryLocationRegionSQLSelectorFull = x => x == null ? null! : new AnonInventoryLocationRegion
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForRegion.PreBuiltRegionSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenInventoryLocationRegionSQLSelectorLite()
        {
            PreBuiltInventoryLocationRegionSQLSelectorLite = x => x == null ? null! : new AnonInventoryLocationRegion
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForRegion.PreBuiltRegionSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenInventoryLocationRegionSQLSelectorList()
        {
            PreBuiltInventoryLocationRegionSQLSelectorList = x => x == null ? null! : new AnonInventoryLocationRegion
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForRegion.PreBuiltRegionSQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
                Master = ModelMapperForInventoryLocation.PreBuiltInventoryLocationSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties
            };
        }

        public static IEnumerable<IInventoryLocationRegionModel> SelectFullInventoryLocationRegionAndMapToInventoryLocationRegionModel(
            this IQueryable<InventoryLocationRegion> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationRegionSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltInventoryLocationRegionSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationRegionModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IInventoryLocationRegionModel> SelectLiteInventoryLocationRegionAndMapToInventoryLocationRegionModel(
            this IQueryable<InventoryLocationRegion> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationRegionSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltInventoryLocationRegionSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationRegionModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IInventoryLocationRegionModel> SelectListInventoryLocationRegionAndMapToInventoryLocationRegionModel(
            this IQueryable<InventoryLocationRegion> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationRegionSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltInventoryLocationRegionSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationRegionModelFromEntityList(x, contextProfileName))!;
        }

        public static IInventoryLocationRegionModel? SelectFirstFullInventoryLocationRegionAndMapToInventoryLocationRegionModel(
            this IQueryable<InventoryLocationRegion> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationRegionSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltInventoryLocationRegionSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationRegionModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IInventoryLocationRegionModel? SelectFirstListInventoryLocationRegionAndMapToInventoryLocationRegionModel(
            this IQueryable<InventoryLocationRegion> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationRegionSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltInventoryLocationRegionSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationRegionModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IInventoryLocationRegionModel? SelectSingleFullInventoryLocationRegionAndMapToInventoryLocationRegionModel(
            this IQueryable<InventoryLocationRegion> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationRegionSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltInventoryLocationRegionSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationRegionModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IInventoryLocationRegionModel? SelectSingleLiteInventoryLocationRegionAndMapToInventoryLocationRegionModel(
            this IQueryable<InventoryLocationRegion> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationRegionSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltInventoryLocationRegionSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationRegionModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IInventoryLocationRegionModel? SelectSingleListInventoryLocationRegionAndMapToInventoryLocationRegionModel(
            this IQueryable<InventoryLocationRegion> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationRegionSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltInventoryLocationRegionSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationRegionModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IInventoryLocationRegionModel> results, int totalPages, int totalCount) SelectFullInventoryLocationRegionAndMapToInventoryLocationRegionModel(
            this IQueryable<InventoryLocationRegion> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationRegionSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltInventoryLocationRegionSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateInventoryLocationRegionModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IInventoryLocationRegionModel> results, int totalPages, int totalCount) SelectLiteInventoryLocationRegionAndMapToInventoryLocationRegionModel(
            this IQueryable<InventoryLocationRegion> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationRegionSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltInventoryLocationRegionSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateInventoryLocationRegionModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IInventoryLocationRegionModel> results, int totalPages, int totalCount) SelectListInventoryLocationRegionAndMapToInventoryLocationRegionModel(
            this IQueryable<InventoryLocationRegion> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationRegionSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltInventoryLocationRegionSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateInventoryLocationRegionModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IInventoryLocationRegionModel? CreateInventoryLocationRegionModelFromEntityFull(this IInventoryLocationRegion? entity, string? contextProfileName)
        {
            return CreateInventoryLocationRegionModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IInventoryLocationRegionModel? CreateInventoryLocationRegionModelFromEntityLite(this IInventoryLocationRegion? entity, string? contextProfileName)
        {
            return CreateInventoryLocationRegionModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IInventoryLocationRegionModel? CreateInventoryLocationRegionModelFromEntityList(this IInventoryLocationRegion? entity, string? contextProfileName)
        {
            return CreateInventoryLocationRegionModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IInventoryLocationRegionModel? CreateInventoryLocationRegionModelFromEntity(
            this IInventoryLocationRegion? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IInventoryLocationRegionModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // InventoryLocationRegion's Properties
                    // InventoryLocationRegion's Related Objects
                    // InventoryLocationRegion's Associated Objects
                    // Additional Mappings
                    if (CreateInventoryLocationRegionModelFromEntityHooksFull != null) { model = CreateInventoryLocationRegionModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // InventoryLocationRegion's Properties
                    // InventoryLocationRegion's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // InventoryLocationRegion's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateInventoryLocationRegionModelFromEntityHooksLite != null) { model = CreateInventoryLocationRegionModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // InventoryLocationRegion's Properties
                    // InventoryLocationRegion's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.MasterName = entity.Master?.Name;
                    model.SlaveID = entity.SlaveID;
                    model.Slave = ModelMapperForRegion.CreateRegionModelFromEntityLite(entity.Slave, contextProfileName);
                    model.SlaveKey = entity.Slave?.CustomKey;
                    model.SlaveName = entity.Slave?.Name;
                    // InventoryLocationRegion's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateInventoryLocationRegionModelFromEntityHooksList != null) { model = CreateInventoryLocationRegionModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
