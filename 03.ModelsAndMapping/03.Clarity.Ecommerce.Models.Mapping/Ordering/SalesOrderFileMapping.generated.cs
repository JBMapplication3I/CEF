// <autogenerated>
// <copyright file="Mapping.Ordering.SalesOrderFile.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Ordering section of the Mapping class</summary>
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

    public static partial class ModelMapperForSalesOrderFile
    {
        public sealed class AnonSalesOrderFile : SalesOrderFile
        {
            // public new SalesOrder? Master { get; set; }
        }

        public static readonly Func<SalesOrderFile?, string?, ISalesOrderFileModel?> MapSalesOrderFileModelFromEntityFull = CreateSalesOrderFileModelFromEntityFull;

        public static readonly Func<SalesOrderFile?, string?, ISalesOrderFileModel?> MapSalesOrderFileModelFromEntityLite = CreateSalesOrderFileModelFromEntityLite;

        public static readonly Func<SalesOrderFile?, string?, ISalesOrderFileModel?> MapSalesOrderFileModelFromEntityList = CreateSalesOrderFileModelFromEntityList;

        public static Func<ISalesOrderFile, ISalesOrderFileModel, string?, ISalesOrderFileModel>? CreateSalesOrderFileModelFromEntityHooksFull { get; set; }

        public static Func<ISalesOrderFile, ISalesOrderFileModel, string?, ISalesOrderFileModel>? CreateSalesOrderFileModelFromEntityHooksLite { get; set; }

        public static Func<ISalesOrderFile, ISalesOrderFileModel, string?, ISalesOrderFileModel>? CreateSalesOrderFileModelFromEntityHooksList { get; set; }

        public static Expression<Func<SalesOrderFile, AnonSalesOrderFile>>? PreBuiltSalesOrderFileSQLSelectorFull { get; set; }

        public static Expression<Func<SalesOrderFile, AnonSalesOrderFile>>? PreBuiltSalesOrderFileSQLSelectorLite { get; set; }

        public static Expression<Func<SalesOrderFile, AnonSalesOrderFile>>? PreBuiltSalesOrderFileSQLSelectorList { get; set; }

        /// <summary>An <see cref="ISalesOrderFileModel"/> extension method that creates a(n) <see cref="SalesOrderFile"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="SalesOrderFile"/> entity.</returns>
        public static ISalesOrderFile CreateSalesOrderFileEntity(
            this ISalesOrderFileModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelNameableBase<ISalesOrderFileModel, SalesOrderFile>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateSalesOrderFileFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="ISalesOrderFileModel"/> extension method that updates a(n) <see cref="SalesOrderFile"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="SalesOrderFile"/> entity.</returns>
        public static ISalesOrderFile UpdateSalesOrderFileFromModel(
            this ISalesOrderFile entity,
            ISalesOrderFileModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapNameableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // SalesOrderFile Properties
            entity.FileAccessTypeID = model.FileAccessTypeID;
            entity.SeoDescription = model.SeoDescription;
            entity.SeoKeywords = model.SeoKeywords;
            entity.SeoMetaData = model.SeoMetaData;
            entity.SeoPageTitle = model.SeoPageTitle;
            entity.SeoUrl = model.SeoUrl;
            entity.SortOrder = model.SortOrder;
            // SalesOrderFile's Related Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenSalesOrderFileSQLSelectorFull()
        {
            PreBuiltSalesOrderFileSQLSelectorFull = x => x == null ? null! : new AnonSalesOrderFile
            {
                SeoUrl = x.SeoUrl,
                SeoKeywords = x.SeoKeywords,
                SeoPageTitle = x.SeoPageTitle,
                SeoDescription = x.SeoDescription,
                SeoMetaData = x.SeoMetaData,
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForStoredFile.PreBuiltStoredFileSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                FileAccessTypeID = x.FileAccessTypeID,
                SortOrder = x.SortOrder,
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

        public static void GenSalesOrderFileSQLSelectorLite()
        {
            PreBuiltSalesOrderFileSQLSelectorLite = x => x == null ? null! : new AnonSalesOrderFile
            {
                SeoUrl = x.SeoUrl,
                SeoKeywords = x.SeoKeywords,
                SeoPageTitle = x.SeoPageTitle,
                SeoDescription = x.SeoDescription,
                SeoMetaData = x.SeoMetaData,
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForStoredFile.PreBuiltStoredFileSQLSelectorList.Expand().Compile().Invoke(x.Slave!),
                FileAccessTypeID = x.FileAccessTypeID,
                SortOrder = x.SortOrder,
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

        public static void GenSalesOrderFileSQLSelectorList()
        {
            PreBuiltSalesOrderFileSQLSelectorList = x => x == null ? null! : new AnonSalesOrderFile
            {
                SeoUrl = x.SeoUrl,
                MasterID = x.MasterID,
                SlaveID = x.SlaveID,
                Slave = ModelMapperForStoredFile.PreBuiltStoredFileSQLSelectorList.Expand().Compile().Invoke(x.Slave!), // For Flattening Properties (List)
                FileAccessTypeID = x.FileAccessTypeID,
                SortOrder = x.SortOrder,
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
                Master = ModelMapperForSalesOrder.PreBuiltSalesOrderSQLSelectorList.Expand().Compile().Invoke(x.Master!), // For Flattening Properties
            };
        }

        public static IEnumerable<ISalesOrderFileModel> SelectFullSalesOrderFileAndMapToSalesOrderFileModel(
            this IQueryable<SalesOrderFile> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderFileSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderFileSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderFileModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<ISalesOrderFileModel> SelectLiteSalesOrderFileAndMapToSalesOrderFileModel(
            this IQueryable<SalesOrderFile> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderFileSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderFileSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderFileModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<ISalesOrderFileModel> SelectListSalesOrderFileAndMapToSalesOrderFileModel(
            this IQueryable<SalesOrderFile> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderFileSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSalesOrderFileSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderFileModelFromEntityList(x, contextProfileName))!;
        }

        public static ISalesOrderFileModel? SelectFirstFullSalesOrderFileAndMapToSalesOrderFileModel(
            this IQueryable<SalesOrderFile> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderFileSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderFileSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderFileModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ISalesOrderFileModel? SelectFirstListSalesOrderFileAndMapToSalesOrderFileModel(
            this IQueryable<SalesOrderFile> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderFileSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderFileSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderFileModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static ISalesOrderFileModel? SelectSingleFullSalesOrderFileAndMapToSalesOrderFileModel(
            this IQueryable<SalesOrderFile> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderFileSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderFileSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderFileModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ISalesOrderFileModel? SelectSingleLiteSalesOrderFileAndMapToSalesOrderFileModel(
            this IQueryable<SalesOrderFile> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderFileSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderFileSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderFileModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static ISalesOrderFileModel? SelectSingleListSalesOrderFileAndMapToSalesOrderFileModel(
            this IQueryable<SalesOrderFile> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderFileSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSalesOrderFileSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSalesOrderFileModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<ISalesOrderFileModel> results, int totalPages, int totalCount) SelectFullSalesOrderFileAndMapToSalesOrderFileModel(
            this IQueryable<SalesOrderFile> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderFileSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderFileSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderFileModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ISalesOrderFileModel> results, int totalPages, int totalCount) SelectLiteSalesOrderFileAndMapToSalesOrderFileModel(
            this IQueryable<SalesOrderFile> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderFileSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderFileSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderFileModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<ISalesOrderFileModel> results, int totalPages, int totalCount) SelectListSalesOrderFileAndMapToSalesOrderFileModel(
            this IQueryable<SalesOrderFile> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSalesOrderFileSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSalesOrderFileSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateSalesOrderFileModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static ISalesOrderFileModel? CreateSalesOrderFileModelFromEntityFull(this ISalesOrderFile? entity, string? contextProfileName)
        {
            return CreateSalesOrderFileModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static ISalesOrderFileModel? CreateSalesOrderFileModelFromEntityLite(this ISalesOrderFile? entity, string? contextProfileName)
        {
            return CreateSalesOrderFileModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static ISalesOrderFileModel? CreateSalesOrderFileModelFromEntityList(this ISalesOrderFile? entity, string? contextProfileName)
        {
            return CreateSalesOrderFileModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static ISalesOrderFileModel? CreateSalesOrderFileModelFromEntity(
            this ISalesOrderFile? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapNameableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<ISalesOrderFileModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // SalesOrderFile's Properties
                    // SalesOrderFile's Related Objects
                    // SalesOrderFile's Associated Objects
                    // Additional Mappings
                    if (CreateSalesOrderFileModelFromEntityHooksFull != null) { model = CreateSalesOrderFileModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // SalesOrderFile's Properties
                    model.SeoDescription = entity.SeoDescription;
                    model.SeoKeywords = entity.SeoKeywords;
                    model.SeoMetaData = entity.SeoMetaData;
                    model.SeoPageTitle = entity.SeoPageTitle;
                    // SalesOrderFile's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SalesOrderFile's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesOrderFileModelFromEntityHooksLite != null) { model = CreateSalesOrderFileModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // SalesOrderFile's Properties
                    model.FileAccessTypeID = entity.FileAccessTypeID;
                    model.SeoUrl = entity.SeoUrl;
                    model.SortOrder = entity.SortOrder;
                    // SalesOrderFile's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.MasterID = entity.MasterID;
                    model.MasterKey = entity.Master?.CustomKey;
                    model.SlaveID = entity.SlaveID;
                    model.Slave = ModelMapperForStoredFile.CreateStoredFileModelFromEntityLite(entity.Slave, contextProfileName);
                    model.SlaveKey = entity.Slave?.CustomKey;
                    model.SlaveName = entity.Slave?.Name;
                    // SalesOrderFile's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSalesOrderFileModelFromEntityHooksList != null) { model = CreateSalesOrderFileModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
