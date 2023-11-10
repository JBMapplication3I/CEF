using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Membership
    {
        public Membership()
        {
            MembershipAdZoneAccesses = new HashSet<MembershipAdZoneAccess>();
            MembershipLevels = new HashSet<MembershipLevel>();
            MembershipRepeatTypes = new HashSet<MembershipRepeatType>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DisplayName { get; set; }
        public int? SortOrder { get; set; }
        public bool IsContractual { get; set; }
        public string? TranslationKey { get; set; }

        public virtual ICollection<MembershipAdZoneAccess> MembershipAdZoneAccesses { get; set; }
        public virtual ICollection<MembershipLevel> MembershipLevels { get; set; }
        public virtual ICollection<MembershipRepeatType> MembershipRepeatTypes { get; set; }
    }
}
