// <copyright file="RateQuoteCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the rate quote workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class RateQuoteWorkflow
    {
        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IRateQuote entity,
            IRateQuoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await base.AssignAdditionalPropertiesAsync(entity, model, timestamp, context).ConfigureAwait(false);
            if (entity.CartID != model.CartID)
            {
                entity.CartID = model.CartID;
            }
            if (entity.SalesOrderID != model.SalesOrderID)
            {
                entity.SalesOrderID = model.SalesOrderID;
            }
            if (entity.PurchaseOrderID != model.PurchaseOrderID)
            {
                entity.PurchaseOrderID = model.PurchaseOrderID;
            }
            if (entity.SalesInvoiceID != model.SalesInvoiceID)
            {
                entity.SalesInvoiceID = model.SalesInvoiceID;
            }
            if (entity.SalesQuoteID != model.SalesQuoteID)
            {
                entity.SalesQuoteID = model.SalesQuoteID;
            }
            if (entity.SampleRequestID != model.SampleRequestID)
            {
                entity.SampleRequestID = model.SampleRequestID;
            }
        }
    }
}
