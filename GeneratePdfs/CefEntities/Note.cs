using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Note
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string Note1 { get; set; } = null!;
        public int TypeId { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public int? PurchaseOrderId { get; set; }
        public int? SalesOrderId { get; set; }
        public int? AccountId { get; set; }
        public int? UserId { get; set; }
        public int? SalesInvoiceId { get; set; }
        public int? SalesQuoteId { get; set; }
        public int? CartId { get; set; }
        public int? VendorId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? SampleRequestId { get; set; }
        public long? Hash { get; set; }
        public int? SalesReturnId { get; set; }
        public int? SalesReturnItemId { get; set; }
        public string? JsonAttributes { get; set; }
        public int? SalesGroupId { get; set; }
        public int? PurchaseOrderItemId { get; set; }
        public int? SalesOrderItemId { get; set; }
        public int? SalesInvoiceItemId { get; set; }
        public int? SalesQuoteItemId { get; set; }
        public int? SampleRequestItemId { get; set; }
        public int? CartItemId { get; set; }
        public int? StoreId { get; set; }
        public int? BrandId { get; set; }
        public int? FranchiseId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual CartItem? CartItem { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual Franchise? Franchise { get; set; }
        public virtual Manufacturer? Manufacturer { get; set; }
        public virtual PurchaseOrder? PurchaseOrder { get; set; }
        public virtual PurchaseOrderItem? PurchaseOrderItem { get; set; }
        public virtual SalesGroup? SalesGroup { get; set; }
        public virtual SalesInvoice? SalesInvoice { get; set; }
        public virtual SalesInvoiceItem? SalesInvoiceItem { get; set; }
        public virtual SalesOrder? SalesOrder { get; set; }
        public virtual SalesOrderItem? SalesOrderItem { get; set; }
        public virtual SalesQuote? SalesQuote { get; set; }
        public virtual SalesQuoteItem? SalesQuoteItem { get; set; }
        public virtual SalesReturn? SalesReturn { get; set; }
        public virtual SalesReturnItem? SalesReturnItem { get; set; }
        public virtual SampleRequest? SampleRequest { get; set; }
        public virtual SampleRequestItem? SampleRequestItem { get; set; }
        public virtual Store? Store { get; set; }
        public virtual NoteType Type { get; set; } = null!;
        public virtual User? UpdatedByUser { get; set; }
        public virtual User? User { get; set; }
        public virtual Vendor? Vendor { get; set; }
    }
}
