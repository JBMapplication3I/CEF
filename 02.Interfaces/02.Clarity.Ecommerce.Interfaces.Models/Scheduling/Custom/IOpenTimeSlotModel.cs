// <copyright file="IOpenTimeSlotModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IOpenTimeSlotModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>
    /// Describes an open slot of time on a calendar.
    /// </summary>
    public interface IOpenTimeSlotModel
    {
        /// <summary>Gets or sets the start of the open time block.</summary>
        /// <value>The start of the open time block.</value>
        DateTime Start { get; set; }

        /// <summary>Gets or sets the end of the open time block.</summary>
        /// <value>The end of the open time block.</value>
        DateTime End { get; set; }

        /// <summary>Gets or sets the ID of the calendar with the open slot.</summary>
        /// <value>The ID of the calendar with the open slot.</value>
        int CalendarID { get; set; }

        /// <summary>Gets or sets the custom key of the calendar with the open slot.</summary>
        /// <value>The custom key of the calendar with the open slot.</value>
        string? CalendarKey { get; set; }

        /// <summary>Gets or sets the calendar with the open slot.</summary>
        /// <value>The calendar with the open slot.</value>
        ICalendarModel? Calendar { get; set; }
    }
}
