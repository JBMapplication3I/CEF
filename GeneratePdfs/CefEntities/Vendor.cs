using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Vendor
    {
        public Vendor()
        {
            BrandVendors = new HashSet<BrandVendor>();
            DiscountVendors = new HashSet<DiscountVendor>();
            FavoriteVendors = new HashSet<FavoriteVendor>();
            FranchiseVendors = new HashSet<FranchiseVendor>();
            FutureImports = new HashSet<FutureImport>();
            NotesNavigation = new HashSet<Note>();
            PriceRuleVendors = new HashSet<PriceRuleVendor>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            Reviews = new HashSet<Review>();
            SalesQuotes = new HashSet<SalesQuote>();
            Shipments = new HashSet<Shipment>();
            StoreVendors = new HashSet<StoreVendor>();
            VendorAccounts = new HashSet<VendorAccount>();
            VendorImages = new HashSet<VendorImage>();
            VendorManufacturers = new HashSet<VendorManufacturer>();
            VendorProducts = new HashSet<VendorProduct>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ContactId { get; set; }
        public string? Notes { get; set; }
        public string? AccountNumber { get; set; }
        public string? Terms { get; set; }
        public string? TermNotes { get; set; }
        public string? SendMethod { get; set; }
        public string? EmailSubject { get; set; }
        public string? ShipTo { get; set; }
        public string? ShipViaNotes { get; set; }
        public string? SignBy { get; set; }
        public decimal? DefaultDiscount { get; set; }
        public bool AllowDropShip { get; set; }
        public decimal? RecommendedPurchaseOrderDollarAmount { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? MinimumOrderDollarAmountWarningMessage { get; set; }
        public decimal? MinimumOrderDollarAmount { get; set; }
        public decimal? MinimumOrderDollarAmountAfter { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityToken { get; set; }
        public bool MustResetPassword { get; set; }
        public int TypeId { get; set; }
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

        public virtual Contact? Contact { get; set; }
        public virtual Category? MinimumForFreeShippingDollarAmountBufferCategory { get; set; }
        public virtual Product? MinimumForFreeShippingDollarAmountBufferProduct { get; set; }
        public virtual Category? MinimumForFreeShippingQuantityAmountBufferCategory { get; set; }
        public virtual Product? MinimumForFreeShippingQuantityAmountBufferProduct { get; set; }
        public virtual Category? MinimumOrderDollarAmountBufferCategory { get; set; }
        public virtual Product? MinimumOrderDollarAmountBufferProduct { get; set; }
        public virtual Category? MinimumOrderQuantityAmountBufferCategory { get; set; }
        public virtual Product? MinimumOrderQuantityAmountBufferProduct { get; set; }
        public virtual VendorType Type { get; set; } = null!;
        public virtual ICollection<BrandVendor> BrandVendors { get; set; }
        public virtual ICollection<DiscountVendor> DiscountVendors { get; set; }
        public virtual ICollection<FavoriteVendor> FavoriteVendors { get; set; }
        public virtual ICollection<FranchiseVendor> FranchiseVendors { get; set; }
        public virtual ICollection<FutureImport> FutureImports { get; set; }
        public virtual ICollection<Note> NotesNavigation { get; set; }
        public virtual ICollection<PriceRuleVendor> PriceRuleVendors { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<SalesQuote> SalesQuotes { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
        public virtual ICollection<StoreVendor> StoreVendors { get; set; }
        public virtual ICollection<VendorAccount> VendorAccounts { get; set; }
        public virtual ICollection<VendorImage> VendorImages { get; set; }
        public virtual ICollection<VendorManufacturer> VendorManufacturers { get; set; }
        public virtual ICollection<VendorProduct> VendorProducts { get; set; }
    }
}
