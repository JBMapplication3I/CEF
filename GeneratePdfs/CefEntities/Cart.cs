using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Cart
    {
        public Cart()
        {
            CartContacts = new HashSet<CartContact>();
            CartDiscounts = new HashSet<CartDiscount>();
            CartEvents = new HashSet<CartEvent>();
            CartFiles = new HashSet<CartFile>();
            CartItems = new HashSet<CartItem>();
            Notes = new HashSet<Note>();
            RateQuotes = new HashSet<RateQuote>();
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
        public Guid? SessionId { get; set; }
        public DateTime? RequestedShipDate { get; set; }
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
        public long? Hash { get; set; }
        public int? ShipmentId { get; set; }
        public int? BrandId { get; set; }
        public int? FranchiseId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Contact? BillingContact { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Franchise? Franchise { get; set; }
        public virtual Shipment? Shipment { get; set; }
        public virtual Contact? ShippingContact { get; set; }
        public virtual CartState State { get; set; } = null!;
        public virtual CartStatus Status { get; set; } = null!;
        public virtual Store? Store { get; set; }
        public virtual CartType Type { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual ICollection<CartContact> CartContacts { get; set; }
        public virtual ICollection<CartDiscount> CartDiscounts { get; set; }
        public virtual ICollection<CartEvent> CartEvents { get; set; }
        public virtual ICollection<CartFile> CartFiles { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<RateQuote> RateQuotes { get; set; }
    }
}
