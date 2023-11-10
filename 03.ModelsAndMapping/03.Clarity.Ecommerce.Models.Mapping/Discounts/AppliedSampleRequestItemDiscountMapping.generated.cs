// <autogenerated>
// <copyright file="Mapping.Discounts.AppliedSampleRequestItemDiscount.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForAppliedSampleRequestItemDiscount
    {
        public sealed class AnonAppliedSampleRequestItemDiscount : AppliedSampleRequestItemDiscount
        {
            // public new SampleRequestItem? Master { get; set; }
        }

        public static readonly Func<AppliedSampleRequestItemDiscount?, string?, IAppliedSampleRequestItemDiscountModel?> MapAppliedSampleRequestItemDiscountModelFromEntityFull = CreateAppliedSampleRequestItemDiscountModelFromEntityFull;

        public static readonly Func<AppliedSampleRequestItemDiscount?, string?, IAppliedSampleRequestItemDiscountModel?> MapAppliedSampleRequestItemDiscountModelFromEntityLite = CreateAppliedSampleRequestItemDiscountModelFromEntityLite;

        public static readonly Func<AppliedSampleRequestItemDiscount?, string?, IAppliedSampleRequestItemDiscountModel?> MapAppliedSampleRequestItemDiscountModelFromEntityList = CreateAppliedSampleRequestItemDiscountModelFromEntityList;

        public static Func<IAppliedSampleRequestItemDiscount, IAppliedSampleRequestItemDiscountModel, string?, IAppliedSampleRequestItemDiscountModel>? CreateAppliedSampleRequestItemDiscountModelFromEntityHooksFull { get; set; }

        public static Func<IAppliedSampleRequestItemDiscount, IAppliedSampleRequestItemDiscountModel, string?, IAppliedSampleRequestItemDiscountModel>? CreateAppliedSampleRequestItemDiscountModelFromEntityHooksLite { get; set; }

        public static Func<IAppliedSampleRequestItemDiscount, IAppliedSampleRequestItemDiscountModel, string?, IAppliedSampleRequestItemDiscountModel>? CreateAppliedSampleRequestItemDiscountModelFromEntityHooksList { get; set; }

        public static Expression<Func<AppliedSampleRequestItemDiscount, AnonAppliedSampleRequestItemDiscount>>? PreBuiltAppliedSampleRequestItemDiscountSQLSelectorFull { get; set; }

        public static Expression<Func<AppliedSampleRequestItemDiscount, AnonAppliedSampleRequestItemDiscount>>? PreBuiltAppliedSampleRequestItemDiscountSQLSelectorLite { get; set; }

        public static Expression<Func<AppliedSampleRequestItemDiscount, AnonAppliedSampleRequestItemDiscount>>? PreBuiltAppliedSampleRequestItemDiscountSQLSelectorList { get; set; }

        /// <summary>An <see cref="IAppliedSampleRequestItemDiscountModel"/> extension method that creates a(n) <see cref="AppliedSampleRequestItemDiscount"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="AppliedSampleRequestItemDiscount"/> entity.</returns>
        public static IAppliedSampleRequestItemDiscount CreateAppliedSampleRequestItemDiscountEntity(
            this IAppliedSampleRequestItemDiscountModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IAppliedSampleRequestItemDiscountModel, AppliedSampleRequestItemDiscount>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateAppliedSampleRequestItemDiscountFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IAppliedSampleRequestItemDiscountModel"/> extension method that updates a(n) <see cref="AppliedSampleRequestItemDiscount"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="AppliedSampleRequestItemDiscount"/> entity.</returns>
        public static IAppliedSampleRequestItemDiscount UpdateAppliedSampleRequestItemDiscountFromModel(
            this IAppliedSampleRequestItemDiscount entity,
            IAppliedSampleRequestItemDiscountModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // IAppliedDiscountBase Properties
            entity.DiscountTotal = model.DiscountTotal;
            entity.ApplicationsUsed = model.ApplicationsUsed;
            entity.TargetApplicationsUsed = model.TargetApplicationsUsed;
            // AppliedSampleRequestItemDiscount's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenAppliedSampleRequestItemDiscountSQLSelectorFull()
        {
            PreBuiltAppliedSampleRequestItemDiscountSQLSelectorFull = x => x == null ? null! : new AnonAppliedSampleRequestItemDiscount
            {
                MasterID = x.MasterID,
                Master = ModelMapperForSampleRequestItem.PreBuiltSampleRequestItemSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                DiscountTotal = x.DiscountTotal,
                ApplicationsUsed = x.ApplicationsUsed,
                TargetApplicationsUsed = x.TargetApplicationsUsed,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenAppliedSampleRequestItemDiscountSQLSelectorLite()
        {
            PreBuiltAppliedSampleRequestItemDiscountSQLSelectorLite = x => x == null ? null! : new AnonAppliedSampleRequestItemDiscount
            {
                MasterID = x.MasterID,
                Master = ModelMapperForSampleRequestItem.PreBuiltSampleRequestItemSQLSelectorList.Expand().Compile().Invoke(x.Master!),
                SlaveID = x.SlaveID,
                DiscountTotal = x.DiscountTotal,
                ApplicationsUsed = x.ApplicationsUsed,
                TargetApplicationsUsed = x.TargetApplicationsUsed,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenAppliedSampleRequestItemDiscountSQLSelectorList()
        {
            PreBuiltAppliedSampleRequestItemDiscountSQLSelectorList = x => x == null ? null! : new AnonAppliedSampleRequestItemDiscount
            {
                MasterID = x.MasterID,
                Master = ModelMapperForSampleRequestItem.PreBuiltSampleRequestItemSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties (List)
                SlaveID = x.SlaveID,
                DiscountTotal = x.DiscountTotal,
                ApplicationsUsed = x.ApplicationsUsed,
                TargetApplicationsUsed = x.TargetApplicationsUsed,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IAppliedSampleRequestItemDiscountModel> SelectFullAppliedSampleRequestItemDiscountAndMapToAppliedSampleRequestItemDiscountModel(
            this IQueryable<AppliedSampleRequestItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateAppliedSampleRequestItemDiscountModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IAppliedSampleRequestItemDiscountModel> SelectLiteAppliedSampleRequestItemDiscountAndMapToAppliedSampleRequestItemDiscountModel(
            this IQueryable<AppliedSampleRequestItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateAppliedSampleRequestItemDiscountModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IAppliedSampleRequestItemDiscountModel> SelectListAppliedSampleRequestItemDiscountAndMapToAppliedSampleRequestItemDiscountModel(
            this IQueryable<AppliedSampleRequestItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateAppliedSampleRequestItemDiscountModelFromEntityList(x, contextProfileName))!;
        }

        public static IAppliedSampleRequestItemDiscountModel? SelectFirstFullAppliedSampleRequestItemDiscountAndMapToAppliedSampleRequestItemDiscountModel(
            this IQueryable<AppliedSampleRequestItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateAppliedSampleRequestItemDiscountModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IAppliedSampleRequestItemDiscountModel? SelectFirstListAppliedSampleRequestItemDiscountAndMapToAppliedSampleRequestItemDiscountModel(
            this IQueryable<AppliedSampleRequestItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateAppliedSampleRequestItemDiscountModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IAppliedSampleRequestItemDiscountModel? SelectSingleFullAppliedSampleRequestItemDiscountAndMapToAppliedSampleRequestItemDiscountModel(
            this IQueryable<AppliedSampleRequestItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateAppliedSampleRequestItemDiscountModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IAppliedSampleRequestItemDiscountModel? SelectSingleLiteAppliedSampleRequestItemDiscountAndMapToAppliedSampleRequestItemDiscountModel(
            this IQueryable<AppliedSampleRequestItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateAppliedSampleRequestItemDiscountModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IAppliedSampleRequestItemDiscountModel? SelectSingleListAppliedSampleRequestItemDiscountAndMapToAppliedSampleRequestItemDiscountModel(
            this IQueryable<AppliedSampleRequestItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateAppliedSampleRequestItemDiscountModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IAppliedSampleRequestItemDiscountModel> results, int totalPages, int totalCount) SelectFullAppliedSampleRequestItemDiscountAndMapToAppliedSampleRequestItemDiscountModel(
            this IQueryable<AppliedSampleRequestItemDiscount> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateAppliedSampleRequestItemDiscountModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IAppliedSampleRequestItemDiscountModel> results, int totalPages, int totalCount) SelectLiteAppliedSampleRequestItemDiscountAndMapToAppliedSampleRequestItemDiscountModel(
            this IQueryable<AppliedSampleRequestItemDiscount> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateAppliedSampleRequestItemDiscountModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IAppliedSampleRequestItemDiscountModel> results, int totalPages, int totalCount) SelectListAppliedSampleRequestItemDiscountAndMapToAppliedSampleRequestItemDiscountModel(
            this IQueryable<AppliedSampleRequestItemDiscount> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltAppliedSampleRequestItemDiscountSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateAppliedSampleRequestItemDiscountModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IAppliedSampleRequestItemDiscountModel? CreateAppliedSampleRequestItemDiscountModelFromEntityFull(this IAppliedSampleRequestItemDiscount? entity, string? contextProfileName)
        {
            return CreateAppliedSampleRequestItemDiscountModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IAppliedSampleRequestItemDiscountModel? CreateAppliedSampleRequestItemDiscountModelFromEntityLite(this IAppliedSampleRequestItemDiscount? entity, string? contextProfileName)
        {
            return CreateAppliedSampleRequestItemDiscountModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IAppliedSampleRequestItemDiscountModel? CreateAppliedSampleRequestItemDiscountModelFromEntityList(this IAppliedSampleRequestItemDiscount? entity, string? contextProfileName)
        {
            return CreateAppliedSampleRequestItemDiscountModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IAppliedSampleRequestItemDiscountModel? CreateAppliedSampleRequestItemDiscountModelFromEntity(
            this IAppliedSampleRequestItemDiscount? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IAppliedSampleRequestItemDiscountModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // AppliedSampleRequestItemDiscount's Properties
                    // AppliedSampleRequestItemDiscount's Related Objects
                    // AppliedSampleRequestItemDiscount's Associated Objects
                    // Additional Mappings
                    if (CreateAppliedSampleRequestItemDiscountModelFromEntityHooksFull != null) { model = CreateAppliedSampleRequestItemDiscountModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // AppliedSampleRequestItemDiscount's Properties
                    // AppliedSampleRequestItemDiscount's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // AppliedSampleRequestItemDiscount's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateAppliedSampleRequestItemDiscountModelFromEntityHooksLite != null) { model = CreateAppliedSampleRequestItemDiscountModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // IAppliedDiscountBase Properties
                    model.DiscountTotal = entity.DiscountTotal;
                    model.ApplicationsUsed = entity.ApplicationsUsed;
                    model.TargetApplicationsUsed = entity.TargetApplicationsUsed;
                    model.MasterID = entity.MasterID;
                    if (entity.Slave != null)
                    {
                        model.DiscountValue = entity.Slave.Value;
                        model.DiscountValueType = entity.Slave.ValueType;
                        model.DiscountPriority = entity.Slave.Priority;
                        model.DiscountTypeID = entity.Slave.DiscountTypeID;
                        model.DiscountCanCombine = entity.Slave.CanCombine;
                        model.Slave = ModelMapperForDiscount.CreateDiscountModelFromEntityList(entity.Slave, contextProfileName);
                    }
                    // AppliedSampleRequestItemDiscount's Properties
                    // AppliedSampleRequestItemDiscount's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.SlaveID = entity.SlaveID;
                    // AppliedSampleRequestItemDiscount's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateAppliedSampleRequestItemDiscountModelFromEntityHooksList != null) { model = CreateAppliedSampleRequestItemDiscountModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
