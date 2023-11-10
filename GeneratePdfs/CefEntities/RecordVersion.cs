using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class RecordVersion
    {
        public int Id { get; set; }
        public int? StoreId { get; set; }
        public int? BrandId { get; set; }
        public int TypeId { get; set; }
        public int? RecordId { get; set; }
        public DateTime? OriginalPublishDate { get; set; }
        public DateTime? MostRecentPublishDate { get; set; }
        public bool IsDraft { get; set; }
        public string? SerializedRecord { get; set; }
        public int? PublishedByUserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual User? PublishedByUser { get; set; }
        public virtual Store? Store { get; set; }
        public virtual RecordVersionType Type { get; set; } = null!;
    }
}
