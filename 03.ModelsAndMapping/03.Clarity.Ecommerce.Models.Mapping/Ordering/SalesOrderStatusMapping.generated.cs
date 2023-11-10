// <autogenerated>
// <copyright file="Mapping.Ordering.SalesOrderStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Ordering section of the Mapping class</summary>
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

    public static partial class ModelMapperForSalesOrderStatus
    {
        public sealed class AnonSalesOrderStatus : SalesOrderStatus
        {
        }

        public static readonly Func<SalesOrderStatus?, string?, IStatusModel?> MapSalesOrderStatusModelFromEntityFull = CreateSalesOrderStatusModelFromEntityFull;

        public static readonly Func<SalesOrderStatus?, string?, IStatusModel?> MapSalesOrderStatusModelFromEntityLite = CreateSalesOrderStatusModelFromEntityLite;

        public static readonly Func<SalesOrderStatus?, string?, IStatusModel?> MapSalesOrderStatusModelFromEntityList = CreateSalesOrderStatusModelFromEntityList;

        public static Func<ISalesOrderStatus, IStatusModel, string?, IStatusModel>? CreateSalesOrderStatusModelFromEntityHooksFull { get; set; }

        public static Func<ISalesOrderStatus, IStatusModel, string?, IStatusModel>? CreateSalesOrderStatusModelFromEntityHooksLite { get; set; }

        public static Func<ISalesOrderStatus, IStatusModel, string?, IStatusModel>? CreateSalesOrderStatusModelFromEntityHooksList { get; set; }

        public static Expression<Func<SalesOrderStatus, AnonSalesOrderStatus>>? PreBuiltSalesOrderStatusSQLSelectorFull { get; set; }

        public static Expression<Func<SalesOrderStatus, AnonSalesOrderStatus>>? PreBuiltSalesOrderStatusSQLSelectorLite { get; set; }

        public static Expression<Func<SalesOrderStatus, AnonSalesOrderStatus>>? PreBuiltSalesOrderStatusSQLSelectorList { get; set; }

        /// <summary>An <see cref="IStatusModel"/> extension method that creates a(n) <see cref="SalesOrderStatus"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="SalesOrderStatus"/> entity.</returns>
        public static ISalesOrderStatus CreateSalesOrderStatusEntity(
            this IStatusModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityStatusableBase<IStatusModel, SalesOrderStatus>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateSalesOrderStatusFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IStatusModel"/> extension method that updates a(n) <see cref="SalesOrderStatus"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="SalesOrderStatus"/> entity.</returns>
        public static ISalesOrderStatus UpdateSalesOrderStatusFromModel(
            this ISalesOrderStatus entity,
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

        public static void GenSalesOrderStatusSQLSelectorFull()
        {
            PreBuiltSalesOrderStatusSQLSelectorFull = x => x == null ? null! : new AnonSalesOrderStatus
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

        public static void GenSalesOrderStatusSQLSelectorLite()
        {
            PreBuiltSalesOrderStatusSQLSelectorLite = x => x == null ? null! : new AnonSalesOrderStatus
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

        public static void GenSalesOrderStatusSQLSelectorList()
        {
            PreBuiltSalesOrderStatusSQLSelectorList = x => x == null ? null! : new AnonSalesOrderStatus
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

        public static IEnumerable<IStatusModel> SelectFullSalesOrderStatusAndMapToStatusModel(
            this IQueryable<SalesOrderStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderStatusModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IStatusModel> SelectLiteSalesOrderStatusAndMapToStatusModel(
            this IQueryable<SalesOrderStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderStatusSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderStatusSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderStatusModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IStatusModel> SelectListSalesOrderStatusAndMapToStatusModel(
            this IQueryable<SalesOrderStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderStatusModelFromEntityList(x, contextProfileName))!;
        }

        public static IStatusModel? SelectFirstFullSalesOrderStatusAndMapToStatusModel(
            this IQueryable<SalesOrderStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderStatusModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStatusModel? SelectFirstListSalesOrderStatusAndMapToStatusModel(
            this IQueryable<SalesOrderStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderStatusModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStatusModel? SelectSingleFullSalesOrderStatusAndMapToStatusModel(
            this IQueryable<SalesOrderStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderStatusModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStatusModel? SelectSingleLiteSalesOrderStatusAndMapToStatusModel(
            this IQueryable<SalesOrderStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderStatusSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderStatusSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderStatusModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStatusModel? SelectSingleListSalesOrderStatusAndMapToStatusModel(
            this IQueryable<SalesOrderStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderStatusModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectFullSalesOrderStatusAndMapToStatusModel(
            this IQueryable<SalesOrderStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderStatusSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderStatusSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderStatusModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectLiteSalesOrderStatusAndMapToStatusModel(
            this IQueryable<SalesOrderStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderStatusSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderStatusSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderStatusModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectListSalesOrderStatusAndMapToStatusModel(
            this IQueryable<SalesOrderStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderStatusSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderStatusSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderStatusModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IStatusModel? CreateSalesOrderStatusModelFromEntityFull(this ISalesOrderStatus? entity, string? contextProfileName)
        {
            return CreateSalesOrderStatusModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IStatusModel? CreateSalesOrderStatusModelFromEntityLite(this ISalesOrderStatus? entity, string? contextProfileName)
        {
            return CreateSalesOrderStatusModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IStatusModel? CreateSalesOrderStatusModelFromEntityList(this ISalesOrderStatus? entity, string? contextProfileName)
        {
            return CreateSalesOrderStatusModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IStatusModel? CreateSalesOrderStatusModelFromEntity(
            this ISalesOrderStatus? entity,
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
                    // SalesOrderStatus's Properties
                    // SalesOrderStatus's Related Objects
                    // SalesOrderStatus's Associated Objects
                    // Additional Mappings
                    if (CreateSalesOrderStatusModelFromEntityHooksFull != null) { model = CreateSalesOrderStatusModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // SalesOrderStatus's Properties
                    // SalesOrderStatus's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SalesOrderStatus's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesOrderStatusModelFromEntityHooksLite != null) { model = CreateSalesOrderStatusModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // SalesOrderStatus's Properties
                    // SalesOrderStatus's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SalesOrderStatus's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesOrderStatusModelFromEntityHooksList != null) { model = CreateSalesOrderStatusModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
