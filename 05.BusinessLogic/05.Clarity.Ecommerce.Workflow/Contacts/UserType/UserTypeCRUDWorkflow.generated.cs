// <autogenerated>
// <copyright file="UserTypeWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A workflow for UserType entities.</summary>
    /// <seealso cref="TypableWorkflowBase{ITypeModel, ITypeSearchModel, IUserType, UserType}"/>
    /// <seealso cref="IUserTypeWorkflow"/>
    public partial class UserTypeWorkflow
        : TypableWorkflowBase<ITypeModel, ITypeSearchModel, IUserType, UserType>
            , IUserTypeWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<UserType?, string?, ITypeModel?> MapFromConcreteFull
            => ModelMapperForUserType.MapUserTypeModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<UserType>, string?, IEnumerable<ITypeModel>> SelectLiteAndMapToModel
            => ModelMapperForUserType.SelectLiteUserTypeAndMapToTypeModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<UserType>, string?, IEnumerable<ITypeModel>> SelectListAndMapToModel
            => ModelMapperForUserType.SelectListUserTypeAndMapToTypeModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<UserType>, string?, ITypeModel?> SelectFirstFullAndMapToModel
            => ModelMapperForUserType.SelectFirstFullUserTypeAndMapToTypeModel;

        /// <inheritdoc/>
        protected override Func<IUserType, ITypeModel, DateTime, DateTime?, IUserType> UpdateEntityFromModel
            => ModelMapperForUserType.UpdateUserTypeFromModel;
        #endregion

        /// <inheritdoc/>
        protected override async Task<IQueryable<UserType>> FilterQueryByModelExtensionAsync(
            IQueryable<UserType> query,
            ITypeSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterUserTypesBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IUserType entity,
            ITypeModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IUserType entity,
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
            IUserType entity,
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
            IUserType entity,
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
