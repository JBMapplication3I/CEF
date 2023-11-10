using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Package
    {
        public Package()
        {
            ProductMasterPacks = new HashSet<Product>();
            ProductPackages = new HashSet<Product>();
            ProductPallets = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Width { get; set; }
        public string? WidthUnitOfMeasure { get; set; }
        public decimal Depth { get; set; }
        public string? DepthUnitOfMeasure { get; set; }
        public decimal Height { get; set; }
        public string? HeightUnitOfMeasure { get; set; }
        public decimal Weight { get; set; }
        public string? WeightUnitOfMeasure { get; set; }
        public decimal DimensionalWeight { get; set; }
        public string? DimensionalWeightUnitOfMeasure { get; set; }
        public bool IsCustom { get; set; }
        public int TypeId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual PackageType Type { get; set; } = null!;
        public virtual ICollection<Product> ProductMasterPacks { get; set; }
        public virtual ICollection<Product> ProductPackages { get; set; }
        public virtual ICollection<Product> ProductPallets { get; set; }
    }
}
