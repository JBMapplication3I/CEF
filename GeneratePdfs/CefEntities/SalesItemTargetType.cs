using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SalesItemTargetType
    {
        public SalesItemTargetType()
        {
            CartItemTargets = new HashSet<CartItemTarget>();
            PurchaseOrderItemTargets = new HashSet<PurchaseOrderItemTarget>();
            SalesInvoiceItemTargets = new HashSet<SalesInvoiceItemTarget>();
            SalesOrderItemTargets = new HashSet<SalesOrderItemTarget>();
            SalesQuoteItemTargets = new HashSet<SalesQuoteItemTarget>();
            SalesReturnItemTargets = new HashSet<SalesReturnItemTarget>();
            SampleRequestItemTargets = new HashSet<SampleRequestItemTarget>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DisplayName { get; set; }
        public int? SortOrder { get; set; }
        public string? TranslationKey { get; set; }

        public virtual ICollection<CartItemTarget> CartItemTargets { get; set; }
        public virtual ICollection<PurchaseOrderItemTarget> PurchaseOrderItemTargets { get; set; }
        public virtual ICollection<SalesInvoiceItemTarget> SalesInvoiceItemTargets { get; set; }
        public virtual ICollection<SalesOrderItemTarget> SalesOrderItemTargets { get; set; }
        public virtual ICollection<SalesQuoteItemTarget> SalesQuoteItemTargets { get; set; }
        public virtual ICollection<SalesReturnItemTarget> SalesReturnItemTargets { get; set; }
        public virtual ICollection<SampleRequestItemTarget> SampleRequestItemTargets { get; set; }
    }
}
