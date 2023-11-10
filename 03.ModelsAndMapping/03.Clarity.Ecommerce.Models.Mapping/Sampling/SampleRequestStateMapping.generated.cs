// <autogenerated>
// <copyright file="Mapping.Sampling.SampleRequestState.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Sampling section of the Mapping class</summary>
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

    public static partial class ModelMapperForSampleRequestState
    {
        public sealed class AnonSampleRequestState : SampleRequestState
        {
        }

        public static readonly Func<SampleRequestState?, string?, IStateModel?> MapSampleRequestStateModelFromEntityFull = CreateSampleRequestStateModelFromEntityFull;

        public static readonly Func<SampleRequestState?, string?, IStateModel?> MapSampleRequestStateModelFromEntityLite = CreateSampleRequestStateModelFromEntityLite;

        public static readonly Func<SampleRequestState?, string?, IStateModel?> MapSampleRequestStateModelFromEntityList = CreateSampleRequestStateModelFromEntityList;

        public static Func<ISampleRequestState, IStateModel, string?, IStateModel>? CreateSampleRequestStateModelFromEntityHooksFull { get; set; }

        public static Func<ISampleRequestState, IStateModel, string?, IStateModel>? CreateSampleRequestStateModelFromEntityHooksLite { get; set; }

        public static Func<ISampleRequestState, IStateModel, string?, IStateModel>? CreateSampleRequestStateModelFromEntityHooksList { get; set; }

        public static Expression<Func<SampleRequestState, AnonSampleRequestState>>? PreBuiltSampleRequestStateSQLSelectorFull { get; set; }

        public static Expression<Func<SampleRequestState, AnonSampleRequestState>>? PreBuiltSampleRequestStateSQLSelectorLite { get; set; }

        public static Expression<Func<SampleRequestState, AnonSampleRequestState>>? PreBuiltSampleRequestStateSQLSelectorList { get; set; }

        /// <summary>An <see cref="IStateModel"/> extension method that creates a(n) <see cref="SampleRequestState"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="SampleRequestState"/> entity.</returns>
        public static ISampleRequestState CreateSampleRequestStateEntity(
            this IStateModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityStateableBase<IStateModel, SampleRequestState>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateSampleRequestStateFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IStateModel"/> extension method that updates a(n) <see cref="SampleRequestState"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="SampleRequestState"/> entity.</returns>
        public static ISampleRequestState UpdateSampleRequestStateFromModel(
            this ISampleRequestState entity,
            IStateModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapStateableBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenSampleRequestStateSQLSelectorFull()
        {
            PreBuiltSampleRequestStateSQLSelectorFull = x => x == null ? null! : new AnonSampleRequestState
            {
                DisplayName = x.DisplayName,
                SortOrder = x.SortOrder,
                TranslationKey = x.TranslationKey,
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

        public static void GenSampleRequestStateSQLSelectorLite()
        {
            PreBuiltSampleRequestStateSQLSelectorLite = x => x == null ? null! : new AnonSampleRequestState
            {
                DisplayName = x.DisplayName,
                SortOrder = x.SortOrder,
                TranslationKey = x.TranslationKey,
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

        public static void GenSampleRequestStateSQLSelectorList()
        {
            PreBuiltSampleRequestStateSQLSelectorList = x => x == null ? null! : new AnonSampleRequestState
            {
                DisplayName = x.DisplayName,
                SortOrder = x.SortOrder,
                TranslationKey = x.TranslationKey,
                Name = x.Name,
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IStateModel> SelectFullSampleRequestStateAndMapToStateModel(
            this IQueryable<SampleRequestState> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSampleRequestStateSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSampleRequestStateSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSampleRequestStateModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IStateModel> SelectLiteSampleRequestStateAndMapToStateModel(
            this IQueryable<SampleRequestState> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSampleRequestStateSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSampleRequestStateSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSampleRequestStateModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IStateModel> SelectListSampleRequestStateAndMapToStateModel(
            this IQueryable<SampleRequestState> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSampleRequestStateSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltSampleRequestStateSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSampleRequestStateModelFromEntityList(x, contextProfileName))!;
        }

        public static IStateModel? SelectFirstFullSampleRequestStateAndMapToStateModel(
            this IQueryable<SampleRequestState> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSampleRequestStateSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSampleRequestStateSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSampleRequestStateModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStateModel? SelectFirstListSampleRequestStateAndMapToStateModel(
            this IQueryable<SampleRequestState> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSampleRequestStateSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSampleRequestStateSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSampleRequestStateModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IStateModel? SelectSingleFullSampleRequestStateAndMapToStateModel(
            this IQueryable<SampleRequestState> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSampleRequestStateSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSampleRequestStateSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateSampleRequestStateModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStateModel? SelectSingleLiteSampleRequestStateAndMapToStateModel(
            this IQueryable<SampleRequestState> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSampleRequestStateSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSampleRequestStateSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateSampleRequestStateModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IStateModel? SelectSingleListSampleRequestStateAndMapToStateModel(
            this IQueryable<SampleRequestState> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSampleRequestStateSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltSampleRequestStateSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateSampleRequestStateModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IStateModel> results, int totalPages, int totalCount) SelectFullSampleRequestStateAndMapToStateModel(
            this IQueryable<SampleRequestState> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSampleRequestStateSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSampleRequestStateSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateSampleRequestStateModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStateModel> results, int totalPages, int totalCount) SelectLiteSampleRequestStateAndMapToStateModel(
            this IQueryable<SampleRequestState> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSampleRequestStateSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSampleRequestStateSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateSampleRequestStateModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IStateModel> results, int totalPages, int totalCount) SelectListSampleRequestStateAndMapToStateModel(
            this IQueryable<SampleRequestState> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltSampleRequestStateSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltSampleRequestStateSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateSampleRequestStateModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IStateModel? CreateSampleRequestStateModelFromEntityFull(this ISampleRequestState? entity, string? contextProfileName)
        {
            return CreateSampleRequestStateModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IStateModel? CreateSampleRequestStateModelFromEntityLite(this ISampleRequestState? entity, string? contextProfileName)
        {
            return CreateSampleRequestStateModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IStateModel? CreateSampleRequestStateModelFromEntityList(this ISampleRequestState? entity, string? contextProfileName)
        {
            return CreateSampleRequestStateModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IStateModel? CreateSampleRequestStateModelFromEntity(
            this ISampleRequestState? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapStateableBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IStateModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // SampleRequestState's Properties
                    // SampleRequestState's Related Objects
                    // SampleRequestState's Associated Objects
                    // Additional Mappings
                    if (CreateSampleRequestStateModelFromEntityHooksFull != null) { model = CreateSampleRequestStateModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // SampleRequestState's Properties
                    // SampleRequestState's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SampleRequestState's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSampleRequestStateModelFromEntityHooksLite != null) { model = CreateSampleRequestStateModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // SampleRequestState's Properties
                    // SampleRequestState's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // SampleRequestState's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateSampleRequestStateModelFromEntityHooksList != null) { model = CreateSampleRequestStateModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
