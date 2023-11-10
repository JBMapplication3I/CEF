using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Calendar
    {
        public Calendar()
        {
            CalendarAppointments = new HashSet<CalendarAppointment>();
        }

        public int Id { get; set; }
        public decimal? MondayHoursStart { get; set; }
        public decimal? MondayHoursEnd { get; set; }
        public decimal? TuesdayHoursStart { get; set; }
        public decimal? TuesdayHoursEnd { get; set; }
        public decimal? WednesdayHoursStart { get; set; }
        public decimal? WednesdayHoursEnd { get; set; }
        public decimal? ThursdayHoursStart { get; set; }
        public decimal? ThursdayHoursEnd { get; set; }
        public decimal? FridayHoursStart { get; set; }
        public decimal? FridayHoursEnd { get; set; }
        public decimal? SaturdayHoursStart { get; set; }
        public decimal? SaturdayHoursEnd { get; set; }
        public decimal? SundayHoursStart { get; set; }
        public decimal? SundayHoursEnd { get; set; }
        public int AccountId { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<CalendarAppointment> CalendarAppointments { get; set; }
    }
}
