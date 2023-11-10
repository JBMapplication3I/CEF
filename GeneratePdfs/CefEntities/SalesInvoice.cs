using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SalesInvoice
    {
        public SalesInvoice()
        {
            Notes = new HashSet<Note>();
            RateQuotes = new HashSet<RateQuote>();
            SalesInvoiceContacts = new HashSet<SalesInvoiceContact>();
            SalesInvoiceDiscounts = new HashSet<SalesInvoiceDiscount>();
            SalesInvoiceEvents = new HashSet<SalesInvoiceEvent>();
            SalesInvoiceFiles = new HashSet<SalesInvoiceFile>();
            SalesInvoiceItems = new HashSet<SalesInvoiceItem>();
            SalesInvoicePayments = new HashSet<SalesInvoicePayment>();
            SalesOrderSalesInvoices = new HashSet<SalesOrderSalesInvoice>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal SubtotalItems { get; set; }
        public decimal SubtotalShipping { get; set; }
        public decimal SubtotalTaxes { get; set; }
        public decimal SubtotalFees { get; set; }
        public decimal SubtotalHandling { get; set; }
        public decimal SubtotalDiscounts { get; set; }
        public decimal Total { get; set; }
        public bool? ShippingSameAsBilling { get; set; }
        public int? BillingContactId { get; set; }
        public int? ShippingContactId { get; set; }
        public int StatusId { get; set; }
        public int StateId { get; set; }
        public int TypeId { get; set; }
        public int? UserId { get; set; }
        public int? AccountId { get; set; }
        public string? JsonAttributes { get; set; }
        public int? StoreId { get; set; }
        public decimal? BalanceDue { get; set; }
        public long? Hash { get; set; }
        public int? SalesGroupId { get; set; }
        public int? BrandId { get; set; }
        public int? FranchiseId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Contact? BillingContact { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Franchise? Franchise { get; set; }
        public virtual SalesGroup? SalesGroup { get; set; }
        public virtual Contact? ShippingContact { get; set; }
        public virtual SalesInvoiceState State { get; set; } = null!;
        public virtual SalesInvoiceStatus Status { get; set; } = null!;
        public virtual Store? Store { get; set; }
        public virtual SalesInvoiceType Type { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<RateQuote> RateQuotes { get; set; }
        public virtual ICollection<SalesInvoiceContact> SalesInvoiceContacts { get; set; }
        public virtual ICollection<SalesInvoiceDiscount> SalesInvoiceDiscounts { get; set; }
        public virtual ICollection<SalesInvoiceEvent> SalesInvoiceEvents { get; set; }
        public virtual ICollection<SalesInvoiceFile> SalesInvoiceFiles { get; set; }
        public virtual ICollection<SalesInvoiceItem> SalesInvoiceItems { get; set; }
        public virtual ICollection<SalesInvoicePayment> SalesInvoicePayments { get; set; }
        public virtual ICollection<SalesOrderSalesInvoice> SalesOrderSalesInvoices { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
