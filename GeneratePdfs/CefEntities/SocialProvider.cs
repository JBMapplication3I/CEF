using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SocialProvider
    {
        public SocialProvider()
        {
            SiteDomainSocialProviders = new HashSet<SiteDomainSocialProvider>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? UrlFormat { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual ICollection<SiteDomainSocialProvider> SiteDomainSocialProviders { get; set; }
    }
}
