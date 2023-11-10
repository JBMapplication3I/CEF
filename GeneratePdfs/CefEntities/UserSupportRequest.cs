using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class UserSupportRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? AuthKey { get; set; }
        public string? ChannelName { get; set; }
        public string? Status { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
