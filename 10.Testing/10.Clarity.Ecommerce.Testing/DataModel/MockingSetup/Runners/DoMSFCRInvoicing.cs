// <copyright file="DoMockingSetupForContextRunnerInvoicing.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner invoicing class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerInvoicingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Sales Invoices
            if (DoAll || DoInvoicing || DoSalesInvoiceTable)
            {
                RawSalesInvoices = new()
                {
                    await CreateADummySalesInvoiceAsync(id: 1, key: "SALES-INVOICE-1", subtotalItems: 0, subtotalShipping: 0, subtotalTaxes: 0, subtotalFees: 0, subtotalHandling: 0, subtotalDiscounts: 0, total: 0, shippingSameAsBilling: true, statusID: 1, userID: 1, accountID: 1, balanceDue: 169, billingContactID: 1).ConfigureAwait(false),
                    await CreateADummySalesInvoiceAsync(id: 2, key: "SALES-INVOICE-2-UNPAID-PASSED-DUE", subtotalItems: 150, subtotalShipping: 10, subtotalTaxes: 9, subtotalFees: 0, subtotalHandling: 0, subtotalDiscounts: 0, total: 169, shippingSameAsBilling: true, statusID: 1, balanceDue: 169, dueDate: DateTime.Today.AddDays(-91), billingContactID: 1, userID: 1, accountID: 1).ConfigureAwait(false),
                    await CreateADummySalesInvoiceAsync(id: 3, key: "SALES-INVOICE-3-UNPAID", subtotalItems: 150, subtotalShipping: 10, subtotalTaxes: 9, subtotalFees: 0, subtotalHandling: 0, subtotalDiscounts: 0, total: 169, shippingSameAsBilling: true, statusID: 1, balanceDue: 169, dueDate: DateTime.Today.AddDays(14), billingContactID: 1, userID: 1, accountID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetSalesInvoicesAsync(mockContext, RawSalesInvoices).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Invoice Contacts
            if (DoAll || DoInvoicing || DoSalesInvoiceContactTable)
            {
                RawSalesInvoiceContacts = new()
                {
                    await CreateADummySalesInvoiceContactAsync(id: 1, key: "SALES-INVOICE-CONTACT-1").ConfigureAwait(false),
                };
                await InitializeMockSetSalesInvoiceContactsAsync(mockContext, RawSalesInvoiceContacts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Invoice Events
            if (DoAll || DoInvoicing || DoSalesInvoiceEventTable)
            {
                var index = 0;
                RawSalesInvoiceEvents = new()
                {
                    await CreateADummySalesInvoiceEventAsync(id: ++index, key: "Event-1", name: "Event 1", desc: "desc", typeID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetSalesInvoiceEventsAsync(mockContext, RawSalesInvoiceEvents).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Invoice Event Types
            if (DoAll || DoInvoicing || DoSalesInvoiceEventTypeTable)
            {
                var index = 0;
                RawSalesInvoiceEventTypes = new()
                {
                    await CreateADummySalesInvoiceEventTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetSalesInvoiceEventTypesAsync(mockContext, RawSalesInvoiceEventTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Invoice Files (New)
            if (DoAll || DoInvoicing || DoSalesInvoiceFileTable)
            {
                RawSalesInvoiceFiles = new()
                {
                    await CreateADummySalesInvoiceFileAsync(id: 1, key: "SALES-INVOICE-FILE-NEW-1", name: "Sales Invoice File New 1", desc: "desc", fileAccessTypeID: 0).ConfigureAwait(false),
                };
                await InitializeMockSetSalesInvoiceFilesAsync(mockContext, RawSalesInvoiceFiles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Invoice Items
            if (DoAll || DoInvoicing || DoSalesInvoiceItemTable)
            {
                RawSalesInvoiceItems = new()
                {
                    await CreateADummySalesInvoiceItemAsync(id: 1, key: "SALES-INVOICE-ITEM-1", name: "Sales Invoice Item 1", desc: "desc", quantity: 1, unitCorePrice: 0, productID: 1151).ConfigureAwait(false),
                };
                await InitializeMockSetSalesInvoiceItemsAsync(mockContext, RawSalesInvoiceItems).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Invoice Item Targets
            if (DoAll || DoInvoicing || DoSalesInvoiceItemTargetTable)
            {
                RawSalesInvoiceItemTargets = new()
                {
                    await CreateADummySalesInvoiceItemTargetAsync(id: 1, key: "SALES-INVOICE-ITEM-TARGET-1", quantity: 0).ConfigureAwait(false),
                };
                await InitializeMockSetSalesInvoiceItemTargetsAsync(mockContext, RawSalesInvoiceItemTargets).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Invoice States
            if (DoAll || DoInvoicing || DoSalesInvoiceStateTable)
            {
                var index = 0;
                RawSalesInvoiceStates = new()
                {
                    await CreateADummySalesInvoiceStateAsync(id: ++index, key: "WORK", name: "Work", desc: "desc", sortOrder: 1, displayName: "Work").ConfigureAwait(false),
                    await CreateADummySalesInvoiceStateAsync(id: ++index, key: "HISTORY", name: "History", desc: "desc", sortOrder: 2, displayName: "History").ConfigureAwait(false),
                };
                await InitializeMockSetSalesInvoiceStatesAsync(mockContext, RawSalesInvoiceStates).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Invoice Statuses
            if (DoAll || DoInvoicing || DoSalesInvoiceStatusTable)
            {
                var index = 0;
                RawSalesInvoiceStatuses = new()
                {
                    await CreateADummySalesInvoiceStatusAsync(id: ++index, key: "Unpaid", name: "Unpaid", desc: "desc", sortOrder: 0, displayName: "Unpaid").ConfigureAwait(false),
                    await CreateADummySalesInvoiceStatusAsync(id: ++index, key: "Partially Paid", name: "Partially Paid", desc: "desc", sortOrder: 1, displayName: "Partially Paid").ConfigureAwait(false),
                    await CreateADummySalesInvoiceStatusAsync(id: ++index, key: "Paid", name: "Paid", desc: "desc", sortOrder: 2, displayName: "Paid").ConfigureAwait(false),
                    await CreateADummySalesInvoiceStatusAsync(id: ++index, key: "Void", name: "Void", desc: "desc", sortOrder: 3, displayName: "Void").ConfigureAwait(false),
                };
                await InitializeMockSetSalesInvoiceStatusesAsync(mockContext, RawSalesInvoiceStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Invoice Types
            if (DoAll || DoInvoicing || DoSalesInvoiceTypeTable)
            {
                var index = 0;
                RawSalesInvoiceTypes = new()
                {
                    await CreateADummySalesInvoiceTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetSalesInvoiceTypesAsync(mockContext, RawSalesInvoiceTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Order Sales Invoices
            if (DoAll || DoInvoicing || DoSalesOrderSalesInvoiceTable)
            {
                //var index = 0;
                RawSalesOrderSalesInvoices = new()
                {
                    await CreateADummySalesOrderSalesInvoiceAsync(id: 1, key: "SALES-ORDER-SALES-INVOICE-1").ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrderSalesInvoicesAsync(mockContext, RawSalesOrderSalesInvoices).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
