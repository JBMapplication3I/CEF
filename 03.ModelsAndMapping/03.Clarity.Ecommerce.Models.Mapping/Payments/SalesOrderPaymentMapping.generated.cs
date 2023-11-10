// <autogenerated>
// <copyright file="Mapping.Payments.SalesOrderPayment.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForSalesOrderPayment
    {
        public sealed class AnonSalesOrderPayment : SalesOrderPayment
        {
            // public new SalesOrder? Master { get; set; }
        }

        public static readonly Func<SalesOrderPayment?, string?, ISalesOrderPaymentModel?> MapSalesOrderPaymentModelFromEntityFull = CreateSalesOrderPaymentModelFromEntityFull;

        public static readonly Func<SalesOrderPayment?, string?, ISalesOrderPaymentModel?> MapSalesOrderPaymentModelFromEntityLite = CreateSalesOrderPaymentModelFromEntityLite;

        public static readonly Func<SalesOrderPayment?, string?, ISalesOrderPaymentModel?> MapSalesOrderPaymentModelFromEntityList = CreateSalesOrderPaymentModelFromEntityList;

        public static Func<ISalesOrderPayment, ISalesOrderPaymentModel, string?, ISalesOrderPaymentModel>? CreateSalesOrderPaymentModelFromEntityHooksFull { get; set; }

        public static Func<ISalesOrderPayment, ISalesOrderPaymentModel, string?, ISalesOrderPaymentModel>? CreateSalesOrderPaymentModelFromEntityHooksLite { get; set; }

        public static Func<ISalesOrderPayment, ISalesOrderPaymentModel, string?, ISalesOrderPaymentModel>? CreateSalesOrderPaymentModelFromEntityHooksList { get; set; }

        public static Expression<Func<SalesOrderPayment, AnonSalesOrderPayment>>? PreBuiltSalesOrderPaymentSQLSelectorFull { get; set; }

        public static Expression<Func<SalesOrderPayment, AnonSalesOrderPayment>>? PreBuiltSalesOrderPaymentSQLSelectorLite { get; set; }

        public static Expression<Func<SalesOrderPayment, AnonSalesOrderPayment>>? PreBuiltSalesOrderPaymentSQLSelectorList { get; set; }

        /// <summary>An <see cref="ISalesOrderPaymentModel"/> extension method that creates a(n) <see cref="SalesOrderPayment"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="SalesOrderPayment"/> entity.</returns>
        public static ISalesOrderPayment CreateSalesOrderPaymentEntity(
            this ISalesOrderPaymentModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<ISalesOrderPaymentModel, SalesOrderPayment>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateSalesOrderPaymentFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="ISalesOrderPaymentModel"/> extension method that updates a(n) <see cref="SalesOrderPayment"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="SalesOrderPayment"/> entity.</returns>
        public static ISalesOrderPayment UpdateSalesOrderPaymentFromModel(
            this ISalesOrderPayment entity,
            ISalesOrderPaymentModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapIAmARelationshipTableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // SalesOrderPayment's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenSalesOrderPaymentSQLSelectorFull()
        {
            PreBuiltSalesOrderPaymentSQLSelectorFull = x => x == null ? null! : new AnonSalesOrderPayment
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForPayment.PreBuiltPaymentSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenSalesOrderPaymentSQLSelectorLite()
        {
            PreBuiltSalesOrderPaymentSQLSelectorLite = x => x == null ? null! : new AnonSalesOrderPayment
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForPayment.PreBuiltPaymentSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenSalesOrderPaymentSQLSelectorList()
        {
            PreBuiltSalesOrderPaymentSQLSelectorList = x => x == null ? null! : new AnonSalesOrderPayment
            {
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForPayment.PreBuiltPaymentSQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
                Master = ModelMapperForSalesOrder.PreBuiltSalesOrderSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties
            };
        }

        public static IEnumerable<ISalesOrderPaymentModel> SelectFullSalesOrderPaymentAndMapToSalesOrderPaymentModel(
            this IQueryable<SalesOrderPayment> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderPaymentSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderPaymentSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderPaymentModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<ISalesOrderPaymentModel> SelectLiteSalesOrderPaymentAndMapToSalesOrderPaymentModel(
            this IQueryable<SalesOrderPayment> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderPaymentSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderPaymentSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderPaymentModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<ISalesOrderPaymentModel> SelectListSalesOrderPaymentAndMapToSalesOrderPaymentModel(
            this IQueryable<SalesOrderPayment> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderPaymentSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderPaymentSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderPaymentModelFromEntityList(x, contextProfileName))!;
        }

        public static ISalesOrderPaymentModel? SelectFirstFullSalesOrderPaymentAndMapToSalesOrderPaymentModel(
            this IQueryable<SalesOrderPayment> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderPaymentSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderPaymentSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderPaymentModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ISalesOrderPaymentModel? SelectFirstListSalesOrderPaymentAndMapToSalesOrderPaymentModel(
            this IQueryable<SalesOrderPayment> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderPaymentSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderPaymentSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderPaymentModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ISalesOrderPaymentModel? SelectSingleFullSalesOrderPaymentAndMapToSalesOrderPaymentModel(
            this IQueryable<SalesOrderPayment> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderPaymentSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderPaymentSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderPaymentModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ISalesOrderPaymentModel? SelectSingleLiteSalesOrderPaymentAndMapToSalesOrderPaymentModel(
            this IQueryable<SalesOrderPayment> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderPaymentSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderPaymentSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderPaymentModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ISalesOrderPaymentModel? SelectSingleListSalesOrderPaymentAndMapToSalesOrderPaymentModel(
            this IQueryable<SalesOrderPayment> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderPaymentSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderPaymentSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderPaymentModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<ISalesOrderPaymentModel> results, int totalPages, int totalCount) SelectFullSalesOrderPaymentAndMapToSalesOrderPaymentModel(
            this IQueryable<SalesOrderPayment> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderPaymentSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderPaymentSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderPaymentModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ISalesOrderPaymentModel> results, int totalPages, int totalCount) SelectLiteSalesOrderPaymentAndMapToSalesOrderPaymentModel(
            this IQueryable<SalesOrderPayment> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderPaymentSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderPaymentSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderPaymentModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ISalesOrderPaymentModel> results, int totalPages, int totalCount) SelectListSalesOrderPaymentAndMapToSalesOrderPaymentModel(
            this IQueryable<SalesOrderPayment> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderPaymentSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderPaymentSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderPaymentModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static ISalesOrderPaymentModel? CreateSalesOrderPaymentModelFromEntityFull(this ISalesOrderPayment? entity, string? contextProfileName)
        {
            return CreateSalesOrderPaymentModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static ISalesOrderPaymentModel? CreateSalesOrderPaymentModelFromEntityLite(this ISalesOrderPayment? entity, string? contextProfileName)
        {
            return CreateSalesOrderPaymentModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static ISalesOrderPaymentModel? CreateSalesOrderPaymentModelFromEntityList(this ISalesOrderPayment? entity, string? contextProfileName)
        {
            return CreateSalesOrderPaymentModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static ISalesOrderPaymentModel? CreateSalesOrderPaymentModelFromEntity(
            this ISalesOrderPayment? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<ISalesOrderPaymentModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // SalesOrderPayment's Properties
                    // SalesOrderPayment's Related Objects
                    // SalesOrderPayment's Associated Objects
                    // Additional Mappings
                    if (CreateSalesOrderPaymentModelFromEntityHooksFull != null) { model = CreateSalesOrderPaymentModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // SalesOrderPayment's Properties
                    // SalesOrderPayment's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SalesOrderPayment's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesOrderPaymentModelFromEntityHooksLite != null) { model = CreateSalesOrderPaymentModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // SalesOrderPayment's Properties
                    // SalesOrderPayment's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.SlaveID = entity.SlaveID;
                    model.Slave = ModelMapperForPayment.CreatePaymentModelFromEntityLite(entity.Slave, contextProfileName);
                    model.SlaveKey = entity.Slave?.CustomKey;
                    // SalesOrderPayment's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesOrderPaymentModelFromEntityHooksList != null) { model = CreateSalesOrderPaymentModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
