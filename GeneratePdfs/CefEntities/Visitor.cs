using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Visitor
    {
        public Visitor()
        {
            Events = new HashSet<Event>();
            PageViews = new HashSet<PageView>();
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? JsonAttributes { get; set; }
        public string? Ipaddress { get; set; }
        public int? Score { get; set; }
        public int? AddressId { get; set; }
        public int? IporganizationId { get; set; }
        public int? UserId { get; set; }
        public long? Hash { get; set; }

        public virtual Address? Address { get; set; }
        public virtual Iporganization? Iporganization { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<PageView> PageViews { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
