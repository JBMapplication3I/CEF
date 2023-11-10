using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class CounterLog
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int TypeId { get; set; }
        public string? JsonAttributes { get; set; }
        public decimal? Value { get; set; }
        public int CounterId { get; set; }
        public long? Hash { get; set; }

        public virtual Counter Counter { get; set; } = null!;
        public virtual CounterLogType Type { get; set; } = null!;
    }
}
