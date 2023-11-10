using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class EmailQueueAttachment
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
        public int SlaveId { get; set; }
        public int MasterId { get; set; }
        public int CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string? SeoUrl { get; set; }
        public string? SeoKeywords { get; set; }
        public string? SeoPageTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoMetaData { get; set; }
        public int FileAccessTypeId { get; set; }
        public int? SortOrder { get; set; }

        public virtual User CreatedByUser { get; set; } = null!;
        public virtual EmailQueue Master { get; set; } = null!;
        public virtual StoredFile Slave { get; set; } = null!;
        public virtual User? UpdatedByUser { get; set; }
    }
}
