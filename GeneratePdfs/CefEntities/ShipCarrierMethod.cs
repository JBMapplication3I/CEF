using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ShipCarrierMethod
    {
        public ShipCarrierMethod()
        {
            DiscountShipCarrierMethods = new HashSet<DiscountShipCarrierMethod>();
            ProductShipCarrierMethods = new HashSet<ProductShipCarrierMethod>();
            RateQuotes = new HashSet<RateQuote>();
            Shipments = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ShipCarrierId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual ShipCarrier ShipCarrier { get; set; } = null!;
        public virtual ICollection<DiscountShipCarrierMethod> DiscountShipCarrierMethods { get; set; }
        public virtual ICollection<ProductShipCarrierMethod> ProductShipCarrierMethods { get; set; }
        public virtual ICollection<RateQuote> RateQuotes { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
