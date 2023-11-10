using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ProductRestriction
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public bool CanPurchaseInternationally { get; set; }
        public bool CanPurchaseDomestically { get; set; }
        public bool CanPurchaseIntraRegion { get; set; }
        public bool CanShipInternationally { get; set; }
        public bool CanShipDomestically { get; set; }
        public bool CanShipIntraRegion { get; set; }
        public int ProductId { get; set; }
        public string? RestrictionsApplyToCity { get; set; }
        public string? RestrictionsApplyToPostalCode { get; set; }
        public int? RestrictionsApplyToCountryId { get; set; }
        public int? RestrictionsApplyToRegionId { get; set; }
        public string? OverrideWithRoles { get; set; }
        public int? OverrideWithAccountTypeId { get; set; }

        public virtual AccountType? OverrideWithAccountType { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual Country? RestrictionsApplyToCountry { get; set; }
        public virtual Region? RestrictionsApplyToRegion { get; set; }
    }
}
