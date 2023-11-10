using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SystemLog
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? StoreId { get; set; }
        public int? DataId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public int? LogLevel { get; set; }
        public int? BrandId { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Store? Store { get; set; }
    }
}
