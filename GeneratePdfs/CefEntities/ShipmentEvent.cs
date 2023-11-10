using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ShipmentEvent
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Note { get; set; }
        public DateTime EventDate { get; set; }
        public int AddressId { get; set; }
        public int ShipmentId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Shipment Shipment { get; set; } = null!;
    }
}
