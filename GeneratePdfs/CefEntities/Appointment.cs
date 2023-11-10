using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Appointment
    {
        public Appointment()
        {
            CalendarAppointments = new HashSet<CalendarAppointment>();
        }

        public int Id { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public DateTime? AppointmentStart { get; set; }
        public DateTime? AppointmentEnd { get; set; }
        public int? SalesOrderId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual SalesOrder? SalesOrder { get; set; }
        public virtual AppointmentStatus Status { get; set; } = null!;
        public virtual AppointmentType Type { get; set; } = null!;
        public virtual ICollection<CalendarAppointment> CalendarAppointments { get; set; }
    }
}
