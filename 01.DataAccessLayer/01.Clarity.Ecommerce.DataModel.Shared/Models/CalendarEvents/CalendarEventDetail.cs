// <copyright file="CalendarEventDetail.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event detail class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    /// <summary>Interface for calendar event detail.</summary>
    public interface ICalendarEventDetail : INameableBase
    {
        #region CalendarEventDetail Properties
        /// <summary>Gets or sets the day.</summary>
        /// <value>The day.</value>
        int Day { get; set; }

        /// <summary>Gets or sets the start time.</summary>
        /// <value>The start time.</value>
        DateTime? StartTime { get; set; }

        /// <summary>Gets or sets the end time.</summary>
        /// <value>The end time.</value>
        DateTime? EndTime { get; set; }

        /// <summary>Gets or sets the location.</summary>
        /// <value>The location.</value>
        string? Location { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the calendar event.</summary>
        /// <value>The identifier of the calendar event.</value>
        int CalendarEventID { get; set; }

        /// <summary>Gets or sets the calendar event.</summary>
        /// <value>The calendar event.</value>
        CalendarEvent? CalendarEvent { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("CalendarEvents", "CalendarEventDetail")]
    public class CalendarEventDetail : NameableBase, ICalendarEventDetail
    {
        #region CalendarEventDetail Properties
        /// <inheritdoc/>
        [DefaultValue(0)]
        public int Day { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? StartTime { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? EndTime { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), DefaultValue(null)]
        public string? Location { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(CalendarEvent)), DefaultValue(0)]
        public int CalendarEventID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, ForceMapOutWithLite]
        public virtual CalendarEvent? CalendarEvent { get; set; }
        #endregion
    }
}
