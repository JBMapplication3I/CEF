// <copyright file="VisitorCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the visitor workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Utilities;

    public partial class VisitorWorkflow
    {
        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IVisitor entity,
            IVisitorModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateVisitorFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            if (model.Address != null)
            {
                model.Address = await Workflows.Addresses.ResolveAddressAsync(model.Address, context.ContextProfileName).ConfigureAwait(false);
            }
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            if (model.Address != null)
            {
                entity.Address!.CountryID = model.Address.CountryID;
                entity.Address.CountryCustom = model.Address.CountryCustom;
                entity.Address.RegionID = model.Address.RegionID;
                entity.Address.RegionCustom = model.Address.RegionCustom;
            }
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }
    }
}
