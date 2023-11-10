// <copyright file="VisitCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the visit workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Utilities;

    public partial class VisitWorkflow
    {
        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IVisit entity,
            IVisitModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateVisitFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            model.Address = await model.Address.AssignPrePropertiesToAddressAsync(Workflows.Addresses, context.ContextProfileName).ConfigureAwait(false);
            await model.Contact.AssignPrePropertiesToContactAndAddressAsync(Workflows.Addresses, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            entity.Address.AssignPostPropertiesToAddress(model.Address, timestamp);
            entity.Contact.AssignPostPropertiesToContactAndAddress(model.Contact, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null, context.ContextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            if (entity.ID > 0)
            {
                entity.TotalTriggers ??= 0;
                entity.TotalTriggers++;
                entity.IsFirstTrigger = false;
            }
            else
            {
                entity.IsFirstTrigger = true;
                entity.TotalTriggers = (entity.TotalTriggers ?? 0) + 1;
            }
        }
    }
}
