using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ProductPricePoint
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int? StoreId { get; set; }
        public decimal? Price { get; set; }
        public decimal? PercentDiscount { get; set; }
        public decimal? MinQuantity { get; set; }
        public decimal? MaxQuantity { get; set; }
        public string? UnitOfMeasure { get; set; }
        public int SlaveId { get; set; }
        public int MasterId { get; set; }
        public int? PriceRoundingId { get; set; }
        public int? CurrencyId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? BrandId { get; set; }
        public int? FranchiseId { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Currency? Currency { get; set; }
        public virtual Franchise? Franchise { get; set; }
        public virtual Product Master { get; set; } = null!;
        public virtual PriceRounding? PriceRounding { get; set; }
        public virtual PricePoint Slave { get; set; } = null!;
        public virtual Store? Store { get; set; }
    }
}
