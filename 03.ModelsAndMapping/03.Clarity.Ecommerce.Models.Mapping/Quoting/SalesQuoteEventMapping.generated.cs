// <autogenerated>
// <copyright file="Mapping.Quoting.SalesQuoteEvent.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Quoting section of the Mapping class</summary>
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

    public static partial class ModelMapperForSalesQuoteEvent
    {
        public sealed class AnonSalesQuoteEvent : SalesQuoteEvent
        {
            // public new SalesQuote? Master { get; set; }
        }

        public static readonly Func<SalesQuoteEvent?, string?, ISalesQuoteEventModel?> MapSalesQuoteEventModelFromEntityFull = CreateSalesQuoteEventModelFromEntityFull;

        public static readonly Func<SalesQuoteEvent?, string?, ISalesQuoteEventModel?> MapSalesQuoteEventModelFromEntityLite = CreateSalesQuoteEventModelFromEntityLite;

        public static readonly Func<SalesQuoteEvent?, string?, ISalesQuoteEventModel?> MapSalesQuoteEventModelFromEntityList = CreateSalesQuoteEventModelFromEntityList;

        public static Func<ISalesQuoteEvent, ISalesQuoteEventModel, string?, ISalesQuoteEventModel>? CreateSalesQuoteEventModelFromEntityHooksFull { get; set; }

        public static Func<ISalesQuoteEvent, ISalesQuoteEventModel, string?, ISalesQuoteEventModel>? CreateSalesQuoteEventModelFromEntityHooksLite { get; set; }

        public static Func<ISalesQuoteEvent, ISalesQuoteEventModel, string?, ISalesQuoteEventModel>? CreateSalesQuoteEventModelFromEntityHooksList { get; set; }

        public static Expression<Func<SalesQuoteEvent, AnonSalesQuoteEvent>>? PreBuiltSalesQuoteEventSQLSelectorFull { get; set; }

        public static Expression<Func<SalesQuoteEvent, AnonSalesQuoteEvent>>? PreBuiltSalesQuoteEventSQLSelectorLite { get; set; }

        public static Expression<Func<SalesQuoteEvent, AnonSalesQuoteEvent>>? PreBuiltSalesQuoteEventSQLSelectorList { get; set; }

        /// <summary>An <see cref="ISalesQuoteEventModel"/> extension method that creates a(n) <see cref="SalesQuoteEvent"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="SalesQuoteEvent"/> entity.</returns>
        public static ISalesQuoteEvent CreateSalesQuoteEventEntity(
            this ISalesQuoteEventModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelNameableBase<ISalesQuoteEventModel, SalesQuoteEvent>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateSalesQuoteEventFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="ISalesQuoteEventModel"/> extension method that updates a(n) <see cref="SalesQuoteEvent"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="SalesQuoteEvent"/> entity.</returns>
        public static ISalesQuoteEvent UpdateSalesQuoteEventFromModel(
            this ISalesQuoteEvent entity,
            ISalesQuoteEventModel model,
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
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenSalesQuoteEventSQLSelectorFull()
        {
            PreBuiltSalesQuoteEventSQLSelectorFull = x => x == null ? null! : new AnonSalesQuoteEvent
            {
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
                Type = ModelMapperForSalesQuoteEventType.PreBuiltSalesQuoteEventTypeSQLSelectorList.Expand().Compile().Invoke(x.Type!),
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

        public static void GenSalesQuoteEventSQLSelectorLite()
        {
            PreBuiltSalesQuoteEventSQLSelectorLite = x => x == null ? null! : new AnonSalesQuoteEvent
            {
                OldStateID = x.OldStateID,
                NewStateID = x.NewStateID,
                OldStatusID = x.OldStatusID,
                NewStatusID = x.NewStatusID,
                OldTypeID = x.OldTypeID,
                NewTypeID = x.NewTypeID,
                OldHash = x.OldHash,
                NewHash = x.NewHash,
                TypeID = x.TypeID,
                Type = ModelMapperForSalesQuoteEventType.PreBuiltSalesQuoteEventTypeSQLSelectorList.Expand().Compile().Invoke(x.Type!),
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

        public static void GenSalesQuoteEventSQLSelectorList()
        {
            PreBuiltSalesQuoteEventSQLSelectorList = x => x == null ? null! : new AnonSalesQuoteEvent
            {
                OldStateID = x.OldStateID,
                NewStateID = x.NewStateID,
                OldStatusID = x.OldStatusID,
                NewStatusID = x.NewStatusID,
                OldTypeID = x.OldTypeID,
                NewTypeID = x.NewTypeID,
                OldHash = x.OldHash,
                NewHash = x.NewHash,
                TypeID = x.TypeID,
                Type = ModelMapperForSalesQuoteEventType.PreBuiltSalesQuoteEventTypeSQLSelectorList.Expand().Compile().Invoke(x.Type!), // For Flattening Properties (List)
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

        public static IEnumerable<ISalesQuoteEventModel> SelectFullSalesQuoteEventAndMapToSalesQuoteEventModel(
            this IQueryable<SalesQuoteEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesQuoteEventSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesQuoteEventSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesQuoteEventModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<ISalesQuoteEventModel> SelectLiteSalesQuoteEventAndMapToSalesQuoteEventModel(
            this IQueryable<SalesQuoteEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesQuoteEventSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesQuoteEventSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesQuoteEventModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<ISalesQuoteEventModel> SelectListSalesQuoteEventAndMapToSalesQuoteEventModel(
            this IQueryable<SalesQuoteEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesQuoteEventSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesQuoteEventSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesQuoteEventModelFromEntityList(x, contextProfileName))!;
        }

        public static ISalesQuoteEventModel? SelectFirstFullSalesQuoteEventAndMapToSalesQuoteEventModel(
            this IQueryable<SalesQuoteEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesQuoteEventSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesQuoteEventSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesQuoteEventModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ISalesQuoteEventModel? SelectFirstListSalesQuoteEventAndMapToSalesQuoteEventModel(
            this IQueryable<SalesQuoteEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesQuoteEventSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesQuoteEventSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesQuoteEventModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ISalesQuoteEventModel? SelectSingleFullSalesQuoteEventAndMapToSalesQuoteEventModel(
            this IQueryable<SalesQuoteEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesQuoteEventSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesQuoteEventSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesQuoteEventModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ISalesQuoteEventModel? SelectSingleLiteSalesQuoteEventAndMapToSalesQuoteEventModel(
            this IQueryable<SalesQuoteEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesQuoteEventSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesQuoteEventSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesQuoteEventModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ISalesQuoteEventModel? SelectSingleListSalesQuoteEventAndMapToSalesQuoteEventModel(
            this IQueryable<SalesQuoteEvent> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesQuoteEventSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesQuoteEventSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesQuoteEventModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<ISalesQuoteEventModel> results, int totalPages, int totalCount) SelectFullSalesQuoteEventAndMapToSalesQuoteEventModel(
            this IQueryable<SalesQuoteEvent> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesQuoteEventSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesQuoteEventSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateSalesQuoteEventModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ISalesQuoteEventModel> results, int totalPages, int totalCount) SelectLiteSalesQuoteEventAndMapToSalesQuoteEventModel(
            this IQueryable<SalesQuoteEvent> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesQuoteEventSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesQuoteEventSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateSalesQuoteEventModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ISalesQuoteEventModel> results, int totalPages, int totalCount) SelectListSalesQuoteEventAndMapToSalesQuoteEventModel(
            this IQueryable<SalesQuoteEvent> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesQuoteEventSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesQuoteEventSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateSalesQuoteEventModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static ISalesQuoteEventModel? CreateSalesQuoteEventModelFromEntityFull(this ISalesQuoteEvent? entity, string? contextProfileName)
        {
            return CreateSalesQuoteEventModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static ISalesQuoteEventModel? CreateSalesQuoteEventModelFromEntityLite(this ISalesQuoteEvent? entity, string? contextProfileName)
        {
            return CreateSalesQuoteEventModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static ISalesQuoteEventModel? CreateSalesQuoteEventModelFromEntityList(this ISalesQuoteEvent? entity, string? contextProfileName)
        {
            return CreateSalesQuoteEventModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static ISalesQuoteEventModel? CreateSalesQuoteEventModelFromEntity(
            this ISalesQuoteEvent? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapNameableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<ISalesQuoteEventModel>(contextProfileName),
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
                    // SalesQuoteEvent's Properties
                    // SalesQuoteEvent's Related Objects
                    // SalesQuoteEvent's Associated Objects
                    // Additional Mappings
                    if (CreateSalesQuoteEventModelFromEntityHooksFull != null) { model = CreateSalesQuoteEventModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // SalesQuoteEvent's Properties
                    // IHaveATypeBase Properties (Forced)
                    model.Type = ModelMapperForSalesQuoteEventType.CreateSalesQuoteEventTypeModelFromEntityLite(entity.Type, contextProfileName);
                    // SalesQuoteEvent's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SalesQuoteEvent's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesQuoteEventModelFromEntityHooksLite != null) { model = CreateSalesQuoteEventModelFromEntityHooksLite(entity, model, contextProfileName); }
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
                    // SalesQuoteEvent's Properties
                    // SalesQuoteEvent's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SalesQuoteEvent's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesQuoteEventModelFromEntityHooksList != null) { model = CreateSalesQuoteEventModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
