// <autogenerated>
// <copyright file="Mapping.Currencies.Currency.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Currencies section of the Mapping class</summary>
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

    public static partial class ModelMapperForCurrency
    {
        public sealed class AnonCurrency : Currency
        {
        }

        public static readonly Func<Currency?, string?, ICurrencyModel?> MapCurrencyModelFromEntityFull = CreateCurrencyModelFromEntityFull;

        public static readonly Func<Currency?, string?, ICurrencyModel?> MapCurrencyModelFromEntityLite = CreateCurrencyModelFromEntityLite;

        public static readonly Func<Currency?, string?, ICurrencyModel?> MapCurrencyModelFromEntityList = CreateCurrencyModelFromEntityList;

        public static Func<ICurrency, ICurrencyModel, string?, ICurrencyModel>? CreateCurrencyModelFromEntityHooksFull { get; set; }

        public static Func<ICurrency, ICurrencyModel, string?, ICurrencyModel>? CreateCurrencyModelFromEntityHooksLite { get; set; }

        public static Func<ICurrency, ICurrencyModel, string?, ICurrencyModel>? CreateCurrencyModelFromEntityHooksList { get; set; }

        public static Expression<Func<Currency, AnonCurrency>>? PreBuiltCurrencySQLSelectorFull { get; set; }

        public static Expression<Func<Currency, AnonCurrency>>? PreBuiltCurrencySQLSelectorLite { get; set; }

        public static Expression<Func<Currency, AnonCurrency>>? PreBuiltCurrencySQLSelectorList { get; set; }

        /// <summary>An <see cref="ICurrencyModel"/> extension method that creates a(n) <see cref="DataModel.Currency"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="DataModel.Currency"/> entity.</returns>
        public static ICurrency CreateCurrencyEntity(
            this ICurrencyModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelNameableBase<ICurrencyModel, Currency>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateCurrencyFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="ICurrencyModel"/> extension method that updates a(n) <see cref="DataModel.Currency"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="DataModel.Currency"/> entity.</returns>
        public static ICurrency UpdateCurrencyFromModel(
            this ICurrency entity,
            ICurrencyModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapNameableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // Currency Properties
            entity.DecimalPlaceAccuracy = model.DecimalPlaceAccuracy;
            entity.HtmlCharacterCode = model.HtmlCharacterCode;
            entity.HtmlDecimalCharacterCode = model.HtmlDecimalCharacterCode;
            entity.HtmlSeparatorCharacterCode = model.HtmlSeparatorCharacterCode;
            entity.ISO4217Alpha = model.ISO4217Alpha;
            entity.ISO4217Numeric = model.ISO4217Numeric;
            entity.RawCharacter = model.RawCharacter;
            entity.RawDecimalCharacter = model.RawDecimalCharacter;
            entity.RawSeparatorCharacter = model.RawSeparatorCharacter;
            entity.UnicodeSymbolValue = model.UnicodeSymbolValue;
            entity.UseSeparator = model.UseSeparator;
            // Currency's Associated Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenCurrencySQLSelectorFull()
        {
            PreBuiltCurrencySQLSelectorFull = x => x == null ? null! : new AnonCurrency
            {
                ISO4217Alpha = x.ISO4217Alpha,
                ISO4217Numeric = x.ISO4217Numeric,
                UnicodeSymbolValue = x.UnicodeSymbolValue,
                HtmlCharacterCode = x.HtmlCharacterCode,
                RawCharacter = x.RawCharacter,
                DecimalPlaceAccuracy = x.DecimalPlaceAccuracy,
                UseSeparator = x.UseSeparator,
                RawDecimalCharacter = x.RawDecimalCharacter,
                HtmlDecimalCharacterCode = x.HtmlDecimalCharacterCode,
                RawSeparatorCharacter = x.RawSeparatorCharacter,
                HtmlSeparatorCharacterCode = x.HtmlSeparatorCharacterCode,
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

        public static void GenCurrencySQLSelectorLite()
        {
            PreBuiltCurrencySQLSelectorLite = x => x == null ? null! : new AnonCurrency
            {
                ISO4217Alpha = x.ISO4217Alpha,
                ISO4217Numeric = x.ISO4217Numeric,
                UnicodeSymbolValue = x.UnicodeSymbolValue,
                HtmlCharacterCode = x.HtmlCharacterCode,
                RawCharacter = x.RawCharacter,
                DecimalPlaceAccuracy = x.DecimalPlaceAccuracy,
                UseSeparator = x.UseSeparator,
                RawDecimalCharacter = x.RawDecimalCharacter,
                HtmlDecimalCharacterCode = x.HtmlDecimalCharacterCode,
                RawSeparatorCharacter = x.RawSeparatorCharacter,
                HtmlSeparatorCharacterCode = x.HtmlSeparatorCharacterCode,
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

        public static void GenCurrencySQLSelectorList()
        {
            PreBuiltCurrencySQLSelectorList = x => x == null ? null! : new AnonCurrency
            {
                ISO4217Alpha = x.ISO4217Alpha,
                ISO4217Numeric = x.ISO4217Numeric,
                UnicodeSymbolValue = x.UnicodeSymbolValue,
                HtmlCharacterCode = x.HtmlCharacterCode,
                RawCharacter = x.RawCharacter,
                DecimalPlaceAccuracy = x.DecimalPlaceAccuracy,
                UseSeparator = x.UseSeparator,
                RawDecimalCharacter = x.RawDecimalCharacter,
                HtmlDecimalCharacterCode = x.HtmlDecimalCharacterCode,
                RawSeparatorCharacter = x.RawSeparatorCharacter,
                HtmlSeparatorCharacterCode = x.HtmlSeparatorCharacterCode,
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<ICurrencyModel> SelectFullCurrencyAndMapToCurrencyModel(
            this IQueryable<Currency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCurrencySQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltCurrencySQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateCurrencyModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<ICurrencyModel> SelectLiteCurrencyAndMapToCurrencyModel(
            this IQueryable<Currency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCurrencySQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltCurrencySQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateCurrencyModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<ICurrencyModel> SelectListCurrencyAndMapToCurrencyModel(
            this IQueryable<Currency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCurrencySQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltCurrencySQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateCurrencyModelFromEntityList(x, contextProfileName))!;
        }

        public static ICurrencyModel? SelectFirstFullCurrencyAndMapToCurrencyModel(
            this IQueryable<Currency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCurrencySQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltCurrencySQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateCurrencyModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ICurrencyModel? SelectFirstListCurrencyAndMapToCurrencyModel(
            this IQueryable<Currency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCurrencySQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltCurrencySQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateCurrencyModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ICurrencyModel? SelectSingleFullCurrencyAndMapToCurrencyModel(
            this IQueryable<Currency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCurrencySQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltCurrencySQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateCurrencyModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ICurrencyModel? SelectSingleLiteCurrencyAndMapToCurrencyModel(
            this IQueryable<Currency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCurrencySQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltCurrencySQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateCurrencyModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ICurrencyModel? SelectSingleListCurrencyAndMapToCurrencyModel(
            this IQueryable<Currency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCurrencySQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltCurrencySQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateCurrencyModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<ICurrencyModel> results, int totalPages, int totalCount) SelectFullCurrencyAndMapToCurrencyModel(
            this IQueryable<Currency> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCurrencySQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltCurrencySQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateCurrencyModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ICurrencyModel> results, int totalPages, int totalCount) SelectLiteCurrencyAndMapToCurrencyModel(
            this IQueryable<Currency> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCurrencySQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltCurrencySQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateCurrencyModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ICurrencyModel> results, int totalPages, int totalCount) SelectListCurrencyAndMapToCurrencyModel(
            this IQueryable<Currency> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltCurrencySQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltCurrencySQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateCurrencyModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static ICurrencyModel? CreateCurrencyModelFromEntityFull(this ICurrency? entity, string? contextProfileName)
        {
            return CreateCurrencyModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static ICurrencyModel? CreateCurrencyModelFromEntityLite(this ICurrency? entity, string? contextProfileName)
        {
            return CreateCurrencyModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static ICurrencyModel? CreateCurrencyModelFromEntityList(this ICurrency? entity, string? contextProfileName)
        {
            return CreateCurrencyModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static ICurrencyModel? CreateCurrencyModelFromEntity(
            this ICurrency? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapNameableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<ICurrencyModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // Currency's Properties
                    // Currency's Related Objects
                    // Currency's Associated Objects
                    // Additional Mappings
                    if (CreateCurrencyModelFromEntityHooksFull != null) { model = CreateCurrencyModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // Currency's Properties
                    // Currency's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // Currency's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateCurrencyModelFromEntityHooksLite != null) { model = CreateCurrencyModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // Currency's Properties
                    model.DecimalPlaceAccuracy = entity.DecimalPlaceAccuracy;
                    model.HtmlCharacterCode = entity.HtmlCharacterCode;
                    model.HtmlDecimalCharacterCode = entity.HtmlDecimalCharacterCode;
                    model.HtmlSeparatorCharacterCode = entity.HtmlSeparatorCharacterCode;
                    model.ISO4217Alpha = entity.ISO4217Alpha;
                    model.ISO4217Numeric = entity.ISO4217Numeric;
                    model.RawCharacter = entity.RawCharacter;
                    model.RawDecimalCharacter = entity.RawDecimalCharacter;
                    model.RawSeparatorCharacter = entity.RawSeparatorCharacter;
                    model.UnicodeSymbolValue = entity.UnicodeSymbolValue;
                    model.UseSeparator = entity.UseSeparator;
                    // Currency's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // Currency's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateCurrencyModelFromEntityHooksList != null) { model = CreateCurrencyModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
