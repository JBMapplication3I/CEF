// <copyright file="DoMockingSetupForContextRunnerTracking.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner tracking class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerTrackingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Campaigns
            if (DoAll || DoTracking || DoCampaignTable)
            {
                RawCampaigns = new()
                {
                    await CreateADummyCampaignAsync(id: 1, key: "CAMPAIGN-1", name: "Campaign 1", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetCampaignsAsync(mockContext, RawCampaigns).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Campaign Ads
            if (DoAll || DoTracking || DoCampaignAdTable)
            {
                RawCampaignAds = new()
                {
                    await CreateADummyCampaignAdAsync(id: 1, key: "CAMPAIGN-AD-1").ConfigureAwait(false),
                };
                await InitializeMockSetCampaignAdsAsync(mockContext, RawCampaignAds).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Campaign Statuses
            if (DoAll || DoTracking || DoCampaignStatusTable)
            {
                RawCampaignStatuses = new()
                {
                    await CreateADummyCampaignStatusAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetCampaignStatusesAsync(mockContext, RawCampaignStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Campaign Types
            if (DoAll || DoTracking || DoCampaignTypeTable)
            {
                RawCampaignTypes = new()
                {
                    await CreateADummyCampaignTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetCampaignTypesAsync(mockContext, RawCampaignTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Events
            if (DoAll || DoTracking || DoEventTable)
            {
                RawEvents = new()
                {
                    await CreateADummyEventAsync(id: 1, key: "EVENT-1", name: "Event 1", desc: "desc", endedOn: CreatedDate.AddHours(1), iPAddress: "127.0.0.1", isFirstTrigger: true, startedOn: CreatedDate).ConfigureAwait(false),
                };
                await InitializeMockSetEventsAsync(mockContext, RawEvents).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Event Statuses
            if (DoAll || DoTracking || DoEventStatusTable)
            {
                RawEventStatuses = new()
                {
                    await CreateADummyEventStatusAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetEventStatusesAsync(mockContext, RawEventStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Event Types
            if (DoAll || DoTracking || DoEventTypeTable)
            {
                RawEventTypes = new()
                {
                    await CreateADummyEventTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetEventTypesAsync(mockContext, RawEventTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: IP Organizations
            if (DoAll || DoTracking || DoIPOrganizationTable)
            {
                RawIPOrganizations = new()
                {
                    await CreateADummyIPOrganizationAsync(id: 1, key: "IP-ORGANIZATION-1", name: "IP Organization 1", desc: "desc", iPAddress: "8.8.8.8").ConfigureAwait(false),
                };
                await InitializeMockSetIPOrganizationsAsync(mockContext, RawIPOrganizations).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: IP Organization Statuses
            if (DoAll || DoTracking || DoIPOrganizationStatusTable)
            {
                RawIPOrganizationStatuses = new()
                {
                    await CreateADummyIPOrganizationStatusAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetIPOrganizationStatusesAsync(mockContext, RawIPOrganizationStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Page Views
            if (DoAll || DoTracking || DoPageViewTable)
            {
                RawPageViews = new()
                {
                    await CreateADummyPageViewAsync(id: 1, key: "PAGE-VIEW-1", name: "Page View 1", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetPageViewsAsync(mockContext, RawPageViews).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Page View Events
            if (DoAll || DoTracking || DoPageViewEventTable)
            {
                RawPageViewEvents = new()
                {
                    await CreateADummyPageViewEventAsync(id: 1, key: "PAGE-VIEW-EVENT-1").ConfigureAwait(false),
                };
                await InitializeMockSetPageViewEventsAsync(mockContext, RawPageViewEvents).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Page View Statuses
            if (DoAll || DoTracking || DoPageViewStatusTable)
            {
                RawPageViewStatuses = new()
                {
                    await CreateADummyPageViewStatusAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetPageViewStatusesAsync(mockContext, RawPageViewStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Page View Types
            if (DoAll || DoTracking || DoPageViewTypeTable)
            {
                RawPageViewTypes = new()
                {
                    await CreateADummyPageViewTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetPageViewTypesAsync(mockContext, RawPageViewTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Visits
            if (DoAll || DoTracking || DoVisitTable)
            {
                RawVisits = new()
                {
                    await CreateADummyVisitAsync(id: 1, key: "VISIT-1", name: "Visit 1", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetVisitsAsync(mockContext, RawVisits).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Visitors
            if (DoAll || DoTracking || DoVisitorTable)
            {
                RawVisitors = new()
                {
                    await CreateADummyVisitorAsync(id: 1, key: "VISITOR-1", name: "Visitor 1", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetVisitorsAsync(mockContext, RawVisitors).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Visit Statuses
            if (DoAll || DoTracking || DoVisitStatusTable)
            {
                RawVisitStatuses = new()
                {
                    await CreateADummyVisitStatusAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetVisitStatusesAsync(mockContext, RawVisitStatuses).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
