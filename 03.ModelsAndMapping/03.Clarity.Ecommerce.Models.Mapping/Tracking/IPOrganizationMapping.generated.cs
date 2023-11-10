// <autogenerated>
// <copyright file="Mapping.Tracking.IPOrganization.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Tracking section of the Mapping class</summary>
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

    public static partial class ModelMapperForIPOrganization
    {
        public sealed class AnonIPOrganization : IPOrganization
        {
            public Contact? PrimaryUserContact { get; set; }
        }

        public static readonly Func<IPOrganization?, string?, IIPOrganizationModel?> MapIPOrganizationModelFromEntityFull = CreateIPOrganizationModelFromEntityFull;

        public static readonly Func<IPOrganization?, string?, IIPOrganizationModel?> MapIPOrganizationModelFromEntityLite = CreateIPOrganizationModelFromEntityLite;

        public static readonly Func<IPOrganization?, string?, IIPOrganizationModel?> MapIPOrganizationModelFromEntityList = CreateIPOrganizationModelFromEntityList;

        public static Func<IIPOrganization, IIPOrganizationModel, string?, IIPOrganizationModel>? CreateIPOrganizationModelFromEntityHooksFull { get; set; }

        public static Func<IIPOrganization, IIPOrganizationModel, string?, IIPOrganizationModel>? CreateIPOrganizationModelFromEntityHooksLite { get; set; }

        public static Func<IIPOrganization, IIPOrganizationModel, string?, IIPOrganizationModel>? CreateIPOrganizationModelFromEntityHooksList { get; set; }

        public static Expression<Func<IPOrganization, AnonIPOrganization>>? PreBuiltIPOrganizationSQLSelectorFull { get; set; }

        public static Expression<Func<IPOrganization, AnonIPOrganization>>? PreBuiltIPOrganizationSQLSelectorLite { get; set; }

        public static Expression<Func<IPOrganization, AnonIPOrganization>>? PreBuiltIPOrganizationSQLSelectorList { get; set; }

        /// <summary>An <see cref="IIPOrganizationModel"/> extension method that creates a(n) <see cref="IPOrganization"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="IPOrganization"/> entity.</returns>
        public static IIPOrganization CreateIPOrganizationEntity(
            this IIPOrganizationModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelNameableBase<IIPOrganizationModel, IPOrganization>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateIPOrganizationFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IIPOrganizationModel"/> extension method that updates a(n) <see cref="IPOrganization"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="IPOrganization"/> entity.</returns>
        public static IIPOrganization UpdateIPOrganizationFromModel(
            this IIPOrganization entity,
            IIPOrganizationModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapNameableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // IPOrganization Properties
            entity.IPAddress = model.IPAddress;
            entity.Score = model.Score;
            entity.VisitorKey = model.VisitorKey;
            // IPOrganization's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenIPOrganizationSQLSelectorFull()
        {
            PreBuiltIPOrganizationSQLSelectorFull = x => x == null ? null! : new AnonIPOrganization
            {
                StatusID = x.StatusID,
                Status = ModelMapperForIPOrganizationStatus.PreBuiltIPOrganizationStatusSQLSelectorList.Expand().Compile().Invoke(x.Status!),
                IPAddress = x.IPAddress,
                Score = x.Score,
                VisitorKey = x.VisitorKey,
                AddressID = x.AddressID,
                Address = ModelMapperForAddress.PreBuiltAddressSQLSelectorList.Expand().Compile().Invoke(x.Address!),
                PrimaryUserID = x.PrimaryUserID,
                PrimaryUser = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.PrimaryUser!),
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

        public static void GenIPOrganizationSQLSelectorLite()
        {
            PreBuiltIPOrganizationSQLSelectorLite = x => x == null ? null! : new AnonIPOrganization
            {
                StatusID = x.StatusID,
                Status = ModelMapperForIPOrganizationStatus.PreBuiltIPOrganizationStatusSQLSelectorList.Expand().Compile().Invoke(x.Status!),
                IPAddress = x.IPAddress,
                Score = x.Score,
                VisitorKey = x.VisitorKey,
                AddressID = x.AddressID,
                Address = ModelMapperForAddress.PreBuiltAddressSQLSelectorList.Expand().Compile().Invoke(x.Address!),
                PrimaryUserID = x.PrimaryUserID,
                PrimaryUser = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.PrimaryUser!),
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

        public static void GenIPOrganizationSQLSelectorList()
        {
            PreBuiltIPOrganizationSQLSelectorList = x => x == null ? null! : new AnonIPOrganization
            {
                StatusID = x.StatusID,
                Status = ModelMapperForIPOrganizationStatus.PreBuiltIPOrganizationStatusSQLSelectorList.Expand().Compile().Invoke(x.Status!), // For Flattening Properties (List)
                IPAddress = x.IPAddress,
                Score = x.Score,
                VisitorKey = x.VisitorKey,
                AddressID = x.AddressID,
                Address = ModelMapperForAddress.PreBuiltAddressSQLSelectorList.Expand().Compile().Invoke(x.Address!), // For Flattening Properties (List)
                PrimaryUserID = x.PrimaryUserID,
                PrimaryUser = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.PrimaryUser!), // For Flattening Properties (List)
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IIPOrganizationModel> SelectFullIPOrganizationAndMapToIPOrganizationModel(
            this IQueryable<IPOrganization> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltIPOrganizationSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltIPOrganizationSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateIPOrganizationModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IIPOrganizationModel> SelectLiteIPOrganizationAndMapToIPOrganizationModel(
            this IQueryable<IPOrganization> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltIPOrganizationSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltIPOrganizationSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateIPOrganizationModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IIPOrganizationModel> SelectListIPOrganizationAndMapToIPOrganizationModel(
            this IQueryable<IPOrganization> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltIPOrganizationSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltIPOrganizationSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateIPOrganizationModelFromEntityList(x, contextProfileName))!;
        }

        public static IIPOrganizationModel? SelectFirstFullIPOrganizationAndMapToIPOrganizationModel(
            this IQueryable<IPOrganization> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltIPOrganizationSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltIPOrganizationSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateIPOrganizationModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IIPOrganizationModel? SelectFirstListIPOrganizationAndMapToIPOrganizationModel(
            this IQueryable<IPOrganization> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltIPOrganizationSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltIPOrganizationSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateIPOrganizationModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IIPOrganizationModel? SelectSingleFullIPOrganizationAndMapToIPOrganizationModel(
            this IQueryable<IPOrganization> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltIPOrganizationSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltIPOrganizationSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateIPOrganizationModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IIPOrganizationModel? SelectSingleLiteIPOrganizationAndMapToIPOrganizationModel(
            this IQueryable<IPOrganization> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltIPOrganizationSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltIPOrganizationSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateIPOrganizationModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IIPOrganizationModel? SelectSingleListIPOrganizationAndMapToIPOrganizationModel(
            this IQueryable<IPOrganization> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltIPOrganizationSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltIPOrganizationSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateIPOrganizationModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IIPOrganizationModel> results, int totalPages, int totalCount) SelectFullIPOrganizationAndMapToIPOrganizationModel(
            this IQueryable<IPOrganization> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltIPOrganizationSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltIPOrganizationSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateIPOrganizationModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IIPOrganizationModel> results, int totalPages, int totalCount) SelectLiteIPOrganizationAndMapToIPOrganizationModel(
            this IQueryable<IPOrganization> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltIPOrganizationSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltIPOrganizationSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateIPOrganizationModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IIPOrganizationModel> results, int totalPages, int totalCount) SelectListIPOrganizationAndMapToIPOrganizationModel(
            this IQueryable<IPOrganization> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltIPOrganizationSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltIPOrganizationSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateIPOrganizationModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IIPOrganizationModel? CreateIPOrganizationModelFromEntityFull(this IIPOrganization? entity, string? contextProfileName)
        {
            return CreateIPOrganizationModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IIPOrganizationModel? CreateIPOrganizationModelFromEntityLite(this IIPOrganization? entity, string? contextProfileName)
        {
            return CreateIPOrganizationModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IIPOrganizationModel? CreateIPOrganizationModelFromEntityList(this IIPOrganization? entity, string? contextProfileName)
        {
            return CreateIPOrganizationModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IIPOrganizationModel? CreateIPOrganizationModelFromEntity(
            this IIPOrganization? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapNameableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IIPOrganizationModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // IPOrganization's Properties
                    // IPOrganization's Related Objects
                    model.Address = ModelMapperForAddress.CreateAddressModelFromEntityLite(entity.Address, contextProfileName);
                    model.PrimaryUser = ModelMapperForUser.CreateUserModelFromEntityLite(entity.PrimaryUser, contextProfileName);
                    // IPOrganization's Associated Objects
                    // Additional Mappings
                    if (CreateIPOrganizationModelFromEntityHooksFull != null) { model = CreateIPOrganizationModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // IPOrganization's Properties
                    // IHaveAStatusBase Properties (Forced)
                    model.Status = ModelMapperForIPOrganizationStatus.CreateIPOrganizationStatusModelFromEntityLite(entity.Status, contextProfileName);
                    // IPOrganization's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // IPOrganization's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateIPOrganizationModelFromEntityHooksLite != null) { model = CreateIPOrganizationModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveAStatusBase Properties
                    model.StatusID = entity.StatusID;
                    if (entity.Status != null)
                    {
                        model.StatusKey = entity.Status.CustomKey;
                        model.StatusName = entity.Status.Name;
                        model.StatusDisplayName = entity.Status.DisplayName;
                        model.StatusTranslationKey = entity.Status.TranslationKey;
                        model.StatusSortOrder = entity.Status.SortOrder;
                    }
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // IPOrganization's Properties
                    model.IPAddress = entity.IPAddress;
                    model.Score = entity.Score;
                    model.VisitorKey = entity.VisitorKey;
                    // IPOrganization's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.AddressID = entity.AddressID;
                    model.AddressKey = entity.Address?.CustomKey;
                    model.PrimaryUserID = entity.PrimaryUserID;
                    model.PrimaryUserKey = entity.PrimaryUser?.CustomKey;
                    // IPOrganization's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateIPOrganizationModelFromEntityHooksList != null) { model = CreateIPOrganizationModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
