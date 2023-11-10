// <copyright file="CalendarEventModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using ServiceStack;

    public partial class CalendarEventModel
    {
        #region CalendarEvent Properties
        /// <inheritdoc/>
        [ApiMember(Name = nameof(StartDate), DataType = "DateTime", ParameterType = "body", IsRequired = false,
            Description = "This is the start date for when the event occurs. (Optional, will use DateTime.MinValue when not set)")]
        public DateTime StartDate { get; set; }

        /// <inheritdoc/>
        public DateTime EndDate { get; set; }

        /// <inheritdoc/>
        public int EventDuration { get; set; }

        /// <inheritdoc/>
        public string? EventDurationUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public int MaxAttendees { get; set; }

        /// <inheritdoc/>
        public string? RecurrenceString { get; set; }

        /// <inheritdoc/>
        public string? ShortDescription { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? UserID { get; set; }

        /// <inheritdoc cref="ICalendarEventModel.User"/>
        public UserModel? User { get; set; }

        /// <inheritdoc/>
        IUserModel? ICalendarEventModel.User { get => User; set => User = (UserModel?)value; }

        /// <inheritdoc/>
        public int? GroupID { get; set; }

        /// <inheritdoc/>
        public string? GroupKey { get; set; }

        /// <inheritdoc/>
        public string? GroupName { get; set; }

        /// <inheritdoc cref="ICalendarEventModel.Group"/>
        public GroupModel? Group { get; set; }

        /// <inheritdoc/>
        IGroupModel? ICalendarEventModel.Group { get => Group; set => Group = (GroupModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="ICalendarEventModel.CalendarEventDetails"/>
        public List<CalendarEventDetailModel>? CalendarEventDetails { get; set; }

        /// <inheritdoc/>
        List<ICalendarEventDetailModel>? ICalendarEventModel.CalendarEventDetails { get => CalendarEventDetails?.ToList<ICalendarEventDetailModel>(); set => CalendarEventDetails = value?.Cast<CalendarEventDetailModel>().ToList(); }

        /// <inheritdoc cref="ICalendarEventModel.CalendarEventProducts"/>
        public List<CalendarEventProductModel>? CalendarEventProducts { get; set; }

        /// <inheritdoc/>
        List<ICalendarEventProductModel>? ICalendarEventModel.CalendarEventProducts { get => CalendarEventProducts?.ToList<ICalendarEventProductModel>(); set => CalendarEventProducts = value?.Cast<CalendarEventProductModel>().ToList(); }

        /// <inheritdoc cref="ICalendarEventModel.UserEventAttendances"/>
        public List<UserEventAttendanceModel>? UserEventAttendances { get; set; }

        /// <inheritdoc/>
        List<IUserEventAttendanceModel>? ICalendarEventModel.UserEventAttendances { get => UserEventAttendances?.ToList<IUserEventAttendanceModel>(); set => UserEventAttendances = value?.Cast<UserEventAttendanceModel>().ToList(); }
        #endregion

        #region Convenience Properties
        /// <inheritdoc/>
        public int CurrentAttendeesCount => UserEventAttendances?.Count(u => u.Active) ?? 0;

        /// <summary>Gets or sets the dates.</summary>
        /// <value>The dates.</value>
        public string? Dates { get; set; }
        #endregion
    }
}
