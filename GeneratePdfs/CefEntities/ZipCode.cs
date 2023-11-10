using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ZipCode
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? ZipCode1 { get; set; }
        public string? ZipType { get; set; }
        public string? CityName { get; set; }
        public string? CityType { get; set; }
        public string? CountyName { get; set; }
        public long? CountyFips { get; set; }
        public string? StateName { get; set; }
        public string? StateAbbreviation { get; set; }
        public long? StateFips { get; set; }
        public long? Msacode { get; set; }
        public string? AreaCode { get; set; }
        public string? TimeZone { get; set; }
        public long? Utc { get; set; }
        public string? Dst { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
    }
}
