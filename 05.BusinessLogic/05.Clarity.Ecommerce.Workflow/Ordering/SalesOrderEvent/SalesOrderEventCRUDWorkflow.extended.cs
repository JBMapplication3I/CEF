// <copyright file="SalesOrderEventCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order event workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class SalesOrderEventWorkflow
    {
        /// <inheritdoc/>
        protected override bool OverrideDuplicateCheck { get; } = true;

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            ISalesOrderEvent entity,
            ISalesOrderEventModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await base.AssignAdditionalPropertiesAsync(entity, model, timestamp, context).ConfigureAwait(false);
            // Intentionally not setting with a relate workflow, if it fails we kill the whole process
            entity.MasterID = model.MasterID;
        }
    }
}
