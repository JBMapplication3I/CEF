// <autogenerated>
// <copyright file="Mapping.Pricing.PriceRuleFranchise.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForPriceRuleFranchise
    {
        public sealed class AnonPriceRuleFranchise : PriceRuleFranchise
        {
            // public new PriceRule? Master { get; set; }
        }

        public static readonly Func<PriceRuleFranchise?, string?, IPriceRuleFranchiseModel?> MapPriceRuleFranchiseModelFromEntityFull = CreatePriceRuleFranchiseModelFromEntityFull;

        public static readonly Func<PriceRuleFranchise?, string?, IPriceRuleFranchiseModel?> MapPriceRuleFranchiseModelFromEntityLite = CreatePriceRuleFranchiseModelFromEntityLite;

        public static readonly Func<PriceRuleFranchise?, string?, IPriceRuleFranchiseModel?> MapPriceRuleFranchiseModelFromEntityList = CreatePriceRuleFranchiseModelFromEntityList;

        public static Func<IPriceRuleFranchise, IPriceRuleFranchiseModel, string?, IPriceRuleFranchiseModel>? CreatePriceRuleFranchiseModelFromEntityHooksFull { get; set; }

        public static Func<IPriceRuleFranchise, IPriceRuleFranchiseModel, string?, IPriceRuleFranchiseModel>? CreatePriceRuleFranchiseModelFromEntityHooksLite { get; set; }

        public static Func<IPriceRuleFranchise, IPriceRuleFranchiseModel, string?, IPriceRuleFranchiseModel>? CreatePriceRuleFranchiseModelFromEntityHooksList { get; set; }

        public static Expression<Func<PriceRuleFranchise, AnonPriceRuleFranchise>>? PreBuiltPriceRuleFranchiseSQLSelectorFull { get; set; }

        public static Expression<Func<PriceRuleFranchise, AnonPriceRuleFranchise>>? PreBuiltPriceRuleFranchiseSQLSelectorLite { get; set; }

        public static Expression<Func<PriceRuleFranchise, AnonPriceRuleFranchise>>? PreBuiltPriceRuleFranchiseSQLSelectorList { get; set; }

        /// <summary>An <see cref="IPriceRuleFranchiseModel"/> extension method that creates a(n) <see cref="PriceRuleFranchise"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="PriceRuleFranchise"/> entity.</returns>
        public static IPriceRuleFranchise CreatePriceRuleFranchiseEntity(
            this IPriceRuleFranchiseModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IPriceRuleFranchiseModel, PriceRuleFranchise>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdatePriceRuleFranchiseFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IPriceRuleFranchiseModel"/> extension method that updates a(n) <see cref="PriceRuleFranchise"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="PriceRuleFranchise"/> entity.</returns>
        public static IPriceRuleFranchise UpdatePriceRuleFranchiseFromModel(
            this IPriceRuleFranchise entity,
            IPriceRuleFranchiseModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // PriceRuleFranchise's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenPriceRuleFranchiseSQLSelectorFull()
        {
            PreBuiltPriceRuleFranchiseSQLSelectorFull = x => x == null ? null! : new AnonPriceRuleFranchise
            {
                MasterID = x.MasterID,
                Master = ModelMapperForPriceRule.PreBuiltPriceRuleSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForFranchise.PreBuiltFranchiseSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenPriceRuleFranchiseSQLSelectorLite()
        {
            PreBuiltPriceRuleFranchiseSQLSelectorLite = x => x == null ? null! : new AnonPriceRuleFranchise
            {
                MasterID = x.MasterID,
                Master = ModelMapperForPriceRule.PreBuiltPriceRuleSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForFranchise.PreBuiltFranchiseSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenPriceRuleFranchiseSQLSelectorList()
        {
            PreBuiltPriceRuleFranchiseSQLSelectorList = x => x == null ? null! : new AnonPriceRuleFranchise
            {
                MasterID = x.MasterID,
                Master = ModelMapperForPriceRule.PreBuiltPriceRuleSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties (List)
                SlaveID = x.SlaveID,
                Slave = ModelMapperForFranchise.PreBuiltFranchiseSQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IPriceRuleFranchiseModel> SelectFullPriceRuleFranchiseAndMapToPriceRuleFranchiseModel(
            this IQueryable<PriceRuleFranchise> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleFranchiseSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltPriceRuleFranchiseSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleFranchiseModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IPriceRuleFranchiseModel> SelectLitePriceRuleFranchiseAndMapToPriceRuleFranchiseModel(
            this IQueryable<PriceRuleFranchise> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleFranchiseSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltPriceRuleFranchiseSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleFranchiseModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IPriceRuleFranchiseModel> SelectListPriceRuleFranchiseAndMapToPriceRuleFranchiseModel(
            this IQueryable<PriceRuleFranchise> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleFranchiseSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltPriceRuleFranchiseSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleFranchiseModelFromEntityList(x, contextProfileName))!;
        }

        public static IPriceRuleFranchiseModel? SelectFirstFullPriceRuleFranchiseAndMapToPriceRuleFranchiseModel(
            this IQueryable<PriceRuleFranchise> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleFranchiseSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPriceRuleFranchiseSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleFranchiseModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IPriceRuleFranchiseModel? SelectFirstListPriceRuleFranchiseAndMapToPriceRuleFranchiseModel(
            this IQueryable<PriceRuleFranchise> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleFranchiseSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPriceRuleFranchiseSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleFranchiseModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IPriceRuleFranchiseModel? SelectSingleFullPriceRuleFranchiseAndMapToPriceRuleFranchiseModel(
            this IQueryable<PriceRuleFranchise> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleFranchiseSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPriceRuleFranchiseSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleFranchiseModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IPriceRuleFranchiseModel? SelectSingleLitePriceRuleFranchiseAndMapToPriceRuleFranchiseModel(
            this IQueryable<PriceRuleFranchise> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleFranchiseSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPriceRuleFranchiseSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleFranchiseModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IPriceRuleFranchiseModel? SelectSingleListPriceRuleFranchiseAndMapToPriceRuleFranchiseModel(
            this IQueryable<PriceRuleFranchise> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleFranchiseSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltPriceRuleFranchiseSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreatePriceRuleFranchiseModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IPriceRuleFranchiseModel> results, int totalPages, int totalCount) SelectFullPriceRuleFranchiseAndMapToPriceRuleFranchiseModel(
            this IQueryable<PriceRuleFranchise> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleFranchiseSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltPriceRuleFranchiseSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreatePriceRuleFranchiseModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IPriceRuleFranchiseModel> results, int totalPages, int totalCount) SelectLitePriceRuleFranchiseAndMapToPriceRuleFranchiseModel(
            this IQueryable<PriceRuleFranchise> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleFranchiseSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltPriceRuleFranchiseSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreatePriceRuleFranchiseModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IPriceRuleFranchiseModel> results, int totalPages, int totalCount) SelectListPriceRuleFranchiseAndMapToPriceRuleFranchiseModel(
            this IQueryable<PriceRuleFranchise> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltPriceRuleFranchiseSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltPriceRuleFranchiseSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreatePriceRuleFranchiseModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IPriceRuleFranchiseModel? CreatePriceRuleFranchiseModelFromEntityFull(this IPriceRuleFranchise? entity, string? contextProfileName)
        {
            return CreatePriceRuleFranchiseModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IPriceRuleFranchiseModel? CreatePriceRuleFranchiseModelFromEntityLite(this IPriceRuleFranchise? entity, string? contextProfileName)
        {
            return CreatePriceRuleFranchiseModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IPriceRuleFranchiseModel? CreatePriceRuleFranchiseModelFromEntityList(this IPriceRuleFranchise? entity, string? contextProfileName)
        {
            return CreatePriceRuleFranchiseModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IPriceRuleFranchiseModel? CreatePriceRuleFranchiseModelFromEntity(
            this IPriceRuleFranchise? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IPriceRuleFranchiseModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // PriceRuleFranchise's Properties
                    // PriceRuleFranchise's Related Objects
                    // PriceRuleFranchise's Associated Objects
                    // Additional Mappings
                    if (CreatePriceRuleFranchiseModelFromEntityHooksFull != null) { model = CreatePriceRuleFranchiseModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // PriceRuleFranchise's Properties
                    // PriceRuleFranchise's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.Slave = ModelMapperForFranchise.CreateFranchiseModelFromEntityLite(entity.Slave, contextProfileName);
                    // PriceRuleFranchise's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreatePriceRuleFranchiseModelFromEntityHooksLite != null) { model = CreatePriceRuleFranchiseModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // PriceRuleFranchise's Properties
                    // PriceRuleFranchise's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.MasterName = entity.Master?.Name;
                    model.SlaveID = entity.SlaveID;
                    model.SlaveKey = entity.Slave?.CustomKey;
                    model.SlaveName = entity.Slave?.Name;
                    // PriceRuleFranchise's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreatePriceRuleFranchiseModelFromEntityHooksList != null) { model = CreatePriceRuleFranchiseModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
