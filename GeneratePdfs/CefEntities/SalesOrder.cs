using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            Appointments = new HashSet<Appointment>();
            Notes = new HashSet<Note>();
            RateQuotes = new HashSet<RateQuote>();
            SalesOrderContacts = new HashSet<SalesOrderContact>();
            SalesOrderDiscounts = new HashSet<SalesOrderDiscount>();
            SalesOrderEvents = new HashSet<SalesOrderEvent>();
            SalesOrderFiles = new HashSet<SalesOrderFile>();
            SalesOrderItems = new HashSet<SalesOrderItem>();
            SalesOrderPayments = new HashSet<SalesOrderPayment>();
            SalesOrderPurchaseOrders = new HashSet<SalesOrderPurchaseOrder>();
            SalesOrderSalesInvoices = new HashSet<SalesOrderSalesInvoice>();
            SalesQuoteSalesOrders = new HashSet<SalesQuoteSalesOrder>();
            SalesReturnSalesOrders = new HashSet<SalesReturnSalesOrder>();
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
        public decimal SubtotalDiscounts { get; set; }
        public decimal SubtotalFees { get; set; }
        public decimal SubtotalHandling { get; set; }
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
        public string? PurchaseOrderNumber { get; set; }
        public decimal? BalanceDue { get; set; }
        public string? TrackingNumber { get; set; }
        public string? PaymentTransactionId { get; set; }
        public string? TaxTransactionId { get; set; }
        public DateTime? OrderApprovedDate { get; set; }
        public DateTime? OrderCommitmentDate { get; set; }
        public DateTime? RequiredShipDate { get; set; }
        public DateTime? RequestedShipDate { get; set; }
        public DateTime? ActualShipDate { get; set; }
        public long? Hash { get; set; }
        public int? SalesGroupAsMasterId { get; set; }
        public int? SalesGroupAsSubId { get; set; }
        public int? BrandId { get; set; }
        public int? InventoryLocationId { get; set; }
        public int? FranchiseId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Contact? BillingContact { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Franchise? Franchise { get; set; }
        public virtual InventoryLocation? InventoryLocation { get; set; }
        public virtual SalesGroup? SalesGroupAsMaster { get; set; }
        public virtual SalesGroup? SalesGroupAsSub { get; set; }
        public virtual Contact? ShippingContact { get; set; }
        public virtual SalesOrderState State { get; set; } = null!;
        public virtual SalesOrderStatus Status { get; set; } = null!;
        public virtual Store? Store { get; set; }
        public virtual SalesOrderType Type { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<RateQuote> RateQuotes { get; set; }
        public virtual ICollection<SalesOrderContact> SalesOrderContacts { get; set; }
        public virtual ICollection<SalesOrderDiscount> SalesOrderDiscounts { get; set; }
        public virtual ICollection<SalesOrderEvent> SalesOrderEvents { get; set; }
        public virtual ICollection<SalesOrderFile> SalesOrderFiles { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; }
        public virtual ICollection<SalesOrderPayment> SalesOrderPayments { get; set; }
        public virtual ICollection<SalesOrderPurchaseOrder> SalesOrderPurchaseOrders { get; set; }
        public virtual ICollection<SalesOrderSalesInvoice> SalesOrderSalesInvoices { get; set; }
        public virtual ICollection<SalesQuoteSalesOrder> SalesQuoteSalesOrders { get; set; }
        public virtual ICollection<SalesReturnSalesOrder> SalesReturnSalesOrders { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
