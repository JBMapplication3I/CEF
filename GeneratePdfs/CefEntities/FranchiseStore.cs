﻿using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class FranchiseStore
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public bool IsVisibleIn { get; set; }

        public virtual Franchise Master { get; set; } = null!;
        public virtual Store Slave { get; set; } = null!;
    }
}
