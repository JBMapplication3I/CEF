using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Counter
    {
        public Counter()
        {
            AdClickCounters = new HashSet<Ad>();
            AdImpressionCounters = new HashSet<Ad>();
            AdZoneAccessClickCounters = new HashSet<AdZoneAccess>();
            AdZoneAccessImpressionCounters = new HashSet<AdZoneAccess>();
            AdZoneClickCounters = new HashSet<AdZone>();
            AdZoneImpressionCounters = new HashSet<AdZone>();
            CounterLogs = new HashSet<CounterLog>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int TypeId { get; set; }
        public string? JsonAttributes { get; set; }
        public decimal? Value { get; set; }
        public long? Hash { get; set; }

        public virtual CounterType Type { get; set; } = null!;
        public virtual ICollection<Ad> AdClickCounters { get; set; }
        public virtual ICollection<Ad> AdImpressionCounters { get; set; }
        public virtual ICollection<AdZoneAccess> AdZoneAccessClickCounters { get; set; }
        public virtual ICollection<AdZoneAccess> AdZoneAccessImpressionCounters { get; set; }
        public virtual ICollection<AdZone> AdZoneClickCounters { get; set; }
        public virtual ICollection<AdZone> AdZoneImpressionCounters { get; set; }
        public virtual ICollection<CounterLog> CounterLogs { get; set; }
    }
}
