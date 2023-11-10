using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SampleRequestItemTarget
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public decimal Quantity { get; set; }
        public int MasterId { get; set; }
        public int DestinationContactId { get; set; }
        public int? OriginProductInventoryLocationSectionId { get; set; }
        public int? SelectedRateQuoteId { get; set; }
        public int TypeId { get; set; }
        public int? OriginStoreProductId { get; set; }
        public int? OriginVendorProductId { get; set; }
        public bool NothingToShip { get; set; }
        public int? BrandProductId { get; set; }

        public virtual BrandProduct? BrandProduct { get; set; }
        public virtual Contact DestinationContact { get; set; } = null!;
        public virtual SampleRequestItem Master { get; set; } = null!;
        public virtual ProductInventoryLocationSection? OriginProductInventoryLocationSection { get; set; }
        public virtual StoreProduct? OriginStoreProduct { get; set; }
        public virtual VendorProduct? OriginVendorProduct { get; set; }
        public virtual RateQuote? SelectedRateQuote { get; set; }
        public virtual SalesItemTargetType Type { get; set; } = null!;
    }
}
