using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class FutureImport
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? StoreId { get; set; }
        public string FileName { get; set; } = null!;
        public DateTime RunImportAt { get; set; }
        public int Attempts { get; set; }
        public bool HasError { get; set; }
        public int StatusId { get; set; }
        public int? VendorId { get; set; }

        public virtual FutureImportStatus Status { get; set; } = null!;
        public virtual Store? Store { get; set; }
        public virtual Vendor? Vendor { get; set; }
    }
}
