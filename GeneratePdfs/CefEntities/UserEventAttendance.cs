using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class UserEventAttendance
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int TypeId { get; set; }
        public int SlaveId { get; set; }
        public int MasterId { get; set; }
        public bool HasAttended { get; set; }
        public DateTime Date { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual CalendarEvent Master { get; set; } = null!;
        public virtual User Slave { get; set; } = null!;
        public virtual UserEventAttendanceType Type { get; set; } = null!;
    }
}
