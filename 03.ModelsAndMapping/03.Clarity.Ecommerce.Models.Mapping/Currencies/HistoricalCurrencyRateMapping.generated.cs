// <autogenerated>
// <copyright file="Mapping.Currencies.HistoricalCurrencyRate.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForHistoricalCurrencyRate
    {
        public sealed class AnonHistoricalCurrencyRate : HistoricalCurrencyRate
        {
        }

        public static readonly Func<HistoricalCurrencyRate?, string?, IHistoricalCurrencyRateModel?> MapHistoricalCurrencyRateModelFromEntityFull = CreateHistoricalCurrencyRateModelFromEntityFull;

        public static readonly Func<HistoricalCurrencyRate?, string?, IHistoricalCurrencyRateModel?> MapHistoricalCurrencyRateModelFromEntityLite = CreateHistoricalCurrencyRateModelFromEntityLite;

        public static readonly Func<HistoricalCurrencyRate?, string?, IHistoricalCurrencyRateModel?> MapHistoricalCurrencyRateModelFromEntityList = CreateHistoricalCurrencyRateModelFromEntityList;

        public static Func<IHistoricalCurrencyRate, IHistoricalCurrencyRateModel, string?, IHistoricalCurrencyRateModel>? CreateHistoricalCurrencyRateModelFromEntityHooksFull { get; set; }

        public static Func<IHistoricalCurrencyRate, IHistoricalCurrencyRateModel, string?, IHistoricalCurrencyRateModel>? CreateHistoricalCurrencyRateModelFromEntityHooksLite { get; set; }

        public static Func<IHistoricalCurrencyRate, IHistoricalCurrencyRateModel, string?, IHistoricalCurrencyRateModel>? CreateHistoricalCurrencyRateModelFromEntityHooksList { get; set; }

        public static Expression<Func<HistoricalCurrencyRate, AnonHistoricalCurrencyRate>>? PreBuiltHistoricalCurrencyRateSQLSelectorFull { get; set; }

        public static Expression<Func<HistoricalCurrencyRate, AnonHistoricalCurrencyRate>>? PreBuiltHistoricalCurrencyRateSQLSelectorLite { get; set; }

        public static Expression<Func<HistoricalCurrencyRate, AnonHistoricalCurrencyRate>>? PreBuiltHistoricalCurrencyRateSQLSelectorList { get; set; }

        /// <summary>An <see cref="IHistoricalCurrencyRateModel"/> extension method that creates a(n) <see cref="HistoricalCurrencyRate"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="HistoricalCurrencyRate"/> entity.</returns>
        public static IHistoricalCurrencyRate CreateHistoricalCurrencyRateEntity(
            this IHistoricalCurrencyRateModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IHistoricalCurrencyRateModel, HistoricalCurrencyRate>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateHistoricalCurrencyRateFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IHistoricalCurrencyRateModel"/> extension method that updates a(n) <see cref="HistoricalCurrencyRate"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="HistoricalCurrencyRate"/> entity.</returns>
        public static IHistoricalCurrencyRate UpdateHistoricalCurrencyRateFromModel(
            this IHistoricalCurrencyRate entity,
            IHistoricalCurrencyRateModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // HistoricalCurrencyRate Properties
            entity.OnDate = model.OnDate;
            entity.Rate = model.Rate;
            // HistoricalCurrencyRate's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenHistoricalCurrencyRateSQLSelectorFull()
        {
            PreBuiltHistoricalCurrencyRateSQLSelectorFull = x => x == null ? null! : new AnonHistoricalCurrencyRate
            {
                Rate = x.Rate,
                OnDate = x.OnDate,
                StartingCurrencyID = x.StartingCurrencyID,
                StartingCurrency = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.StartingCurrency!),
                EndingCurrencyID = x.EndingCurrencyID,
                EndingCurrency = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.EndingCurrency!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenHistoricalCurrencyRateSQLSelectorLite()
        {
            PreBuiltHistoricalCurrencyRateSQLSelectorLite = x => x == null ? null! : new AnonHistoricalCurrencyRate
            {
                Rate = x.Rate,
                OnDate = x.OnDate,
                StartingCurrencyID = x.StartingCurrencyID,
                StartingCurrency = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.StartingCurrency!),
                EndingCurrencyID = x.EndingCurrencyID,
                EndingCurrency = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.EndingCurrency!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenHistoricalCurrencyRateSQLSelectorList()
        {
            PreBuiltHistoricalCurrencyRateSQLSelectorList = x => x == null ? null! : new AnonHistoricalCurrencyRate
            {
                Rate = x.Rate,
                OnDate = x.OnDate,
                StartingCurrencyID = x.StartingCurrencyID,
                StartingCurrency = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.StartingCurrency!), // For Flattening Properties (List)
                EndingCurrencyID = x.EndingCurrencyID,
                EndingCurrency = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.EndingCurrency!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IHistoricalCurrencyRateModel> SelectFullHistoricalCurrencyRateAndMapToHistoricalCurrencyRateModel(
            this IQueryable<HistoricalCurrencyRate> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltHistoricalCurrencyRateSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltHistoricalCurrencyRateSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateHistoricalCurrencyRateModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IHistoricalCurrencyRateModel> SelectLiteHistoricalCurrencyRateAndMapToHistoricalCurrencyRateModel(
            this IQueryable<HistoricalCurrencyRate> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltHistoricalCurrencyRateSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltHistoricalCurrencyRateSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateHistoricalCurrencyRateModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IHistoricalCurrencyRateModel> SelectListHistoricalCurrencyRateAndMapToHistoricalCurrencyRateModel(
            this IQueryable<HistoricalCurrencyRate> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltHistoricalCurrencyRateSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltHistoricalCurrencyRateSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateHistoricalCurrencyRateModelFromEntityList(x, contextProfileName))!;
        }

        public static IHistoricalCurrencyRateModel? SelectFirstFullHistoricalCurrencyRateAndMapToHistoricalCurrencyRateModel(
            this IQueryable<HistoricalCurrencyRate> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltHistoricalCurrencyRateSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltHistoricalCurrencyRateSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateHistoricalCurrencyRateModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IHistoricalCurrencyRateModel? SelectFirstListHistoricalCurrencyRateAndMapToHistoricalCurrencyRateModel(
            this IQueryable<HistoricalCurrencyRate> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltHistoricalCurrencyRateSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltHistoricalCurrencyRateSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateHistoricalCurrencyRateModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IHistoricalCurrencyRateModel? SelectSingleFullHistoricalCurrencyRateAndMapToHistoricalCurrencyRateModel(
            this IQueryable<HistoricalCurrencyRate> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltHistoricalCurrencyRateSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltHistoricalCurrencyRateSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateHistoricalCurrencyRateModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IHistoricalCurrencyRateModel? SelectSingleLiteHistoricalCurrencyRateAndMapToHistoricalCurrencyRateModel(
            this IQueryable<HistoricalCurrencyRate> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltHistoricalCurrencyRateSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltHistoricalCurrencyRateSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateHistoricalCurrencyRateModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IHistoricalCurrencyRateModel? SelectSingleListHistoricalCurrencyRateAndMapToHistoricalCurrencyRateModel(
            this IQueryable<HistoricalCurrencyRate> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltHistoricalCurrencyRateSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltHistoricalCurrencyRateSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateHistoricalCurrencyRateModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IHistoricalCurrencyRateModel> results, int totalPages, int totalCount) SelectFullHistoricalCurrencyRateAndMapToHistoricalCurrencyRateModel(
            this IQueryable<HistoricalCurrencyRate> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltHistoricalCurrencyRateSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltHistoricalCurrencyRateSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateHistoricalCurrencyRateModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IHistoricalCurrencyRateModel> results, int totalPages, int totalCount) SelectLiteHistoricalCurrencyRateAndMapToHistoricalCurrencyRateModel(
            this IQueryable<HistoricalCurrencyRate> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltHistoricalCurrencyRateSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltHistoricalCurrencyRateSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateHistoricalCurrencyRateModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IHistoricalCurrencyRateModel> results, int totalPages, int totalCount) SelectListHistoricalCurrencyRateAndMapToHistoricalCurrencyRateModel(
            this IQueryable<HistoricalCurrencyRate> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltHistoricalCurrencyRateSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltHistoricalCurrencyRateSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateHistoricalCurrencyRateModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IHistoricalCurrencyRateModel? CreateHistoricalCurrencyRateModelFromEntityFull(this IHistoricalCurrencyRate? entity, string? contextProfileName)
        {
            return CreateHistoricalCurrencyRateModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IHistoricalCurrencyRateModel? CreateHistoricalCurrencyRateModelFromEntityLite(this IHistoricalCurrencyRate? entity, string? contextProfileName)
        {
            return CreateHistoricalCurrencyRateModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IHistoricalCurrencyRateModel? CreateHistoricalCurrencyRateModelFromEntityList(this IHistoricalCurrencyRate? entity, string? contextProfileName)
        {
            return CreateHistoricalCurrencyRateModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IHistoricalCurrencyRateModel? CreateHistoricalCurrencyRateModelFromEntity(
            this IHistoricalCurrencyRate? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IHistoricalCurrencyRateModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // HistoricalCurrencyRate's Properties
                    // HistoricalCurrencyRate's Related Objects
                    model.EndingCurrency = ModelMapperForCurrency.CreateCurrencyModelFromEntityLite(entity.EndingCurrency, contextProfileName);
                    model.StartingCurrency = ModelMapperForCurrency.CreateCurrencyModelFromEntityLite(entity.StartingCurrency, contextProfileName);
                    // HistoricalCurrencyRate's Associated Objects
                    // Additional Mappings
                    if (CreateHistoricalCurrencyRateModelFromEntityHooksFull != null) { model = CreateHistoricalCurrencyRateModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // HistoricalCurrencyRate's Properties
                    // HistoricalCurrencyRate's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // HistoricalCurrencyRate's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateHistoricalCurrencyRateModelFromEntityHooksLite != null) { model = CreateHistoricalCurrencyRateModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // HistoricalCurrencyRate's Properties
                    model.OnDate = entity.OnDate;
                    model.Rate = entity.Rate;
                    // HistoricalCurrencyRate's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.EndingCurrencyID = entity.EndingCurrencyID;
                    model.EndingCurrencyKey = entity.EndingCurrency?.CustomKey;
                    model.EndingCurrencyName = entity.EndingCurrency?.Name;
                    model.StartingCurrencyID = entity.StartingCurrencyID;
                    model.StartingCurrencyKey = entity.StartingCurrency?.CustomKey;
                    model.StartingCurrencyName = entity.StartingCurrency?.Name;
                    // HistoricalCurrencyRate's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateHistoricalCurrencyRateModelFromEntityHooksList != null) { model = CreateHistoricalCurrencyRateModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
