// <copyright file="DoMockingSetupForContextRunnerCalendarEventsAsync.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner calendar events class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerCalendarEventsAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Calendar Events
            if (DoAll || DoCalendarEvents || DoCalendarEventTable)
            {
                RawCalendarEvents = new()
                {
                    await CreateADummyCalendarEventAsync(id: 1, key: "CALENDAR-EVENT-1", name: "Calendar Event 1", desc: "desc", endDate: CreatedDate, eventDuration: 8, eventDurationUnitOfMeasure: "hours", maxAttendees: 100, startDate: CreatedDate).ConfigureAwait(false),
                };
                await InitializeMockSetCalendarEventsAsync(mockContext, RawCalendarEvents).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Calendar Event Details
            if (DoAll || DoCalendarEvents || DoCalendarEventDetailTable)
            {
                RawCalendarEventDetails = new()
                {
                    await CreateADummyCalendarEventDetailAsync(id: 1, key: "CALENDAR-EVENT-DETAIL-1", name: "Calendar Event Detail 1", desc: "desc", day: 1, endTime: CreatedDate.AddHours(20), startTime: CreatedDate.AddHours(12)).ConfigureAwait(false),
                };
                await InitializeMockSetCalendarEventDetailsAsync(mockContext, RawCalendarEventDetails).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Calendar Event Files
            if (DoAll || DoCalendarEvents || DoCalendarEventFileTable)
            {
                RawCalendarEventFiles = new()
                {
                    await CreateADummyCalendarEventFileAsync(id: 1, key: "CALENDAR-EVENT-FILE-NEW-1", name: "Calendar Event File New 1", desc: "desc", fileAccessTypeID: 0).ConfigureAwait(false),
                };
                await InitializeMockSetCalendarEventFilesAsync(mockContext, RawCalendarEventFiles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Calendar Event Images
            if (DoAll || DoCalendarEvents || DoCalendarEventImageTable)
            {
                var index = 0;
                RawCalendarEventImages = new()
                {
                    await CreateADummyCalendarEventImageAsync(id: ++index, key: "Image-" + index, name: "An Image " + index, desc: "desc", displayName: "An Image " + index, isPrimary: true, originalFileFormat: "jpg", originalFileName: "image.jpg", thumbnailFileFormat: "jpg", thumbnailFileName: "image.jpg").ConfigureAwait(false),
                };
                await InitializeMockSetCalendarEventImagesAsync(mockContext, RawCalendarEventImages).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Calendar Event Image Types
            if (DoAll || DoCalendarEvents || DoCalendarEventImageTypeTable)
            {
                var index = 0;
                RawCalendarEventImageTypes = new()
                {
                    await CreateADummyCalendarEventImageTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 0, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetCalendarEventImageTypesAsync(mockContext, RawCalendarEventImageTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Calendar Event Products
            if (DoAll || DoCalendarEvents || DoCalendarEventProductTable)
            {
                RawCalendarEventProducts = new()
                {
                    await CreateADummyCalendarEventProductAsync(id: 1, key: "CALENDAR-EVENT-PRODUCT-1", masterID: 1, slaveID: 1151).ConfigureAwait(false),
                };
                await InitializeMockSetCalendarEventProductsAsync(mockContext, RawCalendarEventProducts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Calendar Event Statuses
            if (DoAll || DoCalendarEvents || DoCalendarEventStatusTable)
            {
                RawCalendarEventStatuses = new()
                {
                    await CreateADummyCalendarEventStatusAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetCalendarEventStatusesAsync(mockContext, RawCalendarEventStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Calendar Event Types
            if (DoAll || DoCalendarEvents || DoCalendarEventTypeTable)
            {
                RawCalendarEventTypes = new()
                {
                    await CreateADummyCalendarEventTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetCalendarEventTypesAsync(mockContext, RawCalendarEventTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: User Event Attendances
            if (DoAll || DoCalendarEvents || DoUserEventAttendanceTable)
            {
                RawUserEventAttendances = new()
                {
                    await CreateADummyUserEventAttendanceAsync(id: 1, key: "USER-EVENT-ATTENDANCE-1", date: CreatedDate, hasAttended: true).ConfigureAwait(false),
                };
                await InitializeMockSetUserEventAttendancesAsync(mockContext, RawUserEventAttendances).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: User Event Attendance Types
            if (DoAll || DoCalendarEvents || DoUserEventAttendanceTypeTable)
            {
                RawUserEventAttendanceTypes = new()
                {
                    await CreateADummyUserEventAttendanceTypeAsync(id: 1, key: "GENERAL", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetUserEventAttendanceTypesAsync(mockContext, RawUserEventAttendanceTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
