using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class RateQuote
    {
        public RateQuote()
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
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? JsonAttributes { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public decimal? Rate { get; set; }
        public long? CartHash { get; set; }
        public DateTime? RateTimestamp { get; set; }
        public bool Selected { get; set; }
        public int ShipCarrierMethodId { get; set; }
        public int? CartId { get; set; }
        public int? SampleRequestId { get; set; }
        public int? SalesQuoteId { get; set; }
        public int? SalesOrderId { get; set; }
        public int? PurchaseOrderId { get; set; }
        public int? SalesInvoiceId { get; set; }
        public int? SalesReturnId { get; set; }
        public DateTime? TargetShippingDate { get; set; }

        public virtual Cart? Cart { get; set; }
        public virtual PurchaseOrder? PurchaseOrder { get; set; }
        public virtual SalesInvoice? SalesInvoice { get; set; }
        public virtual SalesOrder? SalesOrder { get; set; }
        public virtual SalesQuote? SalesQuote { get; set; }
        public virtual SalesReturn? SalesReturn { get; set; }
        public virtual SampleRequest? SampleRequest { get; set; }
        public virtual ShipCarrierMethod ShipCarrierMethod { get; set; } = null!;
        public virtual ICollection<CartItemTarget> CartItemTargets { get; set; }
        public virtual ICollection<PurchaseOrderItemTarget> PurchaseOrderItemTargets { get; set; }
        public virtual ICollection<SalesInvoiceItemTarget> SalesInvoiceItemTargets { get; set; }
        public virtual ICollection<SalesOrderItemTarget> SalesOrderItemTargets { get; set; }
        public virtual ICollection<SalesQuoteItemTarget> SalesQuoteItemTargets { get; set; }
        public virtual ICollection<SalesReturnItemTarget> SalesReturnItemTargets { get; set; }
        public virtual ICollection<SampleRequestItemTarget> SampleRequestItemTargets { get; set; }
    }
}
