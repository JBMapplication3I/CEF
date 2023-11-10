﻿using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class StoreVendor
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Store Master { get; set; } = null!;
        public virtual Vendor Slave { get; set; } = null!;
    }
}
