// <autogenerated>
// <copyright file="Mapping.Advertising.Zone.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Advertising section of the Mapping class</summary>
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

    public static partial class ModelMapperForZone
    {
        public sealed class AnonZone : Zone
        {
            public new IEnumerable<AdZone>? AdZones { get; set; }
        }

        public static readonly Func<Zone?, string?, IZoneModel?> MapZoneModelFromEntityFull = CreateZoneModelFromEntityFull;

        public static readonly Func<Zone?, string?, IZoneModel?> MapZoneModelFromEntityLite = CreateZoneModelFromEntityLite;

        public static readonly Func<Zone?, string?, IZoneModel?> MapZoneModelFromEntityList = CreateZoneModelFromEntityList;

        public static Func<IZone, IZoneModel, string?, IZoneModel>? CreateZoneModelFromEntityHooksFull { get; set; }

        public static Func<IZone, IZoneModel, string?, IZoneModel>? CreateZoneModelFromEntityHooksLite { get; set; }

        public static Func<IZone, IZoneModel, string?, IZoneModel>? CreateZoneModelFromEntityHooksList { get; set; }

        public static Expression<Func<Zone, AnonZone>>? PreBuiltZoneSQLSelectorFull { get; set; }

        public static Expression<Func<Zone, AnonZone>>? PreBuiltZoneSQLSelectorLite { get; set; }

        public static Expression<Func<Zone, AnonZone>>? PreBuiltZoneSQLSelectorList { get; set; }

        /// <summary>An <see cref="IZoneModel"/> extension method that creates a(n) <see cref="Zone"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="Zone"/> entity.</returns>
        public static IZone CreateZoneEntity(
            this IZoneModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelNameableBase<IZoneModel, Zone>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateZoneFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IZoneModel"/> extension method that updates a(n) <see cref="Zone"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="Zone"/> entity.</returns>
        public static IZone UpdateZoneFromModel(
            this IZone entity,
            IZoneModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapNameableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // Zone Properties
            entity.Height = model.Height;
            entity.Width = model.Width;
            // Zone's Associated Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenZoneSQLSelectorFull()
        {
            PreBuiltZoneSQLSelectorFull = x => x == null ? null! : new AnonZone
            {
                TypeID = x.TypeID,
                Type = ModelMapperForZoneType.PreBuiltZoneTypeSQLSelectorList.Expand().Compile().Invoke(x.Type!),
                StatusID = x.StatusID,
                Status = ModelMapperForZoneStatus.PreBuiltZoneStatusSQLSelectorList.Expand().Compile().Invoke(x.Status!),
                Width = x.Width,
                Height = x.Height,
                AdZones = x.AdZones!.Where(y => y.Active).Select(ModelMapperForAdZone.PreBuiltAdZoneSQLSelectorList.Expand().Compile()).ToList(),
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

        public static void GenZoneSQLSelectorLite()
        {
            PreBuiltZoneSQLSelectorLite = x => x == null ? null! : new AnonZone
            {
                TypeID = x.TypeID,
                Type = ModelMapperForZoneType.PreBuiltZoneTypeSQLSelectorList.Expand().Compile().Invoke(x.Type!),
                StatusID = x.StatusID,
                Status = ModelMapperForZoneStatus.PreBuiltZoneStatusSQLSelectorList.Expand().Compile().Invoke(x.Status!),
                Width = x.Width,
                Height = x.Height,
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

        public static void GenZoneSQLSelectorList()
        {
            PreBuiltZoneSQLSelectorList = x => x == null ? null! : new AnonZone
            {
                TypeID = x.TypeID,
                Type = ModelMapperForZoneType.PreBuiltZoneTypeSQLSelectorList.Expand().Compile().Invoke(x.Type!), // For Flattening Properties (List)
                StatusID = x.StatusID,
                Status = ModelMapperForZoneStatus.PreBuiltZoneStatusSQLSelectorList.Expand().Compile().Invoke(x.Status!), // For Flattening Properties (List)
                Width = x.Width,
                Height = x.Height,
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IZoneModel> SelectFullZoneAndMapToZoneModel(
            this IQueryable<Zone> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltZoneSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltZoneSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateZoneModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IZoneModel> SelectLiteZoneAndMapToZoneModel(
            this IQueryable<Zone> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltZoneSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltZoneSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateZoneModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IZoneModel> SelectListZoneAndMapToZoneModel(
            this IQueryable<Zone> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltZoneSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltZoneSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateZoneModelFromEntityList(x, contextProfileName))!;
        }

        public static IZoneModel? SelectFirstFullZoneAndMapToZoneModel(
            this IQueryable<Zone> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltZoneSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltZoneSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateZoneModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IZoneModel? SelectFirstListZoneAndMapToZoneModel(
            this IQueryable<Zone> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltZoneSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltZoneSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateZoneModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IZoneModel? SelectSingleFullZoneAndMapToZoneModel(
            this IQueryable<Zone> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltZoneSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltZoneSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateZoneModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IZoneModel? SelectSingleLiteZoneAndMapToZoneModel(
            this IQueryable<Zone> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltZoneSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltZoneSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateZoneModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IZoneModel? SelectSingleListZoneAndMapToZoneModel(
            this IQueryable<Zone> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltZoneSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltZoneSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateZoneModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IZoneModel> results, int totalPages, int totalCount) SelectFullZoneAndMapToZoneModel(
            this IQueryable<Zone> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltZoneSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltZoneSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateZoneModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IZoneModel> results, int totalPages, int totalCount) SelectLiteZoneAndMapToZoneModel(
            this IQueryable<Zone> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltZoneSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltZoneSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateZoneModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IZoneModel> results, int totalPages, int totalCount) SelectListZoneAndMapToZoneModel(
            this IQueryable<Zone> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltZoneSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltZoneSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateZoneModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IZoneModel? CreateZoneModelFromEntityFull(this IZone? entity, string? contextProfileName)
        {
            return CreateZoneModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IZoneModel? CreateZoneModelFromEntityLite(this IZone? entity, string? contextProfileName)
        {
            return CreateZoneModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IZoneModel? CreateZoneModelFromEntityList(this IZone? entity, string? contextProfileName)
        {
            return CreateZoneModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IZoneModel? CreateZoneModelFromEntity(
            this IZone? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapNameableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IZoneModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // Zone's Properties
                    // Zone's Related Objects
                    // Zone's Associated Objects
                    model.AdZones = (entity is AnonZone ? ((AnonZone)entity).AdZones : entity.AdZones)?.Where(x => x.Active).Select(x => ModelMapperForAdZone.CreateAdZoneModelFromEntityList(x, contextProfileName)).ToList()!;
                    // Additional Mappings
                    if (CreateZoneModelFromEntityHooksFull != null) { model = CreateZoneModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // Zone's Properties
                    // IHaveATypeBase Properties (Forced)
                    model.Type = ModelMapperForZoneType.CreateZoneTypeModelFromEntityLite(entity.Type, contextProfileName);
                    // IHaveAStatusBase Properties (Forced)
                    model.Status = ModelMapperForZoneStatus.CreateZoneStatusModelFromEntityLite(entity.Status, contextProfileName);
                    // Zone's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // Zone's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateZoneModelFromEntityHooksLite != null) { model = CreateZoneModelFromEntityHooksLite(entity, model, contextProfileName); }
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
                    // Zone's Properties
                    model.Height = entity.Height;
                    model.Width = entity.Width;
                    // Zone's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // Zone's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateZoneModelFromEntityHooksList != null) { model = CreateZoneModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
