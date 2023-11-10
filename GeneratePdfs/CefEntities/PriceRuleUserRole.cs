using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class PriceRuleUserRole
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? RoleName { get; set; }
        public int PriceRuleId { get; set; }

        public virtual PriceRule PriceRule { get; set; } = null!;
    }
}
