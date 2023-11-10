// <copyright file="PageViewCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the page view workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Utilities;

    public partial class PageViewWorkflow
    {
        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IPageView entity,
            IPageViewModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdatePageViewFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            model.Address = await model.Address.AssignPrePropertiesToAddressAsync(Workflows.Addresses, context.ContextProfileName).ConfigureAwait(false);
            await model.Contact.AssignPrePropertiesToContactAndAddressAsync(Workflows.Addresses, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            entity.Address.AssignPostPropertiesToAddress(model.Address, timestamp);
            entity.Contact.AssignPostPropertiesToContactAndAddress(model.Contact, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null, context.ContextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }
    }
}
