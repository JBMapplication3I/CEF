using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class AdZone
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public int? AdZoneAccessId { get; set; }
        public int? ImpressionCounterId { get; set; }
        public int? ClickCounterId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual AdZoneAccess? AdZoneAccess { get; set; }
        public virtual Counter? ClickCounter { get; set; }
        public virtual Counter? ImpressionCounter { get; set; }
        public virtual Ad Master { get; set; } = null!;
        public virtual Zone Slave { get; set; } = null!;
    }
}
