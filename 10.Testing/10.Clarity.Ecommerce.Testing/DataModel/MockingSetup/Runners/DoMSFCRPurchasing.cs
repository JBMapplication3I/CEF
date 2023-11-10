// <copyright file="DoMockingSetupForContextRunnerPurchasingAsync.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner purchasing class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerPurchasingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Purchase Orders
            if (DoAll || DoPurchasing || DoPurchaseOrderTable)
            {
                var index = 0;
                RawPurchaseOrders = new()
                {
                    await CreateADummyPurchaseOrderAsync(id: ++index, key: "PO-1", dueDate: CreatedDate, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true, actualReceiptDate: CreatedDate, estimatedReceiptDate: CreatedDate, releaseDate: CreatedDate, trackingNumber: "XYZ1111111", salesGroupID: 0001).ConfigureAwait(false),
                    await CreateADummyPurchaseOrderAsync(id: ++index, key: "PO-2", dueDate: CreatedDate, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true, statusID: 2, actualReceiptDate: CreatedDate, estimatedReceiptDate: CreatedDate, releaseDate: CreatedDate, trackingNumber: "XYZ1111111").ConfigureAwait(false),
                };
                await InitializeMockSetPurchaseOrdersAsync(mockContext, RawPurchaseOrders).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Purchase Order Contacts
            if (DoAll || DoPurchasing || DoPurchaseOrderContactTable)
            {
                RawPurchaseOrderContacts = new()
                {
                    await CreateADummyPurchaseOrderContactAsync(id: 1, key: "PURCHASE-ORDER-CONTACT-1").ConfigureAwait(false),
                };
                await InitializeMockSetPurchaseOrderContactsAsync(mockContext, RawPurchaseOrderContacts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Purchase Order Events
            if (DoAll || DoOrdering || DoPurchaseOrderEventTable)
            {
                var index = 0;
                RawPurchaseOrderEvents = new()
                {
                    await CreateADummyPurchaseOrderEventAsync(id: ++index, key: "Event-1", name: "Event 1", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetPurchaseOrderEventsAsync(mockContext, RawPurchaseOrderEvents).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Purchase Order Event Types
            if (DoAll || DoOrdering || DoPurchaseOrderEventTypeTable)
            {
                var index = 0;
                RawPurchaseOrderEventTypes = new()
                {
                    await CreateADummyPurchaseOrderEventTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetPurchaseOrderEventTypesAsync(mockContext, RawPurchaseOrderEventTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Purchase Order Files
            if (DoAll || DoPurchasing || DoPurchaseOrderFileTable)
            {
                RawPurchaseOrderFiles = new()
                {
                    await CreateADummyPurchaseOrderFileAsync(id: 1, key: "PURCHASE-ORDER-FILE-NEW-1", name: "Purchase Order File New 1", desc: "desc", fileAccessTypeID: 0).ConfigureAwait(false),
                };
                await InitializeMockSetPurchaseOrderFilesAsync(mockContext, RawPurchaseOrderFiles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Purchase Order Items
            if (DoAll || DoPurchasing || DoPurchaseOrderItemTable)
            {
                var index = 0;
                RawPurchaseOrderItems = new()
                {
                    // fresh order, nothing received yet.
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-1|SKU-1", name: "name", desc: "description", quantity: 050, quantityBackOrdered: 0, unitCorePrice: 10.0M, unitSoldPrice: 10.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1151, dateReceived: CreatedDate).ConfigureAwait(false),
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-1|SKU-2", name: "name", desc: "different vendor", quantity: 075, quantityBackOrdered: 0, unitCorePrice: 15.0M, unitSoldPrice: 15.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1152, dateReceived: CreatedDate).ConfigureAwait(false),
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-1|SKU-3", name: "name", desc: "description", quantity: 100, quantityBackOrdered: 0, unitCorePrice: 20.0M, unitSoldPrice: 20.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1153, dateReceived: CreatedDate).ConfigureAwait(false),
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-1|SKU-4", name: "name", desc: "description", quantity: 125, quantityBackOrdered: 0, unitCorePrice: 25.0M, unitSoldPrice: 25.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1154, dateReceived: CreatedDate).ConfigureAwait(false),
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-1|SKU-5", name: "name", desc: "in place, none ordered", quantity: 000, quantityBackOrdered: 0, unitCorePrice: 25.0M, unitSoldPrice: 25.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1155, masterID: 2, dateReceived: CreatedDate).ConfigureAwait(false),
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-1|SKU-6", name: "name", desc: "in place, none ordered, no vendor", quantity: 000, quantityBackOrdered: 0, unitCorePrice: 25.0M, unitSoldPrice: 25.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1155, masterID: 2, dateReceived: CreatedDate).ConfigureAwait(false),
                    // order received - attached to PO-2
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-2|SKU-1", name: "name", desc: "description", quantity: 050, quantityBackOrdered: 0, unitCorePrice: 10.0M, unitSoldPrice: 10.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1151, masterID: 2, dateReceived: CreatedDate).ConfigureAwait(false),
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-2|SKU-1", name: "name", desc: "description", quantity: 050, quantityBackOrdered: 0, unitCorePrice: 10.0M, unitSoldPrice: 10.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1151, masterID: 2, dateReceived: CreatedDate).ConfigureAwait(false),
                    // order received - attached to PO-2, didn't get all
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-3|SKU-2", name: "name", desc: "description", quantity: 075, quantityBackOrdered: 0, unitCorePrice: 15.0M, unitSoldPrice: 15.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1152, masterID: 2, dateReceived: CreatedDate).ConfigureAwait(false),
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-3|SKU-2", name: "name", desc: "description", quantity: 050, quantityBackOrdered: 0, unitCorePrice: 15.0M, unitSoldPrice: 15.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1152, masterID: 2, dateReceived: CreatedDate).ConfigureAwait(false),
                    // order received - attached to PO-2, got more than ordered
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-4|SKU-3", name: "name", desc: "description", quantity: 100, quantityBackOrdered: 0, unitCorePrice: 20.0M, unitSoldPrice: 20.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1153, masterID: 2, dateReceived: CreatedDate).ConfigureAwait(false),
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-4|SKU-3", name: "name", desc: "description", quantity: 125, quantityBackOrdered: 0, unitCorePrice: 20.0M, unitSoldPrice: 20.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1153, masterID: 2, dateReceived: CreatedDate).ConfigureAwait(false),
                    // order received - attached to PO-2, didn't get any.
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-5|SKU-4", name: "name", desc: "description", quantity: 125, quantityBackOrdered: 0, unitCorePrice: 25.0M, unitSoldPrice: 25.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1154, masterID: 2, dateReceived: CreatedDate).ConfigureAwait(false),
                    // order received - attached to PO-2, wasn't even ordered
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-6|SKU-5", name: "name", desc: "description", quantity: 125, quantityBackOrdered: 0, unitCorePrice: 25.0M, unitSoldPrice: 25.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1155, masterID: 2, dateReceived: CreatedDate).ConfigureAwait(false),
                    // order received - attached to PO-2, wasn't even ordered and not even known
                    await CreateADummyPurchaseOrderItemAsync(id: ++index, key: "PO-7|SKU-6", name: "name", desc: "description", quantity: 050, quantityBackOrdered: 0, unitCorePrice: 25.0M, unitSoldPrice: 25.0M, sku: "sku", unitOfMeasure: "unitOfMeasure", productID: 1156, masterID: 2, dateReceived: CreatedDate).ConfigureAwait(false),
                };
                await InitializeMockSetPurchaseOrderItemsAsync(mockContext, RawPurchaseOrderItems).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Purchase Order Item Targets
            if (DoAll || DoPurchasing || DoPurchaseOrderItemTargetTable)
            {
                RawPurchaseOrderItemTargets = new()
                {
                    await CreateADummyPurchaseOrderItemTargetAsync(id: 1, key: "PURCHASE-ORDER-ITEM-TARGET-1", quantity: 0).ConfigureAwait(false),
                };
                await InitializeMockSetPurchaseOrderItemTargetsAsync(mockContext, RawPurchaseOrderItemTargets).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Purchase Order States
            if (DoAll || DoPurchasing || DoPurchaseOrderStateTable)
            {
                var index = 0;
                RawPurchaseOrderStates = new()
                {
                    await CreateADummyPurchaseOrderStateAsync(id: ++index, key: "WORK", name: "Work", desc: "desc", sortOrder: 1, displayName: "Work").ConfigureAwait(false),
                    await CreateADummyPurchaseOrderStateAsync(id: ++index, key: "HISTORY", name: "History", desc: "desc", sortOrder: 2, displayName: "History").ConfigureAwait(false),
                };
                await InitializeMockSetPurchaseOrderStatesAsync(mockContext, RawPurchaseOrderStates).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Purchase Order Statuses
            if (DoAll || DoPurchasing || DoPurchaseOrderStatusTable)
            {
                var index = 0;
                RawPurchaseOrderStatuses = new()
                {
                    await CreateADummyPurchaseOrderStatusAsync(id: ++index, key: "Submitted", name: "Submitted", desc: "desc", displayName: "Submitted").ConfigureAwait(false),
                    await CreateADummyPurchaseOrderStatusAsync(id: ++index, key: "In Progress", name: "In Progress", desc: "desc", displayName: "In Progress").ConfigureAwait(false),
                    await CreateADummyPurchaseOrderStatusAsync(id: ++index, key: "Fulfilled", name: "Fulfilled", desc: "desc", displayName: "Fulfilled").ConfigureAwait(false),
                };
                await InitializeMockSetPurchaseOrderStatusesAsync(mockContext, RawPurchaseOrderStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Purchase Order Types
            if (DoAll || DoPurchasing || DoPurchaseOrderTypeTable)
            {
                var index = 0;
                RawPurchaseOrderTypes = new()
                {
                    await CreateADummyPurchaseOrderTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                    await CreateADummyPurchaseOrderTypeAsync(id: ++index, key: "Drop-Ship", name: "Drop-Ship", desc: "desc", displayName: "Drop-Ship").ConfigureAwait(false),
                };
                await InitializeMockSetPurchaseOrderTypesAsync(mockContext, RawPurchaseOrderTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Order Purchase Orders
            if (DoAll || DoPurchasing || DoSalesOrderPurchaseOrderTable)
            {
                RawSalesOrderPurchaseOrders = new()
                {
                    await CreateADummySalesOrderPurchaseOrderAsync(id: 1, key: "SALES-ORDER-PURCHASE-ORDER").ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrderPurchaseOrdersAsync(mockContext, RawSalesOrderPurchaseOrders).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
