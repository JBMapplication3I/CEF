using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Address
    {
        public Address()
        {
            Contacts = new HashSet<Contact>();
            Events = new HashSet<Event>();
            Iporganizations = new HashSet<Iporganization>();
            PageViews = new HashSet<PageView>();
            ServiceAreas = new HashSet<ServiceArea>();
            ShipmentEvents = new HashSet<ShipmentEvent>();
            Visitors = new HashSet<Visitor>();
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? Street3 { get; set; }
        public string? City { get; set; }
        public string? RegionCustom { get; set; }
        public string? CountryCustom { get; set; }
        public string? PostalCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Country? Country { get; set; }
        public virtual Region? Region { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Iporganization> Iporganizations { get; set; }
        public virtual ICollection<PageView> PageViews { get; set; }
        public virtual ICollection<ServiceArea> ServiceAreas { get; set; }
        public virtual ICollection<ShipmentEvent> ShipmentEvents { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
