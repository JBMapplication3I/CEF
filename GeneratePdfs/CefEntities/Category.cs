using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Category
    {
        public Category()
        {
            AuctionCategories = new HashSet<AuctionCategory>();
            BrandCategories = new HashSet<BrandCategory>();
            BrandMinimumForFreeShippingDollarAmountBufferCategories = new HashSet<Brand>();
            BrandMinimumForFreeShippingQuantityAmountBufferCategories = new HashSet<Brand>();
            BrandMinimumOrderDollarAmountBufferCategories = new HashSet<Brand>();
            BrandMinimumOrderQuantityAmountBufferCategories = new HashSet<Brand>();
            CategoryFiles = new HashSet<CategoryFile>();
            CategoryImages = new HashSet<CategoryImage>();
            DiscountCategories = new HashSet<DiscountCategory>();
            FavoriteCategories = new HashSet<FavoriteCategory>();
            FranchiseCategories = new HashSet<FranchiseCategory>();
            FranchiseMinimumForFreeShippingDollarAmountBufferCategories = new HashSet<Franchise>();
            FranchiseMinimumForFreeShippingQuantityAmountBufferCategories = new HashSet<Franchise>();
            FranchiseMinimumOrderDollarAmountBufferCategories = new HashSet<Franchise>();
            FranchiseMinimumOrderQuantityAmountBufferCategories = new HashSet<Franchise>();
            InverseMinimumForFreeShippingDollarAmountBufferCategory = new HashSet<Category>();
            InverseMinimumForFreeShippingQuantityAmountBufferCategory = new HashSet<Category>();
            InverseMinimumOrderDollarAmountBufferCategory = new HashSet<Category>();
            InverseMinimumOrderQuantityAmountBufferCategory = new HashSet<Category>();
            InverseParent = new HashSet<Category>();
            ListingCategories = new HashSet<ListingCategory>();
            LotCategories = new HashSet<LotCategory>();
            ManufacturerMinimumForFreeShippingDollarAmountBufferCategories = new HashSet<Manufacturer>();
            ManufacturerMinimumForFreeShippingQuantityAmountBufferCategories = new HashSet<Manufacturer>();
            ManufacturerMinimumOrderDollarAmountBufferCategories = new HashSet<Manufacturer>();
            ManufacturerMinimumOrderQuantityAmountBufferCategories = new HashSet<Manufacturer>();
            PageViews = new HashSet<PageView>();
            PriceRuleCategories = new HashSet<PriceRuleCategory>();
            ProductCategories = new HashSet<ProductCategory>();
            Reviews = new HashSet<Review>();
            SalesQuoteCategories = new HashSet<SalesQuoteCategory>();
            ScoutCategories = new HashSet<ScoutCategory>();
            StoreCategories = new HashSet<StoreCategory>();
            StoreMinimumForFreeShippingDollarAmountBufferCategories = new HashSet<Store>();
            StoreMinimumForFreeShippingQuantityAmountBufferCategories = new HashSet<Store>();
            StoreMinimumOrderDollarAmountBufferCategories = new HashSet<Store>();
            StoreMinimumOrderQuantityAmountBufferCategories = new HashSet<Store>();
            VendorMinimumForFreeShippingDollarAmountBufferCategories = new HashSet<Vendor>();
            VendorMinimumForFreeShippingQuantityAmountBufferCategories = new HashSet<Vendor>();
            VendorMinimumOrderDollarAmountBufferCategories = new HashSet<Vendor>();
            VendorMinimumOrderQuantityAmountBufferCategories = new HashSet<Vendor>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public string? JsonAttributes { get; set; }
        public string? SeoKeywords { get; set; }
        public string? SeoUrl { get; set; }
        public string? SeoPageTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoMetaData { get; set; }
        public int TypeId { get; set; }
        public string? RequiresRoles { get; set; }
        public string? DisplayName { get; set; }
        public int? SortOrder { get; set; }
        public bool IsVisible { get; set; }
        public bool IncludeInMenu { get; set; }
        public string? HeaderContent { get; set; }
        public string? SidebarContent { get; set; }
        public string? FooterContent { get; set; }
        public decimal? HandlingCharge { get; set; }
        public long? Hash { get; set; }
        public decimal? RestockingFeePercent { get; set; }
        public decimal? RestockingFeeAmount { get; set; }
        public int? RestockingFeeAmountCurrencyId { get; set; }
        public string? MinimumOrderDollarAmountWarningMessage { get; set; }
        public string? MinimumOrderQuantityAmountWarningMessage { get; set; }
        public decimal? MinimumOrderDollarAmount { get; set; }
        public decimal? MinimumOrderQuantityAmount { get; set; }
        public decimal? MinimumOrderDollarAmountAfter { get; set; }
        public decimal? MinimumOrderQuantityAmountAfter { get; set; }
        public int? MinimumOrderDollarAmountBufferProductId { get; set; }
        public int? MinimumOrderQuantityAmountBufferProductId { get; set; }
        public int? MinimumOrderDollarAmountBufferCategoryId { get; set; }
        public int? MinimumOrderQuantityAmountBufferCategoryId { get; set; }
        public decimal? MinimumOrderDollarAmountOverrideFee { get; set; }
        public bool MinimumOrderDollarAmountOverrideFeeIsPercent { get; set; }
        public string? MinimumOrderDollarAmountOverrideFeeWarningMessage { get; set; }
        public string? MinimumOrderDollarAmountOverrideFeeAcceptedMessage { get; set; }
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
        public string? RequiresRolesAlt { get; set; }
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
        public virtual Category? Parent { get; set; }
        public virtual Currency? RestockingFeeAmountCurrency { get; set; }
        public virtual CategoryType Type { get; set; } = null!;
        public virtual ICollection<AuctionCategory> AuctionCategories { get; set; }
        public virtual ICollection<BrandCategory> BrandCategories { get; set; }
        public virtual ICollection<Brand> BrandMinimumForFreeShippingDollarAmountBufferCategories { get; set; }
        public virtual ICollection<Brand> BrandMinimumForFreeShippingQuantityAmountBufferCategories { get; set; }
        public virtual ICollection<Brand> BrandMinimumOrderDollarAmountBufferCategories { get; set; }
        public virtual ICollection<Brand> BrandMinimumOrderQuantityAmountBufferCategories { get; set; }
        public virtual ICollection<CategoryFile> CategoryFiles { get; set; }
        public virtual ICollection<CategoryImage> CategoryImages { get; set; }
        public virtual ICollection<DiscountCategory> DiscountCategories { get; set; }
        public virtual ICollection<FavoriteCategory> FavoriteCategories { get; set; }
        public virtual ICollection<FranchiseCategory> FranchiseCategories { get; set; }
        public virtual ICollection<Franchise> FranchiseMinimumForFreeShippingDollarAmountBufferCategories { get; set; }
        public virtual ICollection<Franchise> FranchiseMinimumForFreeShippingQuantityAmountBufferCategories { get; set; }
        public virtual ICollection<Franchise> FranchiseMinimumOrderDollarAmountBufferCategories { get; set; }
        public virtual ICollection<Franchise> FranchiseMinimumOrderQuantityAmountBufferCategories { get; set; }
        public virtual ICollection<Category> InverseMinimumForFreeShippingDollarAmountBufferCategory { get; set; }
        public virtual ICollection<Category> InverseMinimumForFreeShippingQuantityAmountBufferCategory { get; set; }
        public virtual ICollection<Category> InverseMinimumOrderDollarAmountBufferCategory { get; set; }
        public virtual ICollection<Category> InverseMinimumOrderQuantityAmountBufferCategory { get; set; }
        public virtual ICollection<Category> InverseParent { get; set; }
        public virtual ICollection<ListingCategory> ListingCategories { get; set; }
        public virtual ICollection<LotCategory> LotCategories { get; set; }
        public virtual ICollection<Manufacturer> ManufacturerMinimumForFreeShippingDollarAmountBufferCategories { get; set; }
        public virtual ICollection<Manufacturer> ManufacturerMinimumForFreeShippingQuantityAmountBufferCategories { get; set; }
        public virtual ICollection<Manufacturer> ManufacturerMinimumOrderDollarAmountBufferCategories { get; set; }
        public virtual ICollection<Manufacturer> ManufacturerMinimumOrderQuantityAmountBufferCategories { get; set; }
        public virtual ICollection<PageView> PageViews { get; set; }
        public virtual ICollection<PriceRuleCategory> PriceRuleCategories { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<SalesQuoteCategory> SalesQuoteCategories { get; set; }
        public virtual ICollection<ScoutCategory> ScoutCategories { get; set; }
        public virtual ICollection<StoreCategory> StoreCategories { get; set; }
        public virtual ICollection<Store> StoreMinimumForFreeShippingDollarAmountBufferCategories { get; set; }
        public virtual ICollection<Store> StoreMinimumForFreeShippingQuantityAmountBufferCategories { get; set; }
        public virtual ICollection<Store> StoreMinimumOrderDollarAmountBufferCategories { get; set; }
        public virtual ICollection<Store> StoreMinimumOrderQuantityAmountBufferCategories { get; set; }
        public virtual ICollection<Vendor> VendorMinimumForFreeShippingDollarAmountBufferCategories { get; set; }
        public virtual ICollection<Vendor> VendorMinimumForFreeShippingQuantityAmountBufferCategories { get; set; }
        public virtual ICollection<Vendor> VendorMinimumOrderDollarAmountBufferCategories { get; set; }
        public virtual ICollection<Vendor> VendorMinimumOrderQuantityAmountBufferCategories { get; set; }
    }
}
