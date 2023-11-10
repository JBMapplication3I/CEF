using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Store
    {
        public Store()
        {
            AccountTypes = new HashSet<AccountType>();
            AdStores = new HashSet<AdStore>();
            BrandStores = new HashSet<BrandStore>();
            CampaignTypes = new HashSet<CampaignType>();
            CartTypes = new HashSet<CartType>();
            Carts = new HashSet<Cart>();
            Contractors = new HashSet<Contractor>();
            Conversations = new HashSet<Conversation>();
            DiscountStores = new HashSet<DiscountStore>();
            FavoriteStores = new HashSet<FavoriteStore>();
            FranchiseStores = new HashSet<FranchiseStore>();
            FutureImports = new HashSet<FutureImport>();
            Messages = new HashSet<Message>();
            Notes = new HashSet<Note>();
            Payments = new HashSet<Payment>();
            PriceRuleStores = new HashSet<PriceRuleStore>();
            ProductAssociations = new HashSet<ProductAssociation>();
            ProductPricePoints = new HashSet<ProductPricePoint>();
            ProductShipCarrierMethods = new HashSet<ProductShipCarrierMethod>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            RecordVersions = new HashSet<RecordVersion>();
            Reviews = new HashSet<Review>();
            SalesInvoices = new HashSet<SalesInvoice>();
            SalesOrders = new HashSet<SalesOrder>();
            SalesQuoteResponseAsStores = new HashSet<SalesQuote>();
            SalesQuoteStores = new HashSet<SalesQuote>();
            SalesReturns = new HashSet<SalesReturn>();
            SampleRequests = new HashSet<SampleRequest>();
            Settings = new HashSet<Setting>();
            StoreAccounts = new HashSet<StoreAccount>();
            StoreAuctions = new HashSet<StoreAuction>();
            StoreBadges = new HashSet<StoreBadge>();
            StoreCategories = new HashSet<StoreCategory>();
            StoreContacts = new HashSet<StoreContact>();
            StoreCountries = new HashSet<StoreCountry>();
            StoreDistricts = new HashSet<StoreDistrict>();
            StoreImages = new HashSet<StoreImage>();
            StoreInventoryLocations = new HashSet<StoreInventoryLocation>();
            StoreManufacturers = new HashSet<StoreManufacturer>();
            StoreProducts = new HashSet<StoreProduct>();
            StoreRegions = new HashSet<StoreRegion>();
            StoreSubscriptions = new HashSet<StoreSubscription>();
            StoreUsers = new HashSet<StoreUser>();
            StoreVendors = new HashSet<StoreVendor>();
            SystemLogs = new HashSet<SystemLog>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int TypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? JsonAttributes { get; set; }
        public string? SeoKeywords { get; set; }
        public string? SeoUrl { get; set; }
        public string? SeoPageTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoMetaData { get; set; }
        public string? Slogan { get; set; }
        public string? MissionStatement { get; set; }
        public string? About { get; set; }
        public string? Overview { get; set; }
        public string? ExternalUrl { get; set; }
        public string? OperatingHoursTimeZoneId { get; set; }
        public decimal? OperatingHoursMondayStart { get; set; }
        public decimal? OperatingHoursMondayEnd { get; set; }
        public decimal? OperatingHoursTuesdayStart { get; set; }
        public decimal? OperatingHoursTuesdayEnd { get; set; }
        public decimal? OperatingHoursWednesdayStart { get; set; }
        public decimal? OperatingHoursWednesdayEnd { get; set; }
        public decimal? OperatingHoursThursdayStart { get; set; }
        public decimal? OperatingHoursThursdayEnd { get; set; }
        public decimal? OperatingHoursFridayStart { get; set; }
        public decimal? OperatingHoursFridayEnd { get; set; }
        public decimal? OperatingHoursSaturdayStart { get; set; }
        public decimal? OperatingHoursSaturdayEnd { get; set; }
        public decimal? OperatingHoursSundayStart { get; set; }
        public decimal? OperatingHoursSundayEnd { get; set; }
        public string? OperatingHoursClosedStatement { get; set; }
        public int ContactId { get; set; }
        public int? LanguageId { get; set; }
        public long? Hash { get; set; }
        public string? MinimumOrderDollarAmountWarningMessage { get; set; }
        public decimal? MinimumOrderDollarAmount { get; set; }
        public decimal? MinimumOrderDollarAmountAfter { get; set; }
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
        public decimal? MinimumForFreeShippingDollarAmount { get; set; }
        public decimal? MinimumForFreeShippingDollarAmountAfter { get; set; }
        public string? MinimumForFreeShippingDollarAmountWarningMessage { get; set; }
        public string? MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage { get; set; }
        public decimal? MinimumForFreeShippingQuantityAmount { get; set; }
        public decimal? MinimumForFreeShippingQuantityAmountAfter { get; set; }
        public string? MinimumForFreeShippingQuantityAmountWarningMessage { get; set; }
        public string? MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage { get; set; }
        public int? MinimumOrderDollarAmountBufferProductId { get; set; }
        public int? MinimumOrderQuantityAmountBufferProductId { get; set; }
        public int? MinimumOrderDollarAmountBufferCategoryId { get; set; }
        public int? MinimumOrderQuantityAmountBufferCategoryId { get; set; }
        public int? MinimumForFreeShippingDollarAmountBufferProductId { get; set; }
        public int? MinimumForFreeShippingQuantityAmountBufferProductId { get; set; }
        public int? MinimumForFreeShippingDollarAmountBufferCategoryId { get; set; }
        public int? MinimumForFreeShippingQuantityAmountBufferCategoryId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? SortOrder { get; set; }
        public bool? DisplayInStorefront { get; set; }

        public virtual Contact Contact { get; set; } = null!;
        public virtual Language? Language { get; set; }
        public virtual Category? MinimumForFreeShippingDollarAmountBufferCategory { get; set; }
        public virtual Product? MinimumForFreeShippingDollarAmountBufferProduct { get; set; }
        public virtual Category? MinimumForFreeShippingQuantityAmountBufferCategory { get; set; }
        public virtual Product? MinimumForFreeShippingQuantityAmountBufferProduct { get; set; }
        public virtual Category? MinimumOrderDollarAmountBufferCategory { get; set; }
        public virtual Product? MinimumOrderDollarAmountBufferProduct { get; set; }
        public virtual Category? MinimumOrderQuantityAmountBufferCategory { get; set; }
        public virtual Product? MinimumOrderQuantityAmountBufferProduct { get; set; }
        public virtual StoreType Type { get; set; } = null!;
        public virtual ICollection<AccountType> AccountTypes { get; set; }
        public virtual ICollection<AdStore> AdStores { get; set; }
        public virtual ICollection<BrandStore> BrandStores { get; set; }
        public virtual ICollection<CampaignType> CampaignTypes { get; set; }
        public virtual ICollection<CartType> CartTypes { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Contractor> Contractors { get; set; }
        public virtual ICollection<Conversation> Conversations { get; set; }
        public virtual ICollection<DiscountStore> DiscountStores { get; set; }
        public virtual ICollection<FavoriteStore> FavoriteStores { get; set; }
        public virtual ICollection<FranchiseStore> FranchiseStores { get; set; }
        public virtual ICollection<FutureImport> FutureImports { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<PriceRuleStore> PriceRuleStores { get; set; }
        public virtual ICollection<ProductAssociation> ProductAssociations { get; set; }
        public virtual ICollection<ProductPricePoint> ProductPricePoints { get; set; }
        public virtual ICollection<ProductShipCarrierMethod> ProductShipCarrierMethods { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<RecordVersion> RecordVersions { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
        public virtual ICollection<SalesQuote> SalesQuoteResponseAsStores { get; set; }
        public virtual ICollection<SalesQuote> SalesQuoteStores { get; set; }
        public virtual ICollection<SalesReturn> SalesReturns { get; set; }
        public virtual ICollection<SampleRequest> SampleRequests { get; set; }
        public virtual ICollection<Setting> Settings { get; set; }
        public virtual ICollection<StoreAccount> StoreAccounts { get; set; }
        public virtual ICollection<StoreAuction> StoreAuctions { get; set; }
        public virtual ICollection<StoreBadge> StoreBadges { get; set; }
        public virtual ICollection<StoreCategory> StoreCategories { get; set; }
        public virtual ICollection<StoreContact> StoreContacts { get; set; }
        public virtual ICollection<StoreCountry> StoreCountries { get; set; }
        public virtual ICollection<StoreDistrict> StoreDistricts { get; set; }
        public virtual ICollection<StoreImage> StoreImages { get; set; }
        public virtual ICollection<StoreInventoryLocation> StoreInventoryLocations { get; set; }
        public virtual ICollection<StoreManufacturer> StoreManufacturers { get; set; }
        public virtual ICollection<StoreProduct> StoreProducts { get; set; }
        public virtual ICollection<StoreRegion> StoreRegions { get; set; }
        public virtual ICollection<StoreSubscription> StoreSubscriptions { get; set; }
        public virtual ICollection<StoreUser> StoreUsers { get; set; }
        public virtual ICollection<StoreVendor> StoreVendors { get; set; }
        public virtual ICollection<SystemLog> SystemLogs { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
