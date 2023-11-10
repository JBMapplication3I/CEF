using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ProductType
    {
        public ProductType()
        {
            DiscountProductTypes = new HashSet<DiscountProductType>();
            PriceRuleProductTypes = new HashSet<PriceRuleProductType>();
            Products = new HashSet<Product>();
            UserProductTypes = new HashSet<UserProductType>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DisplayName { get; set; }
        public int? SortOrder { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? TranslationKey { get; set; }

        public virtual ICollection<DiscountProductType> DiscountProductTypes { get; set; }
        public virtual ICollection<PriceRuleProductType> PriceRuleProductTypes { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<UserProductType> UserProductTypes { get; set; }
    }
}
