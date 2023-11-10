// <copyright file="JBMService.Invoicing.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.DataModel;
    using Clarity.Ecommerce.Mapper;
    using Ecommerce;
    using Ecommerce.Interfaces.Models;
    using Ecommerce.Models;
    using Ecommerce.Service;
    using Ecommerce.Utilities;
    using ServiceStack;

    public partial class JBMService : ClarityEcommerceServiceBase
    {
        public async Task<object?> Get(GetShipmentByID request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            return (await context.Shipments
                    .FilterByID(request.ShipmentID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false))
                .CreateShipmentModelFromEntityFull(ServiceContextProfileName);
        }

        public async Task<object?> Get(GetShipmentsBySalesGroupID request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            return (await context.Shipments
                    .FilterByActive(true)
                    .Where(x => x.SalesGroupID == request.SalesGroupID)
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => x.CreateShipmentModelFromEntityFull(ServiceContextProfileName))
                .ToList();
        }

        public async Task<object?> Post(CreateShipmentsForOrder request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            if (Contract.CheckNull(request?.Shipments, request?.Shipments) || Contract.CheckEmpty(request?.Shipments))
            {
                return CEFAR.PassingCEFAR();
            }
            foreach (var s in request!.Shipments!)
            {
                var shipmentlines = (await GetResponseAsync<ShipmentLineResponse>(
                        resource: $"{JBMConfig.JBMFusionShipmentURLExtension}/{s.Shipment}/child/unpackedShipmentLines",
                        queryParams: new Dictionary<string, string> { { "limit", "100" } },
                        overrideUrl: $"{JBMConfig.JBMFusionBaseURL}/{JBMConfig.JBMSalesAPI}")
                    .ConfigureAwait(false))?.Items;
                if (Contract.CheckNull(shipmentlines) || Contract.CheckEmpty(shipmentlines))
                {
                    continue;
                }
                var freightCostId = (await GetResponseAsync<ShipmentCosts>(
                        resource: $"{JBMConfig.JBMFusionShipmentURLExtension}/{s.Shipment}/child/shippingCosts",
                        queryParams: null,
                        overrideUrl: $"{JBMConfig.JBMFusionBaseURL}/{JBMConfig.JBMSalesAPI}"
                    ).ConfigureAwait(false))?.Items.FirstOrDefault()?.FreightCostId;
                if (Contract.CheckNull(freightCostId))
                {
                    continue;
                }
                var trackingNumber = (await GetResponseAsync<ShipmentDFFs>(
                        resource: $"{JBMConfig.JBMFusionShipmentURLExtension}/{s.Shipment}/child/shippingCosts/{freightCostId}/child/shippingCostsDFF",
                        queryParams: null,
                        overrideUrl: $"{JBMConfig.JBMFusionBaseURL}/{JBMConfig.JBMSalesAPI}"
                    ).ConfigureAwait(false))?.Items?.FirstOrDefault()?.JbmTrackingNumber;
                var salesOrderInfo = await context.SalesOrders
                    .FilterByCustomKey(shipmentlines.First().Order, true)
                    .Select(x =>
                        new
                        {
                            SalesGroupID = x.SalesGroupAsMasterID,
                            SalesOrderID = x.ID,
                            x.AccountID,
                        })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                if (Contract.CheckNull(salesOrderInfo))
                {
                    continue;
                }
                var shippingID = await context.Contacts.FilterByCustomKey(s.ShipToPartySiteId).Select(x => x.ID).ToListAsync().ConfigureAwait(false);
                // var billingID = await context.AccountContacts.FilterAccountContactsByAccountID(salesOrderInfo.AccountID).Where(x => !string.IsNullOrWhiteSpace(x.JsonAttributes) && x.JsonAttributes!.Contains("isBilling")).Select(y => y.SlaveID).FirstOrDefaultAsync().ConfigureAwait(false);
                if (Contract.CheckNull(shippingID, /*billingID,*/ salesOrderInfo?.SalesGroupID))
                {
                    continue;
                }
                var timeStamp = DateExtensions.GenDateTime;
                var shipment = new Shipment
                {
                    Active = true,
                    CreatedDate = timeStamp,
                    CustomKey = s.Shipment,
                    DestinationContactID = shippingID.FirstOrDefault(),
                    OriginContactID = JBMConfig.OriginContactID!.Value,
                    EstimatedDeliveryDate = s.UltimateDropoffDateTime,
                    SalesGroupID = salesOrderInfo!.SalesGroupID,
                    SalesOrderID = salesOrderInfo.SalesOrderID,
                    StatusID = s.ShipmentStatus == "Closed" ? 1 : 8,
                    TrackingNumber = trackingNumber,
                    TypeID = 1,
                    ShipmentLines = shipmentlines.Select(x => new ShipmentLine
                    {
                        Active = true,
                        CreatedDate = timeStamp,
                        CustomKey = $"{s.Shipment}|{x.Item}|{x.SourceRequestedQuantityUOMCode}",
                        Description = x.ItemDescription.Count() < 100 ? x.ItemDescription : $"{x.ItemDescription!.Substring(0, 96)}...",
                        ProductID = context.Products.FilterByCustomKey(x.Item, true).Select(y => y.ID).SingleOrDefault(),
                        Quantity = x.ConvertedQuantity,
                        Sku = x.Item,
                    }).ToList(),
                };
                context.Shipments.Add(shipment);
            }
            try
            {
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                return CEFAR.PassingCEFAR();
            }
            catch (Exception)
            {
                return CEFAR.FailingCEFAR();
            }
        }
    }
}