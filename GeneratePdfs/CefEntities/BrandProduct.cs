using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class BrandProduct
    {
        public BrandProduct()
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
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public decimal? PriceBase { get; set; }
        public decimal? PriceMsrp { get; set; }
        public decimal? PriceReduction { get; set; }
        public decimal? PriceSale { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public bool IsVisibleIn { get; set; }

        public virtual Brand Master { get; set; } = null!;
        public virtual Product Slave { get; set; } = null!;
        public virtual ICollection<CartItemTarget> CartItemTargets { get; set; }
        public virtual ICollection<PurchaseOrderItemTarget> PurchaseOrderItemTargets { get; set; }
        public virtual ICollection<SalesInvoiceItemTarget> SalesInvoiceItemTargets { get; set; }
        public virtual ICollection<SalesOrderItemTarget> SalesOrderItemTargets { get; set; }
        public virtual ICollection<SalesQuoteItemTarget> SalesQuoteItemTargets { get; set; }
        public virtual ICollection<SalesReturnItemTarget> SalesReturnItemTargets { get; set; }
        public virtual ICollection<SampleRequestItemTarget> SampleRequestItemTargets { get; set; }
    }
}
