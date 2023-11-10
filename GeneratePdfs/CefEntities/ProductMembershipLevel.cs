using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ProductMembershipLevel
    {
        public ProductMembershipLevel()
        {
            Subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public int MembershipRepeatTypeId { get; set; }

        public virtual Product Master { get; set; } = null!;
        public virtual MembershipRepeatType MembershipRepeatType { get; set; } = null!;
        public virtual MembershipLevel Slave { get; set; } = null!;
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
