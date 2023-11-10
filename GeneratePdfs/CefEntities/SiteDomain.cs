using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SiteDomain
    {
        public SiteDomain()
        {
            BrandSiteDomains = new HashSet<BrandSiteDomain>();
            Events = new HashSet<Event>();
            FranchiseSiteDomains = new HashSet<FranchiseSiteDomain>();
            PageViews = new HashSet<PageView>();
            SiteDomainSocialProviders = new HashSet<SiteDomainSocialProvider>();
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? JsonAttributes { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? HeaderContent { get; set; }
        public string? FooterContent { get; set; }
        public string? SideBarContent { get; set; }
        public string? CatalogContent { get; set; }
        public string Url { get; set; } = null!;
        public string? AlternateUrl1 { get; set; }
        public string? AlternateUrl2 { get; set; }
        public string? AlternateUrl3 { get; set; }
        public long? Hash { get; set; }

        public virtual ICollection<BrandSiteDomain> BrandSiteDomains { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<FranchiseSiteDomain> FranchiseSiteDomains { get; set; }
        public virtual ICollection<PageView> PageViews { get; set; }
        public virtual ICollection<SiteDomainSocialProvider> SiteDomainSocialProviders { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
