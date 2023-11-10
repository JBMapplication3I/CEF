// <autogenerated>
// <copyright file="Mapping.Contacts.UserStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Contacts section of the Mapping class</summary>
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

    public static partial class ModelMapperForUserStatus
    {
        public sealed class AnonUserStatus : UserStatus
        {
        }

        public static readonly Func<UserStatus?, string?, IStatusModel?> MapUserStatusModelFromEntityFull = CreateUserStatusModelFromEntityFull;

        public static readonly Func<UserStatus?, string?, IStatusModel?> MapUserStatusModelFromEntityLite = CreateUserStatusModelFromEntityLite;

        public static readonly Func<UserStatus?, string?, IStatusModel?> MapUserStatusModelFromEntityList = CreateUserStatusModelFromEntityList;

        public static Func<IUserStatus, IStatusModel, string?, IStatusModel>? CreateUserStatusModelFromEntityHooksFull { get; set; }

        public static Func<IUserStatus, IStatusModel, string?, IStatusModel>? CreateUserStatusModelFromEntityHooksLite { get; set; }

        public static Func<IUserStatus, IStatusModel, string?, IStatusModel>? CreateUserStatusModelFromEntityHooksList { get; set; }

        public static Expression<Func<UserStatus, AnonUserStatus>>? PreBuiltUserStatusSQLSelectorFull { get; set; }

        public static Expression<Func<UserStatus, AnonUserStatus>>? PreBuiltUserStatusSQLSelectorLite { get; set; }

        public static Expression<Func<UserStatus, AnonUserStatus>>? PreBuiltUserStatusSQLSelectorList { get; set; }

        /// <summary>An <see cref="IStatusModel"/> extension method that creates a(n) <see cref="UserStatus"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="UserStatus"/> entity.</returns>
        public static IUserStatus CreateUserStatusEntity(
            this IStatusModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityStatusableBase<IStatusModel, UserStatus>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateUserStatusFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IStatusModel"/> extension method that updates a(n) <see cref="UserStatus"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="UserStatus"/> entity.</returns>
        public static IUserStatus UpdateUserStatusFromModel(
            this IUserStatus entity,
            IStatusModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapStatusableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenUserStatusSQLSelectorFull()
        {
            PreBuiltUserStatusSQLSelectorFull = x => x == null ? null! : new AnonUserStatus
            {
                DisplayName = x.DisplayName,
                SortOrder = x.SortOrder,
                TranslationKey = x.TranslationKey,
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

        public static void GenUserStatusSQLSelectorLite()
        {
            PreBuiltUserStatusSQLSelectorLite = x => x == null ? null! : new AnonUserStatus
            {
                DisplayName = x.DisplayName,
                SortOrder = x.SortOrder,
                TranslationKey = x.TranslationKey,
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

        public static void GenUserStatusSQLSelectorList()
        {
            PreBuiltUserStatusSQLSelectorList = x => x == null ? null! : new AnonUserStatus
            {
                DisplayName = x.DisplayName,
                SortOrder = x.SortOrder,
                TranslationKey = x.TranslationKey,
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IStatusModel> SelectFullUserStatusAndMapToStatusModel(
            this IQueryable<UserStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltUserStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltUserStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateUserStatusModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IStatusModel> SelectLiteUserStatusAndMapToStatusModel(
            this IQueryable<UserStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltUserStatusSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltUserStatusSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateUserStatusModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IStatusModel> SelectListUserStatusAndMapToStatusModel(
            this IQueryable<UserStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltUserStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltUserStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateUserStatusModelFromEntityList(x, contextProfileName))!;
        }

        public static IStatusModel? SelectFirstFullUserStatusAndMapToStatusModel(
            this IQueryable<UserStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltUserStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltUserStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateUserStatusModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStatusModel? SelectFirstListUserStatusAndMapToStatusModel(
            this IQueryable<UserStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltUserStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltUserStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateUserStatusModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStatusModel? SelectSingleFullUserStatusAndMapToStatusModel(
            this IQueryable<UserStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltUserStatusSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltUserStatusSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateUserStatusModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStatusModel? SelectSingleLiteUserStatusAndMapToStatusModel(
            this IQueryable<UserStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltUserStatusSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltUserStatusSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateUserStatusModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStatusModel? SelectSingleListUserStatusAndMapToStatusModel(
            this IQueryable<UserStatus> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltUserStatusSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltUserStatusSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateUserStatusModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectFullUserStatusAndMapToStatusModel(
            this IQueryable<UserStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltUserStatusSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltUserStatusSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateUserStatusModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectLiteUserStatusAndMapToStatusModel(
            this IQueryable<UserStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltUserStatusSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltUserStatusSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateUserStatusModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStatusModel> results, int totalPages, int totalCount) SelectListUserStatusAndMapToStatusModel(
            this IQueryable<UserStatus> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltUserStatusSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltUserStatusSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateUserStatusModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IStatusModel? CreateUserStatusModelFromEntityFull(this IUserStatus? entity, string? contextProfileName)
        {
            return CreateUserStatusModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IStatusModel? CreateUserStatusModelFromEntityLite(this IUserStatus? entity, string? contextProfileName)
        {
            return CreateUserStatusModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IStatusModel? CreateUserStatusModelFromEntityList(this IUserStatus? entity, string? contextProfileName)
        {
            return CreateUserStatusModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IStatusModel? CreateUserStatusModelFromEntity(
            this IUserStatus? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapStatusableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IStatusModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // UserStatus's Properties
                    // UserStatus's Related Objects
                    // UserStatus's Associated Objects
                    // Additional Mappings
                    if (CreateUserStatusModelFromEntityHooksFull != null) { model = CreateUserStatusModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // UserStatus's Properties
                    // UserStatus's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // UserStatus's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateUserStatusModelFromEntityHooksLite != null) { model = CreateUserStatusModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // UserStatus's Properties
                    // UserStatus's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // UserStatus's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateUserStatusModelFromEntityHooksList != null) { model = CreateUserStatusModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
