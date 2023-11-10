using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Visit
    {
        public Visit()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int StatusId { get; set; }
        public string? JsonAttributes { get; set; }
        public string? Ipaddress { get; set; }
        public int? Score { get; set; }
        public int? AddressId { get; set; }
        public int? IporganizationId { get; set; }
        public int? UserId { get; set; }
        public bool? DidBounce { get; set; }
        public string? OperatingSystem { get; set; }
        public string? Browser { get; set; }
        public string? Language { get; set; }
        public bool? ContainsSocialProfile { get; set; }
        public int? Delta { get; set; }
        public int? Duration { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? EndedOn { get; set; }
        public string? Time { get; set; }
        public string? EntryPage { get; set; }
        public string? ExitPage { get; set; }
        public bool? IsFirstTrigger { get; set; }
        public string? Flash { get; set; }
        public string? Keywords { get; set; }
        public string? PartitionKey { get; set; }
        public string? Referrer { get; set; }
        public string? ReferringHost { get; set; }
        public string? RowKey { get; set; }
        public int? Source { get; set; }
        public int? CampaignId { get; set; }
        public int? ContactId { get; set; }
        public int? SiteDomainId { get; set; }
        public int? VisitorId { get; set; }
        public int? TotalTriggers { get; set; }
        public long? Hash { get; set; }

        public virtual Address? Address { get; set; }
        public virtual Campaign? Campaign { get; set; }
        public virtual Contact? Contact { get; set; }
        public virtual Iporganization? Iporganization { get; set; }
        public virtual SiteDomain? SiteDomain { get; set; }
        public virtual VisitStatus Status { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual Visitor? Visitor { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
