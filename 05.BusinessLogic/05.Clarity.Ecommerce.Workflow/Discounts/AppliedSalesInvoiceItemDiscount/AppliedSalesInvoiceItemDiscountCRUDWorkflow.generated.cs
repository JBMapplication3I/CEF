// <autogenerated>
// <copyright file="AppliedSalesInvoiceItemDiscountWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for AppliedSalesInvoiceItemDiscount entities.</summary>
    /// <seealso cref="WorkflowBase{IAppliedSalesInvoiceItemDiscountModel, IAppliedSalesInvoiceItemDiscountSearchModel, IAppliedSalesInvoiceItemDiscount, AppliedSalesInvoiceItemDiscount}"/>
    /// <seealso cref="IAppliedSalesInvoiceItemDiscountWorkflow"/>
    public partial class AppliedSalesInvoiceItemDiscountWorkflow
        : WorkflowBase<IAppliedSalesInvoiceItemDiscountModel, IAppliedSalesInvoiceItemDiscountSearchModel, IAppliedSalesInvoiceItemDiscount, AppliedSalesInvoiceItemDiscount>
            , IAppliedSalesInvoiceItemDiscountWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<AppliedSalesInvoiceItemDiscount?, string?, IAppliedSalesInvoiceItemDiscountModel?> MapFromConcreteFull
            => ModelMapperForAppliedSalesInvoiceItemDiscount.MapAppliedSalesInvoiceItemDiscountModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<AppliedSalesInvoiceItemDiscount>, string?, IEnumerable<IAppliedSalesInvoiceItemDiscountModel>> SelectLiteAndMapToModel
            => ModelMapperForAppliedSalesInvoiceItemDiscount.SelectLiteAppliedSalesInvoiceItemDiscountAndMapToAppliedSalesInvoiceItemDiscountModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<AppliedSalesInvoiceItemDiscount>, string?, IEnumerable<IAppliedSalesInvoiceItemDiscountModel>> SelectListAndMapToModel
            => ModelMapperForAppliedSalesInvoiceItemDiscount.SelectListAppliedSalesInvoiceItemDiscountAndMapToAppliedSalesInvoiceItemDiscountModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<AppliedSalesInvoiceItemDiscount>, string?, IAppliedSalesInvoiceItemDiscountModel?> SelectFirstFullAndMapToModel
            => ModelMapperForAppliedSalesInvoiceItemDiscount.SelectFirstFullAppliedSalesInvoiceItemDiscountAndMapToAppliedSalesInvoiceItemDiscountModel;

        /// <inheritdoc/>
        protected override Func<IAppliedSalesInvoiceItemDiscount, IAppliedSalesInvoiceItemDiscountModel, DateTime, DateTime?, IAppliedSalesInvoiceItemDiscount> UpdateEntityFromModel
            => ModelMapperForAppliedSalesInvoiceItemDiscount.UpdateAppliedSalesInvoiceItemDiscountFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<AppliedSalesInvoiceItemDiscount>> FilterQueryByModelExtensionAsync(
            IQueryable<AppliedSalesInvoiceItemDiscount> query,
            IAppliedSalesInvoiceItemDiscountSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterAppliedSalesInvoiceItemDiscountsBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IAppliedSalesInvoiceItemDiscount entity,
            IAppliedSalesInvoiceItemDiscountModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IAppliedSalesInvoiceItemDiscount entity,
            IAppliedSalesInvoiceItemDiscountModel model,
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
            IAppliedSalesInvoiceItemDiscount entity,
            IAppliedSalesInvoiceItemDiscountModel model,
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
            IAppliedSalesInvoiceItemDiscount entity,
            IAppliedSalesInvoiceItemDiscountModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // None to process
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
