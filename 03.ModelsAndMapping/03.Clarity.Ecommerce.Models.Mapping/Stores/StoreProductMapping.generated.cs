// <autogenerated>
// <copyright file="Mapping.Stores.StoreProduct.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Stores section of the Mapping class</summary>
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

    public static partial class ModelMapperForStoreProduct
    {
        public sealed class AnonStoreProduct : StoreProduct
        {
            // public new Store? Master { get; set; }
        }

        public static readonly Func<StoreProduct?, string?, IStoreProductModel?> MapStoreProductModelFromEntityFull = CreateStoreProductModelFromEntityFull;

        public static readonly Func<StoreProduct?, string?, IStoreProductModel?> MapStoreProductModelFromEntityLite = CreateStoreProductModelFromEntityLite;

        public static readonly Func<StoreProduct?, string?, IStoreProductModel?> MapStoreProductModelFromEntityList = CreateStoreProductModelFromEntityList;

        public static Func<IStoreProduct, IStoreProductModel, string?, IStoreProductModel>? CreateStoreProductModelFromEntityHooksFull { get; set; }

        public static Func<IStoreProduct, IStoreProductModel, string?, IStoreProductModel>? CreateStoreProductModelFromEntityHooksLite { get; set; }

        public static Func<IStoreProduct, IStoreProductModel, string?, IStoreProductModel>? CreateStoreProductModelFromEntityHooksList { get; set; }

        public static Expression<Func<StoreProduct, AnonStoreProduct>>? PreBuiltStoreProductSQLSelectorFull { get; set; }

        public static Expression<Func<StoreProduct, AnonStoreProduct>>? PreBuiltStoreProductSQLSelectorLite { get; set; }

        public static Expression<Func<StoreProduct, AnonStoreProduct>>? PreBuiltStoreProductSQLSelectorList { get; set; }

        /// <summary>An <see cref="IStoreProductModel"/> extension method that creates a(n) <see cref="StoreProduct"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="StoreProduct"/> entity.</returns>
        public static IStoreProduct CreateStoreProductEntity(
            this IStoreProductModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IStoreProductModel, StoreProduct>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateStoreProductFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IStoreProductModel"/> extension method that updates a(n) <see cref="StoreProduct"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="StoreProduct"/> entity.</returns>
        public static IStoreProduct UpdateStoreProductFromModel(
            this IStoreProduct entity,
            IStoreProductModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // StoreProduct Properties
            entity.IsVisibleIn = model.IsVisibleIn;
            entity.PriceBase = model.PriceBase;
            entity.PriceMsrp = model.PriceMsrp;
            entity.PriceReduction = model.PriceReduction;
            entity.PriceSale = model.PriceSale;
            // StoreProduct's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenStoreProductSQLSelectorFull()
        {
            PreBuiltStoreProductSQLSelectorFull = x => x == null ? null! : new AnonStoreProduct
            {
                MasterID = x.MasterID,
                Master = ModelMapperForStore.PreBuiltStoreSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForProduct.PreBuiltProductSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                IsVisibleIn = x.IsVisibleIn,
                PriceBase = x.PriceBase,
                PriceMsrp = x.PriceMsrp,
                PriceReduction = x.PriceReduction,
                PriceSale = x.PriceSale,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenStoreProductSQLSelectorLite()
        {
            PreBuiltStoreProductSQLSelectorLite = x => x == null ? null! : new AnonStoreProduct
            {
                MasterID = x.MasterID,
                Master = ModelMapperForStore.PreBuiltStoreSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForProduct.PreBuiltProductSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                IsVisibleIn = x.IsVisibleIn,
                PriceBase = x.PriceBase,
                PriceMsrp = x.PriceMsrp,
                PriceReduction = x.PriceReduction,
                PriceSale = x.PriceSale,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenStoreProductSQLSelectorList()
        {
            PreBuiltStoreProductSQLSelectorList = x => x == null ? null! : new AnonStoreProduct
            {
                MasterID = x.MasterID,
                Master = ModelMapperForStore.PreBuiltStoreSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties (List)
                SlaveID = x.SlaveID,
                Slave = ModelMapperForProduct.PreBuiltProductSQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                IsVisibleIn = x.IsVisibleIn,
                PriceBase = x.PriceBase,
                PriceMsrp = x.PriceMsrp,
                PriceReduction = x.PriceReduction,
                PriceSale = x.PriceSale,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IStoreProductModel> SelectFullStoreProductAndMapToStoreProductModel(
            this IQueryable<StoreProduct> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreProductSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltStoreProductSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateStoreProductModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IStoreProductModel> SelectLiteStoreProductAndMapToStoreProductModel(
            this IQueryable<StoreProduct> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreProductSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltStoreProductSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateStoreProductModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IStoreProductModel> SelectListStoreProductAndMapToStoreProductModel(
            this IQueryable<StoreProduct> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreProductSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltStoreProductSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateStoreProductModelFromEntityList(x, contextProfileName))!;
        }

        public static IStoreProductModel? SelectFirstFullStoreProductAndMapToStoreProductModel(
            this IQueryable<StoreProduct> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreProductSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreProductSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateStoreProductModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStoreProductModel? SelectFirstListStoreProductAndMapToStoreProductModel(
            this IQueryable<StoreProduct> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreProductSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreProductSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateStoreProductModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStoreProductModel? SelectSingleFullStoreProductAndMapToStoreProductModel(
            this IQueryable<StoreProduct> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreProductSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreProductSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateStoreProductModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStoreProductModel? SelectSingleLiteStoreProductAndMapToStoreProductModel(
            this IQueryable<StoreProduct> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreProductSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreProductSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateStoreProductModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStoreProductModel? SelectSingleListStoreProductAndMapToStoreProductModel(
            this IQueryable<StoreProduct> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreProductSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltStoreProductSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateStoreProductModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IStoreProductModel> results, int totalPages, int totalCount) SelectFullStoreProductAndMapToStoreProductModel(
            this IQueryable<StoreProduct> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreProductSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltStoreProductSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateStoreProductModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStoreProductModel> results, int totalPages, int totalCount) SelectLiteStoreProductAndMapToStoreProductModel(
            this IQueryable<StoreProduct> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreProductSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltStoreProductSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateStoreProductModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStoreProductModel> results, int totalPages, int totalCount) SelectListStoreProductAndMapToStoreProductModel(
            this IQueryable<StoreProduct> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltStoreProductSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltStoreProductSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateStoreProductModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IStoreProductModel? CreateStoreProductModelFromEntityFull(this IStoreProduct? entity, string? contextProfileName)
        {
            return CreateStoreProductModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IStoreProductModel? CreateStoreProductModelFromEntityLite(this IStoreProduct? entity, string? contextProfileName)
        {
            return CreateStoreProductModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IStoreProductModel? CreateStoreProductModelFromEntityList(this IStoreProduct? entity, string? contextProfileName)
        {
            return CreateStoreProductModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IStoreProductModel? CreateStoreProductModelFromEntity(
            this IStoreProduct? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IStoreProductModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // StoreProduct's Properties
                    // StoreProduct's Related Objects
                    // StoreProduct's Associated Objects
                    // Additional Mappings
                    if (CreateStoreProductModelFromEntityHooksFull != null) { model = CreateStoreProductModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // StoreProduct's Properties
                    // StoreProduct's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.Slave = ModelMapperForProduct.MapLiteProductOldExt(entity.Slave);
                    // StoreProduct's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateStoreProductModelFromEntityHooksLite != null) { model = CreateStoreProductModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // StoreProduct's Properties
                    model.IsVisibleIn = entity.IsVisibleIn;
                    model.PriceBase = entity.PriceBase;
                    model.PriceMsrp = entity.PriceMsrp;
                    model.PriceReduction = entity.PriceReduction;
                    model.PriceSale = entity.PriceSale;
                    // StoreProduct's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.MasterName = entity.Master?.Name;
                    model.SlaveID = entity.SlaveID;
                    model.SlaveKey = entity.Slave?.CustomKey;
                    model.SlaveName = entity.Slave?.Name;
                    // StoreProduct's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateStoreProductModelFromEntityHooksList != null) { model = CreateStoreProductModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
