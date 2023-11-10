﻿using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class AccountAssociation
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
        public int TypeId { get; set; }

        public virtual Account Master { get; set; } = null!;
        public virtual Account Slave { get; set; } = null!;
        public virtual AccountAssociationType Type { get; set; } = null!;
    }
}
