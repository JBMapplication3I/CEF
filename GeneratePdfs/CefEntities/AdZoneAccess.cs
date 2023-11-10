using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class AdZoneAccess
    {
        public AdZoneAccess()
        {
            AdZones = new HashSet<AdZone>();
            MembershipAdZoneAccesses = new HashSet<MembershipAdZoneAccess>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UniqueAdLimit { get; set; }
        public int ImpressionLimit { get; set; }
        public int ClickLimit { get; set; }
        public int? ZoneId { get; set; }
        public int? SubscriptionId { get; set; }
        public int? ImpressionCounterId { get; set; }
        public int? ClickCounterId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Counter? ClickCounter { get; set; }
        public virtual Counter? ImpressionCounter { get; set; }
        public virtual Subscription? Subscription { get; set; }
        public virtual Zone? Zone { get; set; }
        public virtual ICollection<AdZone> AdZones { get; set; }
        public virtual ICollection<MembershipAdZoneAccess> MembershipAdZoneAccesses { get; set; }
    }
}
