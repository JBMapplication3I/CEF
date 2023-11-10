using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Brand
    {
        public Brand()
        {
            AdBrands = new HashSet<AdBrand>();
            BrandAccounts = new HashSet<BrandAccount>();
            BrandAuctions = new HashSet<BrandAuction>();
            BrandCategories = new HashSet<BrandCategory>();
            BrandCurrencies = new HashSet<BrandCurrency>();
            BrandFranchises = new HashSet<BrandFranchise>();
            BrandImages = new HashSet<BrandImage>();
            BrandInventoryLocations = new HashSet<BrandInventoryLocation>();
            BrandLanguages = new HashSet<BrandLanguage>();
            BrandManufacturers = new HashSet<BrandManufacturer>();
            BrandProducts = new HashSet<BrandProduct>();
            BrandSiteDomains = new HashSet<BrandSiteDomain>();
            BrandStores = new HashSet<BrandStore>();
            BrandUsers = new HashSet<BrandUser>();
            BrandVendors = new HashSet<BrandVendor>();
            CampaignTypes = new HashSet<CampaignType>();
            CartTypes = new HashSet<CartType>();
            Carts = new HashSet<Cart>();
            Conversations = new HashSet<Conversation>();
            DiscountBrands = new HashSet<DiscountBrand>();
            Messages = new HashSet<Message>();
            Notes = new HashSet<Note>();
            Payments = new HashSet<Payment>();
            PriceRuleBrands = new HashSet<PriceRuleBrand>();
            ProductAssociations = new HashSet<ProductAssociation>();
            ProductPricePoints = new HashSet<ProductPricePoint>();
            ProductShipCarrierMethods = new HashSet<ProductShipCarrierMethod>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            Questions = new HashSet<Question>();
            RecordVersions = new HashSet<RecordVersion>();
            SalesGroups = new HashSet<SalesGroup>();
            SalesInvoices = new HashSet<SalesInvoice>();
            SalesOrders = new HashSet<SalesOrder>();
            SalesQuotes = new HashSet<SalesQuote>();
            SalesReturns = new HashSet<SalesReturn>();
            SampleRequests = new HashSet<SampleRequest>();
            Settings = new HashSet<Setting>();
            SystemLogs = new HashSet<SystemLog>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
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

        public virtual Category? MinimumForFreeShippingDollarAmountBufferCategory { get; set; }
        public virtual Product? MinimumForFreeShippingDollarAmountBufferProduct { get; set; }
        public virtual Category? MinimumForFreeShippingQuantityAmountBufferCategory { get; set; }
        public virtual Product? MinimumForFreeShippingQuantityAmountBufferProduct { get; set; }
        public virtual Category? MinimumOrderDollarAmountBufferCategory { get; set; }
        public virtual Product? MinimumOrderDollarAmountBufferProduct { get; set; }
        public virtual Category? MinimumOrderQuantityAmountBufferCategory { get; set; }
        public virtual Product? MinimumOrderQuantityAmountBufferProduct { get; set; }
        public virtual ICollection<AdBrand> AdBrands { get; set; }
        public virtual ICollection<BrandAccount> BrandAccounts { get; set; }
        public virtual ICollection<BrandAuction> BrandAuctions { get; set; }
        public virtual ICollection<BrandCategory> BrandCategories { get; set; }
        public virtual ICollection<BrandCurrency> BrandCurrencies { get; set; }
        public virtual ICollection<BrandFranchise> BrandFranchises { get; set; }
        public virtual ICollection<BrandImage> BrandImages { get; set; }
        public virtual ICollection<BrandInventoryLocation> BrandInventoryLocations { get; set; }
        public virtual ICollection<BrandLanguage> BrandLanguages { get; set; }
        public virtual ICollection<BrandManufacturer> BrandManufacturers { get; set; }
        public virtual ICollection<BrandProduct> BrandProducts { get; set; }
        public virtual ICollection<BrandSiteDomain> BrandSiteDomains { get; set; }
        public virtual ICollection<BrandStore> BrandStores { get; set; }
        public virtual ICollection<BrandUser> BrandUsers { get; set; }
        public virtual ICollection<BrandVendor> BrandVendors { get; set; }
        public virtual ICollection<CampaignType> CampaignTypes { get; set; }
        public virtual ICollection<CartType> CartTypes { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Conversation> Conversations { get; set; }
        public virtual ICollection<DiscountBrand> DiscountBrands { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<PriceRuleBrand> PriceRuleBrands { get; set; }
        public virtual ICollection<ProductAssociation> ProductAssociations { get; set; }
        public virtual ICollection<ProductPricePoint> ProductPricePoints { get; set; }
        public virtual ICollection<ProductShipCarrierMethod> ProductShipCarrierMethods { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<RecordVersion> RecordVersions { get; set; }
        public virtual ICollection<SalesGroup> SalesGroups { get; set; }
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
        public virtual ICollection<SalesQuote> SalesQuotes { get; set; }
        public virtual ICollection<SalesReturn> SalesReturns { get; set; }
        public virtual ICollection<SampleRequest> SampleRequests { get; set; }
        public virtual ICollection<Setting> Settings { get; set; }
        public virtual ICollection<SystemLog> SystemLogs { get; set; }
    }
}
