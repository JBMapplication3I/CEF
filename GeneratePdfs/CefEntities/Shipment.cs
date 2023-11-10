using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Shipment
    {
        public Shipment()
        {
            Carts = new HashSet<Cart>();
            ShipmentEvents = new HashSet<ShipmentEvent>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Reference1 { get; set; }
        public string? Reference2 { get; set; }
        public string? Reference3 { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Destination { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? DateDelivered { get; set; }
        public decimal? NegotiatedRate { get; set; }
        public decimal? PublishedRate { get; set; }
        public int OriginContactId { get; set; }
        public int DestinationContactId { get; set; }
        public int? InventoryLocationSectionId { get; set; }
        public int? ShipCarrierId { get; set; }
        public int? ShipCarrierMethodId { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public int? VendorId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public DateTime? TargetShippingDate { get; set; }

        public virtual Contact DestinationContact { get; set; } = null!;
        public virtual InventoryLocationSection? InventoryLocationSection { get; set; }
        public virtual Contact OriginContact { get; set; } = null!;
        public virtual ShipCarrier? ShipCarrier { get; set; }
        public virtual ShipCarrierMethod? ShipCarrierMethod { get; set; }
        public virtual ShipmentStatus Status { get; set; } = null!;
        public virtual ShipmentType Type { get; set; } = null!;
        public virtual Vendor? Vendor { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<ShipmentEvent> ShipmentEvents { get; set; }
    }
}
