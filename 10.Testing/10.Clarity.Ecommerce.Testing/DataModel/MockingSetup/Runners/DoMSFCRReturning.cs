// <copyright file="DoMockingSetupForContextRunnerReturning.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner returning class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerReturningAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Sales Returns
            if (DoAll || DoReturning || DoSalesReturnTable)
            {
                RawSalesReturns = new()
                {
                    await CreateADummySalesReturnAsync(id: 00001, key: "WEB-16-1",     subtotalItems: 322.05m, subtotalShipping: 30.15m, subtotalTaxes: 26.57m, subtotalFees: 2.00m, subtotalHandling: 04.00m, subtotalDiscounts: 00.00m, total: 369.77m, shippingSameAsBilling: true,               statusID: 01, balanceDue: 0m, refundAmount: 0.00m, salesGroupID: 0001).ConfigureAwait(false),
                    await CreateADummySalesReturnAsync(id: 30000, key: "WEB-16-30000", subtotalItems: 322.05m, subtotalShipping: 30.15m, subtotalTaxes: 26.57m, subtotalFees: 2.00m, subtotalHandling: 04.00m, subtotalDiscounts: 00.00m, total: 369.77m, shippingSameAsBilling: true,               statusID: 01, balanceDue: 0m, refundAmount: 0.00m).ConfigureAwait(false),
                    await CreateADummySalesReturnAsync(id: 30001, key: "WEB-16-30001", subtotalItems: 024.95m, subtotalShipping: 03.05m, subtotalTaxes: 02.00m, subtotalFees: 0.49m, subtotalHandling: 03.47m, subtotalDiscounts: 01.00m, total: 032.96m, shippingSameAsBilling: true,               statusID: 10, balanceDue: 0m, refundAmount: 0.00m, stateID: 2).ConfigureAwait(false),
                    await CreateADummySalesReturnAsync(id: 30002, key: "WEB-16-30002", subtotalItems: 024.95m, subtotalShipping: 03.05m, subtotalTaxes: 02.00m, subtotalFees: 0.49m, subtotalHandling: 03.47m, subtotalDiscounts: 01.00m, total: 032.96m, shippingSameAsBilling: true, accountID: 2, statusID: 07, balanceDue: 0m, refundAmount: 0.00m).ConfigureAwait(false),
                    await CreateADummySalesReturnAsync(id: 30003, key: "WEB-16-30003", subtotalItems: 024.95m, subtotalShipping: 03.05m, subtotalTaxes: 02.00m, subtotalFees: 0.49m, subtotalHandling: 03.47m, subtotalDiscounts: 01.00m, total: 032.96m, shippingSameAsBilling: true, accountID: 2, statusID: 07, balanceDue: 0m, refundAmount: 0.00m).ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnsAsync(mockContext, RawSalesReturns).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Return Contacts
            if (DoAll || DoReturning || DoSalesReturnContactTable)
            {
                RawSalesReturnContacts = new()
                {
                    await CreateADummySalesReturnContactAsync(id: 1, key: "SALES-RETURN-CONTACT-1").ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnContactsAsync(mockContext, RawSalesReturnContacts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Return Events
            if (DoAll || DoReturning || DoSalesReturnEventTable)
            {
                var index = 0;
                RawSalesReturnEvents = new()
                {
                    await CreateADummySalesReturnEventAsync(id: ++index, key: "Event-1", name: "Event 1", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnEventsAsync(mockContext, RawSalesReturnEvents).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Return Event Types
            if (DoAll || DoReturning || DoSalesReturnEventTypeTable)
            {
                var index = 0;
                RawSalesReturnEventTypes = new()
                {
                    await CreateADummySalesReturnEventTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnEventTypesAsync(mockContext, RawSalesReturnEventTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Return Files (New)
            if (DoAll || DoReturning || DoSalesReturnFileTable)
            {
                RawSalesReturnFiles = new()
                {
                    await CreateADummySalesReturnFileAsync(id: 1, key: "SALES-RETURN-FILE-NEW-1", name: "Sales Return File New 1", desc: "desc", fileAccessTypeID: 0).ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnFilesAsync(mockContext, RawSalesReturnFiles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Return Items
            if (DoAll || DoReturning || DoSalesReturnItemTable)
            {
                var index = 0;
                RawSalesReturnItems = new()
                {
                    await CreateADummySalesReturnItemAsync(id: ++index, key: "WEB-16-30000|0200-ANGBT-BFUT-IA", name: "200ml Ang Futura", quantity: 1, unitCorePrice: 13.05m, unitSoldPrice: 13.05m, sku: "0200-ANGBT-BFUT-IA", unitOfMeasure: "EACH", productID: 1151, restockingFeeAmount: 0.00m).ConfigureAwait(false),
                    await CreateADummySalesReturnItemAsync(id: 30000 * 10 + ++index, key: "WEB-16-30000|0200-ANGRF-OPERA-VSI-IA", name: "200ml Opera", quantity: 1, unitCorePrice: 13.05m, unitSoldPrice: 13.05m, sku: "0200-ANGRF-OPERA-VSI-IA", unitOfMeasure: "EACH", productID: 1152, masterID: 30000, restockingFeeAmount: 0.00m).ConfigureAwait(false),
                    await CreateADummySalesReturnItemAsync(id: 30000 * 10 + ++index, key: "WEB-16-30000|ZORK-BLACK", name: "Black Zorks", quantity: 1, unitCorePrice: 0.19m * 1400m, unitSoldPrice: 0.19m * 1400m, sku: "ZORK-BLACK", unitOfMeasure: "CS", productID: 0969, masterID: 30000, restockingFeeAmount: 0.00m).ConfigureAwait(false),
                    await CreateADummySalesReturnItemAsync(id: 30000 * 10 + ++index, key: "WEB-16-30000|7403F-XL--DC-Bag", name: "A Bag", quantity: 1, unitCorePrice: 29.95m, unitSoldPrice: 29.95m, sku: "7403F-XL--DC-Bag", unitOfMeasure: "EACH", productID: 0969, masterID: 30000, restockingFeeAmount: 0.00m).ConfigureAwait(false),
                    await CreateADummySalesReturnItemAsync(id: 30001 * 10 + ++index, key: "WEB-16-30001|7403F-XL--DC-Bag", name: "A Bag", quantity: 1, unitCorePrice: 29.95m, unitSoldPrice: 24.95m, sku: "7403F-XL--DC-Bag", unitOfMeasure: "EACH", productID: 0969, masterID: 30001, restockingFeeAmount: 0.00m).ConfigureAwait(false),
                    await CreateADummySalesReturnItemAsync(id: 30001 * 10 + ++index, key: "WEB-16-30001|0200-ANGBT-BFUT-IA", name: "200ml Ang Futura", quantity: 1, unitCorePrice: 13.05m, unitSoldPrice: 13.05m, sku: "0200-ANGBT-BFUT-IA", unitOfMeasure: "EACH", productID: 1151, masterID: 30001, restockingFeeAmount: 0.00m).ConfigureAwait(false),
                    await CreateADummySalesReturnItemAsync(id: 30002 * 10 + ++index, key: "WEB-16-30002|7403F-XL--DC-Bag", name: "A Bag", quantity: 1, unitCorePrice: 29.95m, unitSoldPrice: 24.95m, sku: "7403F-XL--DC-Bag", unitOfMeasure: "EACH", productID: 0969, masterID: 30002, restockingFeeAmount: 0.00m).ConfigureAwait(false),
                    await CreateADummySalesReturnItemAsync(id: 30003 * 10 + ++index, key: "WEB-16-30003|7403F-XL--DC-Bag", name: "A Bag", quantity: 1, unitCorePrice: 29.95m, unitSoldPrice: 24.95m, sku: "7403F-XL--DC-Bag", unitOfMeasure: "EACH", productID: 0969, masterID: 30003, restockingFeeAmount: 0.00m).ConfigureAwait(false),
                    await CreateADummySalesReturnItemAsync(id: 30003 * 10 + ++index, key: "WEB-16-30003|0200-ANGBT-BFUT-IA", name: "200ml Ang Futura", quantity: 1, unitCorePrice: 13.05m, unitSoldPrice: 13.05m, sku: "0200-ANGBT-BFUT-IA", unitOfMeasure: "EACH", productID: 1151, masterID: 30003, restockingFeeAmount: 0.00m).ConfigureAwait(false),
                    await CreateADummySalesReturnItemAsync(id: 00001 * 10 + ++index, key: "WEB-16-1|0200-ANGBT-BFUT-IA", name: "200ml Ang Futura", quantity: 1, unitCorePrice: 13.05m, unitSoldPrice: 13.05m, sku: "0200-ANGBT-BFUT-IA", unitOfMeasure: "EACH", productID: 1151, restockingFeeAmount: 0.00m).ConfigureAwait(false),
                    await CreateADummySalesReturnItemAsync(id: 00001 * 10 + ++index, key: "WEB-16-1|0200-ANGRF-OPERA-VSI-IA", name: "200ml Opera", quantity: 1, unitCorePrice: 13.05m, unitSoldPrice: 13.05m, sku: "0200-ANGRF-OPERA-VSI-IA", unitOfMeasure: "EACH", productID: 1152, restockingFeeAmount: 0.00m).ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnItemsAsync(mockContext, RawSalesReturnItems).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Return Item Targets
            if (DoAll || DoReturning || DoSalesReturnItemTargetTable)
            {
                RawSalesReturnItemTargets = new()
                {
                    await CreateADummySalesReturnItemTargetAsync(id: 1, key: "SALES-RETURN-ITEM-TARGET-1", quantity: 0).ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnItemTargetsAsync(mockContext, RawSalesReturnItemTargets).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Return Reasons
            if (DoAll || DoReturning || DoSalesReturnReasonTable)
            {
                var index = 0;
                RawSalesReturnReasons = new()
                {
                    await CreateADummySalesReturnReasonAsync(id: ++index, key: "SALES-RETURN-REASON-1", name: "SALES-RETURN-REASON-1", sortOrder: 0, displayName: "SALES-RETURN-REASON-1", restockingFeeAmount: 0.00m, restockingFeePercent: 0.00m).ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnReasonsAsync(mockContext, RawSalesReturnReasons).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Return Sales Orders
            if (DoAll || DoReturning || DoSalesReturnSalesOrderTable)
            {
                RawSalesReturnSalesOrders = new()
                {
                    await CreateADummySalesReturnSalesOrderAsync(id: 1, key: "SALES-RETURN-SALES-ORDER-1").ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnSalesOrdersAsync(mockContext, RawSalesReturnSalesOrders).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Return States
            if (DoAll || DoReturning || DoSalesReturnStateTable)
            {
                var index = 0;
                RawSalesReturnStates = new()
                {
                    await CreateADummySalesReturnStateAsync(id: ++index, key: "WORK", name: "Work", sortOrder: 1, displayName: "Work").ConfigureAwait(false),
                    await CreateADummySalesReturnStateAsync(id: ++index, key: "HISTORY", name: "History", sortOrder: 2, displayName: "History").ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnStatesAsync(mockContext, RawSalesReturnStates).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Return Statuses
            if (DoAll || DoReturning || DoSalesReturnStatusTable)
            {
                var index = 0;
                RawSalesReturnStatuses = new()
                {
                    await CreateADummySalesReturnStatusAsync(id: ++index, key: "Pending", name: "Pending", sortOrder: 1, displayName: "Pending").ConfigureAwait(false), // Submitted
                    await CreateADummySalesReturnStatusAsync(id: ++index, key: "Confirmed", name: "Confirmed", sortOrder: 2, displayName: "Confirmed").ConfigureAwait(false),
                    await CreateADummySalesReturnStatusAsync(id: ++index, key: "Split", name: "Split", sortOrder: 4, displayName: "Split").ConfigureAwait(false), // Closed
                    await CreateADummySalesReturnStatusAsync(id: ++index, key: "Payment Sent", name: "Payment Sent", sortOrder: 6, displayName: "Payment Sent").ConfigureAwait(false),
                    await CreateADummySalesReturnStatusAsync(id: ++index, key: "Shipped", name: "Shipped", sortOrder: 9, displayName: "Shipped").ConfigureAwait(false),
                    await CreateADummySalesReturnStatusAsync(id: ++index, key: "Completed", name: "Completed", sortOrder: 10, displayName: "Completed").ConfigureAwait(false),
                    await CreateADummySalesReturnStatusAsync(id: ++index, key: "Void", name: "Void", sortOrder: 11, displayName: "Void").ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnStatusesAsync(mockContext, RawSalesReturnStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Return Types
            if (DoAll || DoReturning || DoSalesReturnTypeTable)
            {
                var index = 0;
                RawSalesReturnTypes = new()
                {
                    await CreateADummySalesReturnTypeAsync(id: ++index, key: "Web", name: "Web", displayName: "Web").ConfigureAwait(false),
                    await CreateADummySalesReturnTypeAsync(id: ++index, key: "Phone", name: "Phone", displayName: "Phone").ConfigureAwait(false),
                    await CreateADummySalesReturnTypeAsync(id: ++index, key: "On Site", name: "On Site", displayName: "On Site").ConfigureAwait(false),
                    await CreateADummySalesReturnTypeAsync(id: ++index, key: "Sales Return Child", name: "Sales Return Child", displayName: "Sales Return Child").ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnTypesAsync(mockContext, RawSalesReturnTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
