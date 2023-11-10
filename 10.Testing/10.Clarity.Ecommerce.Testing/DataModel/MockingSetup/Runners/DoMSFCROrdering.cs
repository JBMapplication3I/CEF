// <copyright file="DoMockingSetupForContextRunnerOrdering.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner ordering class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerOrderingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Sales Orders
            if (DoAll || DoOrdering || DoSalesOrderTable)
            {
                RawSalesOrders = new()
                {
                    await CreateADummySalesOrderAsync(id: 00001, key: "WEB-16-1",     subtotalItems: 322.05m, subtotalShipping: 30.15m, subtotalTaxes: 26.57m, subtotalFees: 2.00m, subtotalHandling: 04.00m, subtotalDiscounts: 00.00m, total: 369.77m, shippingSameAsBilling: true, accountID: 1, userID: 1, statusID: 01, balanceDue: 0m, stateID: 1, billingContactID: 1, shippingContactID: 1, salesGroupAsMasterID: 0001, jsonAttributes: "{}").ConfigureAwait(false),
                    await CreateADummySalesOrderAsync(id: 30000, key: "WEB-16-30000", subtotalItems: 322.05m, subtotalShipping: 30.15m, subtotalTaxes: 26.57m, subtotalFees: 2.00m, subtotalHandling: 04.00m, subtotalDiscounts: 00.00m, total: 369.77m, shippingSameAsBilling: true, accountID: 1, userID: 1, statusID: 01, balanceDue: 0m, stateID: 1, billingContactID: 1, shippingContactID: 1, jsonAttributes: "{}").ConfigureAwait(false),
                    await CreateADummySalesOrderAsync(id: 30001, key: "WEB-16-30001", subtotalItems: 024.95m, subtotalShipping: 03.05m, subtotalTaxes: 02.00m, subtotalFees: 0.49m, subtotalHandling: 03.47m, subtotalDiscounts: 01.00m, total: 032.96m, shippingSameAsBilling: true, accountID: 1, userID: 1, statusID: 10, balanceDue: 0m, stateID: 2, billingContactID: 1, shippingContactID: 1, jsonAttributes: "{}").ConfigureAwait(false),
                    await CreateADummySalesOrderAsync(id: 30002, key: "WEB-16-30002", subtotalItems: 024.95m, subtotalShipping: 03.05m, subtotalTaxes: 02.00m, subtotalFees: 0.49m, subtotalHandling: 03.47m, subtotalDiscounts: 01.00m, total: 032.96m, shippingSameAsBilling: true, accountID: 2, userID: 2, statusID: 07, balanceDue: 0m, stateID: 1, billingContactID: 1, shippingContactID: 1, jsonAttributes: "{}").ConfigureAwait(false),
                    await CreateADummySalesOrderAsync(id: 30003, key: "WEB-16-30003", subtotalItems: 024.95m, subtotalShipping: 03.05m, subtotalTaxes: 02.00m, subtotalFees: 0.49m, subtotalHandling: 03.47m, subtotalDiscounts: 01.00m, total: 032.96m, shippingSameAsBilling: true, accountID: 2, userID: 2, statusID: 07, balanceDue: 0m, stateID: 1, billingContactID: 1, shippingContactID: 1, jsonAttributes: "{}").ConfigureAwait(false),
                    await CreateADummySalesOrderAsync(id: 30004, key: "WEB-16-30004", subtotalItems: 024.95m, subtotalShipping: 03.05m, subtotalTaxes: 02.00m, subtotalFees: 0.49m, subtotalHandling: 03.47m, subtotalDiscounts: 01.00m, total: 032.96m, shippingSameAsBilling: true, accountID: 1, userID: 1, statusID: 07, balanceDue: 0m, stateID: 1, billingContactID: 1, shippingContactID: 1, jsonAttributes: "{}").ConfigureAwait(false),
                    await CreateADummySalesOrderAsync(id: 30005, key: "WEB-18-30005", subtotalItems: 024.95m, subtotalShipping: 03.05m, subtotalTaxes: 02.00m, subtotalFees: 0.49m, subtotalHandling: 03.47m, subtotalDiscounts: 01.00m, total: 032.96m, shippingSameAsBilling: true, accountID: 1, userID: 1, statusID: 01, balanceDue: 0m, stateID: 1, billingContactID: 1, shippingContactID: 1,
                        jsonAttributes: new SerializableAttributesDictionary { ["Payoneer-Order-ID"] = new SerializableAttributeObject { ID = 1, Key = "Payoneer-Order-ID", Value = "12345678910123" } }.SerializeAttributesDictionary()).ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrdersAsync(mockContext, RawSalesOrders).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Order Contacts
            if (DoAll || DoOrdering || DoSalesOrderContactTable)
            {
                RawSalesOrderContacts = new()
                {
                    await CreateADummySalesOrderContactAsync(id: 1, key: "SALES-ORDER-CONTACT-1").ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrderContactsAsync(mockContext, RawSalesOrderContacts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Order Events
            if (DoAll || DoOrdering || DoSalesOrderEventTable)
            {
                var index = 0;
                RawSalesOrderEvents = new()
                {
                    await CreateADummySalesOrderEventAsync(id: ++index, key: "Event-1", name: "Event 1", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrderEventsAsync(mockContext, RawSalesOrderEvents).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Order Event Types
            if (DoAll || DoOrdering || DoSalesOrderEventTypeTable)
            {
                var index = 0;
                RawSalesOrderEventTypes = new()
                {
                    await CreateADummySalesOrderEventTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrderEventTypesAsync(mockContext, RawSalesOrderEventTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Order Files (New)
            if (DoAll || DoOrdering || DoSalesOrderFileTable)
            {
                RawSalesOrderFiles = new()
                {
                    await CreateADummySalesOrderFileAsync(id: 1, key: "SALES-ORDER-FILE-NEW-1", name: "Sales Order File New 1", desc: "desc", fileAccessTypeID: 0).ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrderFilesAsync(mockContext, RawSalesOrderFiles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Order Items
            if (DoAll || DoOrdering || DoSalesOrderItemTable)
            {
                var index = 0;
                RawSalesOrderItems = new()
                {
                    await CreateADummySalesOrderItemAsync(id: (00001 * 10) + ++index, key: "WEB-16-1|0200-ANGBT-BFUT-IA",          name: "200ml Ang Futura", desc: "desc", quantity: 1, unitCorePrice: 13.05m,        unitSoldPrice: 13.05m,        sku: "0200-ANGBT-BFUT-IA",       unitOfMeasure: "EACH", productID: 1151, masterID: 00001).ConfigureAwait(false),
                    await CreateADummySalesOrderItemAsync(id: (00001 * 10) + ++index, key: "WEB-16-1|0200-ANGRF-OPERA-VSI-IA",     name: "200ml Opera",      desc: "desc", quantity: 1, unitCorePrice: 13.05m,        unitSoldPrice: 13.05m,        sku: "0200-ANGRF-OPERA-VSI-IA",  unitOfMeasure: "EACH", productID: 1152, masterID: 00001).ConfigureAwait(false),
                    await CreateADummySalesOrderItemAsync(id: (30000 * 10) + ++index, key: "WEB-16-30000|0200-ANGBT-BFUT-IA",      name: "200ml Ang Futura", desc: "desc", quantity: 1, unitCorePrice: 13.05m,        unitSoldPrice: 13.05m,        sku: "0200-ANGBT-BFUT-IA",       unitOfMeasure: "EACH", productID: 1151, masterID: 30000).ConfigureAwait(false),
                    await CreateADummySalesOrderItemAsync(id: (30000 * 10) + ++index, key: "WEB-16-30000|0200-ANGRF-OPERA-VSI-IA", name: "200ml Opera",      desc: "desc", quantity: 1, unitCorePrice: 13.05m,        unitSoldPrice: 13.05m,        sku: "0200-ANGRF-OPERA-VSI-IA",  unitOfMeasure: "EACH", productID: 1152, masterID: 30000).ConfigureAwait(false),
                    await CreateADummySalesOrderItemAsync(id: (30000 * 10) + ++index, key: "WEB-16-30000|ZORK-BLACK",              name: "Black Zorks",      desc: "desc", quantity: 1, unitCorePrice: 0.19m * 1400m, unitSoldPrice: 0.19m * 1400m, sku: "ZORK-BLACK",               unitOfMeasure: "CS",   productID: 0969, masterID: 30000).ConfigureAwait(false),
                    await CreateADummySalesOrderItemAsync(id: (30000 * 10) + ++index, key: "WEB-16-30000|7403F-XL--DC-Bag",        name: "A Bag",            desc: "desc", quantity: 1, unitCorePrice: 29.95m,        unitSoldPrice: 29.95m,        sku: "7403F-XL--DC-Bag",         unitOfMeasure: "EACH", productID: 0969, masterID: 30000).ConfigureAwait(false),
                    await CreateADummySalesOrderItemAsync(id: (30001 * 10) + ++index, key: "WEB-16-30001|7403F-XL--DC-Bag",        name: "A Bag",            desc: "desc", quantity: 1, unitCorePrice: 29.95m,        unitSoldPrice: 24.95m,        sku: "7403F-XL--DC-Bag",         unitOfMeasure: "EACH", productID: 0969, masterID: 30001).ConfigureAwait(false),
                    await CreateADummySalesOrderItemAsync(id: (30001 * 10) + ++index, key: "WEB-16-30001|0200-ANGBT-BFUT-IA",      name: "200ml Ang Futura", desc: "desc", quantity: 1, unitCorePrice: 13.05m,        unitSoldPrice: 13.05m,        sku: "0200-ANGBT-BFUT-IA",       unitOfMeasure: "EACH", productID: 1151, masterID: 30001).ConfigureAwait(false),
                    await CreateADummySalesOrderItemAsync(id: (30002 * 10) + ++index, key: "WEB-16-30002|7403F-XL--DC-Bag",        name: "A Bag",            desc: "desc", quantity: 1, unitCorePrice: 29.95m,        unitSoldPrice: 24.95m,        sku: "7403F-XL--DC-Bag",         unitOfMeasure: "EACH", productID: 0969, masterID: 30002).ConfigureAwait(false),
                    await CreateADummySalesOrderItemAsync(id: (30003 * 10) + ++index, key: "WEB-16-30003|7403F-XL--DC-Bag",        name: "A Bag",            desc: "desc", quantity: 1, unitCorePrice: 29.95m,        unitSoldPrice: 24.95m,        sku: "7403F-XL--DC-Bag",         unitOfMeasure: "EACH", productID: 0969, masterID: 30003).ConfigureAwait(false),
                    await CreateADummySalesOrderItemAsync(id: (30003 * 10) + ++index, key: "WEB-16-30003|0200-ANGBT-BFUT-IA",      name: "200ml Ang Futura", desc: "desc", quantity: 1, unitCorePrice: 13.05m,        unitSoldPrice: 13.05m,        sku: "0200-ANGBT-BFUT-IA",       unitOfMeasure: "EACH", productID: 1151, masterID: 30003).ConfigureAwait(false),
                    await CreateADummySalesOrderItemAsync(id: (30004 * 10) + ++index, key: "WEB-16-30004|ZORK-BLUE-C",             name: "BLUE ZORK C",      desc: "desc", quantity: 8, unitCorePrice: 29.95m,        unitSoldPrice: 24.95m,        sku: "ZORK-BLUE-C",              unitOfMeasure: "EACH", productID: 0972, masterID: 30004).ConfigureAwait(false),
                    await CreateADummySalesOrderItemAsync(id: (30005 * 10) + ++index, key: "WEB-18-30005|ZORK-BLUE-C",             name: "BLUE ZORK C",      desc: "desc", quantity: 8, unitCorePrice: 29.95m,        unitSoldPrice: 24.95m,        sku: "ZORK-BLUE-C",              unitOfMeasure: "EACH", productID: 0972, masterID: 30004).ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrderItemsAsync(mockContext, RawSalesOrderItems).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Order Item Targets
            if (DoAll || DoOrdering || DoSalesOrderItemTargetTable)
            {
                RawSalesOrderItemTargets = new()
                {
                    await CreateADummySalesOrderItemTargetAsync(id: 1, key: "SALES-ORDER-ITEM-TARGET-1", quantity: 0).ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrderItemTargetsAsync(mockContext, RawSalesOrderItemTargets).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Order States
            if (DoAll || DoOrdering || DoSalesOrderStateTable)
            {
                var index = 0;
                RawSalesOrderStates = new()
                {
                    await CreateADummySalesOrderStateAsync(id: ++index, key: "WORK", name: "Work", desc: "desc", sortOrder: 1, displayName: "Work").ConfigureAwait(false),
                    await CreateADummySalesOrderStateAsync(id: ++index, key: "HISTORY", name: "History", desc: "desc", sortOrder: 2, displayName: "History").ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrderStatesAsync(mockContext, RawSalesOrderStates).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Order Statuses
            if (DoAll || DoOrdering || DoSalesOrderStatusTable)
            {
                var index = 0;
                RawSalesOrderStatuses = new()
                {
                    await CreateADummySalesOrderStatusAsync(id: ++index, key: "Pending", name: "Pending", desc: "desc", sortOrder: 1, displayName: "Pending").ConfigureAwait(false), // Submitted
                    await CreateADummySalesOrderStatusAsync(id: ++index, key: "Confirmed", name: "Confirmed", desc: "desc", sortOrder: 2, displayName: "Confirmed").ConfigureAwait(false),
                    await CreateADummySalesOrderStatusAsync(id: ++index, key: "Backordered", name: "Backordered", desc: "desc", sortOrder: 3, displayName: "Backordered").ConfigureAwait(false),
                    await CreateADummySalesOrderStatusAsync(id: ++index, key: "Split", name: "Split", desc: "desc", sortOrder: 4, displayName: "Split").ConfigureAwait(false), // Closed
                    await CreateADummySalesOrderStatusAsync(id: ++index, key: "Partial Payment Received", name: "Partial Payment Received", desc: "desc", sortOrder: 5, displayName: "Partial Payment Received").ConfigureAwait(false),
                    await CreateADummySalesOrderStatusAsync(id: ++index, key: "Full Payment Received", name: "Full Payment Received", desc: "desc", sortOrder: 6, displayName: "Full Payment Received").ConfigureAwait(false),
                    await CreateADummySalesOrderStatusAsync(id: ++index, key: "Processing", name: "Processing", desc: "desc", sortOrder: 7, displayName: "Processing").ConfigureAwait(false), // Create Pick ticket
                    await CreateADummySalesOrderStatusAsync(id: ++index, key: "Shipped from Vendor", name: "Shipped from Vendor", desc: "desc", sortOrder: 8, displayName: "Shipped from Vendor").ConfigureAwait(false), // Drop Ship
                    await CreateADummySalesOrderStatusAsync(id: ++index, key: "Shipped", name: "Shipped", desc: "desc", sortOrder: 9, displayName: "Shipped").ConfigureAwait(false),
                    await CreateADummySalesOrderStatusAsync(id: ++index, key: "Completed", name: "Completed", desc: "desc", sortOrder: 10, displayName: "Completed").ConfigureAwait(false),
                    await CreateADummySalesOrderStatusAsync(id: ++index, key: "Void", name: "Void", desc: "desc", sortOrder: 11, displayName: "Void").ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrderStatusesAsync(mockContext, RawSalesOrderStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Order Types
            if (DoAll || DoOrdering || DoSalesOrderTypeTable)
            {
                var index = 0;
                RawSalesOrderTypes = new()
                {
                    await CreateADummySalesOrderTypeAsync(id: ++index, key: "Web", name: "Web", desc: "desc", displayName: "Web").ConfigureAwait(false),
                    await CreateADummySalesOrderTypeAsync(id: ++index, key: "Phone", name: "Phone", desc: "desc", displayName: "Phone").ConfigureAwait(false),
                    await CreateADummySalesOrderTypeAsync(id: ++index, key: "On Site", name: "On Site", desc: "desc", displayName: "On Site").ConfigureAwait(false),
                    await CreateADummySalesOrderTypeAsync(id: ++index, key: "Sales Order Child", name: "Sales Order Child", desc: "desc", displayName: "Sales Order Child").ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrderTypesAsync(mockContext, RawSalesOrderTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
