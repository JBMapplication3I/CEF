// <copyright file="OpenTimeSlotModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the OpenTimeSlotModel class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <inheritdoc/>
    public class OpenTimeSlotModel : IOpenTimeSlotModel
    {
        /// <inheritdoc/>
        public DateTime Start { get; set; }

        /// <inheritdoc/>
        public DateTime End { get; set; }

        /// <inheritdoc/>
        public int CalendarID { get; set; }

        /// <inheritdoc/>
        public string? CalendarKey { get; set; }

        /// <inheritdoc cref="IOpenTimeSlotModel.Calendar"/>
        public CalendarModel? Calendar { get; set; }

        /// <inheritdoc/>
        ICalendarModel? IOpenTimeSlotModel.Calendar { get => Calendar; set => Calendar = (CalendarModel?)value; }
    }
}
