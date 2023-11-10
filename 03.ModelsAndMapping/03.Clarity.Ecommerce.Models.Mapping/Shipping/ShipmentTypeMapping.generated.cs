// <autogenerated>
// <copyright file="Mapping.Shipping.ShipmentType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Shipping section of the Mapping class</summary>
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

    public static partial class ModelMapperForShipmentType
    {
        public sealed class AnonShipmentType : ShipmentType
        {
        }

        public static readonly Func<ShipmentType?, string?, ITypeModel?> MapShipmentTypeModelFromEntityFull = CreateShipmentTypeModelFromEntityFull;

        public static readonly Func<ShipmentType?, string?, ITypeModel?> MapShipmentTypeModelFromEntityLite = CreateShipmentTypeModelFromEntityLite;

        public static readonly Func<ShipmentType?, string?, ITypeModel?> MapShipmentTypeModelFromEntityList = CreateShipmentTypeModelFromEntityList;

        public static Func<IShipmentType, ITypeModel, string?, ITypeModel>? CreateShipmentTypeModelFromEntityHooksFull { get; set; }

        public static Func<IShipmentType, ITypeModel, string?, ITypeModel>? CreateShipmentTypeModelFromEntityHooksLite { get; set; }

        public static Func<IShipmentType, ITypeModel, string?, ITypeModel>? CreateShipmentTypeModelFromEntityHooksList { get; set; }

        public static Expression<Func<ShipmentType, AnonShipmentType>>? PreBuiltShipmentTypeSQLSelectorFull { get; set; }

        public static Expression<Func<ShipmentType, AnonShipmentType>>? PreBuiltShipmentTypeSQLSelectorLite { get; set; }

        public static Expression<Func<ShipmentType, AnonShipmentType>>? PreBuiltShipmentTypeSQLSelectorList { get; set; }

        /// <summary>An <see cref="ITypeModel"/> extension method that creates a(n) <see cref="ShipmentType"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="ShipmentType"/> entity.</returns>
        public static IShipmentType CreateShipmentTypeEntity(
            this ITypeModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityTypableBase<ITypeModel, ShipmentType>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateShipmentTypeFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="ITypeModel"/> extension method that updates a(n) <see cref="ShipmentType"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="ShipmentType"/> entity.</returns>
        public static IShipmentType UpdateShipmentTypeFromModel(
            this IShipmentType entity,
            ITypeModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapTypableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenShipmentTypeSQLSelectorFull()
        {
            PreBuiltShipmentTypeSQLSelectorFull = x => x == null ? null! : new AnonShipmentType
            {
                DisplayName = x.DisplayName,
                SortOrder = x.SortOrder,
                TranslationKey = x.TranslationKey,
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

        public static void GenShipmentTypeSQLSelectorLite()
        {
            PreBuiltShipmentTypeSQLSelectorLite = x => x == null ? null! : new AnonShipmentType
            {
                DisplayName = x.DisplayName,
                SortOrder = x.SortOrder,
                TranslationKey = x.TranslationKey,
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

        public static void GenShipmentTypeSQLSelectorList()
        {
            PreBuiltShipmentTypeSQLSelectorList = x => x == null ? null! : new AnonShipmentType
            {
                DisplayName = x.DisplayName,
                SortOrder = x.SortOrder,
                TranslationKey = x.TranslationKey,
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<ITypeModel> SelectFullShipmentTypeAndMapToTypeModel(
            this IQueryable<ShipmentType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentTypeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltShipmentTypeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateShipmentTypeModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<ITypeModel> SelectLiteShipmentTypeAndMapToTypeModel(
            this IQueryable<ShipmentType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentTypeSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltShipmentTypeSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateShipmentTypeModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<ITypeModel> SelectListShipmentTypeAndMapToTypeModel(
            this IQueryable<ShipmentType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentTypeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltShipmentTypeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateShipmentTypeModelFromEntityList(x, contextProfileName))!;
        }

        public static ITypeModel? SelectFirstFullShipmentTypeAndMapToTypeModel(
            this IQueryable<ShipmentType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentTypeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltShipmentTypeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateShipmentTypeModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ITypeModel? SelectFirstListShipmentTypeAndMapToTypeModel(
            this IQueryable<ShipmentType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentTypeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltShipmentTypeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateShipmentTypeModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ITypeModel? SelectSingleFullShipmentTypeAndMapToTypeModel(
            this IQueryable<ShipmentType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentTypeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltShipmentTypeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateShipmentTypeModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ITypeModel? SelectSingleLiteShipmentTypeAndMapToTypeModel(
            this IQueryable<ShipmentType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentTypeSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltShipmentTypeSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateShipmentTypeModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ITypeModel? SelectSingleListShipmentTypeAndMapToTypeModel(
            this IQueryable<ShipmentType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentTypeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltShipmentTypeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateShipmentTypeModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<ITypeModel> results, int totalPages, int totalCount) SelectFullShipmentTypeAndMapToTypeModel(
            this IQueryable<ShipmentType> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentTypeSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltShipmentTypeSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateShipmentTypeModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ITypeModel> results, int totalPages, int totalCount) SelectLiteShipmentTypeAndMapToTypeModel(
            this IQueryable<ShipmentType> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentTypeSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltShipmentTypeSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateShipmentTypeModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ITypeModel> results, int totalPages, int totalCount) SelectListShipmentTypeAndMapToTypeModel(
            this IQueryable<ShipmentType> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentTypeSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltShipmentTypeSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateShipmentTypeModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static ITypeModel? CreateShipmentTypeModelFromEntityFull(this IShipmentType? entity, string? contextProfileName)
        {
            return CreateShipmentTypeModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static ITypeModel? CreateShipmentTypeModelFromEntityLite(this IShipmentType? entity, string? contextProfileName)
        {
            return CreateShipmentTypeModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static ITypeModel? CreateShipmentTypeModelFromEntityList(this IShipmentType? entity, string? contextProfileName)
        {
            return CreateShipmentTypeModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static ITypeModel? CreateShipmentTypeModelFromEntity(
            this IShipmentType? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapTypableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<ITypeModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // ShipmentType's Properties
                    // ShipmentType's Related Objects
                    // ShipmentType's Associated Objects
                    // Additional Mappings
                    if (CreateShipmentTypeModelFromEntityHooksFull != null) { model = CreateShipmentTypeModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // ShipmentType's Properties
                    // ShipmentType's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // ShipmentType's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateShipmentTypeModelFromEntityHooksLite != null) { model = CreateShipmentTypeModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // ShipmentType's Properties
                    // ShipmentType's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // ShipmentType's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateShipmentTypeModelFromEntityHooksList != null) { model = CreateShipmentTypeModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
