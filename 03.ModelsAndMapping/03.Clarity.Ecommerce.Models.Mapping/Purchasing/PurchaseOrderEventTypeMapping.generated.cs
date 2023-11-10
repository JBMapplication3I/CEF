// <autogenerated>
// <copyright file="Mapping.Purchasing.PurchaseOrderEventType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Purchasing section of the Mapping class</summary>
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

    public static partial class ModelMapperForPurchaseOrderEventType
    {
        public sealed class AnonPurchaseOrderEventType : PurchaseOrderEventType
        {
        }

        public static readonly Func<PurchaseOrderEventType?, string?, ITypeModel?> MapPurchaseOrderEventTypeModelFromEntityFull = CreatePurchaseOrderEventTypeModelFromEntityFull;

        public static readonly Func<PurchaseOrderEventType?, string?, ITypeModel?> MapPurchaseOrderEventTypeModelFromEntityLite = CreatePurchaseOrderEventTypeModelFromEntityLite;

        public static readonly Func<PurchaseOrderEventType?, string?, ITypeModel?> MapPurchaseOrderEventTypeModelFromEntityList = CreatePurchaseOrderEventTypeModelFromEntityList;

        public static Func<IPurchaseOrderEventType, ITypeModel, string?, ITypeModel>? CreatePurchaseOrderEventTypeModelFromEntityHooksFull { get; set; }

        public static Func<IPurchaseOrderEventType, ITypeModel, string?, ITypeModel>? CreatePurchaseOrderEventTypeModelFromEntityHooksLite { get; set; }

        public static Func<IPurchaseOrderEventType, ITypeModel, string?, ITypeModel>? CreatePurchaseOrderEventTypeModelFromEntityHooksList { get; set; }

        public static Expression<Func<PurchaseOrderEventType, AnonPurchaseOrderEventType>>? PreBuiltPurchaseOrderEventTypeSQLSelectorFull { get; set; }

        public static Expression<Func<PurchaseOrderEventType, AnonPurchaseOrderEventType>>? PreBuiltPurchaseOrderEventTypeSQLSelectorLite { get; set; }

        public static Expression<Func<PurchaseOrderEventType, AnonPurchaseOrderEventType>>? PreBuiltPurchaseOrderEventTypeSQLSelectorList { get; set; }

        /// <summary>An <see cref="ITypeModel"/> extension method that creates a(n) <see cref="PurchaseOrderEventType"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="PurchaseOrderEventType"/> entity.</returns>
        public static IPurchaseOrderEventType CreatePurchaseOrderEventTypeEntity(
            this ITypeModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityTypableBase<ITypeModel, PurchaseOrderEventType>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdatePurchaseOrderEventTypeFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="ITypeModel"/> extension method that updates a(n) <see cref="PurchaseOrderEventType"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="PurchaseOrderEventType"/> entity.</returns>
        public static IPurchaseOrderEventType UpdatePurchaseOrderEventTypeFromModel(
            this IPurchaseOrderEventType entity,
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

        public static void GenPurchaseOrderEventTypeSQLSelectorFull()
        {
            PreBuiltPurchaseOrderEventTypeSQLSelectorFull = x => x == null ? null! : new AnonPurchaseOrderEventType
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

        public static void GenPurchaseOrderEventTypeSQLSelectorLite()
        {
            PreBuiltPurchaseOrderEventTypeSQLSelectorLite = x => x == null ? null! : new AnonPurchaseOrderEventType
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

        public static void GenPurchaseOrderEventTypeSQLSelectorList()
        {
            PreBuiltPurchaseOrderEventTypeSQLSelectorList = x => x == null ? null! : new AnonPurchaseOrderEventType
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

        public static IEnumerable<ITypeModel> SelectFullPurchaseOrderEventTypeAndMapToTypeModel(
            this IQueryable<PurchaseOrderEventType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPurchaseOrderEventTypeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltPurchaseOrderEventTypeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreatePurchaseOrderEventTypeModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<ITypeModel> SelectLitePurchaseOrderEventTypeAndMapToTypeModel(
            this IQueryable<PurchaseOrderEventType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPurchaseOrderEventTypeSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltPurchaseOrderEventTypeSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreatePurchaseOrderEventTypeModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<ITypeModel> SelectListPurchaseOrderEventTypeAndMapToTypeModel(
            this IQueryable<PurchaseOrderEventType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPurchaseOrderEventTypeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltPurchaseOrderEventTypeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreatePurchaseOrderEventTypeModelFromEntityList(x, contextProfileName))!;
        }

        public static ITypeModel? SelectFirstFullPurchaseOrderEventTypeAndMapToTypeModel(
            this IQueryable<PurchaseOrderEventType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPurchaseOrderEventTypeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPurchaseOrderEventTypeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreatePurchaseOrderEventTypeModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ITypeModel? SelectFirstListPurchaseOrderEventTypeAndMapToTypeModel(
            this IQueryable<PurchaseOrderEventType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPurchaseOrderEventTypeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPurchaseOrderEventTypeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreatePurchaseOrderEventTypeModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ITypeModel? SelectSingleFullPurchaseOrderEventTypeAndMapToTypeModel(
            this IQueryable<PurchaseOrderEventType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPurchaseOrderEventTypeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPurchaseOrderEventTypeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreatePurchaseOrderEventTypeModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ITypeModel? SelectSingleLitePurchaseOrderEventTypeAndMapToTypeModel(
            this IQueryable<PurchaseOrderEventType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPurchaseOrderEventTypeSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPurchaseOrderEventTypeSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreatePurchaseOrderEventTypeModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ITypeModel? SelectSingleListPurchaseOrderEventTypeAndMapToTypeModel(
            this IQueryable<PurchaseOrderEventType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPurchaseOrderEventTypeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPurchaseOrderEventTypeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreatePurchaseOrderEventTypeModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<ITypeModel> results, int totalPages, int totalCount) SelectFullPurchaseOrderEventTypeAndMapToTypeModel(
            this IQueryable<PurchaseOrderEventType> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPurchaseOrderEventTypeSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltPurchaseOrderEventTypeSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreatePurchaseOrderEventTypeModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ITypeModel> results, int totalPages, int totalCount) SelectLitePurchaseOrderEventTypeAndMapToTypeModel(
            this IQueryable<PurchaseOrderEventType> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPurchaseOrderEventTypeSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltPurchaseOrderEventTypeSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreatePurchaseOrderEventTypeModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ITypeModel> results, int totalPages, int totalCount) SelectListPurchaseOrderEventTypeAndMapToTypeModel(
            this IQueryable<PurchaseOrderEventType> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPurchaseOrderEventTypeSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltPurchaseOrderEventTypeSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreatePurchaseOrderEventTypeModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static ITypeModel? CreatePurchaseOrderEventTypeModelFromEntityFull(this IPurchaseOrderEventType? entity, string? contextProfileName)
        {
            return CreatePurchaseOrderEventTypeModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static ITypeModel? CreatePurchaseOrderEventTypeModelFromEntityLite(this IPurchaseOrderEventType? entity, string? contextProfileName)
        {
            return CreatePurchaseOrderEventTypeModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static ITypeModel? CreatePurchaseOrderEventTypeModelFromEntityList(this IPurchaseOrderEventType? entity, string? contextProfileName)
        {
            return CreatePurchaseOrderEventTypeModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static ITypeModel? CreatePurchaseOrderEventTypeModelFromEntity(
            this IPurchaseOrderEventType? entity,
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
                    // PurchaseOrderEventType's Properties
                    // PurchaseOrderEventType's Related Objects
                    // PurchaseOrderEventType's Associated Objects
                    // Additional Mappings
                    if (CreatePurchaseOrderEventTypeModelFromEntityHooksFull != null) { model = CreatePurchaseOrderEventTypeModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // PurchaseOrderEventType's Properties
                    // PurchaseOrderEventType's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // PurchaseOrderEventType's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreatePurchaseOrderEventTypeModelFromEntityHooksLite != null) { model = CreatePurchaseOrderEventTypeModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // PurchaseOrderEventType's Properties
                    // PurchaseOrderEventType's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // PurchaseOrderEventType's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreatePurchaseOrderEventTypeModelFromEntityHooksList != null) { model = CreatePurchaseOrderEventTypeModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
