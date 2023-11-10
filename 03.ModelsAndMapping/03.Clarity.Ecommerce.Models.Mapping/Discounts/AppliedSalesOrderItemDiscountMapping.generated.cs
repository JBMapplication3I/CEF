// <autogenerated>
// <copyright file="Mapping.Discounts.AppliedSalesOrderItemDiscount.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForAppliedSalesOrderItemDiscount
    {
        public sealed class AnonAppliedSalesOrderItemDiscount : AppliedSalesOrderItemDiscount
        {
            // public new SalesOrderItem? Master { get; set; }
        }

        public static readonly Func<AppliedSalesOrderItemDiscount?, string?, IAppliedSalesOrderItemDiscountModel?> MapAppliedSalesOrderItemDiscountModelFromEntityFull = CreateAppliedSalesOrderItemDiscountModelFromEntityFull;

        public static readonly Func<AppliedSalesOrderItemDiscount?, string?, IAppliedSalesOrderItemDiscountModel?> MapAppliedSalesOrderItemDiscountModelFromEntityLite = CreateAppliedSalesOrderItemDiscountModelFromEntityLite;

        public static readonly Func<AppliedSalesOrderItemDiscount?, string?, IAppliedSalesOrderItemDiscountModel?> MapAppliedSalesOrderItemDiscountModelFromEntityList = CreateAppliedSalesOrderItemDiscountModelFromEntityList;

        public static Func<IAppliedSalesOrderItemDiscount, IAppliedSalesOrderItemDiscountModel, string?, IAppliedSalesOrderItemDiscountModel>? CreateAppliedSalesOrderItemDiscountModelFromEntityHooksFull { get; set; }

        public static Func<IAppliedSalesOrderItemDiscount, IAppliedSalesOrderItemDiscountModel, string?, IAppliedSalesOrderItemDiscountModel>? CreateAppliedSalesOrderItemDiscountModelFromEntityHooksLite { get; set; }

        public static Func<IAppliedSalesOrderItemDiscount, IAppliedSalesOrderItemDiscountModel, string?, IAppliedSalesOrderItemDiscountModel>? CreateAppliedSalesOrderItemDiscountModelFromEntityHooksList { get; set; }

        public static Expression<Func<AppliedSalesOrderItemDiscount, AnonAppliedSalesOrderItemDiscount>>? PreBuiltAppliedSalesOrderItemDiscountSQLSelectorFull { get; set; }

        public static Expression<Func<AppliedSalesOrderItemDiscount, AnonAppliedSalesOrderItemDiscount>>? PreBuiltAppliedSalesOrderItemDiscountSQLSelectorLite { get; set; }

        public static Expression<Func<AppliedSalesOrderItemDiscount, AnonAppliedSalesOrderItemDiscount>>? PreBuiltAppliedSalesOrderItemDiscountSQLSelectorList { get; set; }

        /// <summary>An <see cref="IAppliedSalesOrderItemDiscountModel"/> extension method that creates a(n) <see cref="AppliedSalesOrderItemDiscount"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="AppliedSalesOrderItemDiscount"/> entity.</returns>
        public static IAppliedSalesOrderItemDiscount CreateAppliedSalesOrderItemDiscountEntity(
            this IAppliedSalesOrderItemDiscountModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IAppliedSalesOrderItemDiscountModel, AppliedSalesOrderItemDiscount>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateAppliedSalesOrderItemDiscountFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IAppliedSalesOrderItemDiscountModel"/> extension method that updates a(n) <see cref="AppliedSalesOrderItemDiscount"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="AppliedSalesOrderItemDiscount"/> entity.</returns>
        public static IAppliedSalesOrderItemDiscount UpdateAppliedSalesOrderItemDiscountFromModel(
            this IAppliedSalesOrderItemDiscount entity,
            IAppliedSalesOrderItemDiscountModel model,
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
            // AppliedSalesOrderItemDiscount's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenAppliedSalesOrderItemDiscountSQLSelectorFull()
        {
            PreBuiltAppliedSalesOrderItemDiscountSQLSelectorFull = x => x == null ? null! : new AnonAppliedSalesOrderItemDiscount
            {
                MasterID = x.MasterID,
                Master = ModelMapperForSalesOrderItem.PreBuiltSalesOrderItemSQLSelectorList.Expand().Compile().Invoke(x.Master!),
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

        public static void GenAppliedSalesOrderItemDiscountSQLSelectorLite()
        {
            PreBuiltAppliedSalesOrderItemDiscountSQLSelectorLite = x => x == null ? null! : new AnonAppliedSalesOrderItemDiscount
            {
                MasterID = x.MasterID,
                Master = ModelMapperForSalesOrderItem.PreBuiltSalesOrderItemSQLSelectorList.Expand().Compile().Invoke(x.Master!),
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

        public static void GenAppliedSalesOrderItemDiscountSQLSelectorList()
        {
            PreBuiltAppliedSalesOrderItemDiscountSQLSelectorList = x => x == null ? null! : new AnonAppliedSalesOrderItemDiscount
            {
                MasterID = x.MasterID,
                Master = ModelMapperForSalesOrderItem.PreBuiltSalesOrderItemSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties (List)
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

        public static IEnumerable<IAppliedSalesOrderItemDiscountModel> SelectFullAppliedSalesOrderItemDiscountAndMapToAppliedSalesOrderItemDiscountModel(
            this IQueryable<AppliedSalesOrderItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesOrderItemDiscountModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IAppliedSalesOrderItemDiscountModel> SelectLiteAppliedSalesOrderItemDiscountAndMapToAppliedSalesOrderItemDiscountModel(
            this IQueryable<AppliedSalesOrderItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesOrderItemDiscountModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IAppliedSalesOrderItemDiscountModel> SelectListAppliedSalesOrderItemDiscountAndMapToAppliedSalesOrderItemDiscountModel(
            this IQueryable<AppliedSalesOrderItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesOrderItemDiscountModelFromEntityList(x, contextProfileName))!;
        }

        public static IAppliedSalesOrderItemDiscountModel? SelectFirstFullAppliedSalesOrderItemDiscountAndMapToAppliedSalesOrderItemDiscountModel(
            this IQueryable<AppliedSalesOrderItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesOrderItemDiscountModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IAppliedSalesOrderItemDiscountModel? SelectFirstListAppliedSalesOrderItemDiscountAndMapToAppliedSalesOrderItemDiscountModel(
            this IQueryable<AppliedSalesOrderItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesOrderItemDiscountModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IAppliedSalesOrderItemDiscountModel? SelectSingleFullAppliedSalesOrderItemDiscountAndMapToAppliedSalesOrderItemDiscountModel(
            this IQueryable<AppliedSalesOrderItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesOrderItemDiscountModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IAppliedSalesOrderItemDiscountModel? SelectSingleLiteAppliedSalesOrderItemDiscountAndMapToAppliedSalesOrderItemDiscountModel(
            this IQueryable<AppliedSalesOrderItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesOrderItemDiscountModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IAppliedSalesOrderItemDiscountModel? SelectSingleListAppliedSalesOrderItemDiscountAndMapToAppliedSalesOrderItemDiscountModel(
            this IQueryable<AppliedSalesOrderItemDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesOrderItemDiscountModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IAppliedSalesOrderItemDiscountModel> results, int totalPages, int totalCount) SelectFullAppliedSalesOrderItemDiscountAndMapToAppliedSalesOrderItemDiscountModel(
            this IQueryable<AppliedSalesOrderItemDiscount> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateAppliedSalesOrderItemDiscountModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IAppliedSalesOrderItemDiscountModel> results, int totalPages, int totalCount) SelectLiteAppliedSalesOrderItemDiscountAndMapToAppliedSalesOrderItemDiscountModel(
            this IQueryable<AppliedSalesOrderItemDiscount> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateAppliedSalesOrderItemDiscountModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IAppliedSalesOrderItemDiscountModel> results, int totalPages, int totalCount) SelectListAppliedSalesOrderItemDiscountAndMapToAppliedSalesOrderItemDiscountModel(
            this IQueryable<AppliedSalesOrderItemDiscount> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltAppliedSalesOrderItemDiscountSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateAppliedSalesOrderItemDiscountModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IAppliedSalesOrderItemDiscountModel? CreateAppliedSalesOrderItemDiscountModelFromEntityFull(this IAppliedSalesOrderItemDiscount? entity, string? contextProfileName)
        {
            return CreateAppliedSalesOrderItemDiscountModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IAppliedSalesOrderItemDiscountModel? CreateAppliedSalesOrderItemDiscountModelFromEntityLite(this IAppliedSalesOrderItemDiscount? entity, string? contextProfileName)
        {
            return CreateAppliedSalesOrderItemDiscountModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IAppliedSalesOrderItemDiscountModel? CreateAppliedSalesOrderItemDiscountModelFromEntityList(this IAppliedSalesOrderItemDiscount? entity, string? contextProfileName)
        {
            return CreateAppliedSalesOrderItemDiscountModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IAppliedSalesOrderItemDiscountModel? CreateAppliedSalesOrderItemDiscountModelFromEntity(
            this IAppliedSalesOrderItemDiscount? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IAppliedSalesOrderItemDiscountModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // AppliedSalesOrderItemDiscount's Properties
                    // AppliedSalesOrderItemDiscount's Related Objects
                    // AppliedSalesOrderItemDiscount's Associated Objects
                    // Additional Mappings
                    if (CreateAppliedSalesOrderItemDiscountModelFromEntityHooksFull != null) { model = CreateAppliedSalesOrderItemDiscountModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // AppliedSalesOrderItemDiscount's Properties
                    // AppliedSalesOrderItemDiscount's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // AppliedSalesOrderItemDiscount's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateAppliedSalesOrderItemDiscountModelFromEntityHooksLite != null) { model = CreateAppliedSalesOrderItemDiscountModelFromEntityHooksLite(entity, model, contextProfileName); }
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
                    // AppliedSalesOrderItemDiscount's Properties
                    // AppliedSalesOrderItemDiscount's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.SlaveID = entity.SlaveID;
                    // AppliedSalesOrderItemDiscount's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateAppliedSalesOrderItemDiscountModelFromEntityHooksList != null) { model = CreateAppliedSalesOrderItemDiscountModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
