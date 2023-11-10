// <autogenerated>
// <copyright file="Mapping.Shipping.ShipmentStatus.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForShipmentStatus
    {
        public sealed class AnonShipmentStatus : ShipmentStatus
        {
        }

        public static readonly Func<ShipmentStatus?, string?, IStatusModel?> MapShipmentStatusModelFromEntityFull = CreateShipmentStatusModelFromEntityFull;

        public static readonly Func<ShipmentStatus?, string?, IStatusModel?> MapShipmentStatusModelFromEntityLite = CreateShipmentStatusModelFromEntityLite;

        public static readonly Func<ShipmentStatus?, string?, IStatusModel?> MapShipmentStatusModelFromEntityList = CreateShipmentStatusModelFromEntityList;

        public static Func<IShipmentStatus, IStatusModel, string?, IStatusModel>? CreateShipmentStatusModelFromEntityHooksFull { get; set; }

        public static Func<IShipmentStatus, IStatusModel, string?, IStatusModel>? CreateShipmentStatusModelFromEntityHooksLite { get; set; }

        public static Func<IShipmentStatus, IStatusModel, string?, IStatusModel>? CreateShipmentStatusModelFromEntityHooksList { get; set; }

        public static Expression<Func<ShipmentStatus, AnonShipmentStatus>>? PreBuiltShipmentStatusSQLSelectorFull { get; set; }

        public static Expression<Func<ShipmentStatus, AnonShipmentStatus>>? PreBuiltShipmentStatusSQLSelectorLite { get; set; }

        public static Expression<Func<ShipmentStatus, AnonShipmentStatus>>? PreBuiltShipmentStatusSQLSelectorList { get; set; }

        /// <summary>An <see cref="IStatusModel"/> extension method that creates a(n) <see cref="ShipmentStatus"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="ShipmentStatus"/> entity.</returns>
        public static IShipmentStatus CreateShipmentStatusEntity(
            this IStatusModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityStatusableBase<IStatusModel, ShipmentStatus>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateShipmentStatusFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IStatusModel"/> extension method that updates a(n) <see cref="ShipmentStatus"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="ShipmentStatus"/> entity.</returns>
        public static IShipmentStatus UpdateShipmentStatusFromModel(
            this IShipmentStatus entity,
            IStatusModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapStatusableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenShipmentStatusSQLSelectorFull()
        {
            PreBuiltShipmentStatusSQLSelectorFull = x => x == null ? null! : new AnonShipmentStatus
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

        public static void GenShipmentStatusSQLSelectorLite()
        {
            PreBuiltShipmentStatusSQLSelectorLite = x => x == null ? null! : new AnonShipmentStatus
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

        public static void GenShipmentStatusSQLSelectorList()
        {
            PreBuiltShipmentStatusSQLSelectorList = x => x == null ? null! : new AnonShipmentStatus
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

        public static IEnumerable<IStatusModel> SelectFullShipmentStatusAndMapToStatusModel(
            this IQueryable<ShipmentStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltShipmentStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateShipmentStatusModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IStatusModel> SelectLiteShipmentStatusAndMapToStatusModel(
            this IQueryable<ShipmentStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentStatusSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltShipmentStatusSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateShipmentStatusModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IStatusModel> SelectListShipmentStatusAndMapToStatusModel(
            this IQueryable<ShipmentStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltShipmentStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateShipmentStatusModelFromEntityList(x, contextProfileName))!;
        }

        public static IStatusModel? SelectFirstFullShipmentStatusAndMapToStatusModel(
            this IQueryable<ShipmentStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltShipmentStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateShipmentStatusModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStatusModel? SelectFirstListShipmentStatusAndMapToStatusModel(
            this IQueryable<ShipmentStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltShipmentStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateShipmentStatusModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStatusModel? SelectSingleFullShipmentStatusAndMapToStatusModel(
            this IQueryable<ShipmentStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltShipmentStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateShipmentStatusModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStatusModel? SelectSingleLiteShipmentStatusAndMapToStatusModel(
            this IQueryable<ShipmentStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentStatusSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltShipmentStatusSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateShipmentStatusModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStatusModel? SelectSingleListShipmentStatusAndMapToStatusModel(
            this IQueryable<ShipmentStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltShipmentStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateShipmentStatusModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectFullShipmentStatusAndMapToStatusModel(
            this IQueryable<ShipmentStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentStatusSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltShipmentStatusSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateShipmentStatusModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectLiteShipmentStatusAndMapToStatusModel(
            this IQueryable<ShipmentStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentStatusSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltShipmentStatusSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateShipmentStatusModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectListShipmentStatusAndMapToStatusModel(
            this IQueryable<ShipmentStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltShipmentStatusSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltShipmentStatusSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateShipmentStatusModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IStatusModel? CreateShipmentStatusModelFromEntityFull(this IShipmentStatus? entity, string? contextProfileName)
        {
            return CreateShipmentStatusModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IStatusModel? CreateShipmentStatusModelFromEntityLite(this IShipmentStatus? entity, string? contextProfileName)
        {
            return CreateShipmentStatusModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IStatusModel? CreateShipmentStatusModelFromEntityList(this IShipmentStatus? entity, string? contextProfileName)
        {
            return CreateShipmentStatusModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IStatusModel? CreateShipmentStatusModelFromEntity(
            this IShipmentStatus? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapStatusableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IStatusModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // ShipmentStatus's Properties
                    // ShipmentStatus's Related Objects
                    // ShipmentStatus's Associated Objects
                    // Additional Mappings
                    if (CreateShipmentStatusModelFromEntityHooksFull != null) { model = CreateShipmentStatusModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // ShipmentStatus's Properties
                    // ShipmentStatus's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // ShipmentStatus's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateShipmentStatusModelFromEntityHooksLite != null) { model = CreateShipmentStatusModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // ShipmentStatus's Properties
                    // ShipmentStatus's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // ShipmentStatus's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateShipmentStatusModelFromEntityHooksList != null) { model = CreateShipmentStatusModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
