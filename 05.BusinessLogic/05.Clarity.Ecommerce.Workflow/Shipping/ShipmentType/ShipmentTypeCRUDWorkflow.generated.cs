// <autogenerated>
// <copyright file="ShipmentTypeWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for ShipmentType entities.</summary>
    /// <seealso cref="TypableWorkflowBase{ITypeModel, ITypeSearchModel, IShipmentType, ShipmentType}"/>
    /// <seealso cref="IShipmentTypeWorkflow"/>
    public partial class ShipmentTypeWorkflow
        : TypableWorkflowBase<ITypeModel, ITypeSearchModel, IShipmentType, ShipmentType>
            , IShipmentTypeWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<ShipmentType?, string?, ITypeModel?> MapFromConcreteFull
            => ModelMapperForShipmentType.MapShipmentTypeModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<ShipmentType>, string?, IEnumerable<ITypeModel>> SelectLiteAndMapToModel
            => ModelMapperForShipmentType.SelectLiteShipmentTypeAndMapToTypeModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<ShipmentType>, string?, IEnumerable<ITypeModel>> SelectListAndMapToModel
            => ModelMapperForShipmentType.SelectListShipmentTypeAndMapToTypeModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<ShipmentType>, string?, ITypeModel?> SelectFirstFullAndMapToModel
            => ModelMapperForShipmentType.SelectFirstFullShipmentTypeAndMapToTypeModel;

        /// <inheritdoc/>
        protected override Func<IShipmentType, ITypeModel, DateTime, DateTime?, IShipmentType> UpdateEntityFromModel
            => ModelMapperForShipmentType.UpdateShipmentTypeFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<ShipmentType>> FilterQueryByModelExtensionAsync(
            IQueryable<ShipmentType> query,
            ITypeSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterShipmentTypesBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IShipmentType entity,
            ITypeModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IShipmentType entity,
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
            IShipmentType entity,
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
            IShipmentType entity,
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
