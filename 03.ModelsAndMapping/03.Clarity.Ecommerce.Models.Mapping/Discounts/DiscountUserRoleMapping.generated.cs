// <autogenerated>
// <copyright file="Mapping.Discounts.DiscountUserRole.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Discounts section of the Mapping class</summary>
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

    public static partial class ModelMapperForDiscountUserRole
    {
        public sealed class AnonDiscountUserRole : DiscountUserRole
        {
            // public new Discount? Master { get; set; }
        }

        public static readonly Func<DiscountUserRole?, string?, IDiscountUserRoleModel?> MapDiscountUserRoleModelFromEntityFull = CreateDiscountUserRoleModelFromEntityFull;

        public static readonly Func<DiscountUserRole?, string?, IDiscountUserRoleModel?> MapDiscountUserRoleModelFromEntityLite = CreateDiscountUserRoleModelFromEntityLite;

        public static readonly Func<DiscountUserRole?, string?, IDiscountUserRoleModel?> MapDiscountUserRoleModelFromEntityList = CreateDiscountUserRoleModelFromEntityList;

        public static Func<IDiscountUserRole, IDiscountUserRoleModel, string?, IDiscountUserRoleModel>? CreateDiscountUserRoleModelFromEntityHooksFull { get; set; }

        public static Func<IDiscountUserRole, IDiscountUserRoleModel, string?, IDiscountUserRoleModel>? CreateDiscountUserRoleModelFromEntityHooksLite { get; set; }

        public static Func<IDiscountUserRole, IDiscountUserRoleModel, string?, IDiscountUserRoleModel>? CreateDiscountUserRoleModelFromEntityHooksList { get; set; }

        public static Expression<Func<DiscountUserRole, AnonDiscountUserRole>>? PreBuiltDiscountUserRoleSQLSelectorFull { get; set; }

        public static Expression<Func<DiscountUserRole, AnonDiscountUserRole>>? PreBuiltDiscountUserRoleSQLSelectorLite { get; set; }

        public static Expression<Func<DiscountUserRole, AnonDiscountUserRole>>? PreBuiltDiscountUserRoleSQLSelectorList { get; set; }

        /// <summary>An <see cref="IDiscountUserRoleModel"/> extension method that creates a(n) <see cref="DiscountUserRole"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="DiscountUserRole"/> entity.</returns>
        public static IDiscountUserRole CreateDiscountUserRoleEntity(
            this IDiscountUserRoleModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IDiscountUserRoleModel, DiscountUserRole>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateDiscountUserRoleFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IDiscountUserRoleModel"/> extension method that updates a(n) <see cref="DiscountUserRole"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="DiscountUserRole"/> entity.</returns>
        public static IDiscountUserRole UpdateDiscountUserRoleFromModel(
            this IDiscountUserRole entity,
            IDiscountUserRoleModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // DiscountUserRole Properties
            entity.RoleName = model.RoleName;
            // DiscountUserRole's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenDiscountUserRoleSQLSelectorFull()
        {
            PreBuiltDiscountUserRoleSQLSelectorFull = x => x == null ? null! : new AnonDiscountUserRole
            {
                RoleName = x.RoleName,
                MasterID = x.MasterID,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenDiscountUserRoleSQLSelectorLite()
        {
            PreBuiltDiscountUserRoleSQLSelectorLite = x => x == null ? null! : new AnonDiscountUserRole
            {
                RoleName = x.RoleName,
                MasterID = x.MasterID,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenDiscountUserRoleSQLSelectorList()
        {
            PreBuiltDiscountUserRoleSQLSelectorList = x => x == null ? null! : new AnonDiscountUserRole
            {
                RoleName = x.RoleName,
                MasterID = x.MasterID,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IDiscountUserRoleModel> SelectFullDiscountUserRoleAndMapToDiscountUserRoleModel(
            this IQueryable<DiscountUserRole> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountUserRoleSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltDiscountUserRoleSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateDiscountUserRoleModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IDiscountUserRoleModel> SelectLiteDiscountUserRoleAndMapToDiscountUserRoleModel(
            this IQueryable<DiscountUserRole> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountUserRoleSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltDiscountUserRoleSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateDiscountUserRoleModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IDiscountUserRoleModel> SelectListDiscountUserRoleAndMapToDiscountUserRoleModel(
            this IQueryable<DiscountUserRole> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountUserRoleSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltDiscountUserRoleSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateDiscountUserRoleModelFromEntityList(x, contextProfileName))!;
        }

        public static IDiscountUserRoleModel? SelectFirstFullDiscountUserRoleAndMapToDiscountUserRoleModel(
            this IQueryable<DiscountUserRole> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountUserRoleSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountUserRoleSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateDiscountUserRoleModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IDiscountUserRoleModel? SelectFirstListDiscountUserRoleAndMapToDiscountUserRoleModel(
            this IQueryable<DiscountUserRole> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountUserRoleSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountUserRoleSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateDiscountUserRoleModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IDiscountUserRoleModel? SelectSingleFullDiscountUserRoleAndMapToDiscountUserRoleModel(
            this IQueryable<DiscountUserRole> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountUserRoleSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountUserRoleSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateDiscountUserRoleModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IDiscountUserRoleModel? SelectSingleLiteDiscountUserRoleAndMapToDiscountUserRoleModel(
            this IQueryable<DiscountUserRole> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountUserRoleSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountUserRoleSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateDiscountUserRoleModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IDiscountUserRoleModel? SelectSingleListDiscountUserRoleAndMapToDiscountUserRoleModel(
            this IQueryable<DiscountUserRole> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountUserRoleSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountUserRoleSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateDiscountUserRoleModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IDiscountUserRoleModel> results, int totalPages, int totalCount) SelectFullDiscountUserRoleAndMapToDiscountUserRoleModel(
            this IQueryable<DiscountUserRole> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountUserRoleSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltDiscountUserRoleSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateDiscountUserRoleModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IDiscountUserRoleModel> results, int totalPages, int totalCount) SelectLiteDiscountUserRoleAndMapToDiscountUserRoleModel(
            this IQueryable<DiscountUserRole> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountUserRoleSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltDiscountUserRoleSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateDiscountUserRoleModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IDiscountUserRoleModel> results, int totalPages, int totalCount) SelectListDiscountUserRoleAndMapToDiscountUserRoleModel(
            this IQueryable<DiscountUserRole> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountUserRoleSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltDiscountUserRoleSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateDiscountUserRoleModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IDiscountUserRoleModel? CreateDiscountUserRoleModelFromEntityFull(this IDiscountUserRole? entity, string? contextProfileName)
        {
            return CreateDiscountUserRoleModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IDiscountUserRoleModel? CreateDiscountUserRoleModelFromEntityLite(this IDiscountUserRole? entity, string? contextProfileName)
        {
            return CreateDiscountUserRoleModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IDiscountUserRoleModel? CreateDiscountUserRoleModelFromEntityList(this IDiscountUserRole? entity, string? contextProfileName)
        {
            return CreateDiscountUserRoleModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IDiscountUserRoleModel? CreateDiscountUserRoleModelFromEntity(
            this IDiscountUserRole? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IDiscountUserRoleModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // DiscountUserRole's Properties
                    // DiscountUserRole's Related Objects
                    // DiscountUserRole's Associated Objects
                    // Additional Mappings
                    if (CreateDiscountUserRoleModelFromEntityHooksFull != null) { model = CreateDiscountUserRoleModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // DiscountUserRole's Properties
                    // DiscountUserRole's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // DiscountUserRole's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateDiscountUserRoleModelFromEntityHooksLite != null) { model = CreateDiscountUserRoleModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // DiscountUserRole's Properties
                    model.RoleName = entity.RoleName;
                    // DiscountUserRole's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    // DiscountUserRole's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateDiscountUserRoleModelFromEntityHooksList != null) { model = CreateDiscountUserRoleModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
