// <autogenerated>
// <copyright file="CategoryTypeWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for CategoryType entities.</summary>
    /// <seealso cref="TypableWorkflowBase{ITypeModel, ITypeSearchModel, ICategoryType, CategoryType}"/>
    /// <seealso cref="ICategoryTypeWorkflow"/>
    public partial class CategoryTypeWorkflow
        : TypableWorkflowBase<ITypeModel, ITypeSearchModel, ICategoryType, CategoryType>
            , ICategoryTypeWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<CategoryType?, string?, ITypeModel?> MapFromConcreteFull
            => ModelMapperForCategoryType.MapCategoryTypeModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<CategoryType>, string?, IEnumerable<ITypeModel>> SelectLiteAndMapToModel
            => ModelMapperForCategoryType.SelectLiteCategoryTypeAndMapToTypeModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<CategoryType>, string?, IEnumerable<ITypeModel>> SelectListAndMapToModel
            => ModelMapperForCategoryType.SelectListCategoryTypeAndMapToTypeModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<CategoryType>, string?, ITypeModel?> SelectFirstFullAndMapToModel
            => ModelMapperForCategoryType.SelectFirstFullCategoryTypeAndMapToTypeModel;

        /// <inheritdoc/>
        protected override Func<ICategoryType, ITypeModel, DateTime, DateTime?, ICategoryType> UpdateEntityFromModel
            => ModelMapperForCategoryType.UpdateCategoryTypeFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<CategoryType>> FilterQueryByModelExtensionAsync(
            IQueryable<CategoryType> query,
            ITypeSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterCategoryTypesBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            ICategoryType entity,
            ITypeModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            ICategoryType entity,
            ITypeModel model,
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
            ICategoryType entity,
            ITypeModel model,
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
            ICategoryType entity,
            ITypeModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // None to process
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
