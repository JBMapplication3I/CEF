// <autogenerated>
// <copyright file="Mapping.Geography.RegionCurrency.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Geography section of the Mapping class</summary>
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

    public static partial class ModelMapperForRegionCurrency
    {
        public sealed class AnonRegionCurrency : RegionCurrency
        {
            // public new Region? Master { get; set; }
        }

        public static readonly Func<RegionCurrency?, string?, IRegionCurrencyModel?> MapRegionCurrencyModelFromEntityFull = CreateRegionCurrencyModelFromEntityFull;

        public static readonly Func<RegionCurrency?, string?, IRegionCurrencyModel?> MapRegionCurrencyModelFromEntityLite = CreateRegionCurrencyModelFromEntityLite;

        public static readonly Func<RegionCurrency?, string?, IRegionCurrencyModel?> MapRegionCurrencyModelFromEntityList = CreateRegionCurrencyModelFromEntityList;

        public static Func<IRegionCurrency, IRegionCurrencyModel, string?, IRegionCurrencyModel>? CreateRegionCurrencyModelFromEntityHooksFull { get; set; }

        public static Func<IRegionCurrency, IRegionCurrencyModel, string?, IRegionCurrencyModel>? CreateRegionCurrencyModelFromEntityHooksLite { get; set; }

        public static Func<IRegionCurrency, IRegionCurrencyModel, string?, IRegionCurrencyModel>? CreateRegionCurrencyModelFromEntityHooksList { get; set; }

        public static Expression<Func<RegionCurrency, AnonRegionCurrency>>? PreBuiltRegionCurrencySQLSelectorFull { get; set; }

        public static Expression<Func<RegionCurrency, AnonRegionCurrency>>? PreBuiltRegionCurrencySQLSelectorLite { get; set; }

        public static Expression<Func<RegionCurrency, AnonRegionCurrency>>? PreBuiltRegionCurrencySQLSelectorList { get; set; }

        /// <summary>An <see cref="IRegionCurrencyModel"/> extension method that creates a(n) <see cref="RegionCurrency"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="RegionCurrency"/> entity.</returns>
        public static IRegionCurrency CreateRegionCurrencyEntity(
            this IRegionCurrencyModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IRegionCurrencyModel, RegionCurrency>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateRegionCurrencyFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IRegionCurrencyModel"/> extension method that updates a(n) <see cref="RegionCurrency"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="RegionCurrency"/> entity.</returns>
        public static IRegionCurrency UpdateRegionCurrencyFromModel(
            this IRegionCurrency entity,
            IRegionCurrencyModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // RegionCurrency's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenRegionCurrencySQLSelectorFull()
        {
            PreBuiltRegionCurrencySQLSelectorFull = x => x == null ? null! : new AnonRegionCurrency
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenRegionCurrencySQLSelectorLite()
        {
            PreBuiltRegionCurrencySQLSelectorLite = x => x == null ? null! : new AnonRegionCurrency
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenRegionCurrencySQLSelectorList()
        {
            PreBuiltRegionCurrencySQLSelectorList = x => x == null ? null! : new AnonRegionCurrency
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForCurrency.PreBuiltCurrencySQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
                Master = ModelMapperForRegion.PreBuiltRegionSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties
            };
        }

        public static IEnumerable<IRegionCurrencyModel> SelectFullRegionCurrencyAndMapToRegionCurrencyModel(
            this IQueryable<RegionCurrency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltRegionCurrencySQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltRegionCurrencySQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateRegionCurrencyModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IRegionCurrencyModel> SelectLiteRegionCurrencyAndMapToRegionCurrencyModel(
            this IQueryable<RegionCurrency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltRegionCurrencySQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltRegionCurrencySQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateRegionCurrencyModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IRegionCurrencyModel> SelectListRegionCurrencyAndMapToRegionCurrencyModel(
            this IQueryable<RegionCurrency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltRegionCurrencySQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltRegionCurrencySQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateRegionCurrencyModelFromEntityList(x, contextProfileName))!;
        }

        public static IRegionCurrencyModel? SelectFirstFullRegionCurrencyAndMapToRegionCurrencyModel(
            this IQueryable<RegionCurrency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltRegionCurrencySQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltRegionCurrencySQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateRegionCurrencyModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IRegionCurrencyModel? SelectFirstListRegionCurrencyAndMapToRegionCurrencyModel(
            this IQueryable<RegionCurrency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltRegionCurrencySQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltRegionCurrencySQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateRegionCurrencyModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IRegionCurrencyModel? SelectSingleFullRegionCurrencyAndMapToRegionCurrencyModel(
            this IQueryable<RegionCurrency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltRegionCurrencySQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltRegionCurrencySQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateRegionCurrencyModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IRegionCurrencyModel? SelectSingleLiteRegionCurrencyAndMapToRegionCurrencyModel(
            this IQueryable<RegionCurrency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltRegionCurrencySQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltRegionCurrencySQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateRegionCurrencyModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IRegionCurrencyModel? SelectSingleListRegionCurrencyAndMapToRegionCurrencyModel(
            this IQueryable<RegionCurrency> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltRegionCurrencySQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltRegionCurrencySQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateRegionCurrencyModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IRegionCurrencyModel> results, int totalPages, int totalCount) SelectFullRegionCurrencyAndMapToRegionCurrencyModel(
            this IQueryable<RegionCurrency> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltRegionCurrencySQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltRegionCurrencySQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateRegionCurrencyModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IRegionCurrencyModel> results, int totalPages, int totalCount) SelectLiteRegionCurrencyAndMapToRegionCurrencyModel(
            this IQueryable<RegionCurrency> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltRegionCurrencySQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltRegionCurrencySQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateRegionCurrencyModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IRegionCurrencyModel> results, int totalPages, int totalCount) SelectListRegionCurrencyAndMapToRegionCurrencyModel(
            this IQueryable<RegionCurrency> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltRegionCurrencySQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltRegionCurrencySQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateRegionCurrencyModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IRegionCurrencyModel? CreateRegionCurrencyModelFromEntityFull(this IRegionCurrency? entity, string? contextProfileName)
        {
            return CreateRegionCurrencyModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IRegionCurrencyModel? CreateRegionCurrencyModelFromEntityLite(this IRegionCurrency? entity, string? contextProfileName)
        {
            return CreateRegionCurrencyModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IRegionCurrencyModel? CreateRegionCurrencyModelFromEntityList(this IRegionCurrency? entity, string? contextProfileName)
        {
            return CreateRegionCurrencyModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IRegionCurrencyModel? CreateRegionCurrencyModelFromEntity(
            this IRegionCurrency? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IRegionCurrencyModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // RegionCurrency's Properties
                    // RegionCurrency's Related Objects
                    // RegionCurrency's Associated Objects
                    // Additional Mappings
                    if (CreateRegionCurrencyModelFromEntityHooksFull != null) { model = CreateRegionCurrencyModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // RegionCurrency's Properties
                    // RegionCurrency's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.Slave = ModelMapperForCurrency.CreateCurrencyModelFromEntityLite(entity.Slave, contextProfileName);
                    // RegionCurrency's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateRegionCurrencyModelFromEntityHooksLite != null) { model = CreateRegionCurrencyModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // RegionCurrency's Properties
                    // RegionCurrency's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.MasterName = entity.Master?.Name;
                    model.SlaveID = entity.SlaveID;
                    model.SlaveKey = entity.Slave?.CustomKey;
                    model.SlaveName = entity.Slave?.Name;
                    // RegionCurrency's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateRegionCurrencyModelFromEntityHooksList != null) { model = CreateRegionCurrencyModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
