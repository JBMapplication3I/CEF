// <copyright file="DoMockingSetupForContextRunnerScheduling.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner Scheduling class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerSchedulingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Appointments
            if (DoAll || DoScheduling || DoAppointmentTable)
            {
                var index = 0;
                RawAppointments = new()
                {
                    await CreateADummyAppointmentAsync(id: ++index, key: "Appointment-" + index, "Appointment-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetAppointmentsAsync(mockContext, RawAppointments).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Appointment Statuses
            if (DoAll || DoScheduling || DoAppointmentStatusTable)
            {
                var index = 0;
                RawAppointmentStatuses = new()
                {
                    await CreateADummyAppointmentStatusAsync(id: ++index, key: "Appointment-Status-" + index, name: "Appointment-Status-" + index, displayName: "Appointment-Status-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetAppointmentStatusesAsync(mockContext, RawAppointmentStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Appointment Types
            if (DoAll || DoScheduling || DoAppointmentTypeTable)
            {
                var index = 0;
                RawAppointmentTypes = new()
                {
                    await CreateADummyAppointmentTypeAsync(id: ++index, key: "Appointment-Type-" + index, name: "Appointment-Type-" + index, displayName: "Appointment-Type-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetAppointmentTypesAsync(mockContext, RawAppointmentTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Calendars
            if (DoAll || DoScheduling || DoCalendarTable)
            {
                var index = 0;
                RawCalendars = new()
                {
                    await CreateADummyCalendarAsync(id: ++index, key: "Calendar-" + index).ConfigureAwait(false),
                };
                await InitializeMockSetCalendarsAsync(mockContext, RawCalendars).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Calendar Appointments
            if (DoAll || DoScheduling || DoCalendarAppointmentTable)
            {
                var index = 0;
                RawCalendarAppointments = new()
                {
                    await CreateADummyCalendarAppointmentAsync(id: ++index, key: "Calendar-Appointment-" + index, masterID: 1, slaveID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetCalendarAppointmentsAsync(mockContext, RawCalendarAppointments).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
