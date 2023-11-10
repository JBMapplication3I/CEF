using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class PriceRule
    {
        public PriceRule()
        {
            PriceRuleAccountTypes = new HashSet<PriceRuleAccountType>();
            PriceRuleAccounts = new HashSet<PriceRuleAccount>();
            PriceRuleBrands = new HashSet<PriceRuleBrand>();
            PriceRuleCategories = new HashSet<PriceRuleCategory>();
            PriceRuleCountries = new HashSet<PriceRuleCountry>();
            PriceRuleManufacturers = new HashSet<PriceRuleManufacturer>();
            PriceRuleProductTypes = new HashSet<PriceRuleProductType>();
            PriceRuleProducts = new HashSet<PriceRuleProduct>();
            PriceRuleStores = new HashSet<PriceRuleStore>();
            PriceRuleUserRoles = new HashSet<PriceRuleUserRole>();
            PriceRuleVendors = new HashSet<PriceRuleVendor>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal PriceAdjustment { get; set; }
        public bool IsPercentage { get; set; }
        public bool IsMarkup { get; set; }
        public bool UsePriceBase { get; set; }
        public bool IsExclusive { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public decimal? MinQuantity { get; set; }
        public decimal? MaxQuantity { get; set; }
        public bool IsOnlyForAnonymousUsers { get; set; }
        public int? CurrencyId { get; set; }
        public int? Priority { get; set; }
        public string? UnitOfMeasure { get; set; }

        public virtual Currency? Currency { get; set; }
        public virtual ICollection<PriceRuleAccountType> PriceRuleAccountTypes { get; set; }
        public virtual ICollection<PriceRuleAccount> PriceRuleAccounts { get; set; }
        public virtual ICollection<PriceRuleBrand> PriceRuleBrands { get; set; }
        public virtual ICollection<PriceRuleCategory> PriceRuleCategories { get; set; }
        public virtual ICollection<PriceRuleCountry> PriceRuleCountries { get; set; }
        public virtual ICollection<PriceRuleManufacturer> PriceRuleManufacturers { get; set; }
        public virtual ICollection<PriceRuleProductType> PriceRuleProductTypes { get; set; }
        public virtual ICollection<PriceRuleProduct> PriceRuleProducts { get; set; }
        public virtual ICollection<PriceRuleStore> PriceRuleStores { get; set; }
        public virtual ICollection<PriceRuleUserRole> PriceRuleUserRoles { get; set; }
        public virtual ICollection<PriceRuleVendor> PriceRuleVendors { get; set; }
    }
}
