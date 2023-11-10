using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SalesGroup
    {
        public SalesGroup()
        {
            Notes = new HashSet<Note>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SalesInvoices = new HashSet<SalesInvoice>();
            SalesOrderSalesGroupAsMasters = new HashSet<SalesOrder>();
            SalesOrderSalesGroupAsSubs = new HashSet<SalesOrder>();
            SalesQuoteSalesGroupAsRequestMasters = new HashSet<SalesQuote>();
            SalesQuoteSalesGroupAsRequestSubs = new HashSet<SalesQuote>();
            SalesQuoteSalesGroupAsResponseMasters = new HashSet<SalesQuote>();
            SalesQuoteSalesGroupAsResponseSubs = new HashSet<SalesQuote>();
            SalesReturns = new HashSet<SalesReturn>();
            SampleRequests = new HashSet<SampleRequest>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public int? AccountId { get; set; }
        public int? BillingContactId { get; set; }
        public int? BrandId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Contact? BillingContact { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
        public virtual ICollection<SalesOrder> SalesOrderSalesGroupAsMasters { get; set; }
        public virtual ICollection<SalesOrder> SalesOrderSalesGroupAsSubs { get; set; }
        public virtual ICollection<SalesQuote> SalesQuoteSalesGroupAsRequestMasters { get; set; }
        public virtual ICollection<SalesQuote> SalesQuoteSalesGroupAsRequestSubs { get; set; }
        public virtual ICollection<SalesQuote> SalesQuoteSalesGroupAsResponseMasters { get; set; }
        public virtual ICollection<SalesQuote> SalesQuoteSalesGroupAsResponseSubs { get; set; }
        public virtual ICollection<SalesReturn> SalesReturns { get; set; }
        public virtual ICollection<SampleRequest> SampleRequests { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
