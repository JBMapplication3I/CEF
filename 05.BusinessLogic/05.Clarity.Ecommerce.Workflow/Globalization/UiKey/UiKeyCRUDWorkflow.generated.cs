// <autogenerated>
// <copyright file="UiKeyWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for UiKey entities.</summary>
    /// <seealso cref="WorkflowBase{IUiKeyModel, IUiKeySearchModel, IUiKey, UiKey}"/>
    /// <seealso cref="IUiKeyWorkflow"/>
    public partial class UiKeyWorkflow
        : WorkflowBase<IUiKeyModel, IUiKeySearchModel, IUiKey, UiKey>
            , IUiKeyWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<UiKey?, string?, IUiKeyModel?> MapFromConcreteFull
            => ModelMapperForUiKey.MapUiKeyModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<UiKey>, string?, IEnumerable<IUiKeyModel>> SelectLiteAndMapToModel
            => ModelMapperForUiKey.SelectLiteUiKeyAndMapToUiKeyModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<UiKey>, string?, IEnumerable<IUiKeyModel>> SelectListAndMapToModel
            => ModelMapperForUiKey.SelectListUiKeyAndMapToUiKeyModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<UiKey>, string?, IUiKeyModel?> SelectFirstFullAndMapToModel
            => ModelMapperForUiKey.SelectFirstFullUiKeyAndMapToUiKeyModel;

        /// <inheritdoc/>
        protected override Func<IUiKey, IUiKeyModel, DateTime, DateTime?, IUiKey> UpdateEntityFromModel
            => ModelMapperForUiKey.UpdateUiKeyFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<UiKey>> FilterQueryByModelExtensionAsync(
            IQueryable<UiKey> query,
            IUiKeySearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterUiKeysBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IUiKey entity,
            IUiKeyModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IUiKey entity,
            IUiKeyModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            // Skipped: Not supposed to map this property in via this manner: UiTranslations
        }

        #region Relate Workflows
        /// <inheritdoc/>
        protected override async Task RunDefaultRelateWorkflowsAsync(
            IUiKey entity,
            IUiKeyModel model,
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
            IUiKey entity,
            IUiKeyModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // None to process
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
