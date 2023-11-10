using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class District
    {
        public District()
        {
            DistrictCurrencies = new HashSet<DistrictCurrency>();
            DistrictImages = new HashSet<DistrictImage>();
            DistrictLanguages = new HashSet<DistrictLanguage>();
            FranchiseDistricts = new HashSet<FranchiseDistrict>();
            StoreDistricts = new HashSet<StoreDistrict>();
            TaxDistricts = new HashSet<TaxDistrict>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public int? RegionId { get; set; }
        public int CountryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual Region? Region { get; set; }
        public virtual ICollection<DistrictCurrency> DistrictCurrencies { get; set; }
        public virtual ICollection<DistrictImage> DistrictImages { get; set; }
        public virtual ICollection<DistrictLanguage> DistrictLanguages { get; set; }
        public virtual ICollection<FranchiseDistrict> FranchiseDistricts { get; set; }
        public virtual ICollection<StoreDistrict> StoreDistricts { get; set; }
        public virtual ICollection<TaxDistrict> TaxDistricts { get; set; }
    }
}
