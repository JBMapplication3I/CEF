using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class PriceRuleProduct
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public bool OverridePrice { get; set; }
        public decimal? OverrideBasePrice { get; set; }
        public decimal? OverrideSalePrice { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual PriceRule Master { get; set; } = null!;
        public virtual Product Slave { get; set; } = null!;
    }
}
