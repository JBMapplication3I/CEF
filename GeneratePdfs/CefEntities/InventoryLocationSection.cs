using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class InventoryLocationSection
    {
        public InventoryLocationSection()
        {
            ProductInventoryLocationSections = new HashSet<ProductInventoryLocationSection>();
            Shipments = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int InventoryLocationId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual InventoryLocation InventoryLocation { get; set; } = null!;
        public virtual ICollection<ProductInventoryLocationSection> ProductInventoryLocationSections { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
