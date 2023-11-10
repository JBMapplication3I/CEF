using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class HistoricalCurrencyRate
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public decimal Rate { get; set; }
        public DateTime OnDate { get; set; }
        public int StartingCurrencyId { get; set; }
        public int EndingCurrencyId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Currency EndingCurrency { get; set; } = null!;
        public virtual Currency StartingCurrency { get; set; } = null!;
    }
}
