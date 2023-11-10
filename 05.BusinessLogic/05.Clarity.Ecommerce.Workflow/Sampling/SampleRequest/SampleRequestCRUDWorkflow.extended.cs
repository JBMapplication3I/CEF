// <copyright file="SampleRequestCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample request workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Mapper;
    using Utilities;

    /// <summary>A sample request workflow.</summary>
    /// <seealso cref="ISampleRequestWorkflow"/>
    public partial class SampleRequestWorkflow
    {
        /// <inheritdoc/>
        public override Task<IEnumerable<ISampleRequestModel>> SearchForConnectAsync(
            ISampleRequestSearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.SampleRequests
                    .AsNoTracking()
                    .FilterByActive(search.Active)
                    .FilterSampleRequestsBySearchModel(search)
                    .OrderByDescending(so => so.CreatedDate)
                    .SelectLiteSampleRequestAndMapToSampleRequestModel(contextProfileName));
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            ISampleRequest entity,
            ISampleRequestModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateSampleRequestFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }
    }
}
