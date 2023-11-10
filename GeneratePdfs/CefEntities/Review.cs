using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Review
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TypeId { get; set; }
        public int? SortOrder { get; set; }
        public decimal Value { get; set; }
        public string? Comment { get; set; }
        public bool Approved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public int SubmittedByUserId { get; set; }
        public int? ApprovedByUserId { get; set; }
        public int? CategoryId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? ProductId { get; set; }
        public int? StoreId { get; set; }
        public int? UserId { get; set; }
        public int? VendorId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual User? ApprovedByUser { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Manufacturer? Manufacturer { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Store? Store { get; set; }
        public virtual User SubmittedByUser { get; set; } = null!;
        public virtual ReviewType Type { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual Vendor? Vendor { get; set; }
    }
}
