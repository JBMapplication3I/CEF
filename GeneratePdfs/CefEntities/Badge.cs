using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Badge
    {
        public Badge()
        {
            BadgeImages = new HashSet<BadgeImage>();
            StoreBadges = new HashSet<StoreBadge>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TypeId { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual BadgeType Type { get; set; } = null!;
        public virtual ICollection<BadgeImage> BadgeImages { get; set; }
        public virtual ICollection<StoreBadge> StoreBadges { get; set; }
    }
}
