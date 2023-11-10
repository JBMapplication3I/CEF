// <autogenerated>
// <copyright file="Mapping.Payments.MembershipAdZoneAccess.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Payments section of the Mapping class</summary>
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

    public static partial class ModelMapperForMembershipAdZoneAccess
    {
        public sealed class AnonMembershipAdZoneAccess : MembershipAdZoneAccess
        {
            public new IEnumerable<MembershipAdZoneAccessByLevel>? MembershipAdZoneAccessByLevels { get; set; }
            // public new Membership? Master { get; set; }
        }

        public static readonly Func<MembershipAdZoneAccess?, string?, IMembershipAdZoneAccessModel?> MapMembershipAdZoneAccessModelFromEntityFull = CreateMembershipAdZoneAccessModelFromEntityFull;

        public static readonly Func<MembershipAdZoneAccess?, string?, IMembershipAdZoneAccessModel?> MapMembershipAdZoneAccessModelFromEntityLite = CreateMembershipAdZoneAccessModelFromEntityLite;

        public static readonly Func<MembershipAdZoneAccess?, string?, IMembershipAdZoneAccessModel?> MapMembershipAdZoneAccessModelFromEntityList = CreateMembershipAdZoneAccessModelFromEntityList;

        public static Func<IMembershipAdZoneAccess, IMembershipAdZoneAccessModel, string?, IMembershipAdZoneAccessModel>? CreateMembershipAdZoneAccessModelFromEntityHooksFull { get; set; }

        public static Func<IMembershipAdZoneAccess, IMembershipAdZoneAccessModel, string?, IMembershipAdZoneAccessModel>? CreateMembershipAdZoneAccessModelFromEntityHooksLite { get; set; }

        public static Func<IMembershipAdZoneAccess, IMembershipAdZoneAccessModel, string?, IMembershipAdZoneAccessModel>? CreateMembershipAdZoneAccessModelFromEntityHooksList { get; set; }

        public static Expression<Func<MembershipAdZoneAccess, AnonMembershipAdZoneAccess>>? PreBuiltMembershipAdZoneAccessSQLSelectorFull { get; set; }

        public static Expression<Func<MembershipAdZoneAccess, AnonMembershipAdZoneAccess>>? PreBuiltMembershipAdZoneAccessSQLSelectorLite { get; set; }

        public static Expression<Func<MembershipAdZoneAccess, AnonMembershipAdZoneAccess>>? PreBuiltMembershipAdZoneAccessSQLSelectorList { get; set; }

        /// <summary>An <see cref="IMembershipAdZoneAccessModel"/> extension method that creates a(n) <see cref="MembershipAdZoneAccess"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="MembershipAdZoneAccess"/> entity.</returns>
        public static IMembershipAdZoneAccess CreateMembershipAdZoneAccessEntity(
            this IMembershipAdZoneAccessModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IMembershipAdZoneAccessModel, MembershipAdZoneAccess>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateMembershipAdZoneAccessFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IMembershipAdZoneAccessModel"/> extension method that updates a(n) <see cref="MembershipAdZoneAccess"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="MembershipAdZoneAccess"/> entity.</returns>
        public static IMembershipAdZoneAccess UpdateMembershipAdZoneAccessFromModel(
            this IMembershipAdZoneAccess entity,
            IMembershipAdZoneAccessModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // MembershipAdZoneAccess's Related Objects
            // MembershipAdZoneAccess's Associated Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenMembershipAdZoneAccessSQLSelectorFull()
        {
            PreBuiltMembershipAdZoneAccessSQLSelectorFull = x => x == null ? null! : new AnonMembershipAdZoneAccess
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForAdZoneAccess.PreBuiltAdZoneAccessSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                MembershipAdZoneAccessByLevels = x.MembershipAdZoneAccessByLevels!.Where(y => y.Active).Select(ModelMapperForMembershipAdZoneAccessByLevel.PreBuiltMembershipAdZoneAccessByLevelSQLSelectorList.Expand().Compile()).ToList(),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenMembershipAdZoneAccessSQLSelectorLite()
        {
            PreBuiltMembershipAdZoneAccessSQLSelectorLite = x => x == null ? null! : new AnonMembershipAdZoneAccess
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForAdZoneAccess.PreBuiltAdZoneAccessSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenMembershipAdZoneAccessSQLSelectorList()
        {
            PreBuiltMembershipAdZoneAccessSQLSelectorList = x => x == null ? null! : new AnonMembershipAdZoneAccess
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForAdZoneAccess.PreBuiltAdZoneAccessSQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
                Master = ModelMapperForMembership.PreBuiltMembershipSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties
            };
        }

        public static IEnumerable<IMembershipAdZoneAccessModel> SelectFullMembershipAdZoneAccessAndMapToMembershipAdZoneAccessModel(
            this IQueryable<MembershipAdZoneAccess> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMembershipAdZoneAccessSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltMembershipAdZoneAccessSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateMembershipAdZoneAccessModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IMembershipAdZoneAccessModel> SelectLiteMembershipAdZoneAccessAndMapToMembershipAdZoneAccessModel(
            this IQueryable<MembershipAdZoneAccess> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMembershipAdZoneAccessSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltMembershipAdZoneAccessSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateMembershipAdZoneAccessModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IMembershipAdZoneAccessModel> SelectListMembershipAdZoneAccessAndMapToMembershipAdZoneAccessModel(
            this IQueryable<MembershipAdZoneAccess> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMembershipAdZoneAccessSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltMembershipAdZoneAccessSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateMembershipAdZoneAccessModelFromEntityList(x, contextProfileName))!;
        }

        public static IMembershipAdZoneAccessModel? SelectFirstFullMembershipAdZoneAccessAndMapToMembershipAdZoneAccessModel(
            this IQueryable<MembershipAdZoneAccess> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMembershipAdZoneAccessSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltMembershipAdZoneAccessSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateMembershipAdZoneAccessModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IMembershipAdZoneAccessModel? SelectFirstListMembershipAdZoneAccessAndMapToMembershipAdZoneAccessModel(
            this IQueryable<MembershipAdZoneAccess> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMembershipAdZoneAccessSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltMembershipAdZoneAccessSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateMembershipAdZoneAccessModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IMembershipAdZoneAccessModel? SelectSingleFullMembershipAdZoneAccessAndMapToMembershipAdZoneAccessModel(
            this IQueryable<MembershipAdZoneAccess> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMembershipAdZoneAccessSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltMembershipAdZoneAccessSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateMembershipAdZoneAccessModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IMembershipAdZoneAccessModel? SelectSingleLiteMembershipAdZoneAccessAndMapToMembershipAdZoneAccessModel(
            this IQueryable<MembershipAdZoneAccess> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMembershipAdZoneAccessSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltMembershipAdZoneAccessSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateMembershipAdZoneAccessModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IMembershipAdZoneAccessModel? SelectSingleListMembershipAdZoneAccessAndMapToMembershipAdZoneAccessModel(
            this IQueryable<MembershipAdZoneAccess> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMembershipAdZoneAccessSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltMembershipAdZoneAccessSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateMembershipAdZoneAccessModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IMembershipAdZoneAccessModel> results, int totalPages, int totalCount) SelectFullMembershipAdZoneAccessAndMapToMembershipAdZoneAccessModel(
            this IQueryable<MembershipAdZoneAccess> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMembershipAdZoneAccessSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltMembershipAdZoneAccessSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateMembershipAdZoneAccessModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IMembershipAdZoneAccessModel> results, int totalPages, int totalCount) SelectLiteMembershipAdZoneAccessAndMapToMembershipAdZoneAccessModel(
            this IQueryable<MembershipAdZoneAccess> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMembershipAdZoneAccessSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltMembershipAdZoneAccessSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateMembershipAdZoneAccessModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IMembershipAdZoneAccessModel> results, int totalPages, int totalCount) SelectListMembershipAdZoneAccessAndMapToMembershipAdZoneAccessModel(
            this IQueryable<MembershipAdZoneAccess> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltMembershipAdZoneAccessSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltMembershipAdZoneAccessSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateMembershipAdZoneAccessModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IMembershipAdZoneAccessModel? CreateMembershipAdZoneAccessModelFromEntityFull(this IMembershipAdZoneAccess? entity, string? contextProfileName)
        {
            return CreateMembershipAdZoneAccessModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IMembershipAdZoneAccessModel? CreateMembershipAdZoneAccessModelFromEntityLite(this IMembershipAdZoneAccess? entity, string? contextProfileName)
        {
            return CreateMembershipAdZoneAccessModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IMembershipAdZoneAccessModel? CreateMembershipAdZoneAccessModelFromEntityList(this IMembershipAdZoneAccess? entity, string? contextProfileName)
        {
            return CreateMembershipAdZoneAccessModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IMembershipAdZoneAccessModel? CreateMembershipAdZoneAccessModelFromEntity(
            this IMembershipAdZoneAccess? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IMembershipAdZoneAccessModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // MembershipAdZoneAccess's Properties
                    // MembershipAdZoneAccess's Related Objects
                    // MembershipAdZoneAccess's Associated Objects
                    model.MembershipAdZoneAccessByLevels = (entity is AnonMembershipAdZoneAccess ? ((AnonMembershipAdZoneAccess)entity).MembershipAdZoneAccessByLevels : entity.MembershipAdZoneAccessByLevels)?.Where(x => x.Active).Select(x => ModelMapperForMembershipAdZoneAccessByLevel.CreateMembershipAdZoneAccessByLevelModelFromEntityList(x, contextProfileName)).ToList()!;
                    // Additional Mappings
                    if (CreateMembershipAdZoneAccessModelFromEntityHooksFull != null) { model = CreateMembershipAdZoneAccessModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // MembershipAdZoneAccess's Properties
                    // MembershipAdZoneAccess's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // MembershipAdZoneAccess's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateMembershipAdZoneAccessModelFromEntityHooksLite != null) { model = CreateMembershipAdZoneAccessModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // MembershipAdZoneAccess's Properties
                    // MembershipAdZoneAccess's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.MasterName = entity.Master?.Name;
                    model.SlaveID = entity.SlaveID;
                    model.Slave = ModelMapperForAdZoneAccess.CreateAdZoneAccessModelFromEntityLite(entity.Slave, contextProfileName);
                    model.SlaveKey = entity.Slave?.CustomKey;
                    model.SlaveName = entity.Slave?.Name;
                    // MembershipAdZoneAccess's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateMembershipAdZoneAccessModelFromEntityHooksList != null) { model = CreateMembershipAdZoneAccessModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
