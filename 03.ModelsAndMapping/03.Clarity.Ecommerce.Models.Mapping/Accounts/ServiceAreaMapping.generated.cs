// <autogenerated>
// <copyright file="Mapping.Accounts.ServiceArea.cs" company="clarity-ventures.com">
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

    public static partial class ModelMapperForServiceArea
    {
        public sealed class AnonServiceArea : ServiceArea
        {
        }

        public static readonly Func<ServiceArea?, string?, IServiceAreaModel?> MapServiceAreaModelFromEntityFull = CreateServiceAreaModelFromEntityFull;

        public static readonly Func<ServiceArea?, string?, IServiceAreaModel?> MapServiceAreaModelFromEntityLite = CreateServiceAreaModelFromEntityLite;

        public static readonly Func<ServiceArea?, string?, IServiceAreaModel?> MapServiceAreaModelFromEntityList = CreateServiceAreaModelFromEntityList;

        public static Func<IServiceArea, IServiceAreaModel, string?, IServiceAreaModel>? CreateServiceAreaModelFromEntityHooksFull { get; set; }

        public static Func<IServiceArea, IServiceAreaModel, string?, IServiceAreaModel>? CreateServiceAreaModelFromEntityHooksLite { get; set; }

        public static Func<IServiceArea, IServiceAreaModel, string?, IServiceAreaModel>? CreateServiceAreaModelFromEntityHooksList { get; set; }

        public static Expression<Func<ServiceArea, AnonServiceArea>>? PreBuiltServiceAreaSQLSelectorFull { get; set; }

        public static Expression<Func<ServiceArea, AnonServiceArea>>? PreBuiltServiceAreaSQLSelectorLite { get; set; }

        public static Expression<Func<ServiceArea, AnonServiceArea>>? PreBuiltServiceAreaSQLSelectorList { get; set; }

        /// <summary>An <see cref="IServiceAreaModel"/> extension method that creates a(n) <see cref="ServiceArea"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="ServiceArea"/> entity.</returns>
        public static IServiceArea CreateServiceAreaEntity(
            this IServiceAreaModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IServiceAreaModel, ServiceArea>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateServiceAreaFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IServiceAreaModel"/> extension method that updates a(n) <see cref="ServiceArea"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="ServiceArea"/> entity.</returns>
        public static IServiceArea UpdateServiceAreaFromModel(
            this IServiceArea entity,
            IServiceAreaModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // ServiceArea Properties
            entity.Radius = model.Radius;
            // ServiceArea's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenServiceAreaSQLSelectorFull()
        {
            PreBuiltServiceAreaSQLSelectorFull = x => x == null ? null! : new AnonServiceArea
            {
                Radius = x.Radius,
                ContractorID = x.ContractorID,
                Contractor = ModelMapperForContractor.PreBuiltContractorSQLSelectorList.Expand().Compile().Invoke(x.Contractor!),
                AddressID = x.AddressID,
                Address = ModelMapperForAddress.PreBuiltAddressSQLSelectorList.Expand().Compile().Invoke(x.Address!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenServiceAreaSQLSelectorLite()
        {
            PreBuiltServiceAreaSQLSelectorLite = x => x == null ? null! : new AnonServiceArea
            {
                Radius = x.Radius,
                ContractorID = x.ContractorID,
                Contractor = ModelMapperForContractor.PreBuiltContractorSQLSelectorList.Expand().Compile().Invoke(x.Contractor!),
                AddressID = x.AddressID,
                Address = ModelMapperForAddress.PreBuiltAddressSQLSelectorList.Expand().Compile().Invoke(x.Address!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenServiceAreaSQLSelectorList()
        {
            PreBuiltServiceAreaSQLSelectorList = x => x == null ? null! : new AnonServiceArea
            {
                Radius = x.Radius,
                ContractorID = x.ContractorID,
                Contractor = ModelMapperForContractor.PreBuiltContractorSQLSelectorList.Expand().Compile().Invoke(x.Contractor!), // For Flattening Properties (List)
                AddressID = x.AddressID,
                Address = ModelMapperForAddress.PreBuiltAddressSQLSelectorList.Expand().Compile().Invoke(x.Address!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IServiceAreaModel> SelectFullServiceAreaAndMapToServiceAreaModel(
            this IQueryable<ServiceArea> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltServiceAreaSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltServiceAreaSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateServiceAreaModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IServiceAreaModel> SelectLiteServiceAreaAndMapToServiceAreaModel(
            this IQueryable<ServiceArea> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltServiceAreaSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltServiceAreaSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateServiceAreaModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IServiceAreaModel> SelectListServiceAreaAndMapToServiceAreaModel(
            this IQueryable<ServiceArea> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltServiceAreaSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltServiceAreaSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateServiceAreaModelFromEntityList(x, contextProfileName))!;
        }

        public static IServiceAreaModel? SelectFirstFullServiceAreaAndMapToServiceAreaModel(
            this IQueryable<ServiceArea> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltServiceAreaSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltServiceAreaSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateServiceAreaModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IServiceAreaModel? SelectFirstListServiceAreaAndMapToServiceAreaModel(
            this IQueryable<ServiceArea> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltServiceAreaSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltServiceAreaSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateServiceAreaModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IServiceAreaModel? SelectSingleFullServiceAreaAndMapToServiceAreaModel(
            this IQueryable<ServiceArea> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltServiceAreaSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltServiceAreaSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateServiceAreaModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IServiceAreaModel? SelectSingleLiteServiceAreaAndMapToServiceAreaModel(
            this IQueryable<ServiceArea> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltServiceAreaSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltServiceAreaSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateServiceAreaModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IServiceAreaModel? SelectSingleListServiceAreaAndMapToServiceAreaModel(
            this IQueryable<ServiceArea> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltServiceAreaSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltServiceAreaSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateServiceAreaModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IServiceAreaModel> results, int totalPages, int totalCount) SelectFullServiceAreaAndMapToServiceAreaModel(
            this IQueryable<ServiceArea> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltServiceAreaSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltServiceAreaSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateServiceAreaModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IServiceAreaModel> results, int totalPages, int totalCount) SelectLiteServiceAreaAndMapToServiceAreaModel(
            this IQueryable<ServiceArea> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltServiceAreaSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltServiceAreaSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateServiceAreaModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IServiceAreaModel> results, int totalPages, int totalCount) SelectListServiceAreaAndMapToServiceAreaModel(
            this IQueryable<ServiceArea> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltServiceAreaSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltServiceAreaSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateServiceAreaModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IServiceAreaModel? CreateServiceAreaModelFromEntityFull(this IServiceArea? entity, string? contextProfileName)
        {
            return CreateServiceAreaModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IServiceAreaModel? CreateServiceAreaModelFromEntityLite(this IServiceArea? entity, string? contextProfileName)
        {
            return CreateServiceAreaModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IServiceAreaModel? CreateServiceAreaModelFromEntityList(this IServiceArea? entity, string? contextProfileName)
        {
            return CreateServiceAreaModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IServiceAreaModel? CreateServiceAreaModelFromEntity(
            this IServiceArea? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IServiceAreaModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // ServiceArea's Properties
                    // ServiceArea's Related Objects
                    model.Address = ModelMapperForAddress.CreateAddressModelFromEntityLite(entity.Address, contextProfileName);
                    model.Contractor = ModelMapperForContractor.CreateContractorModelFromEntityLite(entity.Contractor, contextProfileName);
                    // ServiceArea's Associated Objects
                    // Additional Mappings
                    if (CreateServiceAreaModelFromEntityHooksFull != null) { model = CreateServiceAreaModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // ServiceArea's Properties
                    // ServiceArea's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // ServiceArea's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateServiceAreaModelFromEntityHooksLite != null) { model = CreateServiceAreaModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // ServiceArea's Properties
                    model.Radius = entity.Radius;
                    // ServiceArea's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.AddressID = entity.AddressID;
                    model.AddressKey = entity.Address?.CustomKey;
                    model.ContractorID = entity.ContractorID;
                    model.ContractorKey = entity.Contractor?.CustomKey;
                    // ServiceArea's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateServiceAreaModelFromEntityHooksList != null) { model = CreateServiceAreaModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
