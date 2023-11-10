// <autogenerated>
// <copyright file="Mapping.Questionnaire.QuestionOption.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Questionnaire section of the Mapping class</summary>
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

    public static partial class ModelMapperForQuestionOption
    {
        public sealed class AnonQuestionOption : QuestionOption
        {
        }

        public static readonly Func<QuestionOption?, string?, IQuestionOptionModel?> MapQuestionOptionModelFromEntityFull = CreateQuestionOptionModelFromEntityFull;

        public static readonly Func<QuestionOption?, string?, IQuestionOptionModel?> MapQuestionOptionModelFromEntityLite = CreateQuestionOptionModelFromEntityLite;

        public static readonly Func<QuestionOption?, string?, IQuestionOptionModel?> MapQuestionOptionModelFromEntityList = CreateQuestionOptionModelFromEntityList;

        public static Func<IQuestionOption, IQuestionOptionModel, string?, IQuestionOptionModel>? CreateQuestionOptionModelFromEntityHooksFull { get; set; }

        public static Func<IQuestionOption, IQuestionOptionModel, string?, IQuestionOptionModel>? CreateQuestionOptionModelFromEntityHooksLite { get; set; }

        public static Func<IQuestionOption, IQuestionOptionModel, string?, IQuestionOptionModel>? CreateQuestionOptionModelFromEntityHooksList { get; set; }

        public static Expression<Func<QuestionOption, AnonQuestionOption>>? PreBuiltQuestionOptionSQLSelectorFull { get; set; }

        public static Expression<Func<QuestionOption, AnonQuestionOption>>? PreBuiltQuestionOptionSQLSelectorLite { get; set; }

        public static Expression<Func<QuestionOption, AnonQuestionOption>>? PreBuiltQuestionOptionSQLSelectorList { get; set; }

        /// <summary>An <see cref="IQuestionOptionModel"/> extension method that creates a(n) <see cref="QuestionOption"/> entity.</summary>
        /// <param name="model">            The model to act on.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new <see cref="QuestionOption"/> entity.</returns>
        public static IQuestionOption CreateQuestionOptionEntity(
            this IQuestionOptionModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Create the object and Map the Inherited Properties
            return Contract.RequiresNotNull(model)
                .CreateEntityFromModelBase<IQuestionOptionModel, QuestionOption>(timestamp, contextProfileName)
                // Use the Update method to map non-inherited properties
                .UpdateQuestionOptionFromModel(model, timestamp, null);
        }

        /// <summary>An <see cref="IQuestionOptionModel"/> extension method that updates a(n) <see cref="QuestionOption"/> entity.</summary>
        /// <param name="entity">         The entity to act on.</param>
        /// <param name="model">          The model to read from.</param>
        /// <param name="timestamp">      The timestamp Date/Time.</param>
        /// <param name="updateTimestamp">The update timestamp Date/Time.</param>
        /// <returns>The updated <see cref="QuestionOption"/> entity.</returns>
        public static IQuestionOption UpdateQuestionOptionFromModel(
            this IQuestionOption entity,
            IQuestionOptionModel model,
            DateTime timestamp,
            DateTime? updateTimestamp)
        {
            // Map the Inherited Properties
            entity = Contract.RequiresNotNull(entity)
                .MapBaseModelPropertiesToEntity(Contract.RequiresNotNull(model));
            // QuestionOption Properties
            entity.OptionTranslationKey = model.OptionTranslationKey;
            entity.RequiresAdditionalInformation = model.RequiresAdditionalInformation;
            // QuestionOption's Related Objects
            // QuestionOption's Associated Objects
            // Finally, update the timestamp
            entity.UpdatedDate = updateTimestamp == null || updateTimestamp == DateTime.MinValue ? null : updateTimestamp;
            // Return
            return entity;
        }

        public static void GenQuestionOptionSQLSelectorFull()
        {
            PreBuiltQuestionOptionSQLSelectorFull = x => x == null ? null! : new AnonQuestionOption
            {
                OptionTranslationKey = x.OptionTranslationKey,
                RequiresAdditionalInformation = x.RequiresAdditionalInformation,
                QuestionID = x.QuestionID,
                Question = ModelMapperForQuestion.PreBuiltQuestionSQLSelectorList.Expand().Compile().Invoke(x.Question!),
                FollowUpQuestionID = x.FollowUpQuestionID,
                FollowUpQuestion = ModelMapperForQuestion.PreBuiltQuestionSQLSelectorList.Expand().Compile().Invoke(x.FollowUpQuestion!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenQuestionOptionSQLSelectorLite()
        {
            PreBuiltQuestionOptionSQLSelectorLite = x => x == null ? null! : new AnonQuestionOption
            {
                OptionTranslationKey = x.OptionTranslationKey,
                RequiresAdditionalInformation = x.RequiresAdditionalInformation,
                QuestionID = x.QuestionID,
                Question = ModelMapperForQuestion.PreBuiltQuestionSQLSelectorList.Expand().Compile().Invoke(x.Question!),
                FollowUpQuestionID = x.FollowUpQuestionID,
                FollowUpQuestion = ModelMapperForQuestion.PreBuiltQuestionSQLSelectorList.Expand().Compile().Invoke(x.FollowUpQuestion!),
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static void GenQuestionOptionSQLSelectorList()
        {
            PreBuiltQuestionOptionSQLSelectorList = x => x == null ? null! : new AnonQuestionOption
            {
                OptionTranslationKey = x.OptionTranslationKey,
                RequiresAdditionalInformation = x.RequiresAdditionalInformation,
                QuestionID = x.QuestionID,
                Question = ModelMapperForQuestion.PreBuiltQuestionSQLSelectorList.Expand().Compile().Invoke(x.Question!), // For Flattening Properties (List)
                FollowUpQuestionID = x.FollowUpQuestionID,
                FollowUpQuestion = ModelMapperForQuestion.PreBuiltQuestionSQLSelectorList.Expand().Compile().Invoke(x.FollowUpQuestion!), // For Flattening Properties (List)
                ID = x.ID,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                Active = x.Active,
                Hash = x.Hash,
                JsonAttributes = x.JsonAttributes,
            };
        }

        public static IEnumerable<IQuestionOptionModel> SelectFullQuestionOptionAndMapToQuestionOptionModel(
            this IQueryable<QuestionOption> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltQuestionOptionSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltQuestionOptionSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateQuestionOptionModelFromEntityFull(x, contextProfileName))!;
        }

        public static IEnumerable<IQuestionOptionModel> SelectLiteQuestionOptionAndMapToQuestionOptionModel(
            this IQueryable<QuestionOption> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltQuestionOptionSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltQuestionOptionSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateQuestionOptionModelFromEntityLite(x, contextProfileName))!;
        }

        public static IEnumerable<IQuestionOptionModel> SelectListQuestionOptionAndMapToQuestionOptionModel(
            this IQueryable<QuestionOption> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltQuestionOptionSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .Select(PreBuiltQuestionOptionSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateQuestionOptionModelFromEntityList(x, contextProfileName))!;
        }

        public static IQuestionOptionModel? SelectFirstFullQuestionOptionAndMapToQuestionOptionModel(
            this IQueryable<QuestionOption> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltQuestionOptionSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltQuestionOptionSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateQuestionOptionModelFromEntityFull(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IQuestionOptionModel? SelectFirstListQuestionOptionAndMapToQuestionOptionModel(
            this IQueryable<QuestionOption> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltQuestionOptionSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltQuestionOptionSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateQuestionOptionModelFromEntityList(x, contextProfileName))
                .FirstOrDefault();
        }

        public static IQuestionOptionModel? SelectSingleFullQuestionOptionAndMapToQuestionOptionModel(
            this IQueryable<QuestionOption> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltQuestionOptionSQLSelectorFull == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltQuestionOptionSQLSelectorFull!.Compile())
                .ToList()
                .Select(x => CreateQuestionOptionModelFromEntityFull(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IQuestionOptionModel? SelectSingleLiteQuestionOptionAndMapToQuestionOptionModel(
            this IQueryable<QuestionOption> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltQuestionOptionSQLSelectorLite == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltQuestionOptionSQLSelectorLite!.Compile())
                .ToList()
                .Select(x => CreateQuestionOptionModelFromEntityLite(x, contextProfileName))
                .SingleOrDefault();
        }

        public static IQuestionOptionModel? SelectSingleListQuestionOptionAndMapToQuestionOptionModel(
            this IQueryable<QuestionOption> dbSetWithFilters,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltQuestionOptionSQLSelectorList == null);
            return dbSetWithFilters
                .AsExpandable()
                .OrderBy(x => x.ID)
                .Take(1)
                .Select(PreBuiltQuestionOptionSQLSelectorList!.Compile())
                .ToList()
                .Select(x => CreateQuestionOptionModelFromEntityList(x, contextProfileName))
                .SingleOrDefault();
        }

        public static (IEnumerable<IQuestionOptionModel> results, int totalPages, int totalCount) SelectFullQuestionOptionAndMapToQuestionOptionModel(
            this IQueryable<QuestionOption> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltQuestionOptionSQLSelectorFull == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltQuestionOptionSQLSelectorFull!.Compile())
                    .ToList()
                    .Select(x => CreateQuestionOptionModelFromEntityFull(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IQuestionOptionModel> results, int totalPages, int totalCount) SelectLiteQuestionOptionAndMapToQuestionOptionModel(
            this IQueryable<QuestionOption> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltQuestionOptionSQLSelectorLite == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltQuestionOptionSQLSelectorLite!.Compile())
                    .ToList()
                    .Select(x => CreateQuestionOptionModelFromEntityLite(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static (IEnumerable<IQuestionOptionModel> results, int totalPages, int totalCount) SelectListQuestionOptionAndMapToQuestionOptionModel(
            this IQueryable<QuestionOption> dbSetWithFilters,
            Paging? paging,
            Sort[]? sorts,
            Grouping[]? groupings,
            string? contextProfileName)
        {
            BaseModelMapper.Initialize(PreBuiltQuestionOptionSQLSelectorList == null);
            return (dbSetWithFilters
                    .AsExpandable()
                    .ApplySorting(sorts, groupings, contextProfileName)
                    .FilterByPaging(paging, out var totalPages, out var totalCount)
                    .Select(PreBuiltQuestionOptionSQLSelectorList!.Compile())
                    .ToList()
                    .Select(x => CreateQuestionOptionModelFromEntityList(x, contextProfileName)),
                totalPages,
                totalCount)!;
        }

        public static IQuestionOptionModel? CreateQuestionOptionModelFromEntityFull(this IQuestionOption? entity, string? contextProfileName)
        {
            return CreateQuestionOptionModelFromEntity(entity, MappingMode.Full, contextProfileName);
        }

        public static IQuestionOptionModel? CreateQuestionOptionModelFromEntityLite(this IQuestionOption? entity, string? contextProfileName)
        {
            return CreateQuestionOptionModelFromEntity(entity, MappingMode.Lite, contextProfileName);
        }

        public static IQuestionOptionModel? CreateQuestionOptionModelFromEntityList(this IQuestionOption? entity, string? contextProfileName)
        {
            return CreateQuestionOptionModelFromEntity(entity, MappingMode.List, contextProfileName);
        }

        public static IQuestionOptionModel? CreateQuestionOptionModelFromEntity(
            this IQuestionOption? entity,
            MappingMode mode,
            string? contextProfileName)
        {
            if (entity == null) { return null; }
            // Map the Inherited Properties
            // ReSharper disable once InvokeAsExtensionMethod
            var model = BaseModelMapper.MapBaseEntityPropertiesToModel(
                RegistryLoaderWrapper.GetInstance<IQuestionOptionModel>(contextProfileName),
                Contract.RequiresNotNull(entity),
                mode,
                contextProfileName);
            // Map this level's Properties
            switch (mode)
            {
                case MappingMode.Full: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // QuestionOption's Properties
                    // QuestionOption's Related Objects
                    model.FollowUpQuestion = ModelMapperForQuestion.CreateQuestionModelFromEntityLite(entity.FollowUpQuestion, contextProfileName);
                    model.Question = ModelMapperForQuestion.CreateQuestionModelFromEntityLite(entity.Question, contextProfileName);
                    // QuestionOption's Associated Objects
                    // Additional Mappings
                    if (CreateQuestionOptionModelFromEntityHooksFull != null) { model = CreateQuestionOptionModelFromEntityHooksFull(entity, model, contextProfileName); }
                    goto case MappingMode.Lite;
                }
                case MappingMode.Lite: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    // QuestionOption's Properties
                    // QuestionOption's Related Objects (Not Mapped unless Forced, or a flattening property)
                    // QuestionOption's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateQuestionOptionModelFromEntityHooksLite != null) { model = CreateQuestionOptionModelFromEntityHooksLite(entity, model, contextProfileName); }
                    goto case MappingMode.List;
                }
                case MappingMode.List: //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                {
                    // IHaveJsonAttributesBase Properties (Forced)
                    model.SerializableAttributes = entity.JsonAttributes.DeserializeAttributesDictionary();
                    // QuestionOption's Properties
                    model.OptionTranslationKey = entity.OptionTranslationKey;
                    model.RequiresAdditionalInformation = entity.RequiresAdditionalInformation;
                    // QuestionOption's Related Objects (Not Mapped unless Forced, or a flattening property)
                    model.FollowUpQuestionID = entity.FollowUpQuestionID;
                    model.FollowUpQuestionKey = entity.FollowUpQuestion?.CustomKey;
                    model.QuestionID = entity.QuestionID;
                    model.QuestionKey = entity.Question?.CustomKey;
                    // QuestionOption's Associated Objects (Not Mapped unless Forced)
                    // Additional Mappings
                    if (CreateQuestionOptionModelFromEntityHooksList != null) { model = CreateQuestionOptionModelFromEntityHooksList(entity, model, contextProfileName); }
                    break;
                }
            } ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finished!
            return model;
        }
    }
}
