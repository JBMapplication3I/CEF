using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SalesQuote
    {
        public SalesQuote()
        {
            Notes = new HashSet<Note>();
            RateQuotes = new HashSet<RateQuote>();
            SalesQuoteCategories = new HashSet<SalesQuoteCategory>();
            SalesQuoteContacts = new HashSet<SalesQuoteContact>();
            SalesQuoteDiscounts = new HashSet<SalesQuoteDiscount>();
            SalesQuoteEvents = new HashSet<SalesQuoteEvent>();
            SalesQuoteFiles = new HashSet<SalesQuoteFile>();
            SalesQuoteItems = new HashSet<SalesQuoteItem>();
            SalesQuoteSalesOrders = new HashSet<SalesQuoteSalesOrder>();
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
        public long? Hash { get; set; }
        public int? SalesGroupAsRequestMasterId { get; set; }
        public int? SalesGroupAsResponseMasterId { get; set; }
        public int? ResponseAsVendorId { get; set; }
        public int? ResponseAsStoreId { get; set; }
        public int? BrandId { get; set; }
        public int? SalesGroupAsRequestSubId { get; set; }
        public int? SalesGroupAsResponseSubId { get; set; }
        public decimal? SubtotalShippingModifier { get; set; }
        public int? SubtotalShippingModifierMode { get; set; }
        public decimal? SubtotalTaxesModifier { get; set; }
        public int? SubtotalTaxesModifierMode { get; set; }
        public decimal? SubtotalFeesModifier { get; set; }
        public int? SubtotalFeesModifierMode { get; set; }
        public decimal? SubtotalHandlingModifier { get; set; }
        public int? SubtotalHandlingModifierMode { get; set; }
        public decimal? SubtotalDiscountsModifier { get; set; }
        public int? SubtotalDiscountsModifierMode { get; set; }
        public DateTime? RequestedShipDate { get; set; }
        public int? FranchiseId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Contact? BillingContact { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Franchise? Franchise { get; set; }
        public virtual Store? ResponseAsStore { get; set; }
        public virtual Vendor? ResponseAsVendor { get; set; }
        public virtual SalesGroup? SalesGroupAsRequestMaster { get; set; }
        public virtual SalesGroup? SalesGroupAsRequestSub { get; set; }
        public virtual SalesGroup? SalesGroupAsResponseMaster { get; set; }
        public virtual SalesGroup? SalesGroupAsResponseSub { get; set; }
        public virtual Contact? ShippingContact { get; set; }
        public virtual SalesQuoteState State { get; set; } = null!;
        public virtual SalesQuoteStatus Status { get; set; } = null!;
        public virtual Store? Store { get; set; }
        public virtual SalesQuoteType Type { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<RateQuote> RateQuotes { get; set; }
        public virtual ICollection<SalesQuoteCategory> SalesQuoteCategories { get; set; }
        public virtual ICollection<SalesQuoteContact> SalesQuoteContacts { get; set; }
        public virtual ICollection<SalesQuoteDiscount> SalesQuoteDiscounts { get; set; }
        public virtual ICollection<SalesQuoteEvent> SalesQuoteEvents { get; set; }
        public virtual ICollection<SalesQuoteFile> SalesQuoteFiles { get; set; }
        public virtual ICollection<SalesQuoteItem> SalesQuoteItems { get; set; }
        public virtual ICollection<SalesQuoteSalesOrder> SalesQuoteSalesOrders { get; set; }
    }
}
