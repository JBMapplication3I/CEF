// <autogenerated>
// <copyright file="EmailStatusWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for EmailStatus entities.</summary>
    /// <seealso cref="StatusableWorkflowBase{IStatusModel, IStatusSearchModel, IEmailStatus, EmailStatus}"/>
    /// <seealso cref="IEmailStatusWorkflow"/>
    public partial class EmailStatusWorkflow
        : StatusableWorkflowBase<IStatusModel, IStatusSearchModel, IEmailStatus, EmailStatus>
            , IEmailStatusWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<EmailStatus?, string?, IStatusModel?> MapFromConcreteFull
            => ModelMapperForEmailStatus.MapEmailStatusModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<EmailStatus>, string?, IEnumerable<IStatusModel>> SelectLiteAndMapToModel
            => ModelMapperForEmailStatus.SelectLiteEmailStatusAndMapToStatusModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<EmailStatus>, string?, IEnumerable<IStatusModel>> SelectListAndMapToModel
            => ModelMapperForEmailStatus.SelectListEmailStatusAndMapToStatusModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<EmailStatus>, string?, IStatusModel?> SelectFirstFullAndMapToModel
            => ModelMapperForEmailStatus.SelectFirstFullEmailStatusAndMapToStatusModel;

        /// <inheritdoc/>
        protected override Func<IEmailStatus, IStatusModel, DateTime, DateTime?, IEmailStatus> UpdateEntityFromModel
            => ModelMapperForEmailStatus.UpdateEmailStatusFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<EmailStatus>> FilterQueryByModelExtensionAsync(
            IQueryable<EmailStatus> query,
            IStatusSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterEmailStatusesBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IEmailStatus entity,
            IStatusModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IEmailStatus entity,
            IStatusModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            // None to process
        }

        #region Relate Workflows
        /// <inheritdoc/>
        protected override async Task RunDefaultRelateWorkflowsAsync(
            IEmailStatus entity,
            IStatusModel model,
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
            IEmailStatus entity,
            IStatusModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // None to process
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
