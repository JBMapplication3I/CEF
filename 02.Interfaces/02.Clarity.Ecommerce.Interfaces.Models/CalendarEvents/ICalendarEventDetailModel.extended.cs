// <copyright file="ICalendarEventDetailModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICalendarEventDetailModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for calendar event detail model.</summary>
    public partial interface ICalendarEventDetailModel
    {
        /// <summary>Gets or sets the day.</summary>
        /// <value>The day.</value>
        int Day { get; set; }

        /// <summary>Gets or sets the end time.</summary>
        /// <value>The end time.</value>
        DateTime? EndTime { get; set; }

        /// <summary>Gets or sets the start time.</summary>
        /// <value>The start time.</value>
        DateTime? StartTime { get; set; }

        /// <summary>Gets or sets the location.</summary>
        /// <value>The location.</value>
        string? Location { get; set; }

        #region Related objects
        /// <summary>Gets or sets the identifier of the calendar event.</summary>
        /// <value>The identifier of the calendar event.</value>
        int CalendarEventID { get; set; }

        /// <summary>Gets or sets the calendar event key.</summary>
        /// <value>The calendar event key.</value>
        string? CalendarEventKey { get; set; }

        /// <summary>Gets or sets the name of the calendar event.</summary>
        /// <value>The name of the calendar event.</value>
        string? CalendarEventName { get; set; }

        /// <summary>Gets or sets the calendar event.</summary>
        /// <value>The calendar event.</value>
        ICalendarEventModel? CalendarEvent { get; set; }
        #endregion
    }
}
