using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class AccountUserRole
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Account Master { get; set; } = null!;
        public virtual UserRole Slave { get; set; } = null!;
    }
}
