// <autogenerated>
// <copyright file="Mapping.Products.ProductStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Products section of the Mapping class</summary>
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

    public static partial class ModelMapperForProductStatus
    {
        public sealed class AnonProductStatus : ProductStatus
        {
        }

        public static readonly Func<ProductStatus?, string?, IStatusModel?> MapProductStatusModelFromEntityFull = CreateProductStatusModelFromEntityFull;

        public static readonly Func<ProductStatus?, string?, IStatusModel?> MapProductStatusModelFromEntityLite = CreateProductStatusModelFromEntityLite;

        public static readonly Func<ProductStatus?, string?, IStatusModel?> MapProductStatusModelFromEntityList = CreateProductStatusModelFromEntityList;

        public static Func<IProductStatus, IStatusModel, string?, IStatusModel>? CreateProductStatusModelFromEntityHooksFull { get; set; }

        public static Func<IProductStatus, IStatusModel, string?, IStatusModel>? CreateProductStatusModelFromEntityHooksLite { get; set; }

        public static Func<IProductStatus, IStatusModel, string?, IStatusModel>? CreateProductStatusModelFromEntityHooksList { get; set; }

        public static Expression<Func<ProductStatus, AnonProductStatus>>? PreBuiltProductStatusSQLSelectorFull { get; set; }

        public static Expression<Func<ProductStatus, AnonProductStatus>>? PreBuiltProductStatusSQLSelectorLite { get; set; }

        public static Expression<Func<ProductStatus, AnonProductStatus>>? PreBuiltProductStatusSQLSelectorList { get; set; }

        /// <summary>An <see cref="IStatusModel"/> extension method that creates a(n) <see cref="ProductStatus"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="ProductStatus"/> entity.</returns>
        public static IProductStatus CreateProductStatusEntity(
            this IStatusModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityStatusableBase<IStatusModel, ProductStatus>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateProductStatusFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IStatusModel"/> extension method that updates a(n) <see cref="ProductStatus"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="ProductStatus"/> entity.</returns>
        public static IProductStatus UpdateProductStatusFromModel(
            this IProductStatus entity,
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

        public static void GenProductStatusSQLSelectorFull()
        {
            PreBuiltProductStatusSQLSelectorFull = x => x == null ? null! : new AnonProductStatus
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

        public static void GenProductStatusSQLSelectorLite()
        {
            PreBuiltProductStatusSQLSelectorLite = x => x == null ? null! : new AnonProductStatus
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

        public static void GenProductStatusSQLSelectorList()
        {
            PreBuiltProductStatusSQLSelectorList = x => x == null ? null! : new AnonProductStatus
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

        public static IEnumerable<IStatusModel> SelectFullProductStatusAndMapToStatusModel(
            this IQueryable<ProductStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltProductStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateProductStatusModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IStatusModel> SelectLiteProductStatusAndMapToStatusModel(
            this IQueryable<ProductStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductStatusSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltProductStatusSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateProductStatusModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IStatusModel> SelectListProductStatusAndMapToStatusModel(
            this IQueryable<ProductStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltProductStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateProductStatusModelFromEntityList(x, contextProfileName))!;
        }

        public static IStatusModel? SelectFirstFullProductStatusAndMapToStatusModel(
            this IQueryable<ProductStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltProductStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateProductStatusModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStatusModel? SelectFirstListProductStatusAndMapToStatusModel(
            this IQueryable<ProductStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltProductStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateProductStatusModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStatusModel? SelectSingleFullProductStatusAndMapToStatusModel(
            this IQueryable<ProductStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltProductStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateProductStatusModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStatusModel? SelectSingleLiteProductStatusAndMapToStatusModel(
            this IQueryable<ProductStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductStatusSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltProductStatusSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateProductStatusModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStatusModel? SelectSingleListProductStatusAndMapToStatusModel(
            this IQueryable<ProductStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltProductStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateProductStatusModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectFullProductStatusAndMapToStatusModel(
            this IQueryable<ProductStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductStatusSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltProductStatusSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateProductStatusModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectLiteProductStatusAndMapToStatusModel(
            this IQueryable<ProductStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductStatusSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltProductStatusSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateProductStatusModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectListProductStatusAndMapToStatusModel(
            this IQueryable<ProductStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductStatusSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltProductStatusSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateProductStatusModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IStatusModel? CreateProductStatusModelFromEntityFull(this IProductStatus? entity, string? contextProfileName)
        {
            return CreateProductStatusModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IStatusModel? CreateProductStatusModelFromEntityLite(this IProductStatus? entity, string? contextProfileName)
        {
            return CreateProductStatusModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IStatusModel? CreateProductStatusModelFromEntityList(this IProductStatus? entity, string? contextProfileName)
        {
            return CreateProductStatusModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IStatusModel? CreateProductStatusModelFromEntity(
            this IProductStatus? entity,
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
                    // ProductStatus's Properties
                    // ProductStatus's Related Objects
                    // ProductStatus's Associated Objects
                    // Additional Mappings
                    if (CreateProductStatusModelFromEntityHooksFull != null) { model = CreateProductStatusModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // ProductStatus's Properties
                    // ProductStatus's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // ProductStatus's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateProductStatusModelFromEntityHooksLite != null) { model = CreateProductStatusModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // ProductStatus's Properties
                    // ProductStatus's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // ProductStatus's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateProductStatusModelFromEntityHooksList != null) { model = CreateProductStatusModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
