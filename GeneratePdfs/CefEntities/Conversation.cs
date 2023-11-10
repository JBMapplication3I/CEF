using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Conversation
    {
        public Conversation()
        {
            ConversationUsers = new HashSet<ConversationUser>();
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int? StoreId { get; set; }
        public long? Hash { get; set; }
        public bool? HasEnded { get; set; }
        public bool? CopyUserWhenEnded { get; set; }
        public string? JsonAttributes { get; set; }
        public int? BrandId { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Store? Store { get; set; }
        public virtual ICollection<ConversationUser> ConversationUsers { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
