using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class EmailQueue
    {
        public EmailQueue()
        {
            EmailQueueAttachments = new HashSet<EmailQueueAttachment>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? AddressesTo { get; set; }
        public string? AddressesCc { get; set; }
        public string? AddressesBcc { get; set; }
        public string AddressFrom { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public bool IsHtml { get; set; }
        public int Attempts { get; set; }
        public bool HasError { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public int? EmailTemplateId { get; set; }
        public int? MessageRecipientId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual EmailTemplate? EmailTemplate { get; set; }
        public virtual MessageRecipient? MessageRecipient { get; set; }
        public virtual EmailStatus Status { get; set; } = null!;
        public virtual EmailType Type { get; set; } = null!;
        public virtual ICollection<EmailQueueAttachment> EmailQueueAttachments { get; set; }
    }
}
