// <copyright file="DoMockingSetupForContextRunnerQuoting.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner quoting class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerQuotingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Sales Quotes
            if (DoAll || DoQuoting || DoSalesQuoteTable)
            {
                RawSalesQuotes = new()
                {
                    await CreateADummySalesQuoteAsync(id: 1, key: "SALES-QUOTE-1", subtotalItems: 0, subtotalShipping: 0, subtotalTaxes: 0, subtotalFees: 0, subtotalHandling: 0, subtotalDiscounts: 0, total: 0, shippingSameAsBilling: true).ConfigureAwait(false),
                };
                await InitializeMockSetSalesQuotesAsync(mockContext, RawSalesQuotes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Quote Contacts
            if (DoAll || DoQuoting || DoSalesQuoteContactTable)
            {
                RawSalesQuoteContacts = new()
                {
                    await CreateADummySalesQuoteContactAsync(id: 1, key: "SALES-QUOTE-CONTACT-1").ConfigureAwait(false),
                };
                await InitializeMockSetSalesQuoteContactsAsync(mockContext, RawSalesQuoteContacts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Quote Categories
            if (DoAll || DoQuoting || DoSalesQuoteCategoryTable)
            {
                RawSalesQuoteCategories = new()
                {
                    await CreateADummySalesQuoteCategoryAsync(id: 1, key: "SALES-QUOTE-CATEGORY-1").ConfigureAwait(false),
                };
                await InitializeMockSetSalesQuoteCategoriesAsync(mockContext, RawSalesQuoteCategories).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Quote Events
            if (DoAll || DoQuoting || DoSalesQuoteEventTable)
            {
                var index = 0;
                RawSalesQuoteEvents = new()
                {
                    await CreateADummySalesQuoteEventAsync(id: ++index, key: "Event-1", name: "Event 1", desc: "desc", masterID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetSalesQuoteEventsAsync(mockContext, RawSalesQuoteEvents).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Quote Event Types
            if (DoAll || DoQuoting || DoSalesQuoteEventTypeTable)
            {
                var index = 0;
                RawSalesQuoteEventTypes = new()
                {
                    await CreateADummySalesQuoteEventTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetSalesQuoteEventTypesAsync(mockContext, RawSalesQuoteEventTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Quote Files (New)
            if (DoAll || DoQuoting || DoSalesQuoteFileTable)
            {
                RawSalesQuoteFiles = new()
                {
                    await CreateADummySalesQuoteFileAsync(id: 1, key: "SALES-QUOTE-FILE-NEW-1", name: "Sales Quote File New 1", desc: "desc", fileAccessTypeID: 0).ConfigureAwait(false),
                };
                await InitializeMockSetSalesQuoteFilesAsync(mockContext, RawSalesQuoteFiles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Quote Items
            if (DoAll || DoQuoting || DoSalesQuoteItemTable)
            {
                RawSalesQuoteItems = new()
                {
                    await CreateADummySalesQuoteItemAsync(id: 1, key: "SALES-QUOTE-ITEM-1", name: "Sales Quote Item 1", desc: "desc", quantity: 1, unitCorePrice: 0, productID: 1151).ConfigureAwait(false),
                };
                await InitializeMockSetSalesQuoteItemsAsync(mockContext, RawSalesQuoteItems).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Quote Item Targets
            if (DoAll || DoQuoting || DoSalesQuoteItemTargetTable)
            {
                RawSalesQuoteItemTargets = new()
                {
                    await CreateADummySalesQuoteItemTargetAsync(id: 1, key: "SALES-QUOTE-ITEM-TARGET-1", quantity: 0).ConfigureAwait(false),
                };
                await InitializeMockSetSalesQuoteItemTargetsAsync(mockContext, RawSalesQuoteItemTargets).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Quote States
            if (DoAll || DoQuoting || DoSalesQuoteStateTable)
            {
                var index = 0;
                RawSalesQuoteStates = new()
                {
                    await CreateADummySalesQuoteStateAsync(id: ++index, key: "WORK", name: "Work", desc: "desc", sortOrder: 1, displayName: "Work").ConfigureAwait(false),
                    await CreateADummySalesQuoteStateAsync(id: ++index, key: "HISTORY", name: "History", desc: "desc", sortOrder: 2, displayName: "History").ConfigureAwait(false),
                };
                await InitializeMockSetSalesQuoteStatesAsync(mockContext, RawSalesQuoteStates).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Quote Statuses
            if (DoAll || DoQuoting || DoSalesQuoteStatusTable)
            {
                var index = 0;
                RawSalesQuoteStatuses = new()
                {
                    await CreateADummySalesQuoteStatusAsync(id: ++index, key: "Submitted", name: "Submitted", desc: "desc", displayName: "Submitted").ConfigureAwait(false),
                    await CreateADummySalesQuoteStatusAsync(id: ++index, key: "In Process", name: "In Process", desc: "desc", displayName: "In Process").ConfigureAwait(false),
                    await CreateADummySalesQuoteStatusAsync(id: ++index, key: "Processed", name: "Processed", desc: "desc", displayName: "Processed").ConfigureAwait(false),
                    await CreateADummySalesQuoteStatusAsync(id: ++index, key: "Approved", name: "Approved", desc: "desc", displayName: "Approved").ConfigureAwait(false),
                    await CreateADummySalesQuoteStatusAsync(id: ++index, key: "Denied", name: "Denied", desc: "desc", displayName: "Denied").ConfigureAwait(false),
                    await CreateADummySalesQuoteStatusAsync(id: ++index, key: "Confirmed", name: "Confirmed", desc: "desc", displayName: "Confirmed").ConfigureAwait(false),
                    await CreateADummySalesQuoteStatusAsync(id: ++index, key: "Expired", name: "Expired", desc: "desc", displayName: "Expired").ConfigureAwait(false),
                };
                await InitializeMockSetSalesQuoteStatusesAsync(mockContext, RawSalesQuoteStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Quote Types
            if (DoAll || DoQuoting || DoSalesQuoteTypeTable)
            {
                var index = 0;
                RawSalesQuoteTypes = new()
                {
                    await CreateADummySalesQuoteTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetSalesQuoteTypesAsync(mockContext, RawSalesQuoteTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Quote Sales Orders
            if (DoAll || DoQuoting || DoSalesQuoteSalesOrderTable)
            {
                RawSalesQuoteSalesOrders = new()
                {
                    await CreateADummySalesQuoteSalesOrderAsync(id: 1, key: "SALES-QUOTE-SALES-ORDER").ConfigureAwait(false),
                };
                await InitializeMockSetSalesQuoteSalesOrdersAsync(mockContext, RawSalesQuoteSalesOrders).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
