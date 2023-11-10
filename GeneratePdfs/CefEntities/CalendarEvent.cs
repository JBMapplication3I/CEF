using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class CalendarEvent
    {
        public CalendarEvent()
        {
            CalendarEventDetails = new HashSet<CalendarEventDetail>();
            CalendarEventFiles = new HashSet<CalendarEventFile>();
            CalendarEventImages = new HashSet<CalendarEventImage>();
            CalendarEventProducts = new HashSet<CalendarEventProduct>();
            UserEventAttendances = new HashSet<UserEventAttendance>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int ContactId { get; set; }
        public string? JsonAttributes { get; set; }
        public string? ShortDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EventDuration { get; set; }
        public string? EventDurationUnitOfMeasure { get; set; }
        public string? RecurrenceString { get; set; }
        public int MaxAttendees { get; set; }
        public int? GroupId { get; set; }
        public long? Hash { get; set; }

        public virtual Contact Contact { get; set; } = null!;
        public virtual Group? Group { get; set; }
        public virtual CalendarEventStatus Status { get; set; } = null!;
        public virtual CalendarEventType Type { get; set; } = null!;
        public virtual ICollection<CalendarEventDetail> CalendarEventDetails { get; set; }
        public virtual ICollection<CalendarEventFile> CalendarEventFiles { get; set; }
        public virtual ICollection<CalendarEventImage> CalendarEventImages { get; set; }
        public virtual ICollection<CalendarEventProduct> CalendarEventProducts { get; set; }
        public virtual ICollection<UserEventAttendance> UserEventAttendances { get; set; }
    }
}
