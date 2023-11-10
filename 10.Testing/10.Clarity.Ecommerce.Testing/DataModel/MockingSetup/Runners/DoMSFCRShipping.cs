// <copyright file="DoMockingSetupForContextRunnerShipping.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner shipping class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerShippingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Packages
            if (DoAll || DoShipping || DoPackageTable)
            {
                RawPackages = new()
                {
                    await CreateADummyPackageAsync(id: 1, key: "Default", name: "Default", desc: "desc", depth: 3, height: 4, weight: 1, width: 4, depthUnitOfMeasure: "in", heightUnitOfMeasure: "in", widthUnitOfMeasure: "in", weightUnitOfMeasure: "lbs", dimensionalWeight: 0, typeID: 1).ConfigureAwait(false),
                    await CreateADummyPackageAsync(id: 2, key: "Download", name: "Download", desc: "desc", depth: 0, height: 0, weight: 0, width: 0, depthUnitOfMeasure: "in", heightUnitOfMeasure: "in", widthUnitOfMeasure: "in", weightUnitOfMeasure: "lbs", dimensionalWeight: 0, typeID: 4).ConfigureAwait(false),
                };
                await InitializeMockSetPackagesAsync(mockContext, RawPackages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Package Types
            if (DoAll || DoShipping || DoPackageTypeTable)
            {
                var index = 0;
                RawPackageTypes = new()
                {
                    await CreateADummyPackageTypeAsync(id: ++index, key: "Package", name: "Package", desc: "desc", sortOrder: 1, displayName: "Package").ConfigureAwait(false),
                    await CreateADummyPackageTypeAsync(id: ++index, key: "Master Pack", name: "Master Pack", desc: "desc", sortOrder: 2, displayName: "Master Pack").ConfigureAwait(false),
                    await CreateADummyPackageTypeAsync(id: ++index, key: "Pallet", name: "Pallet", desc: "desc", sortOrder: 3, displayName: "Pallet").ConfigureAwait(false),
                    await CreateADummyPackageTypeAsync(id: ++index, key: "Non-Physical", name: "Non-Physical", desc: "desc", sortOrder: 4, displayName: "Non-Physical").ConfigureAwait(false),
                };
                await InitializeMockSetPackageTypesAsync(mockContext, RawPackageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Rate Quotes
            if (DoAll || DoShipping || DoRateQuoteTable)
            {
                var index = 0;
                RawRateQuotes = new()
                {
                    await CreateADummyRateQuoteAsync(id: ++index, key: "RATE-QUOTE-1", name: "UPS 1 day", desc: "desc", estimatedDeliveryDate: CreatedDate.AddDays(value: 1), rate: 15.95m, shipCarrierMethodID: 1, rateTimestamp: CreatedDate, cartID: 8, cartHash: 5665665467895, selected: true).ConfigureAwait(false),
                    await CreateADummyRateQuoteAsync(id: ++index, key: "RATE-QUOTE-2", name: "UPS 3 day", desc: "desc", estimatedDeliveryDate: CreatedDate.AddDays(value: 3), rate: 05.95m, shipCarrierMethodID: 3, rateTimestamp: CreatedDate, cartID: 8, cartHash: 5665665467895).ConfigureAwait(false),
                };
                await InitializeMockSetRateQuotesAsync(mockContext, RawRateQuotes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Ship Carriers
            if (DoAll || DoShipping || DoShipCarrierTable)
            {
                var index = 0;
                RawShipCarriers = new()
                {
                    await CreateADummyShipCarrierAsync(id: ++index, key: "UPS",   name: "UPS",   desc: "desc", accountNumber: "accountNumber", authentication: "authentication", isInbound: true, isOutbound: true, pointOfContact: "point of contact", salesRep: "sales rep", username: "username").ConfigureAwait(false),
                    await CreateADummyShipCarrierAsync(id: ++index, key: "FedEx", name: "FedEx", desc: "desc", accountNumber: "accountNumber", authentication: "authentication", isInbound: true, isOutbound: true, pointOfContact: "point of contact", salesRep: "sales rep", username: "username").ConfigureAwait(false),
                    await CreateADummyShipCarrierAsync(id: ++index, key: "USPS",  name: "USPS",  desc: "desc", accountNumber: "accountNumber", authentication: "authentication", isInbound: true, isOutbound: true, pointOfContact: "point of contact", salesRep: "sales rep", username: "username").ConfigureAwait(false),
                    //CreateADummyShipCarrier(id: ++index, key: "DHL", name: "DHL", desc: "desc", accountNumber: "accountNumber", authentication: "authentication", isInbound: true, isOutbound: true, pointOfContact: "point of contact", salesRep: "sales rep", username: "username"),
                };
                await InitializeMockSetShipCarriersAsync(mockContext, RawShipCarriers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Ship Carrier Methods
            if (DoAll || DoShipping || DoShipCarrierMethodTable)
            {
                var index = 0;
                RawShipCarrierMethods = new()
                {
                    await CreateADummyShipCarrierMethodAsync(id: ++index, key: "UPS-1-DAY", name: "UPS 1 day", desc: "desc").ConfigureAwait(false),
                    await CreateADummyShipCarrierMethodAsync(id: ++index, key: "UPS-2-DAY", name: "UPS 2 day", desc: "desc").ConfigureAwait(false),
                    await CreateADummyShipCarrierMethodAsync(id: ++index, key: "UPS-3-DAY", name: "UPS 3 day", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetShipCarrierMethodsAsync(mockContext, RawShipCarrierMethods).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Shipments
            if (DoAll || DoShipping || DoShipmentTable)
            {
                var index = 0;
                RawShipments = new()
                {
                    await CreateADummyShipmentAsync(id: ++index, key: "SHIPMENT-A", destination: "Some Place", estimatedDeliveryDate: CreatedDate.AddDays(value: 3), negotiatedRate: 11.45m, publishedRate: 12.99m, reference1: "ref1", reference2: "ref2", reference3: "ref3", shipDate: CreatedDate, trackingNumber: "XYZ12346774654").ConfigureAwait(false),
                };
                await InitializeMockSetShipmentsAsync(mockContext, RawShipments).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Shipment Events
            if (DoAll || DoShipping || DoShipmentEventTable)
            {
                RawShipmentEvents = new()
                {
                    await CreateADummyShipmentEventAsync(id: 1, key: "SHIPMENT-EVENT", eventDate: CreatedDate, note: "some event").ConfigureAwait(false),
                };
                await InitializeMockSetShipmentEventsAsync(mockContext, RawShipmentEvents).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Shipment Statuses
            if (DoAll || DoShipping || DoShipmentStatusTable)
            {
                var index = 0;
                RawShipmentStatuses = new()
                {
                    await CreateADummyShipmentStatusAsync(id: ++index, key: "Delivered", name: "Delivered", desc: "desc", displayName: "Delivered").ConfigureAwait(false),
                    await CreateADummyShipmentStatusAsync(id: ++index, key: "Billing Information Received", name: "Billing Information Received", desc: "desc", displayName: "Billing Information Received").ConfigureAwait(false),
                    await CreateADummyShipmentStatusAsync(id: ++index, key: "Arrival Scan", name: "Arrival Scan", desc: "desc", displayName: "Arrival Scan").ConfigureAwait(false),
                    await CreateADummyShipmentStatusAsync(id: ++index, key: "Departure Scan", name: "Departure Scan", desc: "desc", displayName: "Departure Scan").ConfigureAwait(false),
                    await CreateADummyShipmentStatusAsync(id: ++index, key: "Investigation Requested", name: "Investigation Requested", desc: "desc", displayName: "Investigation Requested").ConfigureAwait(false),
                    await CreateADummyShipmentStatusAsync(id: ++index, key: "Severe Weather Conditions Have Delayed Delivery", name: "Severe Weather Conditions Have Delayed Delivery", desc: "desc", displayName: "Severe Weather Conditions Have Delayed Delivery").ConfigureAwait(false),
                    await CreateADummyShipmentStatusAsync(id: ++index, key: "Reported Damaged", name: "Reported Damaged", desc: "desc", displayName: "Reported Damaged").ConfigureAwait(false),
                };
                await InitializeMockSetShipmentStatusesAsync(mockContext, RawShipmentStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Shipment Types
            if (DoAll || DoShipping || DoShipmentTypeTable)
            {
                var index = 0;
                RawShipmentTypes = new()
                {
                    await CreateADummyShipmentTypeAsync(id: ++index, key: "Inbound", name: "Inbound", desc: "desc", displayName: "Inbound").ConfigureAwait(false),
                    await CreateADummyShipmentTypeAsync(id: ++index, key: "Outbound", name: "Outbound", desc: "desc", displayName: "Outbound").ConfigureAwait(false),
                };
                await InitializeMockSetShipmentTypesAsync(mockContext, RawShipmentTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
