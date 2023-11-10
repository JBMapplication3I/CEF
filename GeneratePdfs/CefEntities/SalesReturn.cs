using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SalesReturn
    {
        public SalesReturn()
        {
            Notes = new HashSet<Note>();
            RateQuotes = new HashSet<RateQuote>();
            SalesReturnContacts = new HashSet<SalesReturnContact>();
            SalesReturnDiscounts = new HashSet<SalesReturnDiscount>();
            SalesReturnEvents = new HashSet<SalesReturnEvent>();
            SalesReturnFiles = new HashSet<SalesReturnFile>();
            SalesReturnItems = new HashSet<SalesReturnItem>();
            SalesReturnPayments = new HashSet<SalesReturnPayment>();
            SalesReturnSalesOrders = new HashSet<SalesReturnSalesOrder>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
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
        public string? RefundTransactionId { get; set; }
        public string? TaxTransactionId { get; set; }
        public DateTime? ReturnApprovedDate { get; set; }
        public DateTime? ReturnCommitmentDate { get; set; }
        public DateTime? RequiredShipDate { get; set; }
        public DateTime? RequestedShipDate { get; set; }
        public DateTime? ActualShipDate { get; set; }
        public decimal? RefundAmount { get; set; }
        public int? SalesGroupId { get; set; }
        public int? BrandId { get; set; }
        public int? FranchiseId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Contact? BillingContact { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Franchise? Franchise { get; set; }
        public virtual SalesGroup? SalesGroup { get; set; }
        public virtual Contact? ShippingContact { get; set; }
        public virtual SalesReturnState State { get; set; } = null!;
        public virtual SalesReturnStatus Status { get; set; } = null!;
        public virtual Store? Store { get; set; }
        public virtual SalesReturnType Type { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<RateQuote> RateQuotes { get; set; }
        public virtual ICollection<SalesReturnContact> SalesReturnContacts { get; set; }
        public virtual ICollection<SalesReturnDiscount> SalesReturnDiscounts { get; set; }
        public virtual ICollection<SalesReturnEvent> SalesReturnEvents { get; set; }
        public virtual ICollection<SalesReturnFile> SalesReturnFiles { get; set; }
        public virtual ICollection<SalesReturnItem> SalesReturnItems { get; set; }
        public virtual ICollection<SalesReturnPayment> SalesReturnPayments { get; set; }
        public virtual ICollection<SalesReturnSalesOrder> SalesReturnSalesOrders { get; set; }
    }
}
