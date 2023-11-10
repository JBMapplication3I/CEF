// <copyright file="CalendarEvent.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ICalendarEvent
        : INameableBase,
            IHaveATypeBase<CalendarEventType>,
            IHaveAStatusBase<CalendarEventStatus>,
            IHaveAContactBase,
            IAmFilterableByProduct<CalendarEventProduct>,
            IHaveStoredFilesBase<CalendarEvent, CalendarEventFile>,
            IHaveImagesBase<CalendarEvent, CalendarEventImage, CalendarEventImageType>
    {
        #region CalendarEvent Properties
        /// <summary>Gets or sets information describing the short.</summary>
        /// <value>Information describing the short.</value>
        string? ShortDescription { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime EndDate { get; set; }

        /// <summary>Gets or sets the duration of the event.</summary>
        /// <value>The event duration.</value>
        int EventDuration { get; set; }

        /// <summary>Gets or sets the event duration unit of measure.</summary>
        /// <value>The event duration unit of measure.</value>
        string? EventDurationUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the recurrence string.</summary>
        /// <value>The recurrence string.</value>
        string? RecurrenceString { get; set; }

        /// <summary>Gets or sets the maximum attendees.</summary>
        /// <value>The maximum attendees.</value>
        int MaxAttendees { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the group.</summary>
        /// <value>The identifier of the group.</value>
        int? GroupID { get; set; }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The group.</value>
        Group? Group { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the calendar event details.</summary>
        /// <value>The calendar event details.</value>
        ICollection<CalendarEventDetail>? CalendarEventDetails { get; set; }

        /// <summary>Gets or sets the user event attendances.</summary>
        /// <value>The user event attendances.</value>
        ICollection<UserEventAttendance>? UserEventAttendances { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("CalendarEvents", "CalendarEvent")]
    public class CalendarEvent : NameableBase, ICalendarEvent
    {
        private ICollection<CalendarEventImage>? images;
        private ICollection<CalendarEventFile>? storedFiles;
        private ICollection<CalendarEventProduct>? products;
        private ICollection<CalendarEventDetail>? calendarEventDetails;
        private ICollection<UserEventAttendance>? userEventAttendances;

        public CalendarEvent()
        {
            // IHaveImagesBase
            images = new HashSet<CalendarEventImage>();
            // IHaveStoredFilesBase
            storedFiles = new HashSet<CalendarEventFile>();
            // IAmFilterableByProduct
            products = new HashSet<CalendarEventProduct>();
            // CalendarEvent Specific
            calendarEventDetails = new HashSet<CalendarEventDetail>();
            userEventAttendances = new HashSet<UserEventAttendance>();
        }

        #region IHaveATypeBase<CalendarEventType>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual CalendarEventType? Type { get; set; }
        #endregion

        #region IHaveAStatusBase<CalendarEventStatus>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual CalendarEventStatus? Status { get; set; }
        #endregion

        #region IHaveAContactBase
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Contact)), DefaultValue(0)]
        public int ContactID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Contact? Contact { get; set; }
        #endregion

        #region IHaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<CalendarEventImage>? Images { get => images; set => images = value; }
        #endregion

        #region IHaveStoredFilesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<CalendarEventFile>? StoredFiles { get => storedFiles; set => storedFiles = value; }
        #endregion

        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<CalendarEventProduct>? Products { get => products; set => products = value; }
        #endregion

        #region CalendarEvent Properties
        /// <inheritdoc/>
        [StringLength(0256), StringIsUnicode(false), DefaultValue(null)]
        public string? ShortDescription { get; set; }

        /// <inheritdoc/>
        [StringLength(0128), StringIsUnicode(false), DefaultValue(null)]
        public string? EventDurationUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), StringIsUnicode(false), DefaultValue(null)]
        public string? RecurrenceString { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime StartDate { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime EndDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int EventDuration { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int MaxAttendees { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Group)), DefaultValue(null)]
        public int? GroupID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, ForceMapOutWithLite]
        public virtual Group? Group { get; set; }
        #endregion

        #region Associated objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<CalendarEventDetail>? CalendarEventDetails { get => calendarEventDetails; set => calendarEventDetails = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<UserEventAttendance>? UserEventAttendances { get => userEventAttendances; set => userEventAttendances = value; }
        #endregion
    }
}
