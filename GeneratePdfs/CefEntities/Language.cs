using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Language
    {
        public Language()
        {
            BrandLanguages = new HashSet<BrandLanguage>();
            CountryLanguages = new HashSet<CountryLanguage>();
            DistrictLanguages = new HashSet<DistrictLanguage>();
            FranchiseLanguages = new HashSet<FranchiseLanguage>();
            LanguageImages = new HashSet<LanguageImage>();
            RegionLanguages = new HashSet<RegionLanguage>();
            Stores = new HashSet<Store>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Locale { get; set; }
        public string? UnicodeName { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? Iso63912002 { get; set; }
        public string? Iso63921998 { get; set; }
        public string? Iso63932007 { get; set; }
        public string? Iso63952008 { get; set; }

        public virtual ICollection<BrandLanguage> BrandLanguages { get; set; }
        public virtual ICollection<CountryLanguage> CountryLanguages { get; set; }
        public virtual ICollection<DistrictLanguage> DistrictLanguages { get; set; }
        public virtual ICollection<FranchiseLanguage> FranchiseLanguages { get; set; }
        public virtual ICollection<LanguageImage> LanguageImages { get; set; }
        public virtual ICollection<RegionLanguage> RegionLanguages { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
