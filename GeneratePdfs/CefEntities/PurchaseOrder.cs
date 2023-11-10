using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            Notes = new HashSet<Note>();
            PurchaseOrderContacts = new HashSet<PurchaseOrderContact>();
            PurchaseOrderDiscounts = new HashSet<PurchaseOrderDiscount>();
            PurchaseOrderEvents = new HashSet<PurchaseOrderEvent>();
            PurchaseOrderFiles = new HashSet<PurchaseOrderFile>();
            PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
            RateQuotes = new HashSet<RateQuote>();
            SalesOrderPurchaseOrders = new HashSet<SalesOrderPurchaseOrder>();
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
        public DateTime? ReleaseDate { get; set; }
        public DateTime? EstimatedReceiptDate { get; set; }
        public DateTime? ActualReceiptDate { get; set; }
        public string? TrackingNumber { get; set; }
        public int? InventoryLocationId { get; set; }
        public int? ShipCarrierId { get; set; }
        public int? VendorId { get; set; }
        public long? Hash { get; set; }
        public int? SalesGroupId { get; set; }
        public int? BrandId { get; set; }
        public int? FranchiseId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Contact? BillingContact { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Franchise? Franchise { get; set; }
        public virtual InventoryLocation? InventoryLocation { get; set; }
        public virtual SalesGroup? SalesGroup { get; set; }
        public virtual ShipCarrier? ShipCarrier { get; set; }
        public virtual Contact? ShippingContact { get; set; }
        public virtual PurchaseOrderState State { get; set; } = null!;
        public virtual PurchaseOrderStatus Status { get; set; } = null!;
        public virtual Store? Store { get; set; }
        public virtual PurchaseOrderType Type { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual Vendor? Vendor { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<PurchaseOrderContact> PurchaseOrderContacts { get; set; }
        public virtual ICollection<PurchaseOrderDiscount> PurchaseOrderDiscounts { get; set; }
        public virtual ICollection<PurchaseOrderEvent> PurchaseOrderEvents { get; set; }
        public virtual ICollection<PurchaseOrderFile> PurchaseOrderFiles { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual ICollection<RateQuote> RateQuotes { get; set; }
        public virtual ICollection<SalesOrderPurchaseOrder> SalesOrderPurchaseOrders { get; set; }
    }
}
