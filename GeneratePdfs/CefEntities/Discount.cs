using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Discount
    {
        public Discount()
        {
            CartDiscounts = new HashSet<CartDiscount>();
            CartItemDiscounts = new HashSet<CartItemDiscount>();
            DiscountAccountTypes = new HashSet<DiscountAccountType>();
            DiscountAccounts = new HashSet<DiscountAccount>();
            DiscountBrands = new HashSet<DiscountBrand>();
            DiscountCategories = new HashSet<DiscountCategory>();
            DiscountCodes = new HashSet<DiscountCode>();
            DiscountCountries = new HashSet<DiscountCountry>();
            DiscountFranchises = new HashSet<DiscountFranchise>();
            DiscountManufacturers = new HashSet<DiscountManufacturer>();
            DiscountProductTypes = new HashSet<DiscountProductType>();
            DiscountProducts = new HashSet<DiscountProduct>();
            DiscountShipCarrierMethods = new HashSet<DiscountShipCarrierMethod>();
            DiscountStores = new HashSet<DiscountStore>();
            DiscountUserRoles = new HashSet<DiscountUserRole>();
            DiscountUsers = new HashSet<DiscountUser>();
            DiscountVendors = new HashSet<DiscountVendor>();
            PurchaseOrderDiscounts = new HashSet<PurchaseOrderDiscount>();
            PurchaseOrderItemDiscounts = new HashSet<PurchaseOrderItemDiscount>();
            SalesInvoiceDiscounts = new HashSet<SalesInvoiceDiscount>();
            SalesInvoiceItemDiscounts = new HashSet<SalesInvoiceItemDiscount>();
            SalesOrderDiscounts = new HashSet<SalesOrderDiscount>();
            SalesOrderItemDiscounts = new HashSet<SalesOrderItemDiscount>();
            SalesQuoteDiscounts = new HashSet<SalesQuoteDiscount>();
            SalesQuoteItemDiscounts = new HashSet<SalesQuoteItemDiscount>();
            SalesReturnDiscounts = new HashSet<SalesReturnDiscount>();
            SalesReturnItemDiscounts = new HashSet<SalesReturnItemDiscount>();
            SampleRequestDiscounts = new HashSet<SampleRequestDiscount>();
            SampleRequestItemDiscounts = new HashSet<SampleRequestItemDiscount>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public int RoundingOperation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? UsageLimitGlobally { get; set; }
        public int? Priority { get; set; }
        public bool CanCombine { get; set; }
        public int DiscountTypeId { get; set; }
        public int ValueType { get; set; }
        public int? RoundingType { get; set; }
        public long? Hash { get; set; }
        public bool IsAutoApplied { get; set; }
        public int? DiscountCompareOperator { get; set; }
        public decimal ThresholdAmount { get; set; }
        public string? JsonAttributes { get; set; }
        public decimal? BuyXvalue { get; set; }
        public decimal? GetYvalue { get; set; }
        public int? UsageLimitPerAccount { get; set; }
        public int? UsageLimitPerUser { get; set; }
        public int? UsageLimitPerCart { get; set; }

        public virtual ICollection<CartDiscount> CartDiscounts { get; set; }
        public virtual ICollection<CartItemDiscount> CartItemDiscounts { get; set; }
        public virtual ICollection<DiscountAccountType> DiscountAccountTypes { get; set; }
        public virtual ICollection<DiscountAccount> DiscountAccounts { get; set; }
        public virtual ICollection<DiscountBrand> DiscountBrands { get; set; }
        public virtual ICollection<DiscountCategory> DiscountCategories { get; set; }
        public virtual ICollection<DiscountCode> DiscountCodes { get; set; }
        public virtual ICollection<DiscountCountry> DiscountCountries { get; set; }
        public virtual ICollection<DiscountFranchise> DiscountFranchises { get; set; }
        public virtual ICollection<DiscountManufacturer> DiscountManufacturers { get; set; }
        public virtual ICollection<DiscountProductType> DiscountProductTypes { get; set; }
        public virtual ICollection<DiscountProduct> DiscountProducts { get; set; }
        public virtual ICollection<DiscountShipCarrierMethod> DiscountShipCarrierMethods { get; set; }
        public virtual ICollection<DiscountStore> DiscountStores { get; set; }
        public virtual ICollection<DiscountUserRole> DiscountUserRoles { get; set; }
        public virtual ICollection<DiscountUser> DiscountUsers { get; set; }
        public virtual ICollection<DiscountVendor> DiscountVendors { get; set; }
        public virtual ICollection<PurchaseOrderDiscount> PurchaseOrderDiscounts { get; set; }
        public virtual ICollection<PurchaseOrderItemDiscount> PurchaseOrderItemDiscounts { get; set; }
        public virtual ICollection<SalesInvoiceDiscount> SalesInvoiceDiscounts { get; set; }
        public virtual ICollection<SalesInvoiceItemDiscount> SalesInvoiceItemDiscounts { get; set; }
        public virtual ICollection<SalesOrderDiscount> SalesOrderDiscounts { get; set; }
        public virtual ICollection<SalesOrderItemDiscount> SalesOrderItemDiscounts { get; set; }
        public virtual ICollection<SalesQuoteDiscount> SalesQuoteDiscounts { get; set; }
        public virtual ICollection<SalesQuoteItemDiscount> SalesQuoteItemDiscounts { get; set; }
        public virtual ICollection<SalesReturnDiscount> SalesReturnDiscounts { get; set; }
        public virtual ICollection<SalesReturnItemDiscount> SalesReturnItemDiscounts { get; set; }
        public virtual ICollection<SampleRequestDiscount> SampleRequestDiscounts { get; set; }
        public virtual ICollection<SampleRequestItemDiscount> SampleRequestItemDiscounts { get; set; }
    }
}
