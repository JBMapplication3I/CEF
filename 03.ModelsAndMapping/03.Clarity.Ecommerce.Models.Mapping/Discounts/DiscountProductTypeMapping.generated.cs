// <autogenerated>
// <copyright file="Mapping.Discounts.DiscountProductType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Discounts section of the Mapping class</summary>
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

    public static partial class ModelMapperForDiscountProductType
    {
        public sealed class AnonDiscountProductType : DiscountProductType
        {
            // public new Discount? Master { get; set; }
        }

        public static readonly Func<DiscountProductType?, string?, IDiscountProductTypeModel?> MapDiscountProductTypeModelFromEntityFull = CreateDiscountProductTypeModelFromEntityFull;

        public static readonly Func<DiscountProductType?, string?, IDiscountProductTypeModel?> MapDiscountProductTypeModelFromEntityLite = CreateDiscountProductTypeModelFromEntityLite;

        public static readonly Func<DiscountProductType?, string?, IDiscountProductTypeModel?> MapDiscountProductTypeModelFromEntityList = CreateDiscountProductTypeModelFromEntityList;

        public static Func<IDiscountProductType, IDiscountProductTypeModel, string?, IDiscountProductTypeModel>? CreateDiscountProductTypeModelFromEntityHooksFull { get; set; }

        public static Func<IDiscountProductType, IDiscountProductTypeModel, string?, IDiscountProductTypeModel>? CreateDiscountProductTypeModelFromEntityHooksLite { get; set; }

        public static Func<IDiscountProductType, IDiscountProductTypeModel, string?, IDiscountProductTypeModel>? CreateDiscountProductTypeModelFromEntityHooksList { get; set; }

        public static Expression<Func<DiscountProductType, AnonDiscountProductType>>? PreBuiltDiscountProductTypeSQLSelectorFull { get; set; }

        public static Expression<Func<DiscountProductType, AnonDiscountProductType>>? PreBuiltDiscountProductTypeSQLSelectorLite { get; set; }

        public static Expression<Func<DiscountProductType, AnonDiscountProductType>>? PreBuiltDiscountProductTypeSQLSelectorList { get; set; }

        /// <summary>An <see cref="IDiscountProductTypeModel"/> extension method that creates a(n) <see cref="DiscountProductType"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="DiscountProductType"/> entity.</returns>
        public static IDiscountProductType CreateDiscountProductTypeEntity(
            this IDiscountProductTypeModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IDiscountProductTypeModel, DiscountProductType>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateDiscountProductTypeFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IDiscountProductTypeModel"/> extension method that updates a(n) <see cref="DiscountProductType"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="DiscountProductType"/> entity.</returns>
        public static IDiscountProductType UpdateDiscountProductTypeFromModel(
            this IDiscountProductType entity,
            IDiscountProductTypeModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // DiscountProductType's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenDiscountProductTypeSQLSelectorFull()
        {
            PreBuiltDiscountProductTypeSQLSelectorFull = x => x == null ? null! : new AnonDiscountProductType
            {
                MasterID = x.MasterID,
                Master = ModelMapperForDiscount.PreBuiltDiscountSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForProductType.PreBuiltProductTypeSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenDiscountProductTypeSQLSelectorLite()
        {
            PreBuiltDiscountProductTypeSQLSelectorLite = x => x == null ? null! : new AnonDiscountProductType
            {
                MasterID = x.MasterID,
                Master = ModelMapperForDiscount.PreBuiltDiscountSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForProductType.PreBuiltProductTypeSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenDiscountProductTypeSQLSelectorList()
        {
            PreBuiltDiscountProductTypeSQLSelectorList = x => x == null ? null! : new AnonDiscountProductType
            {
                MasterID = x.MasterID,
                Master = ModelMapperForDiscount.PreBuiltDiscountSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties (List)
                SlaveID = x.SlaveID,
                Slave = ModelMapperForProductType.PreBuiltProductTypeSQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IDiscountProductTypeModel> SelectFullDiscountProductTypeAndMapToDiscountProductTypeModel(
            this IQueryable<DiscountProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountProductTypeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltDiscountProductTypeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateDiscountProductTypeModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IDiscountProductTypeModel> SelectLiteDiscountProductTypeAndMapToDiscountProductTypeModel(
            this IQueryable<DiscountProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountProductTypeSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltDiscountProductTypeSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateDiscountProductTypeModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IDiscountProductTypeModel> SelectListDiscountProductTypeAndMapToDiscountProductTypeModel(
            this IQueryable<DiscountProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountProductTypeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltDiscountProductTypeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateDiscountProductTypeModelFromEntityList(x, contextProfileName))!;
        }

        public static IDiscountProductTypeModel? SelectFirstFullDiscountProductTypeAndMapToDiscountProductTypeModel(
            this IQueryable<DiscountProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountProductTypeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountProductTypeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateDiscountProductTypeModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IDiscountProductTypeModel? SelectFirstListDiscountProductTypeAndMapToDiscountProductTypeModel(
            this IQueryable<DiscountProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountProductTypeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountProductTypeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateDiscountProductTypeModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IDiscountProductTypeModel? SelectSingleFullDiscountProductTypeAndMapToDiscountProductTypeModel(
            this IQueryable<DiscountProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountProductTypeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountProductTypeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateDiscountProductTypeModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IDiscountProductTypeModel? SelectSingleLiteDiscountProductTypeAndMapToDiscountProductTypeModel(
            this IQueryable<DiscountProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountProductTypeSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountProductTypeSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateDiscountProductTypeModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IDiscountProductTypeModel? SelectSingleListDiscountProductTypeAndMapToDiscountProductTypeModel(
            this IQueryable<DiscountProductType> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountProductTypeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountProductTypeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateDiscountProductTypeModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IDiscountProductTypeModel> results, int totalPages, int totalCount) SelectFullDiscountProductTypeAndMapToDiscountProductTypeModel(
            this IQueryable<DiscountProductType> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountProductTypeSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltDiscountProductTypeSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateDiscountProductTypeModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IDiscountProductTypeModel> results, int totalPages, int totalCount) SelectLiteDiscountProductTypeAndMapToDiscountProductTypeModel(
            this IQueryable<DiscountProductType> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountProductTypeSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltDiscountProductTypeSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateDiscountProductTypeModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IDiscountProductTypeModel> results, int totalPages, int totalCount) SelectListDiscountProductTypeAndMapToDiscountProductTypeModel(
            this IQueryable<DiscountProductType> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountProductTypeSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltDiscountProductTypeSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateDiscountProductTypeModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IDiscountProductTypeModel? CreateDiscountProductTypeModelFromEntityFull(this IDiscountProductType? entity, string? contextProfileName)
        {
            return CreateDiscountProductTypeModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IDiscountProductTypeModel? CreateDiscountProductTypeModelFromEntityLite(this IDiscountProductType? entity, string? contextProfileName)
        {
            return CreateDiscountProductTypeModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IDiscountProductTypeModel? CreateDiscountProductTypeModelFromEntityList(this IDiscountProductType? entity, string? contextProfileName)
        {
            return CreateDiscountProductTypeModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IDiscountProductTypeModel? CreateDiscountProductTypeModelFromEntity(
            this IDiscountProductType? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IDiscountProductTypeModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // DiscountProductType's Properties
                    // DiscountProductType's Related Objects
                    model.Slave = ModelMapperForProductType.CreateProductTypeModelFromEntityLite(entity.Slave, contextProfileName);
                    // DiscountProductType's Associated Objects
                    // Additional Mappings
                    if (CreateDiscountProductTypeModelFromEntityHooksFull != null) { model = CreateDiscountProductTypeModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // DiscountProductType's Properties
                    // DiscountProductType's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // DiscountProductType's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateDiscountProductTypeModelFromEntityHooksLite != null) { model = CreateDiscountProductTypeModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // IsIAmADiscountFilterRelationshipTable Properties
                    model.MasterID = entity.MasterID;
                    // DiscountProductType's Properties
                    // DiscountProductType's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.SlaveID = entity.SlaveID;
                    model.SlaveKey = entity.Slave?.CustomKey;
                    model.SlaveName = entity.Slave?.Name;
                    // DiscountProductType's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateDiscountProductTypeModelFromEntityHooksList != null) { model = CreateDiscountProductTypeModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
