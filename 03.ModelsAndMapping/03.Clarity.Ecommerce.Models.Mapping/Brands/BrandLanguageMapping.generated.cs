// <autogenerated>
// <copyright file="Mapping.Brands.BrandLanguage.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Brands section of the Mapping class</summary>
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

    public static partial class ModelMapperForBrandLanguage
    {
        public sealed class AnonBrandLanguage : BrandLanguage
        {
            // public new Brand? Master { get; set; }
        }

        public static readonly Func<BrandLanguage?, string?, IBrandLanguageModel?> MapBrandLanguageModelFromEntityFull = CreateBrandLanguageModelFromEntityFull;

        public static readonly Func<BrandLanguage?, string?, IBrandLanguageModel?> MapBrandLanguageModelFromEntityLite = CreateBrandLanguageModelFromEntityLite;

        public static readonly Func<BrandLanguage?, string?, IBrandLanguageModel?> MapBrandLanguageModelFromEntityList = CreateBrandLanguageModelFromEntityList;

        public static Func<IBrandLanguage, IBrandLanguageModel, string?, IBrandLanguageModel>? CreateBrandLanguageModelFromEntityHooksFull { get; set; }

        public static Func<IBrandLanguage, IBrandLanguageModel, string?, IBrandLanguageModel>? CreateBrandLanguageModelFromEntityHooksLite { get; set; }

        public static Func<IBrandLanguage, IBrandLanguageModel, string?, IBrandLanguageModel>? CreateBrandLanguageModelFromEntityHooksList { get; set; }

        public static Expression<Func<BrandLanguage, AnonBrandLanguage>>? PreBuiltBrandLanguageSQLSelectorFull { get; set; }

        public static Expression<Func<BrandLanguage, AnonBrandLanguage>>? PreBuiltBrandLanguageSQLSelectorLite { get; set; }

        public static Expression<Func<BrandLanguage, AnonBrandLanguage>>? PreBuiltBrandLanguageSQLSelectorList { get; set; }

        /// <summary>An <see cref="IBrandLanguageModel"/> extension method that creates a(n) <see cref="BrandLanguage"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="BrandLanguage"/> entity.</returns>
        public static IBrandLanguage CreateBrandLanguageEntity(
            this IBrandLanguageModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IBrandLanguageModel, BrandLanguage>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateBrandLanguageFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IBrandLanguageModel"/> extension method that updates a(n) <see cref="BrandLanguage"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="BrandLanguage"/> entity.</returns>
        public static IBrandLanguage UpdateBrandLanguageFromModel(
            this IBrandLanguage entity,
            IBrandLanguageModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // BrandLanguage Properties
            entity.OverrideISO639_1_2002 = model.OverrideISO639_1_2002;
            entity.OverrideISO639_2_1998 = model.OverrideISO639_2_1998;
            entity.OverrideISO639_3_2007 = model.OverrideISO639_3_2007;
            entity.OverrideISO639_5_2008 = model.OverrideISO639_5_2008;
            entity.OverrideLocale = model.OverrideLocale;
            entity.OverrideUnicodeName = model.OverrideUnicodeName;
            // BrandLanguage's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenBrandLanguageSQLSelectorFull()
        {
            PreBuiltBrandLanguageSQLSelectorFull = x => x == null ? null! : new AnonBrandLanguage
            {
                MasterID = x.MasterID,
                Master = ModelMapperForBrand.PreBuiltBrandSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForLanguage.PreBuiltLanguageSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                OverrideLocale = x.OverrideLocale,
                OverrideUnicodeName = x.OverrideUnicodeName,
                OverrideISO639_1_2002 = x.OverrideISO639_1_2002,
                OverrideISO639_2_1998 = x.OverrideISO639_2_1998,
                OverrideISO639_3_2007 = x.OverrideISO639_3_2007,
                OverrideISO639_5_2008 = x.OverrideISO639_5_2008,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenBrandLanguageSQLSelectorLite()
        {
            PreBuiltBrandLanguageSQLSelectorLite = x => x == null ? null! : new AnonBrandLanguage
            {
                MasterID = x.MasterID,
                Master = ModelMapperForBrand.PreBuiltBrandSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForLanguage.PreBuiltLanguageSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                OverrideLocale = x.OverrideLocale,
                OverrideUnicodeName = x.OverrideUnicodeName,
                OverrideISO639_1_2002 = x.OverrideISO639_1_2002,
                OverrideISO639_2_1998 = x.OverrideISO639_2_1998,
                OverrideISO639_3_2007 = x.OverrideISO639_3_2007,
                OverrideISO639_5_2008 = x.OverrideISO639_5_2008,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenBrandLanguageSQLSelectorList()
        {
            PreBuiltBrandLanguageSQLSelectorList = x => x == null ? null! : new AnonBrandLanguage
            {
                MasterID = x.MasterID,
                Master = ModelMapperForBrand.PreBuiltBrandSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties (List)
                SlaveID = x.SlaveID,
                Slave = ModelMapperForLanguage.PreBuiltLanguageSQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                OverrideLocale = x.OverrideLocale,
                OverrideUnicodeName = x.OverrideUnicodeName,
                OverrideISO639_1_2002 = x.OverrideISO639_1_2002,
                OverrideISO639_2_1998 = x.OverrideISO639_2_1998,
                OverrideISO639_3_2007 = x.OverrideISO639_3_2007,
                OverrideISO639_5_2008 = x.OverrideISO639_5_2008,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IBrandLanguageModel> SelectFullBrandLanguageAndMapToBrandLanguageModel(
            this IQueryable<BrandLanguage> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandLanguageSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltBrandLanguageSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateBrandLanguageModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IBrandLanguageModel> SelectLiteBrandLanguageAndMapToBrandLanguageModel(
            this IQueryable<BrandLanguage> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandLanguageSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltBrandLanguageSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateBrandLanguageModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IBrandLanguageModel> SelectListBrandLanguageAndMapToBrandLanguageModel(
            this IQueryable<BrandLanguage> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandLanguageSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltBrandLanguageSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateBrandLanguageModelFromEntityList(x, contextProfileName))!;
        }

        public static IBrandLanguageModel? SelectFirstFullBrandLanguageAndMapToBrandLanguageModel(
            this IQueryable<BrandLanguage> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandLanguageSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltBrandLanguageSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateBrandLanguageModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IBrandLanguageModel? SelectFirstListBrandLanguageAndMapToBrandLanguageModel(
            this IQueryable<BrandLanguage> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandLanguageSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltBrandLanguageSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateBrandLanguageModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IBrandLanguageModel? SelectSingleFullBrandLanguageAndMapToBrandLanguageModel(
            this IQueryable<BrandLanguage> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandLanguageSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltBrandLanguageSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateBrandLanguageModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IBrandLanguageModel? SelectSingleLiteBrandLanguageAndMapToBrandLanguageModel(
            this IQueryable<BrandLanguage> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandLanguageSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltBrandLanguageSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateBrandLanguageModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IBrandLanguageModel? SelectSingleListBrandLanguageAndMapToBrandLanguageModel(
            this IQueryable<BrandLanguage> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandLanguageSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltBrandLanguageSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateBrandLanguageModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IBrandLanguageModel> results, int totalPages, int totalCount) SelectFullBrandLanguageAndMapToBrandLanguageModel(
            this IQueryable<BrandLanguage> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandLanguageSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltBrandLanguageSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateBrandLanguageModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IBrandLanguageModel> results, int totalPages, int totalCount) SelectLiteBrandLanguageAndMapToBrandLanguageModel(
            this IQueryable<BrandLanguage> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandLanguageSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltBrandLanguageSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateBrandLanguageModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IBrandLanguageModel> results, int totalPages, int totalCount) SelectListBrandLanguageAndMapToBrandLanguageModel(
            this IQueryable<BrandLanguage> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandLanguageSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltBrandLanguageSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateBrandLanguageModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IBrandLanguageModel? CreateBrandLanguageModelFromEntityFull(this IBrandLanguage? entity, string? contextProfileName)
        {
            return CreateBrandLanguageModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IBrandLanguageModel? CreateBrandLanguageModelFromEntityLite(this IBrandLanguage? entity, string? contextProfileName)
        {
            return CreateBrandLanguageModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IBrandLanguageModel? CreateBrandLanguageModelFromEntityList(this IBrandLanguage? entity, string? contextProfileName)
        {
            return CreateBrandLanguageModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IBrandLanguageModel? CreateBrandLanguageModelFromEntity(
            this IBrandLanguage? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IBrandLanguageModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // BrandLanguage's Properties
                    // BrandLanguage's Related Objects
                    // BrandLanguage's Associated Objects
                    // Additional Mappings
                    if (CreateBrandLanguageModelFromEntityHooksFull != null) { model = CreateBrandLanguageModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // BrandLanguage's Properties
                    // BrandLanguage's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.Slave = ModelMapperForLanguage.CreateLanguageModelFromEntityLite(entity.Slave, contextProfileName);
                    // BrandLanguage's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateBrandLanguageModelFromEntityHooksLite != null) { model = CreateBrandLanguageModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // BrandLanguage's Properties
                    model.OverrideISO639_1_2002 = entity.OverrideISO639_1_2002;
                    model.OverrideISO639_2_1998 = entity.OverrideISO639_2_1998;
                    model.OverrideISO639_3_2007 = entity.OverrideISO639_3_2007;
                    model.OverrideISO639_5_2008 = entity.OverrideISO639_5_2008;
                    model.OverrideLocale = entity.OverrideLocale;
                    model.OverrideUnicodeName = entity.OverrideUnicodeName;
                    // BrandLanguage's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.MasterName = entity.Master?.Name;
                    model.SlaveID = entity.SlaveID;
                    model.SlaveKey = entity.Slave?.CustomKey;
                    // BrandLanguage's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateBrandLanguageModelFromEntityHooksList != null) { model = CreateBrandLanguageModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
