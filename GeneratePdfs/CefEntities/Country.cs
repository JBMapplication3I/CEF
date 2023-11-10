using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Country
    {
        public Country()
        {
            Addresses = new HashSet<Address>();
            CountryCurrencies = new HashSet<CountryCurrency>();
            CountryImages = new HashSet<CountryImage>();
            CountryLanguages = new HashSet<CountryLanguage>();
            DiscountCountries = new HashSet<DiscountCountry>();
            Districts = new HashSet<District>();
            FranchiseCountries = new HashSet<FranchiseCountry>();
            PhonePrefixLookups = new HashSet<PhonePrefixLookup>();
            PriceRuleCountries = new HashSet<PriceRuleCountry>();
            ProductRestrictions = new HashSet<ProductRestriction>();
            Regions = new HashSet<Region>();
            StoreCountries = new HashSet<StoreCountry>();
            TaxCountries = new HashSet<TaxCountry>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? PhoneRegEx { get; set; }
        public string? PhonePrefix { get; set; }
        public string? Iso3166alpha2 { get; set; }
        public string? Iso3166alpha3 { get; set; }
        public int? Iso3166numeric { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<CountryCurrency> CountryCurrencies { get; set; }
        public virtual ICollection<CountryImage> CountryImages { get; set; }
        public virtual ICollection<CountryLanguage> CountryLanguages { get; set; }
        public virtual ICollection<DiscountCountry> DiscountCountries { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<FranchiseCountry> FranchiseCountries { get; set; }
        public virtual ICollection<PhonePrefixLookup> PhonePrefixLookups { get; set; }
        public virtual ICollection<PriceRuleCountry> PriceRuleCountries { get; set; }
        public virtual ICollection<ProductRestriction> ProductRestrictions { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
        public virtual ICollection<StoreCountry> StoreCountries { get; set; }
        public virtual ICollection<TaxCountry> TaxCountries { get; set; }
    }
}
