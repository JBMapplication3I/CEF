// <autogenerated>
// <copyright file="Mapping.Products.ProductType.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForProductType
    {
        public sealed class AnonProductType : ProductType
        {
        }

        public static readonly Func<ProductType?, string?, ITypeModel?> MapProductTypeModelFromEntityFull = CreateProductTypeModelFromEntityFull;

        public static readonly Func<ProductType?, string?, ITypeModel?> MapProductTypeModelFromEntityLite = CreateProductTypeModelFromEntityLite;

        public static readonly Func<ProductType?, string?, ITypeModel?> MapProductTypeModelFromEntityList = CreateProductTypeModelFromEntityList;

        public static Func<IProductType, ITypeModel, string?, ITypeModel>? CreateProductTypeModelFromEntityHooksFull { get; set; }

        public static Func<IProductType, ITypeModel, string?, ITypeModel>? CreateProductTypeModelFromEntityHooksLite { get; set; }

        public static Func<IProductType, ITypeModel, string?, ITypeModel>? CreateProductTypeModelFromEntityHooksList { get; set; }

        public static Expression<Func<ProductType, AnonProductType>>? PreBuiltProductTypeSQLSelectorFull { get; set; }

        public static Expression<Func<ProductType, AnonProductType>>? PreBuiltProductTypeSQLSelectorLite { get; set; }

        public static Expression<Func<ProductType, AnonProductType>>? PreBuiltProductTypeSQLSelectorList { get; set; }

        /// <summary>An <see cref="ITypeModel"/> extension method that creates a(n) <see cref="ProductType"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="ProductType"/> entity.</returns>
        public static IProductType CreateProductTypeEntity(
            this ITypeModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityTypableBase<ITypeModel, ProductType>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateProductTypeFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="ITypeModel"/> extension method that updates a(n) <see cref="ProductType"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="ProductType"/> entity.</returns>
        public static IProductType UpdateProductTypeFromModel(
            this IProductType entity,
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

        public static void GenProductTypeSQLSelectorFull()
        {
            PreBuiltProductTypeSQLSelectorFull = x => x == null ? null! : new AnonProductType
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

        public static void GenProductTypeSQLSelectorLite()
        {
            PreBuiltProductTypeSQLSelectorLite = x => x == null ? null! : new AnonProductType
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

        public static void GenProductTypeSQLSelectorList()
        {
            PreBuiltProductTypeSQLSelectorList = x => x == null ? null! : new AnonProductType
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

        public static IEnumerable<ITypeModel> SelectFullProductTypeAndMapToTypeModel(
            this IQueryable<ProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductTypeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltProductTypeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateProductTypeModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<ITypeModel> SelectLiteProductTypeAndMapToTypeModel(
            this IQueryable<ProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductTypeSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltProductTypeSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateProductTypeModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<ITypeModel> SelectListProductTypeAndMapToTypeModel(
            this IQueryable<ProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductTypeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltProductTypeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateProductTypeModelFromEntityList(x, contextProfileName))!;
        }

        public static ITypeModel? SelectFirstFullProductTypeAndMapToTypeModel(
            this IQueryable<ProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductTypeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltProductTypeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateProductTypeModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ITypeModel? SelectFirstListProductTypeAndMapToTypeModel(
            this IQueryable<ProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductTypeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltProductTypeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateProductTypeModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ITypeModel? SelectSingleFullProductTypeAndMapToTypeModel(
            this IQueryable<ProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductTypeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltProductTypeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateProductTypeModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ITypeModel? SelectSingleLiteProductTypeAndMapToTypeModel(
            this IQueryable<ProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductTypeSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltProductTypeSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateProductTypeModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ITypeModel? SelectSingleListProductTypeAndMapToTypeModel(
            this IQueryable<ProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductTypeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltProductTypeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateProductTypeModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<ITypeModel> results, int totalPages, int totalCount) SelectFullProductTypeAndMapToTypeModel(
            this IQueryable<ProductType> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductTypeSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltProductTypeSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateProductTypeModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ITypeModel> results, int totalPages, int totalCount) SelectLiteProductTypeAndMapToTypeModel(
            this IQueryable<ProductType> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductTypeSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltProductTypeSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateProductTypeModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ITypeModel> results, int totalPages, int totalCount) SelectListProductTypeAndMapToTypeModel(
            this IQueryable<ProductType> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltProductTypeSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltProductTypeSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateProductTypeModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static ITypeModel? CreateProductTypeModelFromEntityFull(this IProductType? entity, string? contextProfileName)
        {
            return CreateProductTypeModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static ITypeModel? CreateProductTypeModelFromEntityLite(this IProductType? entity, string? contextProfileName)
        {
            return CreateProductTypeModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static ITypeModel? CreateProductTypeModelFromEntityList(this IProductType? entity, string? contextProfileName)
        {
            return CreateProductTypeModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static ITypeModel? CreateProductTypeModelFromEntity(
            this IProductType? entity,
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
                    // ProductType's Properties
                    // ProductType's Related Objects
                    // ProductType's Associated Objects
                    // Additional Mappings
                    if (CreateProductTypeModelFromEntityHooksFull != null) { model = CreateProductTypeModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // ProductType's Properties
                    // ProductType's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // ProductType's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateProductTypeModelFromEntityHooksLite != null) { model = CreateProductTypeModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // ProductType's Properties
                    // ProductType's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // ProductType's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateProductTypeModelFromEntityHooksList != null) { model = CreateProductTypeModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
