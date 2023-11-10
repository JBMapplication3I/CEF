// <autogenerated>
// <copyright file="LanguageWorkflow.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Workflow generated to provide base setups</summary>
// <remarks>This file was auto-generated by Workflows.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
#nullable enable
// ReSharper disable ConvertToUsingDeclaration, InvertIf, ReturnValueOfPureMethodIsNotUsed, UnusedMember.Local
#pragma warning disable CS0618,CS1711,CS1572,CS1580,CS1581,CS1584
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Mapper;
    using Utilities;

    /// <summary>A workflow for Language entities.</summary>
    /// <seealso cref="WorkflowBase{ILanguageModel, ILanguageSearchModel, ILanguage, Language}"/>
    /// <seealso cref="ILanguageWorkflow"/>
    public partial class LanguageWorkflow
        : WorkflowBase<ILanguageModel, ILanguageSearchModel, ILanguage, Language>
            , ILanguageWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<Language?, string?, ILanguageModel?> MapFromConcreteFull
            => ModelMapperForLanguage.MapLanguageModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<Language>, string?, IEnumerable<ILanguageModel>> SelectLiteAndMapToModel
            => ModelMapperForLanguage.SelectLiteLanguageAndMapToLanguageModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<Language>, string?, IEnumerable<ILanguageModel>> SelectListAndMapToModel
            => ModelMapperForLanguage.SelectListLanguageAndMapToLanguageModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<Language>, string?, ILanguageModel?> SelectFirstFullAndMapToModel
            => ModelMapperForLanguage.SelectFirstFullLanguageAndMapToLanguageModel;

        /// <inheritdoc/>
        protected override Func<ILanguage, ILanguageModel, DateTime, DateTime?, ILanguage> UpdateEntityFromModel
            => ModelMapperForLanguage.UpdateLanguageFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<Language>> FilterQueryByModelExtensionAsync(
            IQueryable<Language> query,
            ILanguageSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterLanguagesBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            ILanguage entity,
            ILanguageModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            ILanguage entity,
            ILanguageModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            if (model.Images != null) { await Workflows.LanguageWithImagesAssociation.AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false); }
        }

        #region Relate Workflows
        /// <inheritdoc/>
        protected override async Task RunDefaultRelateWorkflowsAsync(
            ILanguage entity,
            ILanguageModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        // ReSharper disable AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
#pragma warning disable 1998
        protected override async Task RunDefaultRelateWorkflowsAsync(
#pragma warning restore 1998
            ILanguage entity,
            ILanguageModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // None to process
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
