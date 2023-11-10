// <autogenerated>
// <copyright file="Mapping.Ordering.SalesOrderEvent.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForSalesOrderEvent
    {
        public sealed class AnonSalesOrderEvent : SalesOrderEvent
        {
            // public new SalesOrder? Master { get; set; }
        }

        public static readonly Func<SalesOrderEvent?, string?, ISalesOrderEventModel?> MapSalesOrderEventModelFromEntityFull = CreateSalesOrderEventModelFromEntityFull;

        public static readonly Func<SalesOrderEvent?, string?, ISalesOrderEventModel?> MapSalesOrderEventModelFromEntityLite = CreateSalesOrderEventModelFromEntityLite;

        public static readonly Func<SalesOrderEvent?, string?, ISalesOrderEventModel?> MapSalesOrderEventModelFromEntityList = CreateSalesOrderEventModelFromEntityList;

        public static Func<ISalesOrderEvent, ISalesOrderEventModel, string?, ISalesOrderEventModel>? CreateSalesOrderEventModelFromEntityHooksFull { get; set; }

        public static Func<ISalesOrderEvent, ISalesOrderEventModel, string?, ISalesOrderEventModel>? CreateSalesOrderEventModelFromEntityHooksLite { get; set; }

        public static Func<ISalesOrderEvent, ISalesOrderEventModel, string?, ISalesOrderEventModel>? CreateSalesOrderEventModelFromEntityHooksList { get; set; }

        public static Expression<Func<SalesOrderEvent, AnonSalesOrderEvent>>? PreBuiltSalesOrderEventSQLSelectorFull { get; set; }

        public static Expression<Func<SalesOrderEvent, AnonSalesOrderEvent>>? PreBuiltSalesOrderEventSQLSelectorLite { get; set; }

        public static Expression<Func<SalesOrderEvent, AnonSalesOrderEvent>>? PreBuiltSalesOrderEventSQLSelectorList { get; set; }

        /// <summary>An <see cref="ISalesOrderEventModel"/> extension method that creates a(n) <see cref="SalesOrderEvent"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="SalesOrderEvent"/> entity.</returns>
        public static ISalesOrderEvent CreateSalesOrderEventEntity(
            this ISalesOrderEventModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelNameableBase<ISalesOrderEventModel, SalesOrderEvent>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateSalesOrderEventFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="ISalesOrderEventModel"/> extension method that updates a(n) <see cref="SalesOrderEvent"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="SalesOrderEvent"/> entity.</returns>
        public static ISalesOrderEvent UpdateSalesOrderEventFromModel(
            this ISalesOrderEvent entity,
            ISalesOrderEventModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapNameableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // ISalesEventBase Properties
            entity.OldHash = model.OldHash;
            entity.NewHash = model.NewHash;
            entity.OldStateID = model.OldStateID;
            entity.NewStateID = model.NewStateID;
            entity.OldStatusID = model.OldStatusID;
            entity.NewStatusID = model.NewStatusID;
            entity.OldTypeID = model.OldTypeID;
            entity.NewTypeID = model.NewTypeID;
            entity.OldRecordSerialized = model.OldRecordSerialized;
            entity.NewRecordSerialized = model.NewRecordSerialized;
            // SalesOrderEvent Properties
            entity.NewBalanceDue = model.NewBalanceDue;
            entity.OldBalanceDue = model.OldBalanceDue;
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenSalesOrderEventSQLSelectorFull()
        {
            PreBuiltSalesOrderEventSQLSelectorFull = x => x == null ? null! : new AnonSalesOrderEvent
            {
                OldBalanceDue = x.OldBalanceDue,
                NewBalanceDue = x.NewBalanceDue,
                OldStateID = x.OldStateID,
                NewStateID = x.NewStateID,
                OldStatusID = x.OldStatusID,
                NewStatusID = x.NewStatusID,
                OldTypeID = x.OldTypeID,
                NewTypeID = x.NewTypeID,
                OldHash = x.OldHash,
                NewHash = x.NewHash,
                OldRecordSerialized = x.OldRecordSerialized,
                NewRecordSerialized = x.NewRecordSerialized,
                TypeID = x.TypeID,
                Type = ModelMapperForSalesOrderEventType.PreBuiltSalesOrderEventTypeSQLSelectorList.Expand().Compile().Invoke(x.Type!),
                MasterID = x.MasterID,
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

        public static void GenSalesOrderEventSQLSelectorLite()
        {
            PreBuiltSalesOrderEventSQLSelectorLite = x => x == null ? null! : new AnonSalesOrderEvent
            {
                OldBalanceDue = x.OldBalanceDue,
                NewBalanceDue = x.NewBalanceDue,
                OldStateID = x.OldStateID,
                NewStateID = x.NewStateID,
                OldStatusID = x.OldStatusID,
                NewStatusID = x.NewStatusID,
                OldTypeID = x.OldTypeID,
                NewTypeID = x.NewTypeID,
                OldHash = x.OldHash,
                NewHash = x.NewHash,
                TypeID = x.TypeID,
                Type = ModelMapperForSalesOrderEventType.PreBuiltSalesOrderEventTypeSQLSelectorList.Expand().Compile().Invoke(x.Type!),
                MasterID = x.MasterID,
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

        public static void GenSalesOrderEventSQLSelectorList()
        {
            PreBuiltSalesOrderEventSQLSelectorList = x => x == null ? null! : new AnonSalesOrderEvent
            {
                OldBalanceDue = x.OldBalanceDue,
                NewBalanceDue = x.NewBalanceDue,
                OldStateID = x.OldStateID,
                NewStateID = x.NewStateID,
                OldStatusID = x.OldStatusID,
                NewStatusID = x.NewStatusID,
                OldTypeID = x.OldTypeID,
                NewTypeID = x.NewTypeID,
                OldHash = x.OldHash,
                NewHash = x.NewHash,
                TypeID = x.TypeID,
                Type = ModelMapperForSalesOrderEventType.PreBuiltSalesOrderEventTypeSQLSelectorList.Expand().Compile().Invoke(x.Type!), // For Flattening Properties (List)
                MasterID = x.MasterID,
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<ISalesOrderEventModel> SelectFullSalesOrderEventAndMapToSalesOrderEventModel(
            this IQueryable<SalesOrderEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderEventSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderEventSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderEventModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<ISalesOrderEventModel> SelectLiteSalesOrderEventAndMapToSalesOrderEventModel(
            this IQueryable<SalesOrderEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderEventSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderEventSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderEventModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<ISalesOrderEventModel> SelectListSalesOrderEventAndMapToSalesOrderEventModel(
            this IQueryable<SalesOrderEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderEventSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderEventSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderEventModelFromEntityList(x, contextProfileName))!;
        }

        public static ISalesOrderEventModel? SelectFirstFullSalesOrderEventAndMapToSalesOrderEventModel(
            this IQueryable<SalesOrderEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderEventSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderEventSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderEventModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ISalesOrderEventModel? SelectFirstListSalesOrderEventAndMapToSalesOrderEventModel(
            this IQueryable<SalesOrderEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderEventSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderEventSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderEventModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ISalesOrderEventModel? SelectSingleFullSalesOrderEventAndMapToSalesOrderEventModel(
            this IQueryable<SalesOrderEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderEventSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderEventSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderEventModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ISalesOrderEventModel? SelectSingleLiteSalesOrderEventAndMapToSalesOrderEventModel(
            this IQueryable<SalesOrderEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderEventSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderEventSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderEventModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ISalesOrderEventModel? SelectSingleListSalesOrderEventAndMapToSalesOrderEventModel(
            this IQueryable<SalesOrderEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderEventSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderEventSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderEventModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<ISalesOrderEventModel> results, int totalPages, int totalCount) SelectFullSalesOrderEventAndMapToSalesOrderEventModel(
            this IQueryable<SalesOrderEvent> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderEventSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderEventSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderEventModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ISalesOrderEventModel> results, int totalPages, int totalCount) SelectLiteSalesOrderEventAndMapToSalesOrderEventModel(
            this IQueryable<SalesOrderEvent> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderEventSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderEventSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderEventModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ISalesOrderEventModel> results, int totalPages, int totalCount) SelectListSalesOrderEventAndMapToSalesOrderEventModel(
            this IQueryable<SalesOrderEvent> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderEventSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderEventSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderEventModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static ISalesOrderEventModel? CreateSalesOrderEventModelFromEntityFull(this ISalesOrderEvent? entity, string? contextProfileName)
        {
            return CreateSalesOrderEventModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static ISalesOrderEventModel? CreateSalesOrderEventModelFromEntityLite(this ISalesOrderEvent? entity, string? contextProfileName)
        {
            return CreateSalesOrderEventModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static ISalesOrderEventModel? CreateSalesOrderEventModelFromEntityList(this ISalesOrderEvent? entity, string? contextProfileName)
        {
            return CreateSalesOrderEventModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static ISalesOrderEventModel? CreateSalesOrderEventModelFromEntity(
            this ISalesOrderEvent? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapNameableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<ISalesOrderEventModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // ISalesEventBase Properties
                    model.OldRecordSerialized = entity.OldRecordSerialized;
                    model.NewRecordSerialized = entity.NewRecordSerialized;
                    // SalesOrderEvent's Properties
                    // SalesOrderEvent's Related Objects
                    // SalesOrderEvent's Associated Objects
                    // Additional Mappings
                    if (CreateSalesOrderEventModelFromEntityHooksFull != null) { model = CreateSalesOrderEventModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // SalesOrderEvent's Properties
                    // IHaveATypeBase Properties (Forced)
                    model.Type = ModelMapperForSalesOrderEventType.CreateSalesOrderEventTypeModelFromEntityLite(entity.Type, contextProfileName);
                    // SalesOrderEvent's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SalesOrderEvent's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesOrderEventModelFromEntityHooksLite != null) { model = CreateSalesOrderEventModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveATypeBase Properties
                    model.TypeID = entity.TypeID;
                    if (entity.Type != null)
                    {
                        model.TypeKey = entity.Type.CustomKey;
                        model.TypeName = entity.Type.Name;
                        model.TypeDisplayName = entity.Type.DisplayName;
                        model.TypeTranslationKey = entity.Type.TranslationKey;
                        model.TypeSortOrder = entity.Type.SortOrder;
                    }
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // ISalesEventBase Properties
                    model.OldHash = entity.OldHash;
                    model.NewHash = entity.NewHash;
                    model.OldStateID = entity.OldStateID;
                    model.NewStateID = entity.NewStateID;
                    model.OldStatusID = entity.OldStatusID;
                    model.NewStatusID = entity.NewStatusID;
                    model.OldTypeID = entity.OldTypeID;
                    model.NewTypeID = entity.NewTypeID;
                    // SalesOrderEvent's Properties
                    model.NewBalanceDue = entity.NewBalanceDue;
                    model.OldBalanceDue = entity.OldBalanceDue;
                    // SalesOrderEvent's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SalesOrderEvent's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesOrderEventModelFromEntityHooksList != null) { model = CreateSalesOrderEventModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
