// <autogenerated>
// <copyright file="Mapping.Ordering.SalesOrderItem.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForSalesOrderItem
    {
        public sealed class AnonSalesOrderItem : SalesOrderItem
        {
            public new IEnumerable<Note>? Notes { get; set; }
            public new IEnumerable<SalesOrderItemTarget>? Targets { get; set; }
            public Contact? UserContact { get; set; }
            // public new SalesOrder? Master { get; set; }
        }

        public static readonly Func<SalesOrderItem?, string?, ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>?> MapSalesOrderItemModelFromEntityFull = CreateSalesOrderItemModelFromEntityFull;

        public static readonly Func<SalesOrderItem?, string?, ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>?> MapSalesOrderItemModelFromEntityLite = CreateSalesOrderItemModelFromEntityLite;

        public static readonly Func<SalesOrderItem?, string?, ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>?> MapSalesOrderItemModelFromEntityList = CreateSalesOrderItemModelFromEntityList;

        public static Func<ISalesOrderItem, ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>, string?, ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>? CreateSalesOrderItemModelFromEntityHooksFull { get; set; }

        public static Func<ISalesOrderItem, ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>, string?, ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>? CreateSalesOrderItemModelFromEntityHooksLite { get; set; }

        public static Func<ISalesOrderItem, ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>, string?, ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>? CreateSalesOrderItemModelFromEntityHooksList { get; set; }

        public static Expression<Func<SalesOrderItem, AnonSalesOrderItem>>? PreBuiltSalesOrderItemSQLSelectorFull { get; set; }

        public static Expression<Func<SalesOrderItem, AnonSalesOrderItem>>? PreBuiltSalesOrderItemSQLSelectorLite { get; set; }

        public static Expression<Func<SalesOrderItem, AnonSalesOrderItem>>? PreBuiltSalesOrderItemSQLSelectorList { get; set; }

        /// <summary>An <see cref="ISalesItemBaseModel"/> extension method that creates a(n) <see cref="SalesOrderItem"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="SalesOrderItem"/> entity.</returns>
        public static ISalesOrderItem CreateSalesOrderItemEntity(
            this ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel> model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelNameableBase<ISalesItemBaseModel, SalesOrderItem>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateSalesOrderItemFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="ISalesItemBaseModel"/> extension method that updates a(n) <see cref="SalesOrderItem"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="SalesOrderItem"/> entity.</returns>
        public static ISalesOrderItem UpdateSalesOrderItemFromModel(
            this ISalesOrderItem entity,
            ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel> model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapNameableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // ISalesItemBase Properties
            entity.Quantity = model.Quantity;
            entity.QuantityBackOrdered = model.QuantityBackOrdered ?? 0m;
            entity.QuantityPreSold = model.QuantityPreSold ?? 0m;
            entity.UnitCorePrice = model.UnitCorePrice;
            entity.UnitSoldPrice = model.UnitSoldPrice;
            entity.UnitCorePriceInSellingCurrency = model.UnitCorePriceInSellingCurrency;
            entity.UnitSoldPriceInSellingCurrency = model.UnitSoldPriceInSellingCurrency;
            entity.Sku = model.Sku;
            entity.UnitOfMeasure = model.UnitOfMeasure;
            entity.ForceUniqueLineItemKey = model.ForceUniqueLineItemKey;
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenSalesOrderItemSQLSelectorFull()
        {
            PreBuiltSalesOrderItemSQLSelectorFull = x => x == null ? null! : new AnonSalesOrderItem
            {
                ProductID = x.ProductID,
                Product = ModelMapperForProduct.PreBuiltProductSQLSelectorList.Expand().Compile().Invoke(x.Product!),
                UserID = x.UserID,
                User = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.User!),
                Sku = x.Sku,
                UnitOfMeasure = x.UnitOfMeasure,
                ForceUniqueLineItemKey = x.ForceUniqueLineItemKey,
                Quantity = x.Quantity,
                QuantityBackOrdered = x.QuantityBackOrdered,
                QuantityPreSold = x.QuantityPreSold,
                UnitCorePrice = x.UnitCorePrice,
                UnitSoldPrice = x.UnitSoldPrice,
                UnitCorePriceInSellingCurrency = x.UnitCorePriceInSellingCurrency,
                UnitSoldPriceInSellingCurrency = x.UnitSoldPriceInSellingCurrency,
                Status = x.Status,
                MasterID = x.MasterID,
                OriginalCurrencyID = x.OriginalCurrencyID,
                OriginalCurrency = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.OriginalCurrency!),
                SellingCurrencyID = x.SellingCurrencyID,
                SellingCurrency = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.SellingCurrency!),
                Targets = x.Targets!.Where(y => y.Active).Select(ModelMapperForSalesOrderItemTarget.PreBuiltSalesOrderItemTargetSQLSelectorList.Expand().Compile()).ToList(),
                Notes = x.Notes!.Where(y => y.Active).Select(ModelMapperForNote.PreBuiltNoteSQLSelectorList.Expand().Compile()).ToList(),
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

        public static void GenSalesOrderItemSQLSelectorLite()
        {
            PreBuiltSalesOrderItemSQLSelectorLite = x => x == null ? null! : new AnonSalesOrderItem
            {
                ProductID = x.ProductID,
                Product = ModelMapperForProduct.PreBuiltProductSQLSelectorList.Expand().Compile().Invoke(x.Product!),
                UserID = x.UserID,
                User = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.User!),
                Sku = x.Sku,
                UnitOfMeasure = x.UnitOfMeasure,
                ForceUniqueLineItemKey = x.ForceUniqueLineItemKey,
                Quantity = x.Quantity,
                QuantityBackOrdered = x.QuantityBackOrdered,
                QuantityPreSold = x.QuantityPreSold,
                UnitCorePrice = x.UnitCorePrice,
                UnitSoldPrice = x.UnitSoldPrice,
                UnitCorePriceInSellingCurrency = x.UnitCorePriceInSellingCurrency,
                UnitSoldPriceInSellingCurrency = x.UnitSoldPriceInSellingCurrency,
                Status = x.Status,
                MasterID = x.MasterID,
                OriginalCurrencyID = x.OriginalCurrencyID,
                OriginalCurrency = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.OriginalCurrency!),
                SellingCurrencyID = x.SellingCurrencyID,
                SellingCurrency = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.SellingCurrency!),
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

        public static void GenSalesOrderItemSQLSelectorList()
        {
            PreBuiltSalesOrderItemSQLSelectorList = x => x == null ? null! : new AnonSalesOrderItem
            {
                ProductID = x.ProductID,
                Product = ModelMapperForProduct.PreBuiltProductSQLSelectorList.Expand().Compile().Invoke(x.Product!), // For Flattening Properties (List)
                UserID = x.UserID,
                User = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.User!), // For Flattening Properties (List)
                Sku = x.Sku,
                UnitOfMeasure = x.UnitOfMeasure,
                ForceUniqueLineItemKey = x.ForceUniqueLineItemKey,
                Quantity = x.Quantity,
                QuantityBackOrdered = x.QuantityBackOrdered,
                QuantityPreSold = x.QuantityPreSold,
                UnitCorePrice = x.UnitCorePrice,
                UnitSoldPrice = x.UnitSoldPrice,
                UnitCorePriceInSellingCurrency = x.UnitCorePriceInSellingCurrency,
                UnitSoldPriceInSellingCurrency = x.UnitSoldPriceInSellingCurrency,
                Status = x.Status,
                MasterID = x.MasterID,
                OriginalCurrencyID = x.OriginalCurrencyID,
                OriginalCurrency = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.OriginalCurrency!), // For Flattening Properties (List)
                SellingCurrencyID = x.SellingCurrencyID,
                SellingCurrency = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.SellingCurrency!), // For Flattening Properties (List)
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>> SelectFullSalesOrderItemAndMapToSalesItemBaseModel(
            this IQueryable<SalesOrderItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderItemSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderItemSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderItemModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>> SelectLiteSalesOrderItemAndMapToSalesItemBaseModel(
            this IQueryable<SalesOrderItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderItemSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderItemSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderItemModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>> SelectListSalesOrderItemAndMapToSalesItemBaseModel(
            this IQueryable<SalesOrderItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderItemSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderItemSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderItemModelFromEntityList(x, contextProfileName))!;
        }

        public static ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>? SelectFirstFullSalesOrderItemAndMapToSalesItemBaseModel(
            this IQueryable<SalesOrderItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderItemSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderItemSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderItemModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>? SelectFirstListSalesOrderItemAndMapToSalesItemBaseModel(
            this IQueryable<SalesOrderItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderItemSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderItemSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderItemModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>? SelectSingleFullSalesOrderItemAndMapToSalesItemBaseModel(
            this IQueryable<SalesOrderItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderItemSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderItemSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderItemModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>? SelectSingleLiteSalesOrderItemAndMapToSalesItemBaseModel(
            this IQueryable<SalesOrderItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderItemSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderItemSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderItemModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>? SelectSingleListSalesOrderItemAndMapToSalesItemBaseModel(
            this IQueryable<SalesOrderItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderItemSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderItemSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderItemModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>> results, int totalPages, int totalCount) SelectFullSalesOrderItemAndMapToSalesItemBaseModel(
            this IQueryable<SalesOrderItem> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderItemSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderItemSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderItemModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>> results, int totalPages, int totalCount) SelectLiteSalesOrderItemAndMapToSalesItemBaseModel(
            this IQueryable<SalesOrderItem> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderItemSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderItemSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderItemModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>> results, int totalPages, int totalCount) SelectListSalesOrderItemAndMapToSalesItemBaseModel(
            this IQueryable<SalesOrderItem> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderItemSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderItemSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderItemModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>? CreateSalesOrderItemModelFromEntityFull(this ISalesOrderItem? entity, string? contextProfileName)
        {
            return CreateSalesOrderItemModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>? CreateSalesOrderItemModelFromEntityLite(this ISalesOrderItem? entity, string? contextProfileName)
        {
            return CreateSalesOrderItemModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>? CreateSalesOrderItemModelFromEntityList(this ISalesOrderItem? entity, string? contextProfileName)
        {
            return CreateSalesOrderItemModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>? CreateSalesOrderItemModelFromEntity(
            this ISalesOrderItem? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapSalesItemBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // ISalesItemBase<,,,,> Properties
                    model.Discounts = (entity is AnonSalesOrderItem ? ((AnonSalesOrderItem)entity).Discounts : entity.Discounts)?.Where(x => x.Active).Select(x => ModelMapperForAppliedSalesOrderItemDiscount.CreateAppliedSalesOrderItemDiscountModelFromEntityList(x, contextProfileName)).ToList()!;
                    model.Targets = (entity is AnonSalesOrderItem ? ((AnonSalesOrderItem)entity).Targets : entity.Targets)?.Where(x => x.Active).Select(x => ModelMapperForSalesOrderItemTarget.CreateSalesOrderItemTargetModelFromEntityList(x, contextProfileName)).ToList()!;
                    // IHaveNotesBase Properties
                    model.Notes = (entity is AnonSalesOrderItem ? ((AnonSalesOrderItem)entity).Notes : entity.Notes)?.Where(x => x.Active).Select(x => ModelMapperForNote.CreateNoteModelFromEntityList(x, contextProfileName)).ToList()!;
                    // SalesOrderItem's Properties
                    // SalesOrderItem's Related Objects
                    // SalesOrderItem's Associated Objects
                    // Additional Mappings
                    if (CreateSalesOrderItemModelFromEntityHooksFull != null) { model = CreateSalesOrderItemModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // SalesOrderItem's Properties
                    // SalesOrderItem's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SalesOrderItem's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesOrderItemModelFromEntityHooksLite != null) { model = CreateSalesOrderItemModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // SalesOrderItem's Properties
                    // SalesOrderItem's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SalesOrderItem's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesOrderItemModelFromEntityHooksList != null) { model = CreateSalesOrderItemModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
