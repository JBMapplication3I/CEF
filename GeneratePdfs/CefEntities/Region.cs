using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Region
    {
        public Region()
        {
            Addresses = new HashSet<Address>();
            Districts = new HashSet<District>();
            FranchiseRegions = new HashSet<FranchiseRegion>();
            InventoryLocationRegions = new HashSet<InventoryLocationRegion>();
            PhonePrefixLookups = new HashSet<PhonePrefixLookup>();
            ProductRestrictions = new HashSet<ProductRestriction>();
            RegionCurrencies = new HashSet<RegionCurrency>();
            RegionImages = new HashSet<RegionImage>();
            RegionLanguages = new HashSet<RegionLanguage>();
            StoreRegions = new HashSet<StoreRegion>();
            TaxRegions = new HashSet<TaxRegion>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
        public int CountryId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? Iso31661 { get; set; }
        public string? Iso31662 { get; set; }
        public string? Iso3166alpha2 { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<FranchiseRegion> FranchiseRegions { get; set; }
        public virtual ICollection<InventoryLocationRegion> InventoryLocationRegions { get; set; }
        public virtual ICollection<PhonePrefixLookup> PhonePrefixLookups { get; set; }
        public virtual ICollection<ProductRestriction> ProductRestrictions { get; set; }
        public virtual ICollection<RegionCurrency> RegionCurrencies { get; set; }
        public virtual ICollection<RegionImage> RegionImages { get; set; }
        public virtual ICollection<RegionLanguage> RegionLanguages { get; set; }
        public virtual ICollection<StoreRegion> StoreRegions { get; set; }
        public virtual ICollection<TaxRegion> TaxRegions { get; set; }
    }
}
