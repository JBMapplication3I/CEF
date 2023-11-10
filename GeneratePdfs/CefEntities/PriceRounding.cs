using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class PriceRounding
    {
        public PriceRounding()
        {
            ProductPricePoints = new HashSet<ProductPricePoint>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? PricePointKey { get; set; }
        public string? ProductKey { get; set; }
        public string? CurrencyKey { get; set; }
        public string? UnitOfMeasure { get; set; }
        public int RoundHow { get; set; }
        public int RoundTo { get; set; }
        public int RoundingAmount { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual ICollection<ProductPricePoint> ProductPricePoints { get; set; }
    }
}
