// <copyright file="ICalendarEventModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICalendarEventModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for calendar event model.</summary>
    public partial interface ICalendarEventModel
    {
        #region CalenderEvent Properties
        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime EndDate { get; set; }

        /// <summary>Gets the number of current attendees.</summary>
        /// <value>The number of current attendees.</value>
        int CurrentAttendeesCount { get; }

        /// <summary>Gets or sets the duration of the event.</summary>
        /// <value>The event duration.</value>
        int EventDuration { get; set; }

        /// <summary>Gets or sets the event duration unit of measure.</summary>
        /// <value>The event duration unit of measure.</value>
        string? EventDurationUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the maximum attendees.</summary>
        /// <value>The maximum attendees.</value>
        int MaxAttendees { get; set; }

        /// <summary>Gets or sets the recurrence string.</summary>
        /// <value>The recurrence string.</value>
        string? RecurrenceString { get; set; }

        /// <summary>Gets or sets information describing the short.</summary>
        /// <value>Information describing the short.</value>
        string? ShortDescription { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        IUserModel? User { get; set; }

        /// <summary>Gets or sets the identifier of the group.</summary>
        /// <value>The identifier of the group.</value>
        int? GroupID { get; set; }

        /// <summary>Gets or sets the group key.</summary>
        /// <value>The group key.</value>
        string? GroupKey { get; set; }

        /// <summary>Gets or sets the name of the group.</summary>
        /// <value>The name of the group.</value>
        string? GroupName { get; set; }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The group.</value>
        IGroupModel? Group { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the calendar event details.</summary>
        /// <value>The calendar event details.</value>
        List<ICalendarEventDetailModel>? CalendarEventDetails { get; set; }

        /// <summary>Gets or sets the calendar event products.</summary>
        /// <value>The calendar event products.</value>
        List<ICalendarEventProductModel>? CalendarEventProducts { get; set; }

        /// <summary>Gets or sets the user event attendances.</summary>
        /// <value>The user event attendances.</value>
        List<IUserEventAttendanceModel>? UserEventAttendances { get; set; }
        #endregion
    }
}
