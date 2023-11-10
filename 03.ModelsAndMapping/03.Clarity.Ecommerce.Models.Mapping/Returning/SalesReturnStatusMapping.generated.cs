// <autogenerated>
// <copyright file="Mapping.Returning.SalesReturnStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Returning section of the Mapping class</summary>
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

    public static partial class ModelMapperForSalesReturnStatus
    {
        public sealed class AnonSalesReturnStatus : SalesReturnStatus
        {
        }

        public static readonly Func<SalesReturnStatus?, string?, IStatusModel?> MapSalesReturnStatusModelFromEntityFull = CreateSalesReturnStatusModelFromEntityFull;

        public static readonly Func<SalesReturnStatus?, string?, IStatusModel?> MapSalesReturnStatusModelFromEntityLite = CreateSalesReturnStatusModelFromEntityLite;

        public static readonly Func<SalesReturnStatus?, string?, IStatusModel?> MapSalesReturnStatusModelFromEntityList = CreateSalesReturnStatusModelFromEntityList;

        public static Func<ISalesReturnStatus, IStatusModel, string?, IStatusModel>? CreateSalesReturnStatusModelFromEntityHooksFull { get; set; }

        public static Func<ISalesReturnStatus, IStatusModel, string?, IStatusModel>? CreateSalesReturnStatusModelFromEntityHooksLite { get; set; }

        public static Func<ISalesReturnStatus, IStatusModel, string?, IStatusModel>? CreateSalesReturnStatusModelFromEntityHooksList { get; set; }

        public static Expression<Func<SalesReturnStatus, AnonSalesReturnStatus>>? PreBuiltSalesReturnStatusSQLSelectorFull { get; set; }

        public static Expression<Func<SalesReturnStatus, AnonSalesReturnStatus>>? PreBuiltSalesReturnStatusSQLSelectorLite { get; set; }

        public static Expression<Func<SalesReturnStatus, AnonSalesReturnStatus>>? PreBuiltSalesReturnStatusSQLSelectorList { get; set; }

        /// <summary>An <see cref="IStatusModel"/> extension method that creates a(n) <see cref="SalesReturnStatus"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="SalesReturnStatus"/> entity.</returns>
        public static ISalesReturnStatus CreateSalesReturnStatusEntity(
            this IStatusModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityStatusableBase<IStatusModel, SalesReturnStatus>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateSalesReturnStatusFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IStatusModel"/> extension method that updates a(n) <see cref="SalesReturnStatus"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="SalesReturnStatus"/> entity.</returns>
        public static ISalesReturnStatus UpdateSalesReturnStatusFromModel(
            this ISalesReturnStatus entity,
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

        public static void GenSalesReturnStatusSQLSelectorFull()
        {
            PreBuiltSalesReturnStatusSQLSelectorFull = x => x == null ? null! : new AnonSalesReturnStatus
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

        public static void GenSalesReturnStatusSQLSelectorLite()
        {
            PreBuiltSalesReturnStatusSQLSelectorLite = x => x == null ? null! : new AnonSalesReturnStatus
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

        public static void GenSalesReturnStatusSQLSelectorList()
        {
            PreBuiltSalesReturnStatusSQLSelectorList = x => x == null ? null! : new AnonSalesReturnStatus
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

        public static IEnumerable<IStatusModel> SelectFullSalesReturnStatusAndMapToStatusModel(
            this IQueryable<SalesReturnStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesReturnStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesReturnStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesReturnStatusModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IStatusModel> SelectLiteSalesReturnStatusAndMapToStatusModel(
            this IQueryable<SalesReturnStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesReturnStatusSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesReturnStatusSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesReturnStatusModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IStatusModel> SelectListSalesReturnStatusAndMapToStatusModel(
            this IQueryable<SalesReturnStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesReturnStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesReturnStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesReturnStatusModelFromEntityList(x, contextProfileName))!;
        }

        public static IStatusModel? SelectFirstFullSalesReturnStatusAndMapToStatusModel(
            this IQueryable<SalesReturnStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesReturnStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesReturnStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesReturnStatusModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStatusModel? SelectFirstListSalesReturnStatusAndMapToStatusModel(
            this IQueryable<SalesReturnStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesReturnStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesReturnStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesReturnStatusModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStatusModel? SelectSingleFullSalesReturnStatusAndMapToStatusModel(
            this IQueryable<SalesReturnStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesReturnStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesReturnStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesReturnStatusModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStatusModel? SelectSingleLiteSalesReturnStatusAndMapToStatusModel(
            this IQueryable<SalesReturnStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesReturnStatusSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesReturnStatusSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesReturnStatusModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStatusModel? SelectSingleListSalesReturnStatusAndMapToStatusModel(
            this IQueryable<SalesReturnStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesReturnStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesReturnStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesReturnStatusModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectFullSalesReturnStatusAndMapToStatusModel(
            this IQueryable<SalesReturnStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesReturnStatusSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesReturnStatusSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateSalesReturnStatusModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectLiteSalesReturnStatusAndMapToStatusModel(
            this IQueryable<SalesReturnStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesReturnStatusSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesReturnStatusSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateSalesReturnStatusModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectListSalesReturnStatusAndMapToStatusModel(
            this IQueryable<SalesReturnStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesReturnStatusSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesReturnStatusSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateSalesReturnStatusModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IStatusModel? CreateSalesReturnStatusModelFromEntityFull(this ISalesReturnStatus? entity, string? contextProfileName)
        {
            return CreateSalesReturnStatusModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IStatusModel? CreateSalesReturnStatusModelFromEntityLite(this ISalesReturnStatus? entity, string? contextProfileName)
        {
            return CreateSalesReturnStatusModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IStatusModel? CreateSalesReturnStatusModelFromEntityList(this ISalesReturnStatus? entity, string? contextProfileName)
        {
            return CreateSalesReturnStatusModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IStatusModel? CreateSalesReturnStatusModelFromEntity(
            this ISalesReturnStatus? entity,
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
                    // SalesReturnStatus's Properties
                    // SalesReturnStatus's Related Objects
                    // SalesReturnStatus's Associated Objects
                    // Additional Mappings
                    if (CreateSalesReturnStatusModelFromEntityHooksFull != null) { model = CreateSalesReturnStatusModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // SalesReturnStatus's Properties
                    // SalesReturnStatus's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SalesReturnStatus's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesReturnStatusModelFromEntityHooksLite != null) { model = CreateSalesReturnStatusModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // SalesReturnStatus's Properties
                    // SalesReturnStatus's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SalesReturnStatus's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesReturnStatusModelFromEntityHooksList != null) { model = CreateSalesReturnStatusModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
