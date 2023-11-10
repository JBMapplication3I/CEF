using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class RoleUser
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? GroupId { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Group? Group { get; set; }
        public virtual UserRole Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
