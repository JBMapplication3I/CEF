using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class HistoricalTaxRate
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? Provider { get; set; }
        public long? CartHash { get; set; }
        public DateTime OnDate { get; set; }
        public decimal? CountryLevelRate { get; set; }
        public decimal? RegionLevelRate { get; set; }
        public decimal? DistrictLevelRate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalTaxable { get; set; }
        public decimal? TotalTax { get; set; }
        public decimal? TotalTaxCalculated { get; set; }
        public decimal Rate { get; set; }
        public string? SerializedRequest { get; set; }
        public string? SerializedResponse { get; set; }
    }
}
