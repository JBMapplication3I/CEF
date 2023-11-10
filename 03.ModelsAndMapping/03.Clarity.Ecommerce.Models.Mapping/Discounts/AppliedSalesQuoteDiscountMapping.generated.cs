// <autogenerated>
// <copyright file="Mapping.Discounts.AppliedSalesQuoteDiscount.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForAppliedSalesQuoteDiscount
    {
        public sealed class AnonAppliedSalesQuoteDiscount : AppliedSalesQuoteDiscount
        {
            // public new SalesQuote? Master { get; set; }
        }

        public static readonly Func<AppliedSalesQuoteDiscount?, string?, IAppliedSalesQuoteDiscountModel?> MapAppliedSalesQuoteDiscountModelFromEntityFull = CreateAppliedSalesQuoteDiscountModelFromEntityFull;

        public static readonly Func<AppliedSalesQuoteDiscount?, string?, IAppliedSalesQuoteDiscountModel?> MapAppliedSalesQuoteDiscountModelFromEntityLite = CreateAppliedSalesQuoteDiscountModelFromEntityLite;

        public static readonly Func<AppliedSalesQuoteDiscount?, string?, IAppliedSalesQuoteDiscountModel?> MapAppliedSalesQuoteDiscountModelFromEntityList = CreateAppliedSalesQuoteDiscountModelFromEntityList;

        public static Func<IAppliedSalesQuoteDiscount, IAppliedSalesQuoteDiscountModel, string?, IAppliedSalesQuoteDiscountModel>? CreateAppliedSalesQuoteDiscountModelFromEntityHooksFull { get; set; }

        public static Func<IAppliedSalesQuoteDiscount, IAppliedSalesQuoteDiscountModel, string?, IAppliedSalesQuoteDiscountModel>? CreateAppliedSalesQuoteDiscountModelFromEntityHooksLite { get; set; }

        public static Func<IAppliedSalesQuoteDiscount, IAppliedSalesQuoteDiscountModel, string?, IAppliedSalesQuoteDiscountModel>? CreateAppliedSalesQuoteDiscountModelFromEntityHooksList { get; set; }

        public static Expression<Func<AppliedSalesQuoteDiscount, AnonAppliedSalesQuoteDiscount>>? PreBuiltAppliedSalesQuoteDiscountSQLSelectorFull { get; set; }

        public static Expression<Func<AppliedSalesQuoteDiscount, AnonAppliedSalesQuoteDiscount>>? PreBuiltAppliedSalesQuoteDiscountSQLSelectorLite { get; set; }

        public static Expression<Func<AppliedSalesQuoteDiscount, AnonAppliedSalesQuoteDiscount>>? PreBuiltAppliedSalesQuoteDiscountSQLSelectorList { get; set; }

        /// <summary>An <see cref="IAppliedSalesQuoteDiscountModel"/> extension method that creates a(n) <see cref="AppliedSalesQuoteDiscount"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="AppliedSalesQuoteDiscount"/> entity.</returns>
        public static IAppliedSalesQuoteDiscount CreateAppliedSalesQuoteDiscountEntity(
            this IAppliedSalesQuoteDiscountModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IAppliedSalesQuoteDiscountModel, AppliedSalesQuoteDiscount>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateAppliedSalesQuoteDiscountFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IAppliedSalesQuoteDiscountModel"/> extension method that updates a(n) <see cref="AppliedSalesQuoteDiscount"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="AppliedSalesQuoteDiscount"/> entity.</returns>
        public static IAppliedSalesQuoteDiscount UpdateAppliedSalesQuoteDiscountFromModel(
            this IAppliedSalesQuoteDiscount entity,
            IAppliedSalesQuoteDiscountModel model,
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
            // AppliedSalesQuoteDiscount's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenAppliedSalesQuoteDiscountSQLSelectorFull()
        {
            PreBuiltAppliedSalesQuoteDiscountSQLSelectorFull = x => x == null ? null! : new AnonAppliedSalesQuoteDiscount
            {
                MasterID = x.MasterID,
                Master = ModelMapperForSalesQuote.PreBuiltSalesQuoteSQLSelectorList.Expand().Compile().Invoke(x.Master!),
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

        public static void GenAppliedSalesQuoteDiscountSQLSelectorLite()
        {
            PreBuiltAppliedSalesQuoteDiscountSQLSelectorLite = x => x == null ? null! : new AnonAppliedSalesQuoteDiscount
            {
                MasterID = x.MasterID,
                Master = ModelMapperForSalesQuote.PreBuiltSalesQuoteSQLSelectorList.Expand().Compile().Invoke(x.Master!),
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

        public static void GenAppliedSalesQuoteDiscountSQLSelectorList()
        {
            PreBuiltAppliedSalesQuoteDiscountSQLSelectorList = x => x == null ? null! : new AnonAppliedSalesQuoteDiscount
            {
                MasterID = x.MasterID,
                Master = ModelMapperForSalesQuote.PreBuiltSalesQuoteSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties (List)
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

        public static IEnumerable<IAppliedSalesQuoteDiscountModel> SelectFullAppliedSalesQuoteDiscountAndMapToAppliedSalesQuoteDiscountModel(
            this IQueryable<AppliedSalesQuoteDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesQuoteDiscountSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltAppliedSalesQuoteDiscountSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesQuoteDiscountModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IAppliedSalesQuoteDiscountModel> SelectLiteAppliedSalesQuoteDiscountAndMapToAppliedSalesQuoteDiscountModel(
            this IQueryable<AppliedSalesQuoteDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesQuoteDiscountSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltAppliedSalesQuoteDiscountSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesQuoteDiscountModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IAppliedSalesQuoteDiscountModel> SelectListAppliedSalesQuoteDiscountAndMapToAppliedSalesQuoteDiscountModel(
            this IQueryable<AppliedSalesQuoteDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesQuoteDiscountSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltAppliedSalesQuoteDiscountSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesQuoteDiscountModelFromEntityList(x, contextProfileName))!;
        }

        public static IAppliedSalesQuoteDiscountModel? SelectFirstFullAppliedSalesQuoteDiscountAndMapToAppliedSalesQuoteDiscountModel(
            this IQueryable<AppliedSalesQuoteDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesQuoteDiscountSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSalesQuoteDiscountSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesQuoteDiscountModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IAppliedSalesQuoteDiscountModel? SelectFirstListAppliedSalesQuoteDiscountAndMapToAppliedSalesQuoteDiscountModel(
            this IQueryable<AppliedSalesQuoteDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesQuoteDiscountSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSalesQuoteDiscountSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesQuoteDiscountModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IAppliedSalesQuoteDiscountModel? SelectSingleFullAppliedSalesQuoteDiscountAndMapToAppliedSalesQuoteDiscountModel(
            this IQueryable<AppliedSalesQuoteDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesQuoteDiscountSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSalesQuoteDiscountSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesQuoteDiscountModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IAppliedSalesQuoteDiscountModel? SelectSingleLiteAppliedSalesQuoteDiscountAndMapToAppliedSalesQuoteDiscountModel(
            this IQueryable<AppliedSalesQuoteDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesQuoteDiscountSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSalesQuoteDiscountSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesQuoteDiscountModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IAppliedSalesQuoteDiscountModel? SelectSingleListAppliedSalesQuoteDiscountAndMapToAppliedSalesQuoteDiscountModel(
            this IQueryable<AppliedSalesQuoteDiscount> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesQuoteDiscountSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltAppliedSalesQuoteDiscountSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateAppliedSalesQuoteDiscountModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IAppliedSalesQuoteDiscountModel> results, int totalPages, int totalCount) SelectFullAppliedSalesQuoteDiscountAndMapToAppliedSalesQuoteDiscountModel(
            this IQueryable<AppliedSalesQuoteDiscount> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesQuoteDiscountSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltAppliedSalesQuoteDiscountSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateAppliedSalesQuoteDiscountModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IAppliedSalesQuoteDiscountModel> results, int totalPages, int totalCount) SelectLiteAppliedSalesQuoteDiscountAndMapToAppliedSalesQuoteDiscountModel(
            this IQueryable<AppliedSalesQuoteDiscount> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesQuoteDiscountSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltAppliedSalesQuoteDiscountSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateAppliedSalesQuoteDiscountModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IAppliedSalesQuoteDiscountModel> results, int totalPages, int totalCount) SelectListAppliedSalesQuoteDiscountAndMapToAppliedSalesQuoteDiscountModel(
            this IQueryable<AppliedSalesQuoteDiscount> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltAppliedSalesQuoteDiscountSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltAppliedSalesQuoteDiscountSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateAppliedSalesQuoteDiscountModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IAppliedSalesQuoteDiscountModel? CreateAppliedSalesQuoteDiscountModelFromEntityFull(this IAppliedSalesQuoteDiscount? entity, string? contextProfileName)
        {
            return CreateAppliedSalesQuoteDiscountModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IAppliedSalesQuoteDiscountModel? CreateAppliedSalesQuoteDiscountModelFromEntityLite(this IAppliedSalesQuoteDiscount? entity, string? contextProfileName)
        {
            return CreateAppliedSalesQuoteDiscountModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IAppliedSalesQuoteDiscountModel? CreateAppliedSalesQuoteDiscountModelFromEntityList(this IAppliedSalesQuoteDiscount? entity, string? contextProfileName)
        {
            return CreateAppliedSalesQuoteDiscountModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IAppliedSalesQuoteDiscountModel? CreateAppliedSalesQuoteDiscountModelFromEntity(
            this IAppliedSalesQuoteDiscount? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IAppliedSalesQuoteDiscountModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // AppliedSalesQuoteDiscount's Properties
                    // AppliedSalesQuoteDiscount's Related Objects
                    // AppliedSalesQuoteDiscount's Associated Objects
                    // Additional Mappings
                    if (CreateAppliedSalesQuoteDiscountModelFromEntityHooksFull != null) { model = CreateAppliedSalesQuoteDiscountModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // AppliedSalesQuoteDiscount's Properties
                    // AppliedSalesQuoteDiscount's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // AppliedSalesQuoteDiscount's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateAppliedSalesQuoteDiscountModelFromEntityHooksLite != null) { model = CreateAppliedSalesQuoteDiscountModelFromEntityHooksLite(entity, model, contextProfileName); }
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
                    // AppliedSalesQuoteDiscount's Properties
                    // AppliedSalesQuoteDiscount's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.SlaveID = entity.SlaveID;
                    // AppliedSalesQuoteDiscount's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateAppliedSalesQuoteDiscountModelFromEntityHooksList != null) { model = CreateAppliedSalesQuoteDiscountModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
