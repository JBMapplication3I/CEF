using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class DiscountCode
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string Code { get; set; } = null!;
        public int DiscountId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public int? UserId { get; set; }

        public virtual Discount Discount { get; set; } = null!;
        public virtual User? User { get; set; }
    }
}
