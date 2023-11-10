using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class EmailTemplate
    {
        public EmailTemplate()
        {
            EmailQueues = new HashSet<EmailQueue>();
            ScheduledJobConfigurations = new HashSet<ScheduledJobConfiguration>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual ICollection<EmailQueue> EmailQueues { get; set; }
        public virtual ICollection<ScheduledJobConfiguration> ScheduledJobConfigurations { get; set; }
    }
}
