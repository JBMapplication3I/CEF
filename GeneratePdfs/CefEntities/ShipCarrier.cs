using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ShipCarrier
    {
        public ShipCarrier()
        {
            FavoriteShipCarriers = new HashSet<FavoriteShipCarrier>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            ShipCarrierMethods = new HashSet<ShipCarrierMethod>();
            Shipments = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PointOfContact { get; set; }
        public bool IsInbound { get; set; }
        public bool IsOutbound { get; set; }
        public string? Username { get; set; }
        public string? EncryptedPassword { get; set; }
        public string? Authentication { get; set; }
        public string? AccountNumber { get; set; }
        public string? SalesRep { get; set; }
        public DateTime? PickupTime { get; set; }
        public long? Hash { get; set; }
        public int? ContactId { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Contact? Contact { get; set; }
        public virtual ICollection<FavoriteShipCarrier> FavoriteShipCarriers { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<ShipCarrierMethod> ShipCarrierMethods { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
