// <copyright file="CalendarEventSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;

    public partial class CalendarEventSearchModel
    {
        // TODO: ApiMember Properties

        /// <inheritdoc/>
        public string?[]? UserAttendanceTypeKeys { get; set; }

        /// <inheritdoc/>
        public DateTime? EndDate { get; set; }

        /// <inheritdoc/>
        public int? DaysUntilDeparture { get; set; }

        /// <inheritdoc/>
        public int? StrictDaysUntilDeparture { get; set; }

        /// <inheritdoc/>
        public int? UserID { get; set; }

        /// <inheritdoc/>
        public bool? CurrentEventsOnly { get; set; }
    }
}
