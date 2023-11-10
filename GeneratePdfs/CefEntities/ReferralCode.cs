using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ReferralCode
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? JsonAttributes { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public string Code { get; set; } = null!;
        public int UserId { get; set; }

        public virtual ReferralCodeStatus Status { get; set; } = null!;
        public virtual ReferralCodeType Type { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
