// <autogenerated>
// <copyright file="Mapping.Pricing.PriceRuleVendor.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Pricing section of the Mapping class</summary>
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

    public static partial class ModelMapperForPriceRuleVendor
    {
        public sealed class AnonPriceRuleVendor : PriceRuleVendor
        {
            // public new PriceRule? Master { get; set; }
        }

        public static readonly Func<PriceRuleVendor?, string?, IPriceRuleVendorModel?> MapPriceRuleVendorModelFromEntityFull = CreatePriceRuleVendorModelFromEntityFull;

        public static readonly Func<PriceRuleVendor?, string?, IPriceRuleVendorModel?> MapPriceRuleVendorModelFromEntityLite = CreatePriceRuleVendorModelFromEntityLite;

        public static readonly Func<PriceRuleVendor?, string?, IPriceRuleVendorModel?> MapPriceRuleVendorModelFromEntityList = CreatePriceRuleVendorModelFromEntityList;

        public static Func<IPriceRuleVendor, IPriceRuleVendorModel, string?, IPriceRuleVendorModel>? CreatePriceRuleVendorModelFromEntityHooksFull { get; set; }

        public static Func<IPriceRuleVendor, IPriceRuleVendorModel, string?, IPriceRuleVendorModel>? CreatePriceRuleVendorModelFromEntityHooksLite { get; set; }

        public static Func<IPriceRuleVendor, IPriceRuleVendorModel, string?, IPriceRuleVendorModel>? CreatePriceRuleVendorModelFromEntityHooksList { get; set; }

        public static Expression<Func<PriceRuleVendor, AnonPriceRuleVendor>>? PreBuiltPriceRuleVendorSQLSelectorFull { get; set; }

        public static Expression<Func<PriceRuleVendor, AnonPriceRuleVendor>>? PreBuiltPriceRuleVendorSQLSelectorLite { get; set; }

        public static Expression<Func<PriceRuleVendor, AnonPriceRuleVendor>>? PreBuiltPriceRuleVendorSQLSelectorList { get; set; }

        /// <summary>An <see cref="IPriceRuleVendorModel"/> extension method that creates a(n) <see cref="PriceRuleVendor"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="PriceRuleVendor"/> entity.</returns>
        public static IPriceRuleVendor CreatePriceRuleVendorEntity(
            this IPriceRuleVendorModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IPriceRuleVendorModel, PriceRuleVendor>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdatePriceRuleVendorFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IPriceRuleVendorModel"/> extension method that updates a(n) <see cref="PriceRuleVendor"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="PriceRuleVendor"/> entity.</returns>
        public static IPriceRuleVendor UpdatePriceRuleVendorFromModel(
            this IPriceRuleVendor entity,
            IPriceRuleVendorModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // PriceRuleVendor's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenPriceRuleVendorSQLSelectorFull()
        {
            PreBuiltPriceRuleVendorSQLSelectorFull = x => x == null ? null! : new AnonPriceRuleVendor
            {
                MasterID = x.MasterID,
                Master = ModelMapperForPriceRule.PreBuiltPriceRuleSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForVendor.PreBuiltVendorSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenPriceRuleVendorSQLSelectorLite()
        {
            PreBuiltPriceRuleVendorSQLSelectorLite = x => x == null ? null! : new AnonPriceRuleVendor
            {
                MasterID = x.MasterID,
                Master = ModelMapperForPriceRule.PreBuiltPriceRuleSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForVendor.PreBuiltVendorSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenPriceRuleVendorSQLSelectorList()
        {
            PreBuiltPriceRuleVendorSQLSelectorList = x => x == null ? null! : new AnonPriceRuleVendor
            {
                MasterID = x.MasterID,
                Master = ModelMapperForPriceRule.PreBuiltPriceRuleSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties (List)
                SlaveID = x.SlaveID,
                Slave = ModelMapperForVendor.PreBuiltVendorSQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IPriceRuleVendorModel> SelectFullPriceRuleVendorAndMapToPriceRuleVendorModel(
            this IQueryable<PriceRuleVendor> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleVendorSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltPriceRuleVendorSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleVendorModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IPriceRuleVendorModel> SelectLitePriceRuleVendorAndMapToPriceRuleVendorModel(
            this IQueryable<PriceRuleVendor> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleVendorSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltPriceRuleVendorSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleVendorModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IPriceRuleVendorModel> SelectListPriceRuleVendorAndMapToPriceRuleVendorModel(
            this IQueryable<PriceRuleVendor> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleVendorSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltPriceRuleVendorSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleVendorModelFromEntityList(x, contextProfileName))!;
        }

        public static IPriceRuleVendorModel? SelectFirstFullPriceRuleVendorAndMapToPriceRuleVendorModel(
            this IQueryable<PriceRuleVendor> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleVendorSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPriceRuleVendorSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleVendorModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IPriceRuleVendorModel? SelectFirstListPriceRuleVendorAndMapToPriceRuleVendorModel(
            this IQueryable<PriceRuleVendor> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleVendorSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPriceRuleVendorSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleVendorModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IPriceRuleVendorModel? SelectSingleFullPriceRuleVendorAndMapToPriceRuleVendorModel(
            this IQueryable<PriceRuleVendor> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleVendorSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPriceRuleVendorSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleVendorModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IPriceRuleVendorModel? SelectSingleLitePriceRuleVendorAndMapToPriceRuleVendorModel(
            this IQueryable<PriceRuleVendor> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleVendorSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPriceRuleVendorSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleVendorModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IPriceRuleVendorModel? SelectSingleListPriceRuleVendorAndMapToPriceRuleVendorModel(
            this IQueryable<PriceRuleVendor> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleVendorSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPriceRuleVendorSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleVendorModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IPriceRuleVendorModel> results, int totalPages, int totalCount) SelectFullPriceRuleVendorAndMapToPriceRuleVendorModel(
            this IQueryable<PriceRuleVendor> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleVendorSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltPriceRuleVendorSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreatePriceRuleVendorModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IPriceRuleVendorModel> results, int totalPages, int totalCount) SelectLitePriceRuleVendorAndMapToPriceRuleVendorModel(
            this IQueryable<PriceRuleVendor> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleVendorSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltPriceRuleVendorSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreatePriceRuleVendorModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IPriceRuleVendorModel> results, int totalPages, int totalCount) SelectListPriceRuleVendorAndMapToPriceRuleVendorModel(
            this IQueryable<PriceRuleVendor> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleVendorSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltPriceRuleVendorSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreatePriceRuleVendorModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IPriceRuleVendorModel? CreatePriceRuleVendorModelFromEntityFull(this IPriceRuleVendor? entity, string? contextProfileName)
        {
            return CreatePriceRuleVendorModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IPriceRuleVendorModel? CreatePriceRuleVendorModelFromEntityLite(this IPriceRuleVendor? entity, string? contextProfileName)
        {
            return CreatePriceRuleVendorModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IPriceRuleVendorModel? CreatePriceRuleVendorModelFromEntityList(this IPriceRuleVendor? entity, string? contextProfileName)
        {
            return CreatePriceRuleVendorModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IPriceRuleVendorModel? CreatePriceRuleVendorModelFromEntity(
            this IPriceRuleVendor? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IPriceRuleVendorModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // PriceRuleVendor's Properties
                    // PriceRuleVendor's Related Objects
                    // PriceRuleVendor's Associated Objects
                    // Additional Mappings
                    if (CreatePriceRuleVendorModelFromEntityHooksFull != null) { model = CreatePriceRuleVendorModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // PriceRuleVendor's Properties
                    // PriceRuleVendor's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.Slave = ModelMapperForVendor.CreateVendorModelFromEntityLite(entity.Slave, contextProfileName);
                    // PriceRuleVendor's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreatePriceRuleVendorModelFromEntityHooksLite != null) { model = CreatePriceRuleVendorModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // PriceRuleVendor's Properties
                    // PriceRuleVendor's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.MasterName = entity.Master?.Name;
                    model.SlaveID = entity.SlaveID;
                    model.SlaveKey = entity.Slave?.CustomKey;
                    model.SlaveName = entity.Slave?.Name;
                    // PriceRuleVendor's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreatePriceRuleVendorModelFromEntityHooksList != null) { model = CreatePriceRuleVendorModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
