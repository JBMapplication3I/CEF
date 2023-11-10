using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Message
    {
        public Message()
        {
            MessageAttachments = new HashSet<MessageAttachment>();
            MessageRecipients = new HashSet<MessageRecipient>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int? StoreId { get; set; }
        public string? Subject { get; set; }
        public string Body { get; set; } = null!;
        public string? Context { get; set; }
        public bool IsReplyAllAllowed { get; set; }
        public int? ConversationId { get; set; }
        public int? SentByUserId { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }
        public int? BrandId { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Conversation? Conversation { get; set; }
        public virtual User? SentByUser { get; set; }
        public virtual Store? Store { get; set; }
        public virtual ICollection<MessageAttachment> MessageAttachments { get; set; }
        public virtual ICollection<MessageRecipient> MessageRecipients { get; set; }
    }
}
