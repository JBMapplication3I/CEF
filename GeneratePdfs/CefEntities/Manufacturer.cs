using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            BrandManufacturers = new HashSet<BrandManufacturer>();
            DiscountManufacturers = new HashSet<DiscountManufacturer>();
            FavoriteManufacturers = new HashSet<FavoriteManufacturer>();
            FranchiseManufacturers = new HashSet<FranchiseManufacturer>();
            ManufacturerImages = new HashSet<ManufacturerImage>();
            ManufacturerProducts = new HashSet<ManufacturerProduct>();
            Notes = new HashSet<Note>();
            PriceRuleManufacturers = new HashSet<PriceRuleManufacturer>();
            Reviews = new HashSet<Review>();
            StoreManufacturers = new HashSet<StoreManufacturer>();
            VendorManufacturers = new HashSet<VendorManufacturer>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public long? Hash { get; set; }
        public int? ContactId { get; set; }
        public string? JsonAttributes { get; set; }
        public int TypeId { get; set; }
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

        public virtual Contact? Contact { get; set; }
        public virtual Category? MinimumForFreeShippingDollarAmountBufferCategory { get; set; }
        public virtual Product? MinimumForFreeShippingDollarAmountBufferProduct { get; set; }
        public virtual Category? MinimumForFreeShippingQuantityAmountBufferCategory { get; set; }
        public virtual Product? MinimumForFreeShippingQuantityAmountBufferProduct { get; set; }
        public virtual Category? MinimumOrderDollarAmountBufferCategory { get; set; }
        public virtual Product? MinimumOrderDollarAmountBufferProduct { get; set; }
        public virtual Category? MinimumOrderQuantityAmountBufferCategory { get; set; }
        public virtual Product? MinimumOrderQuantityAmountBufferProduct { get; set; }
        public virtual ManufacturerType Type { get; set; } = null!;
        public virtual ICollection<BrandManufacturer> BrandManufacturers { get; set; }
        public virtual ICollection<DiscountManufacturer> DiscountManufacturers { get; set; }
        public virtual ICollection<FavoriteManufacturer> FavoriteManufacturers { get; set; }
        public virtual ICollection<FranchiseManufacturer> FranchiseManufacturers { get; set; }
        public virtual ICollection<ManufacturerImage> ManufacturerImages { get; set; }
        public virtual ICollection<ManufacturerProduct> ManufacturerProducts { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<PriceRuleManufacturer> PriceRuleManufacturers { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<StoreManufacturer> StoreManufacturers { get; set; }
        public virtual ICollection<VendorManufacturer> VendorManufacturers { get; set; }
    }
}
