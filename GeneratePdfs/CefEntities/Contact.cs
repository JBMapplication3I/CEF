using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Contact
    {
        public Contact()
        {
            AccountContacts = new HashSet<AccountContact>();
            Auctions = new HashSet<Auction>();
            CalendarEvents = new HashSet<CalendarEvent>();
            CartBillingContacts = new HashSet<Cart>();
            CartContacts = new HashSet<CartContact>();
            CartItemTargets = new HashSet<CartItemTarget>();
            CartShippingContacts = new HashSet<Cart>();
            ContactImages = new HashSet<ContactImage>();
            Events = new HashSet<Event>();
            InventoryLocations = new HashSet<InventoryLocation>();
            Listings = new HashSet<Listing>();
            Lots = new HashSet<Lot>();
            Manufacturers = new HashSet<Manufacturer>();
            PageViews = new HashSet<PageView>();
            Payments = new HashSet<Payment>();
            PurchaseOrderBillingContacts = new HashSet<PurchaseOrder>();
            PurchaseOrderContacts = new HashSet<PurchaseOrderContact>();
            PurchaseOrderItemTargets = new HashSet<PurchaseOrderItemTarget>();
            PurchaseOrderShippingContacts = new HashSet<PurchaseOrder>();
            SalesGroups = new HashSet<SalesGroup>();
            SalesInvoiceBillingContacts = new HashSet<SalesInvoice>();
            SalesInvoiceContacts = new HashSet<SalesInvoiceContact>();
            SalesInvoiceItemTargets = new HashSet<SalesInvoiceItemTarget>();
            SalesInvoiceShippingContacts = new HashSet<SalesInvoice>();
            SalesOrderBillingContacts = new HashSet<SalesOrder>();
            SalesOrderContacts = new HashSet<SalesOrderContact>();
            SalesOrderItemTargets = new HashSet<SalesOrderItemTarget>();
            SalesOrderShippingContacts = new HashSet<SalesOrder>();
            SalesQuoteBillingContacts = new HashSet<SalesQuote>();
            SalesQuoteContacts = new HashSet<SalesQuoteContact>();
            SalesQuoteItemTargets = new HashSet<SalesQuoteItemTarget>();
            SalesQuoteShippingContacts = new HashSet<SalesQuote>();
            SalesReturnBillingContacts = new HashSet<SalesReturn>();
            SalesReturnContacts = new HashSet<SalesReturnContact>();
            SalesReturnItemTargets = new HashSet<SalesReturnItemTarget>();
            SalesReturnShippingContacts = new HashSet<SalesReturn>();
            SampleRequestBillingContacts = new HashSet<SampleRequest>();
            SampleRequestContacts = new HashSet<SampleRequestContact>();
            SampleRequestItemTargets = new HashSet<SampleRequestItemTarget>();
            SampleRequestShippingContacts = new HashSet<SampleRequest>();
            ShipCarriers = new HashSet<ShipCarrier>();
            ShipmentDestinationContacts = new HashSet<Shipment>();
            ShipmentOriginContacts = new HashSet<Shipment>();
            StoreContacts = new HashSet<StoreContact>();
            Stores = new HashSet<Store>();
            Users = new HashSet<User>();
            Vendors = new HashSet<Vendor>();
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int TypeId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Phone3 { get; set; }
        public string? Fax1 { get; set; }
        public string? Email1 { get; set; }
        public string? Website1 { get; set; }
        public int? AddressId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Address? Address { get; set; }
        public virtual ContactType Type { get; set; } = null!;
        public virtual ICollection<AccountContact> AccountContacts { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }
        public virtual ICollection<Cart> CartBillingContacts { get; set; }
        public virtual ICollection<CartContact> CartContacts { get; set; }
        public virtual ICollection<CartItemTarget> CartItemTargets { get; set; }
        public virtual ICollection<Cart> CartShippingContacts { get; set; }
        public virtual ICollection<ContactImage> ContactImages { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<InventoryLocation> InventoryLocations { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
        public virtual ICollection<PageView> PageViews { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrderBillingContacts { get; set; }
        public virtual ICollection<PurchaseOrderContact> PurchaseOrderContacts { get; set; }
        public virtual ICollection<PurchaseOrderItemTarget> PurchaseOrderItemTargets { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrderShippingContacts { get; set; }
        public virtual ICollection<SalesGroup> SalesGroups { get; set; }
        public virtual ICollection<SalesInvoice> SalesInvoiceBillingContacts { get; set; }
        public virtual ICollection<SalesInvoiceContact> SalesInvoiceContacts { get; set; }
        public virtual ICollection<SalesInvoiceItemTarget> SalesInvoiceItemTargets { get; set; }
        public virtual ICollection<SalesInvoice> SalesInvoiceShippingContacts { get; set; }
        public virtual ICollection<SalesOrder> SalesOrderBillingContacts { get; set; }
        public virtual ICollection<SalesOrderContact> SalesOrderContacts { get; set; }
        public virtual ICollection<SalesOrderItemTarget> SalesOrderItemTargets { get; set; }
        public virtual ICollection<SalesOrder> SalesOrderShippingContacts { get; set; }
        public virtual ICollection<SalesQuote> SalesQuoteBillingContacts { get; set; }
        public virtual ICollection<SalesQuoteContact> SalesQuoteContacts { get; set; }
        public virtual ICollection<SalesQuoteItemTarget> SalesQuoteItemTargets { get; set; }
        public virtual ICollection<SalesQuote> SalesQuoteShippingContacts { get; set; }
        public virtual ICollection<SalesReturn> SalesReturnBillingContacts { get; set; }
        public virtual ICollection<SalesReturnContact> SalesReturnContacts { get; set; }
        public virtual ICollection<SalesReturnItemTarget> SalesReturnItemTargets { get; set; }
        public virtual ICollection<SalesReturn> SalesReturnShippingContacts { get; set; }
        public virtual ICollection<SampleRequest> SampleRequestBillingContacts { get; set; }
        public virtual ICollection<SampleRequestContact> SampleRequestContacts { get; set; }
        public virtual ICollection<SampleRequestItemTarget> SampleRequestItemTargets { get; set; }
        public virtual ICollection<SampleRequest> SampleRequestShippingContacts { get; set; }
        public virtual ICollection<ShipCarrier> ShipCarriers { get; set; }
        public virtual ICollection<Shipment> ShipmentDestinationContacts { get; set; }
        public virtual ICollection<Shipment> ShipmentOriginContacts { get; set; }
        public virtual ICollection<StoreContact> StoreContacts { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
