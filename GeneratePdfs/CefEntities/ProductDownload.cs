using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ProductDownload
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public bool IsAbsoluteUrl { get; set; }
        public string? AbsoluteUrl { get; set; }
        public string? RelativeUrl { get; set; }
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual ProductDownloadType Type { get; set; } = null!;
    }
}
