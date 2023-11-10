using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ProductAssociation
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? JsonAttributes { get; set; }
        public decimal? Quantity { get; set; }
        public string? UnitOfMeasure { get; set; }
        public int? SortOrder { get; set; }
        public int TypeId { get; set; }
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public long? Hash { get; set; }
        public int? StoreId { get; set; }
        public int? BrandId { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Product Master { get; set; } = null!;
        public virtual Product Slave { get; set; } = null!;
        public virtual Store? Store { get; set; }
        public virtual ProductAssociationType Type { get; set; } = null!;
    }
}
