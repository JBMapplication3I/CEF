// <copyright file="SampleRequestContactCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample request contact workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Utilities;

    public partial class SampleRequestContactWorkflow
    {
        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            ISampleRequestContact entity,
            ISampleRequestContactModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateSampleRequestContactFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            await model.Contact.AssignPrePropertiesToContactAndAddressAsync(Workflows.Addresses, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            entity.Contact.AssignPostPropertiesToContactAndAddress(model.Contact, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null, context.ContextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<SampleRequestContact>> FilterQueryByModelCustomAsync(
            IQueryable<SampleRequestContact> query,
            ISampleRequestContactSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterContactsByFirstName(search.ContactFirstName)
                .FilterContactsByLastName(search.ContactLastName);
        }
    }
}
