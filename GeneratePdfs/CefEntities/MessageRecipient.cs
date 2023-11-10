using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class MessageRecipient
    {
        public MessageRecipient()
        {
            EmailQueues = new HashSet<EmailQueue>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public bool IsRead { get; set; }
        public bool IsArchived { get; set; }
        public bool HasSentAnEmail { get; set; }
        public DateTime? EmailSentAt { get; set; }
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public int? GroupId { get; set; }
        public long? Hash { get; set; }
        public DateTime? ReadAt { get; set; }
        public DateTime? ArchivedAt { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Group? Group { get; set; }
        public virtual Message Master { get; set; } = null!;
        public virtual User Slave { get; set; } = null!;
        public virtual ICollection<EmailQueue> EmailQueues { get; set; }
    }
}
