using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SampleRequestItem
    {
        public SampleRequestItem()
        {
            Notes = new HashSet<Note>();
            SampleRequestItemDiscounts = new HashSet<SampleRequestItemDiscount>();
            SampleRequestItemTargets = new HashSet<SampleRequestItemTarget>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal? QuantityBackOrdered { get; set; }
        public string? Sku { get; set; }
        public decimal UnitCorePrice { get; set; }
        public decimal? UnitSoldPrice { get; set; }
        public string? UnitOfMeasure { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public int MasterId { get; set; }
        public string? JsonAttributes { get; set; }
        public decimal? UnitCorePriceInSellingCurrency { get; set; }
        public decimal? UnitSoldPriceInSellingCurrency { get; set; }
        public int? OriginalCurrencyId { get; set; }
        public int? SellingCurrencyId { get; set; }
        public long? Hash { get; set; }
        public string? ForceUniqueLineItemKey { get; set; }
        public decimal? QuantityPreSold { get; set; }

        public virtual SampleRequest Master { get; set; } = null!;
        public virtual Currency? OriginalCurrency { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Currency? SellingCurrency { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<SampleRequestItemDiscount> SampleRequestItemDiscounts { get; set; }
        public virtual ICollection<SampleRequestItemTarget> SampleRequestItemTargets { get; set; }
    }
}
