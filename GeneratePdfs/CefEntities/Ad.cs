using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Ad
    {
        public Ad()
        {
            AdAccounts = new HashSet<AdAccount>();
            AdBrands = new HashSet<AdBrand>();
            AdFranchises = new HashSet<AdFranchise>();
            AdImages = new HashSet<AdImage>();
            AdStores = new HashSet<AdStore>();
            AdZones = new HashSet<AdZone>();
            CampaignAds = new HashSet<CampaignAd>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public string TargetUrl { get; set; } = null!;
        public string? Caption { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Weight { get; set; }
        public int? ImpressionCounterId { get; set; }
        public int? ClickCounterId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Counter? ClickCounter { get; set; }
        public virtual Counter? ImpressionCounter { get; set; }
        public virtual AdStatus Status { get; set; } = null!;
        public virtual AdType Type { get; set; } = null!;
        public virtual ICollection<AdAccount> AdAccounts { get; set; }
        public virtual ICollection<AdBrand> AdBrands { get; set; }
        public virtual ICollection<AdFranchise> AdFranchises { get; set; }
        public virtual ICollection<AdImage> AdImages { get; set; }
        public virtual ICollection<AdStore> AdStores { get; set; }
        public virtual ICollection<AdZone> AdZones { get; set; }
        public virtual ICollection<CampaignAd> CampaignAds { get; set; }
    }
}
