// <copyright file="ShipmentCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class ShipmentWorkflow
    {
        /// <inheritdoc/>
        public Task<IShipmentModel?> GetByTrackingNumberAsync(string trackingNumber, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.Shipments
                    .AsNoTracking()
                    .FilterByTrackingNumber(trackingNumber)
                    .SelectFirstListShipmentAndMapToShipmentModel(contextProfileName));
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> DeactivateByTrackingNumberAsync(
            string trackingNumber,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await DeactivateAsync(
                    await context.Shipments
                        .FilterByActive(true)
                        .FilterByTrackingNumber(trackingNumber)
                        .Select(x => x.ID)
                        .SingleAsync()
                        .ConfigureAwait(false),
                    contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IShipment entity,
            IShipmentModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            var origEstDelDate = entity.EstimatedDeliveryDate;
            var origTarShipDate = entity.TargetShippingDate;
            entity.UpdateShipmentFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            entity.EstimatedDeliveryDate = model.EstimatedDeliveryDate ?? origEstDelDate;
            entity.TargetShippingDate = model.TargetShippingDate ?? origTarShipDate;
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }
    }
}
