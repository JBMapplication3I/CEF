// <copyright file="CartWithRateQuotesAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate cart rate quotes workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class CartWithRateQuotesAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IRateQuoteModel model,
            IRateQuote entity,
            IClarityEcommerceEntities context)
        {
            return entity.ShipCarrierMethodID == model.ShipCarrierMethodID
                /*&& entity.CartID == model.CartID
                && entity.PurchaseOrderID == model.PurchaseOrderID
                && entity.SampleRequestID == model.SampleRequestID
                && entity.SalesQuoteID == model.SalesQuoteID
                && entity.SalesOrderID == model.SalesOrderID
                && entity.SalesInvoiceID == model.SalesInvoiceID
                && entity.SalesReturnID == model.SalesReturnID*/
                && entity.CartHash == model.CartHash
                && entity.EstimatedDeliveryDate == model.EstimatedDeliveryDate
                && entity.TargetShippingDate == model.TargetShippingDate
                && entity.Rate == model.Rate
                && entity.RateTimestamp == model.RateTimestamp
                && entity.Selected == model.Selected
                && entity.Name == model.Name
                && entity.Description == model.Description
                && entity.JsonAttributes == model.SerializableAttributes.SerializeAttributesDictionary();
        }

        /// <inheritdoc/>
        protected override Task ModelToNewObjectAdditionalPropertiesAsync(
            IRateQuote newEntity,
            IRateQuoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            return ResolveShipCarrierMethodAsync(newEntity, model, context.ContextProfileName);
        }

        private async Task ResolveShipCarrierMethodAsync(
            IRateQuote newEntity,
            IRateQuoteModel model,
            string? contextProfileName)
        {
            if (Contract.CheckValidIDOrAnyValidKey(
                model.ShipCarrierMethod?.ID ?? model.ShipCarrierMethodID,
                model.ShipCarrierMethodKey,
                model.ShipCarrierMethodName,
                model.ShipCarrierMethod?.CustomKey,
                model.ShipCarrierMethod?.Name))
            {
                newEntity.ShipCarrierMethodID = await Workflows.ShipCarrierMethods.ResolveWithAutoGenerateToIDAsync(
                        model.ShipCarrierMethodID,
                        model.ShipCarrierMethodKey,
                        model.ShipCarrierMethodName,
                        model.ShipCarrierMethod,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
        }
    }
}
