// <autogenerated>
// <copyright file="Mapping.Shopping.CartItem.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Shopping section of the Mapping class</summary>
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

    public static partial class ModelMapperForCartItem
    {
        public sealed class AnonCartItem : CartItem
        {
            public new IEnumerable<CartItemTarget>? Targets { get; set; }
            public new IEnumerable<Note>? Notes { get; set; }
            public Contact? UserContact { get; set; }
            // public new Cart? Master { get; set; }
        }

        public static readonly Func<CartItem?, string?, ISalesItemBaseModel<IAppliedCartItemDiscountModel>?> MapCartItemModelFromEntityFull = CreateCartItemModelFromEntityFull;

        public static readonly Func<CartItem?, string?, ISalesItemBaseModel<IAppliedCartItemDiscountModel>?> MapCartItemModelFromEntityLite = CreateCartItemModelFromEntityLite;

        public static readonly Func<CartItem?, string?, ISalesItemBaseModel<IAppliedCartItemDiscountModel>?> MapCartItemModelFromEntityList = CreateCartItemModelFromEntityList;

        public static Func<ICartItem, ISalesItemBaseModel<IAppliedCartItemDiscountModel>, string?, ISalesItemBaseModel<IAppliedCartItemDiscountModel>>? CreateCartItemModelFromEntityHooksFull { get; set; }

        public static Func<ICartItem, ISalesItemBaseModel<IAppliedCartItemDiscountModel>, string?, ISalesItemBaseModel<IAppliedCartItemDiscountModel>>? CreateCartItemModelFromEntityHooksLite { get; set; }

        public static Func<ICartItem, ISalesItemBaseModel<IAppliedCartItemDiscountModel>, string?, ISalesItemBaseModel<IAppliedCartItemDiscountModel>>? CreateCartItemModelFromEntityHooksList { get; set; }

        public static Expression<Func<CartItem, AnonCartItem>>? PreBuiltCartItemSQLSelectorFull { get; set; }

        public static Expression<Func<CartItem, AnonCartItem>>? PreBuiltCartItemSQLSelectorLite { get; set; }

        public static Expression<Func<CartItem, AnonCartItem>>? PreBuiltCartItemSQLSelectorList { get; set; }

        /// <summary>An <see cref="ISalesItemBaseModel"/> extension method that creates a(n) <see cref="CartItem"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="CartItem"/> entity.</returns>
        public static ICartItem CreateCartItemEntity(
            this ISalesItemBaseModel<IAppliedCartItemDiscountModel> model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelNameableBase<ISalesItemBaseModel, CartItem>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateCartItemFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="ISalesItemBaseModel"/> extension method that updates a(n) <see cref="CartItem"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="CartItem"/> entity.</returns>
        public static ICartItem UpdateCartItemFromModel(
            this ICartItem entity,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> model,
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
            // CartItem Properties
            entity.UnitSoldPriceModifier = model.UnitSoldPriceModifier;
            entity.UnitSoldPriceModifierMode = model.UnitSoldPriceModifierMode;
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenCartItemSQLSelectorFull()
        {
            PreBuiltCartItemSQLSelectorFull = x => x == null ? null! : new AnonCartItem
            {
                UnitSoldPriceModifier = x.UnitSoldPriceModifier,
                UnitSoldPriceModifierMode = x.UnitSoldPriceModifierMode,
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
                Targets = x.Targets!.Where(y => y.Active).Select(ModelMapperForCartItemTarget.PreBuiltCartItemTargetSQLSelectorList.Expand().Compile()).ToList(),
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

        public static void GenCartItemSQLSelectorLite()
        {
            PreBuiltCartItemSQLSelectorLite = x => x == null ? null! : new AnonCartItem
            {
                UnitSoldPriceModifier = x.UnitSoldPriceModifier,
                UnitSoldPriceModifierMode = x.UnitSoldPriceModifierMode,
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

        public static void GenCartItemSQLSelectorList()
        {
            PreBuiltCartItemSQLSelectorList = x => x == null ? null! : new AnonCartItem
            {
                UnitSoldPriceModifier = x.UnitSoldPriceModifier,
                UnitSoldPriceModifierMode = x.UnitSoldPriceModifierMode,
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

        public static IEnumerable<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> SelectFullCartItemAndMapToSalesItemBaseModel(
            this IQueryable<CartItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCartItemSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltCartItemSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateCartItemModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> SelectLiteCartItemAndMapToSalesItemBaseModel(
            this IQueryable<CartItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCartItemSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltCartItemSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateCartItemModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> SelectListCartItemAndMapToSalesItemBaseModel(
            this IQueryable<CartItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCartItemSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltCartItemSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateCartItemModelFromEntityList(x, contextProfileName))!;
        }

        public static ISalesItemBaseModel<IAppliedCartItemDiscountModel>? SelectFirstFullCartItemAndMapToSalesItemBaseModel(
            this IQueryable<CartItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCartItemSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltCartItemSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateCartItemModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ISalesItemBaseModel<IAppliedCartItemDiscountModel>? SelectFirstListCartItemAndMapToSalesItemBaseModel(
            this IQueryable<CartItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCartItemSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltCartItemSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateCartItemModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ISalesItemBaseModel<IAppliedCartItemDiscountModel>? SelectSingleFullCartItemAndMapToSalesItemBaseModel(
            this IQueryable<CartItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCartItemSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltCartItemSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateCartItemModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ISalesItemBaseModel<IAppliedCartItemDiscountModel>? SelectSingleLiteCartItemAndMapToSalesItemBaseModel(
            this IQueryable<CartItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCartItemSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltCartItemSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateCartItemModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ISalesItemBaseModel<IAppliedCartItemDiscountModel>? SelectSingleListCartItemAndMapToSalesItemBaseModel(
            this IQueryable<CartItem> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCartItemSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltCartItemSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateCartItemModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> results, int totalPages, int totalCount) SelectFullCartItemAndMapToSalesItemBaseModel(
            this IQueryable<CartItem> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCartItemSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltCartItemSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateCartItemModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> results, int totalPages, int totalCount) SelectLiteCartItemAndMapToSalesItemBaseModel(
            this IQueryable<CartItem> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCartItemSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltCartItemSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateCartItemModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> results, int totalPages, int totalCount) SelectListCartItemAndMapToSalesItemBaseModel(
            this IQueryable<CartItem> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCartItemSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltCartItemSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateCartItemModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static ISalesItemBaseModel<IAppliedCartItemDiscountModel>? CreateCartItemModelFromEntityFull(this ICartItem? entity, string? contextProfileName)
        {
            return CreateCartItemModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static ISalesItemBaseModel<IAppliedCartItemDiscountModel>? CreateCartItemModelFromEntityLite(this ICartItem? entity, string? contextProfileName)
        {
            return CreateCartItemModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static ISalesItemBaseModel<IAppliedCartItemDiscountModel>? CreateCartItemModelFromEntityList(this ICartItem? entity, string? contextProfileName)
        {
            return CreateCartItemModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static ISalesItemBaseModel<IAppliedCartItemDiscountModel>? CreateCartItemModelFromEntity(
            this ICartItem? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapSalesItemBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // ISalesItemBase<,,,,> Properties
                    model.Discounts = (entity is AnonCartItem ? ((AnonCartItem)entity).Discounts : entity.Discounts)?.Where(x => x.Active).Select(x => ModelMapperForAppliedCartItemDiscount.CreateAppliedCartItemDiscountModelFromEntityList(x, contextProfileName)).ToList()!;
                    // IHaveNotesBase Properties
                    model.Notes = (entity is AnonCartItem ? ((AnonCartItem)entity).Notes : entity.Notes)?.Where(x => x.Active).Select(x => ModelMapperForNote.CreateNoteModelFromEntityList(x, contextProfileName)).ToList()!;
                    // CartItem's Properties
                    // CartItem's Related Objects
                    // CartItem's Associated Objects
                    // Additional Mappings
                    if (CreateCartItemModelFromEntityHooksFull != null) { model = CreateCartItemModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // CartItem's Properties
                    // CartItem's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // CartItem's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateCartItemModelFromEntityHooksLite != null) { model = CreateCartItemModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // ISalesItemBase<,,,,> Properties
                    model.Targets = (entity is AnonCartItem ? ((AnonCartItem)entity).Targets : entity.Targets)?.Where(x => x.Active).Select(x => ModelMapperForCartItemTarget.CreateCartItemTargetModelFromEntityList(x, contextProfileName)).ToList()!;
                    // CartItem's Properties
                    model.UnitSoldPriceModifier = entity.UnitSoldPriceModifier;
                    model.UnitSoldPriceModifierMode = entity.UnitSoldPriceModifierMode;
                    // CartItem's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // CartItem's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateCartItemModelFromEntityHooksList != null) { model = CreateCartItemModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
