using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Iporganization
    {
        public Iporganization()
        {
            Events = new HashSet<Event>();
            PageViews = new HashSet<PageView>();
            Visitors = new HashSet<Visitor>();
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int StatusId { get; set; }
        public string? Ipaddress { get; set; }
        public int? Score { get; set; }
        public string? VisitorKey { get; set; }
        public int? AddressId { get; set; }
        public int? PrimaryUserId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Address? Address { get; set; }
        public virtual User? PrimaryUser { get; set; }
        public virtual IporganizationStatus Status { get; set; } = null!;
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<PageView> PageViews { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
