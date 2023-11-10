// <autogenerated>
// <copyright file="Mapping.Accounts.AccountAssociation.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Accounts section of the Mapping class</summary>
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

    public static partial class ModelMapperForAccountAssociation
    {
        public sealed class AnonAccountAssociation : AccountAssociation
        {
            // public new Account? Master { get; set; }
        }

        public static readonly Func<AccountAssociation?, string?, IAccountAssociationModel?> MapAccountAssociationModelFromEntityFull = CreateAccountAssociationModelFromEntityFull;

        public static readonly Func<AccountAssociation?, string?, IAccountAssociationModel?> MapAccountAssociationModelFromEntityLite = CreateAccountAssociationModelFromEntityLite;

        public static readonly Func<AccountAssociation?, string?, IAccountAssociationModel?> MapAccountAssociationModelFromEntityList = CreateAccountAssociationModelFromEntityList;

        public static Func<IAccountAssociation, IAccountAssociationModel, string?, IAccountAssociationModel>? CreateAccountAssociationModelFromEntityHooksFull { get; set; }

        public static Func<IAccountAssociation, IAccountAssociationModel, string?, IAccountAssociationModel>? CreateAccountAssociationModelFromEntityHooksLite { get; set; }

        public static Func<IAccountAssociation, IAccountAssociationModel, string?, IAccountAssociationModel>? CreateAccountAssociationModelFromEntityHooksList { get; set; }

        public static Expression<Func<AccountAssociation, AnonAccountAssociation>>? PreBuiltAccountAssociationSQLSelectorFull { get; set; }

        public static Expression<Func<AccountAssociation, AnonAccountAssociation>>? PreBuiltAccountAssociationSQLSelectorLite { get; set; }

        public static Expression<Func<AccountAssociation, AnonAccountAssociation>>? PreBuiltAccountAssociationSQLSelectorList { get; set; }

        /// <summary>An <see cref="IAccountAssociationModel"/> extension method that creates a(n) <see cref="AccountAssociation"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="AccountAssociation"/> entity.</returns>
        public static IAccountAssociation CreateAccountAssociationEntity(
            this IAccountAssociationModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IAccountAssociationModel, AccountAssociation>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateAccountAssociationFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IAccountAssociationModel"/> extension method that updates a(n) <see cref="AccountAssociation"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="AccountAssociation"/> entity.</returns>
        public static IAccountAssociation UpdateAccountAssociationFromModel(
            this IAccountAssociation entity,
            IAccountAssociationModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // AccountAssociation's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenAccountAssociationSQLSelectorFull()
        {
            PreBuiltAccountAssociationSQLSelectorFull = x => x == null ? null! : new AnonAccountAssociation
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForAccount.PreBuiltAccountSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                TypeID = x.TypeID,
                Type = ModelMapperForAccountAssociationType.PreBuiltAccountAssociationTypeSQLSelectorList.Expand().Compile().Invoke(x.Type!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenAccountAssociationSQLSelectorLite()
        {
            PreBuiltAccountAssociationSQLSelectorLite = x => x == null ? null! : new AnonAccountAssociation
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForAccount.PreBuiltAccountSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                TypeID = x.TypeID,
                Type = ModelMapperForAccountAssociationType.PreBuiltAccountAssociationTypeSQLSelectorList.Expand().Compile().Invoke(x.Type!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenAccountAssociationSQLSelectorList()
        {
            PreBuiltAccountAssociationSQLSelectorList = x => x == null ? null! : new AnonAccountAssociation
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForAccount.PreBuiltAccountSQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                TypeID = x.TypeID,
                Type = ModelMapperForAccountAssociationType.PreBuiltAccountAssociationTypeSQLSelectorList.Expand().Compile().Invoke(x.Type!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
                Master = ModelMapperForAccount.PreBuiltAccountSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties
            };
        }

        public static IEnumerable<IAccountAssociationModel> SelectFullAccountAssociationAndMapToAccountAssociationModel(
            this IQueryable<AccountAssociation> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAccountAssociationSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltAccountAssociationSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateAccountAssociationModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IAccountAssociationModel> SelectLiteAccountAssociationAndMapToAccountAssociationModel(
            this IQueryable<AccountAssociation> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAccountAssociationSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltAccountAssociationSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateAccountAssociationModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IAccountAssociationModel> SelectListAccountAssociationAndMapToAccountAssociationModel(
            this IQueryable<AccountAssociation> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAccountAssociationSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltAccountAssociationSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateAccountAssociationModelFromEntityList(x, contextProfileName))!;
        }

        public static IAccountAssociationModel? SelectFirstFullAccountAssociationAndMapToAccountAssociationModel(
            this IQueryable<AccountAssociation> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAccountAssociationSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAccountAssociationSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateAccountAssociationModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IAccountAssociationModel? SelectFirstListAccountAssociationAndMapToAccountAssociationModel(
            this IQueryable<AccountAssociation> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAccountAssociationSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAccountAssociationSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateAccountAssociationModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IAccountAssociationModel? SelectSingleFullAccountAssociationAndMapToAccountAssociationModel(
            this IQueryable<AccountAssociation> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAccountAssociationSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAccountAssociationSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateAccountAssociationModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IAccountAssociationModel? SelectSingleLiteAccountAssociationAndMapToAccountAssociationModel(
            this IQueryable<AccountAssociation> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAccountAssociationSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAccountAssociationSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateAccountAssociationModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IAccountAssociationModel? SelectSingleListAccountAssociationAndMapToAccountAssociationModel(
            this IQueryable<AccountAssociation> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAccountAssociationSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAccountAssociationSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateAccountAssociationModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IAccountAssociationModel> results, int totalPages, int totalCount) SelectFullAccountAssociationAndMapToAccountAssociationModel(
            this IQueryable<AccountAssociation> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAccountAssociationSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltAccountAssociationSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateAccountAssociationModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IAccountAssociationModel> results, int totalPages, int totalCount) SelectLiteAccountAssociationAndMapToAccountAssociationModel(
            this IQueryable<AccountAssociation> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAccountAssociationSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltAccountAssociationSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateAccountAssociationModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IAccountAssociationModel> results, int totalPages, int totalCount) SelectListAccountAssociationAndMapToAccountAssociationModel(
            this IQueryable<AccountAssociation> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAccountAssociationSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltAccountAssociationSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateAccountAssociationModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IAccountAssociationModel? CreateAccountAssociationModelFromEntityFull(this IAccountAssociation? entity, string? contextProfileName)
        {
            return CreateAccountAssociationModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IAccountAssociationModel? CreateAccountAssociationModelFromEntityLite(this IAccountAssociation? entity, string? contextProfileName)
        {
            return CreateAccountAssociationModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IAccountAssociationModel? CreateAccountAssociationModelFromEntityList(this IAccountAssociation? entity, string? contextProfileName)
        {
            return CreateAccountAssociationModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IAccountAssociationModel? CreateAccountAssociationModelFromEntity(
            this IAccountAssociation? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IAccountAssociationModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // IHaveATypeBase Properties
                    model.Type = ModelMapperForAccountAssociationType.CreateAccountAssociationTypeModelFromEntityLite(entity.Type, contextProfileName);
                    // AccountAssociation's Properties
                    // AccountAssociation's Related Objects
                    // AccountAssociation's Associated Objects
                    // Additional Mappings
                    if (CreateAccountAssociationModelFromEntityHooksFull != null) { model = CreateAccountAssociationModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // AccountAssociation's Properties
                    // AccountAssociation's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.Slave = ModelMapperForAccount.CreateAccountModelFromEntityLite(entity.Slave, contextProfileName);
                    // AccountAssociation's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateAccountAssociationModelFromEntityHooksLite != null) { model = CreateAccountAssociationModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveATypeBase Properties
                    model.TypeID = entity.TypeID;
                    if (entity.Type != null)
                    {
                        model.TypeKey = entity.Type.CustomKey;
                        model.TypeName = entity.Type.Name;
                        model.TypeDisplayName = entity.Type.DisplayName;
                        model.TypeTranslationKey = entity.Type.TranslationKey;
                        model.TypeSortOrder = entity.Type.SortOrder;
                    }
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // AccountAssociation's Properties
                    // AccountAssociation's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.MasterName = entity.Master?.Name;
                    model.SlaveID = entity.SlaveID;
                    model.SlaveKey = entity.Slave?.CustomKey;
                    model.SlaveName = entity.Slave?.Name;
                    // AccountAssociation's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateAccountAssociationModelFromEntityHooksList != null) { model = CreateAccountAssociationModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
