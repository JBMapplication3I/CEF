// <copyright file="ICalendarEventSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICalendarEventSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for calendar event search model.</summary>
    public partial interface ICalendarEventSearchModel
    {
        /// <summary>Gets or sets the excluded type keys.</summary>
        /// <value>The excluded type keys.</value>
        string?[]? UserAttendanceTypeKeys { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime? EndDate { get; set; }

        /// <summary>Gets or sets the days until departure.</summary>
        /// <value>The days until departure.</value>
        int? DaysUntilDeparture { get; set; }

        /// <summary>Gets or sets the strict days until departure.</summary>
        /// <value>The strict days until departure.</value>
        int? StrictDaysUntilDeparture { get; set; }

        /// <summary>Gets or sets the user ID.</summary>
        /// <value>The user ID to filter by.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the currentEventsOnly Flag.</summary>
        /// <value>Filter by current events (have not ended yet).</value>
        bool? CurrentEventsOnly { get; set; }
    }
}
