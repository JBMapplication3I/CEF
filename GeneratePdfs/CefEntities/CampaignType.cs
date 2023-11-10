using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class CampaignType
    {
        public CampaignType()
        {
            Campaigns = new HashSet<Campaign>();
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
        public int? BrandId { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Store? Store { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
