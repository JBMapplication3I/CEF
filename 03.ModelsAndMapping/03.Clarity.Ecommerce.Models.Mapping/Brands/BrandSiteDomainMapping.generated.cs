// <autogenerated>
// <copyright file="Mapping.Brands.BrandSiteDomain.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForBrandSiteDomain
    {
        public sealed class AnonBrandSiteDomain : BrandSiteDomain
        {
            // public new Brand? Master { get; set; }
        }

        public static readonly Func<BrandSiteDomain?, string?, IBrandSiteDomainModel?> MapBrandSiteDomainModelFromEntityFull = CreateBrandSiteDomainModelFromEntityFull;

        public static readonly Func<BrandSiteDomain?, string?, IBrandSiteDomainModel?> MapBrandSiteDomainModelFromEntityLite = CreateBrandSiteDomainModelFromEntityLite;

        public static readonly Func<BrandSiteDomain?, string?, IBrandSiteDomainModel?> MapBrandSiteDomainModelFromEntityList = CreateBrandSiteDomainModelFromEntityList;

        public static Func<IBrandSiteDomain, IBrandSiteDomainModel, string?, IBrandSiteDomainModel>? CreateBrandSiteDomainModelFromEntityHooksFull { get; set; }

        public static Func<IBrandSiteDomain, IBrandSiteDomainModel, string?, IBrandSiteDomainModel>? CreateBrandSiteDomainModelFromEntityHooksLite { get; set; }

        public static Func<IBrandSiteDomain, IBrandSiteDomainModel, string?, IBrandSiteDomainModel>? CreateBrandSiteDomainModelFromEntityHooksList { get; set; }

        public static Expression<Func<BrandSiteDomain, AnonBrandSiteDomain>>? PreBuiltBrandSiteDomainSQLSelectorFull { get; set; }

        public static Expression<Func<BrandSiteDomain, AnonBrandSiteDomain>>? PreBuiltBrandSiteDomainSQLSelectorLite { get; set; }

        public static Expression<Func<BrandSiteDomain, AnonBrandSiteDomain>>? PreBuiltBrandSiteDomainSQLSelectorList { get; set; }

        /// <summary>An <see cref="IBrandSiteDomainModel"/> extension method that creates a(n) <see cref="BrandSiteDomain"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="BrandSiteDomain"/> entity.</returns>
        public static IBrandSiteDomain CreateBrandSiteDomainEntity(
            this IBrandSiteDomainModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IBrandSiteDomainModel, BrandSiteDomain>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateBrandSiteDomainFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IBrandSiteDomainModel"/> extension method that updates a(n) <see cref="BrandSiteDomain"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="BrandSiteDomain"/> entity.</returns>
        public static IBrandSiteDomain UpdateBrandSiteDomainFromModel(
            this IBrandSiteDomain entity,
            IBrandSiteDomainModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // BrandSiteDomain's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenBrandSiteDomainSQLSelectorFull()
        {
            PreBuiltBrandSiteDomainSQLSelectorFull = x => x == null ? null! : new AnonBrandSiteDomain
            {
                MasterID = x.MasterID,
                Master = ModelMapperForBrand.PreBuiltBrandSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForSiteDomain.PreBuiltSiteDomainSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenBrandSiteDomainSQLSelectorLite()
        {
            PreBuiltBrandSiteDomainSQLSelectorLite = x => x == null ? null! : new AnonBrandSiteDomain
            {
                MasterID = x.MasterID,
                Master = ModelMapperForBrand.PreBuiltBrandSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                Slave = ModelMapperForSiteDomain.PreBuiltSiteDomainSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenBrandSiteDomainSQLSelectorList()
        {
            PreBuiltBrandSiteDomainSQLSelectorList = x => x == null ? null! : new AnonBrandSiteDomain
            {
                MasterID = x.MasterID,
                Master = ModelMapperForBrand.PreBuiltBrandSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties (List)
                SlaveID = x.SlaveID,
                Slave = ModelMapperForSiteDomain.PreBuiltSiteDomainSQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IBrandSiteDomainModel> SelectFullBrandSiteDomainAndMapToBrandSiteDomainModel(
            this IQueryable<BrandSiteDomain> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandSiteDomainSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltBrandSiteDomainSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateBrandSiteDomainModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IBrandSiteDomainModel> SelectLiteBrandSiteDomainAndMapToBrandSiteDomainModel(
            this IQueryable<BrandSiteDomain> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandSiteDomainSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltBrandSiteDomainSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateBrandSiteDomainModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IBrandSiteDomainModel> SelectListBrandSiteDomainAndMapToBrandSiteDomainModel(
            this IQueryable<BrandSiteDomain> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandSiteDomainSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltBrandSiteDomainSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateBrandSiteDomainModelFromEntityList(x, contextProfileName))!;
        }

        public static IBrandSiteDomainModel? SelectFirstFullBrandSiteDomainAndMapToBrandSiteDomainModel(
            this IQueryable<BrandSiteDomain> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandSiteDomainSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltBrandSiteDomainSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateBrandSiteDomainModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IBrandSiteDomainModel? SelectFirstListBrandSiteDomainAndMapToBrandSiteDomainModel(
            this IQueryable<BrandSiteDomain> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandSiteDomainSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltBrandSiteDomainSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateBrandSiteDomainModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IBrandSiteDomainModel? SelectSingleFullBrandSiteDomainAndMapToBrandSiteDomainModel(
            this IQueryable<BrandSiteDomain> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandSiteDomainSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltBrandSiteDomainSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateBrandSiteDomainModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IBrandSiteDomainModel? SelectSingleLiteBrandSiteDomainAndMapToBrandSiteDomainModel(
            this IQueryable<BrandSiteDomain> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandSiteDomainSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltBrandSiteDomainSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateBrandSiteDomainModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IBrandSiteDomainModel? SelectSingleListBrandSiteDomainAndMapToBrandSiteDomainModel(
            this IQueryable<BrandSiteDomain> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandSiteDomainSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltBrandSiteDomainSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateBrandSiteDomainModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IBrandSiteDomainModel> results, int totalPages, int totalCount) SelectFullBrandSiteDomainAndMapToBrandSiteDomainModel(
            this IQueryable<BrandSiteDomain> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandSiteDomainSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltBrandSiteDomainSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateBrandSiteDomainModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IBrandSiteDomainModel> results, int totalPages, int totalCount) SelectLiteBrandSiteDomainAndMapToBrandSiteDomainModel(
            this IQueryable<BrandSiteDomain> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandSiteDomainSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltBrandSiteDomainSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateBrandSiteDomainModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IBrandSiteDomainModel> results, int totalPages, int totalCount) SelectListBrandSiteDomainAndMapToBrandSiteDomainModel(
            this IQueryable<BrandSiteDomain> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltBrandSiteDomainSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltBrandSiteDomainSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateBrandSiteDomainModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IBrandSiteDomainModel? CreateBrandSiteDomainModelFromEntityFull(this IBrandSiteDomain? entity, string? contextProfileName)
        {
            return CreateBrandSiteDomainModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IBrandSiteDomainModel? CreateBrandSiteDomainModelFromEntityLite(this IBrandSiteDomain? entity, string? contextProfileName)
        {
            return CreateBrandSiteDomainModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IBrandSiteDomainModel? CreateBrandSiteDomainModelFromEntityList(this IBrandSiteDomain? entity, string? contextProfileName)
        {
            return CreateBrandSiteDomainModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IBrandSiteDomainModel? CreateBrandSiteDomainModelFromEntity(
            this IBrandSiteDomain? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IBrandSiteDomainModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // BrandSiteDomain's Properties
                    // BrandSiteDomain's Related Objects
                    // BrandSiteDomain's Associated Objects
                    // Additional Mappings
                    if (CreateBrandSiteDomainModelFromEntityHooksFull != null) { model = CreateBrandSiteDomainModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // BrandSiteDomain's Properties
                    // BrandSiteDomain's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // BrandSiteDomain's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateBrandSiteDomainModelFromEntityHooksLite != null) { model = CreateBrandSiteDomainModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // BrandSiteDomain's Properties
                    // BrandSiteDomain's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.MasterName = entity.Master?.Name;
                    model.SlaveID = entity.SlaveID;
                    model.Slave = ModelMapperForSiteDomain.CreateSiteDomainModelFromEntityLite(entity.Slave, contextProfileName);
                    model.SlaveKey = entity.Slave?.CustomKey;
                    model.SlaveName = entity.Slave?.Name;
                    // BrandSiteDomain's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateBrandSiteDomainModelFromEntityHooksList != null) { model = CreateBrandSiteDomainModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
