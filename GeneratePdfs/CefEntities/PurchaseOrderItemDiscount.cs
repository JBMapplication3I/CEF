using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class PurchaseOrderItemDiscount
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public decimal DiscountTotal { get; set; }
        public int SlaveId { get; set; }
        public int MasterId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public int? ApplicationsUsed { get; set; }
        public int? TargetApplicationsUsed { get; set; }

        public virtual PurchaseOrderItem Master { get; set; } = null!;
        public virtual Discount Slave { get; set; } = null!;
    }
}
