// <copyright file="CalendarEventDetailModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event detail model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>A data Model for the calendar event detail.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="ICalendarEventDetailModel"/>
    public partial class CalendarEventDetailModel
    {
        /// <inheritdoc/>
        public int Day { get; set; }

        /// <inheritdoc/>
        public DateTime? EndTime { get; set; }

        /// <inheritdoc/>
        public DateTime? StartTime { get; set; }

        /// <inheritdoc/>
        public string? Location { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int CalendarEventID { get; set; }

        /// <inheritdoc/>
        public string? CalendarEventKey { get; set; }

        /// <inheritdoc/>
        public string? CalendarEventName { get; set; }

        /// <inheritdoc cref="ICalendarEventDetailModel.CalendarEvent"/>
        public CalendarEventModel? CalendarEvent { get; set; }

        /// <inheritdoc/>
        ICalendarEventModel? ICalendarEventDetailModel.CalendarEvent { get => CalendarEvent; set => CalendarEvent = (CalendarEventModel?)value; }
        #endregion
    }
}
