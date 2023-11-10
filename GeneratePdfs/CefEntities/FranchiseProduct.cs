using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class FranchiseProduct
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public decimal? PriceBase { get; set; }
        public decimal? PriceMsrp { get; set; }
        public decimal? PriceReduction { get; set; }
        public decimal? PriceSale { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public bool IsVisibleIn { get; set; }

        public virtual Franchise Master { get; set; } = null!;
        public virtual Product Slave { get; set; } = null!;
    }
}
