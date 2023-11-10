using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SampleRequest
    {
        public SampleRequest()
        {
            CartContacts = new HashSet<CartContact>();
            Notes = new HashSet<Note>();
            RateQuotes = new HashSet<RateQuote>();
            SampleRequestContacts = new HashSet<SampleRequestContact>();
            SampleRequestDiscounts = new HashSet<SampleRequestDiscount>();
            SampleRequestEvents = new HashSet<SampleRequestEvent>();
            SampleRequestFiles = new HashSet<SampleRequestFile>();
            SampleRequestItems = new HashSet<SampleRequestItem>();
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
        public int? BrandId { get; set; }
        public int? SalesGroupId { get; set; }
        public int? FranchiseId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Contact? BillingContact { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Franchise? Franchise { get; set; }
        public virtual SalesGroup? SalesGroup { get; set; }
        public virtual Contact? ShippingContact { get; set; }
        public virtual SampleRequestState State { get; set; } = null!;
        public virtual SampleRequestStatus Status { get; set; } = null!;
        public virtual Store? Store { get; set; }
        public virtual SampleRequestType Type { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual ICollection<CartContact> CartContacts { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<RateQuote> RateQuotes { get; set; }
        public virtual ICollection<SampleRequestContact> SampleRequestContacts { get; set; }
        public virtual ICollection<SampleRequestDiscount> SampleRequestDiscounts { get; set; }
        public virtual ICollection<SampleRequestEvent> SampleRequestEvents { get; set; }
        public virtual ICollection<SampleRequestFile> SampleRequestFiles { get; set; }
        public virtual ICollection<SampleRequestItem> SampleRequestItems { get; set; }
    }
}
