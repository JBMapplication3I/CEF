// <copyright file="DoMockingSetupForContextRunnerSampling.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner sampling class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerSamplingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Sample Requests
            if (DoAll || DoSampling || DoSampleRequestTable)
            {
                RawSampleRequests = new()
                {
                    await CreateADummySampleRequestAsync(id: 1, key: "SAMPLE-REQUEST-1", subtotalItems: 0, subtotalShipping: 0, subtotalTaxes: 0, subtotalFees: 0, subtotalHandling: 0, subtotalDiscounts: 0, total: 0, shippingSameAsBilling: true).ConfigureAwait(false),
                };
                await InitializeMockSetSampleRequestsAsync(mockContext, RawSampleRequests).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sample Request Contacts
            if (DoAll || DoSampling || DoSampleRequestContactTable)
            {
                RawSampleRequestContacts = new()
                {
                    await CreateADummySampleRequestContactAsync(id: 1, key: "SAMPLE-REQUEST-CONTACT-1").ConfigureAwait(false),
                };
                await InitializeMockSetSampleRequestContactsAsync(mockContext, RawSampleRequestContacts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sample Request Events
            if (DoAll || DoSampling || DoSampleRequestEventTable)
            {
                var index = 0;
                RawSampleRequestEvents = new()
                {
                    await CreateADummySampleRequestEventAsync(id: ++index, key: "Event-1", name: "Event 1", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetSampleRequestEventsAsync(mockContext, RawSampleRequestEvents).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sample Request Event Types
            if (DoAll || DoSampling || DoSampleRequestEventTypeTable)
            {
                var index = 0;
                RawSampleRequestEventTypes = new()
                {
                    await CreateADummySampleRequestEventTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetSampleRequestEventTypesAsync(mockContext, RawSampleRequestEventTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sample Request Files (New)
            if (DoAll || DoSampling || DoSampleRequestFileTable)
            {
                RawSampleRequestFiles = new()
                {
                    await CreateADummySampleRequestFileAsync(id: 1, key: "SAMPLE-REQUEST-FILE-NEW-1", name: "Sample Request File New 1", desc: "desc", fileAccessTypeID: 0).ConfigureAwait(false),
                };
                await InitializeMockSetSampleRequestFilesAsync(mockContext, RawSampleRequestFiles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sample Request Items
            if (DoAll || DoSampling || DoSampleRequestItemTable)
            {
                RawSampleRequestItems = new()
                {
                    await CreateADummySampleRequestItemAsync(id: 1, key: "SAMPLE-REQUEST-ITEM-1", name: "Sample Request Item 1", desc: "desc", quantity: 1, unitCorePrice: 0, productID: 1151).ConfigureAwait(false),
                };
                await InitializeMockSetSampleRequestItemsAsync(mockContext, RawSampleRequestItems).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sample Request Item Targets
            if (DoAll || DoSampling || DoSampleRequestItemTargetTable)
            {
                RawSampleRequestItemTargets = new()
                {
                    await CreateADummySampleRequestItemTargetAsync(id: 1, key: "SAMPLE-REQUEST-ITEM-TARGET-1", quantity: 0).ConfigureAwait(false),
                };
                await InitializeMockSetSampleRequestItemTargetsAsync(mockContext, RawSampleRequestItemTargets).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sample Request States
            if (DoAll || DoSampling || DoSampleRequestStateTable)
            {
                RawSampleRequestStates = new()
                {
                    await CreateADummySampleRequestStateAsync(id: 1, key: "WORK", name: "Open", desc: "desc", displayName: "Open").ConfigureAwait(false),
                    await CreateADummySampleRequestStateAsync(id: 2, key: "HISTORY", name: "Closed", desc: "desc", displayName: "Closed").ConfigureAwait(false),
                };
                await InitializeMockSetSampleRequestStatesAsync(mockContext, RawSampleRequestStates).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sample Request Statuses
            if (DoAll || DoSampling || DoSampleRequestStatusTable)
            {
                RawSampleRequestStatuses = new()
                {
                    await CreateADummySampleRequestStatusAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetSampleRequestStatusesAsync(mockContext, RawSampleRequestStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sample Request Types
            if (DoAll || DoSampling || DoSampleRequestTypeTable)
            {
                RawSampleRequestTypes = new()
                {
                    await CreateADummySampleRequestTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetSampleRequestTypesAsync(mockContext, RawSampleRequestTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
