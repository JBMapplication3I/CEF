using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Group
    {
        public Group()
        {
            CalendarEvents = new HashSet<CalendarEvent>();
            GroupUsers = new HashSet<GroupUser>();
            InverseParent = new HashSet<Group>();
            MessageRecipients = new HashSet<MessageRecipient>();
            RoleUsers = new HashSet<RoleUser>();
        }

        public int Id { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public string? JsonAttributes { get; set; }
        public int? GroupOwnerId { get; set; }
        public int? ParentId { get; set; }
        public long? Hash { get; set; }

        public virtual User? GroupOwner { get; set; }
        public virtual Group? Parent { get; set; }
        public virtual GroupStatus Status { get; set; } = null!;
        public virtual GroupType Type { get; set; } = null!;
        public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        public virtual ICollection<Group> InverseParent { get; set; }
        public virtual ICollection<MessageRecipient> MessageRecipients { get; set; }
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
    }
}
