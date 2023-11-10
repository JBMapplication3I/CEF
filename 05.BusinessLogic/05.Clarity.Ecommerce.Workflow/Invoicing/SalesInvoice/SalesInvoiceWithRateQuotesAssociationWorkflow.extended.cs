// <copyright file="SalesInvoiceWithRateQuotesAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate sales invoice rate quotes workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;

    public partial class SalesInvoiceWithRateQuotesAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IRateQuoteModel model,
            IRateQuote entity,
            IClarityEcommerceEntities context)
        {
            return entity.SalesOrderID == model.SalesOrderID
                && entity.PurchaseOrderID == model.PurchaseOrderID
                && entity.SalesInvoiceID == model.SalesInvoiceID
                && entity.SalesQuoteID == model.SalesQuoteID
                && entity.SalesReturnID == model.SalesReturnID
                && entity.SampleRequestID == model.SampleRequestID
                && entity.CartID == model.CartID
                && entity.ShipCarrierMethodID == model.ShipCarrierMethodID
                && entity.CartHash == model.CartHash
                && entity.Rate == model.Rate
                && entity.RateTimestamp == model.RateTimestamp
                && entity.Selected == model.Selected
                && entity.EstimatedDeliveryDate == model.EstimatedDeliveryDate;
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IRateQuote newEntity,
            IRateQuoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.UpdateRateQuoteFromModel(model, timestamp, timestamp);
            newEntity.UpdatedDate = null;
            newEntity.SalesOrderID = await Workflows.SalesOrders.ResolveToIDOptionalAsync(
                    model.SalesOrderID,
                    null,
                    null,
                    context)
                .ConfigureAwait(false);
            newEntity.PurchaseOrderID = await Workflows.PurchaseOrders.ResolveToIDOptionalAsync(
                    model.PurchaseOrderID,
                    null,
                    null,
                    context)
                .ConfigureAwait(false);
            newEntity.SalesInvoiceID = await Workflows.SalesInvoices.ResolveToIDOptionalAsync(
                    model.SalesInvoiceID,
                    null,
                    null,
                    context)
                .ConfigureAwait(false);
            newEntity.SalesQuoteID = await Workflows.SalesQuotes.ResolveToIDOptionalAsync(
                    model.SalesQuoteID,
                    null,
                    null,
                    context)
                .ConfigureAwait(false);
            newEntity.SalesReturnID = await Workflows.SalesReturns.ResolveToIDOptionalAsync(
                    model.SalesReturnID,
                    null,
                    null,
                    context)
                .ConfigureAwait(false);
            newEntity.SampleRequestID = await Workflows.SampleRequests.ResolveToIDOptionalAsync(
                    model.SampleRequestID,
                    null,
                    null,
                    context)
                .ConfigureAwait(false);
            newEntity.CartID = await Workflows.Carts.ResolveToIDOptionalAsync(
                    model.CartID,
                    null,
                    null,
                    context)
                .ConfigureAwait(false);
            newEntity.ShipCarrierMethodID = await Workflows.ShipCarrierMethods.ResolveWithAutoGenerateToIDAsync(
                    model.ShipCarrierMethodID,
                    model.ShipCarrierMethodKey,
                    model.ShipCarrierMethodName,
                    model.ShipCarrierMethod,
                    context)
                .ConfigureAwait(false);
        }
    }
}
