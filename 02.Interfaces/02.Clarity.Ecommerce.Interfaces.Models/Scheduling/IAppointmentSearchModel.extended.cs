// <copyright file="IAppointmentSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Appointment Search Model interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    public partial interface IAppointmentSearchModel
    {
        /// <summary>Gets or sets the start of the date/time to filter the results by. Note that appointments will be
        /// returned if they overlap this period at all.</summary>
        /// <value>The start of the date/time to filter the results by.</value>
        DateTime? FilterStart { get; set; }

        /// <summary>Gets or sets the end of the date/time to filter the results by. Note that appointments will be
        /// returned if they overlap this period at all.</summary>
        /// <value>The end of the date/time to filter the results by.</value>
        DateTime? FilterEnd { get; set; }

        /// <summary>Gets or sets a list of calendars to find appointments on.</summary>
        /// <value>A list of calendars to find appointments on.</value>
        int[]? CalendarIDs { get; set; }
    }
}
