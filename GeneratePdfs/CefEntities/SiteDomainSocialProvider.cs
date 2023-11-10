using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class SiteDomainSocialProvider
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Script { get; set; }
        public string? UrlValues { get; set; }
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual SiteDomain Master { get; set; } = null!;
        public virtual SocialProvider Slave { get; set; } = null!;
    }
}
