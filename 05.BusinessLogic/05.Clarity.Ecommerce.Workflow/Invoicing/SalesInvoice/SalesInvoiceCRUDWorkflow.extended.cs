// <copyright file="SalesInvoiceCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class SalesInvoiceWorkflow
    {
        /// <inheritdoc/>
        public override async Task<IEnumerable<ISalesInvoiceModel>> SearchForConnectAsync(
            ISalesInvoiceSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await SearchAsync(search, false, context).ConfigureAwait(false)).results;
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            ISalesInvoice entity,
            ISalesInvoiceModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Update
            UpdateEntityFromModel(entity, model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            // Associated Objects
            // AssociateSalesItems(model, entity, timestamp, contextProfileName);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }
    }
}
