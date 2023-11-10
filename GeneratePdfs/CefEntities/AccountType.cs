using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class AccountType
    {
        public AccountType()
        {
            Accounts = new HashSet<Account>();
            DiscountAccountTypes = new HashSet<DiscountAccountType>();
            PriceRuleAccountTypes = new HashSet<PriceRuleAccountType>();
            ProductRestrictions = new HashSet<ProductRestriction>();
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
        public int? StoreId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public string? TranslationKey { get; set; }

        public virtual Store? Store { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<DiscountAccountType> DiscountAccountTypes { get; set; }
        public virtual ICollection<PriceRuleAccountType> PriceRuleAccountTypes { get; set; }
        public virtual ICollection<ProductRestriction> ProductRestrictions { get; set; }
    }
}
