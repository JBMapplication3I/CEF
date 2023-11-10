// <autogenerated>
// <copyright file="Mapping.Inventory.InventoryLocationSection.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForInventoryLocationSection
    {
        public sealed class AnonInventoryLocationSection : InventoryLocationSection
        {
            public new IEnumerable<ProductInventoryLocationSection>? ProductInventoryLocationSections { get; set; }
        }

        public static readonly Func<InventoryLocationSection?, string?, IInventoryLocationSectionModel?> MapInventoryLocationSectionModelFromEntityFull = CreateInventoryLocationSectionModelFromEntityFull;

        public static readonly Func<InventoryLocationSection?, string?, IInventoryLocationSectionModel?> MapInventoryLocationSectionModelFromEntityLite = CreateInventoryLocationSectionModelFromEntityLite;

        public static readonly Func<InventoryLocationSection?, string?, IInventoryLocationSectionModel?> MapInventoryLocationSectionModelFromEntityList = CreateInventoryLocationSectionModelFromEntityList;

        public static Func<IInventoryLocationSection, IInventoryLocationSectionModel, string?, IInventoryLocationSectionModel>? CreateInventoryLocationSectionModelFromEntityHooksFull { get; set; }

        public static Func<IInventoryLocationSection, IInventoryLocationSectionModel, string?, IInventoryLocationSectionModel>? CreateInventoryLocationSectionModelFromEntityHooksLite { get; set; }

        public static Func<IInventoryLocationSection, IInventoryLocationSectionModel, string?, IInventoryLocationSectionModel>? CreateInventoryLocationSectionModelFromEntityHooksList { get; set; }

        public static Expression<Func<InventoryLocationSection, AnonInventoryLocationSection>>? PreBuiltInventoryLocationSectionSQLSelectorFull { get; set; }

        public static Expression<Func<InventoryLocationSection, AnonInventoryLocationSection>>? PreBuiltInventoryLocationSectionSQLSelectorLite { get; set; }

        public static Expression<Func<InventoryLocationSection, AnonInventoryLocationSection>>? PreBuiltInventoryLocationSectionSQLSelectorList { get; set; }

        /// <summary>An <see cref="IInventoryLocationSectionModel"/> extension method that creates a(n) <see cref="InventoryLocationSection"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="InventoryLocationSection"/> entity.</returns>
        public static IInventoryLocationSection CreateInventoryLocationSectionEntity(
            this IInventoryLocationSectionModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelNameableBase<IInventoryLocationSectionModel, InventoryLocationSection>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateInventoryLocationSectionFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IInventoryLocationSectionModel"/> extension method that updates a(n) <see cref="InventoryLocationSection"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="InventoryLocationSection"/> entity.</returns>
        public static IInventoryLocationSection UpdateInventoryLocationSectionFromModel(
            this IInventoryLocationSection entity,
            IInventoryLocationSectionModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapNameableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // InventoryLocationSection's Related Objects
            // InventoryLocationSection's Associated Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenInventoryLocationSectionSQLSelectorFull()
        {
            PreBuiltInventoryLocationSectionSQLSelectorFull = x => x == null ? null! : new AnonInventoryLocationSection
            {
                InventoryLocationID = x.InventoryLocationID,
                InventoryLocation = ModelMapperForInventoryLocation.PreBuiltInventoryLocationSQLSelectorList.Expand().Compile().Invoke(x.InventoryLocation!),
                ProductInventoryLocationSections = x.ProductInventoryLocationSections!.Where(y => y.Active).Select(ModelMapperForProductInventoryLocationSection.PreBuiltProductInventoryLocationSectionSQLSelectorList.Expand().Compile()).ToList(),
                Name = x.Name,
                Description = x.Description,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenInventoryLocationSectionSQLSelectorLite()
        {
            PreBuiltInventoryLocationSectionSQLSelectorLite = x => x == null ? null! : new AnonInventoryLocationSection
            {
                InventoryLocationID = x.InventoryLocationID,
                InventoryLocation = ModelMapperForInventoryLocation.PreBuiltInventoryLocationSQLSelectorList.Expand().Compile().Invoke(x.InventoryLocation!),
                Name = x.Name,
                Description = x.Description,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenInventoryLocationSectionSQLSelectorList()
        {
            PreBuiltInventoryLocationSectionSQLSelectorList = x => x == null ? null! : new AnonInventoryLocationSection
            {
                InventoryLocationID = x.InventoryLocationID,
                InventoryLocation = ModelMapperForInventoryLocation.PreBuiltInventoryLocationSQLSelectorList.Expand().Compile().Invoke(x.InventoryLocation!), // For Flattening Properties (List)
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IInventoryLocationSectionModel> SelectFullInventoryLocationSectionAndMapToInventoryLocationSectionModel(
            this IQueryable<InventoryLocationSection> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationSectionSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltInventoryLocationSectionSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationSectionModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IInventoryLocationSectionModel> SelectLiteInventoryLocationSectionAndMapToInventoryLocationSectionModel(
            this IQueryable<InventoryLocationSection> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationSectionSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltInventoryLocationSectionSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationSectionModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IInventoryLocationSectionModel> SelectListInventoryLocationSectionAndMapToInventoryLocationSectionModel(
            this IQueryable<InventoryLocationSection> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationSectionSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltInventoryLocationSectionSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationSectionModelFromEntityList(x, contextProfileName))!;
        }

        public static IInventoryLocationSectionModel? SelectFirstFullInventoryLocationSectionAndMapToInventoryLocationSectionModel(
            this IQueryable<InventoryLocationSection> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationSectionSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltInventoryLocationSectionSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationSectionModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IInventoryLocationSectionModel? SelectFirstListInventoryLocationSectionAndMapToInventoryLocationSectionModel(
            this IQueryable<InventoryLocationSection> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationSectionSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltInventoryLocationSectionSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationSectionModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IInventoryLocationSectionModel? SelectSingleFullInventoryLocationSectionAndMapToInventoryLocationSectionModel(
            this IQueryable<InventoryLocationSection> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationSectionSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltInventoryLocationSectionSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationSectionModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IInventoryLocationSectionModel? SelectSingleLiteInventoryLocationSectionAndMapToInventoryLocationSectionModel(
            this IQueryable<InventoryLocationSection> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationSectionSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltInventoryLocationSectionSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationSectionModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IInventoryLocationSectionModel? SelectSingleListInventoryLocationSectionAndMapToInventoryLocationSectionModel(
            this IQueryable<InventoryLocationSection> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationSectionSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltInventoryLocationSectionSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateInventoryLocationSectionModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IInventoryLocationSectionModel> results, int totalPages, int totalCount) SelectFullInventoryLocationSectionAndMapToInventoryLocationSectionModel(
            this IQueryable<InventoryLocationSection> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationSectionSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltInventoryLocationSectionSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateInventoryLocationSectionModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IInventoryLocationSectionModel> results, int totalPages, int totalCount) SelectLiteInventoryLocationSectionAndMapToInventoryLocationSectionModel(
            this IQueryable<InventoryLocationSection> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationSectionSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltInventoryLocationSectionSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateInventoryLocationSectionModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IInventoryLocationSectionModel> results, int totalPages, int totalCount) SelectListInventoryLocationSectionAndMapToInventoryLocationSectionModel(
            this IQueryable<InventoryLocationSection> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltInventoryLocationSectionSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltInventoryLocationSectionSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateInventoryLocationSectionModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IInventoryLocationSectionModel? CreateInventoryLocationSectionModelFromEntityFull(this IInventoryLocationSection? entity, string? contextProfileName)
        {
            return CreateInventoryLocationSectionModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IInventoryLocationSectionModel? CreateInventoryLocationSectionModelFromEntityLite(this IInventoryLocationSection? entity, string? contextProfileName)
        {
            return CreateInventoryLocationSectionModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IInventoryLocationSectionModel? CreateInventoryLocationSectionModelFromEntityList(this IInventoryLocationSection? entity, string? contextProfileName)
        {
            return CreateInventoryLocationSectionModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IInventoryLocationSectionModel? CreateInventoryLocationSectionModelFromEntity(
            this IInventoryLocationSection? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapNameableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IInventoryLocationSectionModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // InventoryLocationSection's Properties
                    // InventoryLocationSection's Related Objects
                    // InventoryLocationSection's Associated Objects
                    model.ProductInventoryLocationSections = (entity is AnonInventoryLocationSection ? ((AnonInventoryLocationSection)entity).ProductInventoryLocationSections : entity.ProductInventoryLocationSections)?.Where(x => x.Active).Select(x => ModelMapperForProductInventoryLocationSection.CreateProductInventoryLocationSectionModelFromEntityList(x, contextProfileName)).ToList()!;
                    // Additional Mappings
                    if (CreateInventoryLocationSectionModelFromEntityHooksFull != null) { model = CreateInventoryLocationSectionModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // InventoryLocationSection's Properties
                    // InventoryLocationSection's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // InventoryLocationSection's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateInventoryLocationSectionModelFromEntityHooksLite != null) { model = CreateInventoryLocationSectionModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // InventoryLocationSection's Properties
                    // InventoryLocationSection's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.InventoryLocationID = entity.InventoryLocationID;
                    model.InventoryLocation = ModelMapperForInventoryLocation.CreateInventoryLocationModelFromEntityLite(entity.InventoryLocation, contextProfileName);
                    model.InventoryLocationKey = entity.InventoryLocation?.CustomKey;
                    model.InventoryLocationName = entity.InventoryLocation?.Name;
                    // InventoryLocationSection's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateInventoryLocationSectionModelFromEntityHooksList != null) { model = CreateInventoryLocationSectionModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
