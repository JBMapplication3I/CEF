using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class PhonePrefixLookup
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? Prefix { get; set; }
        public string? TimeZone { get; set; }
        public string? CityName { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Country? Country { get; set; }
        public virtual Region? Region { get; set; }
    }
}
