using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class InventoryLocation
    {
        public InventoryLocation()
        {
            BrandInventoryLocations = new HashSet<BrandInventoryLocation>();
            FranchiseInventoryLocations = new HashSet<FranchiseInventoryLocation>();
            InventoryLocationRegions = new HashSet<InventoryLocationRegion>();
            InventoryLocationSections = new HashSet<InventoryLocationSection>();
            InventoryLocationUsers = new HashSet<InventoryLocationUser>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SalesOrders = new HashSet<SalesOrder>();
            StoreInventoryLocations = new HashSet<StoreInventoryLocation>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public long? Hash { get; set; }
        public int? ContactId { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Contact? Contact { get; set; }
        public virtual ICollection<BrandInventoryLocation> BrandInventoryLocations { get; set; }
        public virtual ICollection<FranchiseInventoryLocation> FranchiseInventoryLocations { get; set; }
        public virtual ICollection<InventoryLocationRegion> InventoryLocationRegions { get; set; }
        public virtual ICollection<InventoryLocationSection> InventoryLocationSections { get; set; }
        public virtual ICollection<InventoryLocationUser> InventoryLocationUsers { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
        public virtual ICollection<StoreInventoryLocation> StoreInventoryLocations { get; set; }
    }
}
