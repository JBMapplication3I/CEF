// <copyright file="CalendarAppointmentStandardWorkflowTests.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar appointment standard workflow tests.extended class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using Ecommerce.Testing;

    public partial class Scheduling_CalendarAppointments_StandardWorkflowTests
    {
        protected override MockingSetup GetMockingSetupWithExistingDataForThisTableAndExpandedTables()
        {
            return new MockingSetup
            {
                DoAppointmentStatusTable = true,
                DoAppointmentTable = true,
                DoAppointmentTypeTable = true,
                DoCalendarAppointmentTable = true,
                DoCalendarTable = true,
                DoNoteTable = true,
                DoNoteTypeTable = true,
                DoSalesOrderTable = true,
            };
        }
    }
}
