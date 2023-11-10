using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Product
    {
        public Product()
        {
            AccountProducts = new HashSet<AccountProduct>();
            AccountUsageBalances = new HashSet<AccountUsageBalance>();
            BrandMinimumForFreeShippingDollarAmountBufferProducts = new HashSet<Brand>();
            BrandMinimumForFreeShippingQuantityAmountBufferProducts = new HashSet<Brand>();
            BrandMinimumOrderDollarAmountBufferProducts = new HashSet<Brand>();
            BrandMinimumOrderQuantityAmountBufferProducts = new HashSet<Brand>();
            BrandProducts = new HashSet<BrandProduct>();
            CalendarEventProducts = new HashSet<CalendarEventProduct>();
            CartItems = new HashSet<CartItem>();
            CategoryMinimumForFreeShippingDollarAmountBufferProducts = new HashSet<Category>();
            CategoryMinimumForFreeShippingQuantityAmountBufferProducts = new HashSet<Category>();
            CategoryMinimumOrderDollarAmountBufferProducts = new HashSet<Category>();
            CategoryMinimumOrderQuantityAmountBufferProducts = new HashSet<Category>();
            DiscountProducts = new HashSet<DiscountProduct>();
            FranchiseMinimumForFreeShippingDollarAmountBufferProducts = new HashSet<Franchise>();
            FranchiseMinimumForFreeShippingQuantityAmountBufferProducts = new HashSet<Franchise>();
            FranchiseMinimumOrderDollarAmountBufferProducts = new HashSet<Franchise>();
            FranchiseMinimumOrderQuantityAmountBufferProducts = new HashSet<Franchise>();
            FranchiseProducts = new HashSet<FranchiseProduct>();
            Listings = new HashSet<Listing>();
            Lots = new HashSet<Lot>();
            ManufacturerMinimumForFreeShippingDollarAmountBufferProducts = new HashSet<Manufacturer>();
            ManufacturerMinimumForFreeShippingQuantityAmountBufferProducts = new HashSet<Manufacturer>();
            ManufacturerMinimumOrderDollarAmountBufferProducts = new HashSet<Manufacturer>();
            ManufacturerMinimumOrderQuantityAmountBufferProducts = new HashSet<Manufacturer>();
            ManufacturerProducts = new HashSet<ManufacturerProduct>();
            PageViews = new HashSet<PageView>();
            PriceRuleProducts = new HashSet<PriceRuleProduct>();
            ProductAssociationMasters = new HashSet<ProductAssociation>();
            ProductAssociationSlaves = new HashSet<ProductAssociation>();
            ProductCategories = new HashSet<ProductCategory>();
            ProductDownloads = new HashSet<ProductDownload>();
            ProductFiles = new HashSet<ProductFile>();
            ProductImages = new HashSet<ProductImage>();
            ProductInventoryLocationSections = new HashSet<ProductInventoryLocationSection>();
            ProductMembershipLevels = new HashSet<ProductMembershipLevel>();
            ProductNotifications = new HashSet<ProductNotification>();
            ProductPricePoints = new HashSet<ProductPricePoint>();
            ProductRestrictions = new HashSet<ProductRestriction>();
            ProductShipCarrierMethods = new HashSet<ProductShipCarrierMethod>();
            ProductSubscriptionTypes = new HashSet<ProductSubscriptionType>();
            PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
            Reviews = new HashSet<Review>();
            SalesInvoiceItems = new HashSet<SalesInvoiceItem>();
            SalesOrderItems = new HashSet<SalesOrderItem>();
            SalesQuoteItems = new HashSet<SalesQuoteItem>();
            SalesReturnItems = new HashSet<SalesReturnItem>();
            SampleRequestItems = new HashSet<SampleRequestItem>();
            StoreMinimumForFreeShippingDollarAmountBufferProducts = new HashSet<Store>();
            StoreMinimumForFreeShippingQuantityAmountBufferProducts = new HashSet<Store>();
            StoreMinimumOrderDollarAmountBufferProducts = new HashSet<Store>();
            StoreMinimumOrderQuantityAmountBufferProducts = new HashSet<Store>();
            StoreProducts = new HashSet<StoreProduct>();
            VendorMinimumForFreeShippingDollarAmountBufferProducts = new HashSet<Vendor>();
            VendorMinimumForFreeShippingQuantityAmountBufferProducts = new HashSet<Vendor>();
            VendorMinimumOrderDollarAmountBufferProducts = new HashSet<Vendor>();
            VendorMinimumOrderQuantityAmountBufferProducts = new HashSet<Vendor>();
            VendorProducts = new HashSet<VendorProduct>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? JsonAttributes { get; set; }
        public string? SeoKeywords { get; set; }
        public string? SeoUrl { get; set; }
        public string? SeoPageTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoMetaData { get; set; }
        public string? RequiresRoles { get; set; }
        public decimal? Weight { get; set; }
        public string? WeightUnitOfMeasure { get; set; }
        public decimal? Width { get; set; }
        public string? WidthUnitOfMeasure { get; set; }
        public decimal? Depth { get; set; }
        public string? DepthUnitOfMeasure { get; set; }
        public decimal? Height { get; set; }
        public string? HeightUnitOfMeasure { get; set; }
        public string? ManufacturerPartNumber { get; set; }
        public string? ShortDescription { get; set; }
        public string? BrandName { get; set; }
        public decimal? PriceBase { get; set; }
        public decimal? PriceMsrp { get; set; }
        public decimal? PriceReduction { get; set; }
        public decimal? PriceSale { get; set; }
        public decimal? HandlingCharge { get; set; }
        public decimal? FlatShippingCharge { get; set; }
        public bool IsVisible { get; set; }
        public bool IsTaxable { get; set; }
        public string? TaxCode { get; set; }
        public bool IsFreeShipping { get; set; }
        public DateTime? AvailableStartDate { get; set; }
        public DateTime? AvailableEndDate { get; set; }
        public decimal? StockQuantity { get; set; }
        public decimal? StockQuantityAllocated { get; set; }
        public bool IsUnlimitedStock { get; set; }
        public bool AllowBackOrder { get; set; }
        public bool IsDiscontinued { get; set; }
        public string? UnitOfMeasure { get; set; }
        public decimal? MinimumPurchaseQuantity { get; set; }
        public decimal? MinimumPurchaseQuantityIfPastPurchased { get; set; }
        public decimal? MaximumPurchaseQuantity { get; set; }
        public decimal? MaximumPurchaseQuantityIfPastPurchased { get; set; }
        public decimal? QuantityPerMasterPack { get; set; }
        public decimal? QuantityMasterPackPerPallet { get; set; }
        public decimal? QuantityPerPallet { get; set; }
        public decimal? KitBaseQuantityPriceMultiplier { get; set; }
        public int? SortOrder { get; set; }
        public int? PackageId { get; set; }
        public int? MasterPackId { get; set; }
        public int? PalletId { get; set; }
        public int TypeId { get; set; }
        public decimal? QuantityMasterPackPerLayer { get; set; }
        public decimal? QuantityMasterPackLayersPerPallet { get; set; }
        public decimal? QuantityPerLayer { get; set; }
        public decimal? QuantityLayersPerPallet { get; set; }
        public decimal? TotalPurchasedAmount { get; set; }
        public int? TotalPurchasedAmountCurrencyId { get; set; }
        public decimal? TotalPurchasedQuantity { get; set; }
        public long? Hash { get; set; }
        public decimal? RestockingFeePercent { get; set; }
        public decimal? RestockingFeeAmount { get; set; }
        public bool IsEligibleForReturn { get; set; }
        public int? RestockingFeeAmountCurrencyId { get; set; }
        public bool NothingToShip { get; set; }
        public bool ShippingLeadTimeIsCalendarDays { get; set; }
        public int? ShippingLeadTimeDays { get; set; }
        public int StatusId { get; set; }
        public string? DocumentRequiredForPurchase { get; set; }
        public string? DocumentRequiredForPurchaseMissingWarningMessage { get; set; }
        public string? DocumentRequiredForPurchaseExpiredWarningMessage { get; set; }
        public decimal? DocumentRequiredForPurchaseOverrideFee { get; set; }
        public bool DocumentRequiredForPurchaseOverrideFeeIsPercent { get; set; }
        public string? DocumentRequiredForPurchaseOverrideFeeWarningMessage { get; set; }
        public string? DocumentRequiredForPurchaseOverrideFeeAcceptedMessage { get; set; }
        public decimal? MustPurchaseInMultiplesOfAmount { get; set; }
        public decimal? MustPurchaseInMultiplesOfAmountOverrideFee { get; set; }
        public bool MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent { get; set; }
        public string? MustPurchaseInMultiplesOfAmountOverrideFeeWarningMessage { get; set; }
        public string? MustPurchaseInMultiplesOfAmountOverrideFeeAcceptedMessage { get; set; }
        public string? RequiresRolesAlt { get; set; }
        public string? MustPurchaseInMultiplesOfAmountWarningMessage { get; set; }
        public bool AllowPreSale { get; set; }
        public DateTime? PreSellEndDate { get; set; }
        public decimal? StockQuantityPreSold { get; set; }
        public decimal? MaximumBackOrderPurchaseQuantity { get; set; }
        public decimal? MaximumBackOrderPurchaseQuantityIfPastPurchased { get; set; }
        public decimal? MaximumBackOrderPurchaseQuantityGlobal { get; set; }
        public decimal? MaximumPrePurchaseQuantity { get; set; }
        public decimal? MaximumPrePurchaseQuantityIfPastPurchased { get; set; }
        public decimal? MaximumPrePurchaseQuantityGlobal { get; set; }
        public bool DropShipOnly { get; set; }
        public string? IndexSynonyms { get; set; }

        public virtual Package? MasterPack { get; set; }
        public virtual Package? Package { get; set; }
        public virtual Package? Pallet { get; set; }
        public virtual Currency? RestockingFeeAmountCurrency { get; set; }
        public virtual ProductStatus Status { get; set; } = null!;
        public virtual Currency? TotalPurchasedAmountCurrency { get; set; }
        public virtual ProductType Type { get; set; } = null!;
        public virtual ICollection<AccountProduct> AccountProducts { get; set; }
        public virtual ICollection<AccountUsageBalance> AccountUsageBalances { get; set; }
        public virtual ICollection<Brand> BrandMinimumForFreeShippingDollarAmountBufferProducts { get; set; }
        public virtual ICollection<Brand> BrandMinimumForFreeShippingQuantityAmountBufferProducts { get; set; }
        public virtual ICollection<Brand> BrandMinimumOrderDollarAmountBufferProducts { get; set; }
        public virtual ICollection<Brand> BrandMinimumOrderQuantityAmountBufferProducts { get; set; }
        public virtual ICollection<BrandProduct> BrandProducts { get; set; }
        public virtual ICollection<CalendarEventProduct> CalendarEventProducts { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Category> CategoryMinimumForFreeShippingDollarAmountBufferProducts { get; set; }
        public virtual ICollection<Category> CategoryMinimumForFreeShippingQuantityAmountBufferProducts { get; set; }
        public virtual ICollection<Category> CategoryMinimumOrderDollarAmountBufferProducts { get; set; }
        public virtual ICollection<Category> CategoryMinimumOrderQuantityAmountBufferProducts { get; set; }
        public virtual ICollection<DiscountProduct> DiscountProducts { get; set; }
        public virtual ICollection<Franchise> FranchiseMinimumForFreeShippingDollarAmountBufferProducts { get; set; }
        public virtual ICollection<Franchise> FranchiseMinimumForFreeShippingQuantityAmountBufferProducts { get; set; }
        public virtual ICollection<Franchise> FranchiseMinimumOrderDollarAmountBufferProducts { get; set; }
        public virtual ICollection<Franchise> FranchiseMinimumOrderQuantityAmountBufferProducts { get; set; }
        public virtual ICollection<FranchiseProduct> FranchiseProducts { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
        public virtual ICollection<Manufacturer> ManufacturerMinimumForFreeShippingDollarAmountBufferProducts { get; set; }
        public virtual ICollection<Manufacturer> ManufacturerMinimumForFreeShippingQuantityAmountBufferProducts { get; set; }
        public virtual ICollection<Manufacturer> ManufacturerMinimumOrderDollarAmountBufferProducts { get; set; }
        public virtual ICollection<Manufacturer> ManufacturerMinimumOrderQuantityAmountBufferProducts { get; set; }
        public virtual ICollection<ManufacturerProduct> ManufacturerProducts { get; set; }
        public virtual ICollection<PageView> PageViews { get; set; }
        public virtual ICollection<PriceRuleProduct> PriceRuleProducts { get; set; }
        public virtual ICollection<ProductAssociation> ProductAssociationMasters { get; set; }
        public virtual ICollection<ProductAssociation> ProductAssociationSlaves { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<ProductDownload> ProductDownloads { get; set; }
        public virtual ICollection<ProductFile> ProductFiles { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductInventoryLocationSection> ProductInventoryLocationSections { get; set; }
        public virtual ICollection<ProductMembershipLevel> ProductMembershipLevels { get; set; }
        public virtual ICollection<ProductNotification> ProductNotifications { get; set; }
        public virtual ICollection<ProductPricePoint> ProductPricePoints { get; set; }
        public virtual ICollection<ProductRestriction> ProductRestrictions { get; set; }
        public virtual ICollection<ProductShipCarrierMethod> ProductShipCarrierMethods { get; set; }
        public virtual ICollection<ProductSubscriptionType> ProductSubscriptionTypes { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<SalesInvoiceItem> SalesInvoiceItems { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; }
        public virtual ICollection<SalesQuoteItem> SalesQuoteItems { get; set; }
        public virtual ICollection<SalesReturnItem> SalesReturnItems { get; set; }
        public virtual ICollection<SampleRequestItem> SampleRequestItems { get; set; }
        public virtual ICollection<Store> StoreMinimumForFreeShippingDollarAmountBufferProducts { get; set; }
        public virtual ICollection<Store> StoreMinimumForFreeShippingQuantityAmountBufferProducts { get; set; }
        public virtual ICollection<Store> StoreMinimumOrderDollarAmountBufferProducts { get; set; }
        public virtual ICollection<Store> StoreMinimumOrderQuantityAmountBufferProducts { get; set; }
        public virtual ICollection<StoreProduct> StoreProducts { get; set; }
        public virtual ICollection<Vendor> VendorMinimumForFreeShippingDollarAmountBufferProducts { get; set; }
        public virtual ICollection<Vendor> VendorMinimumForFreeShippingQuantityAmountBufferProducts { get; set; }
        public virtual ICollection<Vendor> VendorMinimumOrderDollarAmountBufferProducts { get; set; }
        public virtual ICollection<Vendor> VendorMinimumOrderQuantityAmountBufferProducts { get; set; }
        public virtual ICollection<VendorProduct> VendorProducts { get; set; }
    }
}
