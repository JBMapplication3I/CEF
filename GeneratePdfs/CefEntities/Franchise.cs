using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Franchise
    {
        public Franchise()
        {
            AdFranchises = new HashSet<AdFranchise>();
            BrandFranchises = new HashSet<BrandFranchise>();
            Carts = new HashSet<Cart>();
            DiscountFranchises = new HashSet<DiscountFranchise>();
            FranchiseAccounts = new HashSet<FranchiseAccount>();
            FranchiseAuctions = new HashSet<FranchiseAuction>();
            FranchiseCategories = new HashSet<FranchiseCategory>();
            FranchiseCountries = new HashSet<FranchiseCountry>();
            FranchiseCurrencies = new HashSet<FranchiseCurrency>();
            FranchiseDistricts = new HashSet<FranchiseDistrict>();
            FranchiseImages = new HashSet<FranchiseImage>();
            FranchiseInventoryLocations = new HashSet<FranchiseInventoryLocation>();
            FranchiseLanguages = new HashSet<FranchiseLanguage>();
            FranchiseManufacturers = new HashSet<FranchiseManufacturer>();
            FranchiseProducts = new HashSet<FranchiseProduct>();
            FranchiseRegions = new HashSet<FranchiseRegion>();
            FranchiseSiteDomains = new HashSet<FranchiseSiteDomain>();
            FranchiseStores = new HashSet<FranchiseStore>();
            FranchiseUsers = new HashSet<FranchiseUser>();
            FranchiseVendors = new HashSet<FranchiseVendor>();
            Notes = new HashSet<Note>();
            ProductPricePoints = new HashSet<ProductPricePoint>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SalesInvoices = new HashSet<SalesInvoice>();
            SalesOrders = new HashSet<SalesOrder>();
            SalesQuotes = new HashSet<SalesQuote>();
            SalesReturns = new HashSet<SalesReturn>();
            SampleRequests = new HashSet<SampleRequest>();
        }

        public int Id { get; set; }
        public decimal? MinimumOrderDollarAmount { get; set; }
        public decimal? MinimumOrderDollarAmountAfter { get; set; }
        public string? MinimumOrderDollarAmountWarningMessage { get; set; }
        public decimal? MinimumOrderDollarAmountOverrideFee { get; set; }
        public bool MinimumOrderDollarAmountOverrideFeeIsPercent { get; set; }
        public string? MinimumOrderDollarAmountOverrideFeeWarningMessage { get; set; }
        public string? MinimumOrderDollarAmountOverrideFeeAcceptedMessage { get; set; }
        public decimal? MinimumOrderQuantityAmount { get; set; }
        public decimal? MinimumOrderQuantityAmountAfter { get; set; }
        public string? MinimumOrderQuantityAmountWarningMessage { get; set; }
        public decimal? MinimumOrderQuantityAmountOverrideFee { get; set; }
        public bool MinimumOrderQuantityAmountOverrideFeeIsPercent { get; set; }
        public string? MinimumOrderQuantityAmountOverrideFeeWarningMessage { get; set; }
        public string? MinimumOrderQuantityAmountOverrideFeeAcceptedMessage { get; set; }
        public int? MinimumOrderDollarAmountBufferProductId { get; set; }
        public int? MinimumOrderQuantityAmountBufferProductId { get; set; }
        public int? MinimumOrderDollarAmountBufferCategoryId { get; set; }
        public int? MinimumOrderQuantityAmountBufferCategoryId { get; set; }
        public decimal? MinimumForFreeShippingDollarAmount { get; set; }
        public decimal? MinimumForFreeShippingDollarAmountAfter { get; set; }
        public string? MinimumForFreeShippingDollarAmountWarningMessage { get; set; }
        public string? MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage { get; set; }
        public decimal? MinimumForFreeShippingQuantityAmount { get; set; }
        public decimal? MinimumForFreeShippingQuantityAmountAfter { get; set; }
        public string? MinimumForFreeShippingQuantityAmountWarningMessage { get; set; }
        public string? MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage { get; set; }
        public int? MinimumForFreeShippingDollarAmountBufferProductId { get; set; }
        public int? MinimumForFreeShippingQuantityAmountBufferProductId { get; set; }
        public int? MinimumForFreeShippingDollarAmountBufferCategoryId { get; set; }
        public int? MinimumForFreeShippingQuantityAmountBufferCategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public int TypeId { get; set; }

        public virtual Category? MinimumForFreeShippingDollarAmountBufferCategory { get; set; }
        public virtual Product? MinimumForFreeShippingDollarAmountBufferProduct { get; set; }
        public virtual Category? MinimumForFreeShippingQuantityAmountBufferCategory { get; set; }
        public virtual Product? MinimumForFreeShippingQuantityAmountBufferProduct { get; set; }
        public virtual Category? MinimumOrderDollarAmountBufferCategory { get; set; }
        public virtual Product? MinimumOrderDollarAmountBufferProduct { get; set; }
        public virtual Category? MinimumOrderQuantityAmountBufferCategory { get; set; }
        public virtual Product? MinimumOrderQuantityAmountBufferProduct { get; set; }
        public virtual FranchiseType Type { get; set; } = null!;
        public virtual ICollection<AdFranchise> AdFranchises { get; set; }
        public virtual ICollection<BrandFranchise> BrandFranchises { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<DiscountFranchise> DiscountFranchises { get; set; }
        public virtual ICollection<FranchiseAccount> FranchiseAccounts { get; set; }
        public virtual ICollection<FranchiseAuction> FranchiseAuctions { get; set; }
        public virtual ICollection<FranchiseCategory> FranchiseCategories { get; set; }
        public virtual ICollection<FranchiseCountry> FranchiseCountries { get; set; }
        public virtual ICollection<FranchiseCurrency> FranchiseCurrencies { get; set; }
        public virtual ICollection<FranchiseDistrict> FranchiseDistricts { get; set; }
        public virtual ICollection<FranchiseImage> FranchiseImages { get; set; }
        public virtual ICollection<FranchiseInventoryLocation> FranchiseInventoryLocations { get; set; }
        public virtual ICollection<FranchiseLanguage> FranchiseLanguages { get; set; }
        public virtual ICollection<FranchiseManufacturer> FranchiseManufacturers { get; set; }
        public virtual ICollection<FranchiseProduct> FranchiseProducts { get; set; }
        public virtual ICollection<FranchiseRegion> FranchiseRegions { get; set; }
        public virtual ICollection<FranchiseSiteDomain> FranchiseSiteDomains { get; set; }
        public virtual ICollection<FranchiseStore> FranchiseStores { get; set; }
        public virtual ICollection<FranchiseUser> FranchiseUsers { get; set; }
        public virtual ICollection<FranchiseVendor> FranchiseVendors { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<ProductPricePoint> ProductPricePoints { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
        public virtual ICollection<SalesQuote> SalesQuotes { get; set; }
        public virtual ICollection<SalesReturn> SalesReturns { get; set; }
        public virtual ICollection<SampleRequest> SampleRequests { get; set; }
    }
}
