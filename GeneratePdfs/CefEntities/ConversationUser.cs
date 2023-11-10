using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class ConversationUser
    {
        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public DateTime? LastHeartbeat { get; set; }
        public bool? IsTyping { get; set; }
        public int MasterId { get; set; }
        public int SlaveId { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Conversation Master { get; set; } = null!;
        public virtual User Slave { get; set; } = null!;
    }
}
