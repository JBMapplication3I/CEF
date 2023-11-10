// <autogenerated>
// <copyright file="Mapping.Discounts.DiscountCode.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForDiscountCode
    {
        public sealed class AnonDiscountCode : DiscountCode
        {
            public Contact? UserContact { get; set; }
        }

        public static readonly Func<DiscountCode?, string?, IDiscountCodeModel?> MapDiscountCodeModelFromEntityFull = CreateDiscountCodeModelFromEntityFull;

        public static readonly Func<DiscountCode?, string?, IDiscountCodeModel?> MapDiscountCodeModelFromEntityLite = CreateDiscountCodeModelFromEntityLite;

        public static readonly Func<DiscountCode?, string?, IDiscountCodeModel?> MapDiscountCodeModelFromEntityList = CreateDiscountCodeModelFromEntityList;

        public static Func<IDiscountCode, IDiscountCodeModel, string?, IDiscountCodeModel>? CreateDiscountCodeModelFromEntityHooksFull { get; set; }

        public static Func<IDiscountCode, IDiscountCodeModel, string?, IDiscountCodeModel>? CreateDiscountCodeModelFromEntityHooksLite { get; set; }

        public static Func<IDiscountCode, IDiscountCodeModel, string?, IDiscountCodeModel>? CreateDiscountCodeModelFromEntityHooksList { get; set; }

        public static Expression<Func<DiscountCode, AnonDiscountCode>>? PreBuiltDiscountCodeSQLSelectorFull { get; set; }

        public static Expression<Func<DiscountCode, AnonDiscountCode>>? PreBuiltDiscountCodeSQLSelectorLite { get; set; }

        public static Expression<Func<DiscountCode, AnonDiscountCode>>? PreBuiltDiscountCodeSQLSelectorList { get; set; }

        /// <summary>An <see cref="IDiscountCodeModel"/> extension method that creates a(n) <see cref="DiscountCode"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="DiscountCode"/> entity.</returns>
        public static IDiscountCode CreateDiscountCodeEntity(
            this IDiscountCodeModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IDiscountCodeModel, DiscountCode>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateDiscountCodeFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IDiscountCodeModel"/> extension method that updates a(n) <see cref="DiscountCode"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="DiscountCode"/> entity.</returns>
        public static IDiscountCode UpdateDiscountCodeFromModel(
            this IDiscountCode entity,
            IDiscountCodeModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // DiscountCode Properties
            entity.Code = model.Code;
            // DiscountCode's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenDiscountCodeSQLSelectorFull()
        {
            PreBuiltDiscountCodeSQLSelectorFull = x => x == null ? null! : new AnonDiscountCode
            {
                Code = x.Code,
                DiscountID = x.DiscountID,
                UserID = x.UserID,
                User = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.User!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenDiscountCodeSQLSelectorLite()
        {
            PreBuiltDiscountCodeSQLSelectorLite = x => x == null ? null! : new AnonDiscountCode
            {
                Code = x.Code,
                DiscountID = x.DiscountID,
                UserID = x.UserID,
                User = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.User!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenDiscountCodeSQLSelectorList()
        {
            PreBuiltDiscountCodeSQLSelectorList = x => x == null ? null! : new AnonDiscountCode
            {
                Code = x.Code,
                DiscountID = x.DiscountID,
                UserID = x.UserID,
                User = ModelMapperForUser.PreBuiltUserSQLSelectorList.Expand().Compile().Invoke(x.User!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IDiscountCodeModel> SelectFullDiscountCodeAndMapToDiscountCodeModel(
            this IQueryable<DiscountCode> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountCodeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltDiscountCodeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateDiscountCodeModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IDiscountCodeModel> SelectLiteDiscountCodeAndMapToDiscountCodeModel(
            this IQueryable<DiscountCode> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountCodeSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltDiscountCodeSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateDiscountCodeModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IDiscountCodeModel> SelectListDiscountCodeAndMapToDiscountCodeModel(
            this IQueryable<DiscountCode> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountCodeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltDiscountCodeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateDiscountCodeModelFromEntityList(x, contextProfileName))!;
        }

        public static IDiscountCodeModel? SelectFirstFullDiscountCodeAndMapToDiscountCodeModel(
            this IQueryable<DiscountCode> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountCodeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountCodeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateDiscountCodeModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IDiscountCodeModel? SelectFirstListDiscountCodeAndMapToDiscountCodeModel(
            this IQueryable<DiscountCode> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountCodeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountCodeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateDiscountCodeModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IDiscountCodeModel? SelectSingleFullDiscountCodeAndMapToDiscountCodeModel(
            this IQueryable<DiscountCode> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountCodeSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountCodeSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateDiscountCodeModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IDiscountCodeModel? SelectSingleLiteDiscountCodeAndMapToDiscountCodeModel(
            this IQueryable<DiscountCode> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountCodeSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountCodeSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateDiscountCodeModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IDiscountCodeModel? SelectSingleListDiscountCodeAndMapToDiscountCodeModel(
            this IQueryable<DiscountCode> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountCodeSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltDiscountCodeSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateDiscountCodeModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IDiscountCodeModel> results, int totalPages, int totalCount) SelectFullDiscountCodeAndMapToDiscountCodeModel(
            this IQueryable<DiscountCode> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountCodeSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltDiscountCodeSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateDiscountCodeModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IDiscountCodeModel> results, int totalPages, int totalCount) SelectLiteDiscountCodeAndMapToDiscountCodeModel(
            this IQueryable<DiscountCode> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountCodeSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltDiscountCodeSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateDiscountCodeModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IDiscountCodeModel> results, int totalPages, int totalCount) SelectListDiscountCodeAndMapToDiscountCodeModel(
            this IQueryable<DiscountCode> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltDiscountCodeSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltDiscountCodeSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateDiscountCodeModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IDiscountCodeModel? CreateDiscountCodeModelFromEntityFull(this IDiscountCode? entity, string? contextProfileName)
        {
            return CreateDiscountCodeModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IDiscountCodeModel? CreateDiscountCodeModelFromEntityLite(this IDiscountCode? entity, string? contextProfileName)
        {
            return CreateDiscountCodeModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IDiscountCodeModel? CreateDiscountCodeModelFromEntityList(this IDiscountCode? entity, string? contextProfileName)
        {
            return CreateDiscountCodeModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IDiscountCodeModel? CreateDiscountCodeModelFromEntity(
            this IDiscountCode? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IDiscountCodeModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // DiscountCode's Properties
                    // DiscountCode's Related Objects
                    model.User = ModelMapperForUser.CreateUserModelFromEntityLite(entity.User, contextProfileName);
                    // DiscountCode's Associated Objects
                    // Additional Mappings
                    if (CreateDiscountCodeModelFromEntityHooksFull != null) { model = CreateDiscountCodeModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // DiscountCode's Properties
                    // DiscountCode's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // DiscountCode's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateDiscountCodeModelFromEntityHooksLite != null) { model = CreateDiscountCodeModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // DiscountCode's Properties
                    model.Code = entity.Code;
                    // DiscountCode's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.DiscountID = entity.DiscountID;
                    model.UserID = entity.UserID;
                    model.UserKey = entity.User?.CustomKey;
                    // DiscountCode's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateDiscountCodeModelFromEntityHooksList != null) { model = CreateDiscountCodeModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
