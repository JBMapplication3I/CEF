using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Zone
    {
        public Zone()
        {
            AdZoneAccesses = new HashSet<AdZoneAccess>();
            AdZones = new HashSet<AdZone>();
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
        public int Width { get; set; }
        public int Height { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual ZoneStatus Status { get; set; } = null!;
        public virtual ZoneType Type { get; set; } = null!;
        public virtual ICollection<AdZoneAccess> AdZoneAccesses { get; set; }
        public virtual ICollection<AdZone> AdZones { get; set; }
    }
}
